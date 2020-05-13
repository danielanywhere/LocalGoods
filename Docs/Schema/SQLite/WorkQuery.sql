/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
/*
	SELECT KeywordLocationID
	FROM KeywordLocation
	INNER JOIN CatalogItem ON
	KeywordLocation.RecordID
	INNER JOIN CatalogBullet ON
	CatalogItem.CatalogItemID =
	CatalogBullet.CatalogItemID
	WHERE
	CatalogItem.CatalogItemID = 6 AND
	KeywordLocation.TableIndexID = 1;
*/
/*
INSERT INTO KeywordLocation(
KeywordID,
TableIndexID,
RecordID)
VALUES
(25, 1, 6);
*/
/*
INSERT INTO CatalogBullet(
CatalogBulletTicket,
CatalogItemID,
BulletIndex,
BulletText)
VALUES
('3D9ADE9F-9922-4A95-B018-338871ECBB77', 6, 0, 'Bullet text.'),
('8FE0B696-B232-4DB9-8DF3-893D51EE748E', 6, 1, 'This is.');
*/
/*
Keywords 71, 72, 3, 11
*/
/*
INSERT INTO Keyword(
KeywordName)
VALUES
('bullet'),
('text');
*/
/*
SELECT * FROM Keyword;
*/
/*
SELECT * FROM CatalogBullet;
*/

/*
SELECT * FROM KeywordLocation;
*/
/* KeywordLocationIDs 125, 126 */
/*
SELECT * FROM CatalogBullet;
*/
/* CatalogBulletIDs 20, 21 */
/*
UPDATE KeywordLocation
SET RecordID = 20
WHERE KeywordLocationID = 125;
UPDATE KeywordLocation
SET RecordID = 21
WHERE KeywordLocationID = 126;
*/
/*
SELECT KeywordLocationID
FROM KeywordLocation
INNER JOIN CatalogBullet ON
CatalogBullet.CatalogBulletID =
KeywordLocation.RecordID
INNER JOIN CatalogItem ON
CatalogItem.CatalogItemID =
CatalogBullet.CatalogItemID
WHERE
CatalogItem.CatalogItemID = 6 AND
KeywordLocation.TableIndexID = 1;
*/

/*
SELECT * FROM UserItem;
*/
/*
SELECT
UserItem.UserItemID = 1 AND
UserItem.UserItemTicket = '6C6ECEFA-C486-4512-9271-C8BF00F86B14' AS Result
FROM UserItem
WHERE UserItem.UserItemID = 1;
*/

/*
SELECT
UserItem.*
FROM UserItem
WHERE
MemberEmail = 'danielanywhere@hotmail.com' AND
MemberPassword = 'password';
*/

/*
SELECT
(COUNT(CatalogItem.CatalogItemID) > 0) AS UserAuthorizedForCatalog
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID =
UserItem.UserItemID
WHERE
CatalogItem.CatalogItemID = 1 AND
UserItem.UserItemTicket = '6C6ECEFA-C486-4512-9271-C8BF00F86B14';
*/

/*
SELECT
(COUNT(MemberUsername) = 0) AS UsernameNotFound
FROM UserItem
WHERE MemberUsername = 'danielanywher';
*/

/* GUIDs must be specified in upper case. */
/*
SELECT
COUNT(CatalogItem.CatalogItemID) AS CatalogItemCount
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID = UserItem.UserItemID
GROUP BY UserItem.UserItemTicket
HAVING UserItem.UserItemTicket = '6C6ECEFA-C486-4512-9271-C8BF00F86B14';
*/

/*
DELETE FROM CatalogItem WHERE CatalogItemID = 6;
SELECT * FROM CatalogItem;
*/

