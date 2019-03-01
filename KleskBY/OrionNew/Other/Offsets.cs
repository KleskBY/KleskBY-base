using System;
using System.Collections.Generic;

using Orion.Managers;

namespace Orion.Other
{
    internal class Offsets
    {
        public static List<string> ScanPatterns()
        {
            List<string> outdatedSignatures = new List<string> { };

            dwGlowObjectManager = MemoryManager.ScanPattern((int)Structs.Base.Client, "A1????????A801754B", 1, 4, true);
            dwEntityList = MemoryManager.ScanPattern((int)Structs.Base.Client, "BB????????83FF010F8C????????3BF8", 1, 0, true);
            dwClientState = MemoryManager.ScanPattern((int)Structs.Base.Engine, "A1????????33D26A006A0033C989B0", 1, 0, true);
            dwForceAttack = MemoryManager.ScanPattern((int)Structs.Base.Client, "890D????????8B0D????????8BF28BC183CE04", 2, 0, true);
            dwForceAttack2 = MemoryManager.ScanPattern((int)Structs.Base.Client, "890D????8B0D????8BF28BC183CE04", 2, 0, true);
            dwForceJump = MemoryManager.ScanPattern((int)Structs.Base.Client, "8B0D????????8BD68BC183CA02", 2, 0, true); 
            dwLocalPlayer = MemoryManager.ScanPattern((int)Structs.Base.Client, "8D3485????????8915????????8B41088B480483F9FF", 3, 4, true);
            dwRadarBase = MemoryManager.ScanPattern((int)Structs.Base.Client, "A1????????8B0CB08B01FF50??463B35????????7CEA8B0D", 1, 0, true);
            return outdatedSignatures;
        }

