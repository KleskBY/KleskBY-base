namespace Orion.Other
{
    internal static class Checks
    {

        public static bool IsAlive(this int health) => health >= 1 
            && Structs.LocalPlayer.LifeState == (int)Enums.LifeState.Alive;

        public static bool IsMyTeam(this int team) => team == Structs.LocalPlayer.Team;

        public static bool HasTeam(this int team) => team == 2 
            || team == 3;

        public static bool CanBunnyhop => Structs.LocalPlayer.MoveType != (int)Enums.MoveType_t.MOVETYPE_LADDER 
            && Structs.LocalPlayer.MoveType != (int)Enums.MoveType_t.MOVETYPE_FLY 
            && Structs.LocalPlayer.MoveType != (int)Enums.MoveType_t.MOVETYPE_NOCLIP 
            && Structs.LocalPlayer.MoveType != (int)Enums.MoveType_t.MOVETYPE_OBSERVER 
            && Structs.LocalPlayer.Flags != 262 && Structs.LocalPlayer.Flags != 256;

        public static bool IsC4(int id) => id == (int)Enums.ClassIDs.CC4 
            || id == (int)Enums.ClassIDs.CPlantedC4;

        public static bool IsSniper(int id) => id == (int)Enums.ClassIDs.CWeaponAWP
            || id == (int)Enums.ClassIDs.CWeaponSSG08
            || id == (int)Enums.ClassIDs.CWeaponSCAR20
            || id == (int)Enums.ClassIDs.CWeaponG3SG1;

        public static bool IsRifle(int id) => id == (int)Enums.ClassIDs.CAK47
            || id == (int)Enums.ClassIDs.CWeaponM4A1
            || id == (int)Enums.ClassIDs.CWeaponM3
            || id == (int)Enums.ClassIDs.CWeaponSG550
            || id == (int)Enums.ClassIDs.CWeaponSG552
            || id == (int)Enums.ClassIDs.CWeaponSG556
            || id == (int)Enums.ClassIDs.CWeaponAug
            || id == (int)Enums.ClassIDs.CWeaponGalil
            || id == (int)Enums.ClassIDs.CWeaponGalilAR
            || id == (int)Enums.ClassIDs.CWeaponFamas;

        public static bool IsPistol(int id) => id == (int)Enums.ClassIDs.CDEagle
            || id == (int)Enums.ClassIDs.CWeaponCycler
            || id == (int)Enums.ClassIDs.CWeaponFiveSeven
            || id == (int)Enums.ClassIDs.CWeaponTec9
            || id == (int)Enums.ClassIDs.CWeaponUSP
            || id == (int)Enums.ClassIDs.CWeaponP250
            || id == (int)Enums.ClassIDs.CWeaponP228
            || id == (int)Enums.ClassIDs.CWeaponHKP2000
            || id == (int)Enums.ClassIDs.CWeaponGlock
            || id == (int)Enums.ClassIDs.CWeaponElite;

        public static bool IsHeavy(int id) => id == (int)Enums.ClassIDs.CWeaponXM1014
            || id == (int)Enums.ClassIDs.CWeaponNOVA
            || id == (int)Enums.ClassIDs.CWeaponMag7
            || id == (int)Enums.ClassIDs.CWeaponSawedoff
            ||id == (int) Enums.ClassIDs.CWeaponNegev
            || id == (int) Enums.ClassIDs.CWeaponM249;

        public static bool IsMP(int id) => id == (int)Enums.ClassIDs.CWeaponUMP45
            || id == (int)Enums.ClassIDs.CWeaponP90
            || id == (int)Enums.ClassIDs.CWeaponMP9
            || id == (int)Enums.ClassIDs.CWeaponMP7
            || id == (int)Enums.ClassIDs.CWeaponMAC10
            || id == (int)Enums.ClassIDs.CWeaponBizon;

        public static bool IsGrenade(int id) => id == (int)Enums.ClassIDs.CBaseCSGrenade
            || id == (int)Enums.ClassIDs.CBaseCSGrenadeProjectile
            || id == (int)Enums.ClassIDs.CBaseGrenade
            || id == (int)Enums.ClassIDs.CDecoyGrenade
            || id == (int)Enums.ClassIDs.CDecoyProjectile
            || id == (int)Enums.ClassIDs.CHEGrenade
            || id == (int)Enums.ClassIDs.CIncendiaryGrenade
            || id == (int)Enums.ClassIDs.CMolotovProjectile
            || id == (int)Enums.ClassIDs.CMolotovGrenade
            || id == (int)Enums.ClassIDs.CSensorGrenade
            || id == (int)Enums.ClassIDs.CSensorGrenadeProjectile
            || id == (int)Enums.ClassIDs.CSmokeGrenade
            || id == (int)Enums.ClassIDs.CSmokeGrenadeProjectile;
    }
}

