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
-- Estrutura para tabela `Usuarios`
--

CREATE TABLE `Usuarios` (
  `Id` int NOT NULL,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Login` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Perfil` int NOT NULL,
  `Senha` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DataCadastro` datetime(6) NOT NULL,
  `DataAtualizacao` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Despejando dados para a tabela `Usuarios`
--

INSERT INTO `Usuarios` (`Id`, `Nome`, `Login`, `Email`, `Perfil`, `Senha`, `DataCadastro`, `DataAtualizacao`) VALUES
(1, 'Padronos Tester', 'padronos', 'padronos@gmail.com', 2, '2e6f9b0d5885b6010f9167787445617f553a735f', '2023-06-05 22:57:30.167565', NULL),
(2, 'Amanda Tester', 'amanda', 'amanda@gmail.com', 2, '2e6f9b0d5885b6010f9167787445617f553a735f', '2023-06-06 13:43:38.514044', NULL);
(3, 'Alvaro', 'admin', 'alvaro@admin', 1, 'd033e22ae348aeb5660fc2140aec35850c4da997', '2023-05-20 19:25:29.387079', '2023-05-20 19:25:29.387079');

--
-- Índices de tabela `Usuarios`
--
ALTER TABLE `Usuarios`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de tabela `Usuarios`
--
ALTER TABLE `Usuarios`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;