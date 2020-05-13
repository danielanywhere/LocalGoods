/*
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
*/
SELECT
KeywordLocation.RecordID,
KeywordLocation.KeywordID,
KeywordLocation.TableIndexID
FROM KeywordLocation
WHERE
KeywordLocation.KeywordID IN({0});
