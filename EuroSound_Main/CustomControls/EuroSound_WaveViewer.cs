﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public class EuroSound_WaveViewer : UserControl
    {
        #region declarations
        public Color PenColor { get; set; }
        public float PenWidth { get; set; }
        public Bitmap currentWaveImage = null;
        public Bitmap CurrentWaveImage
        {
            get
            {
                return currentWaveImage;
            }
            set
            {
                currentWaveImage = value;
            }
        }

        public int RenderDelay = 10;
        public delegate void OnLineDrawHandler(Point point1, Point point2);
        public event OnLineDrawHandler OnLineDrawEvent;
        private Dictionary<Point, Point> ControlPoints = new Dictionary<Point, Point>();

        private System.ComponentModel.Container components = null;
        private RawSourceWaveStream waveStream;
        private int samplesPerPixel = 128;
        private long startPosition;
        private int bytesPerSample;
        private Thread RenderThread;
        #endregion

        public EuroSound_WaveViewer()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            //use double buffer to avoid flickering
            this.DoubleBuffered = true;

            this.PenColor = Color.DarkBlue;
            this.PenWidth = 1;
        }

        /// <summary>
        /// Resize to screen size
        /// </summary>
        public void InitControl()
        {
            if (waveStream == null) return;

            var samples = (int)(waveStream.Length / bytesPerSample);
            startPosition = 0;
            SamplesPerPixel = samples / this.Width;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InitControl();
        }

        /// <summary>
        /// sets the associated wavestream
        /// </summary>
        public RawSourceWaveStream WaveStream
        {
            get
            {
                return waveStream;
            }
            set
            {
                waveStream = value;
                if (waveStream != null)
                {
                    bytesPerSample = (waveStream.WaveFormat.BitsPerSample / 8) * waveStream.WaveFormat.Channels;
                }
                this.Invalidate();
            }
        }

        /// <summary>
        /// The zoom level, in samples per pixel
        /// </summary>
        public int SamplesPerPixel
        {
            get
            {
                return samplesPerPixel;
            }
            set
            {
                samplesPerPixel = Math.Max(1, value);

                this.Invalidate();
            }
        }

        /// <summary>
        /// Start position (currently in bytes)
        /// </summary>
        public long StartPosition
        {
            get
            {
                return startPosition;
            }
            set
            {
                startPosition = value;
            }
        }

        /// <summary>
        /// Draw grid lines
        /// </summary>
        /// <param name="gfx"></param>
        private void DrawImageGrid(Graphics gfx)
        {
            int numOfCells = 30; int cellSize = (int)(gfx.ClipBounds.Height / 4);

            for (int y = 0; y < numOfCells; ++y)
            {
                gfx.DrawLine(new Pen(Color.Gray, 1), 0, y * cellSize, numOfCells * cellSize, y * cellSize);
            }

            for (int x = 0; x < numOfCells; ++x)
            {
                gfx.DrawLine(new Pen(Color.Gray, 1), x * cellSize, 0, x * cellSize, numOfCells * cellSize);
            }
        }

        /// <summary>
        /// Draw lines on paint event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                //draw grid before drawing the wave
                DrawImageGrid(e.Graphics);

                if (waveStream != null)
                {
                    int bytesRead;
                    var waveData = new byte[samplesPerPixel * bytesPerSample];
                    waveStream.Position = startPosition + (e.ClipRectangle.Left * bytesPerSample * samplesPerPixel);
                    ControlPoints.Clear();// clear points 

                    using (var linePen = new Pen(PenColor, PenWidth))
                    {
                        for (var x = e.ClipRectangle.X; x < e.ClipRectangle.Right; x += 1)
                        {
                            short low = 0;
                            short high = 0;

                            bytesRead = waveStream.Read(waveData, 0, samplesPerPixel * bytesPerSample);

                            if (bytesRead == 0) { break; }

                            for (var n = 0; n < bytesRead; n += 2)
                            {
                                var sample = BitConverter.ToInt16(waveData, n);
                                if (sample < low) { low = sample; }
                                if (sample > high) { high = sample; }
                            }

                            //calculate min and max values for the current line
                            var lowPercent = ((((float)low) - short.MinValue) / ushort.MaxValue);
                            var highPercent = ((((float)high) - short.MinValue) / ushort.MaxValue);

                            var point1 = new Point(x, (int)(this.Height * lowPercent));
                            var point2 = new Point(x, (int)(this.Height * highPercent));

                            //if event is not hoocked in, then render wave instantly
                            if (OnLineDrawEvent == null)
                            {
                                e.Graphics.DrawLine(linePen, point1, point2);
                            }
                            else
                            {
                                //save points to be used in the rendering thread
                                ControlPoints.Add(point1, point2);
                            }
                        }
                    }

                    //abort current thead if sound has been reloaded
                    if (RenderThread != null && RenderThread.IsAlive)
                    {
                        RenderThread.Abort();
                    }

                    //start rendering thread
                    RenderThread = new Thread(TriggerDrawing)
                    {
                        IsBackground = true
                    };
                    RenderThread.Start();
                }
            }
            catch (Exception)
            {
            }

            base.OnPaint(e);
        }

        void TriggerDrawing()
        {
            //check if the event is attached
            if (OnLineDrawEvent != null)
            {
                foreach (var pointSet in ControlPoints)
                {
                    OnLineDrawEvent(pointSet.Key, pointSet.Value);//trigger event and pass the points to the UI
                    Thread.Sleep(RenderDelay); //set custom delay
                }
            }
        }

        //Clean resources
        public void CallExit()
        {
            if (RenderThread != null && RenderThread.IsAlive)
            {
                RenderThread.Abort();
            }
            OnLineDrawEvent = null;
            ControlPoints = null;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CustomWaveViewer
            // 
            this.BackColor = Color.Gray;
            this.Name = "CustomWaveViewer";
            this.Size = new Size(569, 183);
            this.ResumeLayout(false);

        }
        #endregion
    }
}
