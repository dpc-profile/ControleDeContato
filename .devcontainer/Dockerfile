#Dockerfile para ser usado em Desenvolvimento
FROM mcr.microsoft.com/dotnet/sdk:3.1
 
WORKDIR /source

ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID

# Create the user
RUN groupadd --gid $USER_GID $USERNAME \
    && useradd --uid $USER_UID --gid $USER_GID -m $USERNAME \
    #
    # [Optional] Add sudo support. Omit if you don't need to install software after connecting.
    && apt-get update \
    && apt-get install -y sudo \
    && echo $USERNAME ALL=\(root\) NOPASSWD:ALL > /etc/sudoers.d/$USERNAME \
    && chmod 0440 /etc/sudoers.d/$USERNAME

# ********************************************************
# * Anything else you want to do like clean up goes here *
# ********************************************************

# [Optional] Set the default user. Omit if you want to keep the default as root.
USER $USERNAME

# Adiciona o tools para poder usar o comando dotnet-ef
ENV PATH=$PATH:/home/vscode/.dotnet/tools

# Nesse ponto, como os arquivos ainda não estão no container,
# é preciso copiar para dentro
COPY dotnet_tools-install.sh /tmp/dotnet_tools-install.sh 

# Instala as ferramentas do dotnet
RUN /tmp/dotnet_tools-install.sh

# Faz o contaienr ficar vivo
CMD ["tail", "-f", "/dev/null"]