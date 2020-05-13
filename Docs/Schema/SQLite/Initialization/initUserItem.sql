/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE FROM UserItem;
INSERT INTO UserItem(UserItemID, UserItemTicket, CityItemID, MemberUsername, MemberEmail, MemberPassword)
VALUES
(1, '6C6ECEFA-C486-4512-9271-C8BF00F86B14', 1, 'danielanywhere', 'danielanywhere@hotmail.com', 'password'),
(2, '29D79A83-5A38-47A8-BA4E-B70D1FA065F2', 2, 'danielwheatland', 'danielanywhere@outlook.com', 'wheatland'),
(3, 'D6F8321F-4AF7-40A1-B1B0-A41AAE6F23CD', 1, 'Mervel', 'danielanywhere@hotmail.vp', 'mervel');
SELECT * FROM UserItem;
