@echo off

set inputFile=memberreactor.js
set outputFile= memberreactor.prod.js
node minifier.js %inputFile% %outputFile%
npx terser %outputFile% -o %outputFile%
node minifier.js %outputFile% %outputFile%
