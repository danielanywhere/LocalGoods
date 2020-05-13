/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
(COUNT(MemberEmail) = 0) AS EmailNotFound
FROM UserItem
WHERE MemberEmail = {0};
