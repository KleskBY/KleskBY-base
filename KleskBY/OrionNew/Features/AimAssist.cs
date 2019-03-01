using System;
using System.Threading;
using System.Runtime.InteropServices;
using Orion.Other;
using Orion.Managers;


namespace Orion.Features
{
    class AimAssist
    {     
        public static void Run()
        {
            int UserSens = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwSensitivity);
            while (true)
            {
                Thread.Sleep(30);
                if (Settings.AimAssist.Enabled && Imports.IsWindowFocues(Orion.menu.GameProcess))
                {
                    for (int i = 0; i < Structs.ClientState.MaxPlayers; i++)
                    {
                        int EntityInCrossID = MemoryManager.ReadMemory<int>(Structs.LocalPlayer.Base + Offsets.m_iCrosshairId);
                        if (EntityInCrossID > 0 && EntityInCrossID <= 64)
                        {
                            int EntityBase = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwEntityList + (EntityInCrossID - 1) * 0x10);
                            int EntityTeam = MemoryManager.ReadMemory<int>((int)EntityBase + Offsets.m_iTeamNum);
                            int PlayerTeam = MemoryManager.ReadMemory<int>((int)Structs.LocalPlayer.Base + Offsets.m_iTeamNum);

                            if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.AimAssist.Key) & 0x8000))
                            {
                                if (EntityTeam != PlayerTeam)
                                {
                                    MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwSensitivity, 531155266);
                                }
                            }
                        }
                        else
                        {
                            MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwSensitivity, UserSens);
                        }
                    }
                }
            }
        }
    }
}
