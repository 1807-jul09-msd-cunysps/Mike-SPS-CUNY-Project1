--DROP TABLE MESSAGE;

CREATE TABLE Message( 
    [Id]			UNIQUEIDENTIFIER		PRIMARY KEY,  
    [WasRead]		BIT,  
    [Fullname]		VARCHAR(100),
	[Email]			VARCHAR(100),
	[Message]		VARCHAR(1000)
);
