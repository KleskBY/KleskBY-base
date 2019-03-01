using System;
using System.Threading;
using System.Runtime.InteropServices;
using Orion.Other;
using Orion.Managers;


namespace Orion.Features
{
    internal class Trigger
    {
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(3);
                if (!Imports.IsWindowFocues(menu.GameProcess)) continue;
                for (int i = 0; i < Structs.ClientState.MaxPlayers; i++)
                {
                    if (!Imports.IsWindowFocues(Orion.menu.GameProcess)) continue;

                    int EntityInCrossID = MemoryManager.ReadMemory<int>(Structs.LocalPlayer.Base + Offsets.m_iCrosshairId);
                    if (EntityInCrossID > 0 && EntityInCrossID <= 64)
                    {
                        int EntityBase = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwEntityList + (EntityInCrossID - 1) * 0x10);
                        int EntityTeam = MemoryManager.ReadMemory<int>((int)EntityBase + Offsets.m_iTeamNum);
                        int PlayerTeam = MemoryManager.ReadMemory<int>(Structs.LocalPlayer.Base + Offsets.m_iTeamNum);
                        if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Trigger.Key) & 0x8000))
                        {
                            if (EntityTeam != PlayerTeam)
                            {
                                Thread.Sleep(Settings.Trigger.Delay);
                                MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceAttack, 5);
                                Thread.Sleep(20);
                                MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceAttack, 4);
                                Thread.Sleep(Settings.Trigger.DelayBetweenShots);
                            }
                        }
                    }
                }
            }
        }
    }
}
