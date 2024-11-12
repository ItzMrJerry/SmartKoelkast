CREATE TABLE IF NOT EXISTS `Kitchens` (
	`kitchen_id` int AUTO_INCREMENT NOT NULL,
	`kitchen_name` varchar(255) NOT NULL DEFAULT '100',
	`location` varchar(255) NOT NULL DEFAULT '255',
    `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`kitchen_id`)
);

CREATE TABLE IF NOT EXISTS `Customers` (
	`customer_id` int AUTO_INCREMENT NOT NULL,
	`first_name` varchar(255) NOT NULL DEFAULT '100',
	`last_name` varchar(255) NOT NULL DEFAULT '100',
	`email` varchar(255) NOT NULL DEFAULT '255',
	`phone_number` varchar(255) NOT NULL DEFAULT '15',
	PRIMARY KEY (`customer_id`)
);

CREATE TABLE IF NOT EXISTS `Products` (
	`product_id` int AUTO_INCREMENT NOT NULL,
    `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`barcode` varchar(255) NOT NULL DEFAULT '50',
	`product_name` varchar(255) NOT NULL DEFAULT '100',
	`category` varchar(255) NOT NULL DEFAULT '100',
	`price` decimal(10,0) NOT NULL,
	`size_ml` int NOT NULL,
	`nutrition_facts` varchar(255) NOT NULL,
	PRIMARY KEY (`product_id`)
);

CREATE TABLE IF NOT EXISTS `Inventory` (
	`inventory_id` int AUTO_INCREMENT NOT NULL,
	`module_id` int NOT NULL,
	`product_id` int NOT NULL,
	`quantity` int NOT NULL,
    `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	`expiry_date` date NOT NULL,
	PRIMARY KEY (`inventory_id`)
);

CREATE TABLE IF NOT EXISTS `KitchenModules` (
	`module_id` int AUTO_INCREMENT NOT NULL,
	`kitchen_id` int NOT NULL,
	`module_name` varchar(255) NOT NULL DEFAULT '100',
    `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	`customer_id` int NOT NULL,
	PRIMARY KEY (`module_id`)
);




ALTER TABLE `Inventory` ADD CONSTRAINT `Inventory_fk1` FOREIGN KEY (`module_id`) REFERENCES `KitchenModules`(`module_id`);

ALTER TABLE `Inventory` ADD CONSTRAINT `Inventory_fk2` FOREIGN KEY (`product_id`) REFERENCES `Products`(`product_id`);
ALTER TABLE `KitchenModules` ADD CONSTRAINT `KitchenModules_fk1` FOREIGN KEY (`kitchen_id`) REFERENCES `Kitchens`(`kitchen_id`);

ALTER TABLE `KitchenModules` ADD CONSTRAINT `KitchenModules_fk5` FOREIGN KEY (`customer_id`) REFERENCES `Customers`(`customer_id`);