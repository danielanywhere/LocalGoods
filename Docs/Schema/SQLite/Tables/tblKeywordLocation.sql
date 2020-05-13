/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS KeywordLocation;
CREATE TABLE KeywordLocation
(
	[KeywordLocationID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	[KeywordID] INTEGER NOT NULL DEFAULT 0,
	[TableIndexID] INTEGER NOT NULL DEFAULT 0,
	[RecordID] INTEGER NOT NULL DEFAULT 0
);
CREATE UNIQUE INDEX idxKeywordLocation ON KeywordLocation(KeywordLocationID);
