using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCam
{
    public partial class Form1 : Form
    {
        //https://www.youtube.com/watch?v=caTo2cPxnzg

        public string Path = @"F:\Ejercicios estudio\C Sharp\WebCam\WebCam\WebCam";
        private bool HayDispositivos;
        private FilterInfoCollection MisDispositivos;
        private VideoCaptureDevice MiWebCam;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargaDispositivos();

        }

        public void CargaDispositivos()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if(MisDispositivos.Count>0)
            {
                HayDispositivos = true;
                for (int i = 0; i < MisDispositivos.Count; i++)
                    comboBox1.Items.Add(MisDispositivos[i].Name.ToString());
                comboBox1.Text = MisDispositivos[0].Name.ToString();

            }
            else
            {
                HayDispositivos = false;
            }

        }

        private void CerrarWebCam()
        {
            if(MiWebCam!=null&& MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
        }
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            CerrarWebCam();
            int i = comboBox1.SelectedIndex;
            string NombreVideo = MisDispositivos[i].MonikerString;
            MiWebCam = new VideoCaptureDevice(NombreVideo);
            MiWebCam.NewFrame += new NewFrameEventHandler(Capturando);
            MiWebCam.Start();

        }

        private void Capturando(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Image = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = Image;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarWebCam();
        }
    }
}
