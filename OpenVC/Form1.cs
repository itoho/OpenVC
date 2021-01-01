using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenVC
{
    public partial class Form1 : Form
    {
        public int enable = 1;
        public int sikiiti = 20;
        Queue<StringBuilder> queue = new Queue<StringBuilder>();
        public Form1()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            int camindex = comboBox2.SelectedIndex;
            var capture = new VideoCapture(camindex);
            int fps = 30;
            int sleepTime = (int)Math.Round((decimal)1000 / fps);
            using (var window = new Window("capture")) ;
            int imagewidth = 640;
            int imageheight = 360;
            int isparse;
            if (int.TryParse(widthbox.Text, out isparse))
            {
                imagewidth = int.Parse(widthbox.Text);
            }
            if (int.TryParse(heightbox.Text, out isparse))
            {
                imageheight = int.Parse(heightbox.Text);
            }
            using (var window = new Window("capture"))
            {
                Mat img = new Mat();
                Mat dst = new Mat();
                while (enable == 1)
                {
                   
                    capture.Read(img);
                    //if (int.Parse(textBox1.Text) == null) textBox1.Text = "0";
                    if (img.Empty()) break;

                    //Cv2.CvtColor(img, dst, ColorConversionCodes.BGR2GRAY);
                    //Cv2.Resize(dst,dst,new OpenCvSharp.Size(255,255));
                    //Cv2.AdaptiveThreshold(dst, dst, 255,AdaptiveThresholdTypes.MeanC,ThresholdTypes.Binary,11,int.Parse(textBox1.Text));
                    //dst2 = cv2.adaptiveThreshold(gray,255,cv2.ADAPTIVE_THRESH_MEAN_C, cv2.THRESH_BINARY,11,float(args[1]))
                    Cv2.Resize(img, img, new OpenCvSharp.Size(imagewidth, imageheight));
                    Cv2.CvtColor(img, dst, ColorConversionCodes.RGB2GRAY);
                   
                    unsafe
                    {
                        int kokuten = 0;
                        StringBuilder LK = new StringBuilder();//Line Kisuu  ����C���̕�����
                        StringBuilder LG = new StringBuilder("dummy\n");//Line Guusuu �������C���̕�����



                        byte* b = dst.DataPointer;
                        for (int i = 0; i < imageheight; i++)
                        {
                            for (int j = 0; j < imagewidth; j++)
                            {
                                //byte valueAt = b[0];
                                // b[0] = (byte)(b[0]/2);
                               
                                //st = st.Replace(",", " ");
                               

                                b += 3;

                            }

                        }


                        
                    }

                    using (CascadeClassifier cascade = new CascadeClassifier("G:/downloads/haarcascade_fullbody.xml"))
                    {
                        foreach (Rect rectFace in cascade.DetectMultiScale(dst))
                        {
                            // 見つかった場所に赤枠を表示
                            Rect rect = new Rect(rectFace.X, rectFace.Y, rectFace.Width, rectFace.Height);
                            Cv2.Rectangle(img, rect, new OpenCvSharp.Scalar(0, 0, 255), 2);
                        }
                    }


                    window.ShowImage(img);
                   
                    Cv2.WaitKey(sleepTime);
                }
                Cv2.DestroyWindow("capture");
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (enable == 1)
            {
                enable = 0;
                this.Text = "開始";
            }
            else
            {
                enable = 1;
                this.Text = "停止";
            }
        }
    }
}
