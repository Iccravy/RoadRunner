using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace Ksenia
{
    public partial class Form1 : Form
    {
       public class Point_road
        {
           public float X;
           public float Y;
            public float X_N = 0;
            public float Y_N = 0;
            public float X_P = 0;
            public float Y_P = 0;
            public bool crossroads = false;
            public bool crossroads_end = false;

            public bool end = false;
            public int id;
            public Point_road(float X, float Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        public class Car
        {
            public float x;
            public int id;
            public Car(int id)
            {
                this.id = id;
            }
        }

        public class End_point
        {
            public int Number;
        }

        Graphics g , g2;    //  графический объект — некий холст
        
         Image image;
        Bitmap buf;  //  буфер для Bitmap-изображения
        bool Line = false, LineButton = false, mouseDown = false, addCar = false, Next = false;
       // float x, y;
        Point point1 = new Point();
        Point pointOld = new Point();

        int id_car = 0;

        Thread thread;


        public static List<Point_road> point_Roads = new List<Point_road>();
        public static List<Car> cars = new List<Car>();
        public static List<PictureBox> pictureBoxes = new List<PictureBox>();
        public event System.Windows.Forms.MouseEventHandler MouseClick;


        public Form1()
        {
            InitializeComponent();
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g2 = Graphics.FromImage(image);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Graphics g = this.CreateGraphics();
            /*buf = new Bitmap(pictureBox1.Width, pictureBox1.Height);  // с размерами
            g = Graphics.FromImage(buf);   // инициализация g
            Pen redPen = new Pen(Color.Red);
            g.DrawLine( redPen,0, 0, pictureBox1.Width, pictureBox1.Height);*/
            //  g = pictureBox1.CreateGraphics();
            //  буфер для Bitmap-изображения
            image = new Bitmap("C://Users//stalk//Pictures//10955_900.png");
            buf = new Bitmap(pictureBox1.Width, pictureBox1.Height);  // с размерами
                                                                      // g = Graphics.FromImage(buf);   // инициализация g
            g = pictureBox1.CreateGraphics();
          //  pictureBox1.BackColor = Color.Transparent;
            g.Clear(Color.Empty);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            float MinDest = 12;
            float dest = 0;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MessageBox.Show("LEFT");
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MessageBox.Show("RIGHT");
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Line)
            {
                //  image = g.
            //    image = pictureBox1.Image;
               // GraphicsState containerState = g.Save();
               // base.OnPaintBackground(e);
               //g2.
                Pen redPen = new Pen(Color.Red);
                Pen white = new Pen(Color.White);

                /* buf = new Bitmap(pictureBox1.Width, pictureBox1.Height);*/
                //  Graphics r = Graphics.FromImage(buf);

                g.DrawLine(redPen, point1, e.Location);

             
                
                g.DrawLine(white, point1, pointOld);
             //   g.DrawImage(image, 0, 0);

                pointOld = e.Location;
                //g.Restore(containerState);
              //  pictureBox1.Image = image;
                
               // g.Restore(image);


            }
        }

        private void add_Point(float x2, float y2)
        {
            double x1 = point1.X;
            double y1 = point1.Y;

           
            // Rectangle eb = new Rectangle(
            SolidBrush mySolidBrushY = new SolidBrush(Color.Aqua);
           
          /*  g.FillEllipse(mySolidBrushY, x2 - 9, y2 - 9, 18, 18);
            g.FillEllipse(mySolidBrushY, Convert.ToSingle(x1) - 9, Convert.ToSingle(y1) - 9, 18, 18);*/
           // mySolidBrushY.Dispose();


           // Rectangle ee = new Rectangle();

            double dx = x1 - x2;
            double dy = y1 - y2;
            int dinst = Convert.ToInt32( Math.Sqrt(dx * dx + dy * dy));
            int RoadCount = point_Roads.Count;
            for (int i = 0, j = point_Roads.Count; i <= dinst; i++, j++)
            {
                double Rab = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
                double k = i / Rab;
                float Xc = Convert.ToSingle( x1 + (x2 - x1) * k);
                float Yc = Convert.ToSingle( y1 + (y2 - y1) * k );
            //    Pen white = new Pen(Color.Black);
               // Rectangle ee = new Rectangle(Convert.ToInt32(Xc), Convert.ToInt32(Yc), 30, 30);
              //  g.DrawLine(white, Xc - 10, Yc - 10, Xc + 10, Yc + 10);
              //  g.DrawEllipse(white, ee);
                point_Roads.Add(new Point_road(Xc, Yc));
                if (i != dinst && i != 0)
                {
                    point_Roads[j].X_P = point_Roads[j - 1].X;
                    point_Roads[j].Y_P = point_Roads[j - 1].Y;
                    point_Roads[j - 1].X_N = point_Roads[j].X;
                    point_Roads[j - 1].Y_N = point_Roads[j].Y;
                }
                else if (i == 0 )
                {
                    point_Roads[j].end = true;
                    point_Roads[j].X_P = 0;
                    point_Roads[j].Y_P = 0;
                }
                else if (i == dinst)
                {

                    point_Roads[j].end = true;
                    point_Roads[j ].X_N = 0;
                    point_Roads[j].Y_N = 0;
                    point_Roads[j].X_P = point_Roads[j - 1].X;
                    point_Roads[j].Y_P = point_Roads[j - 1].Y;
                    point_Roads[j - 1].X_N = point_Roads[j].X;
                    point_Roads[j - 1].Y_N = point_Roads[j].Y;
                }

                float MinDest = 16;
                float dest = 0;
                for (int n = 0; n < RoadCount; n++)
                {
                    if (point_Roads[n].end)
                    {
                        dest = (float)(Math.Sqrt(Math.Pow(point_Roads[n].X - Xc, 2) + Math.Pow(point_Roads[n].Y - Yc, 2)));
                        if (dest < MinDest)
                        {
                            MinDest = dest;
                          //  g.FillEllipse(mySolidBrushY, Xc - 12, Yc - 12, 24, 24);
                            point_Roads[n].end = false;
                            point_Roads[j].end = false;

                            point_Roads[n].crossroads_end = true;
                            point_Roads[j].crossroads = true;

                           /* if (point_Roads[n].X_N == 0 && point_Roads[n].Y_N == 0)
                            {
                                point_Roads[n].X_N = Xc;
                                point_Roads[n].Y_N = Yc;
                            }
                            else if (point_Roads[n].X_P == 0 && point_Roads[n].Y_P == 0)
                            {
                                point_Roads[n].X_P = Xc;
                                point_Roads[n].Y_P = Yc;
                            }
                            */
                           /* point_Roads[n].X_N = Xc;
                            point_Roads[n].Y_N = Yc;*/
                            point_Roads[j].id = n;
                            point_Roads[n].id = j;
                          /*  if (point_Roads[j].end)
                            {
                                if (point_Roads[j].X_N == 0 && point_Roads[j].Y_N == 0)
                                {
                                    point_Roads[j].X_N = point_Roads[n].X;
                                    point_Roads[j].Y_N = point_Roads[n].Y;
                                }
                                else if (point_Roads[j].X_P == 0 && point_Roads[j].Y_P == 0)
                                {
                                    point_Roads[j].X_P = point_Roads[n].X;
                                    point_Roads[j].Y_P = point_Roads[n].Y;
                                }
                            }*/
                            Pen her = new Pen(Color.Red);
                            g.DrawLine(her, point_Roads[n].X, point_Roads[n].Y, Xc, Yc);
                            // n += 20;
                            n += 5;

                        }

                         

                    }
                   else if (Convert.ToInt32(Xc) == Convert.ToInt32(point_Roads[n].X) && Convert.ToInt32(Yc) == Convert.ToInt32(point_Roads[n].Y) && point_Roads[n].end == false)
                    {
                        point_Roads[n].crossroads = true;
                        point_Roads[j].crossroads = true;
                        point_Roads[n].id = j;
                        point_Roads[j].id = n;
                        /*Pen her = new Pen(Color.Blue);
                        g.DrawLine(her, Xc - 30, Yc - 30, Xc + 30, Yc + 30);*/
                        n += 4;
                    }

                }
/*
                for (int n = 0; n < RoadCount; n++)
                {
                    

                    if (Convert.ToInt32(Xc) == Convert.ToInt32(point_Roads[n].X) && Convert.ToInt32(Yc) == Convert.ToInt32(point_Roads[n].Y))
                    {
                        point_Roads[n].crossroads = true;
                        point_Roads[j].crossroads = true;
                        point_Roads[n].id = j;
                        point_Roads[j].id = n;
                        Pen her = new Pen(Color.Blue);
                        g.DrawLine(her, Xc - 30, Yc - 30, Xc + 30, Yc + 30);
                        n += 4;
                    }
                }*/

            }

           
            // ee.Contains(Convert.ToInt32(x2), y2);
        }

        private void add_Car(float x, float y)
        {

            for (int i = 0; i <= point_Roads.Count; i++)
            {
                float MinDest = 6;
                float dest = 0;
                dest = (float)(Math.Sqrt(Math.Pow(point_Roads[i].X - x, 2) + Math.Pow(point_Roads[i].Y - y, 2)));
                if (dest < MinDest)
                {
                    Pen her = new Pen(Color.Bisque);

                  
                    cars.Add(new Car(i));
                    int j = cars.Count - 1;
                    pictureBoxes.Add(new PictureBox());
                    Point point2 = new Point();
                    point2.X = Convert.ToInt32(point_Roads[cars[j].id].X - 10);
                    point2.Y = Convert.ToInt32(point_Roads[cars[j].id].Y - 10);


                    pictureBoxes[j].Image = new Bitmap("images(2).jpg");
                    pictureBoxes[j].Image = ScaleImage(pictureBoxes[j].Image, 20, 20);
                    //  pictureBox1.Image = new Bitmap("C://Users//stalk//Pictures//10955_900.png");
                    pictureBoxes[j].Location = point2;
                    pictureBoxes[j].Width = 20;
                    pictureBoxes[j].Height = 20;
                    pictureBox1.Controls.Add(pictureBoxes[j]);

                    /*  g.DrawLine(her, point_Roads[i].X + 20, point_Roads[i].Y + 20, point_Roads[i].X - 20, point_Roads[i].Y - 20);

                      g.DrawLine(her, point_Roads[i].X - 20, point_Roads[i].Y - 20, point_Roads[i].X + 20, point_Roads[i].Y + 20);*/
                    break;
                }
            }
        }

        public static Semaphore semaphore = new Semaphore(1, 1);
        static Image ScaleImage(Image source, int width, int height)
        {

            Image dest = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, width, height);  // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;

                if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное изображение меньше целевого
                {
                    int left = (width - source.Width) / 2;
                    int top = (height - source.Height) / 2;
                    gr.DrawImage(source, left, top, source.Width, source.Height);
                }
                else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного изображения более широкие
                {
                    float cy = srcheight / srcwidth * dstwidth;
                    float top = ((float)dstheight - cy) / 2.0f;
                    if (top < 1.0f) top = 0;
                    gr.DrawImage(source, 0, top, dstwidth, cy);
                }
                else  // Пропорции исходного изображения более узкие
                {
                    float cx = srcwidth / srcheight * dstheight;
                    float left = ((float)dstwidth - cx) / 2.0f;
                    if (left < 1.0f) left = 0;
                    gr.DrawImage(source, left, 0, cx, dstheight);
                }

                return dest;
            }
        }
        public  int restore(int id, bool step)
        {
            int res = 0;
            if (step)
            {
                for (int i = 0; i <= 16; i++)
                {
                    if (id == 5)
                    { int b = 0; }
                    if (point_Roads[id - i].X_P != 0 && point_Roads[id - i].Y_P != 0)
                    {
                        res++;
                    }
                    else
                    {
                        return id - res + 1;
                    }
                }
                    return  id - res + 1;
            }
            else
            {
                for (int i = 0; i <= 16; i++)
                {
                    if (point_Roads[id + i].X_N != 0 && point_Roads[id + i].Y_N != 0)
                    {
                        res++;
                    }
                    else
                    {
                        return id + res;
                    }
                }
                return id + res;
            }
        }
        public void start(object car_id)
        {
          /*  pictureBoxes.Add(new PictureBox());
            Point point2 = new Point();
            point2.X = Convert.ToInt32(point_Roads[cars[(int)car_id].id].X);
            point2.Y = Convert.ToInt32(point_Roads[cars[(int)car_id].id].Y);
            pictureBoxes[(int)car_id].Image = new Bitmap("C://Users//stalk//Pictures//10955_900.png");
            //  pictureBox1.Image = new Bitmap("C://Users//stalk//Pictures//10955_900.png");
            pictureBoxes[(int)car_id].Location = point2;
            pictureBoxes[(int)car_id].Width = 30;
            pictureBoxes[(int)car_id].Height = 30;
            // pictureBox1.Controls.Add(pictureBoxes[(int)car_id]);
            pictureBoxes[(int)car_id].Parent = pictureBox1;*/
            if (cars.Count != 0)
            {
                // SolidBrush mySolidBrushY = new SolidBrush(Color.Aqua);
                SolidBrush mySolidBrushY = new SolidBrush(Color.Red);
                Pen her = new Pen(Color.Red);
                SolidBrush mySolidBrushX = new SolidBrush(Color.BlanchedAlmond);
                bool step = true;
                int next = 0;
                int rest = 0;
                bool res = false;
                int j = 0;

                for (int i = 0; i < 100000; i++)
                {

                    if (point_Roads[cars[(int)car_id].id].crossroads)
                    {
                        Random rnd = new Random();
                        int cross = rnd.Next(0, 3);
                        if (cross == 2)
                        {
                            next = 0;
                            cars[(int)car_id].id = point_Roads[cars[(int)car_id].id].id;
                            step = true;

                        }
                        j = point_Roads[cars[(int)car_id].id].id;
                        res = true;
                        rest = 0;

                    }
                    else if (point_Roads[cars[(int)car_id].id].crossroads_end && next != 1 && next != 2)
                    {


                        cars[(int)car_id].id = point_Roads[cars[(int)car_id].id].id;
                        step = true;
                        j = point_Roads[cars[(int)car_id].id].id;
                        res = true;
                        rest = 0;



                    }
                    /* lock (this.thread)
                     {*/
                    try
                    {
                        semaphore.WaitOne();
                        if (point_Roads[cars[(int)car_id].id].X_N != 0 && point_Roads[cars[(int)car_id].id].Y_N != 0 && step)
                        {
                          //  Thread.Sleep(1000);
                            cars[(int)car_id].id += 1;
                            Point point2 = new Point();
                            point2.X = Convert.ToInt32(point_Roads[cars[(int)car_id].id].X - 10);
                            point2.Y = Convert.ToInt32(point_Roads[cars[(int)car_id].id].Y - 10);
                            Action action = () => pictureBoxes[(int)car_id].Location = point2;
                            if (pictureBoxes[(int)car_id].InvokeRequired)
                            {
                                pictureBoxes[(int)car_id].Invoke(action);
                            }
                            else
                            {
                                action();
                            }
                            if (!point_Roads[cars[(int)car_id].id].crossroads && res && rest < 36 && rest > 10)
                            {
                                res = false;
                                g.DrawLine(her, point_Roads[restore(j, false)].X_P, point_Roads[restore(j, false)].Y_P, point_Roads[restore(j, true)].X_P, point_Roads[restore(j, true)].Y_P);
                               // g.DrawLine(her, point_Roads[restore(j, true)].X_P, point_Roads[restore(j, true)].Y_P, point_Roads[cars[(int)car_id].id].X, point_Roads[cars[(int)car_id].id].Y);

                                g.DrawLine(her, point_Roads[restore(cars[(int)car_id].id, step)].X_P, point_Roads[restore(cars[(int)car_id].id, step)].Y_P, point_Roads[cars[(int)car_id].id].X, point_Roads[cars[(int)car_id].id].Y);
                            }
                            else if (!point_Roads[cars[(int)car_id].id].crossroads)
                            {
                                g.DrawLine(her, point_Roads[restore(cars[(int)car_id].id, step)].X_P, point_Roads[restore(cars[(int)car_id].id, step)].Y_P, point_Roads[cars[(int)car_id].id].X, point_Roads[cars[(int)car_id].id].Y);

                            }
                            
                            //  pictureBoxes[(int)car_id].Invoke(() => pictureBoxes[(int)car_id].Location = point2);
                            // g.FillEllipse(mySolidBrushY, point_Roads[cars[(int)car_id].id].X - 9, point_Roads[cars[(int)car_id].id].Y - 9, 18, 18);
                            if (point_Roads[cars[(int)car_id].id].X_N == 0 && point_Roads[cars[(int)car_id].id].Y_N == 0) step = false;

                        }
                        else if (point_Roads[cars[(int)car_id].id].X_P != 0 && point_Roads[cars[(int)car_id].id].Y_P != 0)
                        {
                            cars[(int)car_id].id -= 1;
                            Point point2 = new Point();
                            point2.X = Convert.ToInt32(point_Roads[cars[(int)car_id].id].X - 10);
                            point2.Y = Convert.ToInt32(point_Roads[cars[(int)car_id].id].Y - 10);
                            Action action = () => pictureBoxes[(int)car_id].Location = point2;
                            if (pictureBoxes[(int)car_id].InvokeRequired)
                            {
                                pictureBoxes[(int)car_id].Invoke(action);
                            }
                            else
                            {
                                action();
                            }

                            if (!point_Roads[cars[(int)car_id].id].crossroads && res && rest < 55 && rest > 10)
                            {
                                res = false;
                                g.DrawLine(her, point_Roads[restore(j, false)].X_P, point_Roads[restore(j, false)].Y_P, point_Roads[restore(j, true)].X_P, point_Roads[restore(j, true)].Y_P);
                                // g.DrawLine(her, point_Roads[restore(j, true)].X_P, point_Roads[restore(j, true)].Y_P, point_Roads[cars[(int)car_id].id].X, point_Roads[cars[(int)car_id].id].Y);

                                g.DrawLine(her, point_Roads[restore(cars[(int)car_id].id, step)].X_P, point_Roads[restore(cars[(int)car_id].id, step)].Y_P, point_Roads[cars[(int)car_id].id].X, point_Roads[cars[(int)car_id].id].Y);
                            }
                            else if (!point_Roads[cars[(int)car_id].id].crossroads)
                            {
                                g.DrawLine(her, point_Roads[restore(cars[(int)car_id].id, step)].X_P, point_Roads[restore(cars[(int)car_id].id, step)].Y_P, point_Roads[cars[(int)car_id].id].X, point_Roads[cars[(int)car_id].id].Y);

                            }

                            // g.FillEllipse(mySolidBrushX, point_Roads[cars[(int)car_id].id].X - 9, point_Roads[cars[(int)car_id].id].Y - 9, 18, 18);
                            if (point_Roads[cars[(int)car_id].id].X_P == 0 && point_Roads[cars[(int)car_id].id].Y_P == 0) step = true;
                        }
                        else
                        {
                            g.FillEllipse(mySolidBrushY, point_Roads[cars[0].id].X - 13, point_Roads[cars[0].id].Y - 13, 36, 36);
                        }
                        rest++;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                   // }
                    next++;
                    Thread.Sleep(10);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //  pictureBox1.Image = new Bitmap("C://Users//stalk//Pictures//10955_900.png");
            Form1 u = new Form1();
            pictureBoxes.Add(new PictureBox());
            PictureBox pictureBox2 = new PictureBox();
            Point point2 = new Point();
            pictureBox2.Image = new Bitmap("C://Users//stalk//Pictures//10955_900.png");
            point2.X = Convert.ToInt32(point_Roads[cars[0].id].X - 100);
            point2.Y = Convert.ToInt32(point_Roads[cars[0].id].Y - 100);
            pictureBox2.Width = 300;
            pictureBox2.Height = 300;
            pictureBox2.Location = point2;
            
           // picture.
            //this.Controls.Add(picture);
            pictureBox1.Controls.Add(pictureBox2);

        }
        // -----------------------------------------------------------------------------------------
        private void button6_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox2 = new PictureBox();
            Point point2 = new Point();
            pictureBox2.Image = new Bitmap("10955_900.png");
            point2.X = Convert.ToInt32(point_Roads[cars[0].id].X - 100);
            point2.Y = Convert.ToInt32(point_Roads[cars[0].id].Y - 100);
            pictureBox2.Width = 300;
            pictureBox2.Height = 300;
            pictureBox2.Location = point2;
            pictureBox1.Controls.Add(pictureBox2);
            timer1.Start();
            timer1.Interval = 10000;
            timer1.Tick += new EventHandler((o, ev) =>
            {
                if (cars.Count != 0)
                {
                    SolidBrush mySolidBrushY = new SolidBrush(Color.Aqua);
                    SolidBrush mySolidBrushX = new SolidBrush(Color.BlanchedAlmond);
                    bool step = true;
                    int next = 0;

                    for (int i = 0; i < 1000; i++)
                    {
                        if (point_Roads[cars[0].id].crossroads)
                        {
                            Random rnd = new Random();
                            int cross = rnd.Next(0, 3);
                            if (cross == 2)
                            {
                                next = 0;
                                cars[0].id = point_Roads[cars[0].id].id;
                                step = true;

                            }

                        }
                        else if (point_Roads[cars[0].id].crossroads_end && next != 1 && next != 2)
                        {


                            cars[0].id = point_Roads[cars[0].id].id;
                            step = true;


                        }
                        if (point_Roads[cars[0].id].X_N != 0 && point_Roads[cars[0].id].Y_N != 0 && step)
                        {
                            cars[0].id += 1;

                            
                            point2.X = Convert.ToInt32(point_Roads[cars[0].id].X);
                            point2.Y = Convert.ToInt32(point_Roads[cars[0].id].Y);
                            pictureBox2.Location = point2;

                         //   g.FillEllipse(mySolidBrushY, point_Roads[cars[0].id].X - 9, point_Roads[cars[0].id].Y - 9, 18, 18);
                             if (point_Roads[cars[0].id].X_N == 0 && point_Roads[cars[0].id].Y_N == 0) step = false;

                        }
                        else if (point_Roads[cars[0].id].X_P != 0 && point_Roads[cars[0].id].Y_P != 0)
                        {
                            cars[0].id -= 1;
                            point2.X = Convert.ToInt32(point_Roads[cars[0].id].X);
                            point2.Y = Convert.ToInt32(point_Roads[cars[0].id].Y);
                            pictureBox2.Location = point2;
                            // g.FillEllipse(mySolidBrushX, point_Roads[cars[0].id].X - 9, point_Roads[cars[0].id].Y - 9, 18, 18);
                            if (point_Roads[cars[0].id].X_P == 0 && point_Roads[cars[0].id].Y_P == 0) step = true;
                        }
                        else
                        {
                            g.FillEllipse(mySolidBrushY, point_Roads[cars[0].id].X - 13, point_Roads[cars[0].id].Y - 13, 36, 36);
                        }
                        timer1.Interval = 5000;
                        next++;
                    }
                }
            }
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {


            // Thread thread = new Thread(start);
            //  timer1.Start();

            /* timer1.Tick += new EventHandler((o, ev) =>
             {
                 thread = new Thread(start);
                 thread.Name = "1";
                 thread.Start();
                start();


             }
             );*/
            int time = 15;
            if (cars.Count > 5)
            {
                time = 25;
            }
            image = pictureBox1.Image;

            for (int i = 0; i < cars.Count; i++)
            {
              /*  pictureBoxes.Add(new PictureBox());
                Point point2 = new Point();
                point2.X = Convert.ToInt32(point_Roads[cars[i].id].X);
                point2.Y = Convert.ToInt32(point_Roads[cars[i].id].Y);
                

                pictureBoxes[i].Image = new Bitmap("images(2).jpg");
                pictureBoxes[i].Image = ScaleImage(pictureBoxes[i].Image, 20, 20);
              //  pictureBox1.Image = new Bitmap("C://Users//stalk//Pictures//10955_900.png");
                pictureBoxes[i].Location = point2;
                pictureBoxes[i].Width = 20;
                pictureBoxes[i].Height = 20;
                 pictureBox1.Controls.Add(pictureBoxes[i]);*/
               // this.Controls.Add(pictureBoxes[i]);
             //   pictureBoxes[i].Parent = pictureBox1;

                thread = new Thread(start);
                thread.Name =Convert.ToString(i);
                
                thread.Start(i);
                Thread.Sleep(300);
            }
            
            

            // buf = new Bitmap(pictureBox1.Width, pictureBox1.Height);  // с размерами
            // g = Graphics.FromImage(buf);   // инициализация g
           //  Pen redPen = new Pen(Color.Red); // new Pen(Brushes.Red, 4)
            //g.Clear(Color.Blue);
          //  g.DrawLine(redPen, new Point(10, 10), new Point(1900, 1900));
           // Form1.MousePosition();
            
            // g.DrawLine(redPen, 0, 0, 40, 40);


        }
        // -- это старое гавно
        private void button4_Click(object sender, EventArgs e)
        {
            if (cars.Count != 0)
            {
                SolidBrush mySolidBrushY = new SolidBrush(Color.Aqua);
                SolidBrush mySolidBrushX = new SolidBrush(Color.BlanchedAlmond);
                bool step = true;
                int next = 0;
            
                for (int i = 0; i < 100000; i++)
                {
                    if (point_Roads[cars[0].id].crossroads)
                    {
                        Random rnd = new Random();
                        int cross = rnd.Next(0, 3);
                        if (cross == 2)
                        {
                            next = 0;
                            cars[0].id = point_Roads[cars[0].id].id;
                            step = true;

                        }

                    }
                    else if (point_Roads[cars[0].id].crossroads_end && next != 1 && next != 2)
                    {

                        
                            cars[0].id = point_Roads[cars[0].id].id;
                        step = true;
                       
                            
                    }
                    if (point_Roads[cars[0].id].X_N != 0 && point_Roads[cars[0].id].Y_N != 0 && step)
                    {
                        cars[0].id += 1;

                       // g.FillEllipse(mySolidBrushY, point_Roads[cars[0].id].X - 9, point_Roads[cars[0].id].Y - 9, 18, 18);
                        if (point_Roads[cars[0].id].X_N == 0 && point_Roads[cars[0].id].Y_N == 0) step = false;

                    }
                    else if (point_Roads[cars[0].id].X_P != 0 && point_Roads[cars[0].id].Y_P != 0)
                    {
                        cars[0].id -= 1;
                        g.FillEllipse(mySolidBrushX, point_Roads[cars[0].id].X - 9, point_Roads[cars[0].id].Y - 9, 18, 18);
                        if (point_Roads[cars[0].id].X_P == 0 && point_Roads[cars[0].id].Y_P == 0) step = true;
                    }
                    else
                    {
                        g.FillEllipse(mySolidBrushY, point_Roads[cars[0].id].X - 13, point_Roads[cars[0].id].Y - 13, 36, 36);
                    }
                    next++;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (addCar)
            {
                addCar = false;
            }
            else
            {
                addCar = true;
                LineButton = false;
                Line = false;

            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (LineButton)
            {

                if (Line)
                {
                    Pen redPen = new Pen(Color.Red);
                  //  add_Point(e.Location.X, e.Location.Y);

                    g.DrawLine(redPen, point1, e.Location);
                   // g.DrawRectangle(redPen, point1.X - 10, point1.Y - 10, e.X - 10, e.Y - 10);
                   /* g.DrawLine(redPen, point1.X - 10, point1.Y - 10, e.X -10, e.Y - 10);
                    g.DrawLine(redPen, point1.X + 10, point1.Y + 10, e.X + 10, e.Y + 10);*/
                    double dist = 10;
                    double A = 105;
                   // g.s
                    add_Point(e.Location.X, e.Location.Y);
                   /* double startX = point1.X - dist * Math.Cos(-A * Math.PI / 180);
                    double endX = e.X + dist * Math.Cos(-A * Math.PI / 180);

                    double startY = point1.Y - dist * Math.Sin(-A * Math.PI / 180);
                    double endY = e.Y + dist * Math.Sin(-A * Math.PI / 180);
                    g.DrawLine(redPen,Convert.ToSingle(startX), Convert.ToSingle(startY), Convert.ToSingle(endX), Convert.ToSingle(endY));
                   */
                    Line = false;

                }
                else
                {
                    Line = true;
                    point1 = e.Location;
                }
                mouseDown = false;

            }

            if (addCar)
            {
                add_Car(e.X, e.Y);
                float x = e.X;
                float y = e.Y;
            }
            /*  if (e.Button == System.Windows.Forms.MouseButtons.Left)
              {
                  MessageBox.Show("LEFT");
              }
              if (e.Button == System.Windows.Forms.MouseButtons.Right)
              {
                  MessageBox.Show("RIGHT");
              }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LineButton)
            {
                LineButton = false;
            }
            else
            {
                LineButton = true;
               
            }
            
        }
    }
}
