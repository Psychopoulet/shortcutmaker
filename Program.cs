using System;
using IWshRuntimeLibrary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace shortcutmaker
{

    public class Program
    {

        static int Main(string[] args)
        {

            String sOriginPath, sShortCutDirectory, sShortCutWorkingDirectory, sShortCutName, sShortCutDescription, sShortCutIcon;
            bool bHelp = false;

            int nResult = 0;

                try
                {

                    sOriginPath = sShortCutDirectory = sShortCutWorkingDirectory = sShortCutName = sShortCutDescription = sShortCutIcon = "";

                    if (0 == args.Length)
                    {
                        Console.Error.WriteLine("Missing arguments");
                        nResult = 11;
                    }
                    else
                    {

                        for (int i = 0, l = args.Length; i < l; ++i)
                        {

                            switch (args[i])
                            {
                                case "-OP": case "--originpath":

                                    if (i + 1 < l)
                                    {
                                        sOriginPath = args[i + 1];
                                    }

                                break;

                                case "-SCDI": case "--shortcutdirectory":

                                    if (i + 1 < l)
                                    {
                                        sShortCutDirectory = args[i + 1];
                                    }

                                break;

                                case "-SCWDI": case "--shortcutworkingdirectory":

                                    if (i + 1 < l)
                                    {
                                        sShortCutWorkingDirectory = args[i + 1];
                                    }

                                break;

                                case "-SCN": case "--shortcutname":

                                    if (i + 1 < l)
                                    {
                                        sShortCutName = args[i + 1];
                                    }

                                break;

                                case "-SCDE": case "--shortcutdescription":

                                    if (i + 1 < l)
                                    {
                                        sShortCutDescription = args[i + 1];
                                    }

                                break;

                                case "-SCI": case "--shortcuticon":

                                    if (i + 1 < l)
                                    {
                                        sShortCutIcon = args[i + 1];
                                    }

                                break;


                                case "-H": case "--help":
                                    bHelp = true;
                                break;

                            }

                        }

                        if (bHelp)
                        {
                            Console.WriteLine("-H | --help : help");
                            Console.WriteLine("-OP | --originpath : origin path (required)");
                            Console.WriteLine("-SCDI | --shortcutdirectory : shortcut directory (if not, origin's path directory) (can be equal to 'desktop', 'startmenu', 'startup')");
                            Console.WriteLine("-SCWDI | --shortcutworkingdirectory : shortcut directory (if not, =shortcutdirectory)");
                            Console.WriteLine("-SCN | --shortcutname : short cutname (if not, origin's name + '.lnk')");
                            Console.WriteLine("-SCDE | --shortcutdescription : shortcut description");
                            Console.WriteLine("-SCI | --shortcuticon : shortcut icon (if not, origin's icon)");
                            Console.WriteLine("ex :");
                            Console.WriteLine("shortcutmaker.exe -OP \"C:\\Program Files (x86)\\Notepad++\\notepad++.exe\" -SCDI \"desktop\" -SCN \"bestnotepadever.lnk\" -SCDE \"This is Notepad++\"");
                        }
                        else if ("" == sOriginPath)
                        {
                            Console.Error.WriteLine("Missing origin path");
                            nResult = 12;
                        }
                        else
                        {

                            switch (sShortCutDirectory)
                            {
                                case "desktop":
                                    sShortCutDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\";
                                    break;
                                case "startmenu":
                                    sShortCutDirectory = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\";
                                break;
                                case "startup":
                                    sShortCutDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\";
                                break;
                            }

                            _createShortCutTo(sOriginPath, sShortCutDirectory, sShortCutWorkingDirectory, sShortCutName, sShortCutDescription, sShortCutIcon);

                        }

                    }

                }
                catch(Exception e)
                {
                    nResult = 1;
                    Console.Error.WriteLine(e.Message);
                }

            return nResult;

        }

        private static int _createShortCutTo(String p_sOriginPath, String p_sShortCutDirectory, String p_sShortCutWorkingDirectory, String p_sShortCutName, String p_sShortCutDescription, String p_sShortCutIcon)
        {

            IWshShortcut shortcut;
            int nResult = 0;

                try
                {

                    if ("" == p_sShortCutDirectory.Trim())
                    {
                        p_sShortCutDirectory = Path.GetDirectoryName(p_sOriginPath) + "\\";
                    }
                    if ("" == p_sShortCutWorkingDirectory.Trim())
                    {
                        p_sShortCutWorkingDirectory = p_sShortCutDirectory;
                    }
                    if ("" == p_sShortCutName.Trim())
                    {
                        p_sShortCutName = Path.GetFileNameWithoutExtension(p_sOriginPath) + ".lnk";
                    }

                    if (System.IO.File.Exists(p_sShortCutDirectory + p_sShortCutName))
                    {
                        System.IO.File.Delete(p_sShortCutDirectory + p_sShortCutName);
                    }

                    shortcut = (IWshShortcut)new WshShell().CreateShortcut(p_sShortCutDirectory + p_sShortCutName);

                        shortcut.TargetPath = p_sOriginPath;
                        shortcut.WorkingDirectory = p_sShortCutDirectory;

                        if ("" != p_sShortCutDescription.Trim())
                        {
                            shortcut.Description = p_sShortCutDescription;
                        }
                        if ("" != p_sShortCutIcon.Trim())
                        {
                            shortcut.IconLocation = p_sShortCutIcon;
                        }

                    shortcut.Save();

                }
                catch(Exception e)
                {
                    nResult = 2;
                    Console.Error.WriteLine(e.Message);
                }

            return nResult;

        }

    }

}
