FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update
RUN apt-get install -y libc6 libicu-dev libfontconfig1
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN  dotnet nuget add source https://nuget.devexpress.com/7yjCAVbkuOgYOD9e4lLffoUGlHojK1IseySZswxXjZqlXZCA8v/api/v3/index.json -n devexpress-nuget
COPY ["Sunday.Blazor.Server/Sunday.Blazor.Server.csproj", "Sunday.Blazor.Server/"]
COPY ["Sunday.Module/Sunday.Module.csproj", "Sunday.Module/"]
RUN dotnet restore "Sunday.Blazor.Server/Sunday.Blazor.Server.csproj"
COPY . .
WORKDIR "/src/Sunday.Blazor.Server"
RUN dotnet build "Sunday.Blazor.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sunday.Blazor.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sunday.Blazor.Server.dll"]




























#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
##COPY ./NuGet.Config/nuget.config
##EXPOSE 80
##EXPOSE 443
#
#
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["Sunday.Blazor.Server/Sunday.Blazor.Server.csproj", "Sunday.Blazor.Server/"]
#COPY ["BlazorComponents/BlazorComponents.csproj", "BlazorComponents/"]
#COPY ["Sunday.Module/Sunday.Module.csproj", "Sunday.Module/"]
#COPY ["Sunday.Common/Sunday.Common.csproj", "Sunday.Common/"]
#COPY ["SundayNominatim/SundayNominatim.csproj", "SundayNominatim/"]
#RUN dotnet restore "./Sunday.Blazor.Server/./Sunday.Blazor.Server.csproj"
#COPY . .
#WORKDIR "/src/Sunday.Blazor.Server" 
#RUN dotnet build "./Sunday.Blazor.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build 
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./Sunday.Blazor.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#
#
#ENTRYPOINT ["dotnet", "Sunday.Blazor.Server.dll"]