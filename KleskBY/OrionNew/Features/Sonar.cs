using System;
using System.Numerics;
using System.Threading;
using System.Collections.Generic;
using Orion.Other;
using Orion.Managers;
using Math = Orion.Other.Math;
using System.Linq;

namespace Orion.Features
{
    class Sonar
    {
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(1);

                if (!Imports.IsWindowFocues(Orion.menu.GameProcess)) continue;
                Thread.Sleep(Settings.Sonar.interval);
                try
                {
                    if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Sonar.Key) & 0x8000))
                    {
                        int maxplayers = Structs.ClientState.MaxPlayers;
                        byte[] entities = MemoryManager.ReadMemory((int)Structs.Base.Client + Offsets.dwEntityList, maxplayers * 0x10);   //Getting list of entites. Entity = gameobject (guns,players, props etc)
                        List<double> possibleTargets = new List<double> { };

                        for (int i = 0; i < maxplayers; i++)
                        {
                            int Entity = Math.GetInt(entities, i * 0x10);      
                            /*          int EntityTeam = MemoryManager.ReadMemory<int>((int)Entity + Offsets.m_iTeamNum);
                                      if (EntityTeam != 2 && EntityTeam != 3) continue;
                                      int PlayerTeam = MemoryManager.ReadMemory<int>(Structs.LocalPlayer.Base + Offsets.m_iTeamNum); //Reading client player team
                                      if (EntityTeam == PlayerTeam) continue;
                                      int EntityHealth = MemoryManager.ReadMemory<int>((int)Entity + Offsets.m_iHealth);
                                      if (EntityHealth < 1) continue;*/

                            Structs.Enemy_t entityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(Entity);

                            if (!entityStruct.Team.HasTeam()
                                || entityStruct.Team.IsMyTeam()
                                || !entityStruct.Health.IsAlive())
                                continue;

                            Vector3 myPos = MemoryManager.ReadMemory<Vector3>(Structs.LocalPlayer.Base + Offsets.m_vecOrigin);
                            if (myPos == Vector3.Zero) continue;
                            Vector3 hisPos = MemoryManager.ReadMemory<Vector3>(Entity + Offsets.m_vecOrigin);
                            if (hisPos == Vector3.Zero) continue;

                            double dist = Math.GetPlayerDistance(myPos, hisPos);
                            if (dist != 0 && dist < 15)
                            {
                                //  Console.WriteLine($"{dist}");
                                possibleTargets.Add(dist);
                            }
                        }
                        if (!possibleTargets.Any()) continue;

                        double distance = possibleTargets.Min();
                        if (distance <= 15 && distance > 13) Console.Beep(100, 100);
                        if (distance <= 13 && distance > 10) Console.Beep(200, 100);
                        if (distance <= 10 && distance > 7) Console.Beep(300, 100);
                        if (distance <= 7 && distance > 5) Console.Beep(400, 100);
                        if (distance <= 5 && distance > 3) Console.Beep(500, 100);
                        if (distance <= 3) Console.Beep(600, 100);
                    }
                }
                catch { }
            }
        }
    }
}
