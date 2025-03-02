# 1️⃣ Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copia archivos del proyecto y restaura dependencias
COPY ["Felix.Api/Felix.Api.csproj", "Felix.Api/"]
COPY ["Felix.Infrastructure/Felix.Infrastructure.csproj", "Felix.Infrastructure/"]
COPY ["Felix.Application/Felix.Application.csproj", "Felix.Application/"]
COPY ["Felix.Domain/Felix.Domain.csproj", "Felix.Domain/"]
RUN dotnet restore "Felix.Api/Felix.Api.csproj"

# Copia el código fuente y compila
COPY . .
WORKDIR "/src/Felix.Api"
RUN dotnet build "Felix.Api.csproj" -c Release -o /app/build

# 2️⃣ Fase de publicación
FROM build-env AS publish
RUN dotnet publish "Felix.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3️⃣ Fase final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Exponer puertos (si usas Docker Compose o Kubernetes)
EXPOSE 8080
EXPOSE 8081

# Iniciar la aplicación
ENTRYPOINT ["dotnet", "Felix.Api.dll"]
