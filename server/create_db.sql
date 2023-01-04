CREATE TABLE Accounts (
	ID int IDENTITY(0,1) NOT NULL,
	username varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	email varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	passwordHash varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT account_pk PRIMARY KEY (ID),
	CONSTRAINT account_username_unique UNIQUE (username)
);


CREATE TABLE Administrators (
	email varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	password varchar(128) COLLATE Latin1_General_CI_AS NOT NULL,
	ID int IDENTITY(0,1) NOT NULL,
	CONSTRAINT Administrators_PK PRIMARY KEY (ID)
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


CREATE TABLE Rooms (
	ID int IDENTITY(0,1) NOT NULL,
	code varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	currentGameID int NULL,
	hostID int NULL,
	startTime bigint NULL,
	CONSTRAINT Lobby_PK PRIMARY KEY (ID)
);


CREATE TABLE ScoreboardLines (
	ID int IDENTITY(0,1) NOT NULL,
	Score int NOT NULL,
	PlayerID int NOT NULL,
	QuizGameID int NOT NULL,
	CONSTRAINT Scoreboard_PK PRIMARY KEY (ID)
);


-- LosingIsFunToo.dbo.Players foreign keys

ALTER TABLE LosingIsFunToo.dbo.Players ADD CONSTRAINT Player_FK FOREIGN KEY (accountID) REFERENCES Accounts(ID);
ALTER TABLE LosingIsFunToo.dbo.Players ADD CONSTRAINT lobby_Fk FOREIGN KEY (roomID) REFERENCES Rooms(ID);


-- LosingIsFunToo.dbo.QuizGameAnswers foreign keys

ALTER TABLE LosingIsFunToo.dbo.QuizGameAnswers ADD CONSTRAINT QuizGameAnswer_FK FOREIGN KEY (playerID) REFERENCES Players(ID);
ALTER TABLE LosingIsFunToo.dbo.QuizGameAnswers ADD CONSTRAINT QuizGameAnswers_FK FOREIGN KEY (quizGameQuestionID) REFERENCES QuizGameQuestions(ID);
ALTER TABLE LosingIsFunToo.dbo.QuizGameAnswers ADD CONSTRAINT QuizGameAnswers_FK_1 FOREIGN KEY (answerID) REFERENCES Answers(ID) ON DELETE CASCADE;

-- LosingIsFunToo.dbo.QuizGameQuestions foreign keys

ALTER TABLE LosingIsFunToo.dbo.QuizGameQuestions ADD CONSTRAINT RoundQuestions_FK FOREIGN KEY (QuizGameID) REFERENCES QuizGames(ID);
ALTER TABLE LosingIsFunToo.dbo.QuizGameQuestions ADD CONSTRAINT RoundQuestions_FK_1 FOREIGN KEY (questionID) REFERENCES Questions(ID);


-- LosingIsFunToo.dbo.QuizGames foreign keys

ALTER TABLE LosingIsFunToo.dbo.QuizGames ADD CONSTRAINT QuizGames_FK FOREIGN KEY (roomID) REFERENCES Rooms(ID);


-- LosingIsFunToo.dbo.Rooms foreign keys

ALTER TABLE LosingIsFunToo.dbo.Rooms ADD CONSTRAINT Lobby_FK_1 FOREIGN KEY (currentGameID) REFERENCES QuizGames(ID);
ALTER TABLE LosingIsFunToo.dbo.Rooms ADD CONSTRAINT Rooms_FK FOREIGN KEY (hostID) REFERENCES Players(ID);


-- LosingIsFunToo.dbo.Scoreboards foreign keys

ALTER TABLE LosingIsFunToo.dbo.Scoreboards ADD CONSTRAINT Scoreboard_FK FOREIGN KEY (playerID) REFERENCES Players(ID);

-- LosingIsFunToo.dbo.ScoreboardLines foreign keys

ALTER TABLE LosingIsFunToo.dbo.ScoreboardLines ADD CONSTRAINT ScoreboardLine_FK FOREIGN KEY (QuizGameID) REFERENCES QuizGames(ID);
ALTER TABLE LosingIsFunToo.dbo.ScoreboardLines ADD CONSTRAINT Scoreboard_FK FOREIGN KEY (PlayerID) REFERENCES Players(ID);


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



