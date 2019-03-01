namespace Orion.Other
{
    internal class Settings
    {
        public static string UserAgentString = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.33 Safari/537.36";
        public static string username;
        public static string version = "1";

        public struct OtherControls
        {
            public static int ToggleAimbot = Keyboard.VK_F6;
            public static int ToggleTrigger = Keyboard.VK_F7;
            public static int ToggleGlow = Keyboard.VK_F8;
            public static int ToggleRadar = Keyboard.VK_F9;
            public static int ToggleBunnyhop = Keyboard.VK_F4;
            public static int ToggleMenu = Keyboard.VK_INSERT;
            public static int PanicKey = Keyboard.VK_HOME;
        }

        public struct Radar
        {
            public static bool Enabled = true;
        }

        public struct Bunnyhop
        {
            public static bool Enabled = true;
            public static int Key = Keyboard.VK_SPACE;
            public static bool AutoStrafe = false;
            public static bool StrafeEmulator = false;
            public static int StrafeEmulatorKey = Keyboard.VK_MBUTTON;
            public static int sens = 15;
            public static int speed = 15;
        }

        public struct Trigger
        {
            public static bool Enabled = true;
            public static int Key = Keyboard.VK_MENU;
            public static int Delay = 0;
            public static int DelayBetweenShots = 140;
        }

        public struct AimAssist
        {
            public static bool Enabled = false;
            public static int Key = Keyboard.VK_E;
            //    public static int Delay = 0;
            //    public static int DelayBetweenShots = 0;
        }

        public struct Chams
        {
            public static bool Enabled = true;
            public static bool HealthBased = false;
            public static byte Color_R = 0;
            public static byte Color_G = 255;
            public static byte Color_B = 0;
            public static byte Save_Color_R = 0;
            public static byte Save_Color_G = 0;
            public static byte Save_Color_B = 0;

            public static bool Allies = false;
            public static byte Allies_Color_R = 255;
            public static byte Allies_Color_G = 255;
            public static byte Allies_Color_B = 255;
            public static byte Save_Allies_Color_R = 0;
            public static byte Save_Allies_Color_G = 0;
            public static byte Save_Allies_Color_B = 0;

        }

        public struct Sonar
        {
            public static bool Enabled = false;
            public static int Key = Keyboard.VK_N;
            public static int interval = 100;
        }
        public struct Esp
        {
            public static bool Enabled = false;
            public static bool Windowed = false;
            public static bool bSpotted = true;
            public static int Width = 2;
            public static int Health = 3;
            public static bool DotEsp = true;
            public static bool Name = true;
            public static bool Distance = true;
            public static int Color_R = 5;
            public static int Color_G = 5;
            public static int Color_B = 5;
            public static int VisableColor_R = 0;
            public static int VisableColor_G = 255;
            public static int VisableColor_B = 0;
            public static int RefreshRate = 10;
        }

        public struct Glow
        {
            public static bool Enabled = true;
            public static bool bSpotted = true;
            public static bool HealthBased = false;
            public static bool FullBloom = false;
            public static bool ShowWeapons = false;

            public static bool Enemies = true;
            public static float Enemies_Color_R = 0;
            public static float Enemies_Color_G = 255;
            public static float Enemies_Color_B = 0;
            public static float Enemies_Color_A = 255;
            public static float InvisibleEnemies_Color_R = 80;
            public static float InvisibleEnemies_Color_G = 30;
            public static float InvisibleEnemies_Color_B = 30;
            public static float InvisibleEnemies_Color_A = 255;

            public static bool Allies = false;
            public static float Allies_Color_R = 39;
            public static float Allies_Color_G = 174;
            public static float Allies_Color_B = 96;
            public static float Allies_Color_A = 255;
            public static float InvisibleAllies_Color_R = 39;
            public static float InvisibleAllies_Color_G = 174;
            public static float InvisibleAllies_Color_B = 96;
            public static float InvisibleAllies_Color_A = 255;

            public static bool Snipers = true;
            public static float Snipers_Color_R = 155;
            public static float Snipers_Color_G = 89;
            public static float Snipers_Color_B = 182;
            public static float Snipers_Color_A = 255;

            public static bool Rifles = true;
            public static float Rifles_Color_R = 52;
            public static float Rifles_Color_G = 152;
            public static float Rifles_Color_B = 219;
            public static float Rifles_Color_A = 255;

            public static bool MPs = true;
            public static float MPs_Color_R = 46;
            public static float MPs_Color_G = 204;
            public static float MPs_Color_B = 113;
            public static float MPs_Color_A = 255;

            public static bool Pistols = true;
            public static float Pistols_Color_R = 236;
            public static float Pistols_Color_G = 240;
            public static float Pistols_Color_B = 241;
            public static float Pistols_Color_A = 255;

            public static bool Heavy = true;
            public static float Heavy_Color_R = 230;
            public static float Heavy_Color_G = 126;
            public static float Heavy_Color_B = 34;
            public static float Heavy_Color_A = 255;

            public static bool C4 = true;
            public static float C4_Color_R = 231;
            public static float C4_Color_G = 76;
            public static float C4_Color_B = 60;
            public static float C4_Color_A = 255;

            public static bool Grenades = true;
            public static float Grenades_Color_R = 241;
            public static float Grenades_Color_G = 196;
            public static float Grenades_Color_B = 15;
            public static float Grenades_Color_A = 255;


        }

        public struct Aimbot
        {
            public static bool Enabled = true;
            public static bool VisibleOnly = false;
            public static bool bSpottedCheck = false;
            public static int Key = Keyboard.VK_LBUTTON;
            public static int SecondKey = Keyboard.VK_V;
            public static bool SilentAim = false;
            public static bool AutoPistol = true;
            public static bool KillDelay = true;

            //Moyse event
            public static bool UseMouseEvent = false;
            public static int MouseEventFov = 10;
            public static int MouseEventSmooth = 5;
            public static bool UseMouseEventForAutopistol = false;

            public static float Fov = 10f;
            public static bool UseNearest = false;
            public static int Bone = 6;
            public static float Smooth = 15f;
            public static bool RecoilControl = true;
            public static float YawRecoilReductionFactory = 2f;
            public static float PitchRecoilReductionFactory = 2f;
            public static bool Curve = false;
            public static float CurveY = 2;
            public static float CurveX = 2;
            //GLOCK
            public static bool GlockAutoPistol = true;
            public static float GlockFov = 3f;
            public static int GlockBone = 8;
            public static float GlockSmooth = 3.5f;
            public static bool GlockRecoilControl = true;
            public static float GlockYawRecoilReductionFactory = 1.1f;
            public static float GlockPitchRecoilReductionFactory = 1.1f;
            public static bool GlockCurve = false;
            public static float GlockCurveY = 2;
            public static float GlockCurveX = 2;
            //USP
            public static bool USPAutoPistol = true;
            public static float USPFov = 3f;
            public static int USPBone = 8;
            public static float USPSmooth = 3f;
            public static bool USPRecoilControl = true;
            public static float USPYawRecoilReductionFactory = 1.25f;
            public static float USPPitchRecoilReductionFactory = 1.25f;
            public static bool USPCurve = false;
            public static float USPCurveY = 2;
            public static float USPCurveX = 2;
            //USP FIRST BULLET
            public static bool FirstUSP = true;
            public static float FirstUSPFov = 1.25f;
            public static int FirstUSPBone = 8;
            public static float FirstUSPSmooth = 0f;
            //P2000
            public static bool P2000AutoPistol = true;
            public static float P2000Fov = 3f;
            public static int P2000Bone = 8;
            public static float P2000Smooth = 3f;
            public static bool P2000RecoilControl = true;
            public static float P2000YawRecoilReductionFactory = 1.3f;
            public static float P2000PitchRecoilReductionFactory = 1.3f;
            public static bool P2000Curve = false;
            public static float P2000CurveY = 2;
            public static float P2000CurveX = 2;
            //P250
            public static bool P250AutoPistol = true;
            public static float P250Fov = 3.5f;
            public static int P250Bone = 8;
            public static float P250Smooth = 3f;
            public static bool P250RecoilControl = true;
            public static float P250YawRecoilReductionFactory = 1.3f;
            public static float P250PitchRecoilReductionFactory = 1.3f;
            public static bool P250Curve = false;
            public static float P250CurveY = 2;
            public static float P250CurveX = 2;
            //Duals
            public static bool DualsAutoPistol = true;
            public static float DualsFov = 4f;
            public static int DualsBone = 8;
            public static float DualsSmooth = 3.5f;
            public static bool DualsRecoilControl = true;
            public static float DualsYawRecoilReductionFactory = 1.5f;
            public static float DualsPitchRecoilReductionFactory = 1.5f;
            public static bool DualsCurve = false;
            public static float DualsCurveY = 2;
            public static float DualsCurveX = 2;
            //FiveSeven
            public static bool FiveSevenAutoPistol = false;
            public static float FiveSevenFov = 4f;
            public static int FiveSevenBone = 8;
            public static float FiveSevenSmooth = 5f;
            public static bool FiveSevenRecoilControl = true;
            public static float FiveSevenYawRecoilReductionFactory = 1.7f;
            public static float FiveSevenPitchRecoilReductionFactory = 1.7f;
            public static bool FiveSevenCurve = false;
            public static float FiveSevenCurveY = 2;
            public static float FiveSevenCurveX = 2;
            //TEC9
            public static bool TEC9AutoPistol = true;
            public static float TEC9Fov = 4f;
            public static int TEC9Bone = 8;
            public static float TEC9Smooth = 5f;
            public static bool TEC9RecoilControl = true;
            public static float TEC9YawRecoilReductionFactory = 1.7f;
            public static float TEC9PitchRecoilReductionFactory = 1.7f;
            public static bool TEC9Curve = false;
            public static float TEC9CurveY = 2;
            public static float TEC9CurveX = 2;
            //DEAGLE
            public static bool DEAGLEAutoPistol = false;
            public static float DEAGLEFov = 2.2f;
            public static int DEAGLEBone = 8;
            public static float DEAGLESmooth = 0f;
            public static bool DEAGLERecoilControl = false;
            public static float DEAGLEYawRecoilReductionFactory = 0f;
            public static float DEAGLEPitchRecoilReductionFactory = 0f;
            public static bool DEAGLECurve = false;
            public static float DEAGLECurveY = 2;
            public static float DEAGLECurveX = 2f;
            //Revolver
            public static float RevolverFov = 4f;
            public static int RevolverBone = 8;
            public static float RevolverSmooth = 4f;
            public static bool RevolverRecoilControl = false;
            public static float RevolverYawRecoilReductionFactory = 0f;
            public static float RevolverPitchRecoilReductionFactory = 0f;
            public static bool RevolverCurve = false;
            public static float RevolverCurveY = 2;
            public static float RevolverCurveX = 2;
            //CZ
            public static float CZFov = 4f;
            public static int CZBone = 8;
            public static float CZSmooth = 4f;
            public static bool CZRecoilControl = true;
            public static float CZYawRecoilReductionFactory = 2f;
            public static float CZPitchRecoilReductionFactory = 2f;
            public static bool CZCurve = false;
            public static float CZCurveY = 2;
            public static float CZCurveX = 2;
            //
            // SMGS
            //
            //MAC10
            public static float MAC10Fov = 4f;
            public static int MAC10Bone = 8;
            public static float MAC10Smooth = 6f;
            public static bool MAC10RecoilControl = true;
            public static float MAC10YawRecoilReductionFactory = 2f;
            public static float MAC10PitchRecoilReductionFactory = 2f;
            public static bool MAC10Curve = false;
            public static float MAC10CurveY = 2;
            public static float MAC10CurveX = 2;
            //MP9
            public static float MP9Fov = 4f;
            public static int MP9Bone = 7;
            public static float MP9Smooth = 5f;
            public static bool MP9RecoilControl = true;
            public static float MP9YawRecoilReductionFactory = 2f;
            public static float MP9PitchRecoilReductionFactory = 2f;
            public static bool MP9Curve = false;
            public static float MP9CurveY = 2;
            public static float MP9CurveX = 2;
            //MP7
            public static float MP7Fov = 4f;
            public static int MP7Bone = 7;
            public static float MP7Smooth = 6f;
            public static bool MP7RecoilControl = true;
            public static float MP7YawRecoilReductionFactory = 2f;
            public static float MP7PitchRecoilReductionFactory = 2f;
            public static bool MP7Curve = false;
            public static float MP7CurveY = 2;
            public static float MP7CurveX = 2;
            //UMP
            public static float UMPFov = 4f;
            public static int UMPBone = 7;
            public static float UMPSmooth = 6f;
            public static bool UMPRecoilControl = true;
            public static float UMPYawRecoilReductionFactory = 2f;
            public static float UMPPitchRecoilReductionFactory = 2f;
            public static bool UMPCurve = false;
            public static float UMPCurveY = 2;
            public static float UMPCurveX = 2;
            //Bizon
            public static float BizonFov = 4f;
            public static int BizonBone = 7;
            public static float BizonSmooth = 6f;
            public static bool BizonRecoilControl = true;
            public static float BizonYawRecoilReductionFactory = 2f;
            public static float BizonPitchRecoilReductionFactory = 2f;
            public static bool BizonCurve = false;
            public static float BizonCurveY = 2;
            public static float BizonCurveX = 2;
            //P90
            public static float P90Fov = 4f;
            public static int P90Bone = 7;
            public static float P90Smooth = 5f;
            public static bool P90RecoilControl = true;
            public static float P90YawRecoilReductionFactory = 2f;
            public static float P90PitchRecoilReductionFactory = 2f;
            public static bool P90Curve = false;
            public static float P90CurveY = 2;
            public static float P90CurveX = 2;
            //MP5
            public static float MP5Fov = 4f;
            public static int MP5Bone = 7;
            public static float MP5Smooth = 4.5f;
            public static bool MP5RecoilControl = true;
            public static float MP5YawRecoilReductionFactory = 2f;
            public static float MP5PitchRecoilReductionFactory = 2f;
            public static bool MP5Curve = false;
            public static float MP5CurveY = 2;
            public static float MP5CurveX = 2;
            //
            //RIFLES
            //
            //GALIL
            //Galil
            public static float GalilFov = 4f;
            public static int GalilBone = 7;
            public static float GalilSmooth = 6f;
            public static bool GalilRecoilControl = true;
            public static float GalilYawRecoilReductionFactory = 2f;
            public static float GalilPitchRecoilReductionFactory = 2f;
            public static bool GalilCurve = false;
            public static float GalilCurveY = 2;
            public static float GalilCurveX = 2;
            //Famas
            public static float FamasFov = 4f;
            public static int FamasBone = 7;
            public static float FamasSmooth = 6f;
            public static bool FamasRecoilControl = true;
            public static float FamasYawRecoilReductionFactory = 2f;
            public static float FamasPitchRecoilReductionFactory = 2f;
            public static bool FamasCurve = false;
            public static float FamasCurveY = 2;
            public static float FamasCurveX = 2;
            //AK47
            public static float AK47Fov = 4f;
            public static int AK47Bone = 7;
            public static float AK47Smooth = 6f;
            public static bool AK47RecoilControl = true;
            public static float AK47YawRecoilReductionFactory = 2f;
            public static float AK47PitchRecoilReductionFactory = 2f;
            public static bool AK47Curve = false;
            public static float AK47CurveY = 2;
            public static float AK47CurveX = 2;
            //AK47 FIRST BULLET
            public static bool FirstAK47 = true; 
            public static float FirstAK47Fov = 1.2f;
            public static int FirstAK47Bone = 8;
            public static float FirstAK47Smooth = 0f;
            //M4A$
            public static float M4A4Fov = 4f;
            public static int M4A4Bone = 7;
            public static float M4A4Smooth = 6f;
            public static bool M4A4RecoilControl = true;
            public static float M4A4YawRecoilReductionFactory = 2f;
            public static float M4A4PitchRecoilReductionFactory = 2f;
            public static bool M4A4Curve = false;
            public static float M4A4CurveY = 2;
            public static float M4A4CurveX = 2;
            //M4A4 First bullet
            public static bool FirstM4A4 = true;
            public static float FirstM4A4Fov = 1.2f;
            public static int   FirstM4A4Bone = 8;
            public static float FirstM4A4Smooth = 0f;
            //M4A1
            public static float M4A1Fov = 4f;
            public static int M4A1Bone = 7;
            public static float M4A1Smooth = 6f;
            public static bool M4A1RecoilControl = true;
            public static float M4A1YawRecoilReductionFactory = 2f;
            public static float M4A1PitchRecoilReductionFactory = 2f;
            public static bool M4A1Curve = false;
            public static float M4A1CurveY = 2;
            public static float M4A1CurveX = 2;
            //M4A1 First bullet
            public static bool FirstM4A1 = true;
            public static float FirstM4A1Fov = 1.2f;
            public static int FirstM4A1Bone = 8;
            public static float FirstM4A1Smooth = 0f;
            //SG
            public static float SGFov = 4f;
            public static int SGBone = 8;
            public static float SGSmooth = 6f;
            public static bool SGRecoilControl = true;
            public static float SGYawRecoilReductionFactory = 2f;
            public static float SGPitchRecoilReductionFactory = 2f;
            public static bool SGCurve = false;
            public static float SGCurveY = 2;
            public static float SGCurveX = 2;
            //AUG
            public static float AUGFov = 4f;
            public static int AUGBone = 8;
            public static float AUGSmooth = 6f;
            public static bool AUGRecoilControl = true;
            public static float AUGYawRecoilReductionFactory = 2f;
            public static float AUGPitchRecoilReductionFactory = 2f;
            public static bool AUGCurve = false;
            public static float AUGCurveY = 2;
            public static float AUGCurveX = 2;
            //
            //SNIPERS
            //
            //SSG
            //SSG
            public static float SSGFov = 3f;
            public static int SSGBone = 0;
            public static float SSGSmooth = 0f;
            public static bool SSGRecoilControl = false;
            public static float SSGYawRecoilReductionFactory = 0f;
            public static float SSGPitchRecoilReductionFactory = 0f;
            public static bool SSGCurve = false;
            public static float SSGCurveY = 2;
            public static float SSGCurveX = 2;
            //AWP
            public static float AWPFov = 3f;
            public static int AWPBone = 0;
            public static float AWPSmooth = 0f;
            public static bool AWPRecoilControl = false;
            public static float AWPYawRecoilReductionFactory = 0f;
            public static float AWPPitchRecoilReductionFactory = 0f;
            public static bool AWPCurve = false;
            public static float AWPCurveY = 2;
            public static float AWPCurveX = 2;
            //AUTO
            public static float AUTOFov = 4f;
            public static int AUTOBone = 7;
            public static float AUTOSmooth = 8f;
            public static bool AUTORecoilControl = true;
            public static float AUTOYawRecoilReductionFactory = 1f;
            public static float AUTOPitchRecoilReductionFactory = 1f;
            public static bool AUTOCurve = false;
            public static float AUTOCurveY = 2;
            public static float AUTOCurveX = 2;
            //
            //SHOTGUNS
            //
            //Nova
            public static bool NovaAutoPistol = true;
            public static float NovaFov = 4f;
            public static int NovaBone = 8;
            public static float NovaSmooth = 6f;
            public static bool NovaRecoilControl = false;
            public static float NovaYawRecoilReductionFactory = 1f;
            public static float NovaPitchRecoilReductionFactory = 1f;
            public static bool NovaCurve = false;
            public static float NovaCurveY = 2;
            public static float NovaCurveX = 2;
            //XM
            public static float XMFov = 4f;
            public static int XMBone = 8;
            public static float XMSmooth = 6f;
            public static bool XMRecoilControl = true;
            public static float XMYawRecoilReductionFactory = 1f;
            public static float XMPitchRecoilReductionFactory = 1f;
            public static bool XMCurve = false;
            public static float XMCurveY = 2;
            public static float XMCurveX = 2;
            //Sawned-off
            public static float SawnedOffFov = 4f;
            public static int SawnedOffBone = 8;
            public static float SawnedOffSmooth = 10f;
            public static bool SawnedOffRecoilControl = false;
            public static float SawnedOffYawRecoilReductionFactory = 1f;
            public static float SawnedOffPitchRecoilReductionFactory = 1f;
            public static bool SawnedOffCurve = false;
            public static float SawnedOffCurveY = 2;
            public static float SawnedOffCurveX = 2;
            //MAG7
            public static float MAG7Fov = 4f;
            public static int MAG7Bone = 8;
            public static float MAG7Smooth = 10f;
            public static bool MAG7RecoilControl = false;
            public static float MAG7YawRecoilReductionFactory = 1f;
            public static float MAG7PitchRecoilReductionFactory = 1f;
            public static bool MAG7Curve = false;
            public static float MAG7CurveY = 2;
            public static float MAG7CurveX = 2;
            //
            //MASHIEGUN
            //
            //m249
            public static float M249Fov = 4f;
            public static int M249Bone = 7;
            public static float M249Smooth = 6f;
            public static bool M249RecoilControl = true;
            public static float M249YawRecoilReductionFactory = 2f;
            public static float M249PitchRecoilReductionFactory = 2f;
            public static bool M249Curve = false;
            public static float M249CurveY = 2;
            public static float M249CurveX = 2;
            //Negev
            public static float NegevFov = 4f;
            public static int NegevBone = 7;
            public static float NegevSmooth = 7f;
            public static bool NegevRecoilControl = true;
            public static float NegevYawRecoilReductionFactory = 2f;
            public static float NegevPitchRecoilReductionFactory = 2f;
            public static bool NegevCurve = false;
            public static float NegevCurveY = 2;
            public static float NegevCurveX = 2;
            //Unknown
            public static bool UnknownAutoPistol = false;
            public static float UnknownFov = 10f;
            public static int UnknownBone = 6;
            public static float UnknownSmooth = 7f;
            public static bool UnknownRecoilControl = true;
            public static float UnknownYawRecoilReductionFactory = 2.1f;
            public static float UnknownPitchRecoilReductionFactory = 2.1f;
            public static bool UnknownCurve = false;
            public static float UnknownCurveY = 2;
            public static float UnknownCurveX = 2;
        }
    }
}
