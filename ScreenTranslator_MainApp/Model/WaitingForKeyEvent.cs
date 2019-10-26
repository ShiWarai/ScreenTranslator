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
                    this.ActivatorEvent.Invoke();
                    break;
                case Events.KeyUp:
                    this.DeactivatorEvent.Invoke();
                    break;
            }
            
        }
    }
}