        public const Int32 m_hActiveWeapon = hazedumper.netvars.m_hActiveWeapon;
        public const Int32 m_ArmorValue = hazedumper.netvars.m_ArmorValue;
        public const Int32 m_Collision = hazedumper.netvars.m_Collision;
        public const Int32 m_CollisionGroup = hazedumper.netvars.m_CollisionGroup;
        public const Int32 m_Local = hazedumper.netvars.m_Local;
        public const Int32 m_MoveType = hazedumper.netvars.m_MoveType;
        public const Int32 m_OriginalOwnerXuidHigh = hazedumper.netvars.m_OriginalOwnerXuidHigh;
        public const Int32 m_OriginalOwnerXuidLow = hazedumper.netvars.m_OriginalOwnerXuidLow;
        public const Int32 m_aimPunchAngle = hazedumper.netvars.m_aimPunchAngle;
        public const Int32 m_aimPunchAngleVel = hazedumper.netvars.m_aimPunchAngleVel;
        public const Int32 m_bGunGameImmunity = hazedumper.netvars.m_bGunGameImmunity;
        public const Int32 m_bHasDefuser = hazedumper.netvars.m_bHasDefuser;
        public const Int32 m_bHasHelmet = hazedumper.netvars.m_bHasHelmet;
        public const Int32 m_bInReload = hazedumper.netvars.m_bInReload;
        public const Int32 m_bIsDefusing = hazedumper.netvars.m_bIsDefusing;
        public const Int32 m_bIsScoped = hazedumper.netvars.m_bIsScoped;
        public const Int32 m_bSpotted = hazedumper.netvars.m_bSpotted;
        public const Int32 m_bSpottedByMask = hazedumper.netvars.m_bSpottedByMask;
        public const Int32 m_dwBoneMatrix = hazedumper.netvars.m_dwBoneMatrix;
        public const Int32 m_fAccuracyPenalty = hazedumper.netvars.m_fAccuracyPenalty;
        public const Int32 m_fFlags = hazedumper.netvars.m_fFlags;
        public const Int32 m_flFallbackWear = hazedumper.netvars.m_flFallbackWear;
        public const Int32 m_flFlashDuration = hazedumper.netvars.m_flFlashDuration;
        public const Int32 m_flFlashMaxAlpha = hazedumper.netvars.m_flFlashMaxAlpha;
        public const Int32 m_flNextPrimaryAttack = hazedumper.netvars.m_flNextPrimaryAttack;
        public const Int32 m_hMyWeapons = hazedumper.netvars.m_hMyWeapons;
        public const Int32 m_hObserverTarget = hazedumper.netvars.m_hObserverTarget;
        public const Int32 m_hOwner = hazedumper.netvars.m_hOwner;
        public const Int32 m_hOwnerEntity = hazedumper.netvars.m_hOwnerEntity;
        public const Int32 m_iAccountID = hazedumper.netvars.m_iAccountID;
        public const Int32 m_iClip1 = hazedumper.netvars.m_iClip1;
        public const Int32 m_iCompetitiveRanking = hazedumper.netvars.m_iCompetitiveRanking;
        public const Int32 m_iCompetitiveWins = hazedumper.netvars.m_iCompetitiveWins;
        public const Int32 m_iCrosshairId = hazedumper.netvars.m_iCrosshairId;                                        
        public const Int32 m_iEntityQuality = hazedumper.netvars.m_iEntityQuality;
        public const Int32 m_iFOVStart = hazedumper.netvars.m_iFOVStart;
        public const Int32 m_iGlowIndex = hazedumper.netvars.m_iGlowIndex;
        public const Int32 m_iHealth = hazedumper.netvars.m_iHealth;
        public const Int32 m_iItemDefinitionIndex = hazedumper.netvars.m_iItemDefinitionIndex;
        public const Int32 m_iItemIDHigh = hazedumper.netvars.m_iItemIDHigh;
        public const Int32 m_iObserverMode = hazedumper.netvars.m_iObserverMode;
        public const Int32 m_iShotsFired = hazedumper.netvars.m_iShotsFired;
        public const Int32 m_iState = hazedumper.netvars.m_iState;
        public const Int32 m_iTeamNum = hazedumper.netvars.m_iTeamNum;
        public const Int32 m_lifeState = hazedumper.netvars.m_lifeState;
        public const Int32 m_nFallbackPaintKit = hazedumper.netvars.m_nFallbackPaintKit;
        public const Int32 m_nFallbackSeed = hazedumper.netvars.m_nFallbackSeed;
        public const Int32 m_nFallbackStatTrak = hazedumper.netvars.m_nFallbackStatTrak;
        public const Int32 m_nForceBone = hazedumper.netvars.m_nForceBone;
        public const Int32 m_nTickBase = hazedumper.netvars.m_nTickBase;
        public const Int32 m_rgflCoordinateFrame = hazedumper.netvars.m_rgflCoordinateFrame;
        public const Int32 m_szCustomName = hazedumper.netvars.m_szCustomName;
        public const Int32 m_szLastPlaceName = hazedumper.netvars.m_szLastPlaceName;
        public const Int32 m_vecOrigin = hazedumper.netvars.m_vecOrigin;
        public const Int32 m_vecVelocity = hazedumper.netvars.m_vecVelocity;
        public const Int32 m_vecViewOffset = hazedumper.netvars.m_vecViewOffset;
        public const Int32 m_viewPunchAngle = hazedumper.netvars.m_viewPunchAngle;
        public const Int32 m_clrRender = hazedumper.netvars.m_clrRender;                    
        public const Int32 dwClientState_ViewAngles = hazedumper.signatures.dwClientState_ViewAngles;
        public const Int32 dwClientState_State = hazedumper.signatures.dwClientState_State;
        public const Int32 dwClientState_MaxPlayer = hazedumper.signatures.dwClientState_MaxPlayer;
        public static Int32 dwGlowObjectManager = hazedumper.signatures.dwGlowObjectManager;
        public static Int32 dwEntityList = hazedumper.signatures.dwEntityList;
        public static Int32 dwClientState = hazedumper.signatures.dwClientState;
        public static Int32 dwForceJump = hazedumper.signatures.dwForceJump;
        public static Int32 dwLocalPlayer = hazedumper.signatures.dwLocalPlayer;
        public static Int32 dwRadarBase = hazedumper.signatures.dwRadarBase;
        public static Int32 dwGlobalVars = hazedumper.signatures.dwGlobalVars;
        public static Int32 dwPlayerResource = hazedumper.signatures.dwPlayerResource;
        public static Int32 dwClientState_GetLocalPlayer = hazedumper.signatures.dwClientState_GetLocalPlayer;
        public static Int32 dwForceAttack = hazedumper.signatures.dwForceAttack;
        public static Int32 dwForceAttack2 = hazedumper.signatures.dwForceAttack2;
        public const Int32 dwSensitivity = hazedumper.signatures.dwSensitivity;
        public const Int32 dwForceLeft = hazedumper.signatures.dwForceLeft;
        public const Int32 dwForceRight = hazedumper.signatures.dwForceRight;
        public const Int32 dwViewMatrix = hazedumper.signatures.dwViewMatrix;
        public const Int32 m_dwRadarBasePointer = 0x6C;
        public const Int32 m_dwRadarStructSize = 0x168;
        public const Int32 m_dwRadarStructPos = 0x18;

        public const Int32 IsMenu = 0x8AC77C;
    }
}
