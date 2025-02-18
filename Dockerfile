# Usar a imagem oficial do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos do projeto e restaurar dependências
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Usar a imagem do runtime do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 7004
ENTRYPOINT ["dotnet", "Trab1-PS.dll"]
