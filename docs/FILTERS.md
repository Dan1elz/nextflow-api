# Convenções de filtros (`filters`) no GetAll

Este projeto usa **um único parâmetro de query** chamado `filters` para enviar filtros para endpoints `GET /api/<resource>`.

- **Formato**: `filters` é um JSON (objeto) na query string.
- **Exemplo**:

  - `GET /api/cities?offset=0&limit=50&filters={"stateId":"<guid>","search":"Curitiba"}`

> Observação: no browser/cliente o JSON será URL-encoded automaticamente.

## Tipos suportados (recomendação de envio)

Mesmo quando o valor “parece” ser número/bool/data, **envie como string** sempre que possível.

### String (texto)
- **Enviar**: texto normal
- **Exemplos**:
  - `"search":"Paraná"`
  - `"name":"São"`

### Guid
- **Enviar**: string no formato padrão do .NET
- **Exemplo**:
  - `"stateId":"2f3b0a7f-7b8b-4a9b-9fd9-8f6f1c2c8c2a"`

### Boolean
- **Enviar**: `"true"` ou `"false"`
- **Também aceito (tolerância)**: `"1"`, `"0"`, `"sim"`, `"nao"`, `"on"`, `"off"`
- **Exemplo**:
  - `"isActive":"true"`

### Inteiro
- **Enviar**: string numérica sem separadores
- **Exemplo**:
  - `"status":"2"`

### Decimal / money
- **Recomendado**: usar ponto como separador decimal (Invariant Culture)
- **Exemplos**:
  - `"priceMin":"10.50"`
  - `"priceMax":"99.99"`

> O backend tenta tolerar `pt-BR` (vírgula) também, mas prefira `.` para evitar ambiguidades.

### Datas
#### DateOnly (somente data)
- **Recomendado**: ISO-8601: `yyyy-MM-dd`
- **Exemplo**:
  - `"birthDate":"2026-02-05"`

#### DateTime (data/hora)
- **Recomendado**: ISO-8601 com timezone: `yyyy-MM-ddTHH:mm:ssZ`
- **Exemplo**:
  - `"createdAtFrom":"2026-02-05T00:00:00Z"`
  - `"createdAtTo":"2026-02-05T23:59:59Z"`

## Convenção de chaves para range (sugestão)

Para filtros de intervalo, mantenha um padrão consistente:

- **Inteiros/decimais**: `Min` / `Max`
  - `"quantityMin":"10"`, `"quantityMax":"100"`
  - `"priceMin":"10.50"`, `"priceMax":"99.99"`
- **Datas**: `From` / `To`
  - `"createdAtFrom":"2026-02-05T00:00:00Z"`, `"createdAtTo":"2026-02-05T23:59:59Z"`
  - `"birthDateFrom":"2020-01-01"`, `"birthDateTo":"2026-12-31"`

## Observações de implementação

- O backend faz parsing tolerante de:
  - **bool** (`true/false`, `1/0`, etc.)
  - **números** (`InvariantCulture` e fallback `pt-BR`)
  - **datas** (`yyyy-MM-dd`, `dd/MM/yyyy`, e alguns ISO de DateTime)
- Chaves são tratadas como **case-insensitive** (`stateId` == `StateId`).