/*
SELECT
CatalogItem.CatalogItemID,
CatalogItem.CatalogItemTicket,
CatalogItem.DepartmentItemID,
CatalogItem.UserItemID,
CatalogItem.DateCreated,
CatalogItem.DateUpdated,
CatalogItem.DateViewed,
CatalogItem.Visible,
CatalogItem.ViewCount,
CatalogItem.ProductTitle,
CatalogItem.ProductDescription,
CatalogItem.StarCount,
CatalogItem.ItemPrice,
CatalogItem.ItemUnit,
DepartmentItem.DepartmentName,
CityItem.CityName,
UserItem.CityItemID,
(
	SELECT
	CatalogImage.ImageURL
	FROM CatalogImage
	WHERE CatalogImage.CatalogItemID =
	CatalogItem.CatalogItemID
	ORDER BY CatalogImage.ImageIndex
	LIMIT 1
) AS ImageURL
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID = UserItem.UserItemID
INNER JOIN DepartmentItem ON
CatalogItem.DepartmentItemID =
DepartmentItem.DepartmentItemID
INNER JOIN CityItem ON
CatalogItem.CityItemID =
CityItem.CityItemID
WHERE (CatalogItem.CityItemID = 1 OR
(UserItem.CityItemID = 1 AND
CatalogItem.CityItemID = 0)) AND
CatalogItem.CatalogItemID IN(1,2,3,4,5) AND
CatalogItem.Visible = 1
ORDER BY
CatalogItem.DepartmentItemID,
CatalogItem.DateCreated DESC;
*/

/*
SELECT
CatalogItem.CatalogItemID,
CatalogItem.CatalogItemTicket,
CatalogItem.DepartmentItemID,
CatalogItem.UserItemID,
CatalogItem.DateCreated,
CatalogItem.DateUpdated,
CatalogItem.DateViewed,
CatalogItem.Visible,
CatalogItem.ViewCount,
CatalogItem.ProductTitle,
CatalogItem.ProductDescription,
CatalogItem.StarCount,
CatalogItem.ItemPrice,
CatalogItem.ItemUnit,
DepartmentItem.DepartmentName,
CityItem.CityName,
UserItem.CityItemID,
(
	SELECT
	CatalogImage.ImageURL
	FROM CatalogImage
	WHERE CatalogImage.CatalogItemID =
	CatalogItem.CatalogItemID
	ORDER BY CatalogImage.ImageIndex
	LIMIT 1
) AS ImageURL
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID = UserItem.UserItemID
INNER JOIN DepartmentItem ON
CatalogItem.DepartmentItemID =
DepartmentItem.DepartmentItemID
INNER JOIN CityItem ON
CatalogItem.CityItemID =
CityItem.CityItemID
WHERE (CatalogItem.CityItemID = 1 OR
(UserItem.CityItemID = 1 AND
CatalogItem.CityItemID = 0)) AND
CatalogItem.Visible = 1
ORDER BY
CatalogItem.DepartmentItemID,
CatalogItem.DateCreated DESC;
*/

/*
SELECT
CatalogItem.CatalogItemID,
CatalogItem.CatalogItemTicket,
CatalogItem.DepartmentItemID,
CatalogItem.UserItemID,
CatalogItem.DateCreated,
CatalogItem.DateUpdated,
CatalogItem.DateViewed,
CatalogItem.Visible,
CatalogItem.ViewCount,
CatalogItem.ProductTitle,
CatalogItem.ProductDescription,
CatalogItem.StarCount,
CatalogItem.ItemPrice,
CatalogItem.ItemUnit,
DepartmentItem.DepartmentName,
CityItem.CityName,
UserItem.CityItemID,
(
	SELECT
	CatalogImage.ImageURL
	FROM CatalogImage
	WHERE CatalogImage.CatalogItemID =
	CatalogItem.CatalogItemID
	ORDER BY CatalogImage.ImageIndex
	LIMIT 1
) AS ImageURL
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID =
UserItem.UserItemID
INNER JOIN DepartmentItem ON
CatalogItem.DepartmentItemID =
DepartmentItem.DepartmentItemID
INNER JOIN CityItem ON
CatalogItem.CityItemID =
CityItem.CityItemID
WHERE CatalogItem.CatalogItemID IN(1,2,3,4,5) AND
CatalogItem.Visible = 1
ORDER BY
CatalogItem.DepartmentItemID,
CatalogItem.DateCreated DESC;
*/

