FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./*.csproj ./AntilopaApi/
WORKDIR /app/AntilopaApi
RUN dotnet restore

# # copy and publish app and libraries
WORKDIR /app/
COPY ./ ./AntilopaApi/
# COPY utils/. ./utils/
WORKDIR /app/AntilopaApi
RUN dotnet publish -c Release -o out

#RUN dotnet ef database update

# # test application -- see: dotnet-docker-unit-testing.md
# FROM build AS testrunner
# WORKDIR /app/tests
# COPY tests/. .
# ENTRYPOINT ["dotnet", "test", "--logger:trx"]


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/AntilopaApi/out ./
ENTRYPOINT ["dotnet", "AntilopaApi.dll"]