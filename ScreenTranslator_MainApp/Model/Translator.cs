using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace ScreenTranslator_MainApp.Model
{
    internal struct Translation
    {
        public string code { get; set; }
        public string lang { get; set; }
        public string[] text { get; set; }
    }

    public class Translator
    {
        static private string Key = "trnsl.1.1.20191013T000841Z.a1691e726d3b0db8.19a9f0b65a94f0e6f7aea4f246947a0b3fe1ee84";

        static public string Translate(string text = "", string translation_code = "en")
        {
            if (text.Length > 0)
            {
                WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
                    + "key=" + Translator.Key
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