# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything from the solution root
COPY . .

# Restore dependencies using the solution file
RUN dotnet restore "Backend-Restaurant.sln"

# Publish the entry project
RUN dotnet publish "API-Access/API-Access.csproj" -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "API-Access.dll"]
