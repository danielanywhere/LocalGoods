/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
CatalogBullet.CatalogBulletID,
CatalogBullet.CatalogItemID
FROM CatalogBullet
WHERE CatalogBulletID IN({0});
