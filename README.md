## Controle de Contatos
A versão do .NET usada é __3.1__
___
## Executando em Desenvolvimento
Usando a extensão __Dev Containers__ do vscode, um container já configurado para desenvolvimento será criado.

### Configurando o banco de dados:
- Mudar o nome do arquivo __mysql_template.env__ para __mysql.env__ e preencer os espaços vazios.

- Copiar essas mesmas configurações e preencher o arquivo  __ControleDeContatos/appsettings.json__, na chave DbConnection (por enquanto configurações nesse arquivo são hard coded)

### Criando as tabelas
Ao ter um banco de dados rodando, vai ser preciso criar as tabelas.

```bash
#Entra na pasta do projeto
$ cd ControleDeContatos

#Aplica a migration no database
$ dotnet-ef database update
```
### Executando o Projeto
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

# Aplica a migration no database
$ dotnet-ef database update

# Gerar cobertura de teste
$ dotnet test ControleDeContatos.Tests \
/p:CollectCoverage=true \
/p:CoverletOutputFormat=opencover \
/p:CoverletOutput="./results/" \
/p:Exclude="[*]*.Migrations.*"

# ou 

$ dotnet test ControleDeContatos.Tests \
--settings ControleDeContatos.Tests/coverlet.runsettings.xml

# Gerar relatório para HTML
$ reportgenerator \
-reports:"ControleDeContatos.Tests/TestResults/coverage.opencover.xml" \
-targetdir:"coveragereport" \
-reporttypes:Html

#Arquivo index.html estará em 'coveragereport'

#Adiciona todos os csproj a solução ControleDeContatos.sln
dotnet sln ControleDeContatos.sln add **/*.csproj --in-root
```