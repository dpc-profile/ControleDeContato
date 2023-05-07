## Executando em desenvolvimento
Dentro do Dev Containers
```bash
$ dotnet watch --project ControleDeContatos run
```

## Sobre o Desenvolvimento
```bash
#Cria a migration
$ dotnet tool run dotnet-ef migrations add MyMigration

#Aplica a migration no database
$ dotnet tool run dotnet-ef database update
```