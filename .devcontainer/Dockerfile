#Dockerfile para ser usado em Desenvolvimento
FROM mcr.microsoft.com/dotnet/sdk:3.1
 
WORKDIR /source

# Adiciona o tools para poder usar o comando dotnet-ef
ENV PATH=$PATH:/root/.dotnet/tools

#Instala reportgenerator, dotnet-ef e faz o contaienr ficar vivo
CMD dotnet tool install --global dotnet-sonarscanner \ 
&& dotnet tool install -g dotnet-reportgenerator-globaltool \ 
&& dotnet tool install --global dotnet-ef --version 5.0.17 \
&& tail -f /dev/null