--DROP TABLE ADDRESS;
--DROP TABLE PHONE;
--DROP TABLE CONTACTINFO;
--DROP TABLE PERSON;
--DROP TABLE COUNTRY;

CREATE TABLE Person( 
    [Id]			INT				PRIMARY KEY IDENTITY(1,1),  
    [Firstname]		VARCHAR(50),  
    [Lastname]		VARCHAR(50),
	[Age]			INT,
	[Gender]		VARCHAR(50)
);

CREATE TABLE Country(
    [Id]			INT				PRIMARY KEY IDENTITY (1, 1),
    [Name]			VARCHAR (255),
    [PhoneCode]		VARCHAR (15),
    [ISO2]			VARCHAR (2),
    [ISO3]			VARCHAR (3)
);

CREATE TABLE Address( 
    [Id]			INT				PRIMARY KEY IDENTITY(1,1),  
    [FK_Person]		INT				FOREIGN KEY REFERENCES Person(Id), 
    [AddrLine1]		VARCHAR(50),  
    [AddrLine2]		VARCHAR(50),  
    [City]			VARCHAR(50),  
    [State]			VARCHAR(50),  
    [FK_Country]	INT				FOREIGN KEY REFERENCES Country(Id),
    [Zipcode]		VARCHAR(20)
);

CREATE TABLE ContactInfo( 
    [Id]			INT				PRIMARY KEY IDENTITY(1,1),  
	[FK_Person]		INT				FOREIGN KEY REFERENCES Person(Id), 
    [FK_Country]	INT				FOREIGN KEY REFERENCES Country(Id) NULL,
    [Number]		VARCHAR(20), 
    [Ext]			VARCHAR(10),
	[Email]			VARCHAR(255)
);
