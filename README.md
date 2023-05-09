## Controle de Contatos
A versão do .NET usada é __3.1__
___
## Executando em Desenvolvimento
Usando a extensão __Dev Containers__ do vscode, um container já configurado para desenvolvimento será criado.

```bash
#Entra na pasta do projeto
$ cd ControleDeContatos

#Aplica a migration no database
$ dotnet-ef database update
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
$ dotnet-ef migrations add MyMigration

#Aplica a migration no database
$ dotnet-ef database update
```

