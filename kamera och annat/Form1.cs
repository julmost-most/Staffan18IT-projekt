using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;


namespace kamera_och_annat
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        Mat frame;
        Bitmap image;
        private Thread camera;
        bool isCameraRunning = false;
        private object chxDisableButton;
        private object btnbutton1_Click;

        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallback));
            camera.Start();
        }

        private void CaptureCameraCallback()
        {

            frame = new Mat();
            capture = new VideoCapture(0);
            capture.Open(0);

            if (capture.IsOpened())
            {
                while (isCameraRunning)
                {

                    capture.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    pictureBox1.Image = image;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Klicka inte här"))
            {
                CaptureCamera();
                button1.Text = "Du kan verkligen inte läsa";
                isCameraRunning = true;
            }
            else
            {
                capture.Release();
                button1.Text = "Klicka inte här";
                isCameraRunning = false;
            }
        }
        
        private void chxDisableButton_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                if (isCameraRunning)
                {
                    
                    Bitmap snapshot = new Bitmap(pictureBox1.Image);

                   
                    snapshot.Save(string.Format(@"C:\Users\sdkca\Desktop\{0}.png", Guid.NewGuid()), ImageFormat.Png);
                }
                else
                {
                    Console.WriteLine("Cannot take picture if the camera isn't capturing image!");
                }
            }
        }
    }
    
}
