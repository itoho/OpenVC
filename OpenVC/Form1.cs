using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenVC
{
    public partial class Form1 : Form
    {
        public int enable = 1;
        public int sikiiti = 20;
        Queue<StringBuilder> queue = new Queue<StringBuilder>();
        Mat  a = Cv2.ImRead("正解画像/あ.png", ImreadModes.Grayscale);
        Mat  i = Cv2.ImRead("正解画像/い.png", ImreadModes.Grayscale);
        Mat  u = Cv2.ImRead("正解画像/う.png", ImreadModes.Grayscale);
        Mat  e = Cv2.ImRead("正解画像/え.png", ImreadModes.Grayscale);
        Mat  o = Cv2.ImRead("正解画像/お.png", ImreadModes.Grayscale);
        Mat ka = Cv2.ImRead("正解画像/か.png", ImreadModes.Grayscale);
        Mat ki = Cv2.ImRead("正解画像/き.png", ImreadModes.Grayscale);
        Mat ku = Cv2.ImRead("正解画像/く.png", ImreadModes.Grayscale);
        Mat ke = Cv2.ImRead("正解画像/け.png", ImreadModes.Grayscale);
        Mat ko = Cv2.ImRead("正解画像/こ.png", ImreadModes.Grayscale);
        Mat sa = Cv2.ImRead("正解画像/さ.png", ImreadModes.Grayscale);
        Mat si = Cv2.ImRead("正解画像/し.png", ImreadModes.Grayscale);
        Mat su = Cv2.ImRead("正解画像/す.png", ImreadModes.Grayscale);
        Mat se = Cv2.ImRead("正解画像/せ.png", ImreadModes.Grayscale);
        Mat so = Cv2.ImRead("正解画像/そ.png", ImreadModes.Grayscale);
        Mat ta = Cv2.ImRead("正解画像/た.png", ImreadModes.Grayscale);
        Mat ti = Cv2.ImRead("正解画像/ち.png", ImreadModes.Grayscale);
        Mat tu = Cv2.ImRead("正解画像/つ.png", ImreadModes.Grayscale);
        Mat te = Cv2.ImRead("正解画像/て.png", ImreadModes.Grayscale);
        Mat to = Cv2.ImRead("正解画像/と.png", ImreadModes.Grayscale);
        Mat na = Cv2.ImRead("正解画像/な.png", ImreadModes.Grayscale);
        Mat ni = Cv2.ImRead("正解画像/に.png", ImreadModes.Grayscale);
        Mat nu = Cv2.ImRead("正解画像/ぬ.png", ImreadModes.Grayscale);
        Mat ne = Cv2.ImRead("正解画像/ね.png", ImreadModes.Grayscale);
        Mat no = Cv2.ImRead("正解画像/の.png", ImreadModes.Grayscale);
        Mat ha = Cv2.ImRead("正解画像/は.png", ImreadModes.Grayscale);
        Mat hi = Cv2.ImRead("正解画像/ひ.png", ImreadModes.Grayscale);
        Mat hu = Cv2.ImRead("正解画像/ふ.png", ImreadModes.Grayscale);
        Mat he = Cv2.ImRead("正解画像/へ.png", ImreadModes.Grayscale);
        Mat ho = Cv2.ImRead("正解画像/ほ.png", ImreadModes.Grayscale);
        Mat ma = Cv2.ImRead("正解画像/ま.png", ImreadModes.Grayscale);
        Mat mi = Cv2.ImRead("正解画像/み.png", ImreadModes.Grayscale);
        Mat mu = Cv2.ImRead("正解画像/む.png", ImreadModes.Grayscale);
        Mat me = Cv2.ImRead("正解画像/め.png", ImreadModes.Grayscale);
        Mat mo = Cv2.ImRead("正解画像/も.png", ImreadModes.Grayscale);
        Mat ya = Cv2.ImRead("正解画像/や.png", ImreadModes.Grayscale);
        Mat yu = Cv2.ImRead("正解画像/ゆ.png", ImreadModes.Grayscale);
        Mat yo = Cv2.ImRead("正解画像/よ.png", ImreadModes.Grayscale);
        Mat ra = Cv2.ImRead("正解画像/ら.png", ImreadModes.Grayscale);
        Mat ri = Cv2.ImRead("正解画像/り.png", ImreadModes.Grayscale);
        Mat ru = Cv2.ImRead("正解画像/る.png", ImreadModes.Grayscale);
        Mat re = Cv2.ImRead("正解画像/れ.png", ImreadModes.Grayscale);
        Mat ro = Cv2.ImRead("正解画像/ろ.png", ImreadModes.Grayscale);
        Mat wa = Cv2.ImRead("正解画像/わ.png", ImreadModes.Grayscale);
        Mat wo = Cv2.ImRead("正解画像/を.png", ImreadModes.Grayscale);
        Mat wi = Cv2.ImRead("正解画像/ゐ.png", ImreadModes.Grayscale);
        Mat we = Cv2.ImRead("正解画像/ゑ.png", ImreadModes.Grayscale);

        List<Mat> kanas = new List<Mat>(47);

        string hiragana = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをゐゑ";



        string[,] hyaku = new string[,]{{"あきのたのかりほのいほのとまをあらみ","わがころもではつゆにぬれつつ"},
{"はるすぎてなつきにけらししろたへの","ころもほすてふあまのかぐやま"},
{"あしびきのやまどりのをのしだりをの","ながながしよをひとりかもねむ"},
{"たごのうらにうちいでてみればしろたへの","ふじのたかねにゆきはふりつつ"},
{"おくやまにもみぢふみわけなくしかの","こゑきくときぞあきはかなしき"},
{"かささぎのわたせるはしにおくしもの","しろきをみればよぞふけにける"},
{"あまのはらふりさけみればかすがなる","みかさのやまにいでしつきかも"},
{"わがいほはみやこのたつみしかぞすむ","よをうぢやまとひとはいふなり"},
{"はなのいろはうつりにけりないたづらに","わがみよにふるながめせしまに"},
{"これやこのゆくもかへるもわかれては","しるもしらぬもあふさかのせき"},
{"わたのはらやそしまかけてこぎいでぬと","ひとにはつげよあまのつりぶね"},
{"あまつかぜくものかよひぢふきとぢよ","をとめのすがたしばしとどめむ"},
{"つくばねのみねよりおつるみなのがは","こひぞつもりてふちとなりぬる"},
{"みちのくのしのぶもぢずりたれゆゑに","みだれそめにしわれならなくに"},
{"きみがためはるののにいでてわかなつむ","わがころもでにゆきはふりつつ"},
{"たちわかれいなばのやまのみねにおふる","まつとしきかばいまかへりこむ"},
{"ちはやぶるかみよもきかずたつたがは","からくれなゐにみづくくるとは"},
{"すみのえのきしによるなみよるさへや","ゆめのかよひぢひとめよくらむ"},
{"なにはがたみじかきあしのふしのまも","あはでこのよをすぐしてよとや"},
{"わびぬればいまはたおなじなにはなる","みをつくしてもあはむとぞおもふ"},
{"いまこむといひしばかりにながつきの","ありあけのつきをまちいでつるかな"},
{"ふくからにあきのくさきのしをるれば","むべやまかぜをあらしといふらむ"},
{"つきみればちぢにものこそかなしけれ","わがみひとつのあきにはあらねど"},
{"このたびはぬさもとりあへずたむけやま","もみぢのにしきかみのまにまに"},
{"なにしおはばあふさかやまのさねかづら","ひとにしられでくるよしもがな"},
{"をぐらやまみねのもみぢばこころあらば","いまひとたびのみゆきまたなむ"},
{"みかのはらわきてながるるいづみがは","いつみきとてかこひしかるらむ"},
{"やまざとはふゆぞさびしさまさりける","ひとめもくさもかれぬとおもへば"},
{"こころあてにをらばやをらむはつしもの","おきまどはせるしらぎくのはな"},
{"ありあけのつれなくみえしわかれより","あかつきばかりうきものはなし"},
{"あさぼらけありあけのつきとみるまでに","よしののさとにふれるしらゆき"},
{"やまがはにかぜのかけたるしがらみは","ながれもあへぬもみぢなりけり"},
{"ひさかたのひかりのどけきはるのひに","しづごころなくはなのちるらむ"},
{"たれをかもしるひとにせむたかさごの","まつもむかしのともならなくに"},
{"ひとはいさこころもしらずふるさとは","はなぞむかしのかににほひける"},
{"なつのよはまだよひながらあけぬるを","くものいづこにつきやどるらむ"},
{"しらつゆにかぜのふきしくあきののは","つらぬきとめぬたまぞちりける"},
{"わすらるるみをばおもはずちかひてし","ひとのいのちのをしくもあるかな"},
{"あさぢふのをののしのはらしのぶれど","あまりてなどかひとのこひしき"},
{"しのぶれどいろにいでにけりわがこひは","ものやおもふとひとのとふまで"},
{"こひすてふわがなはまだきたちにけり","ひとしれずこそおもひそめしか"},
{"ちぎりきなかたみにそでをしぼりつつ","すゑのまつやまなみこさじとは"},
{"あひみてののちのこころにくらぶれば","むかしはものをおもはざりけり"},
{"あふことのたえてしなくばなかなかに","ひとをもみをもうらみざらまし"},
{"あはれともいふべきひとはおもほえで","みのいたづらになりぬべきかな"},
{"ゆらのとをわたるふなびとかぢをたえ","ゆくへもしらぬこひのみちかな"},
{"やへむぐらしげれるやどのさびしきに","ひとこそみえねあきはきにけり"},
{"かぜをいたみいはうつなみのおのれのみ","くだけてものをおもふころかな"},
{"みかきもりゑじのたくひのよるはもえ","ひるはきえつつものをこそおもへ"},
{"きみがためをしからざりしいのちさへ","ながくもがなとおもひけるかな"},
{"かくとだにえやはいぶきのさしもぐさ","さしもしらじなもゆるおもひを"},
{"あけぬればくるるものとはしりながら","なほうらめしきあさぼらけかな"},
{"なげきつつひとりぬるよのあくるまは","いかにひさしきものとかはしる"},
{"わすれじのゆくすゑまではかたければ","けふをかぎりのいのちともがな"},
{"たきのおとはたえてひさしくなりぬれど","なこそながれてなほきこえけれ"},
{"あらざらむこのよのほかのおもひでに","いまひとたびのあふこともがな"},
{"めぐりあひてみしやそれともわかぬまに","くもがくれにしよはのつきかな"},
{"ありまやまゐなのささはらかぜふけば","いでそよひとをわすれやはする"},
{"やすらはでねなましものをさよふけて","かたぶくまでのつきをみしかな"},
{"おほえやまいくののみちのとほければ","まだふみもみずあまのはしだて"},
{"いにしへのならのみやこのやへざくら","けふここのへににほひぬるかな"},
{"よをこめてとりのそらねははかるとも","よにあふさかのせきはゆるさじ"},
{"いまはただおもひたえなむとばかりを","ひとづてならでいふよしもがな"},
{"あさぼらけうぢのかはぎりたえだえに","あらはれわたるせぜのあじろぎ"},
{"うらみわびほさぬそでだにあるものを","こひにくちなむなこそをしけれ"},
{"もろともにあはれとおもへやまざくら","はなよりほかにしるひともなし"},
{"はるのよのゆめばかりなるたまくらに","かひなくたたむなこそをしけれ"},
{"こころにもあらでうきよにながらへば","こひしかるべきよはのつきかな"},
{"あらしふくみむろのやまのもみぢばは","たつたのかはのにしきなりけり"},
{"さびしさにやどをたちいでてながむれば","いづくもおなじあきのゆふぐれ"},
{"ゆふさればかどたのいなばおとづれて","あしのまろやにあきかぜぞふく"},
{"おとにきくたかしのはまのあだなみは","かけじやそでのぬれもこそすれ"},
{"たかさごのをのへのさくらさきにけり","とやまのかすみたたずもあらなむ"},
{"うかりけるひとをはつせのやまおろしよ","はげしかれとはいのらぬものを"},
{"ちぎりおきしさせもがつゆをいのちにて","あはれことしのあきもいぬめり"},
{"わたのはらこぎいでてみればひさかたの","くもゐにまがふおきつしらなみ"},
{"せをはやみいはにせかるるたきがはの","われてもすゑにあはむとぞおもふ"},
{"あはぢしまかよふちどりのなくこゑに","いくよねざめぬすまのせきもり"},
{"あきかぜにたなびくくものたえまより","もれいづるつきのかげのさやけさ"},
{"ながからむこころもしらずくろかみの","みだれてけさはものをこそおもへ"},
{"ほととぎすなきつるかたをながむれば","ただありあけのつきぞのこれる"},
{"おもひわびさてもいのちはあるものを","うきにたへぬはなみだなりけり"},
{"よのなかよみちこそなけれおもひいる","やまのおくにもしかぞなくなる"},
{"ながらへばまたこのごろやしのばれむ","うしとみしよぞいまはこひしき"},
{"よもすがらものおもふころはあけやらで","ねやのひまさへつれなかりけり"},
{"なげけとてつきやはものをおもはする","かこちがほなるわがなみだかな"},
{"むらさめのつゆもまだひぬまきのはに","きりたちのぼるあきのゆふぐれ"},
{"なにはえのあしのかりねのひとよゆゑ","みをつくしてやこひわたるべき"},
{"たまのをよたえなばたえねながらへば","しのぶることのよわりもぞする"},
{"みせばやなをじまのあまのそでだにも","ぬれにぞぬれしいろはかはらず"},
{"きりぎりすなくやしもよのさむしろに","ころもかたしきひとりかもねむ"},
{"わがそではしほひにみえぬおきのいしの","ひとこそしらねかわくまもなし"},
{"よのなかはつねにもがもななぎさこぐ","あまのをぶねのつなでかなしも"},
{"みよしののやまのあきかぜさよふけて","ふるさとさむくころもうつなり"},
{"おほけなくうきよのたみにおほふかな","わがたつそまにすみぞめのそで"},
{"はなさそふあらしのにはのゆきならで","ふりゆくものはわがみなりけり"},
{"こぬひとをまつほのうらのゆふなぎに","やくやもしほのみもこがれつつ"},
{"かぜそよぐならのをがはのゆふぐれは","みそぎぞなつのしるしなりける"},
{"ひともをしひともうらめしあぢきなく","よをおもふゆゑにものおもふみは"},
{"ももしきやふるきのきばのしのぶにも","なほあまりあるむかしなりけり"}};








        public Form1()
        {
            
            InitializeComponent();

            


            kanas.Add(a);
            kanas.Add(i);
            kanas.Add(u);
            kanas.Add(e);
            kanas.Add(o);
            kanas.Add(ka);
            kanas.Add(ki);
            kanas.Add(ku);
            kanas.Add(ke);
            kanas.Add(ko);
            kanas.Add(sa);
            kanas.Add(si);
            kanas.Add(su);
            kanas.Add(se);
            kanas.Add(so);
            kanas.Add(ta);
            kanas.Add(ti);
            kanas.Add(tu);
            kanas.Add(te);
            kanas.Add(to);
            kanas.Add(na);
            kanas.Add(ni);
            kanas.Add(nu);
            kanas.Add(ne);
            kanas.Add(no);
            kanas.Add(ha);
            kanas.Add(hi);
            kanas.Add(hu);
            kanas.Add(he);
            kanas.Add(ho);
            kanas.Add(ma);
            kanas.Add(mi);
            kanas.Add(mu);
            kanas.Add(me);
            kanas.Add(mo);
            kanas.Add(ya);
            kanas.Add(yu);
            kanas.Add(yo);
            kanas.Add(ra);
            kanas.Add(ri);
            kanas.Add(ru);
            kanas.Add(re);
            kanas.Add(ro);
            kanas.Add(wa);
            kanas.Add(wo);
            kanas.Add(wi);
            kanas.Add(we);//全47文字



            





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
            string langPath = "jpn-ocr/jpn.traineddata";//tesseractの設定
            string lngStr = "jpn";

           
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
                        OpenCvSharp.Rect appRect = Cv2.BoundingRect(normalizedEdges);
                        
                        //OpenCvSharp.Point2f[] point2Fs = new OpenCvSharp.Point2f[] { new Point2f(normalizedEdges[0].X, normalizedEdges[0].Y), new Point2f(normalizedEdges[1].X, normalizedEdges[1].Y), new Point2f(normalizedEdges[2].X, normalizedEdges[2].Y), new Point2f(normalizedEdges[3].X, normalizedEdges[3].Y) };
                        if (normalizedEdges.Length == 4&&appRect.Size.Height*appRect.Size.Width<=dst.Width*dst.Height*0.98&& appRect.Size.Height * appRect.Size.Width >= dst.Width * dst.Height * 0.02)
                        {
                            OpenCvSharp.Point2f[] point2Fs = new OpenCvSharp.Point2f[] { new Point2f(normalizedEdges[0].X, normalizedEdges[0].Y), new Point2f(normalizedEdges[1].X, normalizedEdges[1].Y), new Point2f(normalizedEdges[2].X, normalizedEdges[2].Y), new Point2f(normalizedEdges[3].X, normalizedEdges[3].Y) };
                            Cv2.Line(img, normalizedEdges[0].X, normalizedEdges[0].Y, normalizedEdges[1].X, normalizedEdges[1].Y, new Scalar(255, 0, 0), 3);
                            Cv2.Line(img, normalizedEdges[1].X, normalizedEdges[1].Y, normalizedEdges[2].X, normalizedEdges[2].Y, new Scalar(255, 0, 0), 3);
                            Cv2.Line(img, normalizedEdges[2].X, normalizedEdges[2].Y, normalizedEdges[3].X, normalizedEdges[3].Y, new Scalar(255, 0, 0), 3);
                            Cv2.Line(img, normalizedEdges[3].X, normalizedEdges[3].Y, normalizedEdges[0].X, normalizedEdges[0].Y, new Scalar(255, 0, 0), 3);
                            Mat homo =Cv2.GetPerspectiveTransform(point2Fs , new OpenCvSharp.Point2f[] { new Point2f(266,266), new Point2f(266, 0), new Point2f(0, 0), new Point2f(0, 266) } );//180x274の1割り増しをしてふちに備える
                            Mat huda = new Mat(266, 266, MatType.CV_32F); //もともと180x274の予定だった
                            Cv2.WarpPerspective(dst,huda,homo, new OpenCvSharp.Size(266,266));//スキャナーで読みっとった時のサイズによってこの値に決まった。ふち(5x5)を取るためにさらに加えた
                            Mat huda_hutidori = huda.Clone(new OpenCvSharp.Rect(5, 5, 256, 256));
                            Cv2.AdaptiveThreshold(huda_hutidori,huda_hutidori, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 11, 4);


                            /*
                            using (var tesseract = new Tesseract.TesseractEngine(langPath, lngStr))
                            {
                                // OCRの実行
                                
                                //Tesseract.Page page = tesseract.Process(huda_hutidori);

                                //表示
                                //Console.WriteLine(page.GetText());
                                
                            }
                            */
                            //余白の削除
                           

                            int tmp_wakascore = 14;
                            int waka_no = 0;
                            Mat tmp_huda_hutidori = huda_hutidori.Clone();
                            for (int i = 0; i < 4; i++)
                            {
                                Cv2.Rotate(tmp_huda_hutidori, tmp_huda_hutidori, RotateFlags.Rotate90Clockwise);
                                huda_hutidori = tmp_huda_hutidori.Clone();
                                Mat huda_not = new Mat();
                                Cv2.BitwiseNot(huda_hutidori, huda_not);
                                OpenCvSharp.Point[][] rects = huda_not.FindContoursAsArray(RetrievalModes.Tree, ContourApproximationModes.ApproxNone);
                                int min_x = 125;
                                int min_y = 125;
                                int max_x = 130;
                                int max_y = 130;

                                for (int j = 1; j < rects.GetLength(0); j++)
                                {
                                    OpenCvSharp.Rect ret = Cv2.BoundingRect(rects[j]);
                                    //Console.WriteLine(min_x + " " + min_y + " " + max_x + " " + max_y);
                                    if (min_x > ret.X) min_x = ret.X;
                                    if (min_y > ret.Y) min_y = ret.Y;
                                    //if (max_x < ret.X + ret.Width) max_x = ret.X + ret.Width;
                                    //if (max_y < ret.Y + ret.Height) max_y = ret.Y + ret.Height;
                                }

                                min_x -= 8;
                                min_y -= 8;
                                //max_x += 8;
                                //max_y += 10;
                                if (min_x < 0) min_x = 0;
                                //if (max_x > 255) max_x = 255;
                                if (min_y < 0) min_y = 0;
                                //if (max_y > 255) max_y = 255;
                                Console.WriteLine((255 - min_x) + ":" + (255 - min_y) + ":");

                                Mat yohaku_nukitori = huda_hutidori.Clone(new OpenCvSharp.Rect(min_x, min_y, 255 - min_x, 255 - min_y));

                                if (255 - min_x <= 210) break;
                                if (255 - min_y <= 235) break;

                                //Console.WriteLine(min_x+" " +min_y+" "+ max_x+" "+ max_y);


                                //Console.WriteLine(min_x + "," + min_y + "," + (max_x - min_x) + "," + (max_y - min_y));
                                huda_hutidori = yohaku_nukitori.Clone();

                                //Cv2.Resize(yohaku_nukitori, huda_hutidori, new OpenCvSharp.Size(256, 256));
                                Mat bitwise = new Mat();
                                Mat notb = new Mat();



                                string waka = "";
                                
                                for (int moji = 0; moji < 14; moji++)//14文字すべてを検出する
                                {

                                    Mat trimmed_huda = huda_hutidori.Clone(new OpenCvSharp.Rect(177 - ((moji / 5)) * 85, 5 + (moji % 5) * 46, 47, 37));//210,
                                    Cv2.Rectangle(yohaku_nukitori, new OpenCvSharp.Rect(177 - ((moji / 5)) * 85, 5 + (moji % 5) * 46, 47, 37), new Scalar(0), 1);
                                    int kana_no = 0;
                                    int detected_kana_no = 0;
                                    int min_count = 10000000;
                                    using (new Window("trimmed_huda", WindowMode.AutoSize, trimmed_huda)) ;
                                    Cv2.MoveWindow("trimmed_huda", 1300, 0);
                                    foreach (Mat kana in kanas)
                                    {
                                        Cv2.BitwiseXor(trimmed_huda, kana, bitwise);
                                        int tmp_count = Cv2.CountNonZero(bitwise);
                                        //using (new Window("XOR" + kana_no, WindowMode.AutoSize, bitwise)) ;
                                        //Cv2.MoveWindow("XOR" + kana_no, (kana_no % 5) * 89, (kana_no / 5) * 69);
                                        Cv2.BitwiseNot(kana, notb);
                                        Cv2.BitwiseAnd(notb, trimmed_huda, bitwise);
                                        tmp_count = tmp_count - Cv2.CountNonZero(bitwise);
                                        //using (new Window("AND" + kana_no, WindowMode.AutoSize, bitwise)) ;
                                        //Cv2.MoveWindow("AND" + kana_no, 500 + (kana_no % 5) * 89, (kana_no / 5) * 69);
                                        //using (new Window("AND", WindowMode.AutoSize, bitwise)) ;

                                        if (tmp_count < min_count)
                                        {
                                            detected_kana_no = kana_no;
                                            min_count = tmp_count;
                                        }
                                        kana_no++;
                                    }
                                    //Console.WriteLine(detected_kana_no);
                                    waka += hiragana.Substring(detected_kana_no, 1);
                                    using (new Window("detect", WindowMode.AutoSize, kanas[detected_kana_no])) ;
                                    Cv2.MoveWindow("detect", 1150, 0);
                                    using (new Window("huda_hutidori", WindowMode.AutoSize, huda_hutidori)) ;
                                    
                                }
                                string waka_result = Detectwaka(waka);
                                Console.WriteLine(waka_result);
                                if (int.Parse(waka_result.Split(',')[1]) < tmp_wakascore)
                                {
                                    tmp_wakascore = int.Parse(waka_result.Split(',')[1]);
                                    waka_no = int.Parse(waka_result.Split(',')[0]);
                                }

                                using (new Window("yohaku_nukitori", WindowMode.AutoSize, yohaku_nukitori)) ;

                            }
                            Console.WriteLine(hyaku[waka_no, 1]);
                            
                            

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
                    //using (new Window("dst", WindowMode.AutoSize, dst))
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
                OpenCvSharp.Rect rect;
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

        public string Detectwaka(string waka)
        {
            int hyaku_no=0;
            int hyaku_tmp = 14;
            for (int i = 0; i < hyaku.GetLength(0); i++){
                int a = Calcworddist(waka, hyaku[i,1]);
                if (a < hyaku_tmp){
                    hyaku_tmp = a;
                    hyaku_no = i;
                }
            }
            return hyaku_no+","+hyaku_tmp;
        }

        public int Calcworddist(string waka,string hyaku)
        {
            int match = 0;
            for(int i = 0; i < waka.Length - 1; i++)
            {
                if (waka.Substring(i, 1).Equals(hyaku.Substring(i, 1))){
                    match++;
                }
            }
            return waka.Length - match;
            
        }

    }
}
