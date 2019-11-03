using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenTranslator_MainApp.Model
{
    public class ThreadsMaster
    {
        List<Thread> threads;
        public ThreadsMaster() 
        {
            threads = new List<Thread>();
        }

        public bool Start(ThreadStart func)
        {
            try
            {
                Thread localThread = new Thread(func);
                localThread.Start();
                threads.Add(localThread);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Start(ParameterizedThreadStart func,object obj)
        {
            try
            {
                Thread localThread = new Thread(func);
                localThread.Start(obj);
                threads.Add(localThread);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void KillThreads()
        {
            foreach(Thread thread in threads)
            {
                thread.Abort();
            }
        }
    }
}
