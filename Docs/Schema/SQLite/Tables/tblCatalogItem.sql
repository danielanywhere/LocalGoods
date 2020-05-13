/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [CatalogItem];
CREATE TABLE [CatalogItem]
(
	[CatalogItemID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	[CatalogItemTicket] TEXT(36) NOT NULL,
	[DepartmentItemID] INTEGER NOT NULL DEFAULT 0,
	[UserItemID] INTEGER NOT NULL DEFAULT 0,
	[CityItemID] INTEGER NOT NULL DEFAULT 0,
	[DateCreated] TEXT(23) NULL DEFAULT CURRENT_TIMESTAMP,
	[DateUpdated] TEXT(23) NULL DEFAULT CURRENT_TIMESTAMP,
	[DateViewed] TEXT(23) NULL DEFAULT '1980-01-01 00:00:00.000',
	[Visible] INTEGER NOT NULL DEFAULT 1,
	[ViewCount] INTEGER NOT NULL DEFAULT 0,
	[ProductTitle] TEXT(255) NULL,
	[ProductDescription] TEXT(255) NULL,
	[ContactInfo] TEXT(255) NULL,
	[RatingCount] INTEGER NOT NULL DEFAULT 0,
	[StarCount] REAL NOT NULL DEFAULT 5,
	[ItemPrice] REAL NOT NULL DEFAULT 0,
	[ItemUnit] TEXT(12) NULL DEFAULT 'ea'
);
CREATE UNIQUE INDEX idxCatalogItem ON CatalogItem(CatalogItemID);
