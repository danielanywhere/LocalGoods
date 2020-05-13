/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
(COUNT(MemberUsername) = 0) AS UsernameNotFound
FROM UserItem
WHERE MemberUsername = {0};
