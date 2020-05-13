/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
UserItem.*
FROM UserItem
WHERE
UserItemTicket = {0} AND
([MemberEmail] = {1} OR
[MemberPassword] = {2});
