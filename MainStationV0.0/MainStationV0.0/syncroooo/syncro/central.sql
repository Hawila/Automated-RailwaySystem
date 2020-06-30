CREATE TABLE train (
    ID INT NOT NULL,
	code varchar(225),
	phone iNT ,
	PRIMARY KEY (ID),
);

CREATE TABLE Strain (
    ID int not null ,
    xLoc_small int,
    yLoc_small int,
	xLoc_main int,
    yLoc_main int,
	xLoc_central int,
    yLoc_central int,
	xLoc_train int,
    yLoc_trian int,
    trainStatus int,
	active int,
	path_id int not null,
	speed int,
	ID_main int,
	block_id int,
	PRIMARY KEY (ID),
	CONSTRAINT FK_train_ids FOREIGN KEY (ID)
    REFERENCES train(ID)
);

CREATE TABLE trainblock (
    ID int NOT NULL,
	region varchar(225),
	code varchar(225),
    x_start int ,
    x_end int ,
	y_start int ,
    y_end int ,
    PRIMARY KEY (ID),
);

CREATE TABLE sblock (
    ID int NOT NULL,
    blockClear int,
    PRIMARY KEY (ID),
	CONSTRAINT FK_block_ids FOREIGN KEY (ID)
    REFERENCES trainblock(ID)
);

CREATE TABLE semaphore (
    ID int NOT NULL,
	region varchar(225),
	code varchar(225),
    xLoc int ,
    yLoc int ,
	block_id int ,
    PRIMARY KEY (ID),
    CONSTRAINT FK_semblock_ids FOREIGN KEY (block_id)
    REFERENCES trainblock(ID)
);

CREATE TABLE Ssemaphore (
    ID int NOT NULL,
    semStatus int,
    PRIMARY KEY (ID),
	CONSTRAINT FK_SEM_ids FOREIGN KEY (ID)
    REFERENCES semaphore(ID)
);

CREATE TABLE levelcrossing (
    ID int NOT NULL,
	region varchar(225),
	code varchar(225),
    xLoc int NOT NULL,
    yLoc int NOT NULL,
	phone int NOT NULL,
	block_id int ,
    PRIMARY KEY (ID),
	CONSTRAINT FK_levelblock_ids FOREIGN KEY (block_id)
    REFERENCES trainblock(ID)
);

CREATE TABLE Slevelcrossing (
    ID int NOT NULL,
    LcStatus int NOT NULL,
	LcClear int NOT NULL,
    PRIMARY KEY (ID),
	CONSTRAINT FK_levelcrossing_ids FOREIGN KEY (ID)
    REFERENCES levelcrossing(ID)
);