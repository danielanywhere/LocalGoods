/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE FROM CatalogBullet;
INSERT INTO CatalogBullet(CatalogBulletID, CatalogBulletTicket, CatalogItemID, BulletIndex, BulletText)
VALUES
(-1, '5A579A2F-740B-4FD9-9394-4CDC98427561', -1, 0, 'Pattern'),
(2, 'F3A2580B-8BD5-47FB-A810-F53EAD0D7487', 1, 0, 'Tasty treat.'),
(3, '3A18BC5B-24C8-4900-8F91-555F53780D1E', 1, 1, 'Interesting flavor.'),
(4, 'D897EBB1-E046-468F-85E9-A0D3270B08DD', 1, 2, 'Long lasting.'),
(17, 'EF71A1D3-20BA-4E5D-B6F4-40E47FA15BFA', 5, 0, 'Grown on the vine.'),
(18, '9FE9C849-D230-4EAC-A777-3DAC4F7981B2', 5, 1, 'Vine segment still attached.'),
(19, 'B87D0C6C-6E1B-4789-8EF1-933A12CD5D38', 4, 0, '12" x 9"');
SELECT * FROM CatalogBullet;
