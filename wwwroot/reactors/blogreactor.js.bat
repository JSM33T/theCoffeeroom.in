@echo off

set inputFile=blogreactor.js
set outputFile=blogreactor.prod.js

node minifier.js %inputFile% %outputFile%
npx terser %outputFile% -o %outputFile%
node minifier.js %outputFile% %outputFile%
