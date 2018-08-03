--DROP TABLE ADDRESS;
--DROP TABLE CONTACTINFO;
--DROP TABLE PERSON;
--DROP TABLE COUNTRY;

--CREATE TABLE Person( 
--    [Id]			UNIQUEIDENTIFIER	PRIMARY KEY,
--    [Firstname]		VARCHAR(50),  
--    [Lastname]		VARCHAR(50),
--	[Age]			INT,
--	[Gender]		VARCHAR(50)
--);

--CREATE TABLE Country(
--    [Id]			UNIQUEIDENTIFIER	PRIMARY KEY,
--    [Name]			VARCHAR (255),
--    [PhoneCode]		VARCHAR (15),
--    [ISO2]			VARCHAR (2),
--    [ISO3]			VARCHAR (3)
--);

--CREATE TABLE Address( 
--    [Id]			UNIQUEIDENTIFIER	PRIMARY KEY,
--    [FK_Person]		UNIQUEIDENTIFIER	FOREIGN KEY REFERENCES Person(Id), 
--    [AddrLine1]		VARCHAR(50),  
--    [AddrLine2]		VARCHAR(50),  
--    [City]			VARCHAR(50),  
--    [State]			VARCHAR(50),  
--    [FK_Country]	UNIQUEIDENTIFIER	FOREIGN KEY REFERENCES Country(Id) NULL,
--    [Zipcode]		VARCHAR(20)
--);

--CREATE TABLE ContactInfo( 
--    [Id]			UNIQUEIDENTIFIER	PRIMARY KEY,
--	[FK_Person]		UNIQUEIDENTIFIER	FOREIGN KEY REFERENCES Person(Id), 
--    [FK_Country]	UNIQUEIDENTIFIER	FOREIGN KEY REFERENCES Country(Id) NULL,
--    [Number]		VARCHAR(20), 
--    [Ext]			VARCHAR(10),
--	[Email]			VARCHAR(255)
--);
