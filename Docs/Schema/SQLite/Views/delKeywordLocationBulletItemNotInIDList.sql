/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
DELETE
FROM KeywordLocation
WHERE
KeywordLocation.TableIndexID = 1 AND
KeywordLocation.RecordID = {0} AND
KeywordLocation.KeywordID NOT IN({1});
