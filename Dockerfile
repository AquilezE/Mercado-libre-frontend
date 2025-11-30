# --------------------------
# STAGE 1: Build the project
# --------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore first (better caching)
COPY Mercado-libre-frontend.csproj ./
RUN dotnet restore

# Copy the rest of the project
COPY . ./

# Publish the app
RUN dotnet publish -c Release -o /app/publish

# --------------------------
# STAGE 2: Runtime image
# --------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y locales \
    && sed -i 's/^# *\(es_MX.UTF-8\)/\1/' /etc/locale.gen \
    && locale-gen es_MX.UTF-8

# ---- Set environment for .NET + ICU globalization ----
ENV LANG=es_MX.UTF-8
ENV LC_ALL=es_MX.UTF-8
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false



# Expose port (default for ASP.NET)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

# Copy published output
COPY --from=build /app/publish .

# Run app
ENTRYPOINT ["dotnet", "Mercado-libre-frontend.dll"]
