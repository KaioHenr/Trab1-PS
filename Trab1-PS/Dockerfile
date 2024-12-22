# Etapa 1: Construção da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
WORKDIR /src

# Copia os arquivos do projeto e restaura as dependências
COPY ["Trab1-PS.csproj", "./"]
RUN dotnet restore

# Copia todos os arquivos e compila o projeto
COPY . . 
RUN dotnet publish -c Release -o /app

# Etapa 2: Executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0  
WORKDIR /app

# Copia os arquivos da etapa de construção para a etapa final
COPY --from=build /app . 

# Expõe a porta usada pela aplicação
EXPOSE 7004
EXPOSE 5224

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "Trab1-PS.dll"]
