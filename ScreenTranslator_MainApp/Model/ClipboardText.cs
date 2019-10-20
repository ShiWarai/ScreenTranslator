using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScreenTranslator_MainApp.Model
{
    class ClipboardText : TextInitor
    {
        public ClipboardText() : base()
        {
            text = Clipboard.GetText();
        }
    
    }
}
