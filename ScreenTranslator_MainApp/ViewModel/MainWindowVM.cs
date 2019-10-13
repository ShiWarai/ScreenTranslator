using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ScreenTranslator_MainApp.Model;

namespace ScreenTranslator_MainApp.ViewModel
{
    public class MainWindowVM
    {
        public MainWindowVM()
        {

        }

        public string ClipboardTranslate()
        {
            string text = Clipboard.GetText();
            return (new Translator("trnsl.1.1.20191013T000841Z.a1691e726d3b0db8.19a9f0b65a94f0e6f7aea4f246947a0b3fe1ee84")).Translate(text, "ru");
        }
    }
}
