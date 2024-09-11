FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore --verbosity minimal
# Build and publish a release
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "api.dll"]