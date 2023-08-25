@echo off

set inputFile=musicreactor.js
set outputFile=musicreactor.prod.js

node minifier.js %inputFile% %outputFile%
terser %outputFile% -o %outputFile%
node minifier.js %outputFile% %outputFile%
