@echo off

set inputFile=profilereactor.js
set outputFile= profilereactor.prod.js
node minifier.js %inputFile% %outputFile%
terser %outputFile% -o %outputFile%
node minifier.js %outputFile% %outputFile%
