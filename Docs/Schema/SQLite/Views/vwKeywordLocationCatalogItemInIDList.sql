/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
KeywordLocation.*
FROM KeywordLocation
WHERE
KeywordLocation.TableIndexID = 3 AND
KeywordLocation.RecordID = {0} AND
KeywordLocation.KeywordID IN({1});
