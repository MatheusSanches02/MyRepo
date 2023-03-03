use myrepositoriesdb

create table Repos (
Id INT identity(1,1) NOT NULL PRIMARY KEY,
Name VARCHAR(300) NOT NULL,
Description VARCHAR(300) NOT NULL,
Language VARCHAR(100) NOT NULL,
LastUpdate DATETIME,
RepositorieOwner VARCHAR(300)
)

create table Favorite (
Id INT identity(1,1) NOT NULL PRIMARY KEY,
RepoId INT,
CONSTRAINT FK_RepoId FOREIGN KEY(RepoId) REFERENCES Repos(Id)
ON DELETE Cascade
)

