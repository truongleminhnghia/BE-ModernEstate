# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["BE_ModernEstate.sln", "./"]
COPY ["BE_ModernEstate.WebAPI/BE_ModernEstate.WebAPI.csproj", "BE_ModernEstate.WebAPI/"]
COPY ["ModernEstate.BLL/ModernEstate.BLL.csproj", "ModernEstate.BLL/"]
COPY ["ModernEstate.DAL/ModernEstate.DAL.csproj", "ModernEstate.DAL/"]
COPY ["ModernEstate.Common/ModernEstate.Common.csproj", "ModernEstate.Common/"]

# Restore dependencies
RUN dotnet restore "BE_ModernEstate.sln"

# Copy the rest of the code
COPY . .

# Build the application
RUN dotnet build "BE_ModernEstate.sln" -c Release -o /app/build

# Publish the application
RUN dotnet publish "BE_ModernEstate.WebAPI/BE_ModernEstate.WebAPI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published application
COPY --from=build /app/publish .

# Expose the port the app runs on
EXPOSE 80
EXPOSE 443

# Start the application
ENTRYPOINT ["dotnet", "BE_ModernEstate.WebAPI.dll"] 