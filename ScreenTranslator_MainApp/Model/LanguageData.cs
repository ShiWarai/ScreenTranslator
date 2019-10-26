using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ScreenTranslator_MainApp.Model
{
    public class LanguageData
    {
        private ResourceManager resource;
        public LanguageData()
        {
            resource = LanguageResource;
        }
        public string GetString(string name)
        {
            return resource.GetString(name);
        }
        
        static private ResourceManager LanguageResource
        {
            get
            {
                return new ResourceManager("ScreenTranslator_MainApp.Lang.lang", Assembly.GetExecutingAssembly());
            }
        }

        /// <summary>
        /// Static method of getting strings
        /// </summary>
        /// <param name="name">Name of the string</param>
        /// <returns>String from language resource</returns>
        public static string GetStringFromResource(string name)
        {
            return LanguageResource.GetString(name);
        }
    }
}
