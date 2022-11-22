-- This is only for the current use case. Remove later!

CREATE TABLE Players (
	[session] varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	displayName varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	CONSTRAINT Players_pk PRIMARY KEY (ID)
);

-- The actual SQL for our app

CREATE TABLE Account (
	username varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	email varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	passwordHash varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	accountID int IDENTITY(0,1) NOT NULL,
	CONSTRAINT account_pk PRIMARY KEY (accountID),
	CONSTRAINT account_username_unique UNIQUE (username)
);

CREATE TABLE Question (
	questionID int IDENTITY(0,1) NOT NULL,
	question varchar(128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	startTime datetime2(0) NULL,
	imgPath varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT question_pk PRIMARY KEY (questionID)
);

CREATE TABLE Lobby (
	code varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	lobbyID int NOT NULL,
	gameID int NULL,
	CONSTRAINT Lobby_PK PRIMARY KEY (lobbyID)
);

CREATE TABLE Answer (
	answerID int IDENTITY(0,1) NOT NULL,
	answer varchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	questionID int NOT NULL,
	isCorrect bit NOT NULL,
	CONSTRAINT answer_pk PRIMARY KEY (answerID),
	CONSTRAINT Answer_FK FOREIGN KEY (questionID) REFERENCES Question(questionID)
);

CREATE TABLE Player (
	playerID int IDENTITY(0,1) NOT NULL,
	[session] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	displayName varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	accountID int NULL,
	lobbyID int NOT NULL,
	CONSTRAINT player_pk PRIMARY KEY (playerID),
	CONSTRAINT player_session_unique UNIQUE ([session]),
	CONSTRAINT Player_FK FOREIGN KEY (accountID) REFERENCES Account(accountID),
	CONSTRAINT lobby_Fk FOREIGN KEY (lobbyID) REFERENCES Lobby(lobbyID)
);

CREATE TABLE Scoreboard (
	scoreboardID int IDENTITY(0,1) NOT NULL,
	score int NOT NULL,
	playerID int NOT NULL,
	CONSTRAINT Scoreboard_PK PRIMARY KEY (scoreboardID),
	CONSTRAINT Scoreboard_FK FOREIGN KEY (playerID) REFERENCES Player(playerID)
);

CREATE TABLE QuizGameAnswer (
	quizGameID int IDENTITY(0,1) NOT NULL,
	playerID int NOT NULL,
	answerID int NOT NULL,
	CONSTRAINT QuizGameAnswer_PK PRIMARY KEY (quizGameID),
	CONSTRAINT QuizGameAnswer_FK FOREIGN KEY (playerID) REFERENCES Player(playerID),
	CONSTRAINT QuizGameAnswer_FK_1 FOREIGN KEY (answerID) REFERENCES Answer(answerID)
);

CREATE TABLE LogoQuizGame (
	logoQuizGameID int IDENTITY(0,1) NOT NULL,
	durationMs int NOT NULL,
	maxPlayers int NOT NULL,
	startTime datetime2(0) NULL,
	scoreboardID int NULL,
	CONSTRAINT NewTable_PK PRIMARY KEY (logoQuizGameID),
	CONSTRAINT NewTable_FK FOREIGN KEY (scoreboardID) REFERENCES Scoreboard(scoreboardID)
);