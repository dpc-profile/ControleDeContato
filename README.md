# Controle de Contatos
### Sobre o projeto
Uma plataforma web, que tem com objetivo de cadastrar e organizar contatos dos usuários.
É pretendido que o projeto tenha as features:
- Cadastrar novos usuários. :heavy_check_mark:
- Sessão de login. :heavy_check_mark:
- Cada usuário tem sua propria lista de contatos. :heavy_check_mark:
- Poder visualizar, criar, editar, apagar seus contatos. :heavy_check_mark:

Dev features:
- Testes de unidade. (apenas na parte de contatos)
- Analise de cobertura com SonarCloud. :heavy_check_mark:
- Criptografia de senhas. :heavy_check_mark:
- Uso do docker no desenvolvimento. :heavy_check_mark:
- Uso do docker para criar buid de produção.

### Intuito do projeto
O objetivo é para consolidar meus conhecimento de programação, com o uso de MVC, Banco de dados MySQL, Docker, Testes de Unidade e Cobertura de Código, além de adquirir experiência no framework .NET, e como desafio pessoal, o uso da versão do __.NET Core SDK 3.1__.
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

