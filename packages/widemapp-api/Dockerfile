#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Constack.Widemapp.Api/Constack.Widemapp.Api.csproj", "src/Constack.Widemapp.Api/"]
COPY ["src/Constack.Widemapp.Persistence/Constack.Widemapp.Persistence.csproj", "src/Constack.Widemapp.Persistence/"]
COPY ["src/Constack.Widemapp.Application/Constack.Widemapp.Application.csproj", "src/Constack.Widemapp.Application/"]
COPY ["src/Constack.Widemapp.Common/Constack.Widemapp.Common.csproj", "src/Constack.Widemapp.Common/"]
COPY ["src/Constack.Widemapp.Domain/Constack.Widemapp.Domain.csproj", "src/Constack.Widemapp.Domain/"]
RUN dotnet restore "src/Constack.Widemapp.Api/Constack.Widemapp.Api.csproj"
COPY . .
WORKDIR "/src/src/Constack.Widemapp.Api"
RUN dotnet build "Constack.Widemapp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Constack.Widemapp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Constack.Widemapp.Api.dll"]