using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenTranslator_MainApp.Model
{
    abstract class WaitingForKeyEvent
    {
        protected delegate void SwitchEvent();
        protected event SwitchEvent ActivatorEvent;
        protected event SwitchEvent DeactivatorEvent;
        public enum Events
        {
            KeyDown,
            KeyUp
        }

        public void Activate(Events event_)
        {
            switch (event_)
            {
                case Events.KeyDown:
                    try
                    {
                        this.ActivatorEvent.Invoke();
                    }
                    catch { }
                    break;
                case Events.KeyUp:
                    try
                    {
                        this.DeactivatorEvent.Invoke();
                    }
                    catch { }
                    break;
            }
            
        }
    }
}
