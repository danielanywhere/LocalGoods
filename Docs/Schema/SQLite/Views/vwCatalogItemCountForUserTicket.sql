/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
COUNT(CatalogItem.CatalogItemID) AS CatalogItemCount
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID = UserItem.UserItemID
GROUP BY UserItem.UserItemTicket
HAVING UserItem.UserItemTicket = {0};
