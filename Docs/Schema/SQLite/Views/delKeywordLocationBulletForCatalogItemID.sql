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
	CatalogBullet.CatalogBulletID =
	KeywordLocation.RecordID
	INNER JOIN CatalogItem ON
	CatalogItem.CatalogItemID =
	CatalogBullet.CatalogItemID
	WHERE
	CatalogItem.CatalogItemID = {0} AND
	KeywordLocation.TableIndexID = 1
);
