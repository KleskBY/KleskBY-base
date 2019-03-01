using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Orion.Managers;
using Orion.Other;
using Orion.Features;
using Microsoft.Win32;
using System.Security.Cryptography;


/*
             .-----.
            /       `\
          _|_         |
         /   \        | 
         '==='        |
                      |
                      |
         . ' .        |
        . : ' .       |
           '.         |
       . '    .       |
        .-"""-.       |
       /  \___ \      |   <======= ЭТО KLESKBY, Он прийдет и лично выебет каждого кто будет пытатся продать это говно
       |/`    \|      |
       (  a  a )      |
       |   _\ |       |
       )\  =  /       |
   _.-'  '---;        |
 /`           `-.     |
|                \    |
|    |   .  & .   \   |
\    /      &   |  ;  |
|   |           |  ;  |
|   /\          /  |  |
\   \ )   -:-  /\  \  |
 `.  `-.  -:-  | \  \_|
   '-.  `-.    (  './\`\
    / `'-. `\  |    \/_/
    |    \  |  |      |
    |    /'-\  /      |
     \   \   | |      |
      \   )_/\ |      |
       \      \|      |
        \      \      |
         '.     |     |
           /   /      |
          /  .';      |
        /`  /  |      |
       /   /   |      |
      |  .' \  |      |
      /  \  )  |      |
      \   \ /  '-.._  |
       '.ooO\__._.Ooo |
*/

namespace Orion
{
    public partial class menu : Form
    {
        public static Process[] process;
        public static Process GameProcess;
        public static System.Media.SoundPlayer player;
        public static int GlowIndex = 0;
        public bool Fuck = false;
        public static bool abletochat = true;
        public static bool started = false;
        public static string listofmodules;
        public static Form form;
        public static string ChatText = "";


        public static string[] Modules = new string[] // game dlls
        {
                "client_panorama.dll",
                "engine.dll"
        };

        public menu()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);
            InitializeComponent();
        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            try
            {
                if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                    return;

                System.Reflection.PropertyInfo aProp =
                      typeof(System.Windows.Forms.Control).GetProperty(
                            "DoubleBuffered",
                            System.Reflection.BindingFlags.NonPublic |
                            System.Reflection.BindingFlags.Instance);

                aProp.SetValue(c, true, null);
            }
            catch { }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetDoubleBuffered(tabControl1);
            


