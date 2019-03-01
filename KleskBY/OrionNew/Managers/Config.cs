using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using IniParser;
using IniParser.Model;
using System.Windows.Forms;

using Orion.Other;

namespace Orion.Managers
{
    internal class Config
    {

        public static bool IsFileUsable(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)) 
            {
                return fileStream.CanWrite ? true : false;
            }
        }

        public static void Load(string path)
        {
            if (!IsFileUsable(path)) 
            {
                MessageBox.Show("Config File is not writeable");
                return;
            }

            FileIniDataParser configParser = new FileIniDataParser();
            IniData configData = configParser.ReadFile(path);

            try 
            {
                Settings.Aimbot.Enabled = bool.Parse(configData["Aimbot"]["Enabled"]);
                Settings.Aimbot.Key = int.Parse(configData["Aimbot"]["Key"]);
                Settings.Aimbot.SecondKey = int.Parse(configData["Aimbot"]["AutoDelayKey"]);
                Settings.Aimbot.VisibleOnly = bool.Parse(configData["Aimbot"]["VisibleOnly"]);
                Settings.Aimbot.bSpottedCheck = bool.Parse(configData["Aimbot"]["bSpottedCheck"]);
                Settings.Aimbot.KillDelay = bool.Parse(configData["Aimbot"]["KillDelay"]);
                Settings.Aimbot.UseMouseEvent = bool.Parse(configData["Aimbot"]["UseMouseEvent"]);
                Settings.Aimbot.MouseEventFov = int.Parse(configData["Aimbot"]["MouseEventFov"]);
                Settings.Aimbot.MouseEventSmooth = int.Parse(configData["Aimbot"]["MouseEventSmooth"]);

                Settings.Aimbot.Fov = float.Parse(configData["Aimbot"]["Fov"]);
                Settings.Aimbot.Bone = int.Parse(configData["Aimbot"]["Bone"]);
                Settings.Aimbot.Smooth = float.Parse(configData["Aimbot"]["Smooth"]);
                Settings.Aimbot.RecoilControl = bool.Parse(configData["Aimbot"]["RecoilControl"]);
                Settings.Aimbot.YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["RcsX"]);
                Settings.Aimbot.PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["RcsY"]);
                Settings.Aimbot.Curve = bool.Parse(configData["Aimbot"]["Curve"]);
                Settings.Aimbot.CurveX = float.Parse(configData["Aimbot"]["CurveX"]);
                Settings.Aimbot.CurveY = float.Parse(configData["Aimbot"]["CurveY"]);
                //Glock
                Settings.Aimbot.GlockAutoPistol = bool.Parse(configData["Aimbot"]["GlockAutoPistol"]); ;
                Settings.Aimbot.GlockFov = float.Parse(configData["Aimbot"]["GlockFov"]);
                Settings.Aimbot.GlockBone = int.Parse(configData["Aimbot"]["GlockBone"]);
                Settings.Aimbot.GlockSmooth = float.Parse(configData["Aimbot"]["GlockSmooth"]);
                Settings.Aimbot.GlockRecoilControl = bool.Parse(configData["Aimbot"]["GlockRecoilControl"]);
                Settings.Aimbot.GlockYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["GlockRcsX"]);
                Settings.Aimbot.GlockPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["GlockRcsY"]);
                Settings.Aimbot.GlockCurve = bool.Parse(configData["Aimbot"]["GlockCurve"]);
                Settings.Aimbot.GlockCurveY = float.Parse(configData["Aimbot"]["GlockCurveX"]);
                Settings.Aimbot.GlockCurveX = float.Parse(configData["Aimbot"]["GlockCurveY"]);
                //USP
                Settings.Aimbot.USPAutoPistol = bool.Parse(configData["Aimbot"]["USPAutoPistol"]); ;
                Settings.Aimbot.USPFov = float.Parse(configData["Aimbot"]["USPFov"]);
                Settings.Aimbot.USPBone = int.Parse(configData["Aimbot"]["USPBone"]);
                Settings.Aimbot.USPSmooth = float.Parse(configData["Aimbot"]["USPSmooth"]);
                Settings.Aimbot.USPRecoilControl = bool.Parse(configData["Aimbot"]["USPRecoilControl"]);
                Settings.Aimbot.USPYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["USPRcsX"]);
                Settings.Aimbot.USPPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["USPRcsY"]);
                Settings.Aimbot.USPCurve = bool.Parse(configData["Aimbot"]["USPCurve"]);
                Settings.Aimbot.USPCurveY = float.Parse(configData["Aimbot"]["USPCurveX"]);
                Settings.Aimbot.USPCurveX = float.Parse(configData["Aimbot"]["USPCurveY"]);
                //P2000
                Settings.Aimbot.P2000AutoPistol = bool.Parse(configData["Aimbot"]["P2000AutoPistol"]); ;
                Settings.Aimbot.P2000Fov = float.Parse(configData["Aimbot"]["P2000Fov"]);
                Settings.Aimbot.P2000Bone = int.Parse(configData["Aimbot"]["P2000Bone"]);
                Settings.Aimbot.P2000Smooth = float.Parse(configData["Aimbot"]["P2000Smooth"]);
                Settings.Aimbot.P2000RecoilControl = bool.Parse(configData["Aimbot"]["P2000RecoilControl"]);
                Settings.Aimbot.P2000YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["P2000RcsX"]);
                Settings.Aimbot.P2000PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["P2000RcsY"]);
                Settings.Aimbot.P2000Curve = bool.Parse(configData["Aimbot"]["P2000Curve"]);
                Settings.Aimbot.P2000CurveY = float.Parse(configData["Aimbot"]["P2000CurveX"]);
                Settings.Aimbot.P2000CurveX = float.Parse(configData["Aimbot"]["P2000CurveY"]);
                //DUALS
                Settings.Aimbot.DualsAutoPistol = bool.Parse(configData["Aimbot"]["DualsAutoPistol"]); ;
                Settings.Aimbot.DualsFov = float.Parse(configData["Aimbot"]["DualsFov"]);
                Settings.Aimbot.DualsBone = int.Parse(configData["Aimbot"]["DualsBone"]);
                Settings.Aimbot.DualsSmooth = float.Parse(configData["Aimbot"]["DualsSmooth"]);
                Settings.Aimbot.DualsRecoilControl = bool.Parse(configData["Aimbot"]["DualsRecoilControl"]);
                Settings.Aimbot.DualsYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["DualsRcsX"]);
                Settings.Aimbot.DualsPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["DualsRcsY"]);
                Settings.Aimbot.DualsCurve = bool.Parse(configData["Aimbot"]["DualsCurve"]);
                Settings.Aimbot.DualsCurveY = float.Parse(configData["Aimbot"]["DualsCurveX"]);
                Settings.Aimbot.DualsCurveX = float.Parse(configData["Aimbot"]["DualsCurveY"]);
                //P250
                Settings.Aimbot.P250AutoPistol = bool.Parse(configData["Aimbot"]["P250AutoPistol"]); ;
                Settings.Aimbot.P250Fov = float.Parse(configData["Aimbot"]["P250Fov"]);
                Settings.Aimbot.P250Bone = int.Parse(configData["Aimbot"]["P250Bone"]);
                Settings.Aimbot.P250Smooth = float.Parse(configData["Aimbot"]["P250Smooth"]);
                Settings.Aimbot.P250RecoilControl = bool.Parse(configData["Aimbot"]["P250RecoilControl"]);
                Settings.Aimbot.P250YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["P250RcsX"]);
                Settings.Aimbot.P250PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["P250RcsY"]);
                Settings.Aimbot.P250Curve = bool.Parse(configData["Aimbot"]["P250Curve"]);
                Settings.Aimbot.P250CurveY = float.Parse(configData["Aimbot"]["P250CurveX"]);
                Settings.Aimbot.P250CurveX = float.Parse(configData["Aimbot"]["P250CurveY"]);
                //FiveSeven
                Settings.Aimbot.FiveSevenAutoPistol = bool.Parse(configData["Aimbot"]["FiveSevenAutoPistol"]); ;
                Settings.Aimbot.FiveSevenFov = float.Parse(configData["Aimbot"]["FiveSevenFov"]);
                Settings.Aimbot.FiveSevenBone = int.Parse(configData["Aimbot"]["FiveSevenBone"]);
                Settings.Aimbot.FiveSevenSmooth = float.Parse(configData["Aimbot"]["FiveSevenSmooth"]);
                Settings.Aimbot.FiveSevenRecoilControl = bool.Parse(configData["Aimbot"]["FiveSevenRecoilControl"]);
                Settings.Aimbot.FiveSevenYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["FiveSevenRcsX"]);
                Settings.Aimbot.FiveSevenPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["FiveSevenRcsY"]);
                Settings.Aimbot.FiveSevenCurve = bool.Parse(configData["Aimbot"]["FiveSevenCurve"]);
                Settings.Aimbot.FiveSevenCurveY = float.Parse(configData["Aimbot"]["FiveSevenCurveX"]);
                Settings.Aimbot.FiveSevenCurveX = float.Parse(configData["Aimbot"]["FiveSevenCurveY"]);
                //TEC9
                Settings.Aimbot.TEC9AutoPistol = bool.Parse(configData["Aimbot"]["TEC9AutoPistol"]); ;
                Settings.Aimbot.TEC9Fov = float.Parse(configData["Aimbot"]["TEC9Fov"]);
                Settings.Aimbot.TEC9Bone = int.Parse(configData["Aimbot"]["TEC9Bone"]);
                Settings.Aimbot.TEC9Smooth = float.Parse(configData["Aimbot"]["TEC9Smooth"]);
                Settings.Aimbot.TEC9RecoilControl = bool.Parse(configData["Aimbot"]["TEC9RecoilControl"]);
                Settings.Aimbot.TEC9YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["TEC9RcsX"]);
                Settings.Aimbot.TEC9PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["TEC9RcsY"]);
                Settings.Aimbot.TEC9Curve = bool.Parse(configData["Aimbot"]["TEC9Curve"]);
                Settings.Aimbot.TEC9CurveY = float.Parse(configData["Aimbot"]["TEC9CurveX"]);
                Settings.Aimbot.TEC9CurveX = float.Parse(configData["Aimbot"]["TEC9CurveY"]);
                //DEAGLE
                Settings.Aimbot.DEAGLEAutoPistol = bool.Parse(configData["Aimbot"]["DEAGLEAutoPistol"]); ;
                Settings.Aimbot.DEAGLEFov = float.Parse(configData["Aimbot"]["DEAGLEFov"]);
                Settings.Aimbot.DEAGLEBone = int.Parse(configData["Aimbot"]["DEAGLEBone"]);
                Settings.Aimbot.DEAGLESmooth = float.Parse(configData["Aimbot"]["DEAGLESmooth"]);
                Settings.Aimbot.DEAGLERecoilControl = bool.Parse(configData["Aimbot"]["DEAGLERecoilControl"]);
                Settings.Aimbot.DEAGLEYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["DEAGLERcsX"]);
                Settings.Aimbot.DEAGLEPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["DEAGLERcsY"]);
                Settings.Aimbot.DEAGLECurve = bool.Parse(configData["Aimbot"]["DEAGLECurve"]);
                Settings.Aimbot.DEAGLECurveY = float.Parse(configData["Aimbot"]["DEAGLECurveX"]);
                Settings.Aimbot.DEAGLECurveX = float.Parse(configData["Aimbot"]["DEAGLECurveY"]);
                //Revolver
                Settings.Aimbot.RevolverFov = float.Parse(configData["Aimbot"]["RevolverFov"]);
                Settings.Aimbot.RevolverBone = int.Parse(configData["Aimbot"]["RevolverBone"]);
                Settings.Aimbot.RevolverSmooth = float.Parse(configData["Aimbot"]["RevolverSmooth"]);
                Settings.Aimbot.RevolverRecoilControl = bool.Parse(configData["Aimbot"]["RevolverRecoilControl"]);
                Settings.Aimbot.RevolverYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["RevolverRcsX"]);
                Settings.Aimbot.RevolverPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["RevolverRcsY"]);
                Settings.Aimbot.RevolverCurve = bool.Parse(configData["Aimbot"]["RevolverCurve"]);
                Settings.Aimbot.RevolverCurveY = float.Parse(configData["Aimbot"]["RevolverCurveX"]);
                Settings.Aimbot.RevolverCurveX = float.Parse(configData["Aimbot"]["RevolverCurveY"]);
                //CZ
                Settings.Aimbot.CZFov = float.Parse(configData["Aimbot"]["CZFov"]);
                Settings.Aimbot.CZBone = int.Parse(configData["Aimbot"]["CZBone"]);
                Settings.Aimbot.CZSmooth = float.Parse(configData["Aimbot"]["CZSmooth"]);
                Settings.Aimbot.CZRecoilControl = bool.Parse(configData["Aimbot"]["CZRecoilControl"]);
                Settings.Aimbot.CZYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["CZRcsX"]);
                Settings.Aimbot.CZPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["CZRcsY"]);
                Settings.Aimbot.CZCurve = bool.Parse(configData["Aimbot"]["CZCurve"]);
                Settings.Aimbot.CZCurveY = float.Parse(configData["Aimbot"]["CZCurveX"]);
                Settings.Aimbot.CZCurveX = float.Parse(configData["Aimbot"]["CZCurveY"]);
                //
                //SMGs
                //
                //MAC10
                Settings.Aimbot.MAC10Fov = float.Parse(configData["Aimbot"]["MAC10Fov"]);
                Settings.Aimbot.MAC10Bone = int.Parse(configData["Aimbot"]["MAC10Bone"]);
                Settings.Aimbot.MAC10Smooth = float.Parse(configData["Aimbot"]["MAC10Smooth"]);
                Settings.Aimbot.MAC10RecoilControl = bool.Parse(configData["Aimbot"]["MAC10RecoilControl"]);
                Settings.Aimbot.MAC10YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["MAC10RcsX"]);
                Settings.Aimbot.MAC10PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["MAC10RcsY"]);
                Settings.Aimbot.MAC10Curve = bool.Parse(configData["Aimbot"]["MAC10Curve"]);
                Settings.Aimbot.MAC10CurveY = float.Parse(configData["Aimbot"]["MAC10CurveX"]);
                Settings.Aimbot.MAC10CurveX = float.Parse(configData["Aimbot"]["MAC10CurveY"]);
                //MP9
                Settings.Aimbot.MP9Fov = float.Parse(configData["Aimbot"]["MP9Fov"]);
                Settings.Aimbot.MP9Bone = int.Parse(configData["Aimbot"]["MP9Bone"]);
                Settings.Aimbot.MP9Smooth = float.Parse(configData["Aimbot"]["MP9Smooth"]);
                Settings.Aimbot.MP9RecoilControl = bool.Parse(configData["Aimbot"]["MP9RecoilControl"]);
                Settings.Aimbot.MP9YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["MP9RcsX"]);
                Settings.Aimbot.MP9PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["MP9RcsY"]);
                Settings.Aimbot.MP9Curve = bool.Parse(configData["Aimbot"]["MP9Curve"]);
                Settings.Aimbot.MP9CurveY = float.Parse(configData["Aimbot"]["MP9CurveX"]);
                Settings.Aimbot.MP9CurveX = float.Parse(configData["Aimbot"]["MP9CurveY"]);
                //MP7
                Settings.Aimbot.MP7Fov = float.Parse(configData["Aimbot"]["MP7Fov"]);
                Settings.Aimbot.MP7Bone = int.Parse(configData["Aimbot"]["MP7Bone"]);
                Settings.Aimbot.MP7Smooth = float.Parse(configData["Aimbot"]["MP7Smooth"]);
                Settings.Aimbot.MP7RecoilControl = bool.Parse(configData["Aimbot"]["MP7RecoilControl"]);
                Settings.Aimbot.MP7YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["MP7RcsX"]);
                Settings.Aimbot.MP7PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["MP7RcsY"]);
                Settings.Aimbot.MP7Curve = bool.Parse(configData["Aimbot"]["MP7Curve"]);
                Settings.Aimbot.MP7CurveY = float.Parse(configData["Aimbot"]["MP7CurveX"]);
                Settings.Aimbot.MP7CurveX = float.Parse(configData["Aimbot"]["MP7CurveY"]);
                //MP5
                Settings.Aimbot.MP5Fov = float.Parse(configData["Aimbot"]["MP5Fov"]);
                Settings.Aimbot.MP5Bone = int.Parse(configData["Aimbot"]["MP5Bone"]);
                Settings.Aimbot.MP5Smooth = float.Parse(configData["Aimbot"]["MP5Smooth"]);
                Settings.Aimbot.MP5RecoilControl = bool.Parse(configData["Aimbot"]["MP5RecoilControl"]);
                Settings.Aimbot.MP5YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["MP5RcsX"]);
                Settings.Aimbot.MP5PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["MP5RcsY"]);
                Settings.Aimbot.MP5Curve = bool.Parse(configData["Aimbot"]["MP5Curve"]);
                Settings.Aimbot.MP5CurveY = float.Parse(configData["Aimbot"]["MP5CurveX"]);
                Settings.Aimbot.MP5CurveX = float.Parse(configData["Aimbot"]["MP5CurveY"]);
                //UMP
                Settings.Aimbot.UMPFov = float.Parse(configData["Aimbot"]["UMPFov"]);
                Settings.Aimbot.UMPBone = int.Parse(configData["Aimbot"]["UMPBone"]);
                Settings.Aimbot.UMPSmooth = float.Parse(configData["Aimbot"]["UMPSmooth"]);
                Settings.Aimbot.UMPRecoilControl = bool.Parse(configData["Aimbot"]["UMPRecoilControl"]);
                Settings.Aimbot.UMPYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["UMPRcsX"]);
                Settings.Aimbot.UMPPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["UMPRcsY"]);
                Settings.Aimbot.UMPCurve = bool.Parse(configData["Aimbot"]["UMPCurve"]);
                Settings.Aimbot.UMPCurveY = float.Parse(configData["Aimbot"]["UMPCurveX"]);
                Settings.Aimbot.UMPCurveX = float.Parse(configData["Aimbot"]["UMPCurveY"]);
                //Bizon
                Settings.Aimbot.BizonFov = float.Parse(configData["Aimbot"]["BizonFov"]);
                Settings.Aimbot.BizonBone = int.Parse(configData["Aimbot"]["BizonBone"]);
                Settings.Aimbot.BizonSmooth = float.Parse(configData["Aimbot"]["BizonSmooth"]);
                Settings.Aimbot.BizonRecoilControl = bool.Parse(configData["Aimbot"]["BizonRecoilControl"]);
                Settings.Aimbot.BizonYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["BizonRcsX"]);
                Settings.Aimbot.BizonPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["BizonRcsY"]);
                Settings.Aimbot.BizonCurve = bool.Parse(configData["Aimbot"]["BizonCurve"]);
                Settings.Aimbot.BizonCurveY = float.Parse(configData["Aimbot"]["BizonCurveX"]);
                Settings.Aimbot.BizonCurveX = float.Parse(configData["Aimbot"]["BizonCurveY"]);
                //P90
                Settings.Aimbot.P90Fov = float.Parse(configData["Aimbot"]["P90Fov"]);
                Settings.Aimbot.P90Bone = int.Parse(configData["Aimbot"]["P90Bone"]);
                Settings.Aimbot.P90Smooth = float.Parse(configData["Aimbot"]["P90Smooth"]);
                Settings.Aimbot.P90RecoilControl = bool.Parse(configData["Aimbot"]["P90RecoilControl"]);
                Settings.Aimbot.P90YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["P90RcsX"]);
                Settings.Aimbot.P90PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["P90RcsY"]);
                Settings.Aimbot.P90Curve = bool.Parse(configData["Aimbot"]["P90Curve"]);
                Settings.Aimbot.P90CurveY = float.Parse(configData["Aimbot"]["P90CurveX"]);
                Settings.Aimbot.P90CurveX = float.Parse(configData["Aimbot"]["P90CurveY"]);
                //
                //RIFLES
                //
                //Galil
                Settings.Aimbot.GalilFov = float.Parse(configData["Aimbot"]["GalilFov"]);
                Settings.Aimbot.GalilBone = int.Parse(configData["Aimbot"]["GalilBone"]);
                Settings.Aimbot.GalilSmooth = float.Parse(configData["Aimbot"]["GalilSmooth"]);
                Settings.Aimbot.GalilRecoilControl = bool.Parse(configData["Aimbot"]["GalilRecoilControl"]);
                Settings.Aimbot.GalilYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["GalilRcsX"]);
                Settings.Aimbot.GalilPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["GalilRcsY"]);
                Settings.Aimbot.GalilCurve = bool.Parse(configData["Aimbot"]["GalilCurve"]);
                Settings.Aimbot.GalilCurveY = float.Parse(configData["Aimbot"]["GalilCurveX"]);
                Settings.Aimbot.GalilCurveX = float.Parse(configData["Aimbot"]["GalilCurveY"]);
                //Famas
                Settings.Aimbot.FamasFov = float.Parse(configData["Aimbot"]["FamasFov"]);
                Settings.Aimbot.FamasBone = int.Parse(configData["Aimbot"]["FamasBone"]);
                Settings.Aimbot.FamasSmooth = float.Parse(configData["Aimbot"]["FamasSmooth"]);
                Settings.Aimbot.FamasRecoilControl = bool.Parse(configData["Aimbot"]["FamasRecoilControl"]);
                Settings.Aimbot.FamasYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["FamasRcsX"]);
                Settings.Aimbot.FamasPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["FamasRcsY"]);
                Settings.Aimbot.FamasCurve = bool.Parse(configData["Aimbot"]["FamasCurve"]);
                Settings.Aimbot.FamasCurveY = float.Parse(configData["Aimbot"]["FamasCurveX"]);
                Settings.Aimbot.FamasCurveX = float.Parse(configData["Aimbot"]["FamasCurveY"]);
                //AK47
                Settings.Aimbot.AK47Fov = float.Parse(configData["Aimbot"]["AK47Fov"]);
                Settings.Aimbot.AK47Bone = int.Parse(configData["Aimbot"]["AK47Bone"]);
                Settings.Aimbot.AK47Smooth = float.Parse(configData["Aimbot"]["AK47Smooth"]);
                Settings.Aimbot.AK47RecoilControl = bool.Parse(configData["Aimbot"]["AK47RecoilControl"]);
                Settings.Aimbot.AK47YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["AK47RcsX"]);
                Settings.Aimbot.AK47PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["AK47RcsY"]);
                Settings.Aimbot.AK47Curve = bool.Parse(configData["Aimbot"]["AK47Curve"]);
                Settings.Aimbot.AK47CurveY = float.Parse(configData["Aimbot"]["AK47CurveX"]);
                Settings.Aimbot.AK47CurveX = float.Parse(configData["Aimbot"]["AK47CurveY"]);
                Settings.Aimbot.FirstAK47 = bool.Parse(configData["Aimbot"]["FirstBulletAK47"]);
                Settings.Aimbot.FirstAK47Fov = float.Parse(configData["Aimbot"]["FirstBulletAK47Fov"]);
                Settings.Aimbot.FirstAK47Bone = int.Parse(configData["Aimbot"]["FirstBulletAK47Bone"]);
                Settings.Aimbot.FirstAK47Smooth = float.Parse(configData["Aimbot"]["FirstBulletAK47Smooth"]);
                //M4A4
                Settings.Aimbot.M4A4Fov = float.Parse(configData["Aimbot"]["M4A4Fov"]);
                Settings.Aimbot.M4A4Bone = int.Parse(configData["Aimbot"]["M4A4Bone"]);
                Settings.Aimbot.M4A4Smooth = float.Parse(configData["Aimbot"]["M4A4Smooth"]);
                Settings.Aimbot.M4A4RecoilControl = bool.Parse(configData["Aimbot"]["M4A4RecoilControl"]);
                Settings.Aimbot.M4A4YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["M4A4RcsX"]);
                Settings.Aimbot.M4A4PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["M4A4RcsY"]);
                Settings.Aimbot.M4A4Curve = bool.Parse(configData["Aimbot"]["M4A4Curve"]);
                Settings.Aimbot.M4A4CurveY = float.Parse(configData["Aimbot"]["M4A4CurveX"]);
                Settings.Aimbot.M4A4CurveX = float.Parse(configData["Aimbot"]["M4A4CurveY"]);
                Settings.Aimbot.FirstM4A4 = bool.Parse(configData["Aimbot"]["FirstBulletM4A4"]);
                Settings.Aimbot.FirstM4A4Fov = float.Parse(configData["Aimbot"]["FirstBulletM4A4Fov"]);
                Settings.Aimbot.FirstM4A4Bone = int.Parse(configData["Aimbot"]["FirstBulletM4A4Bone"]);
                Settings.Aimbot.FirstM4A4Smooth = float.Parse(configData["Aimbot"]["FirstBulletM4A4Smooth"]);
                //M4A1
                Settings.Aimbot.M4A1Fov = float.Parse(configData["Aimbot"]["M4A1Fov"]);
                Settings.Aimbot.M4A1Bone = int.Parse(configData["Aimbot"]["M4A1Bone"]);
                Settings.Aimbot.M4A1Smooth = float.Parse(configData["Aimbot"]["M4A1Smooth"]);
                Settings.Aimbot.M4A1RecoilControl = bool.Parse(configData["Aimbot"]["M4A1RecoilControl"]);
                Settings.Aimbot.M4A1YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["M4A1RcsX"]);
                Settings.Aimbot.M4A1PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["M4A1RcsY"]);
                Settings.Aimbot.M4A1Curve = bool.Parse(configData["Aimbot"]["M4A1Curve"]);
                Settings.Aimbot.M4A1CurveY = float.Parse(configData["Aimbot"]["M4A1CurveX"]);
                Settings.Aimbot.M4A1CurveX = float.Parse(configData["Aimbot"]["M4A1CurveY"]);
                Settings.Aimbot.FirstM4A1 = bool.Parse(configData["Aimbot"]["FirstBulletM4A1"]);
                Settings.Aimbot.FirstM4A1Fov = float.Parse(configData["Aimbot"]["FirstBulletM4A1Fov"]);
                Settings.Aimbot.FirstM4A1Bone = int.Parse(configData["Aimbot"]["FirstBulletM4A1Bone"]);
                Settings.Aimbot.FirstM4A1Smooth = float.Parse(configData["Aimbot"]["FirstBulletM4A1Smooth"]);
                //SG
                Settings.Aimbot.SGFov = float.Parse(configData["Aimbot"]["SGFov"]);
                Settings.Aimbot.SGBone = int.Parse(configData["Aimbot"]["SGBone"]);
                Settings.Aimbot.SGSmooth = float.Parse(configData["Aimbot"]["SGSmooth"]);
                Settings.Aimbot.SGRecoilControl = bool.Parse(configData["Aimbot"]["SGRecoilControl"]);
                Settings.Aimbot.SGYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["SGRcsX"]);
                Settings.Aimbot.SGPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["SGRcsY"]);
                Settings.Aimbot.SGCurve = bool.Parse(configData["Aimbot"]["SGCurve"]);
                Settings.Aimbot.SGCurveY = float.Parse(configData["Aimbot"]["SGCurveX"]);
                Settings.Aimbot.SGCurveX = float.Parse(configData["Aimbot"]["SGCurveY"]);
                //AUG
                Settings.Aimbot.AUGFov = float.Parse(configData["Aimbot"]["AUGFov"]);
                Settings.Aimbot.AUGBone = int.Parse(configData["Aimbot"]["AUGBone"]);
                Settings.Aimbot.AUGSmooth = float.Parse(configData["Aimbot"]["AUGSmooth"]);
                Settings.Aimbot.AUGRecoilControl = bool.Parse(configData["Aimbot"]["AUGRecoilControl"]);
                Settings.Aimbot.AUGYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["AUGRcsX"]);
                Settings.Aimbot.AUGPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["AUGRcsY"]);
                Settings.Aimbot.AUGCurve = bool.Parse(configData["Aimbot"]["AUGCurve"]);
                Settings.Aimbot.AUGCurveY = float.Parse(configData["Aimbot"]["AUGCurveX"]);
                Settings.Aimbot.AUGCurveX = float.Parse(configData["Aimbot"]["AUGCurveY"]);
                //
                // SNIPERS
                //
                //SSG
                Settings.Aimbot.SSGFov = float.Parse(configData["Aimbot"]["SSGFov"]);
                Settings.Aimbot.SSGBone = int.Parse(configData["Aimbot"]["SSGBone"]);
                Settings.Aimbot.SSGSmooth = float.Parse(configData["Aimbot"]["SSGSmooth"]);
                Settings.Aimbot.SSGRecoilControl = bool.Parse(configData["Aimbot"]["SSGRecoilControl"]);
                Settings.Aimbot.SSGYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["SSGRcsX"]);
                Settings.Aimbot.SSGPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["SSGRcsY"]);
                Settings.Aimbot.SSGCurve = bool.Parse(configData["Aimbot"]["SSGCurve"]);
                Settings.Aimbot.SSGCurveY = float.Parse(configData["Aimbot"]["SSGCurveX"]);
                Settings.Aimbot.SSGCurveX = float.Parse(configData["Aimbot"]["SSGCurveY"]);
                //AWP
                Settings.Aimbot.AWPFov = float.Parse(configData["Aimbot"]["AWPFov"]);
                Settings.Aimbot.AWPBone = int.Parse(configData["Aimbot"]["AWPBone"]);
                Settings.Aimbot.AWPSmooth = float.Parse(configData["Aimbot"]["AWPSmooth"]);
                Settings.Aimbot.AWPRecoilControl = bool.Parse(configData["Aimbot"]["AWPRecoilControl"]);
                Settings.Aimbot.AWPYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["AWPRcsX"]);
                Settings.Aimbot.AWPPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["AWPRcsY"]);
                Settings.Aimbot.AWPCurve = bool.Parse(configData["Aimbot"]["AWPCurve"]);
                Settings.Aimbot.AWPCurveY = float.Parse(configData["Aimbot"]["AWPCurveX"]);
                Settings.Aimbot.AWPCurveX = float.Parse(configData["Aimbot"]["AWPCurveY"]);
                //AUTO
                Settings.Aimbot.AUTOFov = float.Parse(configData["Aimbot"]["AUTOFov"]);
                Settings.Aimbot.AUTOBone = int.Parse(configData["Aimbot"]["AUTOBone"]);
                Settings.Aimbot.AUTOSmooth = float.Parse(configData["Aimbot"]["AUTOSmooth"]);
                Settings.Aimbot.AUTORecoilControl = bool.Parse(configData["Aimbot"]["AUTORecoilControl"]);
                Settings.Aimbot.AUTOYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["AUTORcsX"]);
                Settings.Aimbot.AUTOPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["AUTORcsY"]);
                Settings.Aimbot.AUTOCurve = bool.Parse(configData["Aimbot"]["AUTOCurve"]);
                Settings.Aimbot.AUTOCurveY = float.Parse(configData["Aimbot"]["AUTOCurveX"]);
                Settings.Aimbot.AUTOCurveX = float.Parse(configData["Aimbot"]["AUTOCurveY"]);
                //
                // SHOTGUN
                //
                //Nova
                Settings.Aimbot.NovaAutoPistol = bool.Parse(configData["Aimbot"]["NovaAutoPistol"]);
                Settings.Aimbot.NovaFov = float.Parse(configData["Aimbot"]["NovaFov"]);
                Settings.Aimbot.NovaBone = int.Parse(configData["Aimbot"]["NovaBone"]);
                Settings.Aimbot.NovaSmooth = float.Parse(configData["Aimbot"]["NovaSmooth"]);
                Settings.Aimbot.NovaRecoilControl = bool.Parse(configData["Aimbot"]["NovaRecoilControl"]);
                Settings.Aimbot.NovaYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["NovaRcsX"]);
                Settings.Aimbot.NovaPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["NovaRcsY"]);
                Settings.Aimbot.NovaCurve = bool.Parse(configData["Aimbot"]["NovaCurve"]);
                Settings.Aimbot.NovaCurveY = float.Parse(configData["Aimbot"]["NovaCurveX"]);
                Settings.Aimbot.NovaCurveX = float.Parse(configData["Aimbot"]["NovaCurveY"]);
                //XM
                Settings.Aimbot.XMFov = float.Parse(configData["Aimbot"]["XMFov"]);
                Settings.Aimbot.XMBone = int.Parse(configData["Aimbot"]["XMBone"]);
                Settings.Aimbot.XMSmooth = float.Parse(configData["Aimbot"]["XMSmooth"]);
                Settings.Aimbot.XMRecoilControl = bool.Parse(configData["Aimbot"]["XMRecoilControl"]);
                Settings.Aimbot.XMYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["XMRcsX"]);
                Settings.Aimbot.XMPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["XMRcsY"]);
                Settings.Aimbot.XMCurve = bool.Parse(configData["Aimbot"]["XMCurve"]);
                Settings.Aimbot.XMCurveY = float.Parse(configData["Aimbot"]["XMCurveX"]);
                Settings.Aimbot.XMCurveX = float.Parse(configData["Aimbot"]["XMCurveY"]);
                //SawnedOff
                Settings.Aimbot.SawnedOffFov = float.Parse(configData["Aimbot"]["SawnedOffFov"]);
                Settings.Aimbot.SawnedOffBone = int.Parse(configData["Aimbot"]["SawnedOffBone"]);
                Settings.Aimbot.SawnedOffSmooth = float.Parse(configData["Aimbot"]["SawnedOffSmooth"]);
                Settings.Aimbot.SawnedOffRecoilControl = bool.Parse(configData["Aimbot"]["SawnedOffRecoilControl"]);
                Settings.Aimbot.SawnedOffYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["SawnedOffRcsX"]);
                Settings.Aimbot.SawnedOffPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["SawnedOffRcsY"]);
                Settings.Aimbot.SawnedOffCurve = bool.Parse(configData["Aimbot"]["SawnedOffCurve"]);
                Settings.Aimbot.SawnedOffCurveY = float.Parse(configData["Aimbot"]["SawnedOffCurveX"]);
                Settings.Aimbot.SawnedOffCurveX = float.Parse(configData["Aimbot"]["SawnedOffCurveY"]);
                //MAG7
                Settings.Aimbot.MAG7Fov = float.Parse(configData["Aimbot"]["MAG7Fov"]);
                Settings.Aimbot.MAG7Bone = int.Parse(configData["Aimbot"]["MAG7Bone"]);
                Settings.Aimbot.MAG7Smooth = float.Parse(configData["Aimbot"]["MAG7Smooth"]);
                Settings.Aimbot.MAG7RecoilControl = bool.Parse(configData["Aimbot"]["MAG7RecoilControl"]);
                Settings.Aimbot.MAG7YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["MAG7RcsX"]);
                Settings.Aimbot.MAG7PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["MAG7RcsY"]);
                Settings.Aimbot.MAG7Curve = bool.Parse(configData["Aimbot"]["MAG7Curve"]);
                Settings.Aimbot.MAG7CurveY = float.Parse(configData["Aimbot"]["MAG7CurveX"]);
                Settings.Aimbot.MAG7CurveX = float.Parse(configData["Aimbot"]["MAG7CurveY"]);
                //M249
                Settings.Aimbot.M249Fov = float.Parse(configData["Aimbot"]["M249Fov"]);
                Settings.Aimbot.M249Bone = int.Parse(configData["Aimbot"]["M249Bone"]);
                Settings.Aimbot.M249Smooth = float.Parse(configData["Aimbot"]["M249Smooth"]);
                Settings.Aimbot.M249RecoilControl = bool.Parse(configData["Aimbot"]["M249RecoilControl"]);
                Settings.Aimbot.M249YawRecoilReductionFactory = float.Parse(configData["Aimbot"]["M249RcsX"]);
                Settings.Aimbot.M249PitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["M249RcsY"]);
                Settings.Aimbot.M249Curve = bool.Parse(configData["Aimbot"]["M249Curve"]);
                Settings.Aimbot.M249CurveY = float.Parse(configData["Aimbot"]["M249CurveX"]);
                Settings.Aimbot.M249CurveX = float.Parse(configData["Aimbot"]["M249CurveY"]);
                //Negev
                Settings.Aimbot.NegevFov = float.Parse(configData["Aimbot"]["NegevFov"]);
                Settings.Aimbot.NegevBone = int.Parse(configData["Aimbot"]["NegevBone"]);
                Settings.Aimbot.NegevSmooth = float.Parse(configData["Aimbot"]["NegevSmooth"]);
                Settings.Aimbot.NegevRecoilControl = bool.Parse(configData["Aimbot"]["NegevRecoilControl"]);
                Settings.Aimbot.NegevYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["NegevRcsX"]);
                Settings.Aimbot.NegevPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["NegevRcsY"]);
                Settings.Aimbot.NegevCurve = bool.Parse(configData["Aimbot"]["NegevCurve"]);
                Settings.Aimbot.NegevCurveY = float.Parse(configData["Aimbot"]["NegevCurveX"]);
                Settings.Aimbot.NegevCurveX = float.Parse(configData["Aimbot"]["NegevCurveY"]);
                //Unknown
                Settings.Aimbot.UnknownAutoPistol = bool.Parse(configData["Aimbot"]["UnknownAutoPistol"]); ;
                Settings.Aimbot.UnknownFov = float.Parse(configData["Aimbot"]["UnknownFov"]);
                Settings.Aimbot.UnknownBone = int.Parse(configData["Aimbot"]["UnknownBone"]);
                Settings.Aimbot.UnknownSmooth = float.Parse(configData["Aimbot"]["UnknownSmooth"]);
                Settings.Aimbot.UnknownRecoilControl = bool.Parse(configData["Aimbot"]["UnknownRecoilControl"]);
                Settings.Aimbot.UnknownYawRecoilReductionFactory = float.Parse(configData["Aimbot"]["UnknownRcsX"]);
                Settings.Aimbot.UnknownPitchRecoilReductionFactory = float.Parse(configData["Aimbot"]["UnknownRcsY"]);
                Settings.Aimbot.UnknownCurve = bool.Parse(configData["Aimbot"]["UnknownCurve"]);
                Settings.Aimbot.UnknownCurveY = float.Parse(configData["Aimbot"]["UnknownCurveX"]);
                Settings.Aimbot.UnknownCurveX = float.Parse(configData["Aimbot"]["UnknownCurveY"]);


                Settings.Radar.Enabled                      = bool.Parse(configData["Radar"]["Enabled"]);

                Settings.Bunnyhop.Enabled                   = bool.Parse(configData["Air"]["BunnyHop"]);
                Settings.Bunnyhop.Key                       = int.Parse(configData["Air"]["Key"]);
                Settings.Bunnyhop.AutoStrafe                = bool.Parse(configData["Air"]["AutoStrafe"]);
                Settings.Bunnyhop.StrafeEmulator = bool.Parse(configData["Air"]["StrafeEmulator"]);
                Settings.Bunnyhop.StrafeEmulatorKey = int.Parse(configData["Air"]["StrafeEmulatorKey"]);
                Settings.Bunnyhop.sens = int.Parse(configData["Air"]["StrafeEmulatorSensitivity"]);
                Settings.Bunnyhop.speed = int.Parse(configData["Air"]["StrafeEmulatorSpeed"]);


                Settings.AimAssist.Enabled = bool.Parse(configData["AimAssist"]["Enabled"]);
                Settings.AimAssist.Key = int.Parse(configData["AimAssist"]["Key"]);

                Settings.Sonar.Enabled = bool.Parse(configData["Sonar"]["Enabled"]);
                Settings.Sonar.Key = int.Parse(configData["Sonar"]["Key"]);
                Settings.Sonar.interval = int.Parse(configData["Sonar"]["Interval"]);

                Settings.Trigger.Enabled                    = bool.Parse(configData["Trigger"]["Enabled"]);
                Settings.Trigger.Key                        = int.Parse(configData["Trigger"]["Key"]);
                Settings.Trigger.Delay                      = int.Parse(configData["Trigger"]["Delay"]);
                Settings.Trigger.DelayBetweenShots = int.Parse(configData["Trigger"]["DelayBetweenShots"]);

                Settings.Chams.Enabled = bool.Parse(configData["Chams"]["Enabled"]);
                Settings.Chams.Color_R = byte.Parse(configData["Chams"]["Color_R"]);
                Settings.Chams.Color_G = byte.Parse(configData["Chams"]["Color_G"]);
                Settings.Chams.Color_B = byte.Parse(configData["Chams"]["Color_B"]);
                Settings.Chams.Allies = bool.Parse(configData["Chams"]["Allies"]);
                Settings.Chams.Allies_Color_R = byte.Parse(configData["Chams"]["Allies_Color_R"]);
                Settings.Chams.Allies_Color_G = byte.Parse(configData["Chams"]["Allies_Color_G"]);
                Settings.Chams.Allies_Color_B = byte.Parse(configData["Chams"]["Allies_Color_B"]);


                Settings.Glow.Enabled                       = bool.Parse(configData["Glow"]["Enabled"]);
                Settings.Glow.bSpotted = bool.Parse(configData["Glow"]["bSpotted"]);
                Settings.Glow.HealthBased                  = bool.Parse(configData["Glow"]["HealthBased"]);
                Settings.Glow.FullBloom                     = bool.Parse(configData["Glow"]["FullBloom"]);
                Settings.Glow.ShowWeapons                   = bool.Parse(configData["Glow"]["ShowWeapons"]);

                Settings.Glow.Enemies = bool.Parse(configData["Glow"]["Enemies"]);
                Settings.Glow.Enemies_Color_R = float.Parse(configData["Glow"]["Enemies_Color_R"]);
                Settings.Glow.Enemies_Color_G = float.Parse(configData["Glow"]["Enemies_Color_G"]);
                Settings.Glow.Enemies_Color_B = float.Parse(configData["Glow"]["Enemies_Color_B"]);
                Settings.Glow.Enemies_Color_A = float.Parse(configData["Glow"]["Enemies_Color_A"]);
                Settings.Glow.InvisibleEnemies_Color_R = float.Parse(configData["Glow"]["InvisibleEnemies_Color_R"]);
                Settings.Glow.InvisibleEnemies_Color_G = float.Parse(configData["Glow"]["InvisibleEnemies_Color_G"]);
                Settings.Glow.InvisibleEnemies_Color_B = float.Parse(configData["Glow"]["InvisibleEnemies_Color_B"]);
                Settings.Glow.InvisibleEnemies_Color_A = float.Parse(configData["Glow"]["InvisibleEnemies_Color_A"]);

                Settings.Glow.Allies                        = bool.Parse(configData["Glow"]["Allies"]);
                Settings.Glow.Allies_Color_R                = float.Parse(configData["Glow"]["Allies_Color_R"]);
                Settings.Glow.Allies_Color_G                = float.Parse(configData["Glow"]["Allies_Color_G"]);
                Settings.Glow.Allies_Color_B                = float.Parse(configData["Glow"]["Allies_Color_B"]);
                Settings.Glow.Allies_Color_A                = float.Parse(configData["Glow"]["Allies_Color_A"]);

                
                Settings.Glow.Snipers                       = bool.Parse(configData["Glow"]["Snipers"]);
                Settings.Glow.Snipers_Color_R               = float.Parse(configData["Glow"]["Snipers_Color_R"]);
                Settings.Glow.Snipers_Color_G               = float.Parse(configData["Glow"]["Snipers_Color_G"]);
                Settings.Glow.Snipers_Color_B               = float.Parse(configData["Glow"]["Snipers_Color_B"]);
                Settings.Glow.Snipers_Color_A               = float.Parse(configData["Glow"]["Snipers_Color_A"]);

                Settings.Glow.Rifles                        = bool.Parse(configData["Glow"]["Rifles"]);
                Settings.Glow.Rifles_Color_R                = float.Parse(configData["Glow"]["Rifles_Color_R"]);
                Settings.Glow.Rifles_Color_G                = float.Parse(configData["Glow"]["Rifles_Color_G"]);
                Settings.Glow.Rifles_Color_B                = float.Parse(configData["Glow"]["Rifles_Color_B"]);
                Settings.Glow.Rifles_Color_A                = float.Parse(configData["Glow"]["Rifles_Color_A"]);

                Settings.Glow.Heavy                   = bool.Parse(configData["Glow"]["Heavy"]);
                Settings.Glow.Heavy_Color_R = float.Parse(configData["Glow"]["Heavy_Color_R"]);
                Settings.Glow.Heavy_Color_G = float.Parse(configData["Glow"]["Heavy_Color_G"]);
                Settings.Glow.Heavy_Color_B = float.Parse(configData["Glow"]["Heavy_Color_B"]);
                Settings.Glow.Heavy_Color_A = float.Parse(configData["Glow"]["Heavy_Color_A"]);

                Settings.Glow.MPs                           = bool.Parse(configData["Glow"]["MPs"]);
                Settings.Glow.MPs_Color_R                   = float.Parse(configData["Glow"]["MPs_Color_R"]);
                Settings.Glow.MPs_Color_G                   = float.Parse(configData["Glow"]["MPs_Color_G"]);
                Settings.Glow.MPs_Color_B                   = float.Parse(configData["Glow"]["MPs_Color_B"]);
                Settings.Glow.MPs_Color_A                   = float.Parse(configData["Glow"]["MPs_Color_A"]);

                Settings.Glow.Pistols                       = bool.Parse(configData["Glow"]["Pistols"]);
                Settings.Glow.Pistols_Color_R               = float.Parse(configData["Glow"]["Pistols_Color_R"]);
                Settings.Glow.Pistols_Color_G               = float.Parse(configData["Glow"]["Pistols_Color_G"]);
                Settings.Glow.Pistols_Color_B               = float.Parse(configData["Glow"]["Pistols_Color_B"]);
                Settings.Glow.Pistols_Color_A               = float.Parse(configData["Glow"]["Pistols_Color_A"]);

                Settings.Glow.C4                            = bool.Parse(configData["Glow"]["C4"]);
                Settings.Glow.C4_Color_R                    = float.Parse(configData["Glow"]["C4_Color_R"]);
                Settings.Glow.C4_Color_G                    = float.Parse(configData["Glow"]["C4_Color_G"]);
                Settings.Glow.C4_Color_B                    = float.Parse(configData["Glow"]["C4_Color_B"]);
                Settings.Glow.C4_Color_A                    = float.Parse(configData["Glow"]["C4_Color_A"]);

                Settings.Glow.Grenades                      = bool.Parse(configData["Glow"]["Grenades"]);
                Settings.Glow.Grenades_Color_R              = float.Parse(configData["Glow"]["Grenades_Color_R"]);
                Settings.Glow.Grenades_Color_G              = float.Parse(configData["Glow"]["Grenades_Color_G"]);
                Settings.Glow.Grenades_Color_B              = float.Parse(configData["Glow"]["Grenades_Color_B"]);
                Settings.Glow.Grenades_Color_A              = float.Parse(configData["Glow"]["Grenades_Color_A"]);

                Settings.OtherControls.ToggleMenu = int.Parse(configData["Toggles"]["Menu"]);
                Settings.OtherControls.PanicKey = int.Parse(configData["Toggles"]["Exit"]);
                Settings.OtherControls.ToggleGlow = int.Parse(configData["Toggles"]["Glow"]);
                Settings.OtherControls.ToggleRadar = int.Parse(configData["Toggles"]["Radar"]);
                Settings.OtherControls.ToggleAimbot = int.Parse(configData["Toggles"]["Aimbot"]);

                Console.Beep(300, 80);
            }
            catch 
            {
                Console.Beep(100, 100);
                System.Threading.Thread.Sleep(100);
                Console.Beep(100, 100);
                //  Extensions.Error("> Your config is outdated. Update it with SAVE button!.", 0, false);
                //   Extensions.Error("> Структура вашего конфига устарела. Обновите при помощи кнопки SAVE.", 0, false);
            }

         //   Extensions.Information("> Config Loaded!", true);


        }

        public static void Save(string path)
        {
            if (File.Exists(path)) 
            {
                if (!IsFileUsable(path))
                {
                    MessageBox.Show("Config File is not writeable");
                    return;
                }
            }

            FileIniDataParser configParser = new FileIniDataParser();
            IniData configData = new IniData();
            configData["Aimbot"]["Enabled"] = Settings.Aimbot.Enabled.ToString();
            configData["Aimbot"]["Key"] = Settings.Aimbot.Key.ToString();
            configData["Aimbot"]["AutoDelayKey"] = Settings.Aimbot.SecondKey.ToString();
            configData["Aimbot"]["VisibleOnly"] = Settings.Aimbot.VisibleOnly.ToString();
            configData["Aimbot"]["bSpottedCheck"] = Settings.Aimbot.bSpottedCheck.ToString();
            configData["Aimbot"]["KillDelay"] = Settings.Aimbot.KillDelay.ToString();
            configData["Aimbot"]["UseMouseEvent"] = Settings.Aimbot.UseMouseEvent.ToString();
            configData["Aimbot"]["MouseEventFov"] = Settings.Aimbot.MouseEventFov.ToString();
            configData["Aimbot"]["MouseEventSmooth"] = Settings.Aimbot.MouseEventSmooth.ToString();

            configData["Aimbot"]["Fov"] = Settings.Aimbot.Fov.ToString();
            configData["Aimbot"]["Bone"] = Settings.Aimbot.Bone.ToString();
            configData["Aimbot"]["Smooth"] = Settings.Aimbot.Smooth.ToString();
            configData["Aimbot"]["RecoilControl"] = Settings.Aimbot.RecoilControl.ToString();
            configData["Aimbot"]["RcsX"] = Settings.Aimbot.YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["RcsY"] = Settings.Aimbot.PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["Curve"] = Settings.Aimbot.Curve.ToString();
            configData["Aimbot"]["CurveX"] = Settings.Aimbot.CurveX.ToString();
            configData["Aimbot"]["CurveY"] = Settings.Aimbot.CurveY.ToString();
            //Glock
            configData["Aimbot"]["GlockAutoPistol"] = Settings.Aimbot.GlockAutoPistol.ToString();
            configData["Aimbot"]["GlockFov"] = Settings.Aimbot.GlockFov.ToString();
            configData["Aimbot"]["GlockBone"] = Settings.Aimbot.GlockBone.ToString();
            configData["Aimbot"]["GlockSmooth"] = Settings.Aimbot.GlockSmooth.ToString();
            configData["Aimbot"]["GlockRecoilControl"] = Settings.Aimbot.GlockRecoilControl.ToString();
            configData["Aimbot"]["GlockRcsX"] = Settings.Aimbot.GlockYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["GlockRcsY"] = Settings.Aimbot.GlockPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["GlockCurve"] = Settings.Aimbot.GlockCurve.ToString();
            configData["Aimbot"]["GlockCurveX"] = Settings.Aimbot.GlockCurveX.ToString();
            configData["Aimbot"]["GlockCurveY"] = Settings.Aimbot.GlockCurveY.ToString();
            //USP
            configData["Aimbot"]["USPAutoPistol"] = Settings.Aimbot.USPAutoPistol.ToString();
            configData["Aimbot"]["USPFov"] = Settings.Aimbot.USPFov.ToString();
            configData["Aimbot"]["USPBone"] = Settings.Aimbot.USPBone.ToString();
            configData["Aimbot"]["USPSmooth"] = Settings.Aimbot.USPSmooth.ToString();
            configData["Aimbot"]["USPRecoilControl"] = Settings.Aimbot.USPRecoilControl.ToString();
            configData["Aimbot"]["USPRcsX"] = Settings.Aimbot.USPYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["USPRcsY"] = Settings.Aimbot.USPPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["USPCurve"] = Settings.Aimbot.USPCurve.ToString();
            configData["Aimbot"]["USPCurveX"] = Settings.Aimbot.USPCurveX.ToString();
            configData["Aimbot"]["USPCurveY"] = Settings.Aimbot.USPCurveY.ToString();
            //P2000
            configData["Aimbot"]["P2000AutoPistol"] = Settings.Aimbot.P2000AutoPistol.ToString();
            configData["Aimbot"]["P2000Fov"] = Settings.Aimbot.P2000Fov.ToString();
            configData["Aimbot"]["P2000Bone"] = Settings.Aimbot.P2000Bone.ToString();
            configData["Aimbot"]["P2000Smooth"] = Settings.Aimbot.P2000Smooth.ToString();
            configData["Aimbot"]["P2000RecoilControl"] = Settings.Aimbot.P2000RecoilControl.ToString();
            configData["Aimbot"]["P2000RcsX"] = Settings.Aimbot.P2000YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["P2000RcsY"] = Settings.Aimbot.P2000PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["P2000Curve"] = Settings.Aimbot.P2000Curve.ToString();
            configData["Aimbot"]["P2000CurveX"] = Settings.Aimbot.P2000CurveX.ToString();
            configData["Aimbot"]["P2000CurveY"] = Settings.Aimbot.P2000CurveY.ToString();
            //Duals
            configData["Aimbot"]["DualsAutoPistol"] = Settings.Aimbot.DualsAutoPistol.ToString();
            configData["Aimbot"]["DualsFov"] = Settings.Aimbot.DualsFov.ToString();
            configData["Aimbot"]["DualsBone"] = Settings.Aimbot.DualsBone.ToString();
            configData["Aimbot"]["DualsSmooth"] = Settings.Aimbot.DualsSmooth.ToString();
            configData["Aimbot"]["DualsRecoilControl"] = Settings.Aimbot.DualsRecoilControl.ToString();
            configData["Aimbot"]["DualsRcsX"] = Settings.Aimbot.DualsYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["DualsRcsY"] = Settings.Aimbot.DualsPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["DualsCurve"] = Settings.Aimbot.DualsCurve.ToString();
            configData["Aimbot"]["DualsCurveX"] = Settings.Aimbot.DualsCurveX.ToString();
            configData["Aimbot"]["DualsCurveY"] = Settings.Aimbot.DualsCurveY.ToString();
            //P250
            configData["Aimbot"]["P250AutoPistol"] = Settings.Aimbot.P250AutoPistol.ToString();
            configData["Aimbot"]["P250Fov"] = Settings.Aimbot.P250Fov.ToString();
            configData["Aimbot"]["P250Bone"] = Settings.Aimbot.P250Bone.ToString();
            configData["Aimbot"]["P250Smooth"] = Settings.Aimbot.P250Smooth.ToString();
            configData["Aimbot"]["P250RecoilControl"] = Settings.Aimbot.P250RecoilControl.ToString();
            configData["Aimbot"]["P250RcsX"] = Settings.Aimbot.P250YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["P250RcsY"] = Settings.Aimbot.P250PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["P250Curve"] = Settings.Aimbot.P250Curve.ToString();
            configData["Aimbot"]["P250CurveX"] = Settings.Aimbot.P250CurveX.ToString();
            configData["Aimbot"]["P250CurveY"] = Settings.Aimbot.P250CurveY.ToString();
            //FiveSeven
            configData["Aimbot"]["FiveSevenAutoPistol"] = Settings.Aimbot.FiveSevenAutoPistol.ToString();
            configData["Aimbot"]["FiveSevenFov"] = Settings.Aimbot.FiveSevenFov.ToString();
            configData["Aimbot"]["FiveSevenBone"] = Settings.Aimbot.FiveSevenBone.ToString();
            configData["Aimbot"]["FiveSevenSmooth"] = Settings.Aimbot.FiveSevenSmooth.ToString();
            configData["Aimbot"]["FiveSevenRecoilControl"] = Settings.Aimbot.FiveSevenRecoilControl.ToString();
            configData["Aimbot"]["FiveSevenRcsX"] = Settings.Aimbot.FiveSevenYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["FiveSevenRcsY"] = Settings.Aimbot.FiveSevenPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["FiveSevenCurve"] = Settings.Aimbot.FiveSevenCurve.ToString();
            configData["Aimbot"]["FiveSevenCurveX"] = Settings.Aimbot.FiveSevenCurveX.ToString();
            configData["Aimbot"]["FiveSevenCurveY"] = Settings.Aimbot.FiveSevenCurveY.ToString();
            //TEC9
            configData["Aimbot"]["TEC9AutoPistol"] = Settings.Aimbot.TEC9AutoPistol.ToString();
            configData["Aimbot"]["TEC9Fov"] = Settings.Aimbot.TEC9Fov.ToString();
            configData["Aimbot"]["TEC9Bone"] = Settings.Aimbot.TEC9Bone.ToString();
            configData["Aimbot"]["TEC9Smooth"] = Settings.Aimbot.TEC9Smooth.ToString();
            configData["Aimbot"]["TEC9RecoilControl"] = Settings.Aimbot.TEC9RecoilControl.ToString();
            configData["Aimbot"]["TEC9RcsX"] = Settings.Aimbot.TEC9YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["TEC9RcsY"] = Settings.Aimbot.TEC9PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["TEC9Curve"] = Settings.Aimbot.TEC9Curve.ToString();
            configData["Aimbot"]["TEC9CurveX"] = Settings.Aimbot.TEC9CurveX.ToString();
            configData["Aimbot"]["TEC9CurveY"] = Settings.Aimbot.TEC9CurveY.ToString();
            //DEAGLE
            configData["Aimbot"]["DEAGLEAutoPistol"] = Settings.Aimbot.DEAGLEAutoPistol.ToString();
            configData["Aimbot"]["DEAGLEFov"] = Settings.Aimbot.DEAGLEFov.ToString();
            configData["Aimbot"]["DEAGLEBone"] = Settings.Aimbot.DEAGLEBone.ToString();
            configData["Aimbot"]["DEAGLESmooth"] = Settings.Aimbot.DEAGLESmooth.ToString();
            configData["Aimbot"]["DEAGLERecoilControl"] = Settings.Aimbot.DEAGLERecoilControl.ToString();
            configData["Aimbot"]["DEAGLERcsX"] = Settings.Aimbot.DEAGLEYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["DEAGLERcsY"] = Settings.Aimbot.DEAGLEPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["DEAGLECurve"] = Settings.Aimbot.DEAGLECurve.ToString();
            configData["Aimbot"]["DEAGLECurveX"] = Settings.Aimbot.DEAGLECurveX.ToString();
            configData["Aimbot"]["DEAGLECurveY"] = Settings.Aimbot.DEAGLECurveY.ToString();
            //Revolver
            configData["Aimbot"]["RevolverFov"] = Settings.Aimbot.RevolverFov.ToString();
            configData["Aimbot"]["RevolverBone"] = Settings.Aimbot.RevolverBone.ToString();
            configData["Aimbot"]["RevolverSmooth"] = Settings.Aimbot.RevolverSmooth.ToString();
            configData["Aimbot"]["RevolverRecoilControl"] = Settings.Aimbot.RevolverRecoilControl.ToString();
            configData["Aimbot"]["RevolverRcsX"] = Settings.Aimbot.RevolverYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["RevolverRcsY"] = Settings.Aimbot.RevolverPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["RevolverCurve"] = Settings.Aimbot.RevolverCurve.ToString();
            configData["Aimbot"]["RevolverCurveX"] = Settings.Aimbot.RevolverCurveX.ToString();
            configData["Aimbot"]["RevolverCurveY"] = Settings.Aimbot.RevolverCurveY.ToString();
            //CZ
            configData["Aimbot"]["CZFov"] = Settings.Aimbot.CZFov.ToString();
            configData["Aimbot"]["CZBone"] = Settings.Aimbot.CZBone.ToString();
            configData["Aimbot"]["CZSmooth"] = Settings.Aimbot.CZSmooth.ToString();
            configData["Aimbot"]["CZRecoilControl"] = Settings.Aimbot.CZRecoilControl.ToString();
            configData["Aimbot"]["CZRcsX"] = Settings.Aimbot.CZYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["CZRcsY"] = Settings.Aimbot.CZPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["CZCurve"] = Settings.Aimbot.CZCurve.ToString();
            configData["Aimbot"]["CZCurveX"] = Settings.Aimbot.CZCurveX.ToString();
            configData["Aimbot"]["CZCurveY"] = Settings.Aimbot.CZCurveY.ToString();
            //
            //SMG
            //
            //MAC10
            configData["Aimbot"]["MAC10Fov"] = Settings.Aimbot.MAC10Fov.ToString();
            configData["Aimbot"]["MAC10Bone"] = Settings.Aimbot.MAC10Bone.ToString();
            configData["Aimbot"]["MAC10Smooth"] = Settings.Aimbot.MAC10Smooth.ToString();
            configData["Aimbot"]["MAC10RecoilControl"] = Settings.Aimbot.MAC10RecoilControl.ToString();
            configData["Aimbot"]["MAC10RcsX"] = Settings.Aimbot.MAC10YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["MAC10RcsY"] = Settings.Aimbot.MAC10PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["MAC10Curve"] = Settings.Aimbot.MAC10Curve.ToString();
            configData["Aimbot"]["MAC10CurveX"] = Settings.Aimbot.MAC10CurveX.ToString();
            configData["Aimbot"]["MAC10CurveY"] = Settings.Aimbot.MAC10CurveY.ToString();
            //MP9
            configData["Aimbot"]["MP9Fov"] = Settings.Aimbot.MP9Fov.ToString();
            configData["Aimbot"]["MP9Bone"] = Settings.Aimbot.MP9Bone.ToString();
            configData["Aimbot"]["MP9Smooth"] = Settings.Aimbot.MP9Smooth.ToString();
            configData["Aimbot"]["MP9RecoilControl"] = Settings.Aimbot.MP9RecoilControl.ToString();
            configData["Aimbot"]["MP9RcsX"] = Settings.Aimbot.MP9YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["MP9RcsY"] = Settings.Aimbot.MP9PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["MP9Curve"] = Settings.Aimbot.MP9Curve.ToString();
            configData["Aimbot"]["MP9CurveX"] = Settings.Aimbot.MP9CurveX.ToString();
            configData["Aimbot"]["MP9CurveY"] = Settings.Aimbot.MP9CurveY.ToString();
            //MP7
            configData["Aimbot"]["MP7Fov"] = Settings.Aimbot.MP7Fov.ToString();
            configData["Aimbot"]["MP7Bone"] = Settings.Aimbot.MP7Bone.ToString();
            configData["Aimbot"]["MP7Smooth"] = Settings.Aimbot.MP7Smooth.ToString();
            configData["Aimbot"]["MP7RecoilControl"] = Settings.Aimbot.MP7RecoilControl.ToString();
            configData["Aimbot"]["MP7RcsX"] = Settings.Aimbot.MP7YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["MP7RcsY"] = Settings.Aimbot.MP7PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["MP7Curve"] = Settings.Aimbot.MP7Curve.ToString();
            configData["Aimbot"]["MP7CurveX"] = Settings.Aimbot.MP7CurveX.ToString();
            configData["Aimbot"]["MP7CurveY"] = Settings.Aimbot.MP7CurveY.ToString();
            //MP5
            configData["Aimbot"]["MP5Fov"] = Settings.Aimbot.MP5Fov.ToString();
            configData["Aimbot"]["MP5Bone"] = Settings.Aimbot.MP5Bone.ToString();
            configData["Aimbot"]["MP5Smooth"] = Settings.Aimbot.MP5Smooth.ToString();
            configData["Aimbot"]["MP5RecoilControl"] = Settings.Aimbot.MP5RecoilControl.ToString();
            configData["Aimbot"]["MP5RcsX"] = Settings.Aimbot.MP5YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["MP5RcsY"] = Settings.Aimbot.MP5PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["MP5Curve"] = Settings.Aimbot.MP5Curve.ToString();
            configData["Aimbot"]["MP5CurveX"] = Settings.Aimbot.MP5CurveX.ToString();
            configData["Aimbot"]["MP5CurveY"] = Settings.Aimbot.MP5CurveY.ToString();
            //UMP
            configData["Aimbot"]["UMPFov"] = Settings.Aimbot.UMPFov.ToString();
            configData["Aimbot"]["UMPBone"] = Settings.Aimbot.UMPBone.ToString();
            configData["Aimbot"]["UMPSmooth"] = Settings.Aimbot.UMPSmooth.ToString();
            configData["Aimbot"]["UMPRecoilControl"] = Settings.Aimbot.UMPRecoilControl.ToString();
            configData["Aimbot"]["UMPRcsX"] = Settings.Aimbot.UMPYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["UMPRcsY"] = Settings.Aimbot.UMPPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["UMPCurve"] = Settings.Aimbot.UMPCurve.ToString();
            configData["Aimbot"]["UMPCurveX"] = Settings.Aimbot.UMPCurveX.ToString();
            configData["Aimbot"]["UMPCurveY"] = Settings.Aimbot.UMPCurveY.ToString();
            //Bizon
            configData["Aimbot"]["BizonFov"] = Settings.Aimbot.BizonFov.ToString();
            configData["Aimbot"]["BizonBone"] = Settings.Aimbot.BizonBone.ToString();
            configData["Aimbot"]["BizonSmooth"] = Settings.Aimbot.BizonSmooth.ToString();
            configData["Aimbot"]["BizonRecoilControl"] = Settings.Aimbot.BizonRecoilControl.ToString();
            configData["Aimbot"]["BizonRcsX"] = Settings.Aimbot.BizonYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["BizonRcsY"] = Settings.Aimbot.BizonPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["BizonCurve"] = Settings.Aimbot.BizonCurve.ToString();
            configData["Aimbot"]["BizonCurveX"] = Settings.Aimbot.BizonCurveX.ToString();
            configData["Aimbot"]["BizonCurveY"] = Settings.Aimbot.BizonCurveY.ToString();
            //P90
            configData["Aimbot"]["P90Fov"] = Settings.Aimbot.P90Fov.ToString();
            configData["Aimbot"]["P90Bone"] = Settings.Aimbot.P90Bone.ToString();
            configData["Aimbot"]["P90Smooth"] = Settings.Aimbot.P90Smooth.ToString();
            configData["Aimbot"]["P90RecoilControl"] = Settings.Aimbot.P90RecoilControl.ToString();
            configData["Aimbot"]["P90RcsX"] = Settings.Aimbot.P90YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["P90RcsY"] = Settings.Aimbot.P90PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["P90Curve"] = Settings.Aimbot.P90Curve.ToString();
            configData["Aimbot"]["P90CurveX"] = Settings.Aimbot.P90CurveX.ToString();
            configData["Aimbot"]["P90CurveY"] = Settings.Aimbot.P90CurveY.ToString();
            //Galil
            configData["Aimbot"]["GalilFov"] = Settings.Aimbot.GalilFov.ToString();
            configData["Aimbot"]["GalilBone"] = Settings.Aimbot.GalilBone.ToString();
            configData["Aimbot"]["GalilSmooth"] = Settings.Aimbot.GalilSmooth.ToString();
            configData["Aimbot"]["GalilRecoilControl"] = Settings.Aimbot.GalilRecoilControl.ToString();
            configData["Aimbot"]["GalilRcsX"] = Settings.Aimbot.GalilYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["GalilRcsY"] = Settings.Aimbot.GalilPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["GalilCurve"] = Settings.Aimbot.GalilCurve.ToString();
            configData["Aimbot"]["GalilCurveX"] = Settings.Aimbot.GalilCurveX.ToString();
            configData["Aimbot"]["GalilCurveY"] = Settings.Aimbot.GalilCurveY.ToString();
            //Famas
            configData["Aimbot"]["FamasFov"] = Settings.Aimbot.FamasFov.ToString();
            configData["Aimbot"]["FamasBone"] = Settings.Aimbot.FamasBone.ToString();
            configData["Aimbot"]["FamasSmooth"] = Settings.Aimbot.FamasSmooth.ToString();
            configData["Aimbot"]["FamasRecoilControl"] = Settings.Aimbot.FamasRecoilControl.ToString();
            configData["Aimbot"]["FamasRcsX"] = Settings.Aimbot.FamasYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["FamasRcsY"] = Settings.Aimbot.FamasPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["FamasCurve"] = Settings.Aimbot.FamasCurve.ToString();
            configData["Aimbot"]["FamasCurveX"] = Settings.Aimbot.FamasCurveX.ToString();
            configData["Aimbot"]["FamasCurveY"] = Settings.Aimbot.FamasCurveY.ToString();
            //AK47
            configData["Aimbot"]["AK47Fov"] = Settings.Aimbot.AK47Fov.ToString();
            configData["Aimbot"]["AK47Bone"] = Settings.Aimbot.AK47Bone.ToString();
            configData["Aimbot"]["AK47Smooth"] = Settings.Aimbot.AK47Smooth.ToString();
            configData["Aimbot"]["AK47RecoilControl"] = Settings.Aimbot.AK47RecoilControl.ToString();
            configData["Aimbot"]["AK47RcsX"] = Settings.Aimbot.AK47YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["AK47RcsY"] = Settings.Aimbot.AK47PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["AK47Curve"] = Settings.Aimbot.AK47Curve.ToString();
            configData["Aimbot"]["AK47CurveX"] = Settings.Aimbot.AK47CurveX.ToString();
            configData["Aimbot"]["AK47CurveY"] = Settings.Aimbot.AK47CurveY.ToString();
            configData["Aimbot"]["FirstBulletAK47"] = Settings.Aimbot.FirstAK47.ToString();
            configData["Aimbot"]["FirstBulletAK47Fov"] = Settings.Aimbot.FirstAK47Fov.ToString();
            configData["Aimbot"]["FirstBulletAK47Bone"] = Settings.Aimbot.FirstAK47Bone.ToString();
            configData["Aimbot"]["FirstBulletAK47Smooth"] = Settings.Aimbot.FirstAK47Smooth.ToString();
            //M4A4
            configData["Aimbot"]["M4A4Fov"] = Settings.Aimbot.M4A4Fov.ToString();
            configData["Aimbot"]["M4A4Bone"] = Settings.Aimbot.M4A4Bone.ToString();
            configData["Aimbot"]["M4A4Smooth"] = Settings.Aimbot.M4A4Smooth.ToString();
            configData["Aimbot"]["M4A4RecoilControl"] = Settings.Aimbot.M4A4RecoilControl.ToString();
            configData["Aimbot"]["M4A4RcsX"] = Settings.Aimbot.M4A4YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["M4A4RcsY"] = Settings.Aimbot.M4A4PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["M4A4Curve"] = Settings.Aimbot.M4A4Curve.ToString();
            configData["Aimbot"]["M4A4CurveX"] = Settings.Aimbot.M4A4CurveX.ToString();
            configData["Aimbot"]["M4A4CurveY"] = Settings.Aimbot.M4A4CurveY.ToString();
            configData["Aimbot"]["FirstBulletM4A4"] = Settings.Aimbot.FirstM4A4.ToString();
            configData["Aimbot"]["FirstBulletM4A4Fov"] = Settings.Aimbot.FirstM4A4Fov.ToString();
            configData["Aimbot"]["FirstBulletM4A4Bone"] = Settings.Aimbot.FirstM4A4Bone.ToString();
            configData["Aimbot"]["FirstBulletM4A4Smooth"] = Settings.Aimbot.FirstM4A4Smooth.ToString();
            //M4A1
            configData["Aimbot"]["M4A1Fov"] = Settings.Aimbot.M4A1Fov.ToString();
            configData["Aimbot"]["M4A1Bone"] = Settings.Aimbot.M4A1Bone.ToString();
            configData["Aimbot"]["M4A1Smooth"] = Settings.Aimbot.M4A1Smooth.ToString();
            configData["Aimbot"]["M4A1RecoilControl"] = Settings.Aimbot.M4A1RecoilControl.ToString();
            configData["Aimbot"]["M4A1RcsX"] = Settings.Aimbot.M4A1YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["M4A1RcsY"] = Settings.Aimbot.M4A1PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["M4A1Curve"] = Settings.Aimbot.M4A1Curve.ToString();
            configData["Aimbot"]["M4A1CurveX"] = Settings.Aimbot.M4A1CurveX.ToString();
            configData["Aimbot"]["M4A1CurveY"] = Settings.Aimbot.M4A1CurveY.ToString();
            configData["Aimbot"]["FirstBulletM4A1"] = Settings.Aimbot.FirstM4A1.ToString();
            configData["Aimbot"]["FirstBulletM4A1Fov"] = Settings.Aimbot.FirstM4A1Fov.ToString();
            configData["Aimbot"]["FirstBulletM4A1Bone"] = Settings.Aimbot.FirstM4A1Bone.ToString();
            configData["Aimbot"]["FirstBulletM4A1Smooth"] = Settings.Aimbot.FirstM4A1Smooth.ToString();
            //SG
            configData["Aimbot"]["SGFov"] = Settings.Aimbot.SGFov.ToString();
            configData["Aimbot"]["SGBone"] = Settings.Aimbot.SGBone.ToString();
            configData["Aimbot"]["SGSmooth"] = Settings.Aimbot.SGSmooth.ToString();
            configData["Aimbot"]["SGRecoilControl"] = Settings.Aimbot.SGRecoilControl.ToString();
            configData["Aimbot"]["SGRcsX"] = Settings.Aimbot.SGYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["SGRcsY"] = Settings.Aimbot.SGPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["SGCurve"] = Settings.Aimbot.SGCurve.ToString();
            configData["Aimbot"]["SGCurveX"] = Settings.Aimbot.SGCurveX.ToString();
            configData["Aimbot"]["SGCurveY"] = Settings.Aimbot.SGCurveY.ToString();
            //AUG
            configData["Aimbot"]["AUGFov"] = Settings.Aimbot.AUGFov.ToString();
            configData["Aimbot"]["AUGBone"] = Settings.Aimbot.AUGBone.ToString();
            configData["Aimbot"]["AUGSmooth"] = Settings.Aimbot.AUGSmooth.ToString();
            configData["Aimbot"]["AUGRecoilControl"] = Settings.Aimbot.AUGRecoilControl.ToString();
            configData["Aimbot"]["AUGRcsX"] = Settings.Aimbot.AUGYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["AUGRcsY"] = Settings.Aimbot.AUGPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["AUGCurve"] = Settings.Aimbot.AUGCurve.ToString();
            configData["Aimbot"]["AUGCurveX"] = Settings.Aimbot.AUGCurveX.ToString();
            configData["Aimbot"]["AUGCurveY"] = Settings.Aimbot.AUGCurveY.ToString();
            //SSG
            configData["Aimbot"]["SSGFov"] = Settings.Aimbot.SSGFov.ToString();
            configData["Aimbot"]["SSGBone"] = Settings.Aimbot.SSGBone.ToString();
            configData["Aimbot"]["SSGSmooth"] = Settings.Aimbot.SSGSmooth.ToString();
            configData["Aimbot"]["SSGRecoilControl"] = Settings.Aimbot.SSGRecoilControl.ToString();
            configData["Aimbot"]["SSGRcsX"] = Settings.Aimbot.SSGYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["SSGRcsY"] = Settings.Aimbot.SSGPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["SSGCurve"] = Settings.Aimbot.SSGCurve.ToString();
            configData["Aimbot"]["SSGCurveX"] = Settings.Aimbot.SSGCurveX.ToString();
            configData["Aimbot"]["SSGCurveY"] = Settings.Aimbot.SSGCurveY.ToString();
            //AWP
            configData["Aimbot"]["AWPFov"] = Settings.Aimbot.AWPFov.ToString();
            configData["Aimbot"]["AWPBone"] = Settings.Aimbot.AWPBone.ToString();
            configData["Aimbot"]["AWPSmooth"] = Settings.Aimbot.AWPSmooth.ToString();
            configData["Aimbot"]["AWPRecoilControl"] = Settings.Aimbot.AWPRecoilControl.ToString();
            configData["Aimbot"]["AWPRcsX"] = Settings.Aimbot.AWPYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["AWPRcsY"] = Settings.Aimbot.AWPPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["AWPCurve"] = Settings.Aimbot.AWPCurve.ToString();
            configData["Aimbot"]["AWPCurveX"] = Settings.Aimbot.AWPCurveX.ToString();
            configData["Aimbot"]["AWPCurveY"] = Settings.Aimbot.AWPCurveY.ToString();
            //AUTO
            configData["Aimbot"]["AUTOFov"] = Settings.Aimbot.AUTOFov.ToString();
            configData["Aimbot"]["AUTOBone"] = Settings.Aimbot.AUTOBone.ToString();
            configData["Aimbot"]["AUTOSmooth"] = Settings.Aimbot.AUTOSmooth.ToString();
            configData["Aimbot"]["AUTORecoilControl"] = Settings.Aimbot.AUTORecoilControl.ToString();
            configData["Aimbot"]["AUTORcsX"] = Settings.Aimbot.AUTOYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["AUTORcsY"] = Settings.Aimbot.AUTOPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["AUTOCurve"] = Settings.Aimbot.AUTOCurve.ToString();
            configData["Aimbot"]["AUTOCurveX"] = Settings.Aimbot.AUTOCurveX.ToString();
            configData["Aimbot"]["AUTOCurveY"] = Settings.Aimbot.AUTOCurveY.ToString();
            //Nova
            configData["Aimbot"]["NovaAutoPistol"] = Settings.Aimbot.NovaAutoPistol.ToString();
            configData["Aimbot"]["NovaFov"] = Settings.Aimbot.NovaFov.ToString();
            configData["Aimbot"]["NovaBone"] = Settings.Aimbot.NovaBone.ToString();
            configData["Aimbot"]["NovaSmooth"] = Settings.Aimbot.NovaSmooth.ToString();
            configData["Aimbot"]["NovaRecoilControl"] = Settings.Aimbot.NovaRecoilControl.ToString();
            configData["Aimbot"]["NovaRcsX"] = Settings.Aimbot.NovaYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["NovaRcsY"] = Settings.Aimbot.NovaPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["NovaCurve"] = Settings.Aimbot.NovaCurve.ToString();
            configData["Aimbot"]["NovaCurveX"] = Settings.Aimbot.NovaCurveX.ToString();
            configData["Aimbot"]["NovaCurveY"] = Settings.Aimbot.NovaCurveY.ToString();
            //XM
            configData["Aimbot"]["XMFov"] = Settings.Aimbot.XMFov.ToString();
            configData["Aimbot"]["XMBone"] = Settings.Aimbot.XMBone.ToString();
            configData["Aimbot"]["XMSmooth"] = Settings.Aimbot.XMSmooth.ToString();
            configData["Aimbot"]["XMRecoilControl"] = Settings.Aimbot.XMRecoilControl.ToString();
            configData["Aimbot"]["XMRcsX"] = Settings.Aimbot.XMYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["XMRcsY"] = Settings.Aimbot.XMPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["XMCurve"] = Settings.Aimbot.XMCurve.ToString();
            configData["Aimbot"]["XMCurveX"] = Settings.Aimbot.XMCurveX.ToString();
            configData["Aimbot"]["XMCurveY"] = Settings.Aimbot.XMCurveY.ToString();
            //SawnedOff
            configData["Aimbot"]["SawnedOffFov"] = Settings.Aimbot.SawnedOffFov.ToString();
            configData["Aimbot"]["SawnedOffBone"] = Settings.Aimbot.SawnedOffBone.ToString();
            configData["Aimbot"]["SawnedOffSmooth"] = Settings.Aimbot.SawnedOffSmooth.ToString();
            configData["Aimbot"]["SawnedOffRecoilControl"] = Settings.Aimbot.SawnedOffRecoilControl.ToString();
            configData["Aimbot"]["SawnedOffRcsX"] = Settings.Aimbot.SawnedOffYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["SawnedOffRcsY"] = Settings.Aimbot.SawnedOffPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["SawnedOffCurve"] = Settings.Aimbot.SawnedOffCurve.ToString();
            configData["Aimbot"]["SawnedOffCurveX"] = Settings.Aimbot.SawnedOffCurveX.ToString();
            configData["Aimbot"]["SawnedOffCurveY"] = Settings.Aimbot.SawnedOffCurveY.ToString();
            //MAG7
            configData["Aimbot"]["MAG7Fov"] = Settings.Aimbot.MAG7Fov.ToString();
            configData["Aimbot"]["MAG7Bone"] = Settings.Aimbot.MAG7Bone.ToString();
            configData["Aimbot"]["MAG7Smooth"] = Settings.Aimbot.MAG7Smooth.ToString();
            configData["Aimbot"]["MAG7RecoilControl"] = Settings.Aimbot.MAG7RecoilControl.ToString();
            configData["Aimbot"]["MAG7RcsX"] = Settings.Aimbot.MAG7YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["MAG7RcsY"] = Settings.Aimbot.MAG7PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["MAG7Curve"] = Settings.Aimbot.MAG7Curve.ToString();
            configData["Aimbot"]["MAG7CurveX"] = Settings.Aimbot.MAG7CurveX.ToString();
            configData["Aimbot"]["MAG7CurveY"] = Settings.Aimbot.MAG7CurveY.ToString();
            //M249
            configData["Aimbot"]["M249Fov"] = Settings.Aimbot.M249Fov.ToString();
            configData["Aimbot"]["M249Bone"] = Settings.Aimbot.M249Bone.ToString();
            configData["Aimbot"]["M249Smooth"] = Settings.Aimbot.M249Smooth.ToString();
            configData["Aimbot"]["M249RecoilControl"] = Settings.Aimbot.M249RecoilControl.ToString();
            configData["Aimbot"]["M249RcsX"] = Settings.Aimbot.M249YawRecoilReductionFactory.ToString();
            configData["Aimbot"]["M249RcsY"] = Settings.Aimbot.M249PitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["M249Curve"] = Settings.Aimbot.M249Curve.ToString();
            configData["Aimbot"]["M249CurveX"] = Settings.Aimbot.M249CurveX.ToString();
            configData["Aimbot"]["M249CurveY"] = Settings.Aimbot.M249CurveY.ToString();
            //Negev
            configData["Aimbot"]["NegevFov"] = Settings.Aimbot.NegevFov.ToString();
            configData["Aimbot"]["NegevBone"] = Settings.Aimbot.NegevBone.ToString();
            configData["Aimbot"]["NegevSmooth"] = Settings.Aimbot.NegevSmooth.ToString();
            configData["Aimbot"]["NegevRecoilControl"] = Settings.Aimbot.NegevRecoilControl.ToString();
            configData["Aimbot"]["NegevRcsX"] = Settings.Aimbot.NegevYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["NegevRcsY"] = Settings.Aimbot.NegevPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["NegevCurve"] = Settings.Aimbot.NegevCurve.ToString();
            configData["Aimbot"]["NegevCurveX"] = Settings.Aimbot.NegevCurveX.ToString();
            configData["Aimbot"]["NegevCurveY"] = Settings.Aimbot.NegevCurveY.ToString();
            //Unknown
            configData["Aimbot"]["UnknownAutoPistol"] = Settings.Aimbot.UnknownAutoPistol.ToString();
            configData["Aimbot"]["UnknownFov"] = Settings.Aimbot.UnknownFov.ToString();
            configData["Aimbot"]["UnknownBone"] = Settings.Aimbot.UnknownBone.ToString();
            configData["Aimbot"]["UnknownSmooth"] = Settings.Aimbot.UnknownSmooth.ToString();
            configData["Aimbot"]["UnknownRecoilControl"] = Settings.Aimbot.UnknownRecoilControl.ToString();
            configData["Aimbot"]["UnknownRcsX"] = Settings.Aimbot.UnknownYawRecoilReductionFactory.ToString();
            configData["Aimbot"]["UnknownRcsY"] = Settings.Aimbot.UnknownPitchRecoilReductionFactory.ToString();
            configData["Aimbot"]["UnknownCurve"] = Settings.Aimbot.UnknownCurve.ToString();
            configData["Aimbot"]["UnknownCurveX"] = Settings.Aimbot.UnknownCurveX.ToString();
            configData["Aimbot"]["UnknownCurveY"] = Settings.Aimbot.UnknownCurveY.ToString();



            configData["Radar"]["Enabled"]                      = Settings.Radar.Enabled.ToString();

            configData["Air"]["BunnyHop"]                   = Settings.Bunnyhop.Enabled.ToString();
            configData["Air"]["Key"]                       = Settings.Bunnyhop.Key.ToString();
            configData["Air"]["AutoStrafe"]                = Settings.Bunnyhop.AutoStrafe.ToString();
            configData["Air"]["StrafeEmulator"]            = Settings.Bunnyhop.StrafeEmulator.ToString();
            configData["Air"]["StrafeEmulatorKey"]          = Settings.Bunnyhop.StrafeEmulatorKey.ToString();
            configData["Air"]["StrafeEmulatorSensitivity"] = Settings.Bunnyhop.sens.ToString();
            configData["Air"]["StrafeEmulatorSpeed"] = Settings.Bunnyhop.speed.ToString();

            configData["AimAssist"]["Enabled"] = Settings.AimAssist.Enabled.ToString();
            configData["AimAssist"]["Key"] = Settings.AimAssist.Key.ToString();

            configData["Sonar"]["Enabled"] = Settings.Sonar.Enabled.ToString();
            configData["Sonar"]["Key"] = Settings.Sonar.Key.ToString();
            configData["Sonar"]["Interval"] = Settings.Sonar.interval.ToString();

            configData["Trigger"]["Enabled"]                    = Settings.Trigger.Enabled.ToString();
            configData["Trigger"]["Key"]                        = Settings.Trigger.Key.ToString();
            configData["Trigger"]["Delay"]                      = Settings.Trigger.Delay.ToString();
            configData["Trigger"]["DelayBetweenShots"] = Settings.Trigger.DelayBetweenShots.ToString();

            configData["Chams"]["Enabled"] = Settings.Chams.Enabled.ToString();
            configData["Chams"]["Color_R"] = Settings.Chams.Color_R.ToString();
            configData["Chams"]["Color_G"] = Settings.Chams.Color_G.ToString();
            configData["Chams"]["Color_B"] = Settings.Chams.Color_B.ToString();
            configData["Chams"]["Allies"] = Settings.Chams.Allies.ToString();
            configData["Chams"]["Allies_Color_R"] = Settings.Chams.Allies_Color_R.ToString();
            configData["Chams"]["Allies_Color_G"] = Settings.Chams.Allies_Color_G.ToString();
            configData["Chams"]["Allies_Color_B"] = Settings.Chams.Allies_Color_B.ToString();

            configData["Glow"]["Enabled"]                       = Settings.Glow.Enabled.ToString();
            configData["Glow"]["bSpotted"] = Settings.Glow.bSpotted.ToString();
            configData["Glow"]["HealthBased"]               = Settings.Glow.HealthBased.ToString();
            configData["Glow"]["FullBloom"]                     = Settings.Glow.FullBloom.ToString();
            configData["Glow"]["ShowWeapons"] = Settings.Glow.ShowWeapons.ToString();
            configData["Glow"]["Enemies"] = Settings.Glow.Enemies.ToString();
            configData["Glow"]["Enemies_Color_R"] = Settings.Glow.Enemies_Color_R.ToString();
            configData["Glow"]["Enemies_Color_G"] = Settings.Glow.Enemies_Color_G.ToString();
            configData["Glow"]["Enemies_Color_B"] = Settings.Glow.Enemies_Color_B.ToString();
            configData["Glow"]["Enemies_Color_A"] = Settings.Glow.Enemies_Color_A.ToString();
            configData["Glow"]["InvisibleEnemies_Color_R"] = Settings.Glow.InvisibleEnemies_Color_R.ToString();
            configData["Glow"]["InvisibleEnemies_Color_G"] = Settings.Glow.InvisibleEnemies_Color_G.ToString();
            configData["Glow"]["InvisibleEnemies_Color_B"] = Settings.Glow.InvisibleEnemies_Color_B.ToString();
            configData["Glow"]["InvisibleEnemies_Color_A"] = Settings.Glow.InvisibleEnemies_Color_A.ToString();


            configData["Glow"]["Allies"]                        = Settings.Glow.Allies.ToString();
            configData["Glow"]["Allies_Color_R"]                = Settings.Glow.Allies_Color_R.ToString();
            configData["Glow"]["Allies_Color_G"]                = Settings.Glow.Allies_Color_G.ToString();
            configData["Glow"]["Allies_Color_B"]                = Settings.Glow.Allies_Color_B.ToString();
            configData["Glow"]["Allies_Color_A"]                = Settings.Glow.Allies_Color_A.ToString();



            configData["Glow"]["Snipers"]                       = Settings.Glow.Snipers.ToString();
            configData["Glow"]["Snipers_Color_R"]               = Settings.Glow.Snipers_Color_R.ToString();
            configData["Glow"]["Snipers_Color_G"]               = Settings.Glow.Snipers_Color_G.ToString();
            configData["Glow"]["Snipers_Color_B"]               = Settings.Glow.Snipers_Color_B.ToString();
            configData["Glow"]["Snipers_Color_A"]               = Settings.Glow.Snipers_Color_A.ToString();

            configData["Glow"]["Rifles"]                        = Settings.Glow.Rifles.ToString();
            configData["Glow"]["Rifles_Color_R"]                = Settings.Glow.Rifles_Color_R.ToString();
            configData["Glow"]["Rifles_Color_G"]                = Settings.Glow.Rifles_Color_G.ToString();
            configData["Glow"]["Rifles_Color_B"]                = Settings.Glow.Rifles_Color_B.ToString();
            configData["Glow"]["Rifles_Color_A"]                = Settings.Glow.Rifles_Color_A.ToString();

            configData["Glow"]["Heavy"]                   = Settings.Glow.Heavy.ToString();
            configData["Glow"]["Heavy_Color_R"]           = Settings.Glow.Heavy_Color_R.ToString();
            configData["Glow"]["Heavy_Color_G"]           = Settings.Glow.Heavy_Color_G.ToString();
            configData["Glow"]["Heavy_Color_B"]           = Settings.Glow.Heavy_Color_B.ToString();
            configData["Glow"]["Heavy_Color_A"]           = Settings.Glow.Heavy_Color_A.ToString();

            configData["Glow"]["MPs"]                           = Settings.Glow.MPs.ToString();
            configData["Glow"]["MPs_Color_R"]                   = Settings.Glow.MPs_Color_R.ToString();
            configData["Glow"]["MPs_Color_G"]                   = Settings.Glow.MPs_Color_G.ToString();
            configData["Glow"]["MPs_Color_B"]                   = Settings.Glow.MPs_Color_B.ToString();
            configData["Glow"]["MPs_Color_A"]                   = Settings.Glow.MPs_Color_A.ToString();

            configData["Glow"]["Pistols"]                       = Settings.Glow.Pistols.ToString();
            configData["Glow"]["Pistols_Color_R"]               = Settings.Glow.Pistols_Color_R.ToString();
            configData["Glow"]["Pistols_Color_G"]               = Settings.Glow.Pistols_Color_G.ToString();
            configData["Glow"]["Pistols_Color_B"]               = Settings.Glow.Pistols_Color_B.ToString();
            configData["Glow"]["Pistols_Color_A"]               = Settings.Glow.Pistols_Color_A.ToString();

            configData["Glow"]["C4"]                            = Settings.Glow.C4.ToString();
            configData["Glow"]["C4_Color_R"]                    = Settings.Glow.C4_Color_R.ToString();
            configData["Glow"]["C4_Color_G"]                    = Settings.Glow.C4_Color_G.ToString();
            configData["Glow"]["C4_Color_B"]                    = Settings.Glow.C4_Color_B.ToString();
            configData["Glow"]["C4_Color_A"]                    = Settings.Glow.C4_Color_A.ToString();

            configData["Glow"]["Grenades"]                      = Settings.Glow.Grenades.ToString();
            configData["Glow"]["Grenades_Color_R"]              = Settings.Glow.Grenades_Color_R.ToString();
            configData["Glow"]["Grenades_Color_G"]              = Settings.Glow.Grenades_Color_G.ToString();
            configData["Glow"]["Grenades_Color_B"]              = Settings.Glow.Grenades_Color_B.ToString();
            configData["Glow"]["Grenades_Color_A"]              = Settings.Glow.Grenades_Color_A.ToString();

            configData["Toggles"]["Menu"] = Settings.OtherControls.ToggleMenu.ToString();
            configData["Toggles"]["Exit"] = Settings.OtherControls.PanicKey.ToString();
            configData["Toggles"]["Glow"] = Settings.OtherControls.ToggleGlow.ToString();
            configData["Toggles"]["Radar"] = Settings.OtherControls.ToggleRadar.ToString();
            configData["Toggles"]["Aimbot"] = Settings.OtherControls.ToggleAimbot.ToString();

            configData["Key list"]["Q"] = Keyboard.VK_Q.ToString();
            configData["Key list"]["W"] = Keyboard.VK_W.ToString();
            configData["Key list"]["E"] = Keyboard.VK_E.ToString();
            configData["Key list"]["R"] = Keyboard.VK_R.ToString();
            configData["Key list"]["T"] = Keyboard.VK_T.ToString();
            configData["Key list"]["Y"] = Keyboard.VK_Y.ToString();
            configData["Key list"]["U"] = Keyboard.VK_U.ToString();
            configData["Key list"]["I"] = Keyboard.VK_I.ToString();
            configData["Key list"]["O"] = Keyboard.VK_O.ToString();
            configData["Key list"]["P"] = Keyboard.VK_P.ToString();
            configData["Key list"]["A"] = Keyboard.VK_A.ToString();
            configData["Key list"]["S"] = Keyboard.VK_S.ToString();
            configData["Key list"]["D"] = Keyboard.VK_D.ToString();
            configData["Key list"]["F"] = Keyboard.VK_F.ToString();
            configData["Key list"]["G"] = Keyboard.VK_G.ToString();
            configData["Key list"]["H"] = Keyboard.VK_H.ToString();
            configData["Key list"]["J"] = Keyboard.VK_J.ToString();
            configData["Key list"]["K"] = Keyboard.VK_K.ToString();
            configData["Key list"]["L"] = Keyboard.VK_L.ToString();
            configData["Key list"]["Z"] = Keyboard.VK_Z.ToString();
            configData["Key list"]["X"] = Keyboard.VK_X.ToString();
            configData["Key list"]["C"] = Keyboard.VK_C.ToString();
            configData["Key list"]["V"] = Keyboard.VK_V.ToString();
            configData["Key list"]["B"] = Keyboard.VK_B.ToString();
            configData["Key list"]["N"] = Keyboard.VK_N.ToString();
            configData["Key list"]["M"] = Keyboard.VK_M.ToString();
            configData["Key list"]["Alt"] = Keyboard.VK_MENU.ToString();
            configData["Key list"]["X1 mouse button"] = Keyboard.VK_XBUTTON1.ToString();
            configData["Key list"]["X2 mouse button"] = Keyboard.VK_XBUTTON2.ToString();
            configData["Key list"]["Middle mouse button"] = Keyboard.VK_MBUTTON.ToString();
            configData["Key list"]["Left mouse button"] = Keyboard.VK_LBUTTON.ToString();
            configData["Key list"]["Right mouse button"] = Keyboard.VK_RBUTTON.ToString();
            configData["Key list"]["Caps Lock"] = Keyboard.VK_CAPITAL.ToString();
            configData["Key list"]["Ctrl"] = Keyboard.VK_CONTROL.ToString();
            configData["Key list"]["Shift"] = Keyboard.VK_SHIFT.ToString();
            configData["Key list"]["Tab"] = Keyboard.VK_TAB.ToString();
            configData["Key list"]["SPACE"] = Keyboard.VK_SPACE.ToString();



            configParser.WriteFile(path, configData);

            Console.Beep(300, 80);
        }
    }
}
