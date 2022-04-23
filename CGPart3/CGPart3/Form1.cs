using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGPart3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initializeBitmap();
            pictureBoxMain.Image = Variables.bitmap;
            Variables.brushColor = labelBrushColor.BackColor;
        }

        public void initializeBitmap()
        {
            Bitmap bmp = new Bitmap(600, 800, PixelFormat.Format32bppArgb);
            BitmapData bits = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);
            unsafe
            {
                for (int i = 0; i < 800; i++)
                {
                    uint* row = (uint*)((byte*)bits.Scan0 + (i * bits.Stride));

                    for (int j = 0; j < 600; j++)
                    {
                        row[j] = 0xFFFFFFFF;
                        Variables.bitmapArray[i, j] = 0xFFFFFFFF;
                        Variables.shapesArray[i, j] = 0;
                    }
                }
            }
            bmp.UnlockBits(bits);
            Variables.bitmap = bmp;
        }

        public void updateBitmap()
        {
            Variables.bitmap.Dispose();
            Bitmap bmp = new Bitmap(600, 800, PixelFormat.Format32bppArgb);
            BitmapData bits = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);
            unsafe
            {
                for (int i = 0; i < 800; i++)
                {
                    uint* row = (uint*)((byte*)bits.Scan0 + (i * bits.Stride));

                    for (int j = 0; j < 600; j++)
                        row[j] = Variables.bitmapArray[i, j];
                }
            }
            bmp.UnlockBits(bits);
            Variables.bitmap = bmp;
            pictureBoxMain.Image = Variables.bitmap;
        }

        public bool isClosePoint(Point mouse, Point point)
        {
            int diffX, diffY;
            if (point.X > mouse.X)
                diffX = point.X - mouse.X;
            else
                diffX = mouse.X - point.X;
            if (point.Y > mouse.Y)
                diffY = point.Y - mouse.Y;
            else
                diffY = mouse.Y - point.Y;

            if (diffX < 4 && diffY < 4 && diffX + diffY < 5)
                return true;
            else
                return false;
        }

        private void mode_CheckedChanged(object sender, EventArgs e)
        {
            Variables.modeName = ((RadioButton)sender).Name;
            Variables.isActive = false;
        }

        private void numericUpDownLineThickness_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownLineThickness.Value % 2 == 0)
                numericUpDownLineThickness.Value -= 1;
        }

        private void buttonBrushColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                labelBrushColor.BackColor = colorDialog.Color;
                Variables.brushColor = colorDialog.Color;
            }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxMain_MouseClick(object sender, MouseEventArgs e)
        {
            switch (Variables.modeName)
            {
                case "radioButtonAddThinLine":
                    if (Variables.isActive)
                    {
                        ((Line)Variables.activeShape).end2 = e.Location;
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                    }
                    else
                    {
                        Variables.activeShape = new Line(e.Location);
                        Variables.isActive = true;
                    }
                    break;

                case "radioButtonAddThickLine":
                    if (Variables.isActive)
                    {
                        ((Line)Variables.activeShape).end2 = e.Location;
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                    }
                    else
                    {
                        Variables.activeShape = new Line(e.Location, 
                            (int)numericUpDownLineThickness.Value);
                        Variables.isActive = true;
                    }
                    break;

                case "radioButtonAddCircle":
                    if (Variables.isActive)
                    {
                        ((Circle)Variables.activeShape).addRadius(e.Location);
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                    }
                    else
                    {
                        Variables.activeShape = new Circle(e.Location);
                        Variables.isActive = true;
                    }
                    break;

                case "radioButtonAddPolygon":
                    if (Variables.isActive)
                    {
                        if (((Polygon)Variables.activeShape).calcDistance(e.Location) < 5)
                        {
                            Variables.shapes.Add(Variables.activeShape);
                            Variables.isActive = false;
                        }
                        else
                            ((Polygon)Variables.activeShape).addVertex(e.Location);
                    }
                    else
                    {
                        Variables.activeShape = new Polygon(e.Location);
                        Variables.isActive = true;
                    }
                    break;

                case "radioButtonBrush":
                    int brushShapeID = Variables.shapesArray[e.Location.X, e.Y];
                    Shape brushShape = Variables.shapes.Find(x => x.ID == brushShapeID);
                    brushShape.color = Variables.brushColor;
                    break;

                case "radioButtonErase":
                    int eraseShapeID = Variables.shapesArray[e.X, e.Y];
                    Shape eraseShape = Variables.shapes.Find(x => x.ID == eraseShapeID);
                    Variables.shapes.Remove(eraseShape);
                    break;

                default:
                    break;
            }
        }

        private void pictureBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            Variables.downPosition = e.Location;
            int shapeID;
            Shape shape;
            if (Variables.pointsArray[e.X, e.Y] != 0)
            {
                switch (Variables.modeName)
                {
                    case "radioButtonResizeCircle":
                        shapeID = Variables.shapesArray[e.X, e.Y];
                        shape = Variables.shapes.Find(x => x.ID == shapeID);

                        if (shape.GetType().Equals(typeof(Circle)))
                        {
                            Variables.activeShape = shape;
                            Variables.shapes.Remove(shape);
                            Variables.isActive = true;
                        }
                        break;

                    case "radioButtonMovePoint":
                        shapeID = Variables.pointsArray[e.X, e.Y];
                        shape = Variables.shapes.Find(x => x.ID == shapeID);

                        if (shape.GetType().Equals(typeof(Circle)))
                        {
                            Variables.activeElement = 1;
                        }
                        else if (shape.GetType().Equals(typeof(Polygon)))
                        {
                            for (int i = 0; i < ((Polygon)shape).vertices.Count; i++)
                            {
                                if (isClosePoint(e.Location, ((Polygon)shape).vertices[i]))
                                {
                                    Variables.activeElement = i;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (isClosePoint(e.Location, ((Line)shape).end1))
                                Variables.activeElement = 1;
                            else
                                Variables.activeElement = 2;
                        }

                        Variables.activeShape = shape;
                        Variables.shapes.Remove(shape);
                        Variables.isActive = true;
                        break;

                    case "radioButtonMovePolygon":
                        shapeID = Variables.shapesArray[e.X, e.Y];
                        shape = Variables.shapes.Find(x => x.ID == shapeID);

                        if (shape.GetType().Equals(typeof(Polygon)))
                        {
                            Variables.activeShape = shape;
                            Variables.shapes.Remove(shape);
                            Variables.isActive = true;
                        }
                        break;

                    case "radioButtonMoveLine":
                        shapeID = Variables.shapesArray[e.X, e.Y];
                        shape = Variables.shapes.Find(x => x.ID == shapeID);

                        if (shape.GetType().Equals(typeof(Polygon)))
                        {
                            // line recognition - on shapesArray?
                            // Variables.activeElement = i;
                        }
                        else if (shape.GetType().Equals(typeof(Line)))
                        {
                            Variables.activeShape = shape;
                            Variables.shapes.Remove(shape);
                            Variables.isActive = true;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private void pictureBoxMain_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pictureBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            // labelBrush.Text = e.X + " " + e.Y;
        }

        private void pictureBoxMain_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }

    public abstract class Shape
    {
        public int ID;
        public Color color;
        // drawing?
    }

    public class Line : Shape
    {
        public Point end1;
        public Point end2;
        public int thickness;

        public Line(Point newEnd, int newThickness = 1)
        {
            ID = Variables.lastID + 1;
            Variables.lastID = ID;
            color = Variables.brushColor;

            end1 = newEnd;
            end2 = new Point(-10000, -10000);
            thickness = newThickness;
        }
    }

    public class Circle : Shape
    {
        public Point center;
        public int radius;

        public Circle(Point newCenter, int newRadius = -10000)
        {
            ID = Variables.lastID + 1;
            Variables.lastID = ID;
            color = Variables.brushColor;

            center = newCenter;
            radius = newRadius;
        }

        public Circle(Point newCenter, Point radiusPoint)
        {
            ID = Variables.lastID + 1;
            Variables.lastID = ID;
            color = Variables.brushColor;

            center = newCenter;
            radius = (int)Math.Sqrt((newCenter.X - radiusPoint.X) * (newCenter.X - radiusPoint.X) 
                + (newCenter.Y - radiusPoint.Y) * (newCenter.Y - radiusPoint.Y));
        }

        public void addRadius(Point radiusPoint)
        {
            radius = (int)Math.Sqrt((center.X - radiusPoint.X) * (center.X - radiusPoint.X)
                + (center.Y - radiusPoint.Y) * (center.Y - radiusPoint.Y));
        }
    }

    public class Polygon : Shape
    {
        public List<Point> vertices;

        public Polygon(Point newPoint)
        {
            ID = Variables.lastID + 1;
            Variables.lastID = ID;
            color = Variables.brushColor;

            vertices.Add(newPoint);
        }

        public double calcDistance(Point position)
        {
            if (vertices.Count == 0)
                return 10000;
            else
                return Math.Sqrt((vertices[0].X - position.X) * (vertices[0].X - position.X) 
                    + (vertices[0].Y - position.Y) * (vertices[0].Y - position.Y));
        }

        public void addVertex(Point newPoint)
        {
            vertices.Add(newPoint);
        }
    }

    public static class Variables
    {
        public static Bitmap bitmap;
        //public static Point mousePosition;
        public static Point downPosition;
        public static int activeElement;
        public static List<Shape> shapes;
        public static Shape activeShape;
        public static bool isActive = false;
        public static int lastID = 0;
        public static Color brushColor = Color.Black;
        public static string modeName = "radioButtonAddThinLine";
        public static uint[,] bitmapArray = new uint[800, 600];
        public static int[,] shapesArray = new int[800, 600];
        public static int[,] pointsArray = new int[800, 600];
        public static bool[,] pointsKernel = new bool[7, 7]
        {
            { false, false, true, true, true, false, false },
            { false, true, true, true, true, true, false },
            { true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true },
            { false, true, true, true, true, true, false },
            { false, false, true, true, true, false, false }
        };
    }
}

//switch (Variables.modeName)
//{
//    case "radioButtonAddThinLine":

//        break;

//    case "radioButtonAddThickLine":

//        break;

//    case "radioButtonAddCircle":

//        break;

//    case "radioButtonAddPolygon":

//        break;

//    case "radioButtonResizeCircle":

//        break;

//    case "radioButtonMovePoint":

//        break;

//    case "radioButtonMovePolygon":

//        break;

//    case "radioButtonMoveLine":

//        break;

//    case "radioButtonBrush":

//        break;

//    case "radioButtonErase":

//        break;
//}