/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE FROM CityItem;
INSERT INTO CityItem(CityItemID, CityItemTicket, CityName)
VALUES
(1, '8BDE00F5-3A32-4A98-8A3B-3CA0847D0EBF', 'Laramie, WY'),
(2, 'A4DD5B8E-17DC-482B-A356-002E804FCF47', 'Wheatland, WY');
SELECT * FROM CityItem;
