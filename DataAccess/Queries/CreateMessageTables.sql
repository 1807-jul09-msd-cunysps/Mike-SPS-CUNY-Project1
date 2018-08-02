--DROP TABLE MESSAGE;

CREATE TABLE Message( 
    [Id]			INT				PRIMARY KEY IDENTITY(1,1),  
    [Read]			BIT,  
    [Fullname]		VARCHAR(100),
	[Email]			VARCHAR(100),
	[Message]		VARCHAR(1000)
);
