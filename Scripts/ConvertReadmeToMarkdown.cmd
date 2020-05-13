:: ConvertReadmeToMarkdown.cmd
:: Convert the document Readme.docx to Readme.md
:: Dependencies:
::  pandoc (https://github.com/jgm/pandoc)
::  FindAndReplace (https://github.com/danielanywhere/FindAndReplace)
::
:: Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
:: Released for public access under the MIT License.
:: http://www.opensource.org/licenses/mit-license.php
@ECHO OFF
SET SOURCE=C:\Users\Daniel\Documents\GitHub\LocalGoods\Docs\Wiki\ReadMe.docx
SET TARGET=C:\Users\Daniel\Documents\GitHub\LocalGoods\README.md
SET POSTCONFIG=C:\Users\Daniel\Documents\GitHub\LocalGoods\Scripts\ReadmePostConversion.json
SET FAR=C:\Users\Daniel\Documents\Visual Studio 2015\Projects\FindAndReplace\FindAndReplace\bin\Debug\FindAndReplace.exe

PANDOC -f docx -t markdown_strict+pipe_tables -s "%SOURCE%" -o "%TARGET%"
"%FAR%" /files:"%TARGET%" /patternfile:"%POSTCONFIG%"
