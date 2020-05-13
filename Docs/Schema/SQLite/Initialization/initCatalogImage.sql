/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE FROM CatalogImage;
INSERT INTO [CatalogImage](CatalogImageID, CatalogImageTicket, CatalogItemID, ImageIndex, ImageURL)
VALUES
(-1, 'DBD7838F-B09F-4B11-97C4-5EDB9254C42D', -1, 0, 'NoImage.png'),
(2, 'F721330F-D2DE-420A-95C5-79C2C95BF9CB', 1, 1, 'F721330F-D2DE-420A-95C5-79C2C95BF9CB.png'),
(3, '4B9BE7C6-BDF2-409F-8300-1D3CD70B3DF1', 1, 2, '4B9BE7C6-BDF2-409F-8300-1D3CD70B3DF1.png'),
(10, 'B262105F-2076-47B3-9473-2D26DC5E2B39', 1, 0, 'b262105f-2076-47b3-9473-2d26dc5e2b39.png'),
(11, '9191CDDF-8C4F-4239-848D-485928D9CC60', 4, 0, '9191cddf-8c4f-4239-848d-485928d9cc60.png'),
(12, '3A7936D5-D338-4105-A0D1-17A9C63E8DBF', 5, 0, '3a7936d5-d338-4105-a0d1-17a9c63e8dbf.png'),
(14, '79165E39-08CF-403D-8C84-5732347704E7', 2, 0, '79165e39-08cf-403d-8c84-5732347704e7.png');
SELECT * FROM CatalogImage;
