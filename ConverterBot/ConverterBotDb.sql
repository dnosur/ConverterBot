SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

CREATE TABLE `symbols` (
  `id` int(11) NOT NULL,
  `title` varchar(6) NOT NULL,
  `description` varchar(256) NOT NULL
) ;

INSERT INTO `symbols` (`id`, `title`, `description`) VALUES
(5, 'AED', 'United Arab Emirates Dirham'),
(6, 'NOK', 'Norwegian Krone'),
(7, 'NPR', 'Nepalese Rupee'),
(8, 'NZD', 'New Zealand Dollar'),
(9, 'OMR', 'Omani Rial'),
(10, 'PAB', 'Panamanian Balboa'),
(11, 'PEN', 'Peruvian Nuevo Sol'),
(12, 'PGK', 'Papua New Guinean Kina'),
(13, 'PHP', 'Philippine Peso'),
(14, 'PKR', 'Pakistani Rupee'),
(15, 'PLN', 'Polish Zloty'),
(16, 'PYG', 'Paraguayan Guarani'),
(17, 'QAR', 'Qatari Rial'),
(18, 'RON', 'Romanian Leu'),
(19, 'RSD', 'Serbian Dinar'),
(20, 'RUB', 'Russian Ruble'),
(21, 'RWF', 'Rwandan Franc'),
(22, 'SAR', 'Saudi Riyal'),
(23, 'NIO', 'Nicaraguan Córdoba'),
(24, 'NGN', 'Nigerian Naira'),
(25, 'NAD', 'Namibian Dollar'),
(26, 'MZN', 'Mozambican Metical'),
(27, 'LRD', 'Liberian Dollar'),
(28, 'LSL', 'Lesotho Loti'),
(29, 'LTL', 'Lithuanian Litas'),
(30, 'LVL', 'Latvian Lats'),
(31, 'LYD', 'Libyan Dinar'),
(32, 'MAD', 'Moroccan Dirham'),
(33, 'MDL', 'Moldovan Leu'),
(34, 'MGA', 'Malagasy Ariary'),
(35, 'SBD', 'Solomon Islands Dollar'),
(36, 'MKD', 'Macedonian Denar'),
(37, 'MNT', 'Mongolian Tugrik'),
(38, 'MOP', 'Macanese Pataca'),
(39, 'MRU', 'Mauritanian Ouguiya'),
(40, 'MUR', 'Mauritian Rupee'),
(41, 'MVR', 'Maldivian Rufiyaa'),
(42, 'MWK', 'Malawian Kwacha'),
(43, 'MXN', 'Mexican Peso'),
(44, 'MYR', 'Malaysian Ringgit'),
(45, 'MMK', 'Myanma Kyat'),
(46, 'LKR', 'Sri Lankan Rupee'),
(47, 'SCR', 'Seychellois Rupee'),
(48, 'SEK', 'Swedish Krona'),
(49, 'UYU', 'Uruguayan Peso'),
(50, 'UZS', 'Uzbekistan Som'),
(51, 'VEF', 'Venezuelan Bolívar Fuerte'),
(52, 'VES', 'Sovereign Bolivar'),
(53, 'VND', 'Vietnamese Dong'),
(54, 'VUV', 'Vanuatu Vatu'),
(55, 'WST', 'Samoan Tala'),
(56, 'XAF', 'CFA Franc BEAC'),
(57, 'XAG', 'Silver (troy ounce)'),
(58, 'XAU', 'Gold (troy ounce)'),
(59, 'XCD', 'East Caribbean Dollar'),
(60, 'XDR', 'Special Drawing Rights'),
(61, 'XOF', 'CFA Franc BCEAO'),
(62, 'XPF', 'CFP Franc'),
(63, 'YER', 'Yemeni Rial'),
(64, 'ZAR', 'South African Rand'),
(65, 'ZMK', 'Zambian Kwacha (pre-2013)'),
(66, 'USD', 'United States Dollar'),
(67, 'UGX', 'Ugandan Shilling'),
(68, 'UAH', 'Ukrainian Hryvnia'),
(69, 'TZS', 'Tanzanian Shilling'),
(70, 'SGD', 'Singapore Dollar'),
(71, 'SHP', 'Saint Helena Pound'),
(72, 'SLE', 'Sierra Leonean Leone'),
(73, 'SLL', 'Sierra Leonean Leone'),
(74, 'SOS', 'Somali Shilling'),
(75, 'SRD', 'Surinamese Dollar'),
(76, 'STD', 'São Tomé and Príncipe Dobra'),
(77, 'SVC', 'Salvadoran Colón'),
(78, 'SDG', 'South Sudanese Pound'),
(79, 'SYP', 'Syrian Pound'),
(80, 'THB', 'Thai Baht'),
(81, 'TJS', 'Tajikistani Somoni'),
(82, 'TMT', 'Turkmenistani Manat'),
(83, 'TND', 'Tunisian Dinar'),
(84, 'TOP', 'Tongan Paʻanga'),
(85, 'TRY', 'Turkish Lira'),
(86, 'TTD', 'Trinidad and Tobago Dollar'),
(87, 'TWD', 'New Taiwan Dollar'),
(88, 'SZL', 'Swazi Lilangeni'),
(89, 'ZMW', 'Zambian Kwacha'),
(90, 'LBP', 'Lebanese Pound'),
(91, 'KZT', 'Kazakhstani Tenge'),
(92, 'BWP', 'Botswanan Pula'),
(93, 'BYN', 'New Belarusian Ruble'),
(94, 'BYR', 'Belarusian Ruble'),
(95, 'BZD', 'Belize Dollar'),
(96, 'CAD', 'Canadian Dollar'),
(97, 'CDF', 'Congolese Franc'),
(98, 'CHF', 'Swiss Franc'),
(99, 'CLF', 'Chilean Unit of Account (UF)'),
(100, 'CLP', 'Chilean Peso'),
(101, 'CNH', 'Chinese Yuan Offshore'),
(102, 'CNY', 'Chinese Yuan'),
(103, 'COP', 'Colombian Peso'),
(104, 'CRC', 'Costa Rican Colón'),
(105, 'CUC', 'Cuban Convertible Peso'),
(106, 'CUP', 'Cuban Peso'),
(107, 'CVE', 'Cape Verdean Escudo'),
(108, 'CZK', 'Czech Republic Koruna'),
(109, 'BTN', 'Bhutanese Ngultrum'),
(110, 'BTC', 'Bitcoin'),
(111, 'BSD', 'Bahamian Dollar'),
(112, 'BRL', 'Brazilian Real'),
(113, 'AFN', 'Afghan Afghani'),
(114, 'ALL', 'Albanian Lek'),
(115, 'AMD', 'Armenian Dram'),
(116, 'ANG', 'Netherlands Antillean Guilder'),
(117, 'AOA', 'Angolan Kwanza'),
(118, 'ARS', 'Argentine Peso'),
(119, 'AUD', 'Australian Dollar'),
(120, 'AWG', 'Aruban Florin'),
(121, 'DJF', 'Djiboutian Franc'),
(122, 'AZN', 'Azerbaijani Manat'),
(123, 'BBD', 'Barbadian Dollar'),
(124, 'BDT', 'Bangladeshi Taka'),
(125, 'BGN', 'Bulgarian Lev'),
(126, 'BHD', 'Bahraini Dinar'),
(127, 'BIF', 'Burundian Franc'),
(128, 'BMD', 'Bermudan Dollar'),
(129, 'BND', 'Brunei Dollar'),
(130, 'BOB', 'Bolivian Boliviano'),
(131, 'BAM', 'Bosnia-Herzegovina Convertible Mark'),
(132, 'LAK', 'Laotian Kip'),
(133, 'DKK', 'Danish Krone'),
(134, 'DZD', 'Algerian Dinar'),
(135, 'IMP', 'Manx pound'),
(136, 'INR', 'Indian Rupee'),
(137, 'IQD', 'Iraqi Dinar'),
(138, 'IRR', 'Iranian Rial'),
(139, 'ISK', 'Icelandic Króna'),
(140, 'JEP', 'Jersey Pound'),
(141, 'JMD', 'Jamaican Dollar'),
(142, 'JOD', 'Jordanian Dinar'),
(143, 'JPY', 'Japanese Yen'),
(144, 'KES', 'Kenyan Shilling'),
(145, 'KGS', 'Kyrgystani Som'),
(146, 'KHR', 'Cambodian Riel'),
(147, 'KMF', 'Comorian Franc'),
(148, 'KPW', 'North Korean Won'),
(149, 'KRW', 'South Korean Won'),
(150, 'KWD', 'Kuwaiti Dinar'),
(151, 'KYD', 'Cayman Islands Dollar'),
(152, 'ILS', 'Israeli New Sheqel'),
(153, 'IDR', 'Indonesian Rupiah'),
(154, 'HUF', 'Hungarian Forint'),
(155, 'HTG', 'Haitian Gourde'),
(156, 'EGP', 'Egyptian Pound'),
(157, 'ERN', 'Eritrean Nakfa'),
(158, 'ETB', 'Ethiopian Birr'),
(159, 'EUR', 'Euro'),
(160, 'FJD', 'Fijian Dollar'),
(161, 'FKP', 'Falkland Islands Pound'),
(162, 'GBP', 'British Pound Sterling'),
(163, 'GEL', 'Georgian Lari'),
(164, 'DOP', 'Dominican Peso'),
(165, 'GGP', 'Guernsey Pound'),
(166, 'GIP', 'Gibraltar Pound'),
(167, 'GMD', 'Gambian Dalasi'),
(168, 'GNF', 'Guinean Franc'),
(169, 'GTQ', 'Guatemalan Quetzal'),
(170, 'GYD', 'Guyanaese Dollar'),
(171, 'HKD', 'Hong Kong Dollar'),
(172, 'HNL', 'Honduran Lempira'),
(173, 'HRK', 'Croatian Kuna'),
(174, 'GHS', 'Ghanaian Cedi'),
(175, 'ZWL', 'Zimbabwean Dollar');

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `telegram_id` varchar(16) NOT NULL
) ;

