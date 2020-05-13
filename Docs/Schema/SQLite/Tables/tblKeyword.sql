/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [Keyword];
CREATE TABLE [Keyword]
(
	[KeywordID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
 [KeywordName] TEXT(255) NULL
);
CREATE UNIQUE INDEX idxKeyword ON Keyword(KeywordID);
