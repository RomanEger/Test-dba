CREATE TABLE Abonents (
  id serial primary key,
  fullName varchar(120) NOT NULL
);

CREATE TABLE Addresses (
  id serial primary key,
  abonentId int NOT NULL,
  streetId int NOT NULL,
  houseNumber varchar(7) NOT NULL,
  description varchar
);

CREATE TABLE PhoneNumbers (
  id serial primary key,
  abonentId int NOT NULL,
  phoneNumber varchar(15) NOT NULL,
  typeId int NOT NULL,
	unique(abonentId, typeId)
);

CREATE TABLE PhoneNumberTypes (
  id serial primary key,
  name varchar(20) NOT NULL
);

CREATE TABLE Streets (
  id serial primary key,
  name varchar(100) NOT NULL
);

ALTER TABLE Addresses ADD FOREIGN KEY (abonentId) REFERENCES Abonents (id);

ALTER TABLE PhoneNumbers ADD FOREIGN KEY (abonentId) REFERENCES Abonents (id);

ALTER TABLE Addresses ADD FOREIGN KEY (streetId) REFERENCES Streets (id);

ALTER TABLE PhoneNumbers ADD FOREIGN KEY (id) REFERENCES PhoneNumberTypes (id);

CREATE INDEX fullName_idx ON abonents (fullName);

CREATE UNIQUE INDEX phoneNumber_idx ON phoneNumbers (phoneNumber);

CREATE UNIQUE INDEX streetName_idx ON streets (name);

CREATE UNIQUE INDEX phoneNumberType_idx ON phoneNumberTypes (name);