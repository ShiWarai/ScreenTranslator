using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenTranslator_MainApp.Properties;
using ScreenTranslator_MainApp.ViewModel;

namespace ScreenTranslator_MainApp.Model
{
    abstract class TextInitor
    {
        protected string text;
        protected string translated_text;

        public string TranslatedText { 
            get 
            {
                if (translated_text == null)
                {
                    Translate();
                    AppManager.TranslationLogger.WriteToLog("From: " + text + "\nTo: " + translated_text);
                }
                return translated_text;
            } 
        }

        protected bool Translate()
        {
            try
            {
                translated_text = Translator.Translate(text, Properties.Settings.Default.Language);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
