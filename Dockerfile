FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "ControleDeContatos/ControleDeContatos.csproj" --disable-parallel
RUN dotnet publish "ControleDeContatos/ControleDeContatos.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "ControleDeContatos.dll"]