using System;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using Orion.Features;
using Orion.Managers;
using Orion.Other;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Orion
{
        public partial class Overlay : Form
        {
        public static System.Numerics.Vector2 ScreenWH;
        public static Rectangle pRect;
        public static Graphics g;
        public static Pen Health = new Pen(Color.Green, 4);
        public static Font bigFont = new Font("Choktoff", 16);
        public static Brush mybrush = new SolidBrush(Color.White);
        public static Brush debugbrush = new SolidBrush(Color.FromArgb(0,0,0,0));

        private IntPtr handle;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetClientRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

        [DllImport("user32.dll")]
        private static extern IntPtr SetActiveWindow(IntPtr handle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        #region useless
        public static Rectangle GetScreen(Control referenceControl)
        {
            return Screen.FromControl(referenceControl).Bounds;
        }

        public static Color FromHealth(float percent)
        {
            if (percent < 0 || percent > 1) return Color.Black;

            int red, green;

            red = percent < 0.5 ? 255 : 255 - (int)(255 * (percent - 0.5) / 0.5);
            green = percent < 0.5 ? (int)(255 * percent) : 255;

            return Color.FromArgb(255, red, green, 0);
        }

        public Overlay()
        {
            this.handle = Handle;
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
            Imports.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, 0x0001 | 0x0002);
            OnResize(null);

            InitializeComponent();

            ScreenWH = new System.Numerics.Vector2(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }

        // Remember change the values of the form in the designer.
        private void Overlay_Load(object sender, EventArgs e)
        {
        /*    this.DoubleBuffered = true; // reduce the flicker
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |// reduce the flicker too
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.Opaque |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);*/
            this.TopMost = true;
            this.Visible = true;
            timer1.Start();

            Activate();
        }

      /* protected override void OnPaint(PaintEventArgs e)
        {
            int[] marg = new int[] { 0, 0, Width, Height };
            DwmExtendFrameIntoClientArea(this.Handle, ref marg);
        }*/

        public const UInt32 SWP_NOSIZE = 0x0001;
        public const UInt32 SWP_NOMOVE = 0x0002;
        public const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        public static IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_EX_TOPMOST = 0x00000008;
        private const int WM_ACTIVATE = 6;
        private const int WA_INACTIVE = 0;
        private const int WM_MOUSEACTIVATE = 0x0021;
        private const int MA_NOACTIVATEANDEAT = 0x0004;




        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams pm = base.CreateParams;
                pm.ExStyle |= 0x80;
                pm.ExStyle |= WS_EX_TOPMOST; // make the form topmost
                pm.ExStyle |= WS_EX_NOACTIVATE; // prevent the form from being activated
                return pm;
            }
        }

        /// <summary>
        /// Makes the form unable to gain focus at all time, 
        /// which should prevent lose focus
        /// </summary>
        /// <summary>
        /// Makes the form unable to gain focus at all time, 
        /// which should prevent lose focus
        /// </summary>
        /*    protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_MOUSEACTIVATE)
                {
                    m.Result = (IntPtr)MA_NOACTIVATEANDEAT;
                    return;
                }
                if (m.Msg == WM_ACTIVATE)
                {
                    if (((int)m.WParam & 0xFFFF) != WA_INACTIVE)
                        if (m.LParam != IntPtr.Zero)
                            SetActiveWindow(m.LParam);
                        else
                            SetActiveWindow(IntPtr.Zero);
                }
                else
                {
                    base.WndProc(ref m);
                }
            }*/
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTTRANSPARENT = -1;
            if (m.Msg == WM_NCHITTEST) m.Result = new IntPtr(HTTRANSPARENT);
            else base.WndProc(ref m);
        }




        #endregion

        public static System.Numerics.Vector2 WorldToScreen(System.Numerics.Vector3 TargetEntity, float[] ViewMatrix, System.Numerics.Vector2 ScreenHeightWidth)
        {
            System.Numerics.Vector2 Out;
            Out.X = TargetEntity.X * ViewMatrix[0] + TargetEntity.Y * ViewMatrix[1] + TargetEntity.Z * ViewMatrix[2] + ViewMatrix[3];
            Out.Y = TargetEntity.X * ViewMatrix[4] + TargetEntity.Y * ViewMatrix[5] + TargetEntity.Z * ViewMatrix[6] + ViewMatrix[7];
            float w = TargetEntity.X * ViewMatrix[12] + TargetEntity.Y * ViewMatrix[13] + TargetEntity.Z * ViewMatrix[14] + ViewMatrix[15];

            if (w < 0.01f)
                return new System.Numerics.Vector2(1, 1);
            else

            Out.X /= w;
            Out.Y /= w;

            Out.X *= ScreenHeightWidth.X / 2.0f;
            Out.X += ScreenHeightWidth.X / 2.0f;

            Out.Y *= -ScreenHeightWidth.Y / 2.0f;
            Out.Y += ScreenHeightWidth.Y / 2.0f;

            return new System.Numerics.Vector2(Out.X, Out.Y);
        }


        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            if (Imports.IsWindowFocues(Orion.menu.GameProcess))
            {
              //  GetClientRect(Orion.menu.GameProcess.Handle,ref pRect);
                g = e.Graphics;
              //  g.DrawString($"{pRect.X} {pRect.Y} {pRect.Width} {pRect.Height}", bigFont, mybrush, 50, 50);
                DrawPlayers();
             //   g.FillRectangle(debugbrush, new Rectangle((int)ScreenWH.X/2 - 5, (int)ScreenWH.Y/2 +18, 12, 12));
            }
        }

        private void DrawPlayers()
        {
            int maxPlayers = Structs.ClientState.MaxPlayers;
            byte[] entities = MemoryManager.ReadMemory((int)Structs.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);

            for (int i = 0; i < maxPlayers; i++)
            {
                int cEntity = Orion.Other.Math.GetInt(entities, i * 0x10);

                Structs.Enemy_t entityStruct = MemoryManager.ReadMemory<Structs.Enemy_t>(cEntity);

                if (!entityStruct.Team.HasTeam()
                    || entityStruct.Team.IsMyTeam()
                    || !entityStruct.Health.IsAlive())
                    continue;


                System.Numerics.Vector3 headPosition = Orion.Other.Math.GetBonePos(cEntity, 8);
                System.Numerics.Vector3 toePosition = Orion.Other.Math.GetBonePos(cEntity, 1);

                if (headPosition == System.Numerics.Vector3.Zero && toePosition == System.Numerics.Vector3.Zero) continue;

                float[] MyViewMatrix = MemoryManager.ReadMatrix<float>((int)Structs.Base.Client + Offsets.dwViewMatrix, 256);

                System.Numerics.Vector2 OnScreenPos = WorldToScreen(headPosition, MyViewMatrix, ScreenWH);
                if (OnScreenPos.X == 1 && OnScreenPos.Y == 1) continue;
                System.Numerics.Vector2 OnScreenToe = WorldToScreen(toePosition, MyViewMatrix, ScreenWH);

                int Size = (int)(OnScreenToe.Y - OnScreenPos.Y);
                int Width = (int)((headPosition - toePosition).Z / 2);
                int w = (int)(Size * 1.3f);
                int x;
                int y;


                 x = (int)(OnScreenPos.X - Size / 20);
                 y = (int)(OnScreenPos.Y + Width / 2f);


                int Dist = (int)Orion.Other.Math.GetPlayerDistance(Structs.LocalPlayer.Position, headPosition);

                Rectangle box = new Rectangle(x - Width / 2, y - 5, Width, (int)(Size * 1.2f));

                Color color = Color.FromArgb(255, Settings.Esp.Color_R, Settings.Esp.Color_G, Settings.Esp.Color_B);
                Color VisableColor = Color.FromArgb(255, Settings.Esp.VisableColor_R, Settings.Esp.VisableColor_G, Settings.Esp.VisableColor_B);

                Pen pen = new Pen(color, Settings.Esp.Width);
                Pen VisablePen = new Pen(VisableColor, Settings.Esp.Width);



                if (Settings.Esp.bSpotted)
                {
                    int EnemySpoted = MemoryManager.ReadMemory<int>(cEntity + Offsets.m_bSpottedByMask);
                    if (EnemySpoted == 0 || EnemySpoted % 2 == 0) g.DrawRectangle(pen, box);
                    else g.DrawRectangle(VisablePen, box);
                }
                else g.DrawRectangle(pen, box);



                if (Settings.Esp.Health == 1)
                {
                    Health = new Pen(FromHealth(entityStruct.Health / 100f), 4);
                    if (entityStruct.Health >= 90) g.DrawLine(Health, x - Width / 2 - 4, y - 4, x - Width / 2 - 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 80 && entityStruct.Health < 90) g.DrawLine(Health, x - Width / 2 - 4, y - 4 + (int)(Size * 0.2f), x - Width / 2 - 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 70 && entityStruct.Health < 80) g.DrawLine(Health, x - Width / 2 - 4, y - 4 + (int)(Size * 0.3f), x - Width / 2 - 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 60 && entityStruct.Health < 70) g.DrawLine(Health, x - Width / 2 - 4, y - 4 + (int)(Size * 0.4f), x - Width / 2 - 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 50 && entityStruct.Health < 60) g.DrawLine(Health, x - Width / 2 - 4, y - 4 + (int)(Size * 0.5f), x - Width / 2 - 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 40 && entityStruct.Health < 50) g.DrawLine(Health, x - Width / 2 - 4, y - 4 + (int)(Size * 0.6f), x - Width / 2 - 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 30 && entityStruct.Health < 40) g.DrawLine(Health, x - Width / 2 - 4, y - 4 + (int)(Size * 0.7f), x - Width / 2 - 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 20 && entityStruct.Health < 30) g.DrawLine(Health, x - Width / 2 - 4, y - 4 + (int)(Size * 0.8f), x - Width / 2 - 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 10 && entityStruct.Health < 20) g.DrawLine(Health, x - Width / 2 - 4, y - 4 + (int)(Size * 0.9f), x - Width / 2 - 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health < 10) g.DrawLine(Health, x - Width / 2 - 4, y - 4 + (int)(Size * 0.95f), x - Width / 2 - 4, y + (int)(Size * 1.15f));
                }
                if (Settings.Esp.Health == 2)
                {
                    Health = new Pen(FromHealth(entityStruct.Health / 100f), 4);
                    if (entityStruct.Health >= 90) g.DrawLine(Health, x + Width / 2 + 4, y - 4, x + Width / 2 + 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 80 && entityStruct.Health < 90) g.DrawLine(Health, x + Width / 2 + 4, y - 4 + (int)(Size * 0.2f), x + Width / 2 + 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 70 && entityStruct.Health < 80) g.DrawLine(Health, x + Width / 2 + 4, y - 4 + (int)(Size * 0.3f), x + Width / 2 + 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 60 && entityStruct.Health < 70) g.DrawLine(Health, x + Width / 2 + 4, y - 4 + (int)(Size * 0.4f), x + Width / 2 + 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 50 && entityStruct.Health < 60) g.DrawLine(Health, x + Width / 2 + 4, y - 4 + (int)(Size * 0.5f), x + Width / 2 + 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 40 && entityStruct.Health < 50) g.DrawLine(Health, x + Width / 2 + 4, y - 4 + (int)(Size * 0.6f), x + Width / 2 + 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 30 && entityStruct.Health < 40) g.DrawLine(Health, x + Width / 2 + 4, y - 4 + (int)(Size * 0.7f), x + Width / 2 + 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 20 && entityStruct.Health < 30) g.DrawLine(Health, x + Width / 2 + 4, y - 4 + (int)(Size * 0.8f), x + Width / 2 + 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health >= 10 && entityStruct.Health < 20) g.DrawLine(Health, x + Width / 2 + 4, y - 4 + (int)(Size * 0.9f), x + Width / 2 + 4, y + (int)(Size * 1.15f));
                    if (entityStruct.Health < 10) g.DrawLine(Health, x + Width / 2 + 4, y - 4 + (int)(Size * 0.95f), x + Width / 2 + 4, y + (int)(Size * 1.15f));
                }
                if (Settings.Esp.Health == 3)
                {
                    Brush HealthBrush = new SolidBrush(FromHealth(entityStruct.Health / 100f));
                    g.DrawString(entityStruct.Health.ToString(), bigFont, HealthBrush, x-18, y + (int)(Size * 1.3f));
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}

