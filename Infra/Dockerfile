FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source
COPY . .
WORKDIR /source/ControleDeContatos
RUN dotnet restore --disable-parallel
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "ControleDeContatos.dll"]