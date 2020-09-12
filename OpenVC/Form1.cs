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
            using (var window = new Window("capture"))
            {
                Mat img = new Mat();
                Mat dst = new Mat();
                while (enable==1)
                {
                    capture.Read(img);
                    //if (int.Parse(textBox1.Text) == null) textBox1.Text = "0";
                    if (img.Empty()) break;
                        
                    Cv2.CvtColor(img, dst, ColorConversionCodes.BGR2GRAY);
                    Cv2.Resize(dst,dst,new OpenCvSharp.Size(255,255));
                    Cv2.AdaptiveThreshold(dst, dst, 255,AdaptiveThresholdTypes.MeanC,ThresholdTypes.Binary,11,int.Parse(textBox1.Text));
                        //dst2 = cv2.adaptiveThreshold(gray,255,cv2.ADAPTIVE_THRESH_MEAN_C, cv2.THRESH_BINARY,11,float(args[1]))
                    
                    int height = dst.Height;
                    int width = dst.Width;
                    unsafe
                    {
                        int kokuten = 0;
                        StringBuilder LK= new StringBuilder("");//Line Kisuu  ����C���̕�����
                        StringBuilder LG= new StringBuilder("");//Line Guusuu �������C���̕�����
                        
                        

                        byte* b = dst.DataPointer;
                        for (int i = 0; i < height; i++)
                        {
                            int oldx = 0;
                            int flug = 0;
                            for (int j = 0; j < width; j++)
                            {
                                //byte valueAt = b[0];
                               // b[0] = (byte)(b[0]/2);
                                b += 1;
                                if (b[0] == 0)
                                {
                                    flug += 1;
                                    if (flug == 1)
                                    {
                                        oldx = j;
                                    }
                                }
                                else
                                {
                                    if (flug != 0)
                                    {
                                        kokuten++;
                                        if (i % 2 == 0)
                                        {
                                            //snprintf(oneelement, sizeof(oneelement), "%d %d %d\n", oldx, x - 1, y);
                                            //strcat(LK, oneelement);
                                            LK.AppendFormat("{0} {1} {2}\n", oldx, j - 1, i);
                                        }
                                        else
                                        {
                                            //snprintf(oneelement, sizeof(oneelement), "%d %d %d\n", oldx, x - 1, y);
                                            //strcat(LG, oneelement);
                                            LG.AppendFormat("{0} {1} {2}\n", oldx, j - 1, i);
                                        }
                                        flug = 0;
                                    }

                                }


                            }
                            if (flug != 0)
                            {    //last pixel
                                kokuten++;
                                if (i % 2 == 0)
                                {
                                    //char oneelement[64] = "";
                                    //snprintf(oneelement, sizeof(oneelement), "%d %d %d\n", oldx, 255, y);
                                    //strcat(LK, oneelement);
                                    LK.AppendFormat("{0} {1} {2}\n", oldx, 255, i);
                                }
                                else
                                {
                                    //char oneelement[64] = "";
                                    //snprintf(oneelement, sizeof(oneelement), "%d %d %d\n", oldx, 255, y);
                                    //strcat(LG, oneelement);
                                    LG.AppendFormat("{0} {1} {2}\n", oldx,255, i);
                                }
                                flug = 0;
                            }
                        }

                        LK.Append(LG);
                        queue.Enqueue(LK);
                    }

                
                
                window.ShowImage(dst);
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
