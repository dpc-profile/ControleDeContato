# Controle de Contatos
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=dpc-profile_ControleDeContato&metric=coverage)](https://sonarcloud.io/summary/new_code?id=dpc-profile_ControleDeContato)[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=dpc-profile_ControleDeContato&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=dpc-profile_ControleDeContato)[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=dpc-profile_ControleDeContato&metric=bugs)](https://sonarcloud.io/summary/new_code?id=dpc-profile_ControleDeContato)

### Sobre o projeto
Uma plataforma web, que tem com objetivo de cadastrar e organizar contatos dos usuários.
É pretendido que o projeto tenha as features:
- Cadastrastro de usuários. :heavy_check_mark:
- Sessão de login. :heavy_check_mark:
- Cada usuário tem sua propria lista de contatos. :heavy_check_mark:
- Poder visualizar, criar, editar, apagar seus contatos. :heavy_check_mark:

Dev features:
- Testes de unidade. :heavy_check_mark:
- Analise de cobertura com SonarCloud. :heavy_check_mark:
- Uso do docker no desenvolvimento. :heavy_check_mark:
- Uso do docker para criação de  buid. :heavy_check_mark:

### Intuito do projeto
O objetivo é para consolidar meus conhecimento de programação, com o uso de MVC, Banco de dados MySQL, Docker, Testes de Unidade e Cobertura de Código, além de adquirir experiência no framework .NET, e como desafio pessoal, o uso da versão do __.NET Core SDK 3.1__.
___
## Executando Projeto
O projeto usa o docker compose para criar os container do database e do app.

### Configurando o banco de dados:
- Mude o nome do arquivo __mysql_template.env__ para __mysql.env__ na pasta __Infra__ e preencer os espaços vazios.


### Rodando tudo

```bash
#Na raiz do projeto
$ docker compose up

#Acessar a aplicação
http://172.25.0.10

#Acessar phpMyAdmin
http://172.25.0.11
```

Para entrar no phpMyAdmin, use as credenciais usadas em __Infra/mysql.env__. No campo __Servidor:__ pode usar o ip do container mysql, ou o próprio nome do container que é __mysql_db__.
___
## Executando em Desenvolvimento
É usado a extensão __Dev Containers__ do vscode, com ela, é criado os container do database, phpMyAdmin e de um ambiente com .NET 3.1, além de instalar algumas extensões apenas no vscode dentro do container.


### Configurando ambiente
Para ter o OmniSharp funcionando é preciso ter o mono instalado, o arquivo __mono-java-install.sh__ faz a instalação do mono e java, que no caso é necessário para usar o Sonar Scanner

```bash
$ bash .devcontainer/mono-java-install.sh
```

Automaticamente é instalado também os dotnet tools necessários, que você pode conferir.
```bash
.devcontainer/dotnet_tools-install.sh
```

### Sobre o Database
Quando ele é iniciado, é executado automaticamente os arquivos em __init-database__ dentro do container, que cria as tabelas do db principal e cria o database de teste e suas tabelas.

O database de teste, é somente para os testes unitários, onde eles já veem pré-populados com informações necessarias.

### Criando as tabelas
Se for necessário criar as tabelas.
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
### Rodando os testes
O projeto possui os testes unitários e teste do cypress

Para rodar os testes unitários

```bash
$ dotnet test ControleDeContatos.Tests \
--settings ControleDeContatos.Tests/coverlet.runsettings.xml
```

Se quiser gerar um relatório de coverage, é usado o reportgenerator

```bash
$ reportgenerator \
-reports:"ControleDeContatos.Tests/TestResults/**/coverage.opencover.xml" \
-targetdir:"coveragereport" \
-reporttypes:Htm
```

Para rodar o cypress é preciso ter o nodejs 18.16.0
Como não foi configurado dentro do container, é preciso abrir o projeto, ou somente a pasta do cypress fora do container, e usar a versão do nodejs instalada localmente.

```bash
#Na pasta testes-cypress
$ npx cypress open
```
