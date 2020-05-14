#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 99

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
RUN curl --silent --location https://deb.nodesource.com/setup_13.x | bash -
RUN apt-get update -y && apt-get install --yes nodejs
COPY [".", "CarSale/"]
RUN dotnet restore "CarSale/CarSale.csproj"
COPY . .
WORKDIR "/src/CarSale"
RUN pwd && ls -l
RUN dotnet build "CarSale.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSale.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSale.dll"]