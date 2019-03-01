using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Collections.Generic;
using Orion.Other;
using Orion.Managers;
using Math = Orion.Other.Math;


namespace Orion.Features
{
    internal class Aimbot
    {
        public static void Run()
        {
            while (true)
            {
                if (Imports.IsWindowFocues(menu.GameProcess))
                {
                    if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Aimbot.Key) & 0x8000) || Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.Aimbot.SecondKey) & 0x8000) && Structs.LocalPlayer.Health.IsAlive()) //&& !Convert.ToBoolean((long)Globals.Imports.GetAsyncKeyState(Settings.Trigger.Key) & 0x8000)
                    {
                        if (Settings.Aimbot.UseMouseEvent)
                        {
                            Thread.Sleep(Settings.Aimbot.Smooth == 0f ? 1 : 25);
                            int maxPlayers = Structs.ClientState.MaxPlayers;
                            byte[] entities = MemoryManager.ReadMemory((int)Structs.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);
                            Dictionary<float, Vector3> possibleTargets = new Dictionary<float, Vector3> { };
                            if (Reader.WeaponID == 42 || Reader.WeaponID == 59 || Reader.WeaponID == 262667 || Reader.WeaponID == 262666 || Reader.WeaponID == 262633 || Reader.WeaponID == 262651 || Reader.WeaponID == 262664) continue;
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

                                //////////bSpotedCheck//////////////
                                if (Settings.Aimbot.VisibleOnly)
                                {
                                    int EnemySpoted = MemoryManager.ReadMemory<int>(cEntity + Offsets.m_bSpottedByMask);
                                    if (EnemySpoted == 0 || EnemySpoted % 2 == 0) continue;
                                }
                                //////////endbSpotedCheck///////////

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

                                    Vector3 HeadDestination = Settings.Aimbot.RecoilControl ? Math.CalcAngle(Structs.LocalPlayer.Position, headPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory) : Math.CalcAngle(Structs.LocalPlayer.Position, headPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                    float HeadDistance = Math.GetDistance3D(HeadDestination, Structs.ClientState.ViewAngles);



                                    if (neckPosition == Vector3.Zero) continue;
                                    Vector3 NeckDestination = Settings.Aimbot.RecoilControl ? Math.CalcAngle(Structs.LocalPlayer.Position, neckPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory) : Math.CalcAngle(Structs.LocalPlayer.Position, neckPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                    float NeckDistance = Math.GetDistance3D(NeckDestination, Structs.ClientState.ViewAngles);



                                    if (chestPosition == Vector3.Zero) continue;

                                    Vector3 ChestDestination = Settings.Aimbot.RecoilControl ? Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory) : Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                    float ChestDistance = Math.GetDistance3D(ChestDestination, Structs.ClientState.ViewAngles);



                                    if (StomachPosition == Vector3.Zero) continue;
                                    Vector3 StomachDestination = Settings.Aimbot.RecoilControl ? Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory) : Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);

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

                                Vector3 distance2 = destination - Structs.ClientState.ViewAngles;
                                if (!(distance <= Settings.Aimbot.Fov)) continue;
                                possibleTargets.Add(distance, distance2);
                            }
                            if (!possibleTargets.Any()) continue;
                            Vector3 aimAngle = possibleTargets.OrderByDescending(x => x.Key).LastOrDefault().Value;
                            if (aimAngle.Y > 0.35 && aimAngle.Y < Settings.Aimbot.Fov)
                            {
                                Imports.mouse_event(0x01, -(int)Settings.Aimbot.Smooth, 0, 0, 0);
                            }
                            else if (aimAngle.Y < -0.35 && aimAngle.Y > -Settings.Aimbot.Fov)
                            {
                                Imports.mouse_event(0x01, (int)Settings.Aimbot.Smooth, 0, 0, 0);
                            }
                            if (aimAngle.X > 0.35)
                            {
                                Imports.mouse_event(0x01, 0, (int)Settings.Aimbot.Smooth, 0, 0);
                            }
                            else if (aimAngle.X < -0.35)
                            {
                                Imports.mouse_event(0x01, 0, -(int)Settings.Aimbot.Smooth, 0, 0);
                            }
                        }
                        else
                        {
                            Thread.Sleep(Settings.Aimbot.Smooth == 0f ? 1 : 40);
                            int maxPlayers = Structs.ClientState.MaxPlayers;
                            byte[] entities = MemoryManager.ReadMemory((int)Structs.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);
                            Dictionary<float, Vector3> possibleTargets = new Dictionary<float, Vector3> { };

                            for (int i = 0; i < maxPlayers; i++)
                            {
                                int cEntity = Math.GetInt(entities, i * 0x10);

                                Structs.Enemy_t entityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(cEntity);

                                if (!entityStruct.Team.HasTeam() || entityStruct.Team.IsMyTeam() || !entityStruct.Health.IsAlive())  continue;

                                Vector3 bonePosition = Math.GetBonePos(cEntity, Settings.Aimbot.Bone);
                                if (bonePosition == Vector3.Zero) continue;


                                //////////bSpotedCheck//////////////
                                if (Settings.Aimbot.VisibleOnly)
                                {
                                    int EnemySpoted = MemoryManager.ReadMemory<int>(cEntity + Offsets.m_bSpottedByMask);
                                    if (EnemySpoted == 0 || EnemySpoted % 2 == 0) continue;
                                }
                                //////////endbSpotedCheck///////////


                                Vector3 destination = Settings.Aimbot.RecoilControl ? Math.CalcAngle(Structs.LocalPlayer.Position, bonePosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory) : Math.CalcAngle(Structs.LocalPlayer.Position, bonePosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                if (destination == Vector3.Zero) continue;
                                float distance = Math.GetDistance3D(destination, Structs.ClientState.ViewAngles);

                                //////////////////////////////NEAREST SEQENCE//////////////////////////////
                                if (Settings.Aimbot.Bone == 0)
                                {
                                    Vector3 headPosition = Math.GetBonePos(cEntity, 8);
                                    Vector3 neckPosition = Math.GetBonePos(cEntity, 7);
                                    Vector3 chestPosition = Math.GetBonePos(cEntity, 6);
                                 //   Vector3 StomachPosition = Math.GetBonePos(cEntity, 5);

                                    if (headPosition == Vector3.Zero) continue;

                                    Vector3 HeadDestination = Settings.Aimbot.RecoilControl ? Math.CalcAngle(Structs.LocalPlayer.Position, headPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory) : Math.CalcAngle(Structs.LocalPlayer.Position, headPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                    float HeadDistance = Math.GetDistance3D(HeadDestination, Structs.ClientState.ViewAngles);

                                    if (neckPosition == Vector3.Zero) continue;

                                    Vector3 NeckDestination = Settings.Aimbot.RecoilControl ? Math.CalcAngle(Structs.LocalPlayer.Position, neckPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory) : Math.CalcAngle(Structs.LocalPlayer.Position, neckPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                    float NeckDistance = Math.GetDistance3D(NeckDestination, Structs.ClientState.ViewAngles);

                                    if (chestPosition == Vector3.Zero) continue;

                                    Vector3 ChestDestination = Settings.Aimbot.RecoilControl ? Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory) : Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                    float ChestDistance = Math.GetDistance3D(ChestDestination, Structs.ClientState.ViewAngles);
                                    /*
                                    if (StomachPosition == Vector3.Zero) continue;
                                    Vector3 StomachDestination = Settings.Aimbot.RecoilControl ? Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, Settings.Aimbot.YawRecoilReductionFactory, Settings.Aimbot.PitchRecoilReductionFactory) : Math.CalcAngle(Structs.LocalPlayer.Position, chestPosition, Structs.LocalPlayer.AimPunch, Structs.LocalPlayer.VecView, 0f, 0f);
                                    float StomachDistance = Math.GetDistance3D(ChestDestination, Structs.ClientState.ViewAngles);
                                    */

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

                                if (!(distance <= Settings.Aimbot.Fov)) continue;
                                possibleTargets.Add(distance, destination);
                            }
                            if (!possibleTargets.Any()) continue;

                            Vector3 aimAngle = possibleTargets.OrderByDescending(x => x.Key).LastOrDefault().Value;

                            if (Settings.Aimbot.Curve)
                            {
                                Vector3 qDelta = aimAngle - Structs.ClientState.ViewAngles;
                                qDelta += new Vector3(qDelta.Y / Settings.Aimbot.CurveY, qDelta.X / Settings.Aimbot.CurveX, qDelta.Z);
                                aimAngle = Structs.ClientState.ViewAngles + qDelta;
                            }

                            aimAngle = Math.NormalizeAngle(aimAngle);
                            aimAngle = Math.ClampAngle(aimAngle);


                            MemoryManager.WriteMemory<Vector3>(Structs.ClientState.Base + Offsets.dwClientState_ViewAngles, Settings.Aimbot.Smooth == 0f ? aimAngle : Math.SmoothAim(Structs.ClientState.ViewAngles, aimAngle, Settings.Aimbot.Smooth));
                        }
                    }
                }
                else Thread.Sleep(100);
            }
        }
    }
}
