using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScreenTranslator_MainApp.Model
{
    class SettingsManager
    {
        static private List<string> allowed_languages;

        public static List<string> AllowedLanguages
        {
            get
            {
                if (allowed_languages == null)
                {
                    allowed_languages = GetAllowedLanguages();
                }
                return allowed_languages;
            }
        }

        public enum MethodsTranslate
        {
            Clipboard,
            Screen
        }

        static public List<string> TranslateMethods
        {
            get
            {
                return new List<string> {
                    MethodsTranslate.Clipboard.ToString(),
                    MethodsTranslate.Screen.ToString()
                    };
            }
        }


        static List<string> GetAllowedLanguages()
        {
            var list_languages = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ScreenTranslator_MainApp.Lang.allowed_languages.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    list_languages.Add(reader.ReadLine());
                }
            }
            return list_languages;
        }

        static public System.Windows.Forms.Keys KeyFromString(string str)
        {
            var kc = new System.Windows.Forms.KeysConverter();

            return (System.Windows.Forms.Keys) kc.ConvertFromString(str);
        }
    }
}
