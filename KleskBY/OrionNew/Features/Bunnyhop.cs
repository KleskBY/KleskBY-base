using System;
using System.Numerics;
using System.Threading;
using Orion.Other;
using Orion.Managers;
using System.Windows.Forms;


namespace Orion.Features
{
    internal class Bunnyhop
    {
        static Vector3 prevAngle;
        static bool strafe;
       // static int state;

        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(3);
                if (!Imports.IsWindowFocues(Orion.menu.GameProcess)) Thread.Sleep(100);
                else
                {

                    if (Settings.Bunnyhop.StrafeEmulator && Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Bunnyhop.StrafeEmulatorKey) & 0x8000))
                    {
                        // Жмем кнопку //0x101
                        MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceLeft, 0);
                        MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceRight, 6);
                        //state = 1;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 2;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 3;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 4;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 5;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 6;
                        // Отпускаем D
                        MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceRight, 0);
                        MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceLeft, 6);
                        //   Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 7;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 8;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 9;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 10;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 11;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 12;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 13;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 14;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 13;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 16;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 17;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, -Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 18;
                        MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceLeft, 0);
                        MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceRight, 6);
                        //  Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 19;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 20;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 21;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 22;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        //state = 23;
                        Thread.Sleep(Settings.Bunnyhop.speed);
                        Imports.mouse_event(0x0001, Settings.Bunnyhop.sens, 0, 0, 0);
                        // Отпускаем "D" 
                        MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceRight, 0);
                        //state = 24;
                    }


                    //AutoStrafe SEQUENCE
                    if (Settings.Bunnyhop.AutoStrafe && Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Bunnyhop.Key) & 0x8000)) //!Checks.CanBunnyhop &&
                    {
                        Vector3 ang = Structs.ClientState.ViewAngles;
                        if (!strafe)
                        {

                            prevAngle = ang;
                        }
                        strafe = true;
                        if (ang.Y > Bunnyhop.prevAngle.Y)
                        {
                            //  Extensions.Information($"[Debug]  LEFT", true);
                            //   sim.Keyboard.KeyUp(VirtualKeyCode.VK_D);
                            //   sim.Keyboard.KeyDown(VirtualKeyCode.VK_A);
                            MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceLeft, 6);
                            MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceRight, 0);
                        }
                        else if (ang.Y < Bunnyhop.prevAngle.Y)
                        {

                            //  sim.Keyboard.KeyUp(VirtualKeyCode.VK_A);
                            //  sim.Keyboard.KeyDown(VirtualKeyCode.VK_D);
                            MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceLeft, 0);
                            MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceRight, 6);

                        }
                        prevAngle = ang;
                    }
                    else if (Settings.Bunnyhop.AutoStrafe && !Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Bunnyhop.Key) & 0x8000))
                    {
                        prevAngle = Structs.ClientState.ViewAngles;
                        if (strafe)
                        {
                            // sim.Keyboard.KeyUp(VirtualKeyCode.VK_A);
                            //  sim.Keyboard.KeyUp(VirtualKeyCode.VK_D);
                            strafe = false;
                            MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceLeft, 0);
                            MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceRight, 0);
                        }
                        strafe = false;

                    }
                    if (!Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Bunnyhop.Key) & 0x8000)
                        || !Imports.IsWindowFocues(Orion.menu.GameProcess)
                        || !Structs.LocalPlayer.Health.IsAlive()
                        || !Checks.CanBunnyhop)
                        continue;

                    if (Settings.Bunnyhop.Enabled)
                    {
                        int IsMenu = MemoryManager.ReadMemory<int>((int)Structs.Base.Engine + Offsets.IsMenu);
                        if (IsMenu == 0)
                        {
                            MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceJump, 5);
                            Thread.Sleep(15);
                            MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceJump, 4);
                        }
                    }
                }
            }
        }
    }
}

