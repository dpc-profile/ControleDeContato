## Comandos para o  Desenvolvimento
Aqui ficam listados os comando úteis apenas durante o desenvolvimento


```bash
# Cria as migrations
$ dotnet-ef migrations add MyMigration

# Aplica a migration no database
$ dotnet-ef database update

# Gerar relatorio que alimentara o reporgenerator
$ dotnet test ControleDeContatos.Tests \
--settings ControleDeContatos.Tests/coverlet.runsettings.xml

# Gerar relatório para HTML
$ reportgenerator \
-reports:"ControleDeContatos.Tests/TestResults/**/coverage.opencover.xml" \
-targetdir:"coveragereport" \
-reporttypes:Html

#Arquivo index.html estará em 'coveragereport'

#Adiciona todos os csproj a solução ControleDeContatos.sln
$ dotnet sln ControleDeContatos.sln add **/*.csproj --in-root
```

## Gerando Relatorios para o SonarCloud

```bash
$ dotnet sonarscanner begin \
/o:USER-SONARCLOUD \
/k:PROJECT-KEY \
/d:sonar.host.url=https://sonarcloud.io \
/d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml

$ dotnet build --no-incremental

# Usando a tool dotnet-coverage para gerar o relatorio
$ dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"

$ dotnet sonarscanner end

# Rodar o teste de mutação
$ dotnet Stryker
```