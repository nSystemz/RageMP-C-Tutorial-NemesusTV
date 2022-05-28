-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 28. Mai 2022 um 22:45
-- Server-Version: 10.4.21-MariaDB
-- PHP-Version: 8.0.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `ragemptutorial`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `accounts`
--

CREATE TABLE `accounts` (
  `id` int(11) NOT NULL,
  `name` varchar(25) NOT NULL,
  `password` varchar(100) NOT NULL,
  `adminlevel` int(2) NOT NULL DEFAULT 0,
  `geld` int(11) NOT NULL,
  `payday` int(2) NOT NULL DEFAULT 60,
  `fraktion` int(2) NOT NULL DEFAULT 0,
  `rang` int(2) NOT NULL DEFAULT 0,
  `posx` float NOT NULL,
  `posy` float NOT NULL,
  `posz` float NOT NULL,
  `posa` float NOT NULL,
  `einreise` int(1) NOT NULL DEFAULT 0,
  `characterdata` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `accounts`
--

INSERT INTO `accounts` (`id`, `name`, `password`, `adminlevel`, `geld`, `payday`, `fraktion`, `rang`, `posx`, `posy`, `posz`, `posa`, `einreise`, `characterdata`) VALUES
(2, 'Nemesus', '$2a$10$Y0XnAEu0moCEbJl6BAbXM.IGw7G1XgJFyebOVmWvBgc5wFYutlQ0.', 3, 5050, 40, 1, 6, 401.444, -1003.79, -99.0041, 127.34, 1, '{\"gender\":\"Männlich\",\"firstname\":\"Test\",\"lastname\":\"Test\",\"birth\":\"01.01.2000\",\"size\":\"1m - 70cm\",\"origin\":\"Los-Santos\",\"hair\":[\"36\",15,0],\"beard\":[\"16\",15],\"blendData\":[0,0,0,0,0,0],\"faceFeatures\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"clothing\":[0,0,0,\"45\"],\"headOverlays\":[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],\"headOverlaysColors\":[0,0,0,0,0,0,0,0,0,0,0,0],\"eyeColor\":[0]}');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `factions`
--

CREATE TABLE `factions` (
  `id` int(11) NOT NULL,
  `factionname` varchar(128) NOT NULL,
  `rang1` varchar(64) NOT NULL,
  `rang2` varchar(64) NOT NULL,
  `rang3` varchar(64) NOT NULL,
  `rang4` varchar(64) NOT NULL,
  `rang5` varchar(64) NOT NULL,
  `rang6` varchar(64) NOT NULL,
  `rang7` varchar(64) NOT NULL,
  `rang8` varchar(64) NOT NULL,
  `rang9` varchar(64) NOT NULL,
  `rang10` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `factions`
--

INSERT INTO `factions` (`id`, `factionname`, `rang1`, `rang2`, `rang3`, `rang4`, `rang5`, `rang6`, `rang7`, `rang8`, `rang9`, `rang10`) VALUES
(0, 'Keine Fraktion', 'Kein Rang', 'Kein Rang', 'Kein Rang', 'Kein Rang', 'Kein Rang', 'Kein Rang', 'Kein Rang', 'Kein Rang', 'Kein Rang', 'Kein Rang'),
(1, 'LSPD', 'Praktikant', 'Praktikant2', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'Boss'),
(2, 'Newsfirma', 'Test', 'Test', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'Chef');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `house`
--

CREATE TABLE `house` (
  `id` int(11) NOT NULL,
  `ipl` varchar(100) NOT NULL,
  `posX` float NOT NULL,
  `posY` float NOT NULL,
  `posZ` float NOT NULL,
  `preis` int(6) NOT NULL,
  `besitzer` varchar(50) NOT NULL,
  `abgeschlossen` int(1) NOT NULL DEFAULT 0,
  `status` int(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `house`
--

INSERT INTO `house` (`id`, `ipl`, `posX`, `posY`, `posZ`, `preis`, `besitzer`, `abgeschlossen`, `status`) VALUES
(1, 'apa_v_mp_h_01_a', -437.341, 1124.94, 325.859, 50, 'Keiner', 0, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `items`
--

CREATE TABLE `items` (
  `id` int(11) NOT NULL,
  `hash` varchar(35) NOT NULL,
  `ownerEntity` varchar(35) NOT NULL,
  `ownerIdentifier` int(6) NOT NULL,
  `amount` int(4) NOT NULL,
  `posX` float NOT NULL,
  `posY` float NOT NULL,
  `PosZ` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `items`
--

INSERT INTO `items` (`id`, `hash`, `ownerEntity`, `ownerIdentifier`, `amount`, `posX`, `posY`, `PosZ`) VALUES
(2, '2565741261', 'Player', 2, 3, 0, 0, 0),
(9, '2565741261', 'Ground', 2, 1, -445.791, 1135.73, 324.985),
(10, '2565741261', 'Ground', 2, 1, -435.519, 1136.22, 324.984);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `whitelist`
--

CREATE TABLE `whitelist` (
  `id` int(11) NOT NULL,
  `socialclubid` int(10) NOT NULL,
  `timestamp` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `whitelist`
--

INSERT INTO `whitelist` (`id`, `socialclubid`, `timestamp`) VALUES
(3, 18021891, '2022-03-08 12:47:33');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `factions`
--
ALTER TABLE `factions`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `house`
--
ALTER TABLE `house`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `whitelist`
--
ALTER TABLE `whitelist`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `accounts`
--
ALTER TABLE `accounts`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `factions`
--
ALTER TABLE `factions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `house`
--
ALTER TABLE `house`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `items`
--
ALTER TABLE `items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT für Tabelle `whitelist`
--
ALTER TABLE `whitelist`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
