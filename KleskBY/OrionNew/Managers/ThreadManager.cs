using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Collections.Generic;
using Orion.Other;
using System.Windows.Forms;

namespace Orion.Managers
{
    internal class ThreadManager
    {
        public static Dictionary<string, Thread> threads = new Dictionary<string, Thread>();
        public static Dictionary<string, Thread> activeThreads = new Dictionary<string, Thread>();
        public static Dictionary<string, Thread> pausedThreads = new Dictionary<string, Thread>();


        public static void Add(string name, ThreadStart function)
        {
            if (threads.TryGetValue(name, out Thread temp)) return;
            threads.Add(name, new Thread(function));
        }

        public static void PauseThread(string name)
        {
            if (activeThreads.TryGetValue(name, out Thread temp))
            {
            #pragma warning disable CS0618 // Typ oder Element ist veraltet
            temp.Suspend();
            #pragma warning restore CS0618 // Typ oder Element ist veraltet

                activeThreads.Remove(name);

                pausedThreads.Add(name, temp);
            }
        }


        public static void StartThread(string name)
        {
            if (activeThreads.TryGetValue(name, out Thread temp))
            {


                //Extensions.Information($"[DEBUG] { name } working ", true);
               // Thread.Sleep(1)

            }
            else
            {
                if (!threads.TryGetValue(name, out temp))
                {
                //    Extensions.Error($"> Could not start { name }", 1500, false);
                    return;
                }

                if (pausedThreads.TryGetValue(name, out Thread temp2))
                {
                #pragma warning disable CS0618 // Typ oder Element ist veraltet
                temp2.Resume();
                #pragma warning restore CS0618 // Typ oder Element ist veraltet
                pausedThreads.Remove(name);
                }
                else
                {
                    temp.Start();
                }
                activeThreads.Add(name, temp);
            }
        }

        public static void ToggleThread(string name)
        {
            if (activeThreads.TryGetValue(name, out Thread temp))
            {
                #pragma warning disable CS0618 // Typ oder Element ist veraltet
                temp.Suspend();
                #pragma warning restore CS0618 // Typ oder Element ist veraltet

                activeThreads.Remove(name);

                pausedThreads.Add(name, temp);

           //     Extensions.Information($"> { name } OFF", true);
            }
            else
            {
                if (!threads.TryGetValue(name, out temp))
                {
              //      Extensions.Error($"> Could not start { name }", 1500, false);
                    return;
                }

                if (pausedThreads.TryGetValue(name, out Thread temp2))
                {
                    #pragma warning disable CS0618 // Typ oder Element ist veraltet
                    temp2.Resume();
                    #pragma warning restore CS0618 // Typ oder Element ist veraltet

                    pausedThreads.Remove(name);

            //        Extensions.Information($"[ThreadManager][Resumed] { name } { Features.Aimbot.ActWeap } {Features.Aimbot.WeapEnt}", true);

                 //   Console.Beep(300, 100);
                }
                else
                {
                    temp.Start();
                   // Extensions.Information($"> { name } ON", true);
                }
                activeThreads.Add(name, temp);
            }
        }
    }
}
