/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
CatalogImage.ImageURL,
CatalogImage.ImageIndex,
CatalogImage.CatalogImageID,
CatalogImage.CatalogImageTicket
FROM CatalogImage
WHERE CatalogImage.CatalogItemID = {0}
ORDER BY
CatalogImage.ImageIndex,
CatalogImage.CatalogImageID;
