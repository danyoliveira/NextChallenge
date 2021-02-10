CREATE DATABASE NextChallenge
Create table [User](
IdUser uniqueidentifier PRIMARY KEY default NEWID(),
Username varchar(120),
Password varchar(50),
CreateDate Datetime, 
IsActive bit
)
Create table [Topic](
IdTopic uniqueidentifier PRIMARY KEY default NEWID(),
IdUser uniqueidentifier,
Title varchar(150),
Body varchar(max),
CreateDate Datetime, 
IsActive bit
)

ALTER TABLE [Topic]
ADD FOREIGN KEY (IdUser) REFERENCES [User](IdUser);