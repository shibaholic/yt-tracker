INSERT INTO UserAccount VALUES (NEWID(), 'admin', 'AdminPassword');

SELECT * FROM UserAccount;

DELETE FROM UserAccount WHERE username = 'admin';