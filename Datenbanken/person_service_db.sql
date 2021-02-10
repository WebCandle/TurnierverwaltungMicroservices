-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 10. Feb 2021 um 16:54
-- Server-Version: 10.4.17-MariaDB
-- PHP-Version: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `person_service_db`
--
CREATE DATABASE IF NOT EXISTS `person_service_db` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `person_service_db`;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `personen`
--

CREATE TABLE `personen` (
  `PersonId` int(11) NOT NULL,
  `Name` text DEFAULT NULL,
  `Nachname` text DEFAULT NULL,
  `Geburtsdatum` datetime NOT NULL,
  `MannschaftId` int(11) NOT NULL,
  `Discriminator` text NOT NULL,
  `Aufgabe` text DEFAULT NULL,
  `Gehalt` decimal(18,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `personen`
--

INSERT INTO `personen` (`PersonId`, `Name`, `Nachname`, `Geburtsdatum`, `MannschaftId`, `Discriminator`, `Aufgabe`, `Gehalt`) VALUES
(2, 'testName', 'testNachname', '2021-01-01 13:53:53', 1, 'Spieler', 'Torwart', NULL),
(6, 'maher', 'al abbasi', '1989-07-06 01:48:00', 1, 'Trainer', NULL, '6000.00');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20210122110023_InitialCreate', '3.1.10');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `personen`
--
ALTER TABLE `personen`
  ADD PRIMARY KEY (`PersonId`);

--
-- Indizes für die Tabelle `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `personen`
--
ALTER TABLE `personen`
  MODIFY `PersonId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
