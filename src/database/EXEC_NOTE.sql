/*markdown
### Select all from table
*/

SELECT * FROM Users;

/*markdown
### Select column info from table
*/

SELECT s.name as schema_name, t.name as table_name, c.* FROM sys.columns AS c
INNER JOIN sys.tables AS t ON t.object_id = c.object_id
INNER JOIN sys.schemas AS s ON s.schema_id = t.schema_id
WHERE t.name = 'Users' AND s.name = 'dbo';

/*markdown
### Select all tables in database
*/

SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'

INSERT INTO Users (id, name, password, refreshtoken, datecreated) VALUES (NEWID(), 'myUser', 'Password1!', NEWID(), SYSDATETIMEOFFSET())