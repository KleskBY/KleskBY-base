using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Collections.Generic;
using Orion.Other;
using Orion.Managers;
using System.Runtime.InteropServices;
using Math = Orion.Other.Math;

namespace Orion.Features
{
    class AutoDelay
    {
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(1);
                if (Imports.IsWindowFocues(menu.GameProcess))
                {
                    if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Aimbot.SecondKey) & 0x8000) && Structs.LocalPlayer.Health.IsAlive())
                    {
                        int maxPlayers = Structs.ClientState.MaxPlayers;
                        byte[] entities = MemoryManager.ReadMemory((int)Structs.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);
                        Dictionary<float, Vector3> possibleTargets = new Dictionary<float, Vector3> { };

                        for (int i = 0; i < maxPlayers; i++)
                        {
                            int cEntity = Math.GetInt(entities, i * 0x10);

                            Structs.Enemy_t entityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(cEntity);

                            if (!entityStruct.Team.HasTeam()
                                || entityStruct.Team.IsMyTeam()
                                || !entityStruct.Health.IsAlive())
                                continue;

                            Vector3 bonePosition = Math.GetBonePos(cEntity, Settings.Aimbot.Bone);
                            if (bonePosition == Vector3.Zero) continue;

                            Vector3 destination = Settings.Aimbot.RecoilControl
                                 ? Math.CalcAngle(Structs.LocalPlayer.Position, bonePosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory)
                                 : Math.CalcAngle(Structs.LocalPlayer.Position, bonePosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);

                            if (destination == Vector3.Zero) continue;

                            float distance = Math.GetDistance3D(destination, Structs.ClientState.ViewAngles);
                            //////////////////////////////NEAREST SEQENCE//////////////////////////////
                            if (Settings.Aimbot.Bone == 0)
                            {
                                Vector3 headPosition = Math.GetBonePos(cEntity, 8);
                                Vector3 neckPosition = Math.GetBonePos(cEntity, 7);
                                Vector3 chestPosition = Math.GetBonePos(cEntity, 6);
                                Vector3 StomachPosition = Math.GetBonePos(cEntity, 5);


                                if (headPosition == Vector3.Zero) continue;
                                Vector3 HeadDestination = Settings.Aimbot.RecoilControl
                                                                ? Math.CalcAngle(Structs.LocalPlayer.Position, headPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory)
                                                                : Math.CalcAngle(Structs.LocalPlayer.Position, headPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                float HeadDistance = Math.GetDistance3D(HeadDestination, Structs.ClientState.ViewAngles);



                                if (neckPosition == Vector3.Zero) continue;
                                Vector3 NeckDestination = Settings.Aimbot.RecoilControl
                                                                ? Math.CalcAngle(Structs.LocalPlayer.Position, neckPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory)
                                                                : Math.CalcAngle(Structs.LocalPlayer.Position, neckPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                float NeckDistance = Math.GetDistance3D(NeckDestination, Structs.ClientState.ViewAngles);



                                if (chestPosition == Vector3.Zero) continue;

                                Vector3 ChestDestination = Settings.Aimbot.RecoilControl
                                                                ? Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory)
                                                                : Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                float ChestDistance = Math.GetDistance3D(ChestDestination, Structs.ClientState.ViewAngles);



                                if (StomachPosition == Vector3.Zero) continue;
                                Vector3 StomachDestination = Settings.Aimbot.RecoilControl
                                                                ? Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory)
                                                                : Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);

                                float StomachDistance = Math.GetDistance3D(ChestDestination, Structs.ClientState.ViewAngles);
                                float ComapreHeadNeck = HeadDistance - NeckDistance;
                                float ComapreHeadChest = HeadDistance - ChestDistance;

                                double HeadDistance2 = (double)HeadDistance;
                                double NeckDistance2 = (double)NeckDistance;
                                double ChestDistance2 = (double)ChestDistance;

                                if (HeadDistance2 < NeckDistance2 && HeadDistance2 < ChestDistance2)
                                {
                                    destination = HeadDestination;
                                    distance = HeadDistance;
                                }
                                if (NeckDistance2 < HeadDistance2 && NeckDistance2 < ChestDistance2)
                                {
                                    destination = NeckDestination;
                                    distance = NeckDistance;
                                }
                                if (ChestDistance2 < HeadDistance2 && ChestDistance2 < NeckDistance2)
                                {
                                    destination = ChestDestination;
                                    distance = ChestDistance;
                                }
                            }
                            ////////////////////////////END NEAREST///////////////////////////////////

                            if (distance < 0.5)
                            {
                                //   Console.WriteLine(distance.ToString());
                                MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceAttack, 5);
                                Thread.Sleep(1);
                                MemoryManager.WriteMemory<int>((int)Structs.Base.Client + Offsets.dwForceAttack, 4);
                            }
                        }
                    }
                }
                else Thread.Sleep(49);
            }
        }
    }
}
