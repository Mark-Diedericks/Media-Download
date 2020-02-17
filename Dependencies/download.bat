cd "C:\Users\Mark Diedericks\Music\Songs"
:retry
"C:\Users\Mark Diedericks\Documents\Visual Studio 2017\Projects\Youtube Download\Dependencies\youtube-dl.exe" --download-archive downloaded.txt --socket-timeout 1 --retries infinite --extract-audio --audio-format mp3 -o "%%(title)s.%%(ext)s" https://www.youtube.com/playlist?list=PLHPWT76TUZsbac3Jwg6bSvYkvVNYTfgL7 -c
IF NOT "%ERRORLEVEL%"=="0" GOTO :retry
@echo COMPLETE, ERRORLEVEL = %ERRORLEVEL%
pause