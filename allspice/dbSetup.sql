CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';



CREATE TABLE IF NOT EXISTS recipes(
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
   id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
   imgUrl TEXT NOT NULL COMMENT 'recipe photo',
   title TEXT NOT NULL COMMENT 'recipe title',
   subtitle TEXT NOT NULL COMMENT 'recipe subtitle',
   category TEXT NOT NULL COMMENT 'recipe category',
   creatorId VARCHAR(255) NOT NULL,
   FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS ingredients(
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL,
  ingredientname TEXT NOT NULL COMMENT 'ingredient name',
  quantity TEXT NOT NULL COMMENT 'ingredient quantity',
  recipeId int NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS steps(
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
  recipesteporder int NOT NULL COMMENT 'order for the steps',
  body TEXT NOT NULL COMMENT 'body for the step',
  recipeId int NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS favorites(
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  id INT NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

SELECT 
r.*,
i.*
FROM recipes r
JOIN ingredients i ON i.recipeId = r.id
WHERE r.id = 9;



/* Get all elves at a station */
/* From on a many to many FROM will always be the many to many table */
/* WHERE is the many to many table and which side of the relationship data you have */
/* JOIN starts with the data you actually want */
-- SELECT NOTE
--   a.*,
--   acctStations.id AS accountStationId
-- FROM accountstations acctStations
-- JOIN accounts a ON acctStations.accountId = a.id
-- WHERE acctStations.stationId = 10;




/* get all stations by account id */
-- SELECT NOTE
--   s.*,
--   acctStations.id AS accountStationId
-- FROM accountstations acctStations
-- JOIN stations s ON s.id = acctStations.stationId
-- WHERE acctStations.accountId = "61bb7496776066b1d032f988"

/* future populating data retrieved from the many-to-many */
-- SELECT NOTE
--   s.*,
--   acctStations.id AS accountStationId,
--   a.*
-- FROM accountstations acctStations
-- JOIN stations s ON s.id = acctStations.stationId
-- JOIN account a ON a.id = s.creatorId
-- WHERE acctStations.accountId = "61bb7496776066b1d032f988"

/* ANCHOR 
NOTE
FIXME
STUB
SECTION
TODO
REVIEW
  query
  _db.Query<AccountStationViewModel, Account, AccountStationViewModel>(sql, (vm, acct)=>{
    vm.creator = acct;
    return vm;
  }, new {accountId}, splitOn: 'id')
 */