/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
CatalogItem.CatalogItemID,
CatalogItem.CatalogItemTicket,
CatalogItem.DepartmentItemID,
CatalogItem.UserItemID,
CatalogItem.DateCreated,
CatalogItem.DateUpdated,
CatalogItem.DateViewed,
CatalogItem.Visible,
CatalogItem.ViewCount,
CatalogItem.ProductTitle,
CatalogItem.ProductDescription,
CatalogItem.StarCount,
CatalogItem.ItemPrice,
CatalogItem.ItemUnit,
DepartmentItem.DepartmentName,
CityItem.CityName,
UserItem.CityItemID,
(
	SELECT
	CatalogImage.ImageURL
	FROM CatalogImage
	WHERE CatalogImage.CatalogItemID =
	CatalogItem.CatalogItemID
	ORDER BY CatalogImage.ImageIndex
	LIMIT 1
) AS ImageURL
FROM CatalogItem
INNER JOIN UserItem ON
CatalogItem.UserItemID = UserItem.UserItemID
INNER JOIN DepartmentItem ON
CatalogItem.DepartmentItemID =
DepartmentItem.DepartmentItemID
INNER JOIN CityItem ON
CatalogItem.CityItemID =
CityItem.CityItemID
WHERE (CatalogItem.CityItemID = {0} OR
(UserItem.CityItemID = {0} AND
CatalogItem.CityItemID = 0)) AND
CatalogItem.Visible = 1
ORDER BY
CatalogItem.DepartmentItemID,
CatalogItem.DateCreated DESC;
