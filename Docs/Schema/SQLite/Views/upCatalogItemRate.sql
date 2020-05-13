/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
UPDATE CatalogItem
SET
StarCount = {1},
RatingCount = {2}
WHERE CatalogItemID = {0};
