@echo off

set inputFile=wallreactor.js
set outputFile= wallreactor.prod.js
node minifier.js %inputFile% %outputFile%
npx terser %outputFile% -o %outputFile%
node minifier.js %outputFile% %outputFile%
