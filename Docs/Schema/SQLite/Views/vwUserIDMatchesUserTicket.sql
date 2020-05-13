/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
UserItem.UserItemID = {0} AND
UserItem.UserItemTicket = {1} AS Result
FROM UserItem
WHERE UserItem.UserItemID = {0};
