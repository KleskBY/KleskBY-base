using System;
using System.Threading;
using System.Windows.Forms;
using System.Linq;



using Microsoft.Win32;

namespace Orion
{
    class UI
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 461808)
                return "4.7.2";
            if (releaseKey >= 461308)
                return "4.7.1";
            if (releaseKey >= 460798)
                return "4.7";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
                return "4.6.1";
            if (releaseKey >= 393295)
                return "4.6";
            if (releaseKey >= 379893)
                return "4.5.2";
            if (releaseKey >= 378675)
                return "4.5.1";
            if (releaseKey >= 378389)
                return "4.5";
            // This code should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }

        public static void MakeTransparent(Label label, PictureBox pictureBox, Form form)
        {
            var pos = form.PointToScreen(label.Location);
            pos = pictureBox.PointToClient(pos);
            label.Parent = pictureBox;
            label.Location = pos;
        }

        public static void MakePicTransparent(PictureBox pciture, PictureBox pictureBox, Form form)
        {
            var pos = form.PointToScreen(pciture.Location);
            pos = pictureBox.PointToClient(pos);
            pciture.Parent = pictureBox;
            pciture.Location = pos;
        }

        public static void MakeButtonTransparent(Button button, PictureBox pictureBox, Form form)
        {
            var pos = form.PointToScreen(button.Location);
            pos = pictureBox.PointToClient(pos);
            button.Parent = pictureBox;
            button.Location = pos;
        }
        /*    public static void MenuPointer(Panel panel, Button button, Form form)
            {
                int sooqa = button.Top - panel.Top;
                if(sooqa < 0)
                {
                    int syk = Math.Abs(sooqa);
                    int tickcount = syk / 12;

                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top - tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = button.Top;
                    button.Refresh();

                }
                else if (sooqa > 0)
                {
                    int tickcount = sooqa / 12;

                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = panel.Top + tickcount;
                    button.Refresh();
                    Thread.Sleep(5);
                    panel.Top = button.Top;
                    button.Refresh();
                }
            }
        */
        public static void MenuPointer(Panel panel, Button button, Form form)
        {
            panel.Height = button.Height;
            panel.Top = button.Top;
            button.Refresh();
        }
            public static void ToggleAnimathionOn(Button toggle)
        {
            toggle.BackgroundImage = Properties.Resources.switch5_18;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_17;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_16;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_15;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_14;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_13;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_12;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_11;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_10;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_9;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_8;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_7;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_6;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_5;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_4;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_3;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_2;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_1;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5;
            Thread.Sleep(10); toggle.Refresh();
        }

        public static void ToggleAnimathionOff(Button toggle)
        {
            toggle.BackgroundImage = Properties.Resources.switch5_1;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_2;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_3;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_4;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_5;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_6;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_7;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_8;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_9;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_10;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_11;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_12;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_13;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_14;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_15;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_16;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_17;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch5_18;
            Thread.Sleep(10); toggle.Refresh();
            toggle.BackgroundImage = Properties.Resources.switch6;
        }
    }
}
