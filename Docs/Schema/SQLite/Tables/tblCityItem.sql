/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [CityItem];
CREATE TABLE [CityItem]
(
	[CityItemID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	[CityItemTicket] TEXT(36) NOT NULL,
	[CityName] TEXT(255) NULL
);
CREATE UNIQUE INDEX idxCityItem ON CityItem(CityItemID);

