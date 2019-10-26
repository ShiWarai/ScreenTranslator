using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace ScreenTranslator_MainApp.Model
{
    class KeyboardHooking
    {
        private IKeyboardMouseEvents m_GlobalHook;
        private Keys activeKey;
        private WaitingForKeyEvent waiter;

        public KeyboardHooking()
        {
            m_GlobalHook = Hook.GlobalEvents();
        }

        ~KeyboardHooking()
        {
            ScreenTranslatorDisactivate();
        }

        public void Start(Keys key, WaitingForKeyEvent obj)
        {
            activeKey = key;
            waiter = obj;
            ScreenTranslatorActivate();
        }

        private void ScreenTranslatorActivate()
        {
            // Create a rectangle of cursor positions
            m_GlobalHook.KeyDown += ActiveKey_Down;
            m_GlobalHook.KeyUp += ActiveKey_Up;
        }

        private void ScreenTranslatorDisactivate()
        {
            m_GlobalHook.KeyDown -= ActiveKey_Down;
            m_GlobalHook.KeyUp -= ActiveKey_Up;
        }

        private void ActiveKey_Up(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == activeKey)
                waiter.Activate(WaitingForKeyEvent.Events.KeyUp);
        }

        private void ActiveKey_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == activeKey)
                waiter.Activate(WaitingForKeyEvent.Events.KeyDown);
        }
    }
}
