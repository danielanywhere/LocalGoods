/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [ConfigItem];
CREATE TABLE [ConfigItem]
(
	[ConfigItemID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	[ConfigItemTicket] TEXT(36) NOT NULL,
	[ConfigName] TEXT(255) NULL,
	[ConfigValue] TEXT(255) NULL
);
CREATE UNIQUE INDEX idxConfigItem ON ConfigItem(ConfigItemID);

