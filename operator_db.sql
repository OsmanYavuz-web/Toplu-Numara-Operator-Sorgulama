-- phpMyAdmin SQL Dump
-- version 4.3.11
-- http://www.phpmyadmin.net
--
-- Anamakine: 127.0.0.1
-- Üretim Zamanı: 04 Tem 2015, 21:03:48
-- Sunucu sürümü: 5.6.24
-- PHP Sürümü: 5.6.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Veritabanı: `operator_db`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `ayarlar`
--

CREATE TABLE IF NOT EXISTS `ayarlar` (
  `id` int(11) NOT NULL,
  `site_url` varchar(250) NOT NULL,
  `site_baslik` varchar(250) NOT NULL,
  `site_desc` varchar(300) NOT NULL,
  `site_keyw` varchar(300) NOT NULL,
  `site_tema` varchar(150) NOT NULL,
  `site_durum` int(11) NOT NULL,
  `site_blog_durum` int(11) NOT NULL,
  `site_eposta` varchar(300) NOT NULL,
  `tema_sayfalama` varchar(100) NOT NULL,
  `site_yapimci` text NOT NULL,
  `site_yapimci_web` text NOT NULL,
  `site_footer` text NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `ayarlar`
--

INSERT INTO `ayarlar` (`id`, `site_url`, `site_baslik`, `site_desc`, `site_keyw`, `site_tema`, `site_durum`, `site_blog_durum`, `site_eposta`, `tema_sayfalama`, `site_yapimci`, `site_yapimci_web`, `site_footer`) VALUES
(1, 'http://localhost', 'Operatör Sorgulama Programı', 'Site Açıklama', 'Site Kelime Anahtarı', 'vine', 1, 1, '', '10|10|10|10', 'Osman Yavuz', '', 'Vine olarak tüm haklarımız saklıdır © 2015');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `uyeler`
--

CREATE TABLE IF NOT EXISTS `uyeler` (
  `uye_id` int(11) NOT NULL,
  `uye_kadi` varchar(200) NOT NULL,
  `uye_adi` text NOT NULL,
  `uye_link` varchar(200) NOT NULL,
  `uye_sifre` varchar(200) NOT NULL,
  `uye_eposta` varchar(200) NOT NULL,
  `uye_tarih` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `uye_rutbe` int(11) NOT NULL,
  `uye_onay` int(11) NOT NULL,
  `uye_pc_adi` text NOT NULL,
  `uye_pc_mac` text NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `uyeler`
--

INSERT INTO `uyeler` (`uye_id`, `uye_kadi`, `uye_adi`, `uye_link`, `uye_sifre`, `uye_eposta`, `uye_tarih`, `uye_rutbe`, `uye_onay`, `uye_pc_adi`, `uye_pc_mac`) VALUES
(19, '', 'Osman Yavuz', '', '251304fd1cc6de4bcb2503ab5ff38fa3', '', '2014-11-11 03:30:02', 1, 1, '', ''),
(23, 'deneme', 'Deneme üye', 'deneme', '869319c58c3f6146b6236fab6029c9f5', '', '2015-07-04 17:34:17', 2, 1, 'BLOW', '94:DB:C9:4D:27:5F');

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `ayarlar`
--
ALTER TABLE `ayarlar`
  ADD PRIMARY KEY (`id`);

--
-- Tablo için indeksler `uyeler`
--
ALTER TABLE `uyeler`
  ADD PRIMARY KEY (`uye_id`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `ayarlar`
--
ALTER TABLE `ayarlar`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
--
-- Tablo için AUTO_INCREMENT değeri `uyeler`
--
ALTER TABLE `uyeler`
  MODIFY `uye_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=24;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
