/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [DepartmentItem];
CREATE TABLE [DepartmentItem]
(
	[DepartmentItemID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	[DepartmentItemTicket] TEXT(36) NOT NULL,
	[DepartmentName] TEXT(255) NULL
);
CREATE UNIQUE INDEX idxDepartmentItem ON DepartmentItem(DepartmentItemID);
