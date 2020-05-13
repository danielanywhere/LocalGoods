/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [CatalogImage];
CREATE TABLE [CatalogImage]
(
	[CatalogImageID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
 [CatalogImageTicket] TEXT(36) NOT NULL,
	[CatalogItemID] INTEGER NOT NULL DEFAULT 0,
	[ImageIndex] INTEGER NOT NULL DEFAULT 0,
	[ImageURL] TEXT(255) NULL
);
CREATE UNIQUE INDEX idxCatalogImage ON CatalogImage(CatalogImageID);
