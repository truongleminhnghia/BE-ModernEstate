# Stage 1: Restore and build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and restore
COPY *.sln .
COPY BE_ModernEstate.WebAPI/BE_ModernEstate.WebAPI.csproj BE_ModernEstate.WebAPI/
COPY ModernEstate.BLL/ModernEstate.BLL.csproj ModernEstate.BLL/
COPY ModernEstate.DAL/ModernEstate.DAL.csproj ModernEstate.DAL/
COPY ModernEstate.Common/ModernEstate.Common.csproj ModernEstate.Common/

RUN dotnet restore

# Copy the rest of the source
COPY . .

# Build the WebAPI project
RUN dotnet publish BE_ModernEstate.WebAPI/BE_ModernEstate.WebAPI.csproj -c Release -o out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "BE_ModernEstate.WebAPI.dll"]