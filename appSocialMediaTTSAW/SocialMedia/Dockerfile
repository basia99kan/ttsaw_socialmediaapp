FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["SocialMedia/SocialMedia.csproj", "SocialMedia/"]
RUN dotnet restore "SocialMedia/SocialMedia.csproj"
COPY . .
WORKDIR "/src/SocialMedia"
RUN dotnet build "SocialMedia.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialMedia.csproj" -c Release -o /app/publish 


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialMedia.dll"]



