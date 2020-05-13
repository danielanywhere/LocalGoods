/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [UserItem];
CREATE TABLE [UserItem]
(
	[UserItemID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
 [UserItemTicket] TEXT(36) NOT NULL,
	[CityItemID] INTEGER NOT NULL DEFAULT 0,
	[MemberUsername] TEXT(255) NULL,
	[MemberEmail] TEXT(255) NULL,
	[MemberPassword] TEXT(255) NULL
);
CREATE UNIQUE INDEX idxUserItem ON UserItem(UserItemID);
