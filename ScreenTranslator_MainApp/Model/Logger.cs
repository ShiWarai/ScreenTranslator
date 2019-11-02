using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ScreenTranslator_MainApp.Model
{
    public class Logger
    {
        private string LogFileWay;
        private bool isEnabled;

        public void SetEnable(bool bl)
        {
            if (!bl)
                this.WriteToLog("!!! End logging !!!");
            this.isEnabled = bl;
            if (bl)
                this.WriteToLog("!!! Start logging !!!");
        }

        public Logger(string way)
        {
            LogFileWay = way;
        }

        static public string CreateLogFile()
        {
            var fd = new SaveFileDialog()
            {
                FileName = "log_of_translations",
                DefaultExt = "log",
                CreatePrompt = false,
                OverwritePrompt = false,
                AddExtension = true,
                Title = LanguageData.GetStringFromResource("LogWayTitle"),
                CheckFileExists = false
            };

            DialogResult result = fd.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (File.Exists(fd.FileName))
                {
                    MessageBoxResult answer = System.Windows.MessageBox.Show
                        (LanguageData.GetStringFromResource("DeleteFileMess"),
                        LanguageData.GetStringFromResource("Delete"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (answer == MessageBoxResult.Yes)
                    {
                        File.Delete(fd.FileName);
                        using (Stream stream = fd.OpenFile())
                        {
                            StreamWriter writer = new StreamWriter(stream);
                            writer.WriteLine(System.DateTime.Now + "\nLog has been created");
                            writer.Close();
                        }
                    }
                }
            }
            else
            {
                return "";
            }
            return fd.FileName;
        }

        public void WriteToLog(string text)
        {
            if (LogFileWay != "" && this.isEnabled)
            {
                using (StreamWriter writer = new StreamWriter(LogFileWay,true))
                {
                    writer.WriteAsync(System.DateTime.Now + "\n" + text+"\n");
                }
            }
        }
    }
}
