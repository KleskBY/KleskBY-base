using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Managers;
using Orion.Other;
using System.Threading;
using System.Runtime.InteropServices;

namespace Orion.Features
{
    class AutoPistol
    {
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(1);
                
                if (Imports.IsWindowFocues(Orion.menu.GameProcess))
                {
                    if (Settings.Aimbot.AutoPistol)
                    {
                        int IsMenu = MemoryManager.ReadMemory<int>((int)Structs.Base.Engine + Offsets.IsMenu);
                        if (IsMenu == 0)
                        {
                            if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Aimbot.Key) & 0x8000)) //&& !Convert.ToBoolean((long)Globals.Imports.GetAsyncKeyState(Settings.Trigger.Key) & 0x8000)
                            {
                                int ForceState = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwForceAttack);

                                if (ForceState == 4) MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceAttack, 5);
                                else MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceAttack, 4);

                                Thread.Sleep(2);
                                if (ForceState == 5) MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceAttack, 4);
                            }
                        }
                    }
                }
                else Thread.Sleep(49);
            }
        }
    }
}

