/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DROP TABLE IF EXISTS [CityRequest];
CREATE TABLE [CityRequest]
(
	[CityRequestID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
 [CityRequestTicket] TEXT(36) NOT NULL,
	[CityRequestName] TEXT(255) NULL
);
CREATE UNIQUE INDEX idxCityRequest ON CityRequest(CityRequestID);
