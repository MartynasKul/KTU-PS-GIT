-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 18, 2023 at 02:32 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `eshop`
--

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `Kategorija_ID` int(11) NOT NULL,
  `Kategorijos_pavadinimas` varchar(255) NOT NULL,
  `Prekiu_Skaicius` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`Kategorija_ID`, `Kategorijos_pavadinimas`, `Prekiu_Skaicius`) VALUES
(1, 'gaidiski itemai', 2);

-- --------------------------------------------------------

--
-- Table structure for table `comments`
--

CREATE TABLE `comments` (
  `CommentID` int(11) NOT NULL,
  `Date_` timestamp NOT NULL DEFAULT current_timestamp(),
  `Text_` text DEFAULT NULL,
  `Likes` int(11) DEFAULT NULL,
  `fk_PostID` int(11) DEFAULT NULL,
  `fk_UserID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `comments`
--

INSERT INTO `comments` (`CommentID`, `Date_`, `Text_`, `Likes`, `fk_PostID`, `fk_UserID`) VALUES
(1, '2023-12-09 19:11:14', 'Rat bastard it was bad item', 5, 1, 1),
(3, '2023-12-10 11:01:28', 'rat', 0, 1, 1),
(4, '2023-12-10 11:58:03', 'I like big tits', 0, 1, 1),
(5, '2023-12-10 12:02:26', 'Rat123', 0, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `item_ID` int(11) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Picture` varchar(255) NOT NULL,
  `Kaina` double NOT NULL,
  `Svoris` varchar(255) NOT NULL,
  `Pagaminimo_data` varchar(255) NOT NULL,
  `Serija` varchar(255) DEFAULT '',
  `Spalva` varchar(255) DEFAULT '',
  `Plotis` varchar(255) DEFAULT '',
  `Aukstis` varchar(255) DEFAULT '',
  `Ilgis` varchar(255) DEFAULT '',
  `Garantija` varchar(255) DEFAULT '',
  `Ekrano_dydis` varchar(255) DEFAULT '',
  `Operacine_sistema` varchar(255) DEFAULT '',
  `Procesorius` varchar(255) DEFAULT '',
  `Kietasis_diskas` varchar(255) DEFAULT '',
  `Vaizdo_plokste` varchar(255) DEFAULT '',
  `Jungtys` varchar(255) DEFAULT '',
  `Baterija` varchar(255) DEFAULT '',
  `Papildoma_info` varchar(255) DEFAULT '',
  `Procesoriaus_karta` varchar(255) DEFAULT '',
  `fk_categoryID` int(11) NOT NULL,
  `fk_manufacturerID` int(11) NOT NULL,
  `AddedTimestamp` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`item_ID`, `Pavadinimas`, `Picture`, `Kaina`, `Svoris`, `Pagaminimo_data`, `Serija`, `Spalva`, `Plotis`, `Aukstis`, `Ilgis`, `Garantija`, `Ekrano_dydis`, `Operacine_sistema`, `Procesorius`, `Kietasis_diskas`, `Vaizdo_plokste`, `Jungtys`, `Baterija`, `Papildoma_info`, `Procesoriaus_karta`, `fk_categoryID`, `fk_manufacturerID`, `AddedTimestamp`) VALUES
(1, 'Geforece GTX 1060', 'https://m.media-amazon.com/images/I/61im3tztWyL.jpg', 80000, '3', '2008', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, '2023-12-08 14:31:36'),
(16, 'Ktu computer verygood', 'https://5.imimg.com/data5/SELLER/Default/2022/12/FN/ZL/OJ/3866941/desktop-computer.jpg', 50, '50', '1901', 'Geforce RTX', 'juoda', '50', '50', '3', 'nera', '6 cm', 'IOS', 'I11', '999999999gb', 'Nvdia', 'visos', 'nera', 'Labai geras kompiuteris :)', '69', 1, 1, '2023-12-08 14:31:36'),
(19, 'asus', 'https://avitela.lt/image/cache/catalog/p/98/316/322/82588/trecias-1080x1000.jpeg', 420, '5', '52225', '4', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, '2023-12-08 14:31:36'),
(23, 'nauja preke', 'asdfasdfasdf', 464654654, '65456464', '54654654', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, '2023-12-08 15:12:13');

-- --------------------------------------------------------

--
-- Table structure for table `item_order`
--

