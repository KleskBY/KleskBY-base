using System.Drawing;
using System.Threading;
using Orion.Other;
using Orion.Managers;
using System.IO;
using System;

namespace Orion.Features
{
    internal class Colors
    {
        public static Color FromHealth(float percent)
        {
            if (percent < 0 || percent > 1) return Color.Black;

            int red, green;

            red = percent < 0.5 ? 255 : 255 - (int)(255 * (percent - 0.5) / 0.5);
            green = percent < 0.5 ? (int)(255 * percent) : 255;

            return Color.FromArgb(red, green, 0);
        }
    }

    internal class Glow
    {
        public static void Run()
        {
         //  Thread.Sleep(100);

            while (true) 
            {
                Thread.Sleep(1);
                
                if (!Settings.Glow.Enabled) continue;

                int gObject = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwGlowObjectManager);
                int gCount = MemoryManager.ReadMemory<int>((int)Structs.Base.Client + Offsets.dwGlowObjectManager + 0x4);
                byte[] gEntities = MemoryManager.ReadMemory(gObject, gCount * 0x38);

                for (int i = 0; i < gCount; i++) 
                {
                    int gEntity = Other.Math.GetInt(gEntities, i * 0x38);
                    if (gEntity == 0) continue;
                  
                    int classID = Imports.GetClassID(gEntity);
                    if (classID < 0) continue;

                    if (Settings.Glow.Snipers && Checks.IsSniper(classID) && Settings.Glow.ShowWeapons)
                    {
                        Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));
                        currGlowObject.r = Settings.Glow.Snipers_Color_R / 255f;
                        currGlowObject.g = Settings.Glow.Snipers_Color_G / 255f;
                        currGlowObject.b = Settings.Glow.Snipers_Color_B / 255f;
                        currGlowObject.a = Settings.Glow.Snipers_Color_A / 255f;
                        currGlowObject.m_bRenderWhenOccluded = true;
                        currGlowObject.m_bRenderWhenUnoccluded = false;

                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                    }
                    else if (Settings.Glow.Rifles && Checks.IsRifle(classID) && Settings.Glow.ShowWeapons)
                    {
                        Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));

