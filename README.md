## Executando em Desenvolvimento
Antes de executar, é preciso instalar a ferramenta __dotnet-ef__ para criar as tabelas no banco de dados

```bash
#Instala o dotnet-ef
$ dotnet tool install --global dotnet-ef --version 5.0.17

#Entra na pasta do projeto
$ cd ControleDeContatos

#Para fazer o comando dotnet-ef disponivel
$ dotnet tool restore

#Aplica a migration no database
$ dotnet tool run dotnet-ef database update
```
## Executando o Projeto
```bash
#Na mesma pasta do .csproj
$ dotnet watch run
```
___
## Comandos para o  Desenvolvimento
Aqui ficam listados os comando úteis apenas durante o desenvolvimento

```bash
# Cria as migrations
$ dotnet tool run dotnet-ef migrations add MyMigration

#Aplica a migration no database
$ dotnet tool run dotnet-ef database update
```

