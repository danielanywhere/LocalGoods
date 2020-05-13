/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE FROM TableIndex;
INSERT INTO TableIndex(TableIndexID, TableName)
VALUES
(1, 'CatalogBullet'),
(2, 'CatalogImage'),
(3, 'CatalogItem'),
(4, 'CityItem'),
(5, 'DepartmentItem'),
(6, 'Keyword'),
(7, 'KeywordLocation'),
(8, 'TableIndex'),
(9, 'UserItem');
SELECT * FROM TableIndex;
