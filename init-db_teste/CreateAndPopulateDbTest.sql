# create databases
CREATE DATABASE IF NOT EXISTS `db_teste`;

# Grant rights to user
GRANT ALL PRIVILEGES ON db_teste.* TO 'user'@'%';

USE db_teste;

INSERT INTO `Usuarios` (`Id`, `Nome`, `Login`, `Email`, `Perfil`, `Senha`, `DataCadastro`, `DataAtualizacao`) VALUES (1, 'Alvaro', 'admin', 'alvaro@admin', '1', 'd033e22ae348aeb5660fc2140aec35850c4da997', '2023-05-20 19:25:29.387079', '2023-05-20 19:25:29.387079');

INSERT IGNORE INTO `Contatos` (`Id`, `Nome`, `Email`, `Celular`, `UsuarioId`) VALUES (1, 'Carlos Tester1', 'carlos1@teste.com', '11 94328764', '1');
INSERT IGNORE INTO `Contatos` (`Id`, `Nome`, `Email`, `Celular`, `UsuarioId`) VALUES (2, 'Carlos Tester2', 'carlos2@teste.com', '11 94328764', '1');
INSERT IGNORE INTO `Contatos` (`Id`, `Nome`, `Email`, `Celular`, `UsuarioId`) VALUES (3, 'Carlos Tester3', 'carlos3@teste.com', '11 94328764', '1');