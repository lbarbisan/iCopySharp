using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace iCopy
{
    static class Utilities
    {
        public static MsgBoxResult MsgBoxWrap(string Message, MsgBoxStyle Style = MsgBoxStyle.DefaultButton1, string Title = "iCopy")
        {
            if (Properties.Settings.Default.Silent == Conversions.ToBoolean("False"))
            {
                return Interaction.MsgBox(Message, Style, Title);
            }
            else
            {
                Console.WriteLine(Message);
                return MsgBoxResult.Cancel;
            }
        }

        public static string GetWritablePath()
        {
            try
            {
                var fi = new FileInfo(Application.ExecutablePath);
                string path = fi.DirectoryName;

                var fs = File.Create(path + @"\writable");
                fs.Close();
                File.Delete(path + @"\writable");
                return path;
            }
            catch (Exception ex)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                path = Path.Combine(path, Application.ProductName);
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        Directory.Delete(path);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
                return path;

            }

            return Application.LocalUserAppDataPath;
        }
    }
}