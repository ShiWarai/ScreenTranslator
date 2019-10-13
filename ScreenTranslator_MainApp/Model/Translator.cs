using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScreenTranslator_MainApp.ViewModel;

namespace ScreenTranslator_MainApp.Model
{
    struct Translation
    {
        public string code { get; set; }
        public string lang { get; set; }
        public string[] text { get; set; }
    }

    public class Translator
    {
        private string Key;
        public Translator(string key)
        {
            this.Key = key;
        }
        public string Translate(string text="",string translation_code="en-ru")
        {
            if (text.Length > 0)
            {
                WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
                    + "key=" + this.Key
                    + "&text=" + text
                    + "&lang=" + translation_code);

                WebResponse response = request.GetResponse();

                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    string line;

                    if ((line = stream.ReadLine()) != null)
                    {
                        Translation translation = JsonConvert.DeserializeObject<Translation>(line);

                        text = "";

                        foreach (string str in translation.text)
                        {
                            text += str;
                        }
                    }
                }

                return text;
            }
            else
                return "";
        }
    }
}
