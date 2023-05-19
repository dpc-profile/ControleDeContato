#Dockerfile para ser usado em Desenvolvimento
FROM mcr.microsoft.com/dotnet/sdk:3.1
 
WORKDIR /source

# Adiciona o tools para poder usar o comando dotnet-ef
ENV PATH=$PATH:/root/.dotnet/tools

# Nesse ponto, como os arquivos ainda não estão no container,
# é preciso copiar para dentro
COPY .devcontainer/dotnet_tools-install.sh /tmp/dotnet_tools-install.sh 

# Instala as ferramentas do dotnet
RUN /tmp/dotnet_tools-install.sh

# Faz o contaienr ficar vivo
CMD ["tail", "-f", "/dev/null"]