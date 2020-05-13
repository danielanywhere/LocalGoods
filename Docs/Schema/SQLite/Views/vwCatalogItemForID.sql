/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
CatalogItem.*,
DepartmentItem.DepartmentName,
UserItem.MemberUsername,
CityItem.CityName
FROM ((CatalogItem
LEFT JOIN DepartmentItem ON
CatalogItem.DepartmentItemID =
DepartmentItem.DepartmentItemID)
LEFT JOIN UserItem ON
CatalogItem.UserItemID = UserItem.UserItemID)
LEFT JOIN CityItem ON
CatalogItem.CityItemID = CityItem.CityItemID
WHERE CatalogItem.CatalogItemID = {0};
