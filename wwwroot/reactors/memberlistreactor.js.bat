@echo off

set inputFile=memberlistreactor.js
set outputFile= memberlistreactor.prod.js
node minifier.js %inputFile% %outputFile%
npx terser %outputFile% -o %outputFile%
node minifier.js %outputFile% %outputFile%
