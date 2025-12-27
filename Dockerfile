FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos de projeto para restaurar dependências (otimização de cache)
COPY *.sln ./
COPY nextflow/*.csproj ./nextflow/
COPY nextflow.Application/*.csproj ./nextflow.Application/
COPY nextflow.Domain/*.csproj ./nextflow.Domain/
COPY nextflow.Infrastructure/*.csproj ./nextflow.Infrastructure/

# Restaurar dependências
RUN dotnet restore

# Copiar todo o código fonte
COPY . .

# Publicar aplicação
RUN dotnet publish nextflow/Nextflow.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Instalar curl para healthcheck e criar usuário não-root
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/* \
    && adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_USE_POLLING_FILE_WATCHER=false

COPY --from=build --chown=appuser:appuser /app/publish .

EXPOSE 8080

# Healthcheck para verificar se a aplicação está respondendo
HEALTHCHECK --interval=30s --timeout=3s --start-period=40s --retries=3 \
  CMD curl --fail --silent --max-time 2 http://localhost:8080/health || exit 1

CMD ["dotnet", "Nextflow.dll"]