/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
INSERT INTO CityRequest(
CityRequestTicket,
CityRequestName)
SELECT
{1} AS RequestTicket,
{0} AS RequestName
FROM CityRequest
WHERE NOT EXISTS
(
	SELECT
	CityRequest.CityRequestName
	FROM
	CityRequest
	WHERE
	CityRequest.CityRequestName = {0}
);