            this.Text = UI.RandomString(10);
            Imports.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, 0x0001 | 0x0002);
            UI.MenuPointer(panel3, button4, this);
            timer1.Start();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            if (Settings.Glow.Enabled) GlowToggle1.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle1.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.bSpotted) GlowToggle2.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle2.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.Allies) GlowToggle3.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle3.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.ShowWeapons) GlowToggle4.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle4.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.FullBloom) GlowToggle5.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle5.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.HealthBased) GlowToggle6.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle6.BackgroundImage = Properties.Resources.switch6;

            GlowButton1.BackColor = Color.FromArgb(255, (int)Settings.Glow.Enemies_Color_R, (int)Settings.Glow.Enemies_Color_G, (int)Settings.Glow.Enemies_Color_B);
            GlowButton2.BackColor = Color.FromArgb(255, (int)Settings.Glow.InvisibleEnemies_Color_R, (int)Settings.Glow.InvisibleEnemies_Color_G, (int)Settings.Glow.InvisibleEnemies_Color_B);
            GlowButton3.BackColor = Color.FromArgb(255, (int)Settings.Glow.Snipers_Color_R, (int)Settings.Glow.Snipers_Color_G, (int)Settings.Glow.Snipers_Color_B);
            GlowButton4.BackColor = Color.FromArgb(255, (int)Settings.Glow.Rifles_Color_R, (int)Settings.Glow.Rifles_Color_G, (int)Settings.Glow.Rifles_Color_B);
            GlowButton5.BackColor = Color.FromArgb(255, (int)Settings.Glow.MPs_Color_R, (int)Settings.Glow.MPs_Color_G, (int)Settings.Glow.MPs_Color_B);
            GlowButton6.BackColor = Color.FromArgb(255, (int)Settings.Glow.Allies_Color_R, (int)Settings.Glow.Allies_Color_G, (int)Settings.Glow.Allies_Color_B);
            GlowButton7.BackColor = Color.FromArgb(255, (int)Settings.Glow.Pistols_Color_R, (int)Settings.Glow.Pistols_Color_G, (int)Settings.Glow.Pistols_Color_B);
            GlowButton8.BackColor = Color.FromArgb(255, (int)Settings.Glow.Heavy_Color_R, (int)Settings.Glow.Heavy_Color_G, (int)Settings.Glow.Heavy_Color_B);
            GlowButton9.BackColor = Color.FromArgb(255, (int)Settings.Glow.C4_Color_R, (int)Settings.Glow.C4_Color_G, (int)Settings.Glow.C4_Color_B);
            GlowButton10.BackColor = Color.FromArgb(255, (int)Settings.Glow.Grenades_Color_R, (int)Settings.Glow.Grenades_Color_G, (int)Settings.Glow.Grenades_Color_B);


            if (Settings.Bunnyhop.Enabled) MiscToggle1.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle1.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Bunnyhop.AutoStrafe) MiscToggle2.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle2.BackgroundImage = Properties.Resources.switch6;

            if (Settings.AimAssist.Enabled) MiscToggle3.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle3.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Sonar.Enabled) MiscToggle4.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle4.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Trigger.Enabled) MiscToggle5.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle5.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Bunnyhop.StrafeEmulator) MiscToggle6.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle6.BackgroundImage = Properties.Resources.switch6;



            if (Settings.Radar.Enabled) MiscToggle8.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle8.BackgroundImage = Properties.Resources.switch6;

            trackBar9.Value = Settings.Trigger.Delay;
            MiscLabel8.Text = Settings.Trigger.Delay.ToString();

            trackBar10.Value = Settings.Trigger.DelayBetweenShots;
            MiscLabel10.Text = Settings.Trigger.DelayBetweenShots.ToString();

            trackBar12.Value = Settings.Bunnyhop.sens;
            MiscLabel12.Text = Settings.Bunnyhop.sens.ToString();

            trackBar11.Value = Settings.Bunnyhop.speed;
            MiscLabel14.Text = Settings.Bunnyhop.speed.ToString();


            Fuck = true;

            //KEYS//

            {
                if (Settings.Aimbot.Key == Keyboard.VK_LBUTTON) KeyPicker1.SelectedIndex = 0;
                if (Settings.Aimbot.Key == Keyboard.VK_RBUTTON) KeyPicker1.SelectedIndex = 1;
                if (Settings.Aimbot.Key == Keyboard.VK_MBUTTON) KeyPicker1.SelectedIndex = 2;
                if (Settings.Aimbot.Key == Keyboard.VK_XBUTTON1) KeyPicker1.SelectedIndex = 3;
                if (Settings.Aimbot.Key == Keyboard.VK_XBUTTON2) KeyPicker1.SelectedIndex = 4;
                if (Settings.Aimbot.Key == Keyboard.VK_MENU) KeyPicker1.SelectedIndex = 5;
                if (Settings.Aimbot.Key == Keyboard.VK_SHIFT) KeyPicker1.SelectedIndex = 6;
                if (Settings.Aimbot.Key == Keyboard.VK_CAPITAL) KeyPicker1.SelectedIndex = 7;
                if (Settings.Aimbot.Key == Keyboard.VK_V) KeyPicker1.SelectedIndex = 8;
                if (Settings.Aimbot.Key == Keyboard.VK_C) KeyPicker1.SelectedIndex = 9;
                if (Settings.Aimbot.Key == Keyboard.VK_B) KeyPicker1.SelectedIndex = 10;
                if (Settings.Aimbot.Key == Keyboard.VK_F) KeyPicker1.SelectedIndex = 11;
                if (Settings.Aimbot.Key == Keyboard.VK_E) KeyPicker1.SelectedIndex = 12;
                if (Settings.Aimbot.Key == Keyboard.VK_Q) KeyPicker1.SelectedIndex = 13;
                if (Settings.Aimbot.Key == Keyboard.VK_W) KeyPicker1.SelectedIndex = 14;
                if (Settings.Aimbot.Key == Keyboard.VK_R) KeyPicker1.SelectedIndex = 15;
                if (Settings.Aimbot.Key == Keyboard.VK_T) KeyPicker1.SelectedIndex = 16;
                if (Settings.Aimbot.Key == Keyboard.VK_Y) KeyPicker1.SelectedIndex = 17;
                if (Settings.Aimbot.Key == Keyboard.VK_U) KeyPicker1.SelectedIndex = 18;
                if (Settings.Aimbot.Key == Keyboard.VK_I) KeyPicker1.SelectedIndex = 19;
                if (Settings.Aimbot.Key == Keyboard.VK_O) KeyPicker1.SelectedIndex = 20;
                if (Settings.Aimbot.Key == Keyboard.VK_P) KeyPicker1.SelectedIndex = 21;
                if (Settings.Aimbot.Key == Keyboard.VK_G) KeyPicker1.SelectedIndex = 22;
                if (Settings.Aimbot.Key == Keyboard.VK_H) KeyPicker1.SelectedIndex = 23;
                if (Settings.Aimbot.Key == Keyboard.VK_J) KeyPicker1.SelectedIndex = 24;
                if (Settings.Aimbot.Key == Keyboard.VK_K) KeyPicker1.SelectedIndex = 25;
                if (Settings.Aimbot.Key == Keyboard.VK_L) KeyPicker1.SelectedIndex = 26;
                if (Settings.Aimbot.Key == Keyboard.VK_Z) KeyPicker1.SelectedIndex = 27;
                if (Settings.Aimbot.Key == Keyboard.VK_X) KeyPicker1.SelectedIndex = 28;
                if (Settings.Aimbot.Key == Keyboard.VK_N) KeyPicker1.SelectedIndex = 29;
                if (Settings.Aimbot.Key == Keyboard.VK_M) KeyPicker1.SelectedIndex = 30;
            }
            {
                if (Settings.Aimbot.SecondKey == Keyboard.VK_LBUTTON) KeyPicker2.SelectedIndex = 0;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_RBUTTON) KeyPicker2.SelectedIndex = 1;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_MBUTTON) KeyPicker2.SelectedIndex = 2;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_XBUTTON1) KeyPicker2.SelectedIndex = 3;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_XBUTTON2) KeyPicker2.SelectedIndex = 4;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_MENU) KeyPicker2.SelectedIndex = 5;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_SHIFT) KeyPicker2.SelectedIndex = 6;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_CAPITAL) KeyPicker2.SelectedIndex = 7;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_V) KeyPicker2.SelectedIndex = 8;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_C) KeyPicker2.SelectedIndex = 9;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_B) KeyPicker2.SelectedIndex = 10;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_F) KeyPicker2.SelectedIndex = 11;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_E) KeyPicker2.SelectedIndex = 12;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_Q) KeyPicker2.SelectedIndex = 13;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_W) KeyPicker2.SelectedIndex = 14;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_R) KeyPicker2.SelectedIndex = 15;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_T) KeyPicker2.SelectedIndex = 16;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_Y) KeyPicker2.SelectedIndex = 17;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_U) KeyPicker2.SelectedIndex = 18;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_I) KeyPicker2.SelectedIndex = 19;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_O) KeyPicker2.SelectedIndex = 20;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_P) KeyPicker2.SelectedIndex = 21;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_G) KeyPicker2.SelectedIndex = 22;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_H) KeyPicker2.SelectedIndex = 23;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_J) KeyPicker2.SelectedIndex = 24;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_K) KeyPicker2.SelectedIndex = 25;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_L) KeyPicker2.SelectedIndex = 26;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_Z) KeyPicker2.SelectedIndex = 27;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_X) KeyPicker2.SelectedIndex = 28;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_N) KeyPicker2.SelectedIndex = 29;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_M) KeyPicker2.SelectedIndex = 30;
            }
            {
                if (Settings.Sonar.Key == Keyboard.VK_TAB) KeyPicker3.SelectedIndex = 0;
                if (Settings.Sonar.Key == Keyboard.VK_RBUTTON) KeyPicker3.SelectedIndex = 1;
                if (Settings.Sonar.Key == Keyboard.VK_MBUTTON) KeyPicker3.SelectedIndex = 2;
                if (Settings.Sonar.Key == Keyboard.VK_XBUTTON1) KeyPicker3.SelectedIndex = 3;
                if (Settings.Sonar.Key == Keyboard.VK_XBUTTON2) KeyPicker3.SelectedIndex = 4;
                if (Settings.Sonar.Key == Keyboard.VK_MENU) KeyPicker3.SelectedIndex = 5;
                if (Settings.Sonar.Key == Keyboard.VK_SHIFT) KeyPicker3.SelectedIndex = 6;
                if (Settings.Sonar.Key == Keyboard.VK_CAPITAL) KeyPicker3.SelectedIndex = 7;
                if (Settings.Sonar.Key == Keyboard.VK_V) KeyPicker3.SelectedIndex = 8;
                if (Settings.Sonar.Key == Keyboard.VK_C) KeyPicker3.SelectedIndex = 9;
                if (Settings.Sonar.Key == Keyboard.VK_B) KeyPicker3.SelectedIndex = 10;
                if (Settings.Sonar.Key == Keyboard.VK_F) KeyPicker3.SelectedIndex = 11;
                if (Settings.Sonar.Key == Keyboard.VK_E) KeyPicker3.SelectedIndex = 12;
                if (Settings.Sonar.Key == Keyboard.VK_Q) KeyPicker3.SelectedIndex = 13;
                if (Settings.Sonar.Key == Keyboard.VK_W) KeyPicker3.SelectedIndex = 14;
                if (Settings.Sonar.Key == Keyboard.VK_R) KeyPicker3.SelectedIndex = 15;
                if (Settings.Sonar.Key == Keyboard.VK_T) KeyPicker3.SelectedIndex = 16;
                if (Settings.Sonar.Key == Keyboard.VK_Y) KeyPicker3.SelectedIndex = 17;
                if (Settings.Sonar.Key == Keyboard.VK_U) KeyPicker3.SelectedIndex = 18;
                if (Settings.Sonar.Key == Keyboard.VK_I) KeyPicker3.SelectedIndex = 19;
                if (Settings.Sonar.Key == Keyboard.VK_O) KeyPicker3.SelectedIndex = 20;
                if (Settings.Sonar.Key == Keyboard.VK_P) KeyPicker3.SelectedIndex = 21;
                if (Settings.Sonar.Key == Keyboard.VK_G) KeyPicker3.SelectedIndex = 22;
                if (Settings.Sonar.Key == Keyboard.VK_H) KeyPicker3.SelectedIndex = 23;
                if (Settings.Sonar.Key == Keyboard.VK_J) KeyPicker3.SelectedIndex = 24;
                if (Settings.Sonar.Key == Keyboard.VK_K) KeyPicker3.SelectedIndex = 25;
                if (Settings.Sonar.Key == Keyboard.VK_L) KeyPicker3.SelectedIndex = 26;
                if (Settings.Sonar.Key == Keyboard.VK_Z) KeyPicker3.SelectedIndex = 27;
                if (Settings.Sonar.Key == Keyboard.VK_X) KeyPicker3.SelectedIndex = 28;
                if (Settings.Sonar.Key == Keyboard.VK_N) KeyPicker3.SelectedIndex = 29;
                if (Settings.Sonar.Key == Keyboard.VK_M) KeyPicker3.SelectedIndex = 30;
            }
            {
                  if (Settings.AimAssist.Key == Keyboard.VK_TAB) KeyPicker4.SelectedIndex = 0;
                if (Settings.AimAssist.Key == Keyboard.VK_RBUTTON) KeyPicker4.SelectedIndex = 1;
                if (Settings.AimAssist.Key == Keyboard.VK_MBUTTON) KeyPicker4.SelectedIndex = 2;
                if (Settings.AimAssist.Key == Keyboard.VK_XBUTTON1) KeyPicker4.SelectedIndex = 3;
                if (Settings.AimAssist.Key == Keyboard.VK_XBUTTON2) KeyPicker4.SelectedIndex = 4;
                if (Settings.AimAssist.Key == Keyboard.VK_MENU) KeyPicker4.SelectedIndex = 5;
                if (Settings.AimAssist.Key == Keyboard.VK_SHIFT) KeyPicker4.SelectedIndex = 6;
                if (Settings.AimAssist.Key == Keyboard.VK_CAPITAL) KeyPicker4.SelectedIndex = 7;
                if (Settings.AimAssist.Key == Keyboard.VK_V) KeyPicker4.SelectedIndex = 8;
                if (Settings.AimAssist.Key == Keyboard.VK_C) KeyPicker4.SelectedIndex = 9;
                if (Settings.AimAssist.Key == Keyboard.VK_B) KeyPicker4.SelectedIndex = 10;
                if (Settings.AimAssist.Key == Keyboard.VK_F) KeyPicker4.SelectedIndex = 11;
                if (Settings.AimAssist.Key == Keyboard.VK_E) KeyPicker4.SelectedIndex = 12;
                if (Settings.AimAssist.Key == Keyboard.VK_Q) KeyPicker4.SelectedIndex = 13;
                if (Settings.AimAssist.Key == Keyboard.VK_W) KeyPicker4.SelectedIndex = 14;
                if (Settings.AimAssist.Key == Keyboard.VK_R) KeyPicker4.SelectedIndex = 15;
                if (Settings.AimAssist.Key == Keyboard.VK_T) KeyPicker4.SelectedIndex = 16;
                if (Settings.AimAssist.Key == Keyboard.VK_Y) KeyPicker4.SelectedIndex = 17;
                if (Settings.AimAssist.Key == Keyboard.VK_U) KeyPicker4.SelectedIndex = 18;
                if (Settings.AimAssist.Key == Keyboard.VK_I) KeyPicker4.SelectedIndex = 19;
                if (Settings.AimAssist.Key == Keyboard.VK_O) KeyPicker4.SelectedIndex = 20;
                if (Settings.AimAssist.Key == Keyboard.VK_P) KeyPicker4.SelectedIndex = 21;
                if (Settings.AimAssist.Key == Keyboard.VK_G) KeyPicker4.SelectedIndex = 22;
                if (Settings.AimAssist.Key == Keyboard.VK_H) KeyPicker4.SelectedIndex = 23;
                if (Settings.AimAssist.Key == Keyboard.VK_J) KeyPicker4.SelectedIndex = 24;
                if (Settings.AimAssist.Key == Keyboard.VK_K) KeyPicker4.SelectedIndex = 25;
                if (Settings.AimAssist.Key == Keyboard.VK_L) KeyPicker4.SelectedIndex = 26;
                if (Settings.AimAssist.Key == Keyboard.VK_Z) KeyPicker4.SelectedIndex = 27;
                if (Settings.AimAssist.Key == Keyboard.VK_X) KeyPicker4.SelectedIndex = 28;
                if (Settings.AimAssist.Key == Keyboard.VK_N) KeyPicker4.SelectedIndex = 29;
                if (Settings.AimAssist.Key == Keyboard.VK_M) KeyPicker4.SelectedIndex = 30;
            }
            {
                if (Settings.Trigger.Key == Keyboard.VK_LBUTTON) KeyPicker7.SelectedIndex = 0;
                if (Settings.Trigger.Key == Keyboard.VK_RBUTTON) KeyPicker7.SelectedIndex = 1;
                if (Settings.Trigger.Key == Keyboard.VK_MBUTTON) KeyPicker7.SelectedIndex = 2;
                if (Settings.Trigger.Key == Keyboard.VK_XBUTTON1) KeyPicker7.SelectedIndex = 3;
                if (Settings.Trigger.Key == Keyboard.VK_XBUTTON2) KeyPicker7.SelectedIndex = 4;
                if (Settings.Trigger.Key == Keyboard.VK_MENU) KeyPicker7.SelectedIndex = 5;
                if (Settings.Trigger.Key == Keyboard.VK_SHIFT) KeyPicker7.SelectedIndex = 6;
                if (Settings.Trigger.Key == Keyboard.VK_CAPITAL) KeyPicker7.SelectedIndex = 7;
                if (Settings.Trigger.Key == Keyboard.VK_V) KeyPicker7.SelectedIndex = 8;
                if (Settings.Trigger.Key == Keyboard.VK_C) KeyPicker7.SelectedIndex = 9;
                if (Settings.Trigger.Key == Keyboard.VK_B) KeyPicker7.SelectedIndex = 10;
                if (Settings.Trigger.Key == Keyboard.VK_F) KeyPicker7.SelectedIndex = 11;
                if (Settings.Trigger.Key == Keyboard.VK_E) KeyPicker7.SelectedIndex = 12;
                if (Settings.Trigger.Key == Keyboard.VK_Q) KeyPicker7.SelectedIndex = 13;
                if (Settings.Trigger.Key == Keyboard.VK_W) KeyPicker7.SelectedIndex = 14;
                if (Settings.Trigger.Key == Keyboard.VK_R) KeyPicker7.SelectedIndex = 15;
                if (Settings.Trigger.Key == Keyboard.VK_T) KeyPicker7.SelectedIndex = 16;
                if (Settings.Trigger.Key == Keyboard.VK_Y) KeyPicker7.SelectedIndex = 17;
                if (Settings.Trigger.Key == Keyboard.VK_U) KeyPicker7.SelectedIndex = 18;
                if (Settings.Trigger.Key == Keyboard.VK_I) KeyPicker7.SelectedIndex = 19;
                if (Settings.Trigger.Key == Keyboard.VK_O) KeyPicker7.SelectedIndex = 20;
                if (Settings.Trigger.Key == Keyboard.VK_P) KeyPicker7.SelectedIndex = 21;
                if (Settings.Trigger.Key == Keyboard.VK_G) KeyPicker7.SelectedIndex = 22;
                if (Settings.Trigger.Key == Keyboard.VK_H) KeyPicker7.SelectedIndex = 23;
                if (Settings.Trigger.Key == Keyboard.VK_J) KeyPicker7.SelectedIndex = 24;
                if (Settings.Trigger.Key == Keyboard.VK_K) KeyPicker7.SelectedIndex = 25;
                if (Settings.Trigger.Key == Keyboard.VK_L) KeyPicker7.SelectedIndex = 26;
                if (Settings.Trigger.Key == Keyboard.VK_Z) KeyPicker7.SelectedIndex = 27;
                if (Settings.Trigger.Key == Keyboard.VK_X) KeyPicker7.SelectedIndex = 28;
                if (Settings.Trigger.Key == Keyboard.VK_N) KeyPicker7.SelectedIndex = 29;
                if (Settings.Trigger.Key == Keyboard.VK_M) KeyPicker7.SelectedIndex = 30;
            }
            {
                if (Settings.Bunnyhop.Key == Keyboard.VK_SPACE) KeyPicker5.SelectedIndex = 0;
                if (Settings.Bunnyhop.Key == Keyboard.VK_RBUTTON) KeyPicker5.SelectedIndex = 1;
                if (Settings.Bunnyhop.Key == Keyboard.VK_MBUTTON) KeyPicker5.SelectedIndex = 2;
                if (Settings.Bunnyhop.Key == Keyboard.VK_XBUTTON1) KeyPicker5.SelectedIndex = 3;
                if (Settings.Bunnyhop.Key == Keyboard.VK_XBUTTON2) KeyPicker5.SelectedIndex = 4;
                if (Settings.Bunnyhop.Key == Keyboard.VK_MENU) KeyPicker5.SelectedIndex = 5;
                if (Settings.Bunnyhop.Key == Keyboard.VK_SHIFT) KeyPicker5.SelectedIndex = 6;
                if (Settings.Bunnyhop.Key == Keyboard.VK_CAPITAL) KeyPicker5.SelectedIndex = 7;
                if (Settings.Bunnyhop.Key == Keyboard.VK_V) KeyPicker5.SelectedIndex = 8;
                if (Settings.Bunnyhop.Key == Keyboard.VK_C) KeyPicker5.SelectedIndex = 9;
                if (Settings.Bunnyhop.Key == Keyboard.VK_B) KeyPicker5.SelectedIndex = 10;
                if (Settings.Bunnyhop.Key == Keyboard.VK_F) KeyPicker5.SelectedIndex = 11;
                if (Settings.Bunnyhop.Key == Keyboard.VK_E) KeyPicker5.SelectedIndex = 12;
                if (Settings.Bunnyhop.Key == Keyboard.VK_Q) KeyPicker5.SelectedIndex = 13;
                if (Settings.Bunnyhop.Key == Keyboard.VK_W) KeyPicker5.SelectedIndex = 14;
                if (Settings.Bunnyhop.Key == Keyboard.VK_R) KeyPicker5.SelectedIndex = 15;
                if (Settings.Bunnyhop.Key == Keyboard.VK_T) KeyPicker5.SelectedIndex = 16;
                if (Settings.Bunnyhop.Key == Keyboard.VK_Y) KeyPicker5.SelectedIndex = 17;
                if (Settings.Bunnyhop.Key == Keyboard.VK_U) KeyPicker5.SelectedIndex = 18;
                if (Settings.Bunnyhop.Key == Keyboard.VK_I) KeyPicker5.SelectedIndex = 19;
                if (Settings.Bunnyhop.Key == Keyboard.VK_O) KeyPicker5.SelectedIndex = 20;
                if (Settings.Bunnyhop.Key == Keyboard.VK_P) KeyPicker5.SelectedIndex = 21;
                if (Settings.Bunnyhop.Key == Keyboard.VK_G) KeyPicker5.SelectedIndex = 22;
                if (Settings.Bunnyhop.Key == Keyboard.VK_H) KeyPicker5.SelectedIndex = 23;
                if (Settings.Bunnyhop.Key == Keyboard.VK_J) KeyPicker5.SelectedIndex = 24;
                if (Settings.Bunnyhop.Key == Keyboard.VK_K) KeyPicker5.SelectedIndex = 25;
                if (Settings.Bunnyhop.Key == Keyboard.VK_L) KeyPicker5.SelectedIndex = 26;
                if (Settings.Bunnyhop.Key == Keyboard.VK_Z) KeyPicker5.SelectedIndex = 27;
                if (Settings.Bunnyhop.Key == Keyboard.VK_X) KeyPicker5.SelectedIndex = 28;
                if (Settings.Bunnyhop.Key == Keyboard.VK_N) KeyPicker5.SelectedIndex = 29;
                if (Settings.Bunnyhop.Key == Keyboard.VK_M) KeyPicker5.SelectedIndex = 30;
            }
            {
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_SPACE) KeyPicker6.SelectedIndex = 0;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_RBUTTON) KeyPicker6.SelectedIndex = 1;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_MBUTTON) KeyPicker6.SelectedIndex = 2;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_XBUTTON1) KeyPicker6.SelectedIndex = 3;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_XBUTTON2) KeyPicker6.SelectedIndex = 4;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_MENU) KeyPicker6.SelectedIndex = 5;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_SHIFT) KeyPicker6.SelectedIndex = 6;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_CAPITAL) KeyPicker6.SelectedIndex = 7;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_V) KeyPicker6.SelectedIndex = 8;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_C) KeyPicker6.SelectedIndex = 9;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_B) KeyPicker6.SelectedIndex = 10;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_F) KeyPicker6.SelectedIndex = 11;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_E) KeyPicker6.SelectedIndex = 12;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_Q) KeyPicker6.SelectedIndex = 13;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_W) KeyPicker6.SelectedIndex = 14;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_R) KeyPicker6.SelectedIndex = 15;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_T) KeyPicker6.SelectedIndex = 16;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_Y) KeyPicker6.SelectedIndex = 17;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_U) KeyPicker6.SelectedIndex = 18;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_I) KeyPicker6.SelectedIndex = 19;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_O) KeyPicker6.SelectedIndex = 20;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_P) KeyPicker6.SelectedIndex = 21;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_G) KeyPicker6.SelectedIndex = 22;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_H) KeyPicker6.SelectedIndex = 23;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_J) KeyPicker6.SelectedIndex = 24;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_K) KeyPicker6.SelectedIndex = 25;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_L) KeyPicker6.SelectedIndex = 26;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_Z) KeyPicker6.SelectedIndex = 27;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_X) KeyPicker6.SelectedIndex = 28;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_N) KeyPicker6.SelectedIndex = 29;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_M) KeyPicker6.SelectedIndex = 30;
            }
            {
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F6) KeyPicker8.SelectedIndex = 0;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F7) KeyPicker8.SelectedIndex = 1;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F8) KeyPicker8.SelectedIndex = 2;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F9) KeyPicker8.SelectedIndex = 3;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F10) KeyPicker8.SelectedIndex = 4;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F11) KeyPicker8.SelectedIndex = 5;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_INSERT) KeyPicker8.SelectedIndex = 6;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_DELETE) KeyPicker8.SelectedIndex = 7;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_HOME) KeyPicker8.SelectedIndex = 8;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_END) KeyPicker8.SelectedIndex = 9;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_PRIOR) KeyPicker8.SelectedIndex = 10;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NEXT) KeyPicker8.SelectedIndex = 11;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD1) KeyPicker8.SelectedIndex = 12;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD2) KeyPicker8.SelectedIndex = 13;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD3) KeyPicker8.SelectedIndex = 14;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD4) KeyPicker8.SelectedIndex = 15;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD5) KeyPicker8.SelectedIndex = 16;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD6) KeyPicker8.SelectedIndex = 17;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD7) KeyPicker8.SelectedIndex = 18;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD8) KeyPicker8.SelectedIndex = 19;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD9) KeyPicker8.SelectedIndex = 20;
            }
            {
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F6) KeyPicker9.SelectedIndex = 0;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F7) KeyPicker9.SelectedIndex = 1;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F8) KeyPicker9.SelectedIndex = 2;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F9) KeyPicker9.SelectedIndex = 3;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F10) KeyPicker9.SelectedIndex = 4;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F11) KeyPicker9.SelectedIndex = 5;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_INSERT) KeyPicker9.SelectedIndex = 6;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_DELETE) KeyPicker9.SelectedIndex = 7;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_HOME) KeyPicker9.SelectedIndex = 8;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_END) KeyPicker9.SelectedIndex = 9;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_PRIOR) KeyPicker9.SelectedIndex = 10;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NEXT) KeyPicker9.SelectedIndex = 11;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD1) KeyPicker9.SelectedIndex = 12;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD2) KeyPicker9.SelectedIndex = 13;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD3) KeyPicker9.SelectedIndex = 14;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD4) KeyPicker9.SelectedIndex = 15;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD5) KeyPicker9.SelectedIndex = 16;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD6) KeyPicker9.SelectedIndex = 17;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD7) KeyPicker9.SelectedIndex = 18;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD8) KeyPicker9.SelectedIndex = 19;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD9) KeyPicker9.SelectedIndex = 20;
            }
            {
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F6) KeyPicker10.SelectedIndex = 0;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F7) KeyPicker10.SelectedIndex = 1;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F8) KeyPicker10.SelectedIndex = 2;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F9) KeyPicker10.SelectedIndex = 3;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F10) KeyPicker10.SelectedIndex = 4;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F11) KeyPicker10.SelectedIndex = 5;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_INSERT) KeyPicker10.SelectedIndex = 6;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_DELETE) KeyPicker10.SelectedIndex = 7;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_HOME) KeyPicker10.SelectedIndex = 8;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_END) KeyPicker10.SelectedIndex = 9;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_PRIOR) KeyPicker10.SelectedIndex = 10;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NEXT) KeyPicker10.SelectedIndex = 11;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD1) KeyPicker10.SelectedIndex = 12;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD2) KeyPicker10.SelectedIndex = 13;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD3) KeyPicker10.SelectedIndex = 14;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD4) KeyPicker10.SelectedIndex = 15;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD5) KeyPicker10.SelectedIndex = 16;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD6) KeyPicker10.SelectedIndex = 17;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD7) KeyPicker10.SelectedIndex = 18;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD8) KeyPicker10.SelectedIndex = 19;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD9) KeyPicker10.SelectedIndex = 20;
            }
            {
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F6) KeyPicker11.SelectedIndex = 0;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F7) KeyPicker11.SelectedIndex = 1;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F8) KeyPicker11.SelectedIndex = 2;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F9) KeyPicker11.SelectedIndex = 3;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F10) KeyPicker11.SelectedIndex = 4;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F11) KeyPicker11.SelectedIndex = 5;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_INSERT) KeyPicker11.SelectedIndex = 6;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_DELETE) KeyPicker11.SelectedIndex = 7;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_HOME) KeyPicker11.SelectedIndex = 8;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_END) KeyPicker11.SelectedIndex = 9;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_PRIOR) KeyPicker11.SelectedIndex = 10;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NEXT) KeyPicker11.SelectedIndex = 11;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD1) KeyPicker11.SelectedIndex = 12;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD2) KeyPicker11.SelectedIndex = 13;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD3) KeyPicker11.SelectedIndex = 14;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD4) KeyPicker11.SelectedIndex = 15;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD5) KeyPicker11.SelectedIndex = 16;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD6) KeyPicker11.SelectedIndex = 17;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD7) KeyPicker11.SelectedIndex = 18;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD8) KeyPicker11.SelectedIndex = 19;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD9) KeyPicker11.SelectedIndex = 20;
            }
            {
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F6) KeyPicker12.SelectedIndex = 0;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F7) KeyPicker12.SelectedIndex = 1;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F8) KeyPicker12.SelectedIndex = 2;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F9) KeyPicker12.SelectedIndex = 3;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F10) KeyPicker12.SelectedIndex = 4;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F11) KeyPicker12.SelectedIndex = 5;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_INSERT) KeyPicker12.SelectedIndex = 6;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_DELETE) KeyPicker12.SelectedIndex = 7;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_HOME) KeyPicker12.SelectedIndex = 8;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_END) KeyPicker12.SelectedIndex = 9;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_PRIOR) KeyPicker12.SelectedIndex = 10;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NEXT) KeyPicker12.SelectedIndex = 11;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD1) KeyPicker12.SelectedIndex = 12;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD2) KeyPicker12.SelectedIndex = 13;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD3) KeyPicker12.SelectedIndex = 14;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD4) KeyPicker12.SelectedIndex = 15;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD5) KeyPicker12.SelectedIndex = 16;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD6) KeyPicker12.SelectedIndex = 17;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD7) KeyPicker12.SelectedIndex = 18;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD8) KeyPicker12.SelectedIndex = 19;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD9) KeyPicker12.SelectedIndex = 20;
            }

            if (Settings.Esp.Enabled) EspToggle1.BackgroundImage = Properties.Resources.switch5;
            else EspToggle1.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Esp.Enabled) EspToggle1.BackgroundImage = Properties.Resources.switch5;
            else EspToggle1.BackgroundImage = Properties.Resources.switch6;

            EspTrackBar1.Value = Settings.Esp.Color_R; EspLabel8.Text = Settings.Esp.Color_R.ToString();
            EspTrackBar2.Value = Settings.Esp.Color_G; EspLabel9.Text = Settings.Esp.Color_G.ToString();
            EspTrackBar3.Value = Settings.Esp.Color_B; EspLabel10.Text = Settings.Esp.Color_B.ToString();
            EspTrackBar4.Value = Settings.Esp.VisableColor_R; EspLabel14.Text = Settings.Esp.VisableColor_R.ToString();
            EspTrackBar5.Value = Settings.Esp.VisableColor_G; EspLabel15.Text = Settings.Esp.VisableColor_G.ToString();
            EspTrackBar6.Value = Settings.Esp.VisableColor_B; EspLabel16.Text = Settings.Esp.VisableColor_B.ToString();

            ProfileLabel1.Text = ProfileLabel1.Text + Settings.username;
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    ProfileLabel2.Text = ProfileLabel2.Text + UI.CheckFor45PlusVersion((int)ndpKey.GetValue("Release"));
                }
            }
            
            ProfileLabel3.Text = ProfileLabel3.Text + Settings.JoinDate;
            ProfileLabel4.Text = ProfileLabel4.Text + Settings.EndDate;
            ProfileLabel9.Text = ProfileLabel9.Text + Settings.version;
            string ver = Environment.OSVersion.ToString();
            ver = ver.Replace("Windows", "");
            ver = ver.Replace("Microsoft", "");
            ver = ver.Replace("Service", "");
            ver = ver.Replace("Pack", "");
            ver = ver.Replace("NT", "");
            ver = ver.Replace(" ", "");
            ProfileLabel6.Text = ProfileLabel6.Text + ver;


            if (Settings.Chams.Enabled) ChamsToggle1.BackgroundImage = Properties.Resources.switch5;
            else ChamsToggle1.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Chams.Allies) ChamsToggle2.BackgroundImage = Properties.Resources.switch5;
            else ChamsToggle2.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Chams.HealthBased) ChamsToggle3.BackgroundImage = Properties.Resources.switch5;
            else ChamsToggle3.BackgroundImage = Properties.Resources.switch6;

            ChamsTrackBar1.Value = Settings.Chams.Color_R;
            ChamsLabel10.Text = Settings.Chams.Color_R.ToString();

            ChamsTrackBar2.Value = Settings.Chams.Color_G;
            ChamsLabel11.Text = Settings.Chams.Color_G.ToString();

            ChamsTrackBar3.Value = Settings.Chams.Color_B;
            ChamsLabel12.Text = Settings.Chams.Color_B.ToString();

            ChamsTrackBar4.Value = Settings.Chams.Allies_Color_R;
            ChamsLabel13.Text = Settings.Chams.Allies_Color_R.ToString();

            ChamsTrackBar5.Value = Settings.Chams.Allies_Color_G;
            ChamsLabel14.Text = Settings.Chams.Allies_Color_G.ToString();

            ChamsTrackBar6.Value = Settings.Chams.Allies_Color_B;
            ChamsLabel15.Text = Settings.Chams.Allies_Color_B.ToString();

            #region Hack
            process = Process.GetProcessesByName("csgo"); //Creating applicathion list with csgo procces name
            if (process.Length < 1)
            {
                MessageBox.Show("Run game first!!!");
                Environment.Exit(0); // Closeing app if game is not running
            }
            GameProcess = process[0];

            List<string> loadedModules = new List<string>(Modules.Length);
         /*   while (loadedModules.Count < 2) //waiting when app will load all dlls
            {
                foreach (ProcessModule module in GameProcess.Modules)
                {
                    if (File.Exists("modules.txt"))
                    {
                        listofmodules = File.ReadAllText("modules.txt");
                    }
                    File.WriteAllText("modules.txt", listofmodules + module.FileName + Environment.NewLine);

                    if (Modules.Contains(module.ModuleName) && !loadedModules.Contains(module.ModuleName))
                    {
                        loadedModules.Add(module.ModuleName); //Get module
                        switch (module.ModuleName)
                        {
                            case "engine.dll":
                                Structs.Base.Engine = module.BaseAddress;
                                break;
                            case "client_panorama.dll":
                                Structs.Base.Client = module.BaseAddress;
                                break;
                            default:
                                break;
                        }
                    }
                }
                Thread.Sleep(100);
            }*/
            while (true)
            {
                if (loadedModules.Count < 2)
                {
                    foreach (ProcessModule module in GameProcess.Modules)
                    {
                        if (!loadedModules.Contains(module.ModuleName))
                        {
                            if (module.ModuleName.Contains("engine.dll"))
                            {
                                loadedModules.Add(module.ModuleName);
                              //  MessageBox.Show(module.ModuleName);
                                Structs.Base.Engine = module.BaseAddress;
                            }
                            else if(module.ModuleName.Contains("client_panorama.dll"))
                            {
                                loadedModules.Add(module.ModuleName);
                              //  MessageBox.Show(module.ModuleName);
                                Structs.Base.Client = module.BaseAddress;
                            }
                        }
                    }
                    process = Process.GetProcessesByName("csgo"); //Creating applicathion list with csgo procces name
                    if (process.Length < 1)
                    {
                        MessageBox.Show("Run game first!!!");
                        Environment.Exit(0); // Closeing app if game is not running
                    }
                    GameProcess = process[0];
                   // MessageBox.Show("NO FOUND!");
                }
                else
                {
                 //  MessageBox.Show("DONE!");
                    break;
                }
            }


       /*     while (true)
            {
                List<string> loadedModules = new List<string>(Modules.Length);
                if (loadedModules.Count < 2)
                {
                    if (started == false)
                    {
                        started = true;

                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }*/

            MemoryManager.Initialize(GameProcess.Id);

            List<string> outdatedSignatures = Offsets.ScanPatterns();

            Console.Beep(400, 100);
            Thread.Sleep(50);
            Console.Beep(400, 100);
            Thread.Sleep(50);
            Console.Beep(400, 100);
            Thread.Sleep(50);

            ThreadManager.Add("Reader", Reader.Run);
            ThreadManager.Add("Aimbot", Aimbot.Run);
            ThreadManager.Add("Trigger", Trigger.Run);
            ThreadManager.Add("AimAssist", AimAssist.Run);
            ThreadManager.Add("KillDelay", KillDelay.Run);
            ThreadManager.Add("Sonar", Sonar.Run);
            ThreadManager.Add("Bunnyhop", Bunnyhop.Run);
            ThreadManager.Add("Glow", Glow.Run);
            ThreadManager.Add("Chams", Chams.Run);
            ThreadManager.Add("Radar", Radar.Run);
            ThreadManager.Add("AutoPistol", AutoPistol.Run);
            ThreadManager.Add("Watcher", Watcher.Run);
            ThreadManager.Add("AutoDelay", AutoDelay.Run);

            ThreadManager.ToggleThread("Reader");
            ThreadManager.ToggleThread("Watcher");
            ThreadManager.ToggleThread("AutoPistol");
            ThreadManager.ToggleThread("AutoDelay");
            ThreadManager.ToggleThread("KillDelay");

            if (Settings.Aimbot.Enabled) ThreadManager.ToggleThread("Aimbot");
            if (Settings.Trigger.Enabled) ThreadManager.ToggleThread("Trigger");
            if (Settings.Glow.Enabled) ThreadManager.ToggleThread("Glow");
            if (Settings.Chams.Enabled) ThreadManager.ToggleThread("Chams");
            if (Settings.Radar.Enabled) ThreadManager.ToggleThread("Radar");
            if (Settings.Bunnyhop.Enabled) ThreadManager.ToggleThread("Bunnyhop");
            if (Settings.AimAssist.Enabled) ThreadManager.ToggleThread("AimAssist");
            if (Settings.Sonar.Enabled) ThreadManager.ToggleThread("Sonar");

            #endregion
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Toggle1_Click(object sender, EventArgs e)
        {
            Settings.Aimbot.Enabled = !Settings.Aimbot.Enabled;
            if (Settings.Aimbot.Enabled)
            {
                ThreadManager.StartThread("Aimbot");
                UI.ToggleAnimathionOn(Toggle1);
            }
            else
            {
                ThreadManager.PauseThread("Aimbot");
                UI.ToggleAnimathionOff(Toggle1);
            }
        }

        private void Toggle2_Click(object sender, EventArgs e)
        {
            Settings.Aimbot.VisibleOnly = !Settings.Aimbot.VisibleOnly;
            if (Settings.Aimbot.VisibleOnly)
            {
                UI.ToggleAnimathionOn(Toggle2);
            }
            else
            {
                UI.ToggleAnimathionOff(Toggle2);
            }
        }

        private void Toggle3_Click(object sender, EventArgs e)
        {
            Settings.Aimbot.KillDelay = !Settings.Aimbot.KillDelay;
            if (Settings.Aimbot.KillDelay)
            {
                ThreadManager.StartThread("KillDelay");
                UI.ToggleAnimathionOn(Toggle3);
            }
            else
            {
                ThreadManager.PauseThread("KillDelay");
                UI.ToggleAnimathionOff(Toggle3);
            }
        }

        private void Toggle4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.GlockAutoPistol = !Settings.Aimbot.GlockAutoPistol;
                if (Settings.Aimbot.GlockAutoPistol)
                {
                    UI.ToggleAnimathionOn(Toggle4);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle4);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.USPAutoPistol = !Settings.Aimbot.USPAutoPistol;
                if (Settings.Aimbot.USPAutoPistol)
                {
                    UI.ToggleAnimathionOn(Toggle4);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle4);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.P2000AutoPistol = !Settings.Aimbot.P2000AutoPistol;
                if (Settings.Aimbot.P2000AutoPistol)
                {
                    UI.ToggleAnimathionOn(Toggle4);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle4);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.P250AutoPistol = !Settings.Aimbot.P250AutoPistol;
                if (Settings.Aimbot.P250AutoPistol)
                {
                    UI.ToggleAnimathionOn(Toggle4);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle4);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.DualsAutoPistol = !Settings.Aimbot.DualsAutoPistol;
                if (Settings.Aimbot.DualsAutoPistol)
                {
                    UI.ToggleAnimathionOn(Toggle4);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle4);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.FiveSevenAutoPistol = !Settings.Aimbot.FiveSevenAutoPistol;
                if (Settings.Aimbot.FiveSevenAutoPistol)
                {
                    UI.ToggleAnimathionOn(Toggle4);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle4);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.TEC9AutoPistol = !Settings.Aimbot.TEC9AutoPistol;
                if (Settings.Aimbot.TEC9AutoPistol)
                {
                    UI.ToggleAnimathionOn(Toggle4);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle4);
                }
            }
        }

        private void Toggle5_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.GlockRecoilControl = !Settings.Aimbot.GlockRecoilControl;
                if (Settings.Aimbot.GlockRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.USPRecoilControl = !Settings.Aimbot.USPRecoilControl;
                if (Settings.Aimbot.USPRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.P2000RecoilControl = !Settings.Aimbot.P2000RecoilControl;
                if (Settings.Aimbot.P2000RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.P250RecoilControl = !Settings.Aimbot.P250RecoilControl;
                if (Settings.Aimbot.P250RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.DualsRecoilControl = !Settings.Aimbot.DualsRecoilControl;
                if (Settings.Aimbot.DualsRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.FiveSevenRecoilControl = !Settings.Aimbot.FiveSevenRecoilControl;
                if (Settings.Aimbot.FiveSevenRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.TEC9RecoilControl = !Settings.Aimbot.TEC9RecoilControl;
                if (Settings.Aimbot.TEC9RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
            {
                Settings.Aimbot.DEAGLERecoilControl = !Settings.Aimbot.DEAGLERecoilControl;
                if (Settings.Aimbot.DEAGLERecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.RevolverRecoilControl = !Settings.Aimbot.RevolverRecoilControl;
                if (Settings.Aimbot.RevolverRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.CZRecoilControl = !Settings.Aimbot.CZRecoilControl;
                if (Settings.Aimbot.CZRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.MP9RecoilControl = !Settings.Aimbot.MP9RecoilControl;
                if (Settings.Aimbot.MP9RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.MP9RecoilControl = !Settings.Aimbot.MP9RecoilControl;
                if (Settings.Aimbot.MP9RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MP7RecoilControl = !Settings.Aimbot.MP7RecoilControl;
                if (Settings.Aimbot.MP7RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.MP5RecoilControl = !Settings.Aimbot.MP5RecoilControl;
                if (Settings.Aimbot.MP5RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.UMPRecoilControl = !Settings.Aimbot.UMPRecoilControl;
                if (Settings.Aimbot.UMPRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.BizonRecoilControl = !Settings.Aimbot.BizonRecoilControl;
                if (Settings.Aimbot.BizonRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.P90RecoilControl = !Settings.Aimbot.P90RecoilControl;
                if (Settings.Aimbot.P90RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.AK47RecoilControl = !Settings.Aimbot.AK47RecoilControl;
                if (Settings.Aimbot.AK47RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.M4A4RecoilControl = !Settings.Aimbot.M4A4RecoilControl;
                if (Settings.Aimbot.M4A4RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.M4A1RecoilControl = !Settings.Aimbot.M4A1RecoilControl;
                if (Settings.Aimbot.M4A1RecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.GalilRecoilControl = !Settings.Aimbot.GalilRecoilControl;
                if (Settings.Aimbot.GalilRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.FamasRecoilControl = !Settings.Aimbot.FamasRecoilControl;
                if (Settings.Aimbot.FamasRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.SGRecoilControl = !Settings.Aimbot.SGRecoilControl;
                if (Settings.Aimbot.SGRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.AUGRecoilControl = !Settings.Aimbot.AUGRecoilControl;
                if (Settings.Aimbot.AUGRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.SSGRecoilControl = !Settings.Aimbot.SSGRecoilControl;
                if (Settings.Aimbot.SSGRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.AWPRecoilControl = !Settings.Aimbot.AWPRecoilControl;
                if (Settings.Aimbot.AWPRecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.AUTORecoilControl = !Settings.Aimbot.AUTORecoilControl;
                if (Settings.Aimbot.AUTORecoilControl)
                {
                    UI.ToggleAnimathionOn(Toggle5);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle5);
                }
            }
        }

        private void Toggle6_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.GlockCurve = !Settings.Aimbot.GlockCurve;
                if (Settings.Aimbot.GlockCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.USPCurve = !Settings.Aimbot.USPCurve;
                if (Settings.Aimbot.USPCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.P2000Curve = !Settings.Aimbot.P2000Curve;
                if (Settings.Aimbot.P2000Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.P250Curve = !Settings.Aimbot.P250Curve;
                if (Settings.Aimbot.P250Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.DualsCurve = !Settings.Aimbot.DualsCurve;
                if (Settings.Aimbot.DualsCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.FiveSevenCurve = !Settings.Aimbot.FiveSevenCurve;
                if (Settings.Aimbot.FiveSevenCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.TEC9Curve = !Settings.Aimbot.TEC9Curve;
                if (Settings.Aimbot.TEC9Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
            {
                Settings.Aimbot.DEAGLECurve = !Settings.Aimbot.DEAGLECurve;
                if (Settings.Aimbot.DEAGLECurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.RevolverCurve = !Settings.Aimbot.RevolverCurve;
                if (Settings.Aimbot.RevolverCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.CZCurve = !Settings.Aimbot.CZCurve;
                if (Settings.Aimbot.CZCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.MP9Curve = !Settings.Aimbot.MP9Curve;
                if (Settings.Aimbot.MP9Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.MP9Curve = !Settings.Aimbot.MP9Curve;
                if (Settings.Aimbot.MP9Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MP7Curve = !Settings.Aimbot.MP7Curve;
                if (Settings.Aimbot.MP7Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.MP5Curve = !Settings.Aimbot.MP5Curve;
                if (Settings.Aimbot.MP5Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.UMPCurve = !Settings.Aimbot.UMPCurve;
                if (Settings.Aimbot.UMPCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.BizonCurve = !Settings.Aimbot.BizonCurve;
                if (Settings.Aimbot.BizonCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.P90Curve = !Settings.Aimbot.P90Curve;
                if (Settings.Aimbot.P90Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.AK47Curve = !Settings.Aimbot.AK47Curve;
                if (Settings.Aimbot.AK47Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.M4A4Curve = !Settings.Aimbot.M4A4Curve;
                if (Settings.Aimbot.M4A4Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.M4A1Curve = !Settings.Aimbot.M4A1Curve;
                if (Settings.Aimbot.M4A1Curve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.GalilCurve = !Settings.Aimbot.GalilCurve;
                if (Settings.Aimbot.GalilCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.FamasCurve = !Settings.Aimbot.FamasCurve;
                if (Settings.Aimbot.FamasCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.SGCurve = !Settings.Aimbot.SGCurve;
                if (Settings.Aimbot.SGCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.AUGCurve = !Settings.Aimbot.AUGCurve;
                if (Settings.Aimbot.AUGCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.SSGCurve = !Settings.Aimbot.SSGCurve;
                if (Settings.Aimbot.SSGCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.AWPCurve = !Settings.Aimbot.AWPCurve;
                if (Settings.Aimbot.AWPCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.AUTOCurve = !Settings.Aimbot.AUTOCurve;
                if (Settings.Aimbot.AUTOCurve)
                {
                    UI.ToggleAnimathionOn(Toggle6);
                }
                else
                {
                    UI.ToggleAnimathionOff(Toggle6);
                }
            }
        }

        private void Toggle7_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                if (comboBox3.SelectedIndex == 1)
                {
                    Settings.Aimbot.FirstUSP = !Settings.Aimbot.FirstUSP;
                    if (Settings.Aimbot.FirstUSP)
                    {
                        UI.ToggleAnimathionOn(Toggle7);
                    }
                    else
                    {
                        UI.ToggleAnimathionOff(Toggle7);
                    }
                }
            }
            if (comboBox2.SelectedIndex == 2)
            {
                if (comboBox3.SelectedIndex == 0)
                {
                    Settings.Aimbot.FirstAK47 = !Settings.Aimbot.FirstAK47;
                    if (Settings.Aimbot.FirstAK47)
                    {
                        UI.ToggleAnimathionOn(Toggle7);
                    }
                    else
                    {
                        UI.ToggleAnimathionOff(Toggle7);
                    }
                }
                if (comboBox3.SelectedIndex == 1)
                {
                    Settings.Aimbot.FirstM4A4 = !Settings.Aimbot.FirstM4A4;
                    if (Settings.Aimbot.FirstM4A4)
                    {
                        UI.ToggleAnimathionOn(Toggle7);
                    }
                    else
                    {
                        UI.ToggleAnimathionOff(Toggle7);
                    }
                }
                if (comboBox3.SelectedIndex == 2)
                {
                    Settings.Aimbot.FirstM4A1 = !Settings.Aimbot.FirstM4A1;
                    if (Settings.Aimbot.FirstM4A1)
                    {
                        UI.ToggleAnimathionOn(Toggle7);
                    }
                    else
                    {
                        UI.ToggleAnimathionOff(Toggle7);
                    }
                }
            }
        }
        private void Toggle8_Click(object sender, EventArgs e)
        {
            Settings.Aimbot.UseMouseEvent = !Settings.Aimbot.UseMouseEvent;
            if (Settings.Aimbot.UseMouseEvent)
            {
                UI.ToggleAnimathionOn(Toggle8);
            }
            else
            {
                UI.ToggleAnimathionOff(Toggle8);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.GlockFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.GlockFov.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.USPFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.USPFov.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.P2000Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.P2000Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.P250Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.P250Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.DualsFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.DualsFov.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.FiveSevenFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.FiveSevenFov.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.TEC9Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.TEC9Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
            {
                Settings.Aimbot.DEAGLEFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.DEAGLEFov.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.RevolverFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.RevolverFov.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 9)
            {
                Settings.Aimbot.CZFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.CZFov.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.MP9Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.MP9Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.MP9Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.MP9Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MP7Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.MP7Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.MP5Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.MP5Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.UMPFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.UMPFov.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.BizonFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.BizonFov.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.P90Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.P90Fov.ToString();
            }
            //////////RIFLES////////
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.AK47Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.AK47Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.M4A4Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.M4A4Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.M4A1Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.M4A1Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.GalilFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.GalilFov.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.FamasFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.FamasFov.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.SGFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.SGFov.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.AUGFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.AUGFov.ToString();
            }
            ///////////SNIPERS/////////////
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.SSGFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.SSGFov.ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.AWPFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.AWPFov.ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.AUTOFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.AUTOFov.ToString();
            }
            ///////////Heavy//////////
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.NovaFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.NovaFov.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.XMFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.XMFov.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MAG7Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.MAG7Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.SawnedOffFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.SawnedOffFov.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.NegevFov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.NegevFov.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.M249Fov = ((float)(trackBar1.Value * 0.1));
                label11.Text = Settings.Aimbot.M249Fov.ToString();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.GlockSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.GlockSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.USPSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.USPSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.P2000Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.P2000Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.P250Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.P250Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.DualsSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.DualsSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.FiveSevenSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.FiveSevenSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.TEC9Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.TEC9Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
            {
                Settings.Aimbot.DEAGLESmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.DEAGLESmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.RevolverSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.RevolverSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 9)
            {
                Settings.Aimbot.CZSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.CZSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.MP9Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.MP9Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.MP9Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.MP9Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MP7Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.MP7Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.MP5Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.MP5Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.UMPSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.UMPSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.BizonSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.BizonSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.P90Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.P90Smooth.ToString();
            }
            //////////RIFLES////////
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.AK47Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.AK47Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.M4A4Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.M4A4Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.M4A1Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.M4A1Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.GalilSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.GalilSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.FamasSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.FamasSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.SGSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.SGSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.AUGSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.AUGSmooth.ToString();
            }
            ///////////SNIPERS/////////////
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.SSGSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.SSGSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.AWPSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.AWPSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.AUTOSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.AUTOSmooth.ToString();
            }
            ///////////Heavy//////////
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.NovaSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.NovaSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.XMSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.XMSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MAG7Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.MAG7Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.SawnedOffSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.SawnedOffSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.NegevSmooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.NegevSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.M249Smooth = ((float)(trackBar2.Value * 0.1));
                label13.Text = Settings.Aimbot.M249Smooth.ToString();
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.GlockYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.GlockYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.USPYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.USPYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.P2000YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.P2000YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.P250YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.P250YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.DualsYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.DualsYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.FiveSevenYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.FiveSevenYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.TEC9YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.TEC9YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
            {
                Settings.Aimbot.DEAGLEYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.DEAGLEYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.RevolverYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.RevolverYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 9)
            {
                Settings.Aimbot.CZYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.CZYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.MAC10YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.MAC10YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.MP9YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.MP9YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MP7YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.MP7YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.MP5YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.MP5YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.UMPYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.UMPYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.BizonYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.BizonYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.P90YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.P90YawRecoilReductionFactory * 100).ToString();
            }
            //////////RIFLES////////
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.AK47YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.AK47YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.M4A4YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.M4A4YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.M4A1YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.M4A1YawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.GalilYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.GalilYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.FamasYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.FamasYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.SGYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.SGYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.AUGYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.AUGYawRecoilReductionFactory * 100).ToString();
            }
            ///////////SNIPERS/////////////
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.SSGYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.SSGYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.AWPYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.AWPYawRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.AUTOYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = (Settings.Aimbot.AUTOYawRecoilReductionFactory * 100).ToString();
            }
            ///////////Heavy//////////
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.NovaYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = Settings.Aimbot.NovaYawRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.XMYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = Settings.Aimbot.XMYawRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MAG7YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = Settings.Aimbot.MAG7YawRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.SawnedOffYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = Settings.Aimbot.SawnedOffYawRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.NegevYawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = Settings.Aimbot.NegevYawRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.M249YawRecoilReductionFactory = ((float)(trackBar3.Value * 0.01));
                label16.Text = Settings.Aimbot.M249YawRecoilReductionFactory.ToString();
            }
        }
        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.GlockCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.GlockCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.USPCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.USPCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.P2000CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.P2000CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.P250CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.P250CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.DualsCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.DualsCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.FiveSevenCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.FiveSevenCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.TEC9CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.TEC9CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
            {
                Settings.Aimbot.DEAGLECurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.DEAGLECurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.RevolverCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.RevolverCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 9)
            {
                Settings.Aimbot.CZCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.CZCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.MAC10CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.MAC10CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.MP9CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.MP9CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MP7CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.MP7CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.MP5CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.MP5CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.UMPCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.UMPCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.BizonCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.BizonCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.P90CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.P90CurveY.ToString();
            }
            //////////RIFLES////////
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.AK47CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.AK47CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.M4A4CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.M4A4CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.M4A1CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.M4A1CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.GalilCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.GalilCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.FamasCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.FamasCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.SGCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.SGCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.AUGCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.AUGCurveY.ToString();
            }
            ///////////SNIPERS/////////////
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.SSGCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.SSGCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.AWPCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.AWPCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.AUTOCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.AUTOCurveY.ToString();
            }
            ///////////Heavy//////////
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.NovaCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.NovaCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.XMCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.XMCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MAG7CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.MAG7CurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.SawnedOffCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.SawnedOffCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.NegevCurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.NegevCurveY.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.M249CurveY = ((float)(trackBar6.Value * 0.1));
                label22.Text = Settings.Aimbot.M249CurveY.ToString();
            }
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.GlockCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.GlockCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.USPCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.USPCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.P2000CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.P2000CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.P250CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.P250CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.DualsCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.DualsCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.FiveSevenCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.FiveSevenCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.TEC9CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.TEC9CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
            {
                Settings.Aimbot.DEAGLECurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.DEAGLECurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.RevolverCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.RevolverCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 9)
            {
                Settings.Aimbot.CZCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.CZCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.MAC10CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.MAC10CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.MP9CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.MP9CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MP7CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.MP7CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.MP5CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.MP5CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.UMPCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.UMPCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.BizonCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.BizonCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.P90CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.P90CurveX.ToString();
            }
            //////////RIFLES////////
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.AK47CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.AK47CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.M4A4CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.M4A4CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.M4A1CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.M4A1CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.GalilCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.GalilCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.FamasCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.FamasCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.SGCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.SGCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.AUGCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.AUGCurveX.ToString();
            }
            ///////////SNIPERS/////////////
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.SSGCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.SSGCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.AWPCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.AWPCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.AUTOCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.AUTOCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.NovaCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.NovaCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.XMCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.XMCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MAG7CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.MAG7CurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.SawnedOffCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.SawnedOffCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.NegevCurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.NegevCurveX.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.M249CurveX = ((float)(trackBar5.Value * 0.1));
                label20.Text = Settings.Aimbot.M249CurveX.ToString();
            }

        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.GlockPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.GlockPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.USPPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.USPPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.P2000PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.P2000PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.P250PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.P250PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.DualsPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.DualsPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.FiveSevenPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.FiveSevenPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.TEC9PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.TEC9PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
            {
                Settings.Aimbot.DEAGLEPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.DEAGLEPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                Settings.Aimbot.RevolverPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.RevolverPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 9)
            {
                Settings.Aimbot.CZPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.CZPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.MAC10PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.MAC10PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.MP9PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.MP9PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MP7PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.MP7PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.MP5PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.MP5PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.UMPPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.UMPPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.BizonPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.BizonPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.P90PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.P90PitchRecoilReductionFactory * 100).ToString();
            }
            //////////RIFLES////////
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.AK47PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.AK47PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.M4A4PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.M4A4PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.M4A1PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.M4A1PitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.GalilPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.GalilPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.FamasPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.FamasPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.SGPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.SGPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
            {
                Settings.Aimbot.AUGPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.AUGPitchRecoilReductionFactory * 100).ToString();
            }
            ///////////SNIPERS/////////////
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.SSGPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.SSGPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.AWPPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.AWPPitchRecoilReductionFactory * 100).ToString();
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.AUTOPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = (Settings.Aimbot.AUTOPitchRecoilReductionFactory * 100).ToString();
            }
            ///////////Heavy//////////
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.NovaPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = Settings.Aimbot.NovaPitchRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.XMPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = Settings.Aimbot.XMPitchRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.MAG7PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = Settings.Aimbot.MAG7PitchRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 3)
            {
                Settings.Aimbot.SawnedOffPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = Settings.Aimbot.SawnedOffPitchRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 4)
            {
                Settings.Aimbot.NegevPitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = Settings.Aimbot.NegevPitchRecoilReductionFactory.ToString();
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 5)
            {
                Settings.Aimbot.M249PitchRecoilReductionFactory = ((float)(trackBar4.Value * 0.01));
                label18.Text = Settings.Aimbot.M249PitchRecoilReductionFactory.ToString();
            }
        }
        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.FirstUSPFov = ((float)(trackBar8.Value * 0.1));
                label26.Text = Settings.Aimbot.FirstUSPFov.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.FirstAK47Fov = ((float)(trackBar8.Value * 0.1));
                label26.Text = Settings.Aimbot.FirstAK47Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.FirstM4A4Fov = ((float)(trackBar8.Value * 0.1));
                label26.Text = Settings.Aimbot.FirstM4A4Fov.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.FirstM4A1Fov = ((float)(trackBar8.Value * 0.1));
                label26.Text = Settings.Aimbot.FirstM4A1Fov.ToString();
            }
        }
        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.FirstUSPSmooth = ((float)(trackBar7.Value * 0.1));
                label24.Text = Settings.Aimbot.FirstUSPSmooth.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                Settings.Aimbot.FirstAK47Smooth = ((float)(trackBar7.Value * 0.1));
                label24.Text = Settings.Aimbot.FirstAK47Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                Settings.Aimbot.FirstM4A4Smooth = ((float)(trackBar7.Value * 0.1));
                label24.Text = Settings.Aimbot.FirstM4A4Smooth.ToString();
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                Settings.Aimbot.FirstM4A1Smooth = ((float)(trackBar7.Value * 0.1));
                label24.Text = Settings.Aimbot.FirstM4A1Smooth.ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Glock");
                comboBox3.Items.Add("USP");
                comboBox3.Items.Add("P2000");
                comboBox3.Items.Add("P250");
                comboBox3.Items.Add("Duals");
                comboBox3.Items.Add("FiveSeven");
                comboBox3.Items.Add("TEC-9");
                comboBox3.Items.Add("DEAGLE");
                comboBox3.Items.Add("REVOLVER");
                comboBox3.Items.Add("CZ");
                comboBox3.SelectedIndex = 0;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("MAC-10");
                comboBox3.Items.Add("MP9");
                comboBox3.Items.Add("MP7");
                comboBox3.Items.Add("MP5");
                comboBox3.Items.Add("UMP");
                comboBox3.Items.Add("Bizon");
                comboBox3.Items.Add("P90");
                comboBox3.SelectedIndex = 0;
            }
            if (comboBox2.SelectedIndex == 2)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("AK-47");
                comboBox3.Items.Add("M4A4");
                comboBox3.Items.Add("M4A1");
                comboBox3.Items.Add("Galil");
                comboBox3.Items.Add("Famas");
                comboBox3.Items.Add("SG 553");
                comboBox3.Items.Add("AUG");
                comboBox3.SelectedIndex = 0;
            }
            if (comboBox2.SelectedIndex == 3)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("SSG");
                comboBox3.Items.Add("AWP");
                comboBox3.Items.Add("AUTOs");
                comboBox3.SelectedIndex = 0;
            }
            if (comboBox2.SelectedIndex == 4)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Nova");
                comboBox3.Items.Add("XM1014");
                comboBox3.Items.Add("MAG-7");
                comboBox3.Items.Add("Sawned-off");
                comboBox3.Items.Add("Negev");
                comboBox3.Items.Add("M249");
                comboBox3.SelectedIndex = 0;
            }
            AimbotUpdate();
            label3.Focus();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            AimbotUpdate();
            label3.Focus();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.GlockBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.GlockBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.GlockBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.GlockBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.GlockBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.USPBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.USPBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.USPBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.USPBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.USPBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.P2000Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.P2000Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.P2000Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.P2000Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.P2000Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.P250Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.P250Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.P250Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.P250Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.P250Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.DualsBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.DualsBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.DualsBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.DualsBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.DualsBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.FiveSevenBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.FiveSevenBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.FiveSevenBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.FiveSevenBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.FiveSevenBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.TEC9Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.TEC9Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.TEC9Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.TEC9Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.TEC9Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.DEAGLEBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.DEAGLEBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.DEAGLEBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.DEAGLEBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.DEAGLEBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.RevolverBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.RevolverBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.RevolverBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.RevolverBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.RevolverBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.CZBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.CZBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.CZBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.CZBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.CZBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.MAC10Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.MAC10Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.MAC10Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.MAC10Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.MAC10Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.MP9Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.MP9Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.MP9Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.MP9Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.MP9Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.MP7Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.MP7Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.MP7Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.MP7Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.MP7Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.MP5Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.MP5Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.MP5Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.MP5Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.MP5Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.UMPBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.UMPBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.UMPBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.UMPBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.UMPBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 5)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.BizonBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.BizonBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.BizonBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.BizonBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.BizonBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 6)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.P90Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.P90Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.P90Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.P90Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.P90Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.AK47Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.AK47Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.AK47Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.AK47Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.AK47Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.M4A4Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.M4A4Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.M4A4Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.M4A4Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.M4A4Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.M4A1Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.M4A1Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.M4A1Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.M4A1Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.M4A1Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.GalilBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.GalilBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.GalilBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.GalilBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.GalilBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.FamasBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.FamasBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.FamasBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.FamasBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.FamasBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.SGBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.SGBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.SGBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.SGBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.SGBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.AUGBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.AUGBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.AUGBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.AUGBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.AUGBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.SSGBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.SSGBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.SSGBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.SSGBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.SSGBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.AWPBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.AWPBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.AWPBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.AWPBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.AWPBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.AUTOBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.AUTOBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.AUTOBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.AUTOBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.AUTOBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 0)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.NovaBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.NovaBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.NovaBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.NovaBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.NovaBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 1)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.XMBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.XMBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.XMBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.XMBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.XMBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 2)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.MAG7Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.MAG7Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.MAG7Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.MAG7Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.MAG7Bone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 3)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.SawnedOffBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.SawnedOffBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.SawnedOffBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.SawnedOffBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.SawnedOffBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 4)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.NegevBone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.NegevBone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.NegevBone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.NegevBone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.NegevBone = 0;
                }
            }
            if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 5)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Settings.Aimbot.M249Bone = 8;
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Settings.Aimbot.M249Bone = 7;
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    Settings.Aimbot.M249Bone = 6;
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    Settings.Aimbot.M249Bone = 5;
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    Settings.Aimbot.M249Bone = 0;
                }
            }
            label3.Focus();
        }
        private void AimbotUpdate()
        {

            if (Settings.Aimbot.Enabled) Toggle1.BackgroundImage = Properties.Resources.switch5;
            else Toggle1.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Aimbot.VisibleOnly) Toggle2.BackgroundImage = Properties.Resources.switch5;
            else Toggle2.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Aimbot.KillDelay) Toggle3.BackgroundImage = Properties.Resources.switch5;
            else Toggle3.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Aimbot.UseMouseEvent) Toggle8.BackgroundImage = Properties.Resources.switch5;
            else Toggle8.BackgroundImage = Properties.Resources.switch6;

            {
                if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 0)
                {
                    if (Settings.Aimbot.GlockRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (trackBar1.Visible == true)
                    {
                        Toggle4.Visible = true; label29.Visible = true;
                    }
                    if (Settings.Aimbot.GlockAutoPistol) Toggle4.BackgroundImage = Properties.Resources.switch5;
                    else Toggle4.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.GlockCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.GlockFov * 10;
                    label11.Text = Settings.Aimbot.GlockFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.GlockSmooth * 10;
                    label13.Text = Settings.Aimbot.GlockSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.GlockYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.GlockYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.GlockPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.GlockPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.GlockCurveY * 10;
                    label22.Text = Settings.Aimbot.GlockCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.GlockCurveX * 10;
                    label20.Text = Settings.Aimbot.GlockCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;

                    if (Settings.Aimbot.GlockBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.GlockBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.GlockBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.GlockBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.GlockBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 1)
                {
                    if (Settings.Aimbot.USPRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (trackBar1.Visible == true)
                    {
                        Toggle4.Visible = true; label29.Visible = true;
                    }
                    if (Settings.Aimbot.USPAutoPistol) Toggle4.BackgroundImage = Properties.Resources.switch5;
                    else Toggle4.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.USPCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.USPFov * 10;
                    label11.Text = Settings.Aimbot.USPFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.USPSmooth * 10;
                    label13.Text = Settings.Aimbot.USPSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.USPYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.USPYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.USPPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.USPPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.USPCurveY * 10;
                    label22.Text = Settings.Aimbot.USPCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.USPCurveX * 10;
                    label20.Text = Settings.Aimbot.USPCurveX.ToString();


                    if (Settings.Aimbot.FirstUSP) Toggle7.BackgroundImage = Properties.Resources.switch5;
                    else Toggle7.BackgroundImage = Properties.Resources.switch6;
                    trackBar8.Value = (int)Settings.Aimbot.FirstUSPFov * 10;
                    label26.Text = Settings.Aimbot.FirstUSPFov.ToString();

                    trackBar7.Value = (int)Settings.Aimbot.FirstUSPSmooth * 10;
                    label24.Text = Settings.Aimbot.FirstUSPSmooth.ToString();

                    if (Settings.Aimbot.USPBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.USPBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.USPBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.USPBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.USPBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 2)
                {

                    if (Settings.Aimbot.P2000RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (trackBar1.Visible == true)
                    {
                        Toggle4.Visible = true; label29.Visible = true;
                    }
                    if (Settings.Aimbot.P2000AutoPistol) Toggle4.BackgroundImage = Properties.Resources.switch5;
                    else Toggle4.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.P2000Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.P2000Fov * 10;
                    label11.Text = Settings.Aimbot.P2000Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.P2000Smooth * 10;
                    label13.Text = Settings.Aimbot.P2000Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.P2000YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.P2000YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.P2000PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.P2000PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.P2000CurveY * 10;
                    label22.Text = Settings.Aimbot.P2000CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.P2000CurveX * 10;
                    label20.Text = Settings.Aimbot.P2000CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;

                    if (Settings.Aimbot.P2000Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.P2000Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.P2000Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.P2000Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.P2000Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 3)
                {
                    if (Settings.Aimbot.P250RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (trackBar1.Visible == true)
                    {
                        Toggle4.Visible = true; label29.Visible = true;
                    }
                    if (Settings.Aimbot.P250AutoPistol) Toggle4.BackgroundImage = Properties.Resources.switch5;
                    else Toggle4.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.P250Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.P250Fov * 10;
                    label11.Text = Settings.Aimbot.P250Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.P250Smooth * 10;
                    label13.Text = Settings.Aimbot.P250Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.P250YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.P250YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.P250PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.P250PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.P250CurveY * 10;
                    label22.Text = Settings.Aimbot.P250CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.P250CurveX * 10;
                    label20.Text = Settings.Aimbot.P250CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;

                    if (Settings.Aimbot.P250Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.P250Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.P250Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.P250Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.P250Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 4)
                {
                    if (Settings.Aimbot.DualsRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (trackBar1.Visible == true)
                    {
                        Toggle4.Visible = true; label29.Visible = true;
                    }
                    if (Settings.Aimbot.DualsAutoPistol) Toggle4.BackgroundImage = Properties.Resources.switch5;
                    else Toggle4.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.DualsCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.DualsFov * 10;
                    label11.Text = Settings.Aimbot.DualsFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.DualsSmooth * 10;
                    label13.Text = Settings.Aimbot.DualsSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.DualsYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.DualsYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.DualsPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.DualsPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.DualsCurveY * 10;
                    label22.Text = Settings.Aimbot.DualsCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.DualsCurveX * 10;
                    label20.Text = Settings.Aimbot.DualsCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;

                    if (Settings.Aimbot.DualsBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.DualsBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.DualsBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.DualsBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.DualsBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 5)
                {
                    if (Settings.Aimbot.FiveSevenRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (trackBar1.Visible == true)
                    {
                        Toggle4.Visible = true; label29.Visible = true;
                    }
                    if (Settings.Aimbot.FiveSevenAutoPistol) Toggle4.BackgroundImage = Properties.Resources.switch5;
                    else Toggle4.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.FiveSevenCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.FiveSevenFov * 10;
                    label11.Text = Settings.Aimbot.FiveSevenFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.FiveSevenSmooth * 10;
                    label13.Text = Settings.Aimbot.FiveSevenSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.FiveSevenYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.FiveSevenYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.FiveSevenPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.FiveSevenPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.FiveSevenCurveY * 10;
                    label22.Text = Settings.Aimbot.FiveSevenCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.FiveSevenCurveX * 10;
                    label20.Text = Settings.Aimbot.FiveSevenCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;

                    if (Settings.Aimbot.FiveSevenBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.FiveSevenBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.FiveSevenBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.FiveSevenBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.FiveSevenBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 6)
                {
                    if (Settings.Aimbot.TEC9RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (trackBar1.Visible == true)
                    {
                        Toggle4.Visible = true; label29.Visible = true;
                    }
                    if (Settings.Aimbot.TEC9AutoPistol) Toggle4.BackgroundImage = Properties.Resources.switch5;
                    else Toggle4.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.TEC9Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.TEC9Fov * 10;
                    label11.Text = Settings.Aimbot.TEC9Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.TEC9Smooth * 10;
                    label13.Text = Settings.Aimbot.TEC9Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.TEC9YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.TEC9YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.TEC9PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.TEC9PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.TEC9CurveY * 10;
                    label22.Text = Settings.Aimbot.TEC9CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.TEC9CurveX * 10;
                    label20.Text = Settings.Aimbot.TEC9CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;

                    if (Settings.Aimbot.TEC9Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.TEC9Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.TEC9Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.TEC9Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.TEC9Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 7)
                {
                    if (Settings.Aimbot.DEAGLERecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.DEAGLECurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.DEAGLEFov * 10;
                    label11.Text = Settings.Aimbot.DEAGLEFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.DEAGLESmooth * 10;
                    label13.Text = Settings.Aimbot.DEAGLESmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.DEAGLEYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.DEAGLEYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.DEAGLEPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.DEAGLEPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.DEAGLECurveY * 10;
                    label22.Text = Settings.Aimbot.DEAGLECurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.DEAGLECurveX * 10;
                    label20.Text = Settings.Aimbot.DEAGLECurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.DEAGLEBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.DEAGLEBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.DEAGLEBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.DEAGLEBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.DEAGLEBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 8)
                {
                    if (Settings.Aimbot.RevolverRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.RevolverCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.RevolverFov * 10;
                    label11.Text = Settings.Aimbot.RevolverFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.RevolverSmooth * 10;
                    label13.Text = Settings.Aimbot.RevolverSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.RevolverYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.RevolverYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.RevolverPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.RevolverPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.RevolverCurveY * 10;
                    label22.Text = Settings.Aimbot.RevolverCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.RevolverCurveX * 10;
                    label20.Text = Settings.Aimbot.RevolverCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.RevolverBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.RevolverBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.RevolverBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.RevolverBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.RevolverBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 0 && comboBox3.SelectedIndex == 9)
                {
                    if (Settings.Aimbot.CZRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.CZCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.CZFov * 10;
                    label11.Text = Settings.Aimbot.CZFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.CZSmooth * 10;
                    label13.Text = Settings.Aimbot.CZSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.CZYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.CZYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.CZPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.CZPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.CZCurveY * 10;
                    label22.Text = Settings.Aimbot.CZCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.CZCurveX * 10;
                    label20.Text = Settings.Aimbot.CZCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.CZBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.CZBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.CZBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.CZBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.CZBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                /////////SMG//////
                else if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
                {
                    if (Settings.Aimbot.MAC10RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.MAC10Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.MAC10Fov * 10;
                    label11.Text = Settings.Aimbot.MAC10Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.MAC10Smooth * 10;
                    label13.Text = Settings.Aimbot.MAC10Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.MAC10YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.MAC10YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.MAC10PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.MAC10PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.MAC10CurveY * 10;
                    label22.Text = Settings.Aimbot.MAC10CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.MAC10CurveX * 10;
                    label20.Text = Settings.Aimbot.MAC10CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.MAC10Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.MAC10Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.MAC10Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.MAC10Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.MAC10Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 1)
                {
                    if (Settings.Aimbot.MP9RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.MP9Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.MP9Fov * 10;
                    label11.Text = Settings.Aimbot.MP9Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.MP9Smooth * 10;
                    label13.Text = Settings.Aimbot.MP9Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.MP9YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.MP9YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.MP9PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.MP9PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.MP9CurveY * 10;
                    label22.Text = Settings.Aimbot.MP9CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.MP9CurveX * 10;
                    label20.Text = Settings.Aimbot.MP9CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.MP9Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.MP9Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.MP9Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.MP9Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.MP9Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 2)
                {
                    if (Settings.Aimbot.MP7RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.MP7Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.MP7Fov * 10;
                    label11.Text = Settings.Aimbot.MP7Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.MP7Smooth * 10;
                    label13.Text = Settings.Aimbot.MP7Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.MP7YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.MP7YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.MP7PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.MP7PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.MP7CurveY * 10;
                    label22.Text = Settings.Aimbot.MP7CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.MP7CurveX * 10;
                    label20.Text = Settings.Aimbot.MP7CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.MP7Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.MP7Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.MP7Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.MP7Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.MP7Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 3)
                {
                    if (Settings.Aimbot.MP5RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.MP5Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.MP5Fov * 10;
                    label11.Text = Settings.Aimbot.MP5Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.MP5Smooth * 10;
                    label13.Text = Settings.Aimbot.MP5Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.MP5YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.MP5YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.MP5PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.MP5PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.MP5CurveY * 10;
                    label22.Text = Settings.Aimbot.MP5CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.MP5CurveX * 10;
                    label20.Text = Settings.Aimbot.MP5CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.MP5Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.MP5Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.MP5Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.MP5Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.MP5Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 4)
                {
                    if (Settings.Aimbot.UMPRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.UMPCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.UMPFov * 10;
                    label11.Text = Settings.Aimbot.UMPFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.UMPSmooth * 10;
                    label13.Text = Settings.Aimbot.UMPSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.UMPYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.UMPYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.UMPPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.UMPPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.UMPCurveY * 10;
                    label22.Text = Settings.Aimbot.UMPCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.UMPCurveX * 10;
                    label20.Text = Settings.Aimbot.UMPCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.UMPBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.UMPBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.UMPBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.UMPBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.UMPBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
                {
                    if (Settings.Aimbot.BizonRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.BizonCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.BizonFov * 10;
                    label11.Text = Settings.Aimbot.BizonFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.BizonSmooth * 10;
                    label13.Text = Settings.Aimbot.BizonSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.BizonYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.BizonYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.BizonPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.BizonPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.BizonCurveY * 10;
                    label22.Text = Settings.Aimbot.BizonCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.BizonCurveX * 10;
                    label20.Text = Settings.Aimbot.BizonCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.BizonBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.BizonBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.BizonBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.BizonBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.BizonBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 1 && comboBox3.SelectedIndex == 0)
                {
                    if (Settings.Aimbot.P90RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.P90Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.P90Fov * 10;
                    label11.Text = Settings.Aimbot.P90Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.P90Smooth * 10;
                    label13.Text = Settings.Aimbot.P90Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.P90YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.P90YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.P90PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.P90PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.P90CurveY * 10;
                    label22.Text = Settings.Aimbot.P90CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.P90CurveX * 10;
                    label20.Text = Settings.Aimbot.P90CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.P90Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.P90Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.P90Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.P90Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.P90Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 0)
                {
                    if (Settings.Aimbot.AK47RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    Toggle4.Visible = false; label29.Visible = false;
                    if (Settings.Aimbot.AK47Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.AK47Fov * 10;
                    label11.Text = Settings.Aimbot.AK47Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.AK47Smooth * 10;
                    label13.Text = Settings.Aimbot.AK47Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.AK47YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.AK47YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.AK47PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.AK47PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.AK47CurveY * 10;
                    label22.Text = Settings.Aimbot.AK47CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.AK47CurveX * 10;
                    label20.Text = Settings.Aimbot.AK47CurveX.ToString();

                    if (trackBar1.Visible == true)
                    {
                        label28.Visible = true; Toggle7.Visible = true;
                        label27.Visible = true; trackBar8.Visible = true; label26.Visible = true;
                        label25.Visible = true; trackBar7.Visible = true; label24.Visible = true;
                    }

                    if (Settings.Aimbot.FirstAK47) Toggle7.BackgroundImage = Properties.Resources.switch5;
                    else Toggle7.BackgroundImage = Properties.Resources.switch6;
                    trackBar8.Value = (int)Settings.Aimbot.FirstAK47Fov * 10;
                    label26.Text = Settings.Aimbot.FirstAK47Fov.ToString();

                    trackBar7.Value = (int)Settings.Aimbot.FirstAK47Smooth * 10;
                    label24.Text = Settings.Aimbot.FirstAK47Smooth.ToString();

                    if (Settings.Aimbot.AK47Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.AK47Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.AK47Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.AK47Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.AK47Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 1)
                {
                    if (Settings.Aimbot.M4A4RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    Toggle4.Visible = false; label29.Visible = false;
                    if (Settings.Aimbot.M4A4Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.M4A4Fov * 10;
                    label11.Text = Settings.Aimbot.M4A4Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.M4A4Smooth * 10;
                    label13.Text = Settings.Aimbot.M4A4Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.M4A4YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.M4A4YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.M4A4PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.M4A4PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.M4A4CurveY * 10;
                    label22.Text = Settings.Aimbot.M4A4CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.M4A4CurveX * 10;
                    label20.Text = Settings.Aimbot.M4A4CurveX.ToString();

                    if (trackBar1.Visible == true)
                    {
                        label28.Visible = true; Toggle7.Visible = true;
                        label27.Visible = true; trackBar8.Visible = true; label26.Visible = true;
                        label25.Visible = true; trackBar7.Visible = true; label24.Visible = true;
                    }

                    if (Settings.Aimbot.FirstM4A4) Toggle7.BackgroundImage = Properties.Resources.switch5;
                    else Toggle7.BackgroundImage = Properties.Resources.switch6;
                    trackBar8.Value = (int)Settings.Aimbot.FirstM4A4Fov * 10;
                    label26.Text = Settings.Aimbot.FirstM4A4Fov.ToString();

                    trackBar7.Value = (int)Settings.Aimbot.FirstM4A4Smooth * 10;
                    label24.Text = Settings.Aimbot.FirstM4A4Smooth.ToString();

                    if (Settings.Aimbot.M4A4Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.M4A4Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.M4A4Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.M4A4Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.M4A4Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 2)
                {
                    if (Settings.Aimbot.M4A1RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    Toggle4.Visible = false; label29.Visible = false;
                    if (Settings.Aimbot.M4A1Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.M4A1Fov * 10;
                    label11.Text = Settings.Aimbot.M4A1Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.M4A1Smooth * 10;
                    label13.Text = Settings.Aimbot.M4A1Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.M4A1YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.M4A1YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.M4A1PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.M4A1PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.M4A1CurveY * 10;
                    label22.Text = Settings.Aimbot.M4A1CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.M4A1CurveX * 10;
                    label20.Text = Settings.Aimbot.M4A1CurveX.ToString();

                    if (trackBar1.Visible == true)
                    {
                        label28.Visible = true; Toggle7.Visible = true;
                        label27.Visible = true; trackBar8.Visible = true; label26.Visible = true;
                        label25.Visible = true; trackBar7.Visible = true; label24.Visible = true;
                    }

                    if (Settings.Aimbot.FirstM4A1) Toggle7.BackgroundImage = Properties.Resources.switch5;
                    else Toggle7.BackgroundImage = Properties.Resources.switch6;
                    trackBar8.Value = (int)Settings.Aimbot.FirstM4A1Fov * 10;
                    label26.Text = Settings.Aimbot.FirstM4A1Fov.ToString();

                    trackBar7.Value = (int)Settings.Aimbot.FirstM4A1Smooth * 10;
                    label24.Text = Settings.Aimbot.FirstM4A1Smooth.ToString();

                    if (Settings.Aimbot.M4A1Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.M4A1Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.M4A1Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.M4A1Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.M4A1Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 3)
                {
                    if (Settings.Aimbot.GalilRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.GalilCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.GalilFov * 10;
                    label11.Text = Settings.Aimbot.GalilFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.GalilSmooth * 10;
                    label13.Text = Settings.Aimbot.GalilSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.GalilYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.GalilYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.GalilPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.GalilPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.GalilCurveY * 10;
                    label22.Text = Settings.Aimbot.GalilCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.GalilCurveX * 10;
                    label20.Text = Settings.Aimbot.GalilCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.GalilBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.GalilBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.GalilBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.GalilBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.GalilBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 4)
                {
                    if (Settings.Aimbot.FamasRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.FamasCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.FamasFov * 10;
                    label11.Text = Settings.Aimbot.FamasFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.FamasSmooth * 10;
                    label13.Text = Settings.Aimbot.FamasSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.FamasYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.FamasYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.FamasPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.FamasPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.FamasCurveY * 10;
                    label22.Text = Settings.Aimbot.FamasCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.FamasCurveX * 10;
                    label20.Text = Settings.Aimbot.FamasCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.FamasBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.FamasBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.FamasBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.FamasBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.FamasBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 5)
                {
                    if (Settings.Aimbot.SGRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.SGCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.SGFov * 10;
                    label11.Text = Settings.Aimbot.SGFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.SGSmooth * 10;
                    label13.Text = Settings.Aimbot.SGSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.SGYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.SGYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.SGPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.SGPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.SGCurveY * 10;
                    label22.Text = Settings.Aimbot.SGCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.SGCurveX * 10;
                    label20.Text = Settings.Aimbot.SGCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.SGBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.SGBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.SGBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.SGBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.SGBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 2 && comboBox3.SelectedIndex == 6)
                {
                    if (Settings.Aimbot.AUGRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.AUGCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.AUGFov * 10;
                    label11.Text = Settings.Aimbot.AUGFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.AUGSmooth * 10;
                    label13.Text = Settings.Aimbot.AUGSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.AUGYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.AUGYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.AUGPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.AUGPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.AUGCurveY * 10;
                    label22.Text = Settings.Aimbot.AUGCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.AUGCurveX * 10;
                    label20.Text = Settings.Aimbot.AUGCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.AUGBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.AUGBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.AUGBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.AUGBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.AUGBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 0)
                {
                    if (Settings.Aimbot.SSGRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.SSGCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.SSGFov * 10;
                    label11.Text = Settings.Aimbot.SSGFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.SSGSmooth * 10;
                    label13.Text = Settings.Aimbot.SSGSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.SSGYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.SSGYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.SSGPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.SSGPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.SSGCurveY * 10;
                    label22.Text = Settings.Aimbot.SSGCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.SSGCurveX * 10;
                    label20.Text = Settings.Aimbot.SSGCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.SSGBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.SSGBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.SSGBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.SSGBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.SSGBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 1)
                {
                    if (Settings.Aimbot.AWPRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.AWPCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.AWPFov * 10;
                    label11.Text = Settings.Aimbot.AWPFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.AWPSmooth * 10;
                    label13.Text = Settings.Aimbot.AWPSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.AWPYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.AWPYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.AWPPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.AWPPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.AWPCurveY * 10;
                    label22.Text = Settings.Aimbot.AWPCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.AWPCurveX * 10;
                    label20.Text = Settings.Aimbot.AWPCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.AWPBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.AWPBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.AWPBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.AWPBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.AWPBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 3 && comboBox3.SelectedIndex == 2)
                {
                    if (Settings.Aimbot.AUTORecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.AUTOCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.AUTOFov * 10;
                    label11.Text = Settings.Aimbot.AUTOFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.AUTOSmooth * 10;
                    label13.Text = Settings.Aimbot.AUTOSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.AUTOYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.AUTOYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.AUTOPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.AUTOPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.AUTOCurveY * 10;
                    label22.Text = Settings.Aimbot.AUTOCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.AUTOCurveX * 10;
                    label20.Text = Settings.Aimbot.AUTOCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.AUTOBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.AUTOBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.AUTOBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.AUTOBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.AUTOBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 0)
                {
                    if (Settings.Aimbot.NovaRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.NovaCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.NovaFov * 10;
                    label11.Text = Settings.Aimbot.NovaFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.NovaSmooth * 10;
                    label13.Text = Settings.Aimbot.NovaSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.NovaYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.NovaYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.NovaPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.NovaPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.NovaCurveY * 10;
                    label22.Text = Settings.Aimbot.NovaCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.NovaCurveX * 10;
                    label20.Text = Settings.Aimbot.NovaCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.NovaBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.NovaBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.NovaBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.NovaBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.NovaBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 1)
                {
                    if (Settings.Aimbot.XMRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.XMCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.XMFov * 10;
                    label11.Text = Settings.Aimbot.XMFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.XMSmooth * 10;
                    label13.Text = Settings.Aimbot.XMSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.XMYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.XMYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.XMPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.XMPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.XMCurveY * 10;
                    label22.Text = Settings.Aimbot.XMCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.XMCurveX * 10;
                    label20.Text = Settings.Aimbot.XMCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.XMBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.XMBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.XMBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.XMBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.XMBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 2)
                {
                    if (Settings.Aimbot.MAG7RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.MAG7Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.MAG7Fov * 10;
                    label11.Text = Settings.Aimbot.MAG7Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.MAG7Smooth * 10;
                    label13.Text = Settings.Aimbot.MAG7Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.MAG7YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.MAG7YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.MAG7PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.MAG7PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.MAG7CurveY * 10;
                    label22.Text = Settings.Aimbot.MAG7CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.MAG7CurveX * 10;
                    label20.Text = Settings.Aimbot.MAG7CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.MAG7Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.MAG7Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.MAG7Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.MAG7Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.MAG7Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 3)
                {
                    if (Settings.Aimbot.SawnedOffRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.SawnedOffCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.SawnedOffFov * 10;
                    label11.Text = Settings.Aimbot.SawnedOffFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.SawnedOffSmooth * 10;
                    label13.Text = Settings.Aimbot.SawnedOffSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.SawnedOffYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.SawnedOffYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.SawnedOffPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.SawnedOffPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.SawnedOffCurveY * 10;
                    label22.Text = Settings.Aimbot.SawnedOffCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.SawnedOffCurveX * 10;
                    label20.Text = Settings.Aimbot.SawnedOffCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.SawnedOffBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.SawnedOffBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.SawnedOffBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.SawnedOffBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.SawnedOffBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 4)
                {
                    if (Settings.Aimbot.NegevRecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.NegevCurve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.NegevFov * 10;
                    label11.Text = Settings.Aimbot.NegevFov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.NegevSmooth * 10;
                    label13.Text = Settings.Aimbot.NegevSmooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.NegevYawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.NegevYawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.NegevPitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.NegevPitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.NegevCurveY * 10;
                    label22.Text = Settings.Aimbot.NegevCurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.NegevCurveX * 10;
                    label20.Text = Settings.Aimbot.NegevCurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.NegevBone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.NegevBone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.NegevBone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.NegevBone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.NegevBone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
                else if (comboBox2.SelectedIndex == 4 && comboBox3.SelectedIndex == 5)
                {
                    if (Settings.Aimbot.M249RecoilControl) Toggle5.BackgroundImage = Properties.Resources.switch5;
                    else Toggle5.BackgroundImage = Properties.Resources.switch6;
                    if (Settings.Aimbot.M249Curve) Toggle6.BackgroundImage = Properties.Resources.switch5;
                    else Toggle6.BackgroundImage = Properties.Resources.switch6;

                    trackBar1.Value = (int)Settings.Aimbot.M249Fov * 10;
                    label11.Text = Settings.Aimbot.M249Fov.ToString();
                    trackBar2.Value = (int)Settings.Aimbot.M249Smooth * 10;
                    label13.Text = Settings.Aimbot.M249Smooth.ToString();

                    trackBar3.Value = (int)Settings.Aimbot.M249YawRecoilReductionFactory * 100;
                    label16.Text = (Settings.Aimbot.M249YawRecoilReductionFactory * 100).ToString();
                    trackBar4.Value = (int)Settings.Aimbot.M249PitchRecoilReductionFactory * 100;
                    label18.Text = (Settings.Aimbot.M249PitchRecoilReductionFactory * 100).ToString();

                    trackBar6.Value = (int)Settings.Aimbot.M249CurveY * 10;
                    label22.Text = Settings.Aimbot.M249CurveY.ToString();

                    trackBar5.Value = (int)Settings.Aimbot.M249CurveX * 10;
                    label20.Text = Settings.Aimbot.M249CurveX.ToString();

                    label28.Visible = false; Toggle7.Visible = false;
                    label27.Visible = false; trackBar8.Visible = false; label26.Visible = false;
                    label25.Visible = false; trackBar7.Visible = false; label24.Visible = false;
                    Toggle4.Visible = false; label29.Visible = false;

                    if (Settings.Aimbot.M249Bone == 8)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (Settings.Aimbot.M249Bone == 7)
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    if (Settings.Aimbot.M249Bone == 6)
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    if (Settings.Aimbot.M249Bone == 5)
                    {
                        comboBox1.SelectedIndex = 3;
                    }
                    if (Settings.Aimbot.M249Bone == 0)
                    {
                        comboBox1.SelectedIndex = 4;
                    }
                }
            }
            this.Refresh();
        }

        #region GlowButtons
        private void GlowButton1_Click(object sender, EventArgs e)
        {
            GlowIndex = 1;
        }

        private void GlowButton2_Click(object sender, EventArgs e)
        {
            GlowIndex = 2;
        }

        private void GlowButton3_Click(object sender, EventArgs e)
        {
            GlowIndex = 3;
        }

        private void GlowButton4_Click(object sender, EventArgs e)
        {
            GlowIndex = 4;
        }

        private void GlowButton5_Click(object sender, EventArgs e)
        {
            GlowIndex = 5;
        }

        private void GlowButton6_Click(object sender, EventArgs e)
        {
            GlowIndex = 6;
        }

        private void GlowButton7_Click(object sender, EventArgs e)
        {
            GlowIndex = 7;
        }

        private void GlowButton8_Click(object sender, EventArgs e)
        {
            GlowIndex = 8;
        }

        private void GlowButton9_Click(object sender, EventArgs e)
        {
            GlowIndex = 9;
        }

        private void GlowButton10_Click(object sender, EventArgs e)
        {
            GlowIndex = 10;
        }
        #endregion

        private void ToggleGlow1_Click(object sender, EventArgs e)
        {
            Settings.Glow.Enabled = !Settings.Glow.Enabled;
            if (Settings.Glow.Enabled)
            {
                ThreadManager.StartThread("Glow");
                UI.ToggleAnimathionOn(GlowToggle1);
            }
            else
            {
                ThreadManager.PauseThread("Glow");
                UI.ToggleAnimathionOff(GlowToggle1);
            }
        }
        #region CfgUpdate
        private void CFGUpdate()
        {
            if (Settings.Glow.Enabled) GlowToggle1.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle1.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.bSpotted) GlowToggle2.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle2.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.Allies) GlowToggle3.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle3.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.ShowWeapons) GlowToggle4.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle4.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.FullBloom) GlowToggle5.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle5.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Glow.HealthBased) GlowToggle6.BackgroundImage = Properties.Resources.switch5;
            else GlowToggle6.BackgroundImage = Properties.Resources.switch6;

            GlowButton1.BackColor = Color.FromArgb(255, (int)Settings.Glow.Enemies_Color_R, (int)Settings.Glow.Enemies_Color_G, (int)Settings.Glow.Enemies_Color_B);
            GlowButton2.BackColor = Color.FromArgb(255, (int)Settings.Glow.InvisibleEnemies_Color_R, (int)Settings.Glow.InvisibleEnemies_Color_G, (int)Settings.Glow.InvisibleEnemies_Color_B);
            GlowButton3.BackColor = Color.FromArgb(255, (int)Settings.Glow.Snipers_Color_R, (int)Settings.Glow.Snipers_Color_G, (int)Settings.Glow.Snipers_Color_B);
            GlowButton4.BackColor = Color.FromArgb(255, (int)Settings.Glow.Rifles_Color_R, (int)Settings.Glow.Rifles_Color_G, (int)Settings.Glow.Rifles_Color_B);
            GlowButton5.BackColor = Color.FromArgb(255, (int)Settings.Glow.MPs_Color_R, (int)Settings.Glow.MPs_Color_G, (int)Settings.Glow.MPs_Color_B);
            GlowButton6.BackColor = Color.FromArgb(255, (int)Settings.Glow.Allies_Color_R, (int)Settings.Glow.Allies_Color_G, (int)Settings.Glow.Allies_Color_B);
            GlowButton7.BackColor = Color.FromArgb(255, (int)Settings.Glow.Pistols_Color_R, (int)Settings.Glow.Pistols_Color_G, (int)Settings.Glow.Pistols_Color_B);
            GlowButton8.BackColor = Color.FromArgb(255, (int)Settings.Glow.Heavy_Color_R, (int)Settings.Glow.Heavy_Color_G, (int)Settings.Glow.Heavy_Color_B);
            GlowButton9.BackColor = Color.FromArgb(255, (int)Settings.Glow.C4_Color_R, (int)Settings.Glow.C4_Color_G, (int)Settings.Glow.C4_Color_B);
            GlowButton10.BackColor = Color.FromArgb(255, (int)Settings.Glow.Grenades_Color_R, (int)Settings.Glow.Grenades_Color_G, (int)Settings.Glow.Grenades_Color_B);


            if (Settings.Bunnyhop.Enabled) MiscToggle1.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle1.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Bunnyhop.AutoStrafe) MiscToggle2.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle2.BackgroundImage = Properties.Resources.switch6;

            if (Settings.AimAssist.Enabled) MiscToggle3.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle3.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Sonar.Enabled) MiscToggle4.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle4.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Trigger.Enabled) MiscToggle5.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle5.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Bunnyhop.StrafeEmulator) MiscToggle6.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle6.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Radar.Enabled) MiscToggle8.BackgroundImage = Properties.Resources.switch5;
            else MiscToggle8.BackgroundImage = Properties.Resources.switch6;

            trackBar9.Value = Settings.Trigger.Delay;
            MiscLabel8.Text = Settings.Trigger.Delay.ToString();

            trackBar10.Value = Settings.Trigger.DelayBetweenShots;
            MiscLabel10.Text = Settings.Trigger.DelayBetweenShots.ToString();

            trackBar12.Value = Settings.Bunnyhop.sens;
            MiscLabel12.Text = Settings.Bunnyhop.sens.ToString();

            trackBar11.Value = Settings.Bunnyhop.speed;
            MiscLabel14.Text = Settings.Bunnyhop.speed.ToString();

            //KEYS//

            {
                if (Settings.Aimbot.Key == Keyboard.VK_LBUTTON) KeyPicker1.SelectedIndex = 0;
                if (Settings.Aimbot.Key == Keyboard.VK_RBUTTON) KeyPicker1.SelectedIndex = 1;
                if (Settings.Aimbot.Key == Keyboard.VK_MBUTTON) KeyPicker1.SelectedIndex = 2;
                if (Settings.Aimbot.Key == Keyboard.VK_XBUTTON1) KeyPicker1.SelectedIndex = 3;
                if (Settings.Aimbot.Key == Keyboard.VK_XBUTTON2) KeyPicker1.SelectedIndex = 4;
                if (Settings.Aimbot.Key == Keyboard.VK_MENU) KeyPicker1.SelectedIndex = 5;
                if (Settings.Aimbot.Key == Keyboard.VK_SHIFT) KeyPicker1.SelectedIndex = 6;
                if (Settings.Aimbot.Key == Keyboard.VK_CAPITAL) KeyPicker1.SelectedIndex = 7;
                if (Settings.Aimbot.Key == Keyboard.VK_V) KeyPicker1.SelectedIndex = 8;
                if (Settings.Aimbot.Key == Keyboard.VK_C) KeyPicker1.SelectedIndex = 9;
                if (Settings.Aimbot.Key == Keyboard.VK_B) KeyPicker1.SelectedIndex = 10;
                if (Settings.Aimbot.Key == Keyboard.VK_F) KeyPicker1.SelectedIndex = 11;
                if (Settings.Aimbot.Key == Keyboard.VK_E) KeyPicker1.SelectedIndex = 12;
                if (Settings.Aimbot.Key == Keyboard.VK_Q) KeyPicker1.SelectedIndex = 13;
                if (Settings.Aimbot.Key == Keyboard.VK_W) KeyPicker1.SelectedIndex = 14;
                if (Settings.Aimbot.Key == Keyboard.VK_R) KeyPicker1.SelectedIndex = 15;
                if (Settings.Aimbot.Key == Keyboard.VK_T) KeyPicker1.SelectedIndex = 16;
                if (Settings.Aimbot.Key == Keyboard.VK_Y) KeyPicker1.SelectedIndex = 17;
                if (Settings.Aimbot.Key == Keyboard.VK_U) KeyPicker1.SelectedIndex = 18;
                if (Settings.Aimbot.Key == Keyboard.VK_I) KeyPicker1.SelectedIndex = 19;
                if (Settings.Aimbot.Key == Keyboard.VK_O) KeyPicker1.SelectedIndex = 20;
                if (Settings.Aimbot.Key == Keyboard.VK_P) KeyPicker1.SelectedIndex = 21;
                if (Settings.Aimbot.Key == Keyboard.VK_G) KeyPicker1.SelectedIndex = 22;
                if (Settings.Aimbot.Key == Keyboard.VK_H) KeyPicker1.SelectedIndex = 23;
                if (Settings.Aimbot.Key == Keyboard.VK_J) KeyPicker1.SelectedIndex = 24;
                if (Settings.Aimbot.Key == Keyboard.VK_K) KeyPicker1.SelectedIndex = 25;
                if (Settings.Aimbot.Key == Keyboard.VK_L) KeyPicker1.SelectedIndex = 26;
                if (Settings.Aimbot.Key == Keyboard.VK_Z) KeyPicker1.SelectedIndex = 27;
                if (Settings.Aimbot.Key == Keyboard.VK_X) KeyPicker1.SelectedIndex = 28;
                if (Settings.Aimbot.Key == Keyboard.VK_N) KeyPicker1.SelectedIndex = 29;
                if (Settings.Aimbot.Key == Keyboard.VK_M) KeyPicker1.SelectedIndex = 30;
            }
            {
                if (Settings.Aimbot.SecondKey == Keyboard.VK_LBUTTON) KeyPicker2.SelectedIndex = 0;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_RBUTTON) KeyPicker2.SelectedIndex = 1;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_MBUTTON) KeyPicker2.SelectedIndex = 2;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_XBUTTON1) KeyPicker2.SelectedIndex = 3;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_XBUTTON2) KeyPicker2.SelectedIndex = 4;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_MENU) KeyPicker2.SelectedIndex = 5;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_SHIFT) KeyPicker2.SelectedIndex = 6;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_CAPITAL) KeyPicker2.SelectedIndex = 7;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_V) KeyPicker2.SelectedIndex = 8;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_C) KeyPicker2.SelectedIndex = 9;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_B) KeyPicker2.SelectedIndex = 10;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_F) KeyPicker2.SelectedIndex = 11;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_E) KeyPicker2.SelectedIndex = 12;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_Q) KeyPicker2.SelectedIndex = 13;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_W) KeyPicker2.SelectedIndex = 14;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_R) KeyPicker2.SelectedIndex = 15;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_T) KeyPicker2.SelectedIndex = 16;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_Y) KeyPicker2.SelectedIndex = 17;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_U) KeyPicker2.SelectedIndex = 18;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_I) KeyPicker2.SelectedIndex = 19;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_O) KeyPicker2.SelectedIndex = 20;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_P) KeyPicker2.SelectedIndex = 21;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_G) KeyPicker2.SelectedIndex = 22;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_H) KeyPicker2.SelectedIndex = 23;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_J) KeyPicker2.SelectedIndex = 24;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_K) KeyPicker2.SelectedIndex = 25;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_L) KeyPicker2.SelectedIndex = 26;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_Z) KeyPicker2.SelectedIndex = 27;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_X) KeyPicker2.SelectedIndex = 28;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_N) KeyPicker2.SelectedIndex = 29;
                if (Settings.Aimbot.SecondKey == Keyboard.VK_M) KeyPicker2.SelectedIndex = 30;
            }
            {
                if (Settings.Sonar.Key == Keyboard.VK_TAB) KeyPicker3.SelectedIndex = 0;
                if (Settings.Sonar.Key == Keyboard.VK_RBUTTON) KeyPicker3.SelectedIndex = 1;
                if (Settings.Sonar.Key == Keyboard.VK_MBUTTON) KeyPicker3.SelectedIndex = 2;
                if (Settings.Sonar.Key == Keyboard.VK_XBUTTON1) KeyPicker3.SelectedIndex = 3;
                if (Settings.Sonar.Key == Keyboard.VK_XBUTTON2) KeyPicker3.SelectedIndex = 4;
                if (Settings.Sonar.Key == Keyboard.VK_MENU) KeyPicker3.SelectedIndex = 5;
                if (Settings.Sonar.Key == Keyboard.VK_SHIFT) KeyPicker3.SelectedIndex = 6;
                if (Settings.Sonar.Key == Keyboard.VK_CAPITAL) KeyPicker3.SelectedIndex = 7;
                if (Settings.Sonar.Key == Keyboard.VK_V) KeyPicker3.SelectedIndex = 8;
                if (Settings.Sonar.Key == Keyboard.VK_C) KeyPicker3.SelectedIndex = 9;
                if (Settings.Sonar.Key == Keyboard.VK_B) KeyPicker3.SelectedIndex = 10;
                if (Settings.Sonar.Key == Keyboard.VK_F) KeyPicker3.SelectedIndex = 11;
                if (Settings.Sonar.Key == Keyboard.VK_E) KeyPicker3.SelectedIndex = 12;
                if (Settings.Sonar.Key == Keyboard.VK_Q) KeyPicker3.SelectedIndex = 13;
                if (Settings.Sonar.Key == Keyboard.VK_W) KeyPicker3.SelectedIndex = 14;
                if (Settings.Sonar.Key == Keyboard.VK_R) KeyPicker3.SelectedIndex = 15;
                if (Settings.Sonar.Key == Keyboard.VK_T) KeyPicker3.SelectedIndex = 16;
                if (Settings.Sonar.Key == Keyboard.VK_Y) KeyPicker3.SelectedIndex = 17;
                if (Settings.Sonar.Key == Keyboard.VK_U) KeyPicker3.SelectedIndex = 18;
                if (Settings.Sonar.Key == Keyboard.VK_I) KeyPicker3.SelectedIndex = 19;
                if (Settings.Sonar.Key == Keyboard.VK_O) KeyPicker3.SelectedIndex = 20;
                if (Settings.Sonar.Key == Keyboard.VK_P) KeyPicker3.SelectedIndex = 21;
                if (Settings.Sonar.Key == Keyboard.VK_G) KeyPicker3.SelectedIndex = 22;
                if (Settings.Sonar.Key == Keyboard.VK_H) KeyPicker3.SelectedIndex = 23;
                if (Settings.Sonar.Key == Keyboard.VK_J) KeyPicker3.SelectedIndex = 24;
                if (Settings.Sonar.Key == Keyboard.VK_K) KeyPicker3.SelectedIndex = 25;
                if (Settings.Sonar.Key == Keyboard.VK_L) KeyPicker3.SelectedIndex = 26;
                if (Settings.Sonar.Key == Keyboard.VK_Z) KeyPicker3.SelectedIndex = 27;
                if (Settings.Sonar.Key == Keyboard.VK_X) KeyPicker3.SelectedIndex = 28;
                if (Settings.Sonar.Key == Keyboard.VK_N) KeyPicker3.SelectedIndex = 29;
                if (Settings.Sonar.Key == Keyboard.VK_M) KeyPicker3.SelectedIndex = 30;
            }
            {
                if (Settings.AimAssist.Key == Keyboard.VK_TAB) KeyPicker4.SelectedIndex = 0;
                if (Settings.AimAssist.Key == Keyboard.VK_RBUTTON) KeyPicker4.SelectedIndex = 1;
                if (Settings.AimAssist.Key == Keyboard.VK_MBUTTON) KeyPicker4.SelectedIndex = 2;
                if (Settings.AimAssist.Key == Keyboard.VK_XBUTTON1) KeyPicker4.SelectedIndex = 3;
                if (Settings.AimAssist.Key == Keyboard.VK_XBUTTON2) KeyPicker4.SelectedIndex = 4;
                if (Settings.AimAssist.Key == Keyboard.VK_MENU) KeyPicker4.SelectedIndex = 5;
                if (Settings.AimAssist.Key == Keyboard.VK_SHIFT) KeyPicker4.SelectedIndex = 6;
                if (Settings.AimAssist.Key == Keyboard.VK_CAPITAL) KeyPicker4.SelectedIndex = 7;
                if (Settings.AimAssist.Key == Keyboard.VK_V) KeyPicker4.SelectedIndex = 8;
                if (Settings.AimAssist.Key == Keyboard.VK_C) KeyPicker4.SelectedIndex = 9;
                if (Settings.AimAssist.Key == Keyboard.VK_B) KeyPicker4.SelectedIndex = 10;
                if (Settings.AimAssist.Key == Keyboard.VK_F) KeyPicker4.SelectedIndex = 11;
                if (Settings.AimAssist.Key == Keyboard.VK_E) KeyPicker4.SelectedIndex = 12;
                if (Settings.AimAssist.Key == Keyboard.VK_Q) KeyPicker4.SelectedIndex = 13;
                if (Settings.AimAssist.Key == Keyboard.VK_W) KeyPicker4.SelectedIndex = 14;
                if (Settings.AimAssist.Key == Keyboard.VK_R) KeyPicker4.SelectedIndex = 15;
                if (Settings.AimAssist.Key == Keyboard.VK_T) KeyPicker4.SelectedIndex = 16;
                if (Settings.AimAssist.Key == Keyboard.VK_Y) KeyPicker4.SelectedIndex = 17;
                if (Settings.AimAssist.Key == Keyboard.VK_U) KeyPicker4.SelectedIndex = 18;
                if (Settings.AimAssist.Key == Keyboard.VK_I) KeyPicker4.SelectedIndex = 19;
                if (Settings.AimAssist.Key == Keyboard.VK_O) KeyPicker4.SelectedIndex = 20;
                if (Settings.AimAssist.Key == Keyboard.VK_P) KeyPicker4.SelectedIndex = 21;
                if (Settings.AimAssist.Key == Keyboard.VK_G) KeyPicker4.SelectedIndex = 22;
                if (Settings.AimAssist.Key == Keyboard.VK_H) KeyPicker4.SelectedIndex = 23;
                if (Settings.AimAssist.Key == Keyboard.VK_J) KeyPicker4.SelectedIndex = 24;
                if (Settings.AimAssist.Key == Keyboard.VK_K) KeyPicker4.SelectedIndex = 25;
                if (Settings.AimAssist.Key == Keyboard.VK_L) KeyPicker4.SelectedIndex = 26;
                if (Settings.AimAssist.Key == Keyboard.VK_Z) KeyPicker4.SelectedIndex = 27;
                if (Settings.AimAssist.Key == Keyboard.VK_X) KeyPicker4.SelectedIndex = 28;
                if (Settings.AimAssist.Key == Keyboard.VK_N) KeyPicker4.SelectedIndex = 29;
                if (Settings.AimAssist.Key == Keyboard.VK_M) KeyPicker4.SelectedIndex = 30;
            }
            {
                if (Settings.Trigger.Key == Keyboard.VK_LBUTTON) KeyPicker7.SelectedIndex = 0;
                if (Settings.Trigger.Key == Keyboard.VK_RBUTTON) KeyPicker7.SelectedIndex = 1;
                if (Settings.Trigger.Key == Keyboard.VK_MBUTTON) KeyPicker7.SelectedIndex = 2;
                if (Settings.Trigger.Key == Keyboard.VK_XBUTTON1) KeyPicker7.SelectedIndex = 3;
                if (Settings.Trigger.Key == Keyboard.VK_XBUTTON2) KeyPicker7.SelectedIndex = 4;
                if (Settings.Trigger.Key == Keyboard.VK_MENU) KeyPicker7.SelectedIndex = 5;
                if (Settings.Trigger.Key == Keyboard.VK_SHIFT) KeyPicker7.SelectedIndex = 6;
                if (Settings.Trigger.Key == Keyboard.VK_CAPITAL) KeyPicker7.SelectedIndex = 7;
                if (Settings.Trigger.Key == Keyboard.VK_V) KeyPicker7.SelectedIndex = 8;
                if (Settings.Trigger.Key == Keyboard.VK_C) KeyPicker7.SelectedIndex = 9;
                if (Settings.Trigger.Key == Keyboard.VK_B) KeyPicker7.SelectedIndex = 10;
                if (Settings.Trigger.Key == Keyboard.VK_F) KeyPicker7.SelectedIndex = 11;
                if (Settings.Trigger.Key == Keyboard.VK_E) KeyPicker7.SelectedIndex = 12;
                if (Settings.Trigger.Key == Keyboard.VK_Q) KeyPicker7.SelectedIndex = 13;
                if (Settings.Trigger.Key == Keyboard.VK_W) KeyPicker7.SelectedIndex = 14;
                if (Settings.Trigger.Key == Keyboard.VK_R) KeyPicker7.SelectedIndex = 15;
                if (Settings.Trigger.Key == Keyboard.VK_T) KeyPicker7.SelectedIndex = 16;
                if (Settings.Trigger.Key == Keyboard.VK_Y) KeyPicker7.SelectedIndex = 17;
                if (Settings.Trigger.Key == Keyboard.VK_U) KeyPicker7.SelectedIndex = 18;
                if (Settings.Trigger.Key == Keyboard.VK_I) KeyPicker7.SelectedIndex = 19;
                if (Settings.Trigger.Key == Keyboard.VK_O) KeyPicker7.SelectedIndex = 20;
                if (Settings.Trigger.Key == Keyboard.VK_P) KeyPicker7.SelectedIndex = 21;
                if (Settings.Trigger.Key == Keyboard.VK_G) KeyPicker7.SelectedIndex = 22;
                if (Settings.Trigger.Key == Keyboard.VK_H) KeyPicker7.SelectedIndex = 23;
                if (Settings.Trigger.Key == Keyboard.VK_J) KeyPicker7.SelectedIndex = 24;
                if (Settings.Trigger.Key == Keyboard.VK_K) KeyPicker7.SelectedIndex = 25;
                if (Settings.Trigger.Key == Keyboard.VK_L) KeyPicker7.SelectedIndex = 26;
                if (Settings.Trigger.Key == Keyboard.VK_Z) KeyPicker7.SelectedIndex = 27;
                if (Settings.Trigger.Key == Keyboard.VK_X) KeyPicker7.SelectedIndex = 28;
                if (Settings.Trigger.Key == Keyboard.VK_N) KeyPicker7.SelectedIndex = 29;
                if (Settings.Trigger.Key == Keyboard.VK_M) KeyPicker7.SelectedIndex = 30;
            }
            {
                if (Settings.Bunnyhop.Key == Keyboard.VK_SPACE) KeyPicker5.SelectedIndex = 0;
                if (Settings.Bunnyhop.Key == Keyboard.VK_RBUTTON) KeyPicker5.SelectedIndex = 1;
                if (Settings.Bunnyhop.Key == Keyboard.VK_MBUTTON) KeyPicker5.SelectedIndex = 2;
                if (Settings.Bunnyhop.Key == Keyboard.VK_XBUTTON1) KeyPicker5.SelectedIndex = 3;
                if (Settings.Bunnyhop.Key == Keyboard.VK_XBUTTON2) KeyPicker5.SelectedIndex = 4;
                if (Settings.Bunnyhop.Key == Keyboard.VK_MENU) KeyPicker5.SelectedIndex = 5;
                if (Settings.Bunnyhop.Key == Keyboard.VK_SHIFT) KeyPicker5.SelectedIndex = 6;
                if (Settings.Bunnyhop.Key == Keyboard.VK_CAPITAL) KeyPicker5.SelectedIndex = 7;
                if (Settings.Bunnyhop.Key == Keyboard.VK_V) KeyPicker5.SelectedIndex = 8;
                if (Settings.Bunnyhop.Key == Keyboard.VK_C) KeyPicker5.SelectedIndex = 9;
                if (Settings.Bunnyhop.Key == Keyboard.VK_B) KeyPicker5.SelectedIndex = 10;
                if (Settings.Bunnyhop.Key == Keyboard.VK_F) KeyPicker5.SelectedIndex = 11;
                if (Settings.Bunnyhop.Key == Keyboard.VK_E) KeyPicker5.SelectedIndex = 12;
                if (Settings.Bunnyhop.Key == Keyboard.VK_Q) KeyPicker5.SelectedIndex = 13;
                if (Settings.Bunnyhop.Key == Keyboard.VK_W) KeyPicker5.SelectedIndex = 14;
                if (Settings.Bunnyhop.Key == Keyboard.VK_R) KeyPicker5.SelectedIndex = 15;
                if (Settings.Bunnyhop.Key == Keyboard.VK_T) KeyPicker5.SelectedIndex = 16;
                if (Settings.Bunnyhop.Key == Keyboard.VK_Y) KeyPicker5.SelectedIndex = 17;
                if (Settings.Bunnyhop.Key == Keyboard.VK_U) KeyPicker5.SelectedIndex = 18;
                if (Settings.Bunnyhop.Key == Keyboard.VK_I) KeyPicker5.SelectedIndex = 19;
                if (Settings.Bunnyhop.Key == Keyboard.VK_O) KeyPicker5.SelectedIndex = 20;
                if (Settings.Bunnyhop.Key == Keyboard.VK_P) KeyPicker5.SelectedIndex = 21;
                if (Settings.Bunnyhop.Key == Keyboard.VK_G) KeyPicker5.SelectedIndex = 22;
                if (Settings.Bunnyhop.Key == Keyboard.VK_H) KeyPicker5.SelectedIndex = 23;
                if (Settings.Bunnyhop.Key == Keyboard.VK_J) KeyPicker5.SelectedIndex = 24;
                if (Settings.Bunnyhop.Key == Keyboard.VK_K) KeyPicker5.SelectedIndex = 25;
                if (Settings.Bunnyhop.Key == Keyboard.VK_L) KeyPicker5.SelectedIndex = 26;
                if (Settings.Bunnyhop.Key == Keyboard.VK_Z) KeyPicker5.SelectedIndex = 27;
                if (Settings.Bunnyhop.Key == Keyboard.VK_X) KeyPicker5.SelectedIndex = 28;
                if (Settings.Bunnyhop.Key == Keyboard.VK_N) KeyPicker5.SelectedIndex = 29;
                if (Settings.Bunnyhop.Key == Keyboard.VK_M) KeyPicker5.SelectedIndex = 30;
            }
            {
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_SPACE) KeyPicker6.SelectedIndex = 0;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_RBUTTON) KeyPicker6.SelectedIndex = 1;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_MBUTTON) KeyPicker6.SelectedIndex = 2;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_XBUTTON1) KeyPicker6.SelectedIndex = 3;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_XBUTTON2) KeyPicker6.SelectedIndex = 4;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_MENU) KeyPicker6.SelectedIndex = 5;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_SHIFT) KeyPicker6.SelectedIndex = 6;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_CAPITAL) KeyPicker6.SelectedIndex = 7;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_V) KeyPicker6.SelectedIndex = 8;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_C) KeyPicker6.SelectedIndex = 9;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_B) KeyPicker6.SelectedIndex = 10;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_F) KeyPicker6.SelectedIndex = 11;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_E) KeyPicker6.SelectedIndex = 12;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_Q) KeyPicker6.SelectedIndex = 13;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_W) KeyPicker6.SelectedIndex = 14;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_R) KeyPicker6.SelectedIndex = 15;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_T) KeyPicker6.SelectedIndex = 16;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_Y) KeyPicker6.SelectedIndex = 17;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_U) KeyPicker6.SelectedIndex = 18;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_I) KeyPicker6.SelectedIndex = 19;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_O) KeyPicker6.SelectedIndex = 20;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_P) KeyPicker6.SelectedIndex = 21;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_G) KeyPicker6.SelectedIndex = 22;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_H) KeyPicker6.SelectedIndex = 23;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_J) KeyPicker6.SelectedIndex = 24;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_K) KeyPicker6.SelectedIndex = 25;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_L) KeyPicker6.SelectedIndex = 26;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_Z) KeyPicker6.SelectedIndex = 27;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_X) KeyPicker6.SelectedIndex = 28;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_N) KeyPicker6.SelectedIndex = 29;
                if (Settings.Bunnyhop.StrafeEmulatorKey == Keyboard.VK_M) KeyPicker6.SelectedIndex = 30;
            }
            {
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F6) KeyPicker8.SelectedIndex = 0;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F7) KeyPicker8.SelectedIndex = 1;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F8) KeyPicker8.SelectedIndex = 2;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F9) KeyPicker8.SelectedIndex = 3;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F10) KeyPicker8.SelectedIndex = 4;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_F11) KeyPicker8.SelectedIndex = 5;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_INSERT) KeyPicker8.SelectedIndex = 6;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_DELETE) KeyPicker8.SelectedIndex = 7;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_HOME) KeyPicker8.SelectedIndex = 8;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_END) KeyPicker8.SelectedIndex = 9;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_PRIOR) KeyPicker8.SelectedIndex = 10;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NEXT) KeyPicker8.SelectedIndex = 11;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD1) KeyPicker8.SelectedIndex = 12;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD2) KeyPicker8.SelectedIndex = 13;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD3) KeyPicker8.SelectedIndex = 14;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD4) KeyPicker8.SelectedIndex = 15;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD5) KeyPicker8.SelectedIndex = 16;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD6) KeyPicker8.SelectedIndex = 17;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD7) KeyPicker8.SelectedIndex = 18;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD8) KeyPicker8.SelectedIndex = 19;
                if (Settings.OtherControls.ToggleGlow == Keyboard.VK_NUMPAD9) KeyPicker8.SelectedIndex = 20;
            }
            {
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F6) KeyPicker9.SelectedIndex = 0;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F7) KeyPicker9.SelectedIndex = 1;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F8) KeyPicker9.SelectedIndex = 2;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F9) KeyPicker9.SelectedIndex = 3;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F10) KeyPicker9.SelectedIndex = 4;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_F11) KeyPicker9.SelectedIndex = 5;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_INSERT) KeyPicker9.SelectedIndex = 6;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_DELETE) KeyPicker9.SelectedIndex = 7;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_HOME) KeyPicker9.SelectedIndex = 8;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_END) KeyPicker9.SelectedIndex = 9;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_PRIOR) KeyPicker9.SelectedIndex = 10;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NEXT) KeyPicker9.SelectedIndex = 11;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD1) KeyPicker9.SelectedIndex = 12;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD2) KeyPicker9.SelectedIndex = 13;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD3) KeyPicker9.SelectedIndex = 14;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD4) KeyPicker9.SelectedIndex = 15;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD5) KeyPicker9.SelectedIndex = 16;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD6) KeyPicker9.SelectedIndex = 17;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD7) KeyPicker9.SelectedIndex = 18;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD8) KeyPicker9.SelectedIndex = 19;
                if (Settings.OtherControls.ToggleRadar == Keyboard.VK_NUMPAD9) KeyPicker9.SelectedIndex = 20;
            }
            {
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F6) KeyPicker10.SelectedIndex = 0;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F7) KeyPicker10.SelectedIndex = 1;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F8) KeyPicker10.SelectedIndex = 2;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F9) KeyPicker10.SelectedIndex = 3;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F10) KeyPicker10.SelectedIndex = 4;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_F11) KeyPicker10.SelectedIndex = 5;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_INSERT) KeyPicker10.SelectedIndex = 6;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_DELETE) KeyPicker10.SelectedIndex = 7;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_HOME) KeyPicker10.SelectedIndex = 8;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_END) KeyPicker10.SelectedIndex = 9;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_PRIOR) KeyPicker10.SelectedIndex = 10;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NEXT) KeyPicker10.SelectedIndex = 11;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD1) KeyPicker10.SelectedIndex = 12;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD2) KeyPicker10.SelectedIndex = 13;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD3) KeyPicker10.SelectedIndex = 14;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD4) KeyPicker10.SelectedIndex = 15;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD5) KeyPicker10.SelectedIndex = 16;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD6) KeyPicker10.SelectedIndex = 17;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD7) KeyPicker10.SelectedIndex = 18;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD8) KeyPicker10.SelectedIndex = 19;
                if (Settings.OtherControls.ToggleAimbot == Keyboard.VK_NUMPAD9) KeyPicker10.SelectedIndex = 20;
            }
            {
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F6) KeyPicker11.SelectedIndex = 0;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F7) KeyPicker11.SelectedIndex = 1;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F8) KeyPicker11.SelectedIndex = 2;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F9) KeyPicker11.SelectedIndex = 3;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F10) KeyPicker11.SelectedIndex = 4;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_F11) KeyPicker11.SelectedIndex = 5;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_INSERT) KeyPicker11.SelectedIndex = 6;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_DELETE) KeyPicker11.SelectedIndex = 7;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_HOME) KeyPicker11.SelectedIndex = 8;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_END) KeyPicker11.SelectedIndex = 9;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_PRIOR) KeyPicker11.SelectedIndex = 10;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NEXT) KeyPicker11.SelectedIndex = 11;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD1) KeyPicker11.SelectedIndex = 12;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD2) KeyPicker11.SelectedIndex = 13;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD3) KeyPicker11.SelectedIndex = 14;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD4) KeyPicker11.SelectedIndex = 15;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD5) KeyPicker11.SelectedIndex = 16;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD6) KeyPicker11.SelectedIndex = 17;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD7) KeyPicker11.SelectedIndex = 18;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD8) KeyPicker11.SelectedIndex = 19;
                if (Settings.OtherControls.PanicKey == Keyboard.VK_NUMPAD9) KeyPicker11.SelectedIndex = 20;
            }
            {
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F6) KeyPicker12.SelectedIndex = 0;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F7) KeyPicker12.SelectedIndex = 1;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F8) KeyPicker12.SelectedIndex = 2;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F9) KeyPicker12.SelectedIndex = 3;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F10) KeyPicker12.SelectedIndex = 4;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_F11) KeyPicker12.SelectedIndex = 5;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_INSERT) KeyPicker12.SelectedIndex = 6;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_DELETE) KeyPicker12.SelectedIndex = 7;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_HOME) KeyPicker12.SelectedIndex = 8;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_END) KeyPicker12.SelectedIndex = 9;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_PRIOR) KeyPicker12.SelectedIndex = 10;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NEXT) KeyPicker12.SelectedIndex = 11;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD1) KeyPicker12.SelectedIndex = 12;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD2) KeyPicker12.SelectedIndex = 13;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD3) KeyPicker12.SelectedIndex = 14;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD4) KeyPicker12.SelectedIndex = 15;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD5) KeyPicker12.SelectedIndex = 16;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD6) KeyPicker12.SelectedIndex = 17;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD7) KeyPicker12.SelectedIndex = 18;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD8) KeyPicker12.SelectedIndex = 19;
                if (Settings.OtherControls.ToggleMenu == Keyboard.VK_NUMPAD9) KeyPicker12.SelectedIndex = 20;
            }

            if (Settings.Chams.Enabled) ChamsToggle1.BackgroundImage = Properties.Resources.switch5;
            else ChamsToggle1.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Chams.Allies) ChamsToggle2.BackgroundImage = Properties.Resources.switch5;
            else ChamsToggle2.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Chams.HealthBased) ChamsToggle3.BackgroundImage = Properties.Resources.switch5;
            else ChamsToggle3.BackgroundImage = Properties.Resources.switch6;

            ChamsTrackBar1.Value = Settings.Chams.Color_R;
            ChamsLabel10.Text = Settings.Chams.Color_R.ToString();

            ChamsTrackBar2.Value = Settings.Chams.Color_G;
            ChamsLabel11.Text = Settings.Chams.Color_G.ToString();

            ChamsTrackBar3.Value = Settings.Chams.Color_B;
            ChamsLabel12.Text = Settings.Chams.Color_B.ToString();

            ChamsTrackBar4.Value = Settings.Chams.Allies_Color_R;
            ChamsLabel13.Text = Settings.Chams.Allies_Color_R.ToString();

            ChamsTrackBar5.Value = Settings.Chams.Allies_Color_G;
            ChamsLabel14.Text = Settings.Chams.Allies_Color_G.ToString();

            ChamsTrackBar6.Value = Settings.Chams.Allies_Color_B;
            ChamsLabel15.Text = Settings.Chams.Allies_Color_B.ToString();

            if (Settings.Esp.Enabled) EspToggle1.BackgroundImage = Properties.Resources.switch5;
            else EspToggle1.BackgroundImage = Properties.Resources.switch6;

            if (Settings.Esp.Enabled) EspToggle1.BackgroundImage = Properties.Resources.switch5;
            else EspToggle1.BackgroundImage = Properties.Resources.switch6;

            EspTrackBar1.Value = Settings.Esp.Color_R; EspLabel8.Text = Settings.Esp.Color_R.ToString();
            EspTrackBar2.Value = Settings.Esp.Color_G; EspLabel9.Text = Settings.Esp.Color_G.ToString();
            EspTrackBar3.Value = Settings.Esp.Color_B; EspLabel10.Text = Settings.Esp.Color_B.ToString();
            EspTrackBar4.Value = Settings.Esp.VisableColor_R; EspLabel14.Text = Settings.Esp.VisableColor_R.ToString();
            EspTrackBar5.Value = Settings.Esp.VisableColor_G; EspLabel15.Text = Settings.Esp.VisableColor_G.ToString();
            EspTrackBar6.Value = Settings.Esp.VisableColor_B; EspLabel16.Text = Settings.Esp.VisableColor_B.ToString();
        }
        #endregion
        #region GlowToggles
        private void GlowToggle1_Click(object sender, EventArgs e)
        {
            Settings.Glow.Enabled = !Settings.Glow.Enabled;
            if (Settings.Glow.Enabled)
            {
                ThreadManager.StartThread("Glow");
                UI.ToggleAnimathionOn(GlowToggle1);
            }
            else
            {
                ThreadManager.PauseThread("Glow");
                UI.ToggleAnimathionOff(GlowToggle1);
            }
        }

        private void GlowToggle2_Click(object sender, EventArgs e)
        {
            Settings.Glow.bSpotted = !Settings.Glow.bSpotted;
            if (Settings.Glow.bSpotted) UI.ToggleAnimathionOn(GlowToggle2);
            else UI.ToggleAnimathionOff(GlowToggle2);
        }

        private void GlowToggle3_Click(object sender, EventArgs e)
        {
            Settings.Glow.Allies = !Settings.Glow.Allies;
            if (Settings.Glow.Allies) UI.ToggleAnimathionOn(GlowToggle3);
            else UI.ToggleAnimathionOff(GlowToggle3);
        }

        private void GlowToggle4_Click(object sender, EventArgs e)
        {
            Settings.Glow.ShowWeapons = !Settings.Glow.ShowWeapons;
            if (Settings.Glow.ShowWeapons) UI.ToggleAnimathionOn(GlowToggle4);
            else UI.ToggleAnimathionOff(GlowToggle4);
        }

        private void GlowToggle5_Click(object sender, EventArgs e)
        {
            Settings.Glow.FullBloom = !Settings.Glow.FullBloom;
            if (Settings.Glow.FullBloom) UI.ToggleAnimathionOn(GlowToggle5);
            else UI.ToggleAnimathionOff(GlowToggle5);
        }

        private void GlowToggle6_Click(object sender, EventArgs e)
        {
            Settings.Glow.HealthBased = !Settings.Glow.HealthBased;
            if (Settings.Glow.HealthBased) UI.ToggleAnimathionOn(GlowToggle6);
            else UI.ToggleAnimathionOff(GlowToggle6);
        }
        #endregion
        #region glow
        private void roundedPictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Point cursor = new Point();
                Imports.GetCursorPos(ref cursor);
                Bitmap bmp = new Bitmap(1, 1);
                Color GetColorAt(int x, int y)
                {
                    Rectangle bounds = new Rectangle(x, y, 1, 1);
                    using (Graphics g = Graphics.FromImage(bmp))
                        g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                    return bmp.GetPixel(0, 0);
                }
                var c = GetColorAt(cursor.X, cursor.Y);
                pictureBox3.BackColor = c;
            }
            catch { }
        }

        private void roundedPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void roundedPictureBox1_Click(object sender, EventArgs e)
        {
            Point cursor = new Point();
            Imports.GetCursorPos(ref cursor);
            Bitmap bmp = new Bitmap(1, 1);
            Color GetColorAt(int x, int y)
            {
                Rectangle bounds = new Rectangle(x, y, 1, 1);
                using (Graphics g = Graphics.FromImage(bmp))
                    g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                return bmp.GetPixel(0, 0);
            }
            var c = GetColorAt(cursor.X, cursor.Y);
            pictureBox3.BackColor = c;
            if (GlowIndex == 1)
            {
                GlowButton1.BackColor = c;
                Settings.Glow.Enemies_Color_G = c.G;
                Settings.Glow.Enemies_Color_B = c.B;
                Settings.Glow.Enemies_Color_R = c.R;
            }
            else if (GlowIndex == 2)
            {
                GlowButton2.BackColor = c;
                Settings.Glow.InvisibleEnemies_Color_G = c.G;
                Settings.Glow.InvisibleEnemies_Color_B = c.B;
                Settings.Glow.InvisibleEnemies_Color_R = c.R;
            }
            else if (GlowIndex == 3)
            {
                GlowButton3.BackColor = c;
                Settings.Glow.Snipers_Color_G = c.G;
                Settings.Glow.Snipers_Color_B = c.B;
                Settings.Glow.Snipers_Color_R = c.R;
            }
            else if (GlowIndex == 4)
            {
                GlowButton4.BackColor = c;
                Settings.Glow.Rifles_Color_G = c.G;
                Settings.Glow.Rifles_Color_B = c.B;
                Settings.Glow.Rifles_Color_R = c.R;
            }
            else if (GlowIndex == 5)
            {
                GlowButton5.BackColor = c;
                Settings.Glow.MPs_Color_G = c.G;
                Settings.Glow.MPs_Color_B = c.B;
                Settings.Glow.MPs_Color_R = c.R;
            }
            else if (GlowIndex == 6)
            {
                GlowButton6.BackColor = c;
                Settings.Glow.Allies_Color_G = c.G;
                Settings.Glow.Allies_Color_B = c.B;
                Settings.Glow.Allies_Color_R = c.R;
            }
            else if (GlowIndex == 7)
            {
                GlowButton7.BackColor = c;
                Settings.Glow.Pistols_Color_G = c.G;
                Settings.Glow.Pistols_Color_B = c.B;
                Settings.Glow.Pistols_Color_R = c.R;
            }
            else if (GlowIndex == 8)
            {
                GlowButton8.BackColor = c;
                Settings.Glow.Heavy_Color_G = c.G;
                Settings.Glow.Heavy_Color_B = c.B;
                Settings.Glow.Heavy_Color_R = c.R;
            }
            else if (GlowIndex == 9)
            {
                GlowButton9.BackColor = c;
                Settings.Glow.C4_Color_G = c.G;
                Settings.Glow.C4_Color_B = c.B;
                Settings.Glow.C4_Color_R = c.R;
            }
            else if (GlowIndex == 10)
            {
                GlowButton10.BackColor = c;
                Settings.Glow.Grenades_Color_G = c.G;
                Settings.Glow.Grenades_Color_B = c.B;
                Settings.Glow.Grenades_Color_R = c.R;
            }
        }
        #endregion
        #region Misc
        private void MiscToggle1_Click(object sender, EventArgs e)
        {
            Settings.Bunnyhop.Enabled = !Settings.Bunnyhop.Enabled;
            if (Settings.Bunnyhop.Enabled)
            {
                ThreadManager.StartThread("BunnyHop");
                UI.ToggleAnimathionOn(MiscToggle1);
            }
            else
            {
                ThreadManager.PauseThread("BunnyHop");
                UI.ToggleAnimathionOff(MiscToggle1);
            }
        }

        private void MiscToggle2_Click(object sender, EventArgs e)
        {
            Settings.Bunnyhop.AutoStrafe = !Settings.Bunnyhop.AutoStrafe;
            if (Settings.Bunnyhop.AutoStrafe)
            {
                UI.ToggleAnimathionOn(MiscToggle2);
            }
            else
            {
                UI.ToggleAnimathionOff(MiscToggle2);
            }
        }

        private void MiscToggle3_Click(object sender, EventArgs e)
        {
            Settings.AimAssist.Enabled = !Settings.AimAssist.Enabled;
            if (Settings.AimAssist.Enabled)
            {
                ThreadManager.StartThread("AimAssist");
                UI.ToggleAnimathionOn(MiscToggle3);
            }
            else
            {
                ThreadManager.PauseThread("AimAssist");
                UI.ToggleAnimathionOff(MiscToggle3);
            }
        }

        private void MiscToggle4_Click(object sender, EventArgs e)
        {
            Settings.Sonar.Enabled = !Settings.Sonar.Enabled;
            if (Settings.Sonar.Enabled)
            {
                ThreadManager.StartThread("Sonar");
                UI.ToggleAnimathionOn(MiscToggle4);
            }
            else
            {
                ThreadManager.PauseThread("Sonar");
                UI.ToggleAnimathionOff(MiscToggle4);
            }
        }

        private void MiscToggle5_Click(object sender, EventArgs e)
        {
            Settings.Trigger.Enabled = !Settings.Trigger.Enabled;
            if (Settings.Trigger.Enabled)
            {
                ThreadManager.StartThread("Trigger");
                UI.ToggleAnimathionOn(MiscToggle5);
            }
            else
            {
                ThreadManager.PauseThread("Trigger");
                UI.ToggleAnimathionOff(MiscToggle5);
            }
        }

        private void MiscToggle6_Click(object sender, EventArgs e)
        {
            Settings.Bunnyhop.StrafeEmulator = !Settings.Bunnyhop.StrafeEmulator;
            if (Settings.Bunnyhop.StrafeEmulator)
            {
                UI.ToggleAnimathionOn(MiscToggle6);
            }
            else
            {
                UI.ToggleAnimathionOff(MiscToggle6);
            }
        }


        private void MiscToggle8_Click(object sender, EventArgs e)
        {
            Settings.Radar.Enabled = !Settings.Radar.Enabled;
            if (Settings.Radar.Enabled)
            {
                ThreadManager.StartThread("Radar");
                UI.ToggleAnimathionOn(MiscToggle8);
            }
            else
            {
                ThreadManager.PauseThread("Radar");
                UI.ToggleAnimathionOff(MiscToggle8);
            }
        }
        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            Settings.Trigger.Delay = trackBar9.Value;
            MiscLabel8.Text = Settings.Trigger.Delay.ToString();
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            Settings.Trigger.DelayBetweenShots = trackBar10.Value;
            MiscLabel10.Text = Settings.Trigger.DelayBetweenShots.ToString();
        }

        private void trackBar12_Scroll(object sender, EventArgs e)
        {
            Settings.Bunnyhop.sens = trackBar12.Value;
            MiscLabel12.Text = Settings.Bunnyhop.sens.ToString();
        }

        private void trackBar11_Scroll(object sender, EventArgs e)
        {
            Settings.Bunnyhop.speed = trackBar11.Value;
            MiscLabel14.Text = Settings.Bunnyhop.speed.ToString();
        }

        #endregion
        #region Chams
        private void ChamsToggle1_Click(object sender, EventArgs e)
        {
            Settings.Chams.Enabled = !Settings.Chams.Enabled;
            if (Settings.Chams.Enabled == false)
            {
                Settings.Chams.Color_R = 255;
                Settings.Chams.Color_G = 255;
                Settings.Chams.Color_B = 255;
                Settings.Chams.Allies_Color_R = 255;
                Settings.Chams.Allies_Color_G = 255;
                Settings.Chams.Allies_Color_B = 255;
                UI.ToggleAnimathionOff(ChamsToggle1);
                Thread.Sleep(80);
                ThreadManager.PauseThread("Chams");
                Settings.Chams.Color_R = (byte)ChamsTrackBar1.Value;
                Settings.Chams.Color_G = (byte)ChamsTrackBar2.Value;
                Settings.Chams.Color_B = (byte)ChamsTrackBar3.Value;
                Settings.Chams.Allies_Color_R = (byte)ChamsTrackBar4.Value;
                Settings.Chams.Allies_Color_G = (byte)ChamsTrackBar5.Value;
                Settings.Chams.Allies_Color_B = (byte)ChamsTrackBar6.Value;

            }
            else
            {
                Settings.Chams.Color_R = (byte)ChamsTrackBar1.Value;
                Settings.Chams.Color_G = (byte)ChamsTrackBar2.Value;
                Settings.Chams.Color_B = (byte)ChamsTrackBar3.Value;
                Settings.Chams.Allies_Color_R = (byte)ChamsTrackBar4.Value;
                Settings.Chams.Allies_Color_G = (byte)ChamsTrackBar5.Value;
                Settings.Chams.Allies_Color_B = (byte)ChamsTrackBar6.Value;
                Thread.Sleep(10);
                ThreadManager.StartThread("Chams");
                UI.ToggleAnimathionOn(ChamsToggle1);
            }
        }

        private void ChamsToggle2_Click(object sender, EventArgs e)
        {
            Settings.Chams.Allies = !Settings.Chams.Allies;
            if (Settings.Chams.Allies == false)
            {
                Settings.Chams.Allies_Color_R = 255;
                Settings.Chams.Allies_Color_G = 255;
                Settings.Chams.Allies_Color_B = 255;
                Thread.Sleep(10);
                Settings.Chams.Allies_Color_R = (byte)ChamsTrackBar4.Value;
                Settings.Chams.Allies_Color_G = (byte)ChamsTrackBar5.Value;
                Settings.Chams.Allies_Color_B = (byte)ChamsTrackBar6.Value;
                UI.ToggleAnimathionOff(ChamsToggle2);
            }
            else
            {
                Settings.Chams.Allies_Color_R = (byte)ChamsTrackBar4.Value;
                Settings.Chams.Allies_Color_G = (byte)ChamsTrackBar5.Value;
                Settings.Chams.Allies_Color_B = (byte)ChamsTrackBar6.Value;
                Thread.Sleep(10);
                UI.ToggleAnimathionOn(ChamsToggle2);
            }
        }

        private void ChamsToggle3_Click(object sender, EventArgs e)
        {
            Settings.Chams.HealthBased = !Settings.Chams.HealthBased;
            if (Settings.Chams.HealthBased) UI.ToggleAnimathionOn(ChamsToggle3);
            else UI.ToggleAnimathionOff(ChamsToggle3);
        }

        private void ChamsTrackBar1_Scroll(object sender, EventArgs e)
        {
            Settings.Chams.Color_R = (byte)ChamsTrackBar1.Value;
            ChamsLabel10.Text = Settings.Chams.Color_R.ToString();
        }

        private void ChamsTrackBar2_Scroll(object sender, EventArgs e)
        {
            Settings.Chams.Color_G = (byte)ChamsTrackBar2.Value;
            ChamsLabel11.Text = Settings.Chams.Color_G.ToString();
        }

        private void ChamsTrackBar3_Scroll(object sender, EventArgs e)
        {
            Settings.Chams.Color_B = (byte)ChamsTrackBar3.Value;
            ChamsLabel12.Text = Settings.Chams.Color_B.ToString();
        }

        private void ChamsTrackBar4_Scroll(object sender, EventArgs e)
        {
            Settings.Chams.Allies_Color_R = (byte)ChamsTrackBar4.Value;
            ChamsLabel13.Text = Settings.Chams.Allies_Color_R.ToString();
        }

        private void ChamsTrackBar5_Scroll(object sender, EventArgs e)
        {
            Settings.Chams.Allies_Color_G = (byte)ChamsTrackBar5.Value;
            ChamsLabel14.Text = Settings.Chams.Allies_Color_G.ToString();
        }

        private void ChamsTrackBar6_Scroll(object sender, EventArgs e)
        {
            Settings.Chams.Allies_Color_B = (byte)ChamsTrackBar6.Value;
            ChamsLabel15.Text = Settings.Chams.Allies_Color_B.ToString();
        }
        #endregion
        #region keys
        private void KeyPicker1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker1.SelectedIndex == 0) Settings.Aimbot.Key = Keyboard.VK_LBUTTON;
            if (KeyPicker1.SelectedIndex == 1) Settings.Aimbot.Key = Keyboard.VK_RBUTTON;
            if (KeyPicker1.SelectedIndex == 2) Settings.Aimbot.Key = Keyboard.VK_MBUTTON;
            if (KeyPicker1.SelectedIndex == 3) Settings.Aimbot.Key = Keyboard.VK_XBUTTON1;
            if (KeyPicker1.SelectedIndex == 4) Settings.Aimbot.Key = Keyboard.VK_XBUTTON2;
            if (KeyPicker1.SelectedIndex == 5) Settings.Aimbot.Key = Keyboard.VK_MENU;
            if (KeyPicker1.SelectedIndex == 6) Settings.Aimbot.Key = Keyboard.VK_SHIFT;
            if (KeyPicker1.SelectedIndex == 7) Settings.Aimbot.Key = Keyboard.VK_CAPITAL;
            if (KeyPicker1.SelectedIndex == 8) Settings.Aimbot.Key = Keyboard.VK_V;
            if (KeyPicker1.SelectedIndex == 9) Settings.Aimbot.Key = Keyboard.VK_C;
            if (KeyPicker1.SelectedIndex == 10) Settings.Aimbot.Key = Keyboard.VK_B;
            if (KeyPicker1.SelectedIndex == 11) Settings.Aimbot.Key = Keyboard.VK_F;
            if (KeyPicker1.SelectedIndex == 12) Settings.Aimbot.Key = Keyboard.VK_E;
            if (KeyPicker1.SelectedIndex == 13) Settings.Aimbot.Key = Keyboard.VK_Q;
            if (KeyPicker1.SelectedIndex == 14) Settings.Aimbot.Key = Keyboard.VK_W;
            if (KeyPicker1.SelectedIndex == 15) Settings.Aimbot.Key = Keyboard.VK_R;
            if (KeyPicker1.SelectedIndex == 16) Settings.Aimbot.Key = Keyboard.VK_T;
            if (KeyPicker1.SelectedIndex == 17) Settings.Aimbot.Key = Keyboard.VK_Y;
            if (KeyPicker1.SelectedIndex == 18) Settings.Aimbot.Key = Keyboard.VK_U;
            if (KeyPicker1.SelectedIndex == 19) Settings.Aimbot.Key = Keyboard.VK_I;
            if (KeyPicker1.SelectedIndex == 20) Settings.Aimbot.Key = Keyboard.VK_O;
            if (KeyPicker1.SelectedIndex == 21) Settings.Aimbot.Key = Keyboard.VK_P;
            if (KeyPicker1.SelectedIndex == 22) Settings.Aimbot.Key = Keyboard.VK_G;
            if (KeyPicker1.SelectedIndex == 23) Settings.Aimbot.Key = Keyboard.VK_H;
            if (KeyPicker1.SelectedIndex == 24) Settings.Aimbot.Key = Keyboard.VK_J;
            if (KeyPicker1.SelectedIndex == 25) Settings.Aimbot.Key = Keyboard.VK_K;
            if (KeyPicker1.SelectedIndex == 26) Settings.Aimbot.Key = Keyboard.VK_L;
            if (KeyPicker1.SelectedIndex == 27) Settings.Aimbot.Key = Keyboard.VK_Z;
            if (KeyPicker1.SelectedIndex == 28) Settings.Aimbot.Key = Keyboard.VK_X;
            if (KeyPicker1.SelectedIndex == 29) Settings.Aimbot.Key = Keyboard.VK_N;
            if (KeyPicker1.SelectedIndex == 30) Settings.Aimbot.Key = Keyboard.VK_M;
            label3.Focus();
        }
        private void KeyPicker2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker2.SelectedIndex == 0) Settings.Aimbot.SecondKey = Keyboard.VK_LBUTTON;
            if (KeyPicker2.SelectedIndex == 1) Settings.Aimbot.SecondKey = Keyboard.VK_RBUTTON;
            if (KeyPicker2.SelectedIndex == 2) Settings.Aimbot.SecondKey = Keyboard.VK_MBUTTON;
            if (KeyPicker2.SelectedIndex == 3) Settings.Aimbot.SecondKey = Keyboard.VK_XBUTTON1;
            if (KeyPicker2.SelectedIndex == 4) Settings.Aimbot.SecondKey = Keyboard.VK_XBUTTON2;
            if (KeyPicker2.SelectedIndex == 5) Settings.Aimbot.SecondKey = Keyboard.VK_MENU;
            if (KeyPicker2.SelectedIndex == 6) Settings.Aimbot.SecondKey = Keyboard.VK_SHIFT;
            if (KeyPicker2.SelectedIndex == 7) Settings.Aimbot.SecondKey = Keyboard.VK_CAPITAL;
            if (KeyPicker2.SelectedIndex == 8) Settings.Aimbot.SecondKey = Keyboard.VK_V;
            if (KeyPicker2.SelectedIndex == 9) Settings.Aimbot.SecondKey = Keyboard.VK_C;
            if (KeyPicker2.SelectedIndex == 10) Settings.Aimbot.SecondKey = Keyboard.VK_B;
            if (KeyPicker2.SelectedIndex == 11) Settings.Aimbot.SecondKey = Keyboard.VK_F;
            if (KeyPicker2.SelectedIndex == 12) Settings.Aimbot.SecondKey = Keyboard.VK_E;
            if (KeyPicker2.SelectedIndex == 13) Settings.Aimbot.SecondKey = Keyboard.VK_Q;
            if (KeyPicker2.SelectedIndex == 14) Settings.Aimbot.SecondKey = Keyboard.VK_W;
            if (KeyPicker2.SelectedIndex == 15) Settings.Aimbot.SecondKey = Keyboard.VK_R;
            if (KeyPicker2.SelectedIndex == 16) Settings.Aimbot.SecondKey = Keyboard.VK_T;
            if (KeyPicker2.SelectedIndex == 17) Settings.Aimbot.SecondKey = Keyboard.VK_Y;
            if (KeyPicker2.SelectedIndex == 18) Settings.Aimbot.SecondKey = Keyboard.VK_U;
            if (KeyPicker2.SelectedIndex == 19) Settings.Aimbot.SecondKey = Keyboard.VK_I;
            if (KeyPicker2.SelectedIndex == 20) Settings.Aimbot.SecondKey = Keyboard.VK_O;
            if (KeyPicker2.SelectedIndex == 21) Settings.Aimbot.SecondKey = Keyboard.VK_P;
            if (KeyPicker2.SelectedIndex == 22) Settings.Aimbot.SecondKey = Keyboard.VK_G;
            if (KeyPicker2.SelectedIndex == 23) Settings.Aimbot.SecondKey = Keyboard.VK_H;
            if (KeyPicker2.SelectedIndex == 24) Settings.Aimbot.SecondKey = Keyboard.VK_J;
            if (KeyPicker2.SelectedIndex == 25) Settings.Aimbot.SecondKey = Keyboard.VK_K;
            if (KeyPicker2.SelectedIndex == 26) Settings.Aimbot.SecondKey = Keyboard.VK_L;
            if (KeyPicker2.SelectedIndex == 27) Settings.Aimbot.SecondKey = Keyboard.VK_Z;
            if (KeyPicker2.SelectedIndex == 28) Settings.Aimbot.SecondKey = Keyboard.VK_X;
            if (KeyPicker2.SelectedIndex == 29) Settings.Aimbot.SecondKey = Keyboard.VK_N;
            if (KeyPicker2.SelectedIndex == 30) Settings.Aimbot.SecondKey = Keyboard.VK_M;
            label3.Focus();
        }
        private void KeyPicker3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker3.SelectedIndex == 0) Settings.Sonar.Key = Keyboard.VK_TAB;
            if (KeyPicker3.SelectedIndex == 1) Settings.Sonar.Key = Keyboard.VK_RBUTTON;
            if (KeyPicker3.SelectedIndex == 2) Settings.Sonar.Key = Keyboard.VK_MBUTTON;
            if (KeyPicker3.SelectedIndex == 3) Settings.Sonar.Key = Keyboard.VK_XBUTTON1;
            if (KeyPicker3.SelectedIndex == 4) Settings.Sonar.Key = Keyboard.VK_XBUTTON2;
            if (KeyPicker3.SelectedIndex == 5) Settings.Sonar.Key = Keyboard.VK_MENU;
            if (KeyPicker3.SelectedIndex == 6) Settings.Sonar.Key = Keyboard.VK_SHIFT;
            if (KeyPicker3.SelectedIndex == 7) Settings.Sonar.Key = Keyboard.VK_CAPITAL;
            if (KeyPicker3.SelectedIndex == 8) Settings.Sonar.Key = Keyboard.VK_V;
            if (KeyPicker3.SelectedIndex == 9) Settings.Sonar.Key = Keyboard.VK_C;
            if (KeyPicker3.SelectedIndex == 10) Settings.Sonar.Key = Keyboard.VK_B;
            if (KeyPicker3.SelectedIndex == 11) Settings.Sonar.Key = Keyboard.VK_F;
            if (KeyPicker3.SelectedIndex == 12) Settings.Sonar.Key = Keyboard.VK_E;
            if (KeyPicker3.SelectedIndex == 13) Settings.Sonar.Key = Keyboard.VK_Q;
            if (KeyPicker3.SelectedIndex == 14) Settings.Sonar.Key = Keyboard.VK_W;
            if (KeyPicker3.SelectedIndex == 15) Settings.Sonar.Key = Keyboard.VK_R;
            if (KeyPicker3.SelectedIndex == 16) Settings.Sonar.Key = Keyboard.VK_T;
            if (KeyPicker3.SelectedIndex == 17) Settings.Sonar.Key = Keyboard.VK_Y;
            if (KeyPicker3.SelectedIndex == 18) Settings.Sonar.Key = Keyboard.VK_U;
            if (KeyPicker3.SelectedIndex == 19) Settings.Sonar.Key = Keyboard.VK_I;
            if (KeyPicker3.SelectedIndex == 20) Settings.Sonar.Key = Keyboard.VK_O;
            if (KeyPicker3.SelectedIndex == 21) Settings.Sonar.Key = Keyboard.VK_P;
            if (KeyPicker3.SelectedIndex == 22) Settings.Sonar.Key = Keyboard.VK_G;
            if (KeyPicker3.SelectedIndex == 23) Settings.Sonar.Key = Keyboard.VK_H;
            if (KeyPicker3.SelectedIndex == 24) Settings.Sonar.Key = Keyboard.VK_J;
            if (KeyPicker3.SelectedIndex == 25) Settings.Sonar.Key = Keyboard.VK_K;
            if (KeyPicker3.SelectedIndex == 26) Settings.Sonar.Key = Keyboard.VK_L;
            if (KeyPicker3.SelectedIndex == 27) Settings.Sonar.Key = Keyboard.VK_Z;
            if (KeyPicker3.SelectedIndex == 28) Settings.Sonar.Key = Keyboard.VK_X;
            if (KeyPicker3.SelectedIndex == 29) Settings.Sonar.Key = Keyboard.VK_N;
            if (KeyPicker3.SelectedIndex == 30) Settings.Sonar.Key = Keyboard.VK_M;
            label3.Focus();
        }
        private void KeyPicker4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker4.SelectedIndex == 0) Settings.AimAssist.Key = Keyboard.VK_TAB;
            if (KeyPicker4.SelectedIndex == 1) Settings.AimAssist.Key = Keyboard.VK_RBUTTON;
            if (KeyPicker4.SelectedIndex == 2) Settings.AimAssist.Key = Keyboard.VK_MBUTTON;
            if (KeyPicker4.SelectedIndex == 3) Settings.AimAssist.Key = Keyboard.VK_XBUTTON1;
            if (KeyPicker4.SelectedIndex == 4) Settings.AimAssist.Key = Keyboard.VK_XBUTTON2;
            if (KeyPicker4.SelectedIndex == 5) Settings.AimAssist.Key = Keyboard.VK_MENU;
            if (KeyPicker4.SelectedIndex == 6) Settings.AimAssist.Key = Keyboard.VK_SHIFT;
            if (KeyPicker4.SelectedIndex == 7) Settings.AimAssist.Key = Keyboard.VK_CAPITAL;
            if (KeyPicker4.SelectedIndex == 8) Settings.AimAssist.Key = Keyboard.VK_V;
            if (KeyPicker4.SelectedIndex == 9) Settings.AimAssist.Key = Keyboard.VK_C;
            if (KeyPicker4.SelectedIndex == 10) Settings.AimAssist.Key = Keyboard.VK_B;
            if (KeyPicker4.SelectedIndex == 11) Settings.AimAssist.Key = Keyboard.VK_F;
            if (KeyPicker4.SelectedIndex == 12) Settings.AimAssist.Key = Keyboard.VK_E;
            if (KeyPicker4.SelectedIndex == 13) Settings.AimAssist.Key = Keyboard.VK_Q;
            if (KeyPicker4.SelectedIndex == 14) Settings.AimAssist.Key = Keyboard.VK_W;
            if (KeyPicker4.SelectedIndex == 15) Settings.AimAssist.Key = Keyboard.VK_R;
            if (KeyPicker4.SelectedIndex == 16) Settings.AimAssist.Key = Keyboard.VK_T;
            if (KeyPicker4.SelectedIndex == 17) Settings.AimAssist.Key = Keyboard.VK_Y;
            if (KeyPicker4.SelectedIndex == 18) Settings.AimAssist.Key = Keyboard.VK_U;
            if (KeyPicker4.SelectedIndex == 19) Settings.AimAssist.Key = Keyboard.VK_I;
            if (KeyPicker4.SelectedIndex == 20) Settings.AimAssist.Key = Keyboard.VK_O;
            if (KeyPicker4.SelectedIndex == 21) Settings.AimAssist.Key = Keyboard.VK_P;
            if (KeyPicker4.SelectedIndex == 22) Settings.AimAssist.Key = Keyboard.VK_G;
            if (KeyPicker4.SelectedIndex == 23) Settings.AimAssist.Key = Keyboard.VK_H;
            if (KeyPicker4.SelectedIndex == 24) Settings.AimAssist.Key = Keyboard.VK_J;
            if (KeyPicker4.SelectedIndex == 25) Settings.AimAssist.Key = Keyboard.VK_K;
            if (KeyPicker4.SelectedIndex == 26) Settings.AimAssist.Key = Keyboard.VK_L;
            if (KeyPicker4.SelectedIndex == 27) Settings.AimAssist.Key = Keyboard.VK_Z;
            if (KeyPicker4.SelectedIndex == 28) Settings.AimAssist.Key = Keyboard.VK_X;
            if (KeyPicker4.SelectedIndex == 29) Settings.AimAssist.Key = Keyboard.VK_N;
            if (KeyPicker4.SelectedIndex == 30) Settings.AimAssist.Key = Keyboard.VK_M;
            label3.Focus();
        }
        private void KeyPicker5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker5.SelectedIndex == 0) Settings.Bunnyhop.Key = Keyboard.VK_SPACE;
            if (KeyPicker5.SelectedIndex == 1) Settings.Bunnyhop.Key = Keyboard.VK_RBUTTON;
            if (KeyPicker5.SelectedIndex == 2) Settings.Bunnyhop.Key = Keyboard.VK_MBUTTON;
            if (KeyPicker5.SelectedIndex == 3) Settings.Bunnyhop.Key = Keyboard.VK_XBUTTON1;
            if (KeyPicker5.SelectedIndex == 4) Settings.Bunnyhop.Key = Keyboard.VK_XBUTTON2;
            if (KeyPicker5.SelectedIndex == 5) Settings.Bunnyhop.Key = Keyboard.VK_MENU;
            if (KeyPicker5.SelectedIndex == 6) Settings.Bunnyhop.Key = Keyboard.VK_SHIFT;
            if (KeyPicker5.SelectedIndex == 7) Settings.Bunnyhop.Key = Keyboard.VK_CAPITAL;
            if (KeyPicker5.SelectedIndex == 8) Settings.Bunnyhop.Key = Keyboard.VK_V;
            if (KeyPicker5.SelectedIndex == 9) Settings.Bunnyhop.Key = Keyboard.VK_C;
            if (KeyPicker5.SelectedIndex == 10) Settings.Bunnyhop.Key = Keyboard.VK_B;
            if (KeyPicker5.SelectedIndex == 11) Settings.Bunnyhop.Key = Keyboard.VK_F;
            if (KeyPicker5.SelectedIndex == 12) Settings.Bunnyhop.Key = Keyboard.VK_E;
            if (KeyPicker5.SelectedIndex == 13) Settings.Bunnyhop.Key = Keyboard.VK_Q;
            if (KeyPicker5.SelectedIndex == 14) Settings.Bunnyhop.Key = Keyboard.VK_W;
            if (KeyPicker5.SelectedIndex == 15) Settings.Bunnyhop.Key = Keyboard.VK_R;
            if (KeyPicker5.SelectedIndex == 16) Settings.Bunnyhop.Key = Keyboard.VK_T;
            if (KeyPicker5.SelectedIndex == 17) Settings.Bunnyhop.Key = Keyboard.VK_Y;
            if (KeyPicker5.SelectedIndex == 18) Settings.Bunnyhop.Key = Keyboard.VK_U;
            if (KeyPicker5.SelectedIndex == 19) Settings.Bunnyhop.Key = Keyboard.VK_I;
            if (KeyPicker5.SelectedIndex == 20) Settings.Bunnyhop.Key = Keyboard.VK_O;
            if (KeyPicker5.SelectedIndex == 21) Settings.Bunnyhop.Key = Keyboard.VK_P;
            if (KeyPicker5.SelectedIndex == 22) Settings.Bunnyhop.Key = Keyboard.VK_G;
            if (KeyPicker5.SelectedIndex == 23) Settings.Bunnyhop.Key = Keyboard.VK_H;
            if (KeyPicker5.SelectedIndex == 24) Settings.Bunnyhop.Key = Keyboard.VK_J;
            if (KeyPicker5.SelectedIndex == 25) Settings.Bunnyhop.Key = Keyboard.VK_K;
            if (KeyPicker5.SelectedIndex == 26) Settings.Bunnyhop.Key = Keyboard.VK_L;
            if (KeyPicker5.SelectedIndex == 27) Settings.Bunnyhop.Key = Keyboard.VK_Z;
            if (KeyPicker5.SelectedIndex == 28) Settings.Bunnyhop.Key = Keyboard.VK_X;
            if (KeyPicker5.SelectedIndex == 29) Settings.Bunnyhop.Key = Keyboard.VK_N;
            if (KeyPicker5.SelectedIndex == 30) Settings.Bunnyhop.Key = Keyboard.VK_M;
            label3.Focus();
        }
        private void KeyPicker6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker6.SelectedIndex == 0) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_SPACE;
            if (KeyPicker6.SelectedIndex == 1) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_RBUTTON;
            if (KeyPicker6.SelectedIndex == 2) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_MBUTTON;
            if (KeyPicker6.SelectedIndex == 3) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_XBUTTON1;
            if (KeyPicker6.SelectedIndex == 4) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_XBUTTON2;
            if (KeyPicker6.SelectedIndex == 5) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_MENU;
            if (KeyPicker6.SelectedIndex == 6) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_SHIFT;
            if (KeyPicker6.SelectedIndex == 7) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_CAPITAL;
            if (KeyPicker6.SelectedIndex == 8) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_V;
            if (KeyPicker6.SelectedIndex == 9) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_C;
            if (KeyPicker6.SelectedIndex == 10) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_B;
            if (KeyPicker6.SelectedIndex == 11) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_F;
            if (KeyPicker6.SelectedIndex == 12) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_E;
            if (KeyPicker6.SelectedIndex == 13) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_Q;
            if (KeyPicker6.SelectedIndex == 14) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_W;
            if (KeyPicker6.SelectedIndex == 15) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_R;
            if (KeyPicker6.SelectedIndex == 16) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_T;
            if (KeyPicker6.SelectedIndex == 17) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_Y;
            if (KeyPicker6.SelectedIndex == 18) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_U;
            if (KeyPicker6.SelectedIndex == 19) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_I;
            if (KeyPicker6.SelectedIndex == 20) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_O;
            if (KeyPicker6.SelectedIndex == 21) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_P;
            if (KeyPicker6.SelectedIndex == 22) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_G;
            if (KeyPicker6.SelectedIndex == 23) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_H;
            if (KeyPicker6.SelectedIndex == 24) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_J;
            if (KeyPicker6.SelectedIndex == 25) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_K;
            if (KeyPicker6.SelectedIndex == 26) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_L;
            if (KeyPicker6.SelectedIndex == 27) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_Z;
            if (KeyPicker6.SelectedIndex == 28) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_X;
            if (KeyPicker6.SelectedIndex == 29) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_N;
            if (KeyPicker6.SelectedIndex == 30) Settings.Bunnyhop.StrafeEmulatorKey = Keyboard.VK_M;
            label3.Focus();
        }
        private void KeyPicker7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker7.SelectedIndex == 0) Settings.Trigger.Key = Keyboard.VK_LBUTTON;
            if (KeyPicker7.SelectedIndex == 1) Settings.Trigger.Key = Keyboard.VK_RBUTTON;
            if (KeyPicker7.SelectedIndex == 2) Settings.Trigger.Key = Keyboard.VK_MBUTTON;
            if (KeyPicker7.SelectedIndex == 3) Settings.Trigger.Key = Keyboard.VK_XBUTTON1;
            if (KeyPicker7.SelectedIndex == 4) Settings.Trigger.Key = Keyboard.VK_XBUTTON2;
            if (KeyPicker7.SelectedIndex == 5) Settings.Trigger.Key = Keyboard.VK_MENU;
            if (KeyPicker7.SelectedIndex == 6) Settings.Trigger.Key = Keyboard.VK_SHIFT;
            if (KeyPicker7.SelectedIndex == 7) Settings.Trigger.Key = Keyboard.VK_CAPITAL;
            if (KeyPicker7.SelectedIndex == 8) Settings.Trigger.Key = Keyboard.VK_V;
            if (KeyPicker7.SelectedIndex == 9) Settings.Trigger.Key = Keyboard.VK_C;
            if (KeyPicker7.SelectedIndex == 10) Settings.Trigger.Key = Keyboard.VK_B;
            if (KeyPicker7.SelectedIndex == 11) Settings.Trigger.Key = Keyboard.VK_F;
            if (KeyPicker7.SelectedIndex == 12) Settings.Trigger.Key = Keyboard.VK_E;
            if (KeyPicker7.SelectedIndex == 13) Settings.Trigger.Key = Keyboard.VK_Q;
            if (KeyPicker7.SelectedIndex == 14) Settings.Trigger.Key = Keyboard.VK_W;
            if (KeyPicker7.SelectedIndex == 15) Settings.Trigger.Key = Keyboard.VK_R;
            if (KeyPicker7.SelectedIndex == 16) Settings.Trigger.Key = Keyboard.VK_T;
            if (KeyPicker7.SelectedIndex == 17) Settings.Trigger.Key = Keyboard.VK_Y;
            if (KeyPicker7.SelectedIndex == 18) Settings.Trigger.Key = Keyboard.VK_U;
            if (KeyPicker7.SelectedIndex == 19) Settings.Trigger.Key = Keyboard.VK_I;
            if (KeyPicker7.SelectedIndex == 20) Settings.Trigger.Key = Keyboard.VK_O;
            if (KeyPicker7.SelectedIndex == 21) Settings.Trigger.Key = Keyboard.VK_P;
            if (KeyPicker7.SelectedIndex == 22) Settings.Trigger.Key = Keyboard.VK_G;
            if (KeyPicker7.SelectedIndex == 23) Settings.Trigger.Key = Keyboard.VK_H;
            if (KeyPicker7.SelectedIndex == 24) Settings.Trigger.Key = Keyboard.VK_J;
            if (KeyPicker7.SelectedIndex == 25) Settings.Trigger.Key = Keyboard.VK_K;
            if (KeyPicker7.SelectedIndex == 26) Settings.Trigger.Key = Keyboard.VK_L;
            if (KeyPicker7.SelectedIndex == 27) Settings.Trigger.Key = Keyboard.VK_Z;
            if (KeyPicker7.SelectedIndex == 28) Settings.Trigger.Key = Keyboard.VK_X;
            if (KeyPicker7.SelectedIndex == 29) Settings.Trigger.Key = Keyboard.VK_N;
            if (KeyPicker7.SelectedIndex == 30) Settings.Trigger.Key = Keyboard.VK_M;
            label3.Focus();
        }
        private void KeyPicker8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker8.SelectedIndex == 0) Settings.OtherControls.ToggleGlow = Keyboard.VK_F6;
            if (KeyPicker8.SelectedIndex == 1) Settings.OtherControls.ToggleGlow = Keyboard.VK_F7;
            if (KeyPicker8.SelectedIndex == 2) Settings.OtherControls.ToggleGlow = Keyboard.VK_F8;
            if (KeyPicker8.SelectedIndex == 3) Settings.OtherControls.ToggleGlow = Keyboard.VK_F9;
            if (KeyPicker8.SelectedIndex == 4) Settings.OtherControls.ToggleGlow = Keyboard.VK_F10;
            if (KeyPicker8.SelectedIndex == 5) Settings.OtherControls.ToggleGlow = Keyboard.VK_F11;
            if (KeyPicker8.SelectedIndex == 6) Settings.OtherControls.ToggleGlow = Keyboard.VK_INSERT;
            if (KeyPicker8.SelectedIndex == 7) Settings.OtherControls.ToggleGlow = Keyboard.VK_DELETE;
            if (KeyPicker8.SelectedIndex == 8) Settings.OtherControls.ToggleGlow = Keyboard.VK_HOME;
            if (KeyPicker8.SelectedIndex == 9) Settings.OtherControls.ToggleGlow = Keyboard.VK_END;
            if (KeyPicker8.SelectedIndex == 10) Settings.OtherControls.ToggleGlow = Keyboard.VK_PRIOR;
            if (KeyPicker8.SelectedIndex == 11) Settings.OtherControls.ToggleGlow = Keyboard.VK_NEXT;
            if (KeyPicker8.SelectedIndex == 12) Settings.OtherControls.ToggleGlow = Keyboard.VK_NUMPAD1;
            if (KeyPicker8.SelectedIndex == 13) Settings.OtherControls.ToggleGlow = Keyboard.VK_NUMPAD2;
            if (KeyPicker8.SelectedIndex == 14) Settings.OtherControls.ToggleGlow = Keyboard.VK_NUMPAD3;
            if (KeyPicker8.SelectedIndex == 15) Settings.OtherControls.ToggleGlow = Keyboard.VK_NUMPAD4;
            if (KeyPicker8.SelectedIndex == 16) Settings.OtherControls.ToggleGlow = Keyboard.VK_NUMPAD5;
            if (KeyPicker8.SelectedIndex == 17) Settings.OtherControls.ToggleGlow = Keyboard.VK_NUMPAD7;
            if (KeyPicker8.SelectedIndex == 18) Settings.OtherControls.ToggleGlow = Keyboard.VK_NUMPAD8;
            if (KeyPicker8.SelectedIndex == 19) Settings.OtherControls.ToggleGlow = Keyboard.VK_NUMPAD9;
            label3.Focus();
        }
        private void KeyPicker9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker9.SelectedIndex == 0) Settings.OtherControls.ToggleRadar = Keyboard.VK_F6;
            if (KeyPicker9.SelectedIndex == 1) Settings.OtherControls.ToggleRadar = Keyboard.VK_F7;
            if (KeyPicker9.SelectedIndex == 2) Settings.OtherControls.ToggleRadar = Keyboard.VK_F8;
            if (KeyPicker9.SelectedIndex == 3) Settings.OtherControls.ToggleRadar = Keyboard.VK_F9;
            if (KeyPicker9.SelectedIndex == 4) Settings.OtherControls.ToggleRadar = Keyboard.VK_F10;
            if (KeyPicker9.SelectedIndex == 5) Settings.OtherControls.ToggleRadar = Keyboard.VK_F11;
            if (KeyPicker9.SelectedIndex == 6) Settings.OtherControls.ToggleRadar = Keyboard.VK_INSERT;
            if (KeyPicker9.SelectedIndex == 7) Settings.OtherControls.ToggleRadar = Keyboard.VK_DELETE;
            if (KeyPicker9.SelectedIndex == 8) Settings.OtherControls.ToggleRadar = Keyboard.VK_HOME;
            if (KeyPicker9.SelectedIndex == 9) Settings.OtherControls.ToggleRadar = Keyboard.VK_END;
            if (KeyPicker9.SelectedIndex == 10) Settings.OtherControls.ToggleRadar = Keyboard.VK_PRIOR;
            if (KeyPicker9.SelectedIndex == 11) Settings.OtherControls.ToggleRadar = Keyboard.VK_NEXT;
            if (KeyPicker9.SelectedIndex == 12) Settings.OtherControls.ToggleRadar = Keyboard.VK_NUMPAD1;
            if (KeyPicker9.SelectedIndex == 13) Settings.OtherControls.ToggleRadar = Keyboard.VK_NUMPAD2;
            if (KeyPicker9.SelectedIndex == 14) Settings.OtherControls.ToggleRadar = Keyboard.VK_NUMPAD3;
            if (KeyPicker9.SelectedIndex == 15) Settings.OtherControls.ToggleRadar = Keyboard.VK_NUMPAD4;
            if (KeyPicker9.SelectedIndex == 16) Settings.OtherControls.ToggleRadar = Keyboard.VK_NUMPAD5;
            if (KeyPicker9.SelectedIndex == 17) Settings.OtherControls.ToggleRadar = Keyboard.VK_NUMPAD7;
            if (KeyPicker9.SelectedIndex == 18) Settings.OtherControls.ToggleRadar = Keyboard.VK_NUMPAD8;
            if (KeyPicker9.SelectedIndex == 19) Settings.OtherControls.ToggleRadar = Keyboard.VK_NUMPAD9;
            label3.Focus();
        }
        private void KeyPicker10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker10.SelectedIndex == 0) Settings.OtherControls.ToggleAimbot = Keyboard.VK_F6;
            if (KeyPicker10.SelectedIndex == 1) Settings.OtherControls.ToggleAimbot = Keyboard.VK_F7;
            if (KeyPicker10.SelectedIndex == 2) Settings.OtherControls.ToggleAimbot = Keyboard.VK_F8;
            if (KeyPicker10.SelectedIndex == 3) Settings.OtherControls.ToggleAimbot = Keyboard.VK_F9;
            if (KeyPicker10.SelectedIndex == 4) Settings.OtherControls.ToggleAimbot = Keyboard.VK_F10;
            if (KeyPicker10.SelectedIndex == 5) Settings.OtherControls.ToggleAimbot = Keyboard.VK_F11;
            if (KeyPicker10.SelectedIndex == 6) Settings.OtherControls.ToggleAimbot = Keyboard.VK_INSERT;
            if (KeyPicker10.SelectedIndex == 7) Settings.OtherControls.ToggleAimbot = Keyboard.VK_DELETE;
            if (KeyPicker10.SelectedIndex == 8) Settings.OtherControls.ToggleAimbot = Keyboard.VK_HOME;
            if (KeyPicker10.SelectedIndex == 9) Settings.OtherControls.ToggleAimbot = Keyboard.VK_END;
            if (KeyPicker10.SelectedIndex == 10) Settings.OtherControls.ToggleAimbot = Keyboard.VK_PRIOR;
            if (KeyPicker10.SelectedIndex == 11) Settings.OtherControls.ToggleAimbot = Keyboard.VK_NEXT;
            if (KeyPicker10.SelectedIndex == 12) Settings.OtherControls.ToggleAimbot = Keyboard.VK_NUMPAD1;
            if (KeyPicker10.SelectedIndex == 13) Settings.OtherControls.ToggleAimbot = Keyboard.VK_NUMPAD2;
            if (KeyPicker10.SelectedIndex == 14) Settings.OtherControls.ToggleAimbot = Keyboard.VK_NUMPAD3;
            if (KeyPicker10.SelectedIndex == 15) Settings.OtherControls.ToggleAimbot = Keyboard.VK_NUMPAD4;
            if (KeyPicker10.SelectedIndex == 16) Settings.OtherControls.ToggleAimbot = Keyboard.VK_NUMPAD5;
            if (KeyPicker10.SelectedIndex == 17) Settings.OtherControls.ToggleAimbot = Keyboard.VK_NUMPAD7;
            if (KeyPicker10.SelectedIndex == 18) Settings.OtherControls.ToggleAimbot = Keyboard.VK_NUMPAD8;
            if (KeyPicker10.SelectedIndex == 19) Settings.OtherControls.ToggleAimbot = Keyboard.VK_NUMPAD9;
            label3.Focus();
        }
        private void KeyPicker11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker11.SelectedIndex == 0) Settings.OtherControls.PanicKey = Keyboard.VK_F6;
            if (KeyPicker11.SelectedIndex == 1) Settings.OtherControls.PanicKey = Keyboard.VK_F7;
            if (KeyPicker11.SelectedIndex == 2) Settings.OtherControls.PanicKey = Keyboard.VK_F8;
            if (KeyPicker11.SelectedIndex == 3) Settings.OtherControls.PanicKey = Keyboard.VK_F9;
            if (KeyPicker11.SelectedIndex == 4) Settings.OtherControls.PanicKey = Keyboard.VK_F10;
            if (KeyPicker11.SelectedIndex == 5) Settings.OtherControls.PanicKey = Keyboard.VK_F11;
            if (KeyPicker11.SelectedIndex == 6) Settings.OtherControls.PanicKey = Keyboard.VK_INSERT;
            if (KeyPicker11.SelectedIndex == 7) Settings.OtherControls.PanicKey = Keyboard.VK_DELETE;
            if (KeyPicker11.SelectedIndex == 8) Settings.OtherControls.PanicKey = Keyboard.VK_HOME;
            if (KeyPicker11.SelectedIndex == 9) Settings.OtherControls.PanicKey = Keyboard.VK_END;
            if (KeyPicker11.SelectedIndex == 10) Settings.OtherControls.PanicKey = Keyboard.VK_PRIOR;
            if (KeyPicker11.SelectedIndex == 11) Settings.OtherControls.PanicKey = Keyboard.VK_NEXT;
            if (KeyPicker11.SelectedIndex == 12) Settings.OtherControls.PanicKey = Keyboard.VK_NUMPAD1;
            if (KeyPicker11.SelectedIndex == 13) Settings.OtherControls.PanicKey = Keyboard.VK_NUMPAD2;
            if (KeyPicker11.SelectedIndex == 14) Settings.OtherControls.PanicKey = Keyboard.VK_NUMPAD3;
            if (KeyPicker11.SelectedIndex == 15) Settings.OtherControls.PanicKey = Keyboard.VK_NUMPAD4;
            if (KeyPicker11.SelectedIndex == 16) Settings.OtherControls.PanicKey = Keyboard.VK_NUMPAD5;
            if (KeyPicker11.SelectedIndex == 17) Settings.OtherControls.PanicKey = Keyboard.VK_NUMPAD7;
            if (KeyPicker11.SelectedIndex == 18) Settings.OtherControls.PanicKey = Keyboard.VK_NUMPAD8;
            if (KeyPicker11.SelectedIndex == 19) Settings.OtherControls.PanicKey = Keyboard.VK_NUMPAD9;
            label3.Focus();
        }
        private void KeyPicker12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyPicker12.SelectedIndex == 0) Settings.OtherControls.ToggleMenu = Keyboard.VK_F6;
            if (KeyPicker12.SelectedIndex == 1) Settings.OtherControls.ToggleMenu = Keyboard.VK_F7;
            if (KeyPicker12.SelectedIndex == 2) Settings.OtherControls.ToggleMenu = Keyboard.VK_F8;
            if (KeyPicker12.SelectedIndex == 3) Settings.OtherControls.ToggleMenu = Keyboard.VK_F9;
            if (KeyPicker12.SelectedIndex == 4) Settings.OtherControls.ToggleMenu = Keyboard.VK_F10;
            if (KeyPicker12.SelectedIndex == 5) Settings.OtherControls.ToggleMenu = Keyboard.VK_F11;
            if (KeyPicker12.SelectedIndex == 6) Settings.OtherControls.ToggleMenu = Keyboard.VK_INSERT;
            if (KeyPicker12.SelectedIndex == 7) Settings.OtherControls.ToggleMenu = Keyboard.VK_DELETE;
            if (KeyPicker12.SelectedIndex == 8) Settings.OtherControls.ToggleMenu = Keyboard.VK_HOME;
            if (KeyPicker12.SelectedIndex == 9) Settings.OtherControls.ToggleMenu = Keyboard.VK_END;
            if (KeyPicker12.SelectedIndex == 10) Settings.OtherControls.ToggleMenu = Keyboard.VK_PRIOR;
            if (KeyPicker12.SelectedIndex == 11) Settings.OtherControls.ToggleMenu = Keyboard.VK_NEXT;
            if (KeyPicker12.SelectedIndex == 12) Settings.OtherControls.ToggleMenu = Keyboard.VK_NUMPAD1;
            if (KeyPicker12.SelectedIndex == 13) Settings.OtherControls.ToggleMenu = Keyboard.VK_NUMPAD2;
            if (KeyPicker12.SelectedIndex == 14) Settings.OtherControls.ToggleMenu = Keyboard.VK_NUMPAD3;
            if (KeyPicker12.SelectedIndex == 15) Settings.OtherControls.ToggleMenu = Keyboard.VK_NUMPAD4;
            if (KeyPicker12.SelectedIndex == 16) Settings.OtherControls.ToggleMenu = Keyboard.VK_NUMPAD5;
            if (KeyPicker12.SelectedIndex == 17) Settings.OtherControls.ToggleMenu = Keyboard.VK_NUMPAD7;
            if (KeyPicker12.SelectedIndex == 18) Settings.OtherControls.ToggleMenu = Keyboard.VK_NUMPAD8;
            if (KeyPicker12.SelectedIndex == 19) Settings.OtherControls.ToggleMenu = Keyboard.VK_NUMPAD9;
            label3.Focus();
        }
        #endregion
        #region buttonmoouseenter
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.ForeColor = Color.DarkRed;
        }
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Red;
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.DarkRed;
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Red;
        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.DarkRed;
        }
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Red;
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.DarkRed;
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Red;
        }
        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.ForeColor = Color.DarkRed;
        }
        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.ForeColor = Color.Red;
        }
        private void button7_MouseEnter(object sender, EventArgs e)
        {
            button7.ForeColor = Color.DarkRed;
        }
        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.ForeColor = Color.Red;
        }
        private void button8_MouseEnter(object sender, EventArgs e)
        {
            button8.ForeColor = Color.DarkRed;
        }
        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.ForeColor = Color.Red;
        }

        private void button13_MouseEnter(object sender, EventArgs e)
        {
            button13.ForeColor = Color.DarkRed;
        }

        private void button13_MouseLeave(object sender, EventArgs e)
        {
            button13.ForeColor = Color.Red;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.ForeColor = Color.DarkRed;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.ForeColor = Color.Red;
        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {
            button9.ForeColor = Color.DarkRed;
        }
        private void button9_MouseLeave(object sender, EventArgs e)
        {
            button9.ForeColor = Color.Red;
        }
        private void button10_MouseEnter(object sender, EventArgs e)
        {
            button10.ForeColor = Color.DarkRed;
        }
        private void button10_MouseLeave(object sender, EventArgs e)
        {
            button10.ForeColor = Color.Red;
        }

        private void button12_MouseEnter(object sender, EventArgs e)
        {
            button12.ForeColor = Color.DarkRed;
        }

        private void button12_MouseLeave(object sender, EventArgs e)
        {
            button12.ForeColor = Color.Red;
        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            button11.ForeColor = Color.DarkRed;
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.ForeColor = Color.Red;
        }
        private void button14_MouseEnter(object sender, EventArgs e)
        {
            button14.ForeColor = Color.DarkRed;
        }

        private void button14_MouseLeave(object sender, EventArgs e)
        {
            button14.ForeColor = Color.Red;
        }
        #endregion
        #region buttonclicks
        private void button11_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 0)
            {
                if (File.Exists("default.ini"))
                {
                    Config.Load("default.ini");
                    AimbotUpdate();
                    CFGUpdate();
                }
                else
                {
                    Config.Save("default.ini");
                }
            }
            if (comboBox4.SelectedIndex == 1)
            {
                if (File.Exists("legit.ini"))
                {
                    Config.Load("legit.ini");
                    AimbotUpdate();
                    CFGUpdate();
                }
                else
                {
                    Config.Save("legit.ini");
                }
            }
            if (comboBox4.SelectedIndex == 2)
            {
                if (File.Exists("semi.ini"))
                {
                    Config.Load("semi.ini");
                    AimbotUpdate();
                    CFGUpdate();
                }
                else
                {
                    Config.Save("semi.ini");
                }
            }
            if (comboBox4.SelectedIndex == 3)
            {
                if (File.Exists("repachino.ini"))
                {
                    Config.Load("repachino.ini");
                    AimbotUpdate();
                    CFGUpdate();
                }
                else
                {
                    Config.Save("repachino.ini");
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 0) Config.Save("default.ini");
            if (comboBox4.SelectedIndex == 1) Config.Save("legit.ini");
            if (comboBox4.SelectedIndex == 2) Config.Save("semi.ini");
            if (comboBox4.SelectedIndex == 3) Config.Save("repachino.ini");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            UI.MenuPointer(panel3, button4, this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            UI.MenuPointer(panel3, button1, this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            UI.MenuPointer(panel3, button2, this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
            UI.MenuPointer(panel3, button3, this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
            UI.MenuPointer(panel3, button5, this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(5);
            UI.MenuPointer(panel3, button6, this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(6);
            UI.MenuPointer(panel3, button7, this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(7);
            UI.MenuPointer(panel3, button8, this);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(8);
            UI.MenuPointer(panel3, button13, this);
            UpdateChat();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            /*   if (Watcher.Menu)
               {
                   foreach (Form f in Application.OpenForms)
                   {
                       f.Hide();
                       Imports.SetForegroundWindow(menu.GameProcess.MainWindowHandle);
                   }
                   Watcher.Menu = false;
               }*/
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
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
            Environment.Exit(0);
        }
#endregion
        #region dragable
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void PROFILE_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void AIMBOT_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void GLOW_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void MISC_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void ESP_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void RADAR_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void CHAMS_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void KEYS_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void CHAT_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Imports.ReleaseCapture();
                Imports.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
        #endregion

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        private void runBrowserThread(string url)
        {
            var th = new Thread(() => {
                var br = new WebBrowser();
                br.Navigate(url);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void SendMessage()
        {
            try
            {
                if (abletochat)
                {
                    if (textBox1.Text.Length > 1 && textBox1.Text.Length < 140)
                    {
                        var src = DateTime.Now;
                        var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
                        string hash = Settings.username + "-" + src.Minute.ToString();
                        runBrowserThread("http://91.235.129.157/chat.php" + "?nick=" + Settings.username + "&hash=" + MD5Hash(hash) + "&text=" + textBox1.Text);
                        richTextBox1.Text = $"{richTextBox1.Text}{Environment.NewLine}[{src.Day}.{src.Month}.{src.Year}] [{src.Hour}:{src.Minute}]  {Settings.username}::{Environment.NewLine}{textBox1.Text}";
                        textBox1.Clear();
                        abletochat = false;
                        timer2.Start();
                    }
                    else Console.Beep(100, 100);
                }
                else Console.Beep(100, 100);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void UpdateChat()
        {
            string chat = new System.Net.WebClient().DownloadString("http://91.235.129.157/chat.txt");
            if (chat != ChatText)
            {
                ChatText = chat;
                string[] result = chat.Split(new[] { '\r', '\n' });
                richTextBox1.Clear();
                foreach (string line in result)
                {
                    if (line.Length > 5)
                    {
                        char[] charSeparators = new char[] { ',' };
                        string[] date = line.Split(charSeparators);
                        richTextBox1.Text = $"{richTextBox1.Text}{Environment.NewLine}[{date[1]}] [{date[2]}]  {date[3]}::{Environment.NewLine}{date[4]}";
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            abletochat = true;
            timer2.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 8)
            {
                UpdateChat();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SendMessage();
        }


        private void EspToggle1_Click(object sender, EventArgs e)
        {
            Settings.Esp.Enabled = !Settings.Esp.Enabled;
            if (Settings.Esp.Enabled)
            {
                form = new Overlay();
                form.Show();
                UI.ToggleAnimathionOn(EspToggle1);
            }
            else
            {
                form.Hide();
                form.Close();
                form.Dispose();
                UI.ToggleAnimathionOff(EspToggle1);
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == 0) Settings.Esp.Health = 0;
            if (comboBox5.SelectedIndex == 1) Settings.Esp.Health = 1;
            if (comboBox5.SelectedIndex == 2) Settings.Esp.Health = 2;
            if (comboBox5.SelectedIndex == 3) Settings.Esp.Health = 3;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex == 0) Settings.Esp.Width = 1;
            if (comboBox6.SelectedIndex == 1) Settings.Esp.Width = 2;
            if (comboBox6.SelectedIndex == 2) Settings.Esp.Width = 3;
            if (comboBox6.SelectedIndex == 3) Settings.Esp.Width = 4;
        }

        private void EspTrackBar1_Scroll(object sender, EventArgs e)
        {
            Settings.Esp.Color_R = EspTrackBar1.Value;
            EspLabel8.Text = Settings.Esp.Color_R.ToString();
        }

        private void EspTrackBar2_Scroll(object sender, EventArgs e)
        {
            Settings.Esp.Color_G = EspTrackBar2.Value;
            EspLabel9.Text = Settings.Esp.Color_G.ToString();
        }

        private void EspTrackBar3_Scroll(object sender, EventArgs e)
        {
            Settings.Esp.Color_B = EspTrackBar3.Value;
            EspLabel10.Text = Settings.Esp.Color_B.ToString();
        }

        private void EspTrackBar4_Scroll(object sender, EventArgs e)
        {
            Settings.Esp.VisableColor_R = EspTrackBar4.Value;
            EspLabel14.Text = Settings.Esp.VisableColor_R.ToString();
        }

        private void EspTrackBar5_Scroll(object sender, EventArgs e)
        {
            Settings.Esp.VisableColor_G = EspTrackBar5.Value;
            EspLabel15.Text = Settings.Esp.VisableColor_G.ToString();
        }

        private void EspTrackBar6_Scroll(object sender, EventArgs e)
        {
            Settings.Esp.VisableColor_B = EspTrackBar6.Value;
            EspLabel16.Text = Settings.Esp.VisableColor_B.ToString();
        }

        private void EspToggle2_Click(object sender, EventArgs e)
        {
            Settings.Esp.bSpotted = !Settings.Esp.bSpotted;
            if (Settings.Esp.bSpotted)
            {

                UI.ToggleAnimathionOn(EspToggle2);
            }
            else
            {
                UI.ToggleAnimathionOff(EspToggle2);
            }
        }

        private void ProfileLabel11_Click(object sender, EventArgs e)
        {

        }
    }
}
