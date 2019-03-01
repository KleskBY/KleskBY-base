using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Other;
using Orion.Managers;
using System.Threading;
using System.Drawing;

namespace Orion.Features
{
    class Chams
    {
        public static void Run()
        {
            Thread.Sleep(10);

            while (true)
            {
                Thread.Sleep(100);

              //  if (!Imports.IsWindowFocues(Form1.GameProcess))
                    //Chams SEQENCE

                byte[] entities = MemoryManager.ReadMemory((int)Structs.Base.Client + Offsets.dwEntityList, Structs.ClientState.MaxPlayers * 0x10);
                for (int i = 0; i < Structs.ClientState.MaxPlayers; i++)
                { 
                    int cEntity = Other.Math.GetInt(entities, i * 0x10);

                    Structs.Enemy_t entityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(cEntity);

                    if (entityStruct.Team.HasTeam() || entityStruct.Health.IsAlive() || Structs.LocalPlayer.Team != 0 || entityStruct.Team != 0 || Structs.LocalPlayer.Health.IsAlive())
                    {
                        if (!entityStruct.Team.IsMyTeam())
                        {
                            if (Settings.Chams.HealthBased)
                            {
                                int EnemySpoted = MemoryManager.ReadMemory<int>(cEntity + Offsets.m_bSpottedByMask);
                                if (EnemySpoted % 2 != 0)
                                {
                                    Color color = Colors.FromHealth(entityStruct.Health / 100f);
                                    var HealthChams = new Structs.ChamsObject()
                                    {
                                        r = (byte)(color.R),
                                        g = (byte)(color.G),
                                        b = (byte)(color.B),
                                        a = 255
                                    };
                                    MemoryManager.WriteMemory<Structs.ChamsObject>(cEntity + 0x70, HealthChams);
                                }
                            }
                            else
                            {
                                var chamsObject = new Structs.ChamsObject()
                                {
                                    r = Settings.Chams.Color_R,
                                    g = Settings.Chams.Color_G,
                                    b = Settings.Chams.Color_B,
                                    a = 255
                                };
                                MemoryManager.WriteMemory<Structs.ChamsObject>(cEntity + 0x70, chamsObject);
                            }
                        }

                        else if (entityStruct.Team.IsMyTeam())
                        {
                            if (Settings.Chams.Allies)
                            {
                                var chamsObject2 = new Structs.ChamsObject()
                                {
                                    r = Settings.Chams.Allies_Color_R,
                                    g = Settings.Chams.Allies_Color_G,
                                    b = Settings.Chams.Allies_Color_B,
                                    a = 255
                                };
                                MemoryManager.WriteMemory<Structs.ChamsObject>(cEntity + 0x70, chamsObject2);
                            }
                        }
                    }
                }
            }
        }
    }
}
