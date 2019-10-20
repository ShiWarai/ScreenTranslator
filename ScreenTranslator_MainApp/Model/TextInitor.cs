using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenTranslator_MainApp.Properties;

namespace ScreenTranslator_MainApp.Model
{
    abstract class TextInitor
    {
        protected string text;
        protected string found_text;

        public string FoundText { 
            get 
            {
                if (found_text == null)
                    Translate();
                return found_text;
            } 
        }

        protected bool Translate()
        {
            try
            {
                found_text = Translator.Translate(text, Properties.Settings.Default.Language);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
