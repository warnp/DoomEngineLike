using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Doom.Model
{
    class Wall : Objects
    {
        public Line DrawingLine { get; set; }
        public Point CenterOfRotation { get; set; }

        public string name { get; set; }

        public Point Edge1Dist
        {
            get
            {
                Point p = new Point(DrawingLine.X1 - CenterOfRotation.X, DrawingLine.Y1 - CenterOfRotation.Y);
                return p;
            }

        }

        public Point Edge2Dist
        {
            get
            {
                Point p = new Point(DrawingLine.X2 - CenterOfRotation.X, DrawingLine.Y2 - CenterOfRotation.Y);
                return p;
            }

        }

        public double GetDistanceExt1
        {
            get
            {
                return Math.Sqrt(Math.Pow(DrawingLine.X1 - CenterOfRotation.X, 2) + Math.Pow(DrawingLine.Y1 - CenterOfRotation.Y, 2));
            }
        }
        public double GetDistanceExt2
        {
            get
            {
                return Math.Sqrt(Math.Pow(DrawingLine.X2 - CenterOfRotation.X, 2) + Math.Pow(DrawingLine.Y2 - CenterOfRotation.Y, 2));
            }
        }

        public double DistanceOrder
        {
            get
            {
                Point lineCenter = new Point();
                lineCenter.X = (DrawingLine.X1 + DrawingLine.X2) / 2.0d;
                lineCenter.Y = (DrawingLine.Y1 + DrawingLine.Y2) / 2.0d;

                double a = Math.Pow(lineCenter.X - CenterOfRotation.X, 2);
                double b = Math.Pow(lineCenter.Y - CenterOfRotation.Y, 2);

                double result = Math.Sqrt(a + b);
                return result;
            }
        }

        public double GetAngleSin1
        {
            get
            {
                return -(DrawingLine.Y1 - CenterOfRotation.Y) / GetDistanceExt1;

            }
        }
        public double GetAngleSin2
        {
            get
            {
                return -(DrawingLine.Y2 - CenterOfRotation.Y) / GetDistanceExt2;

            }
        }

        public double GetAngleCos1
        {
            get
            {
                return (DrawingLine.X1 - CenterOfRotation.X) / GetDistanceExt1;

            }
        }
        public double GetAngleCos2
        {
            get
            {
                return (DrawingLine.X2 - CenterOfRotation.X) / GetDistanceExt2;

            }
        }


        public Wall(Line line)
        {
            this.DrawingLine = line;
            CenterOfRotation = new Point() { X = 500, Y = 500 };

        }

        /// <summary>
        /// angle en radian
        /// </summary>
        /// <param name="Angle"></param>
        public void Rotation(float Angle)
        {
            //Calculate position from center
            double X1 = DrawingLine.X1 - CenterOfRotation.X;
            double X2 = DrawingLine.X2 - CenterOfRotation.X;
            double Y1 = DrawingLine.Y1 - CenterOfRotation.Y;
            double Y2 = DrawingLine.Y2 - CenterOfRotation.Y;


            DrawingLine.X1 = (X1 * Math.Cos(Angle) - Y1 * Math.Sin(Angle)) + CenterOfRotation.X;
            DrawingLine.Y1 = (X1 * Math.Sin(Angle) + Y1 * Math.Cos(Angle)) + CenterOfRotation.Y;

            DrawingLine.X2 = (X2 * Math.Cos(Angle) - Y2 * Math.Sin(Angle)) + CenterOfRotation.X;
            DrawingLine.Y2 = (X2 * Math.Sin(Angle) + Y2 * Math.Cos(Angle)) + CenterOfRotation.Y;
        }

        public void Translate(float x, float y)
        {
            DrawingLine.X1 += x;
            DrawingLine.Y1 += y;
            DrawingLine.X2 += x;
            DrawingLine.Y2 += y;
        }


    }
}
