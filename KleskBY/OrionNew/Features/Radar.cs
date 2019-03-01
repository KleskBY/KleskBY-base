using System.Threading;
using Orion.Other;
using Orion.Managers;

namespace Orion.Features
{
    internal class Radar
    {
        public static void Run()
        {
            while (true) 
            {
                Thread.Sleep(50);
                if (Imports.IsWindowFocues(Orion.menu.GameProcess) && !Settings.Radar.Enabled) continue;
                byte[] entities = MemoryManager.ReadMemory((int)Structs.Base.Client + Offsets.dwEntityList, Structs.ClientState.MaxPlayers * 0x10);

                for (int i = 0; i < Structs.ClientState.MaxPlayers; i++) 
                {
                    int cEntity = Math.GetInt(entities, i * 0x10);

                    Structs.Enemy_t cEntityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(cEntity);

                    if (cEntityStruct.Spotted 
                        || !cEntityStruct.Health.IsAlive() 
                        || cEntityStruct.Team.IsMyTeam())
                         continue;

                    MemoryManager.WriteMemory<int>(cEntity + Offsets.m_bSpotted, 1);

                    Thread.Sleep(150);
                }
            }
        }
    }
}