/*
SELECT
CatalogItem.CatalogItemID,
CatalogItem.CatalogItemTicket,
CatalogItem.DepartmentItemID,
CatalogItem.UserItemID,
CatalogItem.DateCreated,
CatalogItem.DateUpdated,
CatalogItem.DateViewed,
CatalogItem.Visible,
CatalogItem.ViewCount,
CatalogItem.ProductTitle,
CatalogItem.ProductDescription,
CatalogItem.StarCount,
CatalogItem.ItemPrice,
CatalogItem.ItemUnit,
DepartmentItem.DepartmentName,
CityItem.CityName,
UserItem.CityItemID,
(
	SELECT
	CatalogImage.ImageURL
	FROM CatalogImage
	WHERE CatalogImage.CatalogItemID =
	CatalogItem.CatalogItemID
	ORDER BY CatalogImage.ImageIndex
	LIMIT 1
) AS ImageURL
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID = UserItem.UserItemID
INNER JOIN DepartmentItem ON
CatalogItem.DepartmentItemID =
DepartmentItem.DepartmentItemID
INNER JOIN CityItem ON
CatalogItem.CityItemID =
CityItem.CityItemID
WHERE (CatalogItem.CityItemID = 1 OR
(UserItem.CityItemID = 1 AND
CatalogItem.CityItemID = 0)) AND
CatalogItem.CatalogItemID IN(1,2,3,4,5) AND
CatalogItem.Visible = 1
ORDER BY
CatalogItem.DepartmentItemID,
CatalogItem.DateCreated DESC;
*/

/* SELECT * FROM UserItem; */

/*
SELECT
CatalogItem.CatalogItemID,
CatalogItem.CatalogItemTicket,
CatalogItem.DepartmentItemID,
CatalogItem.UserItemID,
CatalogItem.DateCreated,
CatalogItem.DateUpdated,
CatalogItem.DateViewed,
CatalogItem.Visible,
CatalogItem.ViewCount,
CatalogItem.ProductTitle,
CatalogItem.ProductDescription,
CatalogItem.StarCount,
CatalogItem.ItemPrice,
CatalogItem.ItemUnit,
DepartmentItem.DepartmentName,
CityItem.CityName,
UserItem.CityItemID,
(
	SELECT
	CatalogImage.ImageURL
	FROM CatalogImage
	WHERE CatalogImage.CatalogItemID =
	CatalogItem.CatalogItemID
	ORDER BY CatalogImage.ImageIndex
	LIMIT 1
) AS ImageURL
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID = UserItem.UserItemID
INNER JOIN DepartmentItem ON
CatalogItem.DepartmentItemID =
DepartmentItem.DepartmentItemID
INNER JOIN CityItem ON
CatalogItem.CityItemID =
CityItem.CityItemID
WHERE CatalogItem.CatalogItemID IN(1,2,3,4,5) AND
UserItem.UserItemTicket = '6C6ECEFA-C486-4512-9271-C8BF00F86B14'
ORDER BY
CatalogItem.CatalogItemID;
*/

/*
DELETE FROM CatalogItem WHERE CatalogItemID = 11;
*/

/*
SELECT * FROM CatalogItem;
SELECT * FROM CatalogImage;
*/

/*
SELECT
CatalogItem.*,
DepartmentItem.DepartmentName,
UserItem.MemberUsername,
CityItem.CityName
FROM ((CatalogItem
LEFT JOIN DepartmentItem ON
CatalogItem.DepartmentItemID =
DepartmentItem.DepartmentItemID)
LEFT JOIN UserItem ON
CatalogItem.UserItemID = UserItem.UserItemID)
LEFT JOIN CityItem ON
CatalogItem.CityItemID = CityItem.CityItemID
WHERE CatalogItem.CatalogItemID = 11;
*/

/*
SELECT *
FROM Keyword
ORDER BY KeywordID DESC
LIMIT 10;

SELECT *
FROM KeywordLocation
ORDER BY KeywordLocationID DESC
LIMIT 10;
*/

/*
DELETE FROM CityRequest WHERE CityRequestID = 5;
SELECT * FROM CityRequest;
*/
