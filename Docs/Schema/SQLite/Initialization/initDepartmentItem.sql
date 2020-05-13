/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE FROM DepartmentItem;
INSERT INTO DepartmentItem(DepartmentItemID, DepartmentItemTicket, DepartmentName)
VALUES
(1, '548C6970-E014-4F53-917F-73592989D1A8', 'Food'),
(2, '8D19F745-B84C-4879-86A6-850DCB67136F', 'Supplies');
SELECT * FROM DepartmentItem;
