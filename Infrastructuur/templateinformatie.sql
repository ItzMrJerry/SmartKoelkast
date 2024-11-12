-- Sample data for Kitchens table (Keukens)
INSERT INTO `Kitchens` (`kitchen_name`, `location`) VALUES
('Centrum Keuken', 'Hoofdstraat 123, Stadsdam'),
('Buitenwijk Keuken', 'Lindelaan 456, Dorpveen');

-- Sample data for Customers table (Klanten)
INSERT INTO `Customers` (`first_name`, `last_name`, `email`, `phone_number`) VALUES
('Eva', 'Jansen', 'eva.jansen@voorbeeld.com', '010-123-4567'),
('Lars', 'De Vries', 'lars.devries@voorbeeld.com', '020-234-5678'),
('Sara', 'Bakker', 'sara.bakker@voorbeeld.com', '030-345-6789');

-- Sample data for Products table (Producten)
INSERT INTO `Products` (`barcode`, `product_name`, `category`, `price`, `size_ml`, `nutrition_facts`) VALUES
('001234567890', 'Biologische Melk', 'Zuivel', 3.99, 1000, 'Calorieën: 150; Eiwit: 8g'),
('001234567891', 'Amandel Pindakaas', 'Voorraadkast', 6.49, 500, 'Calorieën: 190; Eiwit: 7g'),
('001234567892', 'Volkoren Brood', 'Bakkerij', 2.99, 750, 'Calorieën: 120; Vezels: 3g');

-- Sample data for KitchenModules table (KeukenModules)
INSERT INTO `KitchenModules` (`kitchen_id`, `module_name`, `customer_id`) VALUES
(1, 'Koelkast', 1),
(1, 'Voorraadkast', 1),
(2, 'Vriezer', 3);

-- Sample data for Inventory table (Voorraad)
INSERT INTO `Inventory` (`module_id`, `product_id`, `quantity`, `expiry_date`) VALUES
(1, 1, 10, '2024-01-15'),
(2, 2, 5, '2024-02-20'),
(3, 3, 3, '2024-03-25');
