/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
(COUNT(CatalogItem.CatalogItemID) > 0) AS UserAuthorizedForCatalog
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID =
UserItem.UserItemID
WHERE
CatalogItem.CatalogItemID = {1} AND
UserItem.UserItemTicket = {0};
