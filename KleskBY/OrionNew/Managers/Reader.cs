using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Other;
using System.Threading;
using Orion.Managers;

namespace Orion.Managers
{
    class Reader
    {
        public static int WeaponID;
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(10);

                    Structs.LocalPlayer.Base = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwLocalPlayer);
                    Structs.LocalPlayer_t localPlayerStruct = MemoryManager.ReadMemory<Structs.LocalPlayer_t>(Structs.LocalPlayer.Base);
                    Structs.LocalPlayer.LifeState = localPlayerStruct.LifeState;
                    Structs.LocalPlayer.Health = localPlayerStruct.Health;
                    Structs.LocalPlayer.Flags = localPlayerStruct.Flags;
                    Structs.LocalPlayer.MoveType = localPlayerStruct.MoveType;
                    Structs.LocalPlayer.Team = localPlayerStruct.Team;
                    Structs.LocalPlayer.ShotsFired = localPlayerStruct.ShotsFired;
                    Structs.LocalPlayer.Position = localPlayerStruct.Position;
                    Structs.LocalPlayer.AimPunch = localPlayerStruct.AimPunch;
                    Structs.LocalPlayer.VecView = localPlayerStruct.VecView;
                    Structs.ClientState.Base = MemoryManager.ReadMemory<int>((int)Structs.Base.Engine + Offsets.dwClientState);
                    Structs.ClientState_t clientStateStruct = MemoryManager.ReadMemory<Structs.ClientState_t>(Structs.ClientState.Base);
                    Structs.ClientState.GameState = clientStateStruct.GameState;
                    Structs.ClientState.ViewAngles = clientStateStruct.ViewAngles;
                    Structs.ClientState.MaxPlayers = clientStateStruct.MaxPlayers;