INSERT INTO `users` (`id`, `telegram_id`) VALUES
(9, '993793675');

CREATE TABLE `usersCurrencies` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `symbol_from_id` int(11) NOT NULL,
  `symbol_to_id` int(6) NOT NULL,
  `userid` int(11) DEFAULT NULL,
  `symbol_toid` int(11) DEFAULT NULL,
  `symbol_fromid` int(11) DEFAULT NULL
) ;

INSERT INTO `usersCurrencies` (`id`, `user_id`, `symbol_from_id`, `symbol_to_id`, `userid`, `symbol_toid`, `symbol_fromid`) VALUES
(2, 9, 66, 156, NULL, NULL, NULL);


ALTER TABLE `symbols`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `usersCurrencies`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `symbol_from_id` (`symbol_from_id`),
  ADD KEY `symbol_to_id` (`symbol_to_id`);


ALTER TABLE `symbols`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=176;

ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

ALTER TABLE `usersCurrencies`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;


ALTER TABLE `usersCurrencies`
  ADD CONSTRAINT `userscurrencies_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  ADD CONSTRAINT `userscurrencies_ibfk_2` FOREIGN KEY (`symbol_from_id`) REFERENCES `symbols` (`id`),
  ADD CONSTRAINT `userscurrencies_ibfk_3` FOREIGN KEY (`symbol_to_id`) REFERENCES `symbols` (`id`);
COMMIT;