CREATE TABLE `item_order` (
  `fk_itemID` int(11) NOT NULL,
  `fk_orderID` int(11) NOT NULL,
  `amount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `item_order`
--

INSERT INTO `item_order` (`fk_itemID`, `fk_orderID`, `amount`) VALUES
(1, 2, 1),
(2, 2, 2),
(2, 3, 2),
(1, 4, 1),
(1, 5, 1),
(16, 6, 1),
(16, 7, 3),
(1, 7, 1);

-- --------------------------------------------------------

--
-- Table structure for table `likes`
--

CREATE TABLE `likes` (
  `ID` int(11) NOT NULL,
  `fk_PostID` int(11) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
  `Date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `likes`
--

INSERT INTO `likes` (`ID`, `fk_PostID`, `UserID`, `Date`) VALUES
(1, 1, 4, '2023-12-18 14:07:38'),
(2, 2, 4, '2023-12-18 14:07:44');

-- --------------------------------------------------------

--
-- Table structure for table `manufacturers`
--

CREATE TABLE `manufacturers` (
  `Gamintojas_ID` int(11) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Salis` varchar(255) NOT NULL,
  `fk_itemID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `manufacturers`
--

INSERT INTO `manufacturers` (`Gamintojas_ID`, `Pavadinimas`, `Salis`, `fk_itemID`) VALUES
(1, 'birka', 'lietuva', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `id` int(11) NOT NULL,
  `fk_userID` int(11) NOT NULL,
  `address` varchar(255) NOT NULL,
  `price` decimal(18,5) NOT NULL,
  `status` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`id`, `fk_userID`, `address`, `price`, `status`) VALUES
(2, 1, 'test', 475.00000, 'Processing'),
(3, 1, 'test', 240.00000, 'Success'),
(4, 1, 'test2', 235.00000, 'Success'),
(5, 1, 'Lenkija krc', 235.00000, 'Processing'),
(6, 1, 'nahui', 90000.00000, 'Processing'),
(7, 1, 'nahi', 80150.00000, 'Processing');

-- --------------------------------------------------------

--
-- Table structure for table `post`
--

CREATE TABLE `post` (
  `PostID` int(11) NOT NULL,
  `Date_` timestamp NOT NULL DEFAULT current_timestamp(),
  `Text_` text DEFAULT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `Likes` int(11) DEFAULT NULL,
  `fk_ItemID` int(11) DEFAULT NULL,
  `fk_UserID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `post`
--

INSERT INTO `post` (`PostID`, `Date_`, `Text_`, `Title`, `Likes`, `fk_ItemID`, `fk_UserID`) VALUES
(1, '2023-12-09 19:10:34', 'Good', 'Bad', 5, 1, 1),
(2, '2023-12-10 11:29:16', 'rat', 'rat', 0, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserID` int(11) NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Surname` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Points` int(11) DEFAULT 1,
  `Username` varchar(255) DEFAULT NULL,
  `UserType` int(1) NOT NULL DEFAULT 1 COMMENT '1 - paprastas useris\r\n2 - adminas',
  `Salt` varchar(64) NOT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `PhoneNumber` varchar(14) NOT NULL DEFAULT '+37060000000'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `Name`, `Surname`, `Password`, `Email`, `Points`, `Username`, `UserType`, `Salt`, `Address`, `PhoneNumber`) VALUES
(1, 'Nedas', 'Nedas', 'Nedas', 'Nedas', 5, 'Nedas', 2, '', NULL, ''),
(32, 'Martynas', 'Kuliesius', 'Test123!', 'martiss200@gmail.com', 111, 'markul4', 1, 'qydbPg/MJPehDau9xiPVrA==', 'Juragiai, Virbaliskiu 14', '+37067944804'),
(33, 'Admin', 'Admin', 'Admin', 'Admin@Admin.lt', 99999, 'Admin', 2, 'oCeAu3pwI43hGQl0uKQ7Mw==', NULL, '+37060000000');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`Kategorija_ID`);

--
-- Indexes for table `comments`
--
ALTER TABLE `comments`
  ADD PRIMARY KEY (`CommentID`),
  ADD KEY `fk_PostID` (`fk_PostID`),
  ADD KEY `fk_UserID` (`fk_UserID`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`item_ID`),
  ADD KEY `fk_category` (`fk_categoryID`),
  ADD KEY `fk_manufacturer` (`fk_manufacturerID`);

--
-- Indexes for table `likes`
--
ALTER TABLE `likes`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fk_PostID` (`fk_PostID`);

--
-- Indexes for table `manufacturers`
--
ALTER TABLE `manufacturers`
  ADD PRIMARY KEY (`Gamintojas_ID`),
  ADD KEY `fk_item` (`fk_itemID`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `post`
--
ALTER TABLE `post`
  ADD PRIMARY KEY (`PostID`),
  ADD KEY `fk_ItemID` (`fk_ItemID`),
  ADD KEY `fk_UserID` (`fk_UserID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `Kategorija_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `comments`
--
ALTER TABLE `comments`
  MODIFY `CommentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `item_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT for table `likes`
--
ALTER TABLE `likes`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `manufacturers`
--
ALTER TABLE `manufacturers`
  MODIFY `Gamintojas_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `post`
--
ALTER TABLE `post`
  MODIFY `PostID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `comments`
--
ALTER TABLE `comments`
  ADD CONSTRAINT `comments_ibfk_1` FOREIGN KEY (`fk_PostID`) REFERENCES `post` (`PostID`),
  ADD CONSTRAINT `comments_ibfk_2` FOREIGN KEY (`fk_UserID`) REFERENCES `users` (`UserID`);

--
-- Constraints for table `items`
--
ALTER TABLE `items`
  ADD CONSTRAINT `fk_category` FOREIGN KEY (`fk_categoryID`) REFERENCES `categories` (`Kategorija_ID`),
  ADD CONSTRAINT `fk_manufacturer` FOREIGN KEY (`fk_manufacturerID`) REFERENCES `manufacturers` (`Gamintojas_ID`);

--
-- Constraints for table `likes`
--
ALTER TABLE `likes`
  ADD CONSTRAINT `likes_ibfk_1` FOREIGN KEY (`fk_PostID`) REFERENCES `post` (`PostID`);

--
-- Constraints for table `manufacturers`
--
ALTER TABLE `manufacturers`
  ADD CONSTRAINT `fk_item` FOREIGN KEY (`fk_itemID`) REFERENCES `items` (`item_ID`);

--
-- Constraints for table `post`
--
ALTER TABLE `post`
  ADD CONSTRAINT `post_ibfk_1` FOREIGN KEY (`fk_ItemID`) REFERENCES `items` (`item_ID`),
  ADD CONSTRAINT `post_ibfk_2` FOREIGN KEY (`fk_UserID`) REFERENCES `users` (`UserID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
