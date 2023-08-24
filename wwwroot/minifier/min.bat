@echo off
setlocal

call :processFile "../reactors/blogreactor.js" "../reactors/blogreactor.prod.js"

call :processFile "../reactors/accountreactor.js" "../reactors/accountreactor.prod.js"

call :processFile "../reactors/profilereactor.js" "../reactors/profilereactor.prod.js"

call :processFile "../reactors/docsreactor.js" "../reactors/docsreactor.prod.js"

call :processFile "../reactors/musicreactor.js" "../reactors/musicreactor.prod.js"

goto :eof

:processFile
echo Processing %~1 to %~2

node minifier.js "%~1" "%~2"
terser "%~2" -o "%~2"
node minifier.js "%~2" "%~2"

goto :eof
