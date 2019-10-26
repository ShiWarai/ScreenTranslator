using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

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

    public struct MouseRectangle 
    {
        private MouseCoordinates first;
        private MouseCoordinates second;

        public MouseCoordinates First
        {
            get { return !first.isEmpty ? first : new MouseCoordinates(); }
            set
            {
                first = value;
            }
        }

        public MouseCoordinates Second
        {
            get { return !second.isEmpty ? second : new MouseCoordinates(); }
            set 
            { 
                second = value;
            }
        }

        public bool isEmpty
        {
            get { return (first.isEmpty || second.isEmpty) ? true : false;  }
        }
    }

    public class MouseCoordinator
    { 

        static public MouseCoordinates GetCurrentMousePosition()
        {
            return new MouseCoordinates() { X = Cursor.Position.X, Y = Cursor.Position.Y };
        }

    }
}