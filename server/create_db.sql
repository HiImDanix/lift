
CREATE TABLE Accounts (
	ID int IDENTITY(0,1) NOT NULL,
	username varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	email varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	passwordHash varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT account_pk PRIMARY KEY (ID),
	CONSTRAINT account_username_unique UNIQUE (username)
);

CREATE TABLE Questions (
	ID int IDENTITY(0,1) NOT NULL,
	question varchar(128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	category varchar(32) COLLATE Latin1_General_CI_AS NULL,
	imgPath varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	RowVer timestamp NOT NULL,
	CONSTRAINT question_pk PRIMARY KEY (ID)
);

CREATE TABLE Answers (
	ID int IDENTITY(0,1) NOT NULL,
	answer varchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	questionID int NOT NULL,
	isCorrect bit NOT NULL,
	CONSTRAINT answer_pk PRIMARY KEY (ID),
	CONSTRAINT Answer_FK FOREIGN KEY (questionID) REFERENCES Questions(ID)
);

CREATE TABLE Rooms (
	ID int IDENTITY(0,1) NOT NULL,
	code varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	currentGameID int NULL,
	hostID int NULL,
	startTime bigint NULL,
	CONSTRAINT Lobby_PK PRIMARY KEY (ID)
);

CREATE TABLE QuizGames (
	ID int IDENTITY(0,1) NOT NULL,
	scoreboardID int NULL,
	roomID int NOT NULL,
	totalRounds int NOT NULL,
	currentRound int NULL,
	status varchar(100) COLLATE Latin1_General_CI_AS NOT NULL,
	currentQuizGameQuestionID int NULL,
	currentRoundStartTime bigint NULL,
	startTime bigint NULL,
	CONSTRAINT NewTable_PK PRIMARY KEY (ID)
);

CREATE TABLE Players (
	ID int IDENTITY(0,1) NOT NULL,
	[session] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	displayName varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	accountID int NULL,
	roomID int NOT NULL,
	CONSTRAINT player_pk PRIMARY KEY (ID),
	CONSTRAINT player_session_unique UNIQUE ([session])
);

CREATE TABLE QuizGameAnswers (
	ID int IDENTITY(0,1) NOT NULL,
	playerID int NOT NULL,
	answerID int NOT NULL,
	quizGameQuestionID int NOT NULL,
	answeredTime bigint NOT NULL,
	CONSTRAINT QuizGameAnswer_PK PRIMARY KEY (ID)
);

CREATE TABLE QuizGameQuestions (
	ID int IDENTITY(0,1) NOT NULL,
	questionID int NOT NULL,
	QuizGameID int NOT NULL,
	CONSTRAINT RoundQuestions_PK PRIMARY KEY (ID)
);


CREATE TABLE RoundQuestions (
	ID int IDENTITY(0,1) NOT NULL,
	questionID int NOT NULL,
	logoQuizGameID int NOT NULL,
	CONSTRAINT RoundQuestions_PK PRIMARY KEY (ID)
);

CREATE TABLE Scoreboards (
	ID int IDENTITY(0,1) NOT NULL,
	score int NOT NULL,
	playerID int NOT NULL,
	CONSTRAINT Scoreboard_PK PRIMARY KEY (ID)
);

CREATE TABLE Administrators (
	[email] [varchar](50) NOT NULL,
	[password] [varchar](128) NOT NULL,
	ID int IDENTITY(0,1) NOT NULL,
 	CONSTRAINT Administrators_PK PRIMARY KEY (ID)
);


-- LosingIsFunToo.dbo.QuizGames foreign keys

ALTER TABLE LosingIsFunToo.dbo.QuizGames ADD CONSTRAINT NewTable_FK FOREIGN KEY (scoreboardID) REFERENCES Scoreboards(ID);
ALTER TABLE LosingIsFunToo.dbo.QuizGames ADD CONSTRAINT QuizGames_FK FOREIGN KEY (roomID) REFERENCES Rooms(ID);
ALTER TABLE LosingIsFunToo.dbo.QuizGames ADD CONSTRAINT QuizGames_FK2 FOREIGN KEY (currentQuestionID) REFERENCES Questions(ID);


-- LosingIsFunToo.dbo.Players foreign keys

ALTER TABLE LosingIsFunToo.dbo.Players ADD CONSTRAINT Player_FK FOREIGN KEY (accountID) REFERENCES Accounts(ID);
ALTER TABLE LosingIsFunToo.dbo.Players ADD CONSTRAINT lobby_Fk FOREIGN KEY (roomID) REFERENCES Rooms(ID);


-- LosingIsFunToo.dbo.QuizGameAnswers foreign keys

ALTER TABLE LosingIsFunToo.dbo.QuizGameAnswers ADD CONSTRAINT QuizGameAnswer_FK FOREIGN KEY (playerID) REFERENCES Players(ID);
ALTER TABLE LosingIsFunToo.dbo.QuizGameAnswers ADD CONSTRAINT QuizGameAnswer_FK_1 FOREIGN KEY (answerID) REFERENCES Answers(ID);


-- LosingIsFunToo.dbo.Rooms foreign keys

ALTER TABLE LosingIsFunToo.dbo.Rooms ADD CONSTRAINT Lobby_FK_1 FOREIGN KEY (currentGameID) REFERENCES QuizGames(ID);
ALTER TABLE LosingIsFunToo.dbo.Rooms ADD CONSTRAINT Rooms_FK FOREIGN KEY (hostID) REFERENCES Players(ID);


-- LosingIsFunToo.dbo.RoundQuestions foreign keys

ALTER TABLE LosingIsFunToo.dbo.RoundQuestions ADD CONSTRAINT RoundQuestions_FK FOREIGN KEY (logoQuizGameID) REFERENCES QuizGames(ID);
ALTER TABLE LosingIsFunToo.dbo.RoundQuestions ADD CONSTRAINT RoundQuestions_FK_1 FOREIGN KEY (questionID) REFERENCES Questions(ID);


-- LosingIsFunToo.dbo.Scoreboards foreign keys

ALTER TABLE LosingIsFunToo.dbo.Scoreboards ADD CONSTRAINT Scoreboard_FK FOREIGN KEY (playerID) REFERENCES Players(ID);



-- Pre-seed data

-- ADministrators
INSERT INTO Administrators (email, password) VALUES('tinyDoggy12', '$2a$12$QCy8QfdJa39UuwnXaEIsN.a3K7.OxjBqeRsk1QpNfOEKlljbeVmqa');

-- Questions with answers
INSERT INTO Questions (question, category, imgPath) VALUES('What is this logo?', 'logo', 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Google_2015_logo.svg/2000px-Google_2015_logo.svg.png');
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Google', 0, 1);
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Microsoft', 0, 0);
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Apple', 0, 0);
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Facebook', 0, 0);

INSERT INTO Questions (question, category, imgPath) VALUES('What is this logo?', 'logo', 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Logo_NIKE.svg/1280px-Logo_NIKE.svg.png');
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Nike', 1, 1);
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Adidas', 1, 0);
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Puma', 1, 0);
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Reebok', 1, 0);

INSERT INTO Questions (question, category, imgPath) VALUES('What is this logo?', 'logo', 'https://upload.wikimedia.org/wikipedia/commons/thumb/b/b2/Bootstrap_logo.svg/2560px-Bootstrap_logo.svg.png');
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Bootstrap', 2, 1);
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Bulma', 2, 0);
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Bit', 2, 0);
INSERT INTO Answers (answer, questionID, isCorrect) VALUES('Foundation', 2, 0);



