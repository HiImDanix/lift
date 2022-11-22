CREATE TABLE Players (
	[session] varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	displayName varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	CONSTRAINT Players_pk PRIMARY KEY (ID)
);