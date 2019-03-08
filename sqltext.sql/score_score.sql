CREATE DATABASE  IF NOT EXISTS `score` ;
USE `score`;

--
-- Table structure for table `score`
--

DROP TABLE IF EXISTS `score`;
CREATE TABLE `score` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TypeID` int(11) DEFAULT NULL,
  `TypeName` varchar(100) DEFAULT NULL,
  `Number` varchar(45) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Sex` varchar(45) DEFAULT NULL,
  `Birth` varchar(45) DEFAULT NULL,
  `Grade` decimal(10,2) DEFAULT NULL,
  `Unit` varchar(100) DEFAULT NULL,
  `Job` varchar(100) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `ValidTime` datetime DEFAULT NULL,
  `CreatTime` datetime DEFAULT NULL,
  `Creator` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;



DROP TABLE IF EXISTS `scoretype`;
CREATE TABLE `scoretype` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TypeName` varchar(100) DEFAULT NULL,
  `Isdele` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
