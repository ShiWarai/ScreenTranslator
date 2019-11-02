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
        static private WaitingForKeyEvent EventWaiter;

        static public void ChangeFunction(WaitingForKeyEvent waiting)
        {
            EventWaiter = waiting;
        }

        /// <summary>
        /// Hooking для клавиш, с последующим запуском события
        /// </summary>
        /// <param name="func"></param>
        public KeyboardHooking(WaitingForKeyEvent func)
        {
            m_GlobalHook = Hook.GlobalEvents();
            EventWaiter = func;
        }

        ~KeyboardHooking()
        {
            Disactivate();
        }

        /// <summary>
        /// Запускает hooking
        /// </summary>
        /// <param name="key">Ожидаемая клавиша</param>
        public void Start(Keys key = Keys.G)
        {
            activeKey = key;
            Activate();
        }

        public void Stop()
        {
            Disactivate();
            EventWaiter = null;
        }

        public void Pause()
        {
            m_GlobalHook.KeyDown -= ActiveKey_Down;
            m_GlobalHook.KeyUp -= ActiveKey_Up;
        }

        private void Activate()
        {
            // Create a rectangle of cursor positions
            m_GlobalHook.KeyDown += ActiveKey_Down;
            m_GlobalHook.KeyUp += ActiveKey_Up;
        }

        private void Disactivate()
        {
            m_GlobalHook.KeyDown -= ActiveKey_Down;
            m_GlobalHook.KeyUp -= ActiveKey_Up;
        }

        private void ActiveKey_Up(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == activeKey)
                EventWaiter.Activate(WaitingForKeyEvent.Events.KeyUp);
        }

        private void ActiveKey_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == activeKey)
                EventWaiter.Activate(WaitingForKeyEvent.Events.KeyDown);
        }
    }
}
