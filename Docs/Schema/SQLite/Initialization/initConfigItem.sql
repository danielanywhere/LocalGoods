/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE FROM ConfigItem;
INSERT INTO ConfigItem(ConfigItemID, ConfigItemTicket, ConfigName, ConfigValue)
VALUES
(1, '384AACF6-AFCB-4869-A44E-BA21D10D81D3', 'UseAsTemp', 'true');
SELECT * FROM ConfigItem;
