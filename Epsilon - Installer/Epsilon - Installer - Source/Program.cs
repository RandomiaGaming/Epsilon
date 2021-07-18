using System;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using System.Windows.Forms;
namespace DontMeltInstaller
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            if (Directory.Exists(@"C:\Program Files\DontMelt"))
            {
                if (MessageBox.Show($"Dont Melt is already installed.  Would you like to uninstall Dont Melt?", $"Uninstall Dont Melt?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        Uninstall();
                        MessageBox.Show($"Dont Melt was succesfully uninstalled.", "Uninstallation Successful.", MessageBoxButtons.OK);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Uninstallation of Dont Melt was aborted due to an error! {e.Message}", "Uninstall Aborted!", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show($"Would you like to install Dont Melt?", $"Install Dont Melt?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        DialogResult desktopShortcutResult = MessageBox.Show($"Would you like to create a desktop shortcut for Dont Melt?", "Create Desktop Shortcut?", MessageBoxButtons.YesNo);
                        DialogResult startMenuShortcutresult = MessageBox.Show($"Would you like to create a start menu shortcut for Dont Melt? A start menu shortcut allows Dont Melt to be opended from Cortana.", "Create Start Menu Shortcut?", MessageBoxButtons.YesNo);
                        Install(desktopShortcutResult == DialogResult.Yes, startMenuShortcutresult == DialogResult.Yes);
                        MessageBox.Show($"Dont Melt was succesfully installed.", "Installation Successful.", MessageBoxButtons.OK);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Installation of Dont Melt was aborted due to an error! {e.Message}", "Install Aborted!", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        public static void Install(bool createDesktopShortcut, bool createStartMenuShortcut)
        {
            if (Directory.Exists(@"C:\Program Files\DontMelt"))
            {
                Directory.Delete(@"C:\Program Files\DontMelt", true);
            }

            Directory.CreateDirectory(@"C:\Program Files\DontMelt");

            Assembly assembly = Assembly.GetCallingAssembly();

            Stream payloadStream = assembly.GetManifestResourceStream("DontMeltInstaller.Payload.zip");
            byte[] payloadBytes = new byte[payloadStream.Length];
            payloadStream.Read(payloadBytes, 0, (int)payloadStream.Length);
            payloadStream.Dispose();

            Directory.CreateDirectory(@"%Temp%\DontMelt");
            File.WriteAllBytes(@"%Temp%\DontMelt\Payload.zip", payloadBytes);
            ZipFile.ExtractToDirectory(@"%Temp%\DontMelt\Payload.zip", @"C:\Program Files\DontMelt");
            Directory.Delete(@"%Temp%\DontMelt", true);

            if (createStartMenuShortcut)
            {
                IWshRuntimeLibrary.IWshShortcut startMenuShortcut = (IWshRuntimeLibrary.IWshShortcut)new IWshRuntimeLibrary.WshShell().CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\Programs\DontMelt.lnk");
                startMenuShortcut.Arguments = "";
                startMenuShortcut.Description = "";
                startMenuShortcut.Hotkey = "";
                startMenuShortcut.TargetPath = @"C:\Program Files\DontMelt\DontMelt.exe";
                startMenuShortcut.WindowStyle = 0;
                startMenuShortcut.Save();
            }

            if (createDesktopShortcut)
            {
                IWshRuntimeLibrary.IWshShortcut desktopShortcut = (IWshRuntimeLibrary.IWshShortcut)new IWshRuntimeLibrary.WshShell().CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\DontMelt.lnk");
                desktopShortcut.Arguments = "";
                desktopShortcut.Description = "";
                desktopShortcut.Hotkey = "";
                desktopShortcut.TargetPath = @"C:\Program Files\DontMelt\DontMelt.exe";
                desktopShortcut.WindowStyle = 0;
                desktopShortcut.Save();
            }
        }
        public static void Uninstall()
        {
            if (Directory.Exists(@"C:\Program Files\DontMelt"))
            {
                Directory.Delete(@"C:\Program Files\DontMelt", true);
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\Programs\DontMelt.lnk"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\Programs\DontMelt.lnk");
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\DontMelt.lnk"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\DontMelt.lnk");
            }
        }
    }
}
