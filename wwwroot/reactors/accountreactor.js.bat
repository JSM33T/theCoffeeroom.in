@echo off

set inputFile=accountreactor.js
set outputFile=accountreactor.prod.js
node minifier.js %inputFile% %outputFile%
terser %outputFile% -o %outputFile%
node minifier.js %outputFile% %outputFile%
