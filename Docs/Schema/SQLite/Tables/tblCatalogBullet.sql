/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [CatalogBullet];
CREATE TABLE [CatalogBullet]
(
	[CatalogBulletID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[CatalogBulletTicket] TEXT(36) NOT NULL, 
	[CatalogItemID] INTEGER NOT NULL DEFAULT 0, 
	[BulletIndex] INTEGER NOT NULL DEFAULT 0, 
	[BulletText] TEXT(255) NULL 
);
CREATE UNIQUE INDEX idxCatalogBullet ON CatalogBullet(CatalogBulletID);
