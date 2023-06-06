USE db;
----------------------------------------------------------

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

--
-- Banco de dados: `db`
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
-- Índices de tabela `Contatos`
--
ALTER TABLE `Contatos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Contatos_UsuarioId` (`UsuarioId`);

--
-- AUTO_INCREMENT de tabela `Contatos`
--
ALTER TABLE `Contatos`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- Restrições para tabelas `Contatos`
--
ALTER TABLE `Contatos`
  ADD CONSTRAINT `FK_Contatos_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`Id`) ON DELETE CASCADE;
COMMIT;