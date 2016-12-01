using Doom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Doom
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Wall> lstWalls;
        float angle = 0.0f;
        float x = 0;
        float y = 0;

        ImageBrush ib;


        int screenHeight = 800;
        int screenWidth = 1000;
        public MainWindow()
        {
            InitializeComponent();

            win.Width = screenWidth;
            win.Height = screenHeight;


            ib = new ImageBrush(new BitmapImage(new Uri(@"C:\Users\romain\documents\visual studio 2013\Projects\Doom\Doom\Content\BrickOldRounded0061_5_S.jpg")));
            lstWalls = new List<Wall>();


            lstWalls.Add(new Wall(new Line() { X1 = 110, X2 = 110, Y1 = 55, Y2 = 110, Stroke = Brushes.Black, StrokeThickness = 2 }));
            lstWalls.Add(new Wall(new Line() { X1 = 110, X2 = 20, Y1 = 55, Y2 = 5, Stroke = Brushes.Black, StrokeThickness = 2 }));
            lstWalls.Add(new Wall(new Line() { X1 = 10, X2 = 20, Y1 = 30, Y2 = 5, Stroke = Brushes.Black, StrokeThickness = 2 }));
            lstWalls.Add(new Wall(new Line() { X1 = 10, X2 = 110, Y1 = 30, Y2 = 110, Stroke = Brushes.Black, StrokeThickness = 2 }));
            AddPlanes();
            Initialize();
            WindowLoaded();
        }

        void Initialize()
        {
            foreach (var item in lstWalls)
            {
                myGrid.Children.Add(item.DrawingLine);
            }
        }

        void WindowLoaded()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            GameLoop();
        }

        void GameLoop()
        {
            List<Wall> orderedList = lstWalls.OrderByDescending(x => x.DistanceOrder).ToList<Wall>();
            int zIndex = 0 ;


            angle1.Content = lstWalls.ElementAt(0).DistanceOrder;
            angle2.Content = lstWalls.ElementAt(1).DistanceOrder;

            distX1.Content = lstWalls.ElementAt(2).DistanceOrder;
            distX2.Content = lstWalls.ElementAt(3).DistanceOrder;


            foreach (Wall wall in orderedList)
            {
                int index = lstWalls.IndexOf(wall);
                wall.Rotation(angle);
                wall.Translate(x, y);


                double size1 = 200 - wall.GetDistanceExt1;
                double size2 = 200 - wall.GetDistanceExt2;
                double cosOrient1 = Math.Acos(wall.GetAngleCos1);
                double cosOrient2 = Math.Acos(wall.GetAngleCos2);



                double horiz1 = 0.0d; 
                double horiz2 = 0.0d; 



                horiz1 = 1000 - ((cosOrient1 - (Math.PI / 4)) / (Math.PI / 2.0d)) * 1000;

                horiz2 = 1000 - ((cosOrient2 - (Math.PI / 4)) / (Math.PI / 2.0d)) * 1000;                

           
                if (size1 < 0)
                {
                    size1 = 0.0;
                }
                if (size2 < 0)
                {
                    size2 = 0.0;
                }



                

                myGrid.Children.OfType<Polygon>().ElementAt(index).Points.Clear();
                //int toto = Grid.GetZIndex(myGrid.Children.OfType<Polygon>().ElementAt(index));
                
                if (wall.GetAngleSin1 > 0 && wall.GetAngleSin2 > 0)
                {
                    myGrid.Children.OfType<Polygon>().ElementAt(index).Points.Add(new Point(horiz1, (screenHeight / 2) - size1 / 2));
                    myGrid.Children.OfType<Polygon>().ElementAt(index).Points.Add(new Point(horiz1, (screenHeight / 2) + size1 / 2));
                    myGrid.Children.OfType<Polygon>().ElementAt(index).Points.Add(new Point(horiz2, (screenHeight / 2) + size2 / 2));
                    myGrid.Children.OfType<Polygon>().ElementAt(index).Points.Add(new Point(horiz2, (screenHeight / 2) - size2 / 2));
                }

                

                //myGrid.Children.OfType<Polygon>().ElementAt(index).Fill = lstBrushes.ElementAt(zIndex);
                Grid.SetZIndex(myGrid.Children.OfType<Polygon>().ElementAt(index), zIndex);

                zIndex++;

            }

        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Left)
            {
                angle = 0.08f;
                if (Keyboard.IsKeyDown(Key.Up))
                {
                    y = 2.0f;
                }
                if (Keyboard.IsKeyDown(Key.Down))
                {
                    y = -2.0f;
                }
            }

            if (e.Key == Key.Right)
            {
                angle = -0.08f;

                if (Keyboard.IsKeyDown(Key.Up))
                {
                    y = 2.0f;
                }
                if (Keyboard.IsKeyDown(Key.Down))
                {
                    y = -2.0f;
                }
            }

            if (e.Key == Key.Up)
            {
                y = 2.0f;
                if (Keyboard.IsKeyDown(Key.Right))
                {
                    angle = -0.08f;
                }
                if (Keyboard.IsKeyDown(Key.Right))
                {
                    angle = 0.08f;
                }
            }
            if (e.Key == Key.Down)
            {
                y = -2.0f;
                if (Keyboard.IsKeyDown(Key.Right))
                {
                    angle = -0.08f;
                }
                if (Keyboard.IsKeyDown(Key.Right))
                {
                    angle = 0.08f;
                }
            }

        }


        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            angle = 0.0f;
            x = 0;
            y = 0;
        }


        public void AddPlanes()
        {

            List<Brush> lstBrushes = new List<Brush>();

            lstBrushes.Add(Brushes.Cornsilk);
            lstBrushes.Add(Brushes.DarkSlateGray);
            lstBrushes.Add(Brushes.Lavender);
            lstBrushes.Add(Brushes.LightGreen);
            lstBrushes.Add(Brushes.LightSteelBlue);
            lstBrushes.Add(Brushes.OrangeRed);
            lstBrushes.Add(Brushes.LightGray);
            lstBrushes.Add(Brushes.MidnightBlue);
            lstBrushes.Add(Brushes.Ivory);
            lstBrushes.Add(Brushes.Olive);

            Random r = new Random();

            for (int i = 0; i < lstWalls.Count; i++)
            {

                Polygon poly = new Polygon()
                {

                    //Stroke = lstBrushes.ElementAt(r.Next(0, 10)),
                    //Stroke = lstBrushes.ElementAt(i),
                    StrokeThickness = 0,
                    Fill = ib,
                    //Fill = lstBrushes.ElementAt(r.Next(0, 10)),
                    Opacity = 1
                    
                };
                myGrid.Children.Add(poly);
            }
        }

       

    }
}