                    int getHandleWeap = MemoryManager.ReadMemory<int>((int)Structs.LocalPlayer.Base + Offsets.m_hActiveWeapon);
                    int getWeapEnt = getHandleWeap &= 0xFFF;
                    int dwWeapon = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwEntityList + (getWeapEnt - 1) * 0x10);
                    int weapID = MemoryManager.ReadMemory<int>((int)dwWeapon + Offsets.m_iItemDefinitionIndex);
                  //  int Spoted = MemoryManager.ReadMemory<int>((int)Structs.LocalPlayer.Base + Offsets.m_bSpotted);
                    WeaponID = weapID;

                 //  Console.WriteLine($"{Structs.LocalPlayer.AimPunch},{Structs.LocalPlayer.VecView}");

                //Glock
                if (WeaponID == 4 || WeaponID == 262148)
                {

                    Settings.Aimbot.AutoPistol = Settings.Aimbot.GlockAutoPistol;
                    Settings.Aimbot.Fov = Settings.Aimbot.GlockFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.GlockBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.GlockSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.GlockRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.GlockYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.GlockPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.GlockCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.GlockCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.GlockCurveX;

                }
                //USP
                else if (WeaponID == 61 || WeaponID == 262205)
                {
                        //   Settings.Aimbot.SilentAim = false;
                        Settings.Aimbot.AutoPistol = Settings.Aimbot.USPAutoPistol;
                        Settings.Aimbot.Fov = Settings.Aimbot.USPFov;
                        Settings.Aimbot.Bone = Settings.Aimbot.USPBone;
                        Settings.Aimbot.Smooth = Settings.Aimbot.USPSmooth;
                        Settings.Aimbot.RecoilControl = Settings.Aimbot.USPRecoilControl;
                        Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.USPYawRecoilReductionFactory;
                        Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.USPPitchRecoilReductionFactory;
                        Settings.Aimbot.Curve = Settings.Aimbot.USPCurve;
                        Settings.Aimbot.CurveY = Settings.Aimbot.USPCurveY;
                        Settings.Aimbot.CurveX = Settings.Aimbot.USPCurveX;
                }
                //P2000
                else if (WeaponID == 32 || WeaponID == 262176)
                {
                    Settings.Aimbot.AutoPistol = Settings.Aimbot.P2000AutoPistol;
                    Settings.Aimbot.Fov = Settings.Aimbot.P2000Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.P2000Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.P2000Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.P2000RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.P2000YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.P2000PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.P2000Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.P2000CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.P2000CurveX;
                }
                //Duals
                else if (WeaponID == 2 || WeaponID == 262146)
                {
                    Settings.Aimbot.AutoPistol = Settings.Aimbot.DualsAutoPistol;
                    Settings.Aimbot.Fov = Settings.Aimbot.DualsFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.DualsBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.DualsSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.DualsRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.DualsYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.DualsPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.DualsCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.DualsCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.DualsCurveX;
                }
                //P250
                else if (WeaponID == 36 || WeaponID == 262180)
                {

                    Settings.Aimbot.AutoPistol = Settings.Aimbot.P250AutoPistol;
                    Settings.Aimbot.Fov = Settings.Aimbot.P250Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.P250Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.P250Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.P250RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.P250YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.P250PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.P250Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.P250CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.P250CurveX;
                }
                //FiveSeven
                else if (WeaponID == 3 || WeaponID == 262147)
                {

                    Settings.Aimbot.AutoPistol = Settings.Aimbot.FiveSevenAutoPistol;
                    Settings.Aimbot.Fov = Settings.Aimbot.FiveSevenFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.FiveSevenBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.FiveSevenSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.FiveSevenRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.FiveSevenYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.FiveSevenPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.FiveSevenCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.FiveSevenCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.FiveSevenCurveX;
                }
                //TEC9
                else if (WeaponID == 30 || WeaponID == 262174)
                {

                    Settings.Aimbot.AutoPistol = Settings.Aimbot.TEC9AutoPistol;
                    Settings.Aimbot.Fov = Settings.Aimbot.TEC9Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.TEC9Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.TEC9Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.TEC9RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.TEC9YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.TEC9PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.TEC9Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.TEC9CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.TEC9CurveX;
                }
                //DEAGLE
                else if (WeaponID == 1 || WeaponID == 262145)
                {
                    if (Structs.LocalPlayer.AimPunch.X < -0.01)
                    {
                        Settings.Aimbot.Fov = 0;
                        Settings.Aimbot.Bone = 0;
                        Settings.Aimbot.Smooth = 100;
                    }
                    Settings.Aimbot.AutoPistol = Settings.Aimbot.DEAGLEAutoPistol;
                    Settings.Aimbot.Fov = Settings.Aimbot.DEAGLEFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.DEAGLEBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.DEAGLESmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.DEAGLERecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.DEAGLEYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.DEAGLEPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.DEAGLECurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.DEAGLECurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.DEAGLECurveX;
                }
                //REVOLVER
                else if (WeaponID == 64 || WeaponID == 262208)
                {
                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.RevolverFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.RevolverBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.RevolverSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.RevolverRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.RevolverYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.RevolverPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.RevolverCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.RevolverCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.RevolverCurveX;
                }
                //CZ
                else if (WeaponID == 63 || WeaponID == 262207)
                {
                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.CZFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.CZBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.CZSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.CZRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.CZYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.CZPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.CZCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.CZCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.CZCurveX;
                }
                //SMGs
                //MAC10
                else if (WeaponID == 17 || WeaponID == 262161)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.MAC10Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.MAC10Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.MAC10Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.MAC10RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.MAC10YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.MAC10PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.MAC10Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.MAC10CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.MAC10CurveX;
                }
                //MP9
                else if (WeaponID == 34 || WeaponID == 262178)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.MP9Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.MP9Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.MP9Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.MP9RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.MP9YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.MP9PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.MP9Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.MP9CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.MP9CurveX;
                }
                //MP7
                else if (WeaponID == 33 || WeaponID == 262177)
                {
                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.MP7Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.MP7Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.MP7Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.MP7RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.MP7YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.MP7PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.MP7Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.MP7CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.MP7CurveX;
                }
                //UMP
                else if (WeaponID == 24 || WeaponID == 262168)
                {


                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.UMPFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.UMPBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.UMPSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.UMPRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.UMPYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.UMPPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.UMPCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.UMPCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.UMPCurveX;
                }
                //Bizon
                else if (WeaponID == 26 || WeaponID == 262170)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.BizonFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.BizonBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.BizonSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.BizonRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.BizonYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.BizonPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.BizonCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.BizonCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.BizonCurveX;
                }
                //P90
                else if (WeaponID == 19 || WeaponID == 262163)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.P90Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.P90Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.P90Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.P90RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.P90YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.P90PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.P90Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.P90CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.P90CurveX;
                }
                //mp5
                else if (WeaponID == 262167)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.MP5Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.MP5Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.MP5Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.MP5RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.MP5YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.MP5PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.MP5Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.MP5CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.MP5CurveX;
                }
                //RIFLES
                //GALIL
                else if (WeaponID == 262157)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.GalilFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.GalilBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.GalilSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.GalilRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.GalilYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.GalilPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.GalilCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.GalilCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.GalilCurveX;
                }
                //FAMAS
                else if (WeaponID == 10 || WeaponID == 262154)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.FamasFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.FamasBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.FamasSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.FamasRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.FamasYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.FamasPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.FamasCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.FamasCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.FamasCurveX;
                }
                //AK47
                else if (WeaponID == 7 || WeaponID == 262151)
                {
                    if (Structs.LocalPlayer.AimPunch.X > -0.1 && Settings.Aimbot.FirstAK47)
                    {

                        Settings.Aimbot.AutoPistol = false;
                        Settings.Aimbot.Fov = Settings.Aimbot.FirstAK47Fov;
                        Settings.Aimbot.Bone = Settings.Aimbot.FirstAK47Bone;
                        Settings.Aimbot.Smooth = Settings.Aimbot.FirstAK47Smooth;
                        Settings.Aimbot.RecoilControl = Settings.Aimbot.AK47RecoilControl;
                        Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.AK47YawRecoilReductionFactory;
                        Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.AK47PitchRecoilReductionFactory;
                        Settings.Aimbot.Curve = Settings.Aimbot.AK47Curve;
                        Settings.Aimbot.CurveY = Settings.Aimbot.AK47CurveY;
                        Settings.Aimbot.CurveX = Settings.Aimbot.AK47CurveX;
                    }
                    else
                    {

                        Settings.Aimbot.AutoPistol = false;
                        Settings.Aimbot.Fov = Settings.Aimbot.AK47Fov;
                        Settings.Aimbot.Bone = Settings.Aimbot.AK47Bone;
                        Settings.Aimbot.Smooth = Settings.Aimbot.AK47Smooth;
                        Settings.Aimbot.RecoilControl = Settings.Aimbot.AK47RecoilControl;
                        Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.AK47YawRecoilReductionFactory;
                        Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.AK47PitchRecoilReductionFactory;
                        Settings.Aimbot.Curve = Settings.Aimbot.AK47Curve;
                        Settings.Aimbot.CurveY = Settings.Aimbot.AK47CurveY;
                        Settings.Aimbot.CurveX = Settings.Aimbot.AK47CurveX;
                    }
                }
                //M4A4
                else if (WeaponID == 16 || WeaponID == 262160)
                {
                    if (Structs.LocalPlayer.AimPunch.X > -0.1 && Settings.Aimbot.FirstM4A4)
                    {
                        Settings.Aimbot.Fov = Settings.Aimbot.FirstM4A4Fov;
                        Settings.Aimbot.Bone = Settings.Aimbot.FirstM4A4Bone;
                        Settings.Aimbot.Smooth = Settings.Aimbot.FirstM4A4Smooth;
                    }
                    else
                    {

                        Settings.Aimbot.AutoPistol = false;
                        Settings.Aimbot.Fov = Settings.Aimbot.M4A4Fov;
                        Settings.Aimbot.Bone = Settings.Aimbot.M4A4Bone;
                        Settings.Aimbot.Smooth = Settings.Aimbot.M4A4Smooth;
                        Settings.Aimbot.RecoilControl = Settings.Aimbot.M4A4RecoilControl;
                        Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.M4A4YawRecoilReductionFactory;
                        Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.M4A4PitchRecoilReductionFactory;
                        Settings.Aimbot.Curve = Settings.Aimbot.M4A4Curve;
                        Settings.Aimbot.CurveY = Settings.Aimbot.M4A4CurveY;
                        Settings.Aimbot.CurveX = Settings.Aimbot.M4A4CurveX;
                    }
                }
                //M4A1
                else if (WeaponID == 60 || WeaponID == 262204)
                {
                    if (Structs.LocalPlayer.AimPunch.X > -0.1 && Settings.Aimbot.FirstM4A1)
                    {
                        Settings.Aimbot.Fov = Settings.Aimbot.FirstM4A1Fov;
                        Settings.Aimbot.Bone = Settings.Aimbot.FirstM4A1Bone;
                        Settings.Aimbot.Smooth = Settings.Aimbot.FirstM4A1Smooth;
                    }
                    else
                    {

                        Settings.Aimbot.AutoPistol = false;
                        Settings.Aimbot.Fov = Settings.Aimbot.M4A1Fov;
                        Settings.Aimbot.Bone = Settings.Aimbot.M4A1Bone;
                        Settings.Aimbot.Smooth = Settings.Aimbot.M4A1Smooth;
                        Settings.Aimbot.RecoilControl = Settings.Aimbot.M4A1RecoilControl;
                        Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.M4A1YawRecoilReductionFactory;
                        Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.M4A1PitchRecoilReductionFactory;
                        Settings.Aimbot.Curve = Settings.Aimbot.M4A1Curve;
                        Settings.Aimbot.CurveY = Settings.Aimbot.M4A1CurveY;
                        Settings.Aimbot.CurveX = Settings.Aimbot.M4A1CurveX;
                    }
                }
                //SG
                else if (WeaponID == 39 || WeaponID == 262183)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.SGFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.SGBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.SGSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.SGRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.SGYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.SGPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.SGCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.SGCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.SGCurveX;
                }
                //AUG
                else if (WeaponID == 8 || WeaponID == 262152)
                {
                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.AUGFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.AUGBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.AUGSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.AUGRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.AUGYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.AUGPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.AUGCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.AUGCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.AUGCurveX;
                }
                //SNIPERS
                //SSG
                else if (WeaponID == 40 || WeaponID == 262184)
                {
                    if (Structs.LocalPlayer.AimPunch.X < -0.01)
                    {
                        Settings.Aimbot.Fov = 0;
                        Settings.Aimbot.Bone = 0;
                        Settings.Aimbot.Smooth = 100;
                    }
                    else
                    {
                        Settings.Aimbot.AutoPistol = false;
                        Settings.Aimbot.Fov = Settings.Aimbot.SSGFov;
                        Settings.Aimbot.Bone = Settings.Aimbot.SSGBone;
                        Settings.Aimbot.Smooth = Settings.Aimbot.SSGSmooth;
                        Settings.Aimbot.RecoilControl = Settings.Aimbot.SSGRecoilControl;
                        Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.SSGYawRecoilReductionFactory;
                        Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.SSGPitchRecoilReductionFactory;
                        Settings.Aimbot.Curve = Settings.Aimbot.SSGCurve;
                        Settings.Aimbot.CurveY = Settings.Aimbot.SSGCurveY;
                        Settings.Aimbot.CurveX = Settings.Aimbot.SSGCurveX;
                    }
                }
                //AWP
                else if (WeaponID == 9 || WeaponID == 262153)
                {
                    if (Structs.LocalPlayer.AimPunch.X < -0.3)
                    {
                        //Thread.Sleep(30);
                        Settings.Aimbot.Fov = 0;
                        Settings.Aimbot.Bone = 0;
                        Settings.Aimbot.Smooth = 100;
                    }
                    else
                    {

                        Settings.Aimbot.AutoPistol = false;
                        Settings.Aimbot.Fov = Settings.Aimbot.AWPFov;
                        Settings.Aimbot.Bone = Settings.Aimbot.AWPBone;
                        Settings.Aimbot.Smooth = Settings.Aimbot.AWPSmooth;
                        Settings.Aimbot.RecoilControl = Settings.Aimbot.AWPRecoilControl;
                        Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.AWPYawRecoilReductionFactory;
                        Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.AWPPitchRecoilReductionFactory;
                        Settings.Aimbot.Curve = Settings.Aimbot.AWPCurve;
                        Settings.Aimbot.CurveY = Settings.Aimbot.AWPCurveY;
                        Settings.Aimbot.CurveX = Settings.Aimbot.AWPCurveX;
                    }
                }
                //AUTOs
                else if (WeaponID == 11 || WeaponID == 262155 || WeaponID == 38 || WeaponID == 262182)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.AUTOFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.AUTOBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.AUTOSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.AUTORecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.AUTOYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.AUTOPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.AUTOCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.AUTOCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.AUTOCurveX;
                }
                //SHOTGUNS
                //NOVA
                else if (WeaponID == 35 || WeaponID == 262179)
                {
                    Settings.Aimbot.AutoPistol = Settings.Aimbot.NovaAutoPistol;
                    Settings.Aimbot.Fov = Settings.Aimbot.NovaFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.NovaBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.NovaSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.NovaRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.NovaYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.NovaPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.NovaCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.NovaCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.NovaCurveX;
                }
                //AUTOSHOTGUN
                else if (WeaponID == 25 || WeaponID == 262169)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.XMFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.XMBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.XMSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.XMRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.XMYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.XMPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.XMCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.XMCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.XMCurveX;
                }
                //Sawned-off
                else if (WeaponID == 29 || WeaponID == 262173)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.SawnedOffFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.SawnedOffBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.SawnedOffSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.SawnedOffRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.SawnedOffYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.SawnedOffPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.SawnedOffCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.SawnedOffCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.SawnedOffCurveX;
                }
                //MAG7
                else if (WeaponID == 27 || WeaponID == 262171)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.MAG7Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.MAG7Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.MAG7Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.MAG7RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.MAG7YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.MAG7PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.MAG7Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.MAG7CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.MAG7CurveX;
                }
                //MACHINEGUNS
                //M249
                else if (WeaponID == 14 || WeaponID == 262158)
                {

                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.M249Fov;
                    Settings.Aimbot.Bone = Settings.Aimbot.M249Bone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.M249Smooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.M249RecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.M249YawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.M249PitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.M249Curve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.M249CurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.M249CurveX;
                }
                //Negev
                else if (WeaponID == 28 || WeaponID == 262172)
                {
                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Fov = Settings.Aimbot.NegevFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.NegevBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.NegevSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.NegevRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.NegevYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.NegevPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.NegevCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.NegevCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.NegevCurveX;
                }
                //Nades
                else if (WeaponID == 45 || WeaponID == 44 || WeaponID == 43 || WeaponID == 47 || WeaponID == 48 || WeaponID == 49 || WeaponID == 46 || WeaponID == 262186)
                {
                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Smooth = 10000f;
                }
                else if (WeaponID == 42 || WeaponID == 59 || WeaponID == 262667 || WeaponID == 262666 || WeaponID == 262633 || WeaponID == 262651 || WeaponID == 262664)
                {
                    Settings.Aimbot.AutoPistol = false;
                    Settings.Aimbot.Smooth = 10000f;
                }
                else
                {
                    Settings.Aimbot.AutoPistol = Settings.Aimbot.UnknownAutoPistol;
                    Settings.Aimbot.Fov = Settings.Aimbot.UnknownFov;
                    Settings.Aimbot.Bone = Settings.Aimbot.UnknownBone;
                    Settings.Aimbot.Smooth = Settings.Aimbot.UnknownSmooth;
                    Settings.Aimbot.RecoilControl = Settings.Aimbot.UnknownRecoilControl;
                    Settings.Aimbot.YawRecoilReductionFactory = Settings.Aimbot.UnknownYawRecoilReductionFactory;
                    Settings.Aimbot.PitchRecoilReductionFactory = Settings.Aimbot.UnknownPitchRecoilReductionFactory;
                    Settings.Aimbot.Curve = Settings.Aimbot.UnknownCurve;
                    Settings.Aimbot.CurveY = Settings.Aimbot.UnknownCurveY;
                    Settings.Aimbot.CurveX = Settings.Aimbot.UnknownCurveX;
                }
            }
        }
    }
}
