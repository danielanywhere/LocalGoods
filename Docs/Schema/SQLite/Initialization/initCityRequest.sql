/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE FROM CityRequest;
INSERT INTO CityRequest(CityRequestID, CityRequestTicket, CityRequestName)
VALUES
(2, '49B26A9B-F1F2-4210-9E11-C9A0238549EE', 'Fort Collins, CO'),
(3, '751A76F4-1CA4-4911-8F59-F6CE741166A8', 'Tampa, FL');
SELECT * FROM CityRequest;
