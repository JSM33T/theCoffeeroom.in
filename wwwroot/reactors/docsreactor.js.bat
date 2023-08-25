@echo off

set inputFile=docsreactor.js
set outputFile=docsreactor.prod.js

node minifier.js %inputFile% %outputFile%
terser %outputFile% -o %outputFile%
node minifier.js %outputFile% %outputFile%
