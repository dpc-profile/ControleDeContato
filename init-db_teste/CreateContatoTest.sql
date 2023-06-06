-- create databases
CREATE DATABASE IF NOT EXISTS `db_teste`;

-- Grant rights to user
GRANT ALL PRIVILEGES ON db_teste.* TO 'user'@'%';

USE db_teste;

-- --------------------------------------------------------

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

-- --------------------------------------------------------
--
-- Banco de dados: `db_teste`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `Contatos`
--

CREATE TABLE `Contatos` (
  `Id` int NOT NULL,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Celular` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UsuarioId` int NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `Contatos`
--
ALTER TABLE `Contatos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Contatos_UsuarioId` (`UsuarioId`);

--
-- AUTO_INCREMENT para tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `Contatos`
--
ALTER TABLE `Contatos`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Restrições para tabelas despejadas
--

--
-- Restrições para tabelas `Contatos`
--
ALTER TABLE `Contatos`
  ADD CONSTRAINT `FK_Contatos_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`Id`) ON DELETE CASCADE;
COMMIT;