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
            using (var window = new Window("capture"));
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
                       

                        
                        byte* b = dst.DataPointer;
                        byte* color = img.DataPointer;
                        
                        for (int i = 0; i < imageheight; i++)
                        {
                            for (int j = 0; j < imagewidth; j++)
                            {
                                /*緑との差
                                //byte valueAt = b[0];
                                // b[0] = (byte)(b[0]/2);
                                if ((color[1] - color[0]/2 - color[2]/2) <= 0){
                                    b[0] = 0;
                                }else if(((color[1] - color[0]/2  - color[2]/2 ) ) >= 256)
                                {
                                    b[0] = 255;
                                }
                                else {
                                    b[0] = (byte)((color[1] - color[0]/2 - color[2]/2));
                                }
                                //st = st.Replace(",", " ");

                                color += 3;
                                b += 1;
                                */
                                b[0] = (byte)(color[1]);
                                color += 3;
                                b += 1;

                            }

                        }
                        

                        //Cv2.AdaptiveThreshold(dst, dst, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 11, int.Parse(textBox1.Text));


                    }
                    /**
                    using (CascadeClassifier cascade = new CascadeClassifier("G:/downloads/haarcascade_fullbody.xml"))
                    {
                        foreach (Rect rectFace in cascade.DetectMultiScale(dst))
                        {
                            // 見つかった場所に赤枠を表示
                            Rect rect = new Rect(rectFace.X, rectFace.Y, rectFace.Width, rectFace.Height);
                            Cv2.Rectangle(img, rect, new OpenCvSharp.Scalar(0, 0, 255), 2);
                        }
                    }
                    **/

                    Mat blurred = new Mat();
                    blurred = dst.GaussianBlur(new OpenCvSharp.Size(5, 5), 0);
                    
                    //Binarization of the image.             
                    Mat threshold = new Mat();
                    Cv2.AdaptiveThreshold(dst, threshold, 255,AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 11, int.Parse(textBox1.Text));
                    threshold = blurred.Threshold(20, 255, ThresholdTypes.Otsu);

                    var contours = new Mat[] { };
                    var hierarchy = new Mat();
                    threshold.FindContours(out contours, hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxNone);
                    OpenCvSharp.Point[][] edgesArray = threshold.Clone().FindContoursAsArray(RetrievalModes.List, ContourApproximationModes.ApproxNone);
                    foreach (OpenCvSharp.Point[] edges in edgesArray)
                    {
                        OpenCvSharp.Point[] normalizedEdges = Cv2.ApproxPolyDP(edges, 17, true);
                        Rect appRect = Cv2.BoundingRect(normalizedEdges);
                        Cv2.Rectangle(img, appRect, new Scalar(255, 0, 0), 3);
                        //boundRect.Add(appRect);
                    }

                    for (int i = 0; i <contours.Length;i++)
                    {
                        //Console.WriteLine(contours[i]);
                        Mat inner_contour = contours[i];

                        //Console.WriteLine(inner_contour.Rows);//nannkakukeika
                        if (inner_contour.ContourArea() >= 100)
                        {
                            int epsilon = (int)(0.1 * Cv2.ArcLength(inner_contour, true));
                            //object approx = Cv2.ApproxPolyDP       (inner_contour, epsilon, true);
                            Console.WriteLine(inner_contour.ContourArea());
                            Cv2.DrawContours(img, contours, i, new Scalar(255, 0, 255));
                        }
                        //Console.WriteLine(contours[i]["Height"]);
                        //Cv2.Circle(img, new Cv2Point(contours[i].Value.X, contours[i].Value.Y), 5, new CvScalar(0, 0, 255));
                        //Cv2.Rectangle(img, new Rect(contours[i][0], 50, 100, 100), new Scalar(255, 0, 0), 2);

                    }
                    //Cv2.DrawContours(img, contours, -1,new Scalar(255,0,0));
                    //using (new Window("src", WindowMode.AutoSize, img))
                    using (new Window("dst", WindowMode.AutoSize, dst))
                    using (new Window("threshold", WindowMode.AutoSize, threshold))



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

        private string GetShape(OpenCvSharp.Point[] c)
        {
            string shape = "unidentified";
            double peri = Cv2.ArcLength(c, true);
            OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(c, 0.04 * peri, true);


            if (approx.Length == 3) //if the shape is a triangle, it will have 3 vertices
            {
                shape = "triangle";
            }
            else if (approx.Length == 4)    //if the shape has 4 vertices, it is either a square or a rectangle
            {
                Rect rect;
                rect = Cv2.BoundingRect(approx);
                double ar = rect.Width / (double)rect.Height;

                if (ar >= 0.95 && ar <= 1.05) shape = "square";
                else shape = "rectangle";
            }
            else if (approx.Length == 5)    //if the shape has 5 vertice, it is a pantagon
            {
                shape = "pentagon";
            }
            else   //otherwise, shape is a circle
            {
                shape = "circle";
            }
            return shape;
        }

    }
}
