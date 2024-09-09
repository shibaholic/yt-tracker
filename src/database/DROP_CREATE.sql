DROP TABLE UserAccount;

CREATE TABLE UserAccount (
    id UNIQUEIDENTIFIER NOT NULL,
    username NVARCHAR(255) NOT NULL,
    password_hash NVARCHAR(MAX) NULL,
    PRIMARY KEY(id)
);