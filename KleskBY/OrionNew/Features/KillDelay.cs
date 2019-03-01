using System;
using System.Threading;
using Orion.Managers;
using Orion.Other;


namespace Orion.Features
{
    class KillDelay
    {
        public static int target = 0;
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(30);
                if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Aimbot.Key) & 0x8000))
                {
                    if (Settings.Aimbot.KillDelay && Settings.Aimbot.Enabled)
                    {
                        int maxPlayers = Structs.ClientState.MaxPlayers;
                        for (int i = 0; i < maxPlayers; i++)
                        {
                            int EntityInCrossID = MemoryManager.ReadMemory<int>(Structs.LocalPlayer.Base + Offsets.m_iCrosshairId);
                            if (EntityInCrossID > 0 && EntityInCrossID <= 64)
                            {
                                int EntityBase = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwEntityList + (EntityInCrossID - 1) * 0x10);
                                int EntityTeam = MemoryManager.ReadMemory<int>(EntityBase + Offsets.m_iTeamNum);
                                int PlayerTeam = MemoryManager.ReadMemory<int>(Structs.LocalPlayer.Base + Offsets.m_iTeamNum);

                                if (EntityTeam != PlayerTeam)
                                {
                                    target = EntityInCrossID;
                                }
                            }
                        }

                        if (Structs.LocalPlayer.Health.IsAlive())
                        {
                            if (target != 0)
                            {

                                int EntityBase = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwEntityList + (target - 1) * 0x10);
                                Structs.Enemy_t enemy = MemoryManager.ReadMemory<Structs.Enemy_t>(EntityBase);

                                if (enemy.Health.IsAlive())
                                {
                                    //Extensions.Information($"[Debug] ЖИВ {enemy.Health} !", true);
                                }
                                else
                                {
                                    target = 0;
                                    Settings.Aimbot.Smooth = 100;
                                    Settings.Aimbot.Fov = 0;
                                    ThreadManager.PauseThread("Aimbot");
                                    Settings.Aimbot.Smooth = 100;
                                    Settings.Aimbot.Fov = 0;
                                    Thread.Sleep(550);
                                    ThreadManager.ToggleThread("Aimbot");
                                    Settings.Aimbot.Smooth = 100;
                                    Settings.Aimbot.Fov = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
