# shortcutmaker

Create shortuts easily

## options

--help | -H
	
	get the documentation

--commandtofile | -CTF
	
	print the command sended in the file

--originpath | -OP
	
	origin path (required)

--shortcutdirectory | -SCDI
	
	shortcut directory (if not, origin's path directory) (can be equal to 'desktop', 'startmenu', 'startup')

--shortcutworkingdirectory | -SCWDI
	
	working shortcut directory (if not, =shortcutdirectory)

--shortcutname | -SCN
	
	short cutname (if not, origin's name + '.lnk')

--shortcutdescription | -SCDE
	
	shortcut descriptionac

--shortcuticon | -SCI
	
	shortcut icon (if not, origin's icon)

## samples

shortcutmaker.exe -OP "C:\Program Files (x86)\Notepad++\notepad++.exe" -SCDI "desktop" -SCN "bestnotepadever.lnk" -SCDE "This is Notepad++"

shortcutmaker.exe -OP "C:\Program Files (x86)\Notepad++\notepad++.exe" -SCDI "startmenu" -SCN "bestnotepadever.lnk" -SCDE "This is Notepad++"

shortcutmaker.exe -OP "C:\Program Files (x86)\Notepad++\notepad++.exe" -SCDI "startup" -SCN "bestnotepadever.lnk" -SCDE "This is Notepad++"

shortcutmaker.exe -OP "C:\Program Files (x86)\Notepad++\notepad++.exe" -SCDI "./" -SCN "bestnotepadever.lnk" -SCDE "This is Notepad++"