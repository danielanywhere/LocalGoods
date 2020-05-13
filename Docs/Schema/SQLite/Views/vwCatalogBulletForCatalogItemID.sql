/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
CatalogBullet.BulletText,
CatalogBullet.BulletIndex,
CatalogBullet.CatalogBulletID,
CatalogBullet.CatalogBulletTicket
FROM CatalogBullet
WHERE CatalogBullet.CatalogItemID = {0}
ORDER BY
CatalogBullet.BulletIndex,
CatalogBullet.CatalogBulletID;
