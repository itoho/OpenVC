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
        Mat yu = Cv2.ImRead("正解画像/ゆ.png",ImreadModes.Grayscale);
        
        public Form1()
        {
            InitializeComponent();
            using (new Window("yu", WindowMode.AutoSize, yu)) ;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int camindex = comboBox2.SelectedIndex;
            var capture = new VideoCapture(camindex);
            int fps = 30;
            int sleepTime = (int)Math.Round((decimal)1000 / fps);
            using (var window = new Window("capture"));
            int imagewidth = 1280;
            int imageheight = 720;
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
                       

                        /*
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
                                
                                b[0] = (byte)(color[1]);
                                color += 3;
                                b += 1;

                            }

                        }
                            */
                        

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
                    //Cv2.AdaptiveThreshold(dst, threshold, 255,AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 11, int.Parse(textBox1.Text));
                    threshold = blurred.Threshold(int.Parse(textBox1.Text), 255, ThresholdTypes.Otsu);

                    var contours = new Mat[] { };
                    var hierarchy = new Mat();
                    threshold.FindContours(out contours, hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxNone);
                    OpenCvSharp.Point[][] edgesArray = threshold.FindContoursAsArray(RetrievalModes.List, ContourApproximationModes.ApproxNone);
                    foreach (OpenCvSharp.Point[] edges in edgesArray)
                    {
                        OpenCvSharp.Point[] normalizedEdges = Cv2.ApproxPolyDP(edges, 17, true);
                        Rect appRect = Cv2.BoundingRect(normalizedEdges);
                        
                        //OpenCvSharp.Point2f[] point2Fs = new OpenCvSharp.Point2f[] { new Point2f(normalizedEdges[0].X, normalizedEdges[0].Y), new Point2f(normalizedEdges[1].X, normalizedEdges[1].Y), new Point2f(normalizedEdges[2].X, normalizedEdges[2].Y), new Point2f(normalizedEdges[3].X, normalizedEdges[3].Y) };
                        if (normalizedEdges.Length == 4&&appRect.Size.Height*appRect.Size.Width<=dst.Width*dst.Height*0.98&& appRect.Size.Height * appRect.Size.Width >= dst.Width * dst.Height * 0.01)
                        {
                            OpenCvSharp.Point2f[] point2Fs = new OpenCvSharp.Point2f[] { new Point2f(normalizedEdges[0].X, normalizedEdges[0].Y), new Point2f(normalizedEdges[1].X, normalizedEdges[1].Y), new Point2f(normalizedEdges[2].X, normalizedEdges[2].Y), new Point2f(normalizedEdges[3].X, normalizedEdges[3].Y) };
                            Cv2.Line(img, normalizedEdges[0].X, normalizedEdges[0].Y, normalizedEdges[1].X, normalizedEdges[1].Y, new Scalar(255, 0, 0), 3);
                            Cv2.Line(img, normalizedEdges[1].X, normalizedEdges[1].Y, normalizedEdges[2].X, normalizedEdges[2].Y, new Scalar(255, 0, 0), 3);
                            Cv2.Line(img, normalizedEdges[2].X, normalizedEdges[2].Y, normalizedEdges[3].X, normalizedEdges[3].Y, new Scalar(255, 0, 0), 3);
                            Cv2.Line(img, normalizedEdges[3].X, normalizedEdges[3].Y, normalizedEdges[0].X, normalizedEdges[0].Y, new Scalar(255, 0, 0), 3);
                            Mat homo =Cv2.GetPerspectiveTransform(point2Fs , new OpenCvSharp.Point2f[] { new Point2f(266,266), new Point2f(266, 0), new Point2f(0, 0), new Point2f(0, 266) } );//180x274の1割り増しをしてふちに備える
                            Mat huda = new Mat(266, 266, MatType.CV_32F); //もともと180x274の予定だった
                            Cv2.WarpPerspective(dst,huda,homo, new OpenCvSharp.Size(266,266));//スキャナーで読みっとった時のサイズによってこの値に決まった。ふち(5x5)を取るためにさらに加えた
                            Mat huda_hutidori = huda.Clone(new Rect(5, 5, 256, 256));
                            Cv2.AdaptiveThreshold(huda_hutidori,huda_hutidori, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 11, 4);
                            
                            int min_x = huda_hutidori.Width-1;
                            int min_y = huda_hutidori.Height - 1 ;
                            int max_x = 1;
                            int max_y = 1;
                            /*
                            unsafe
                            {
                                byte* b = huda_hutidori.DataPointer;
                                
                                // 余白の切り抜き
                                 
                                int height = huda_hutidori.Height;
                                int width = huda_hutidori.Width;
                                for (int i = 0; i < height; i++)
                                {
                                    for (int j = 0; j <width; j++)
                                    {
                                        if (b[0] == 0&&j<254&&j>2)
                                        {
                                            if (j < min_x) min_x = j;
                                            if (j > max_x) max_x = j;
                                            
                                        }
                                        b += 1;

                                    }
                                    if (b[0] == 0)
                                    if (i < min_y) min_y = i;
                                    if (i > max_y) max_y = i;

                                }
                            }
                        */

                            
                            
                                Mat trimmed_huda = huda_hutidori.Clone(new Rect(24, 18, 47, 37));
                                //Console.WriteLine(min_x + "," + min_y + "," + (max_x - min_x) + "," + (max_y - min_y));
                                Mat bitwise = new Mat();
                            Cv2.BitwiseXor(trimmed_huda, yu, bitwise);
                                using (new Window("trimmed_huda", WindowMode.AutoSize, bitwise)) ;
                            
                            
                            using (new Window("huda_hutidori", WindowMode.AutoSize, huda_hutidori)) ;


                        }
                        
                        //boundRect.Add(appRect);
                    }
                    /*
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
                    */
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
