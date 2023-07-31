FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
ENV DATABASE_ADDRESS=172.16.9.73,33001
COPY --from=build-env /App/TaskPlannerMetrum/bin/Release/net7.0/ .
ENTRYPOINT ["dotnet", "TaskPlannerMetrum.dll"]
