/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE FROM CatalogItem;
INSERT INTO CatalogItem(CatalogItemID, CatalogItemTicket, DepartmentItemID, UserItemID, CityItemID, DateCreated, DateUpdated, DateViewed, Visible, ViewCount, ProductTitle, ProductDescription, ContactInfo, RatingCount, StarCount, ItemPrice, ItemUnit)
VALUES
(-1, '7B338B36-DEE7-4CE9-9BE0-7DC04C99BD33', 0, 0, 0, '1980-01-01 00:00:00.000', '1980-01-01 00:00:00.000', '1980-01-01 00:00:00.000', 0, 0, 'Blank', 'Blank Item.', 'ContactInfo', 0, 0.0, 0.0, 'ea'),
(1, '34EBFDD5-1270-44A9-AE77-C38106EB1E92', 2, 1, 2, '2020-04-27 00:00:00.000', '2020-05-08 00:00:00.000', '1980-01-01 00:00:00.000', 1, 0, 'Test Item', 'This item will be deleted as soon as testing is finished.', 'Big Hollow Food Co-Op, Laramie', 2, 4.5, 22.5, 'ea'),
(2, '442E4496-BF9C-4F2E-8F37-D8BB8A1305DF', 2, 1, 2, '2020-04-27 00:00:00.000', '2020-05-08 00:00:00.000', '1980-01-01 00:00:00.000', 1, 0, 'Wheatland Test', 'This is a test item in the Wheatland market.', 'Big Hollow Food Co-Op, Laramie', 0, 5, 21.95, 'ea'),
(4, '2E3AB7F9-152D-4D2A-989D-610CDA05648E', 1, 1, 1, '2020-05-07 00:00:00.000', '2020-05-08 00:00:00.000', '1980-01-01 00:00:00.000', 1, 0, 'Party platter #1 (Test only)', 'Updated product description.', 'Big Hollow Food Co-Op, Laramie', 0, 5, 57.9, 'ea'),
(5, 'ACB3104C-2655-4773-AFCF-4B5F4685E86E', 1, 1, 1, '2020-05-07 00:00:00.000', '2020-05-08 00:00:00.000', '1980-01-01 00:00:00.000', 1, 0, 'Vine-ripe tomatoes (Test only)', 'Get some delicious vine-ripe tomatoes directly from the Laramie River Valley!', 'Big Hollow Food Co-Op, Laramie', 0, 5, 1.95, 'lb');
SELECT * FROM CatalogItem;

