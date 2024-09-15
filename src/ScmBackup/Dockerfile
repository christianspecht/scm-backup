#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#More info on customizing debug container.
#https://learn.microsoft.com/en-us/visualstudio/containers/container-build?view=vs-2022#debugging

ARG TZ
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER root

# Set the time zone to Stockholm
ENV TZ=Europe/Stockholm
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# Install Git
RUN apt-get update && apt-get install -y git

USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ScmBackup/ScmBackup.csproj", "src/ScmBackup/"]
RUN dotnet restore "./src/ScmBackup/./ScmBackup.csproj"
COPY . .
WORKDIR "/src/src/ScmBackup"
RUN dotnet build "./ScmBackup.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ScmBackup.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ScmBackup.dll"]