using System;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using System.Windows.Forms;
namespace Epsilon.Installer
{
    public static class Program
    {
        public const string installPath = @"C:\Program Files\Epsilon";
        public const string startLinkSubPath = @"Programs\Epsilon.lnk";
        public const string desktopLinkSubPath = @"Epsilon.lnk";
        public const string programNameUpperCase = @"Epsilon";
        public const string programNameLowerCase = @"epsilon";
        public const string exeSubPath = "Epsilon.exe";
        public const string tempSubPath = "EpsilonInstallerTempFolder";
        [STAThread]
        public static void Main()
        {
            if (Directory.Exists(installPath))
            {
                if (MessageBox.Show($"{programNameUpperCase} has already been installed. Would you like to uninstall {programNameLowerCase}?", $"Uninstall {programNameUpperCase}?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        Uninstall();
                        MessageBox.Show($"{programNameUpperCase} was successfully uninstalled!", $"Uninstall Successful!", MessageBoxButtons.OK);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"{programNameUpperCase} could not be uninstalled due to exception: {e.Message}!", "Uninstall Aborted!", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show($"{programNameUpperCase} has not been installed. Would you like to install {programNameLowerCase}?", $"Install {programNameUpperCase}?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        DialogResult desktopShortcutResult = MessageBox.Show($"Would you like to create a desktop shortcut for {programNameLowerCase}?", "Create Desktop Shortcut?", MessageBoxButtons.YesNo);
                        DialogResult startMenuShortcutresult = MessageBox.Show($"Would you like to create a start menu shortcut for {programNameLowerCase}?", "Create Start Menu Shortcut?", MessageBoxButtons.YesNo);
                        Install(desktopShortcutResult == DialogResult.Yes, startMenuShortcutresult == DialogResult.Yes);
                        MessageBox.Show($"{programNameUpperCase} was succesfully installed!", "Install Successful!", MessageBoxButtons.OK);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"{programNameUpperCase} could not be installed due to exception: {e.Message}!", "Install Aborted!", MessageBoxButtons.OK);
                        MessageBox.Show($"Attempting to undo changes!", "Undo Changes!", MessageBoxButtons.OK);
                        try
                        {
                            Abort();
                            MessageBox.Show($"Changes were successfully undone!", "Undo Successful!", MessageBoxButtons.OK);
                        }
                        catch (Exception e2)
                        {
                            MessageBox.Show($"Changes could not be undone due to exception: {e2.Message}!", "Undo Aborted!", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }
        /// <summary>
        /// Installs the payload program and creates shortcuts if requested.
        /// </summary>
        public static void Install(bool createDesktopShortcut, bool createStartMenuShortcut)
        {
            //Get the payload as a stream.
            Assembly assembly = Assembly.GetCallingAssembly();
            Stream payloadStream = assembly.GetManifestResourceStream("Installer.Payload.zip");
            //Get the temp folder path.
            string tempFolderPath = Path.GetTempPath() + tempSubPath;
            //Create the temp folder unless it already exists.
            if (Directory.Exists(tempFolderPath))
            {
                throw new Exception($"Temp directory already exists at \"{tempFolderPath}\". This usually means another installer is running.");
            }
            else
            {
                Directory.CreateDirectory(tempFolderPath);
            }
            //Get the payload path
            string payloadFilePath = tempFolderPath + @"\Payload.zip";
            //Create the payload file.
            FileStream payloadFileStream = File.Create(payloadFilePath);
            //Write the payload to the payload file.
            payloadStream.Seek(0, SeekOrigin.Begin);
            payloadStream.CopyTo(payloadFileStream);
            //Close the payload file.
            payloadFileStream.Close();
            //Dispose of the unneeded streams.
            payloadStream.Dispose();
            payloadFileStream.Dispose();
            //Get the payload folder path.
            string payloadFolderPath = tempFolderPath + @"\Payload";
            //Create the payload folder.
            Directory.CreateDirectory(payloadFolderPath);
            //Extract the payload to the payload folder.
            ZipFile.ExtractToDirectory(payloadFilePath, payloadFolderPath);
            //Delete the payload file.
            File.Delete(payloadFilePath);
            //Check if the install folder already exists.
            if (Directory.Exists(installPath))
            {
                throw new Exception($"Install directory already exists at \"{installPath}\"");
            }
            //Move the payload folder to the install folder.
            Directory.Move(payloadFolderPath, installPath);
            //Delete the temp folder.
            Directory.Delete(tempFolderPath, true);
            //If instructed to then create a start menu shortcut.
            if (createStartMenuShortcut)
            {
                //Create Start Menu Shortcut.
                IWshRuntimeLibrary.IWshShortcut startMenuShortcut = (IWshRuntimeLibrary.IWshShortcut)new IWshRuntimeLibrary.WshShell().CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\" + startLinkSubPath);
                startMenuShortcut.Arguments = "";
                startMenuShortcut.Description = "";
                startMenuShortcut.Hotkey = "";
                startMenuShortcut.TargetPath = installPath + @"\" + exeSubPath;
                startMenuShortcut.WindowStyle = 0;
                startMenuShortcut.Save();
            }
            //If instructed to then create a desktop shortcut.
            if (createDesktopShortcut)
            {
                //Create Desktop Shortcut.
                IWshRuntimeLibrary.IWshShortcut desktopShortcut = (IWshRuntimeLibrary.IWshShortcut)new IWshRuntimeLibrary.WshShell().CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + desktopLinkSubPath);
                desktopShortcut.Arguments = "";
                desktopShortcut.Description = "";
                desktopShortcut.Hotkey = "";
                desktopShortcut.TargetPath = installPath + @"\" + exeSubPath;
                desktopShortcut.WindowStyle = 0;
                desktopShortcut.Save();
            }
        }
        /// <summary>
        /// Uninstalls the payload program
        /// </summary>
        public static void Uninstall()
        {
            if (Directory.Exists(installPath))
            {
                Directory.Delete(installPath, true);
            }
            else
            {
                throw new Exception($"Install directory does not exist at \"{installPath}\"");
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\" + startLinkSubPath))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\" + startLinkSubPath);
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + desktopLinkSubPath))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + desktopLinkSubPath);
            }
        }
        /// <summary>
        /// Cleans up after an error by deleting all installation fragments.
        /// </summary>
        public static void Abort()
        {
            if (Directory.Exists(installPath))
            {
                Directory.Delete(installPath, true);
            }

            string tempFolderPath = Path.GetTempPath() + tempSubPath;
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\" + startLinkSubPath))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\" + startLinkSubPath);
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + desktopLinkSubPath))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + desktopLinkSubPath);
            }
        }
    }
}
