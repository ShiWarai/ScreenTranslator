using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace ScreenTranslator_MainApp.Model
{
    public delegate MouseCoordinates MousePositionChanged();
    public struct MouseCoordinates
    {
        private bool init;
        private int x;
        private int y;
        public int X
        {
            get { return init ? x : -1; }
            set { x = value; init = true; }
        }
        public int Y
        {
            get { return init ? y : -1; }
            set { y = value; init = true; }
        }
        public bool isEmpty
        {
            get { return !init; }
        }
    }


    public class MouseAndKeyboardTracking
    {
        static public MouseCoordinates localFirstCoordinate;
        static public MouseCoordinates localSecondCoordinate;
        static private IKeyboardMouseEvents m_GlobalHook;

        static public MouseCoordinates GetCurrentMousePosition()
        {
            return new MouseCoordinates() { X = Cursor.Position.X, Y = Cursor.Position.Y };
        }
        
        static public (MouseCoordinates FirstPos,MouseCoordinates SecondPos) CreateCursorRectangle()
        {
            Hook.GlobalEvents().KeyDown += M_GlobalHook_KeyDown;
            Hook.GlobalEvents().KeyDown += M_GlobalHook_KeyUp;
            while (localFirstCoordinate.isEmpty) { }
            var coord1 = localFirstCoordinate;
            while (localSecondCoordinate.isEmpty) { }
            var coord2 = localSecondCoordinate;
            Hook.GlobalEvents().KeyDown -= M_GlobalHook_KeyDown;
            Hook.GlobalEvents().KeyUp -= M_GlobalHook_KeyUp;
            Hook.GlobalEvents().Dispose();
            return (coord1, coord2);
        }

        private static void M_GlobalHook_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    localFirstCoordinate = MouseAndKeyboardTracking.GetCurrentMousePosition();
                    break;
            }
        }

        private static void M_GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    localFirstCoordinate = MouseAndKeyboardTracking.GetCurrentMousePosition();
                    break;
            }
        }



    }
}
