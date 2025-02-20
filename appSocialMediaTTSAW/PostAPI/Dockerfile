#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PostAPI/PostAPI.csproj", "PostAPI/"]
RUN dotnet restore "PostAPI/PostAPI.csproj"
COPY . .
#WORKDIR "/src/PostAPI."
WORKDIR "/src/PostAPI"
RUN dotnet build "PostAPI.csproj" -c Release -o /app/build





FROM build AS publish
#ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "PostAPI.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostAPI.dll","--environment=Development"]


