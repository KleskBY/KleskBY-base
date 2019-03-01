using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Orion.Other
{
    internal class Structs
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct Glow_t
        {
            public float r; //0x4
            public float g; //0x8
            public float b; //0xC
            public float a; //0x10

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] junk1;

            public bool m_bRenderWhenOccluded;      //0x24
            public bool m_bRenderWhenUnoccluded;    //0x25
            public bool m_bFullBloom;               //0x26
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct LocalPlayer_t
        {
            [FieldOffset(Offsets.m_lifeState)]
            public int LifeState;

            [FieldOffset(Offsets.m_iHealth)]
            public int Health;

            [FieldOffset(Offsets.m_fFlags)]
            public int Flags;

            [FieldOffset(Offsets.m_iTeamNum)]
            public int Team;

            [FieldOffset(Offsets.m_iShotsFired)]
            public int ShotsFired;

            [FieldOffset(Offsets.m_iCrosshairId)]
            public int CrosshairID;


            [FieldOffset(Offsets.m_MoveType)]
            public int MoveType;

            [FieldOffset(Offsets.m_vecOrigin)]
            public Vector3 Position;

            [FieldOffset(Offsets.m_aimPunchAngle)]
            public Vector3 AimPunch;

            [FieldOffset(Offsets.m_vecViewOffset)]
            public Vector3 VecView;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Enemy_Crosshair_t
        {
            [FieldOffset(Offsets.m_iHealth)]
            public int Health;

            [FieldOffset(Offsets.m_iTeamNum)]
            public int Team;

        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Enemy_t
        {
            [FieldOffset(Offsets.m_iHealth)]
            public int Health;

            [FieldOffset(Offsets.m_iTeamNum)]
            public int Team;

            [FieldOffset(Offsets.m_bSpotted)]
            public bool Spotted;

            [FieldOffset(Offsets.m_vecOrigin)]
            public Vector3 Position;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct ClientState_t
        {
            [FieldOffset(Offsets.dwClientState_State)]
            public int GameState;

            [FieldOffset(Offsets.dwClientState_MaxPlayer)]
            public int MaxPlayers;

            [FieldOffset(Offsets.dwClientState_ViewAngles)]
            public Vector3 ViewAngles;
        }

        public struct Base
        {
            public static IntPtr Client { get; set; }
            public static IntPtr Engine { get; set; }
        }

        public struct LocalPlayer
        {
            public static int Base { get; set; }
            public static int LifeState { get; set; }
            public static int Health { get; set; }
            public static bool Dormant { get; set; }
            public static int Flags { get; set; }
            public static int MoveType { get; set; }
            public static int Team { get; set; }
            public static int ShotsFired { get; set; }
            public static int CrosshairID { get; set; }
            public static Vector3 Position { get; set; }
            public static Vector3 AimPunch { get; set; }
            public static Vector3 VecView { get; set; }
        }

        public struct Enemy
        {
            public int Base { get; set; }
            public int Health { get; set; }
            public int Team { get; set; }
            public bool Dormant { get; set; }
            public bool Spotted { get; set; }
            public Vector3 Position { get; set; }
        }

        public struct ClientState
        {
            public static int Base { get; set; }
            public static int GameState { get; set; }
            public static int MaxPlayers { get; set; }
            public static Vector3 ViewAngles { get; set; }
        }

        public struct ChamsObject
        {
            public byte r;
            public byte g;
            public byte b;
            public byte a;
        }
    }
}
