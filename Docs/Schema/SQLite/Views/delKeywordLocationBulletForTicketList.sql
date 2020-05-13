/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE
FROM KeywordLocation
WHERE
KeywordLocationID IN(
	SELECT KeywordLocationID
	FROM KeywordLocation
	INNER JOIN CatalogBullet ON
	KeywordLocation.RecordID =
	CatalogBullet.CatalogBulletID
	WHERE
	KeywordLocation.TableIndexID = 1 AND 
	CatalogBullet.CatalogBulletTicket IN({0})
);
