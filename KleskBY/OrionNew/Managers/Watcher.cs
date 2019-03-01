using System;
using System.Threading;
using System.Windows.Forms;
using Orion.Other;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Orion.Features;
using Orion.Managers;
using System.Diagnostics;



namespace Orion.Managers
{
    internal class Watcher
    {

        private static bool chat;
      //  private static bool checker = true;
        public static bool Menu = true;
      //  public static string GameDir;
        
        //     public static Form ActiveForm;
        //     public static Form lastOpenedForm;
        [STAThread]
        public static void Run()
        {
            
            while (true)
            {
                Thread.Sleep(75);
                {
                    var shit = Process.GetProcessesByName("csgo");

                    if(shit.Length < 1)
                    {

                        foreach (Form f in Application.OpenForms)
                        {
                                f.Hide();
                        }
                        Menu = false;
                        Thread.Sleep(10);
                            Settings.Chams.Color_R = 255;
                            Settings.Chams.Color_G = 255;
                            Settings.Chams.Color_B = 255;
                            Settings.Chams.Allies_Color_R = 255;
                            Settings.Chams.Allies_Color_G = 255;
                            Settings.Chams.Allies_Color_B = 255;
                            Thread.Sleep(80);
                            Settings.Chams.Color_R = 255;
                            Settings.Chams.Color_G = 255;
                            Settings.Chams.Color_B = 255;
                            Settings.Chams.Allies_Color_R = 255;
                            Settings.Chams.Allies_Color_G = 255;
                            Settings.Chams.Allies_Color_B = 255;
                         //   ThreadManager.PauseThread("Chams");
                            Console.Beep(100, 75);
                            Environment.Exit(0);
                    }
                }

                if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.OtherControls.ToggleGlow) & 0x8000))
                {
                    ThreadManager.ToggleThread("Glow");
                    Thread.Sleep(150);
                }

                if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.OtherControls.ToggleRadar) & 0x8000))
                {
                    ThreadManager.ToggleThread("Radar");
                    Thread.Sleep(150);
                }

                if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.OtherControls.ToggleAimbot) & 0x8000))
                {
                    ThreadManager.ToggleThread("Aimbot");
                    Thread.Sleep(150);
                }



                if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.OtherControls.PanicKey) & 0x8000))
                {
                    foreach (Form f in Application.OpenForms)
                    {
                            f.Hide();
                    }
                    Settings.Chams.Color_R = 255;
                    Settings.Chams.Color_G = 255;
                    Settings.Chams.Color_B = 255;
                    Settings.Chams.Allies_Color_R = 255;
                    Settings.Chams.Allies_Color_G = 255;
                    Settings.Chams.Allies_Color_B = 255;
                    Settings.Chams.Color_R = 255;
                    Settings.Chams.Color_G = 255;
                    Settings.Chams.Color_B = 255;
                    Thread.Sleep(80);
                    Settings.Chams.Color_R = 255;
                    Settings.Chams.Color_G = 255;
                    Settings.Chams.Color_B = 255;
                    Settings.Chams.Allies_Color_R = 255;
                    Settings.Chams.Allies_Color_G = 255;
                    Settings.Chams.Allies_Color_B = 255;
                    Settings.Chams.Color_R = 255;
                    Settings.Chams.Color_G = 255;
                    Settings.Chams.Color_B = 255;
                    ThreadManager.PauseThread("Chams");
                    Console.Beep(100, 100);
                    Application.Exit();
                    Environment.Exit(0);
                }

                if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Settings.OtherControls.ToggleMenu) & 0x8000))
                {
                    if (Menu)
                    {
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Name != "Overlay_SharpDX")
                            { 
                                f.Hide();
                                Imports.SetForegroundWindow(menu.GameProcess.MainWindowHandle);
                            }
                        }
                            Menu = false;
                            Thread.Sleep(100);
                    }
                    else
                    {
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Name != "myupdate" && f.Name != "Login")
                            {
                                f.Show();
                                f.Activate();
                                Thread.Sleep(1);
                                f.Activate();
                                Thread.Sleep(5);
                                f.Activate();
                                Thread.Sleep(5);
                                f.Activate();
                            }
                        }
                        Menu = true;
                        Thread.Sleep(100);
                    }
                }


                if (Settings.Bunnyhop.Enabled)
                {
                    if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Keyboard.VK_Y) & 0x8000) || (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Keyboard.VK_U) & 0x8000)))
                    {
                        ThreadManager.PauseThread("Bunnyhop");
                        chat = true;
                    }
                    if (chat == true)
                    {
                        if (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Keyboard.VK_RETURN) & 0x8000) || (Convert.ToBoolean((long)Imports.GetAsyncKeyState(Keyboard.VK_ESCAPE) & 0x8000)))
                        {
                            ThreadManager.ToggleThread("Bunnyhop");
                            chat = false;
                        }
                    }
                }
            }
        }
    }
}