                        currGlowObject.r = Settings.Glow.Rifles_Color_R / 255f;
                        currGlowObject.g = Settings.Glow.Rifles_Color_G / 255f;
                        currGlowObject.b = Settings.Glow.Rifles_Color_B / 255f;
                        currGlowObject.a = Settings.Glow.Rifles_Color_A / 255f;
                        currGlowObject.m_bRenderWhenOccluded = true;
                        currGlowObject.m_bRenderWhenUnoccluded = false;

                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                    }
                    else if (Settings.Glow.Pistols && Checks.IsPistol(classID) && Settings.Glow.ShowWeapons)
                    {
                        Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));

                        currGlowObject.r = Settings.Glow.Pistols_Color_R / 255f;
                        currGlowObject.g = Settings.Glow.Pistols_Color_G / 255f;
                        currGlowObject.b = Settings.Glow.Pistols_Color_B / 255f;
                        currGlowObject.a = Settings.Glow.Pistols_Color_A / 255f;
                        currGlowObject.m_bRenderWhenOccluded = true;
                        currGlowObject.m_bRenderWhenUnoccluded = false;

                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                    }
                    else if (Settings.Glow.Heavy && Checks.IsHeavy(classID) && Settings.Glow.ShowWeapons)
                    {
                        Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));

                        currGlowObject.r = Settings.Glow.Heavy_Color_R / 255f;
                        currGlowObject.g = Settings.Glow.Heavy_Color_G / 255f;
                        currGlowObject.b = Settings.Glow.Heavy_Color_B / 255f;
                        currGlowObject.a = Settings.Glow.Heavy_Color_A / 255f;
                        currGlowObject.m_bRenderWhenOccluded = true;
                        currGlowObject.m_bRenderWhenUnoccluded = false;

                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                    }
                    else if (Settings.Glow.MPs && Checks.IsMP(classID) && Settings.Glow.ShowWeapons)
                    {
                        Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));

                        currGlowObject.r = Settings.Glow.MPs_Color_R / 255f;
                        currGlowObject.g = Settings.Glow.MPs_Color_G / 255f;
                        currGlowObject.b = Settings.Glow.MPs_Color_B / 255f;
                        currGlowObject.a = Settings.Glow.MPs_Color_A / 255f;
                        currGlowObject.m_bRenderWhenOccluded = true;
                        currGlowObject.m_bRenderWhenUnoccluded = false;

                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                    }
                    else if (Settings.Glow.C4 && Checks.IsC4(classID) && Settings.Glow.ShowWeapons)
                    {
                        Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));

                        currGlowObject.r = Settings.Glow.C4_Color_R / 255f;
                        currGlowObject.g = Settings.Glow.C4_Color_G / 255f;
                        currGlowObject.b = Settings.Glow.C4_Color_B / 255f;
                        currGlowObject.a = Settings.Glow.C4_Color_A / 255f;
                        currGlowObject.m_bRenderWhenOccluded = true;
                        currGlowObject.m_bRenderWhenUnoccluded = false;

                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                    }
                    else if (Settings.Glow.Grenades && Checks.IsGrenade(classID) && Settings.Glow.ShowWeapons)
                    {
                        Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));

                        currGlowObject.r = Settings.Glow.Grenades_Color_R / 255f;
                        currGlowObject.g = Settings.Glow.Grenades_Color_G / 255f;
                        currGlowObject.b = Settings.Glow.Grenades_Color_B / 255f;
                        currGlowObject.a = Settings.Glow.Grenades_Color_A / 255f;
                        currGlowObject.m_bRenderWhenOccluded = true;
                        currGlowObject.m_bRenderWhenUnoccluded = false;

                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                    }
                    else if (Settings.Glow.Allies || Settings.Glow.Enemies && classID == (int)Enums.ClassIDs.CCSPlayer)
                    {
                        Structs.Enemy_t glowEntity = MemoryManager.ReadMemory<Structs.Enemy_t>(gEntity);
                        if (!glowEntity.Team.HasTeam()) continue; //glowEntity.Dormant ||

                        if (Settings.Glow.Enemies && !glowEntity.Team.IsMyTeam())
                        {
                            ///////////// ENEMY////////////////
                                Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));
                                Structs.Enemy_t entityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(gEntity);


                            if (entityStruct.Team.HasTeam() || !entityStruct.Team.IsMyTeam() || entityStruct.Health.IsAlive()) //|| !entityStruct.Dormant
                            {
                                if (Settings.Glow.bSpotted)
                                {
                                    int EnemySpoted = MemoryManager.ReadMemory<int>(gEntity + Offsets.m_bSpottedByMask);
                                    if (EnemySpoted == 0)
                                    {
                                        currGlowObject.r = Settings.Glow.InvisibleEnemies_Color_R / 255f;
                                        currGlowObject.g = Settings.Glow.InvisibleEnemies_Color_G / 255f;
                                        currGlowObject.b = Settings.Glow.InvisibleEnemies_Color_B / 255f;
                                        currGlowObject.a = Settings.Glow.InvisibleEnemies_Color_A / 255f;
                                        currGlowObject.m_bRenderWhenOccluded = true;
                                        currGlowObject.m_bRenderWhenUnoccluded = false;
                                    }
                                    else
                                    {
                                        if (EnemySpoted % 2 == 0)
                                        {
                                            currGlowObject.r = Settings.Glow.InvisibleEnemies_Color_R / 255f;
                                            currGlowObject.g = Settings.Glow.InvisibleEnemies_Color_G / 255f;
                                            currGlowObject.b = Settings.Glow.InvisibleEnemies_Color_B / 255f;
                                            currGlowObject.a = Settings.Glow.InvisibleEnemies_Color_A / 255f;
                                            currGlowObject.m_bRenderWhenOccluded = true;
                                            currGlowObject.m_bRenderWhenUnoccluded = false;
                                        }
                                        else
                                        {
                                            if (Settings.Glow.HealthBased == true)
                                            {
                                                Color color = Colors.FromHealth(glowEntity.Health / 100f);

                                                currGlowObject.r = color.R / 255f;
                                                currGlowObject.g = color.G / 255f;
                                                currGlowObject.b = color.B / 255f;
                                                currGlowObject.a = Settings.Glow.Enemies_Color_A / 255f;
                                                currGlowObject.m_bRenderWhenOccluded = true;
                                                currGlowObject.m_bRenderWhenUnoccluded = false;

                                                if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                                                MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                                            }
                                            else
                                            {
                                                currGlowObject.r = Settings.Glow.Enemies_Color_R / 255f;
                                                currGlowObject.g = Settings.Glow.Enemies_Color_G / 255f;
                                                currGlowObject.b = Settings.Glow.Enemies_Color_B / 255f;
                                                currGlowObject.a = Settings.Glow.Enemies_Color_A / 255f;
                                                currGlowObject.m_bRenderWhenOccluded = true;
                                                currGlowObject.m_bRenderWhenUnoccluded = false;
                                                if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                                                MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                                            }
                                        }
                                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                                    }
                                }
                                else
                                {
                                    if (Settings.Glow.HealthBased == true)
                                    {
                                        Color color = Colors.FromHealth(glowEntity.Health / 100f);

                                        currGlowObject.r = color.R / 255f;
                                        currGlowObject.g = color.G / 255f;
                                        currGlowObject.b = color.B / 255f;
                                        currGlowObject.a = Settings.Glow.Enemies_Color_A / 255f;
                                        currGlowObject.m_bRenderWhenOccluded = true;
                                        currGlowObject.m_bRenderWhenUnoccluded = false;

                                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                                    }
                                    else
                                    {
                                        currGlowObject.r = Settings.Glow.InvisibleEnemies_Color_R / 255f;
                                        currGlowObject.g = Settings.Glow.InvisibleEnemies_Color_G / 255f;
                                        currGlowObject.b = Settings.Glow.InvisibleEnemies_Color_B / 255f;
                                        currGlowObject.a = Settings.Glow.Enemies_Color_A / 255f;
                                        currGlowObject.m_bRenderWhenOccluded = true;
                                        currGlowObject.m_bRenderWhenUnoccluded = false;
                                        if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                                        MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                                    }
                                }

                                if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;
                                MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                            }
                        }

                        if (Settings.Glow.Allies && glowEntity.Team.IsMyTeam() && glowEntity.Health.IsAlive())
                        {
                            /*         if (Settings.Glow.HealthBased == true)
                                     {
                                         Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));

                                         Color color = Colors.FromHealth(glowEntity.Health / 100f);

                                         currGlowObject.r = color.R / 255f;
                                         currGlowObject.g = color.G / 255f;
                                         currGlowObject.b = color.B / 255f;
                                         currGlowObject.a = Settings.Glow.Allies_Color_A / 255f;
                                         currGlowObject.m_bRenderWhenOccluded = true;
                                         currGlowObject.m_bRenderWhenUnoccluded = false;

                                         if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                                         MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                                     }
                                     else if (Settings.Glow.HealthBased == false)
                                     {*/
                                Structs.Glow_t currGlowObject = MemoryManager.ReadMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4));

                                currGlowObject.r = Settings.Glow.Allies_Color_R / 255f;
                                currGlowObject.g = Settings.Glow.Allies_Color_G / 255f;
                                currGlowObject.b = Settings.Glow.Allies_Color_B / 255f;
                                currGlowObject.a = Settings.Glow.Allies_Color_A / 255f;
                                currGlowObject.m_bRenderWhenOccluded = true;
                                currGlowObject.m_bRenderWhenUnoccluded = false;

                                if (Settings.Glow.FullBloom) currGlowObject.m_bFullBloom = true;

                                MemoryManager.WriteMemory<Structs.Glow_t>((gObject + (i * 0x38) + 0x4), currGlowObject);
                          //  }
                        }
                    }
                }
            }
        }
    }
}
