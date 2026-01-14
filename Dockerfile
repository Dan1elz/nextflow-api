FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solução
COPY *.sln ./

# Copiar projetos (case correto)
COPY Nextflow/*.csproj ./Nextflow/
COPY Nextflow.Application/*.csproj ./Nextflow.Application/
COPY Nextflow.Domain/*.csproj ./Nextflow.Domain/
COPY Nextflow.Infrastructure/*.csproj ./Nextflow.Infrastructure/

# Configurar cache de pacotes NuGet
ENV NUGET_PACKAGES=/root/.nuget/packages
ENV NUGET_FALLBACK_PACKAGES=

# Restore
RUN dotnet restore

# Copiar tudo
COPY . .

# Publish
RUN dotnet publish Nextflow/Nextflow.csproj -c Release -o /app/publish /p:UseAppHost=false

# =====================

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN apt-get update && apt-get install -y --no-install-recommends curl \
  && rm -rf /var/lib/apt/lists/* \
  && adduser --disabled-password --gecos '' appuser \
  && chown -R appuser /app

USER appuser

ENV ASPNETCORE_URLS=http://+:8080 \
  ASPNETCORE_ENVIRONMENT=Production \
  DOTNET_RUNNING_IN_CONTAINER=true \
  DOTNET_USE_POLLING_FILE_WATCHER=false

COPY --from=build --chown=appuser:appuser /app/publish .

EXPOSE 8080

HEALTHCHECK --interval=30s --timeout=3s --start-period=40s --retries=3 \
  CMD curl --fail --silent --max-time 2 http://localhost:8080/health || exit 1

CMD ["dotnet", "Nextflow.dll"]
