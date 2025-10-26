# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./

COPY PatientOrdersService.Common/ ./PatientOrdersService.Common/
COPY PatientOrdersService/ ./PatientOrdersService/

WORKDIR /src/PatientOrdersService
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 5000
ENTRYPOINT ["dotnet", "PatientOrdersService.dll"]

