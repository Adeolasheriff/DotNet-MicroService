# # Use .NET 8 SDK to build the app
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# WORKDIR /app

# # Copy solution and project files
# # COPY *.sln .
# COPY *.csproj ./
# RUN dotnet restore

# COPY . ./


# RUN dotnet publish -c Release -o out

# # Use .NET 8 runtime image to run the app
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app

# ENV ASPNETCORE_URLS=http://+:5000


# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "PlatformService.dll"]
# Use .NET 8 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy and restore
COPY *.csproj ./
RUN dotnet restore

# Copy and publish
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80


COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PlatformService.dll"]
