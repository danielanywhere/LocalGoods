/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [TableIndex];
CREATE TABLE [TableIndex]
(
	[TableIndexID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[TableName] TEXT(255) NULL
);
CREATE UNIQUE INDEX idxTableIndex ON TableIndex(TableIndexID);

