DROP DATABASE IF EXISTS pantry_mate
CREATE DATABASE pantry_mate

---------------------------------------------------------
DROP TABLE IF EXISTS brand;
CREATE TABLE brand (
	brand_id serial PRIMARY KEY,
	name VARCHAR (100) UNIQUE NOT NULL,
	description VARCHAR(1000),
	created_on TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_on TIMESTAMP 
);
---------------------------------------------------------
DROP TABLE IF EXISTS account;
CREATE TABLE account (
	account_id serial PRIMARY KEY,
	username VARCHAR (100) UNIQUE NOT NULL,
	password_hash TEXT NOT NULL,
	created_on TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_on TIMESTAMP,
	last_login TIMESTAMP,
	active boolean
);
---------------------------------------------------------
DROP TABLE IF EXISTS profile;
CREATE TABLE profile (
	profile_id serial PRIMARY KEY,
	first_name VARCHAR(100),
	last_name VARCHAR(100),
	email_address VARCHAR(100),
	phone_number VARCHAR(20),
	account_id INT UNIQUE NOT NULL,
	created_on TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_on TIMESTAMP,
	FOREIGN KEY (account_id) REFERENCES account(account_id) ON DELETE CASCADE
);
---------------------------------------------------------
DROP TABLE IF EXISTS inventory;
CREATE TABLE inventory (
	inventory_id serial PRIMARY KEY,
	name VARCHAR (100) NOT NULL,
	description VARCHAR(1000),
	account_id INT NOT NULL,
	created_on TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_on TIMESTAMP,
	FOREIGN KEY (account_id) REFERENCES account(account_id) ON DELETE CASCADE
);
---------------------------------------------------------
DROP TABLE IF EXISTS item;
CREATE TABLE item (
	item_id serial PRIMARY KEY,
	name VARCHAR (100) NOT NULL,
	description VARCHAR(1000),
	price DECIMAL DEFAULT 0.00,
	account_id INT NOT NULL,
	brand_id INT,
	created_on TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_on TIMESTAMP,
	FOREIGN KEY (brand_id) REFERENCES brand(brand_id),
	FOREIGN KEY (account_id) REFERENCES account(account_id) ON DELETE CASCADE
);
---------------------------------------------------------
DROP TABLE IF EXISTS inventory_item;
CREATE TABLE inventory_item (
	created_on TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_on TIMESTAMP,
	inventory_id int NOT NULL,
  	item_id int NOT NULL,
  	PRIMARY KEY (inventory_id, item_id),
  	FOREIGN KEY (inventory_id) REFERENCES inventory(inventory_id) ON UPDATE CASCADE,
  	FOREIGN KEY (item_id) REFERENCES item(item_id) ON UPDATE CASCADE
);
---------------------------------------------------------

---------------------------------------------------------