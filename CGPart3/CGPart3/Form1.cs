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
        }

        public void initializeBitmap()
        {
            Bitmap bmp = new Bitmap(Variables.bitmapWidth, Variables.bitmapHeight, PixelFormat.Format32bppArgb);
            BitmapData bits = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);
            unsafe
            {
                for (int i = 0; i < Variables.bitmapHeight; i++)
                {
                    int* row = (int*)((byte*)bits.Scan0 + (i * bits.Stride));

                    for (int j = 0; j < Variables.bitmapWidth; j++)
                    {
                        row[j] = -1;
                        Variables.bitmapArray[j, i] = -1;
                        Variables.shapesArray[j, i] = 0;
                        Variables.pointsArray[j, i] = 0;
                    }
                }
            }
            bmp.UnlockBits(bits);
            Variables.bitmap = bmp;
            pictureBoxMain.Image = Variables.bitmap;
        }

        public void updateBitmap()
        {
            Variables.bitmap.Dispose();
            Bitmap bmp = new Bitmap(Variables.bitmapWidth, Variables.bitmapHeight, PixelFormat.Format32bppArgb);
            BitmapData bits = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);
            unsafe
            {
                for (int i = 0; i < Variables.bitmapHeight; i++)
                {
                    for (int j = 0; j < Variables.bitmapWidth; j++)
                    {
                        Variables.bitmapArray[j, i] = -1;
                    }
                }
                for (int i = 0; i < Variables.shapes.Count; i++)
                {
                    if (Variables.shapes[i].GetType().Equals(typeof(Line)))
                    {
                        addPoint(((Line)Variables.shapes[i]).end1, Variables.shapes[i].ID);
                        addPoint(((Line)Variables.shapes[i]).end2, Variables.shapes[i].ID);
                        addLine((Line)Variables.shapes[i]);
                    }
                    else if (Variables.shapes[i].Equals(typeof(Circle)))
                    {

                    }
                    else // Polygon
                    {

                    }
                }
                // active shape
                if (Variables.isPointMode)
                {

                }

                for (int i = 0; i < Variables.bitmapHeight; i++)
                {
                    int* row = (int*)((byte*)bits.Scan0 + (i * bits.Stride));

                    for (int j = 0; j < Variables.bitmapWidth; j++)
                        row[j] = Variables.bitmapArray[j, i];
                }
            }
            bmp.UnlockBits(bits);
            Variables.bitmap = bmp;
            pictureBoxMain.Image = Variables.bitmap;
        }

        public unsafe bool isClosePoint(Point mouse, Point point)
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

        public void addPoint(Point point, int ID)
        {
            for (int i = Math.Max(point.X - 3, 0); i <= Math.Min(point.X + 3, Variables.bitmapWidth - 1); i++)
            {
                for (int j = Math.Max(point.Y - 3, 0); j <= Math.Min(point.Y + 3, Variables.bitmapHeight - 1); j++)
                {
                    if (Variables.pointsKernel[i - point.X + 3, j - point.Y + 3])
                    {
                        Variables.pointsArray[i, j] = ID;
                    }
                }
            }
        }

        public void drawPoint(Point point, int ID)
        {
            for (int i = Math.Max(point.X - 3, 0); i <= Math.Min(point.X + 3, Variables.bitmapWidth - 1); i++)
            {
                for (int j = Math.Max(point.Y - 3, 0); j <= Math.Min(point.Y + 3, Variables.bitmapHeight - 1); j++)
                {
                    if (Variables.pointsKernel[i - point.X + 3, j - point.Y + 3])
                    {
                        Variables.bitmapArray[i, j] = Color.Red.ToArgb();
                        Variables.pointsArray[i, j] = ID;
                    }
                }
            }
        }

        public void addLine(Line line)
        {
            for (int i = line.end1.X; i < line.end2.X; i++)
            {
                Variables.bitmapArray[i, line.end1.Y] = line.color;
                Variables.shapesArray[i, line.end1.Y] = line.ID;
            }
        }

        public void addCircle(Circle circle)
        {

        }

        private void mode_CheckedChanged(object sender, EventArgs e)
        {
            Variables.modeName = ((RadioButton)sender).Name;
            Variables.isActive = false;
            switch (Variables.modeName)
            {
                case "radioButtonAddThinLine":
                case "radioButtonAddThickLine":
                case "radioButtonAddCircle":
                case "radioButtonAddPolygon":
                case "radioButtonResizeCircle":
                case "radioButtonMovePolygon":
                case "radioButtonMoveLine":
                case "radioButtonBrush":
                case "radioButtonErase":
                    if (Variables.isPointMode)
                    {
                        Variables.isPointMode = false;
                    }
                    break;

                case "radioButtonMovePoint":
                    if (!Variables.isPointMode)
                    {
                        Variables.isPointMode = true;
                    }
                    break;
            }
            updateBitmap();
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
                Variables.brushColor = colorDialog.Color.ToArgb();
            }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            Variables.shapes.Clear();
            Variables.isActive = false;
            Variables.lastID = 0;
            Variables.bitmap.Dispose();
            initializeBitmap();
        }

        private void pictureBoxMain_MouseClick(object sender, MouseEventArgs e)
        {
            switch (Variables.modeName)
            {
                case "radioButtonAddThinLine":
                    if (Variables.isActive)
                    {
                        ((Line)Variables.activeShape).end2 = e.Location;
                        addPoint(((Line)Variables.activeShape).end1, Variables.activeShape.ID);
                        addPoint(((Line)Variables.activeShape).end1, Variables.activeShape.ID);
                        Variables.shapes.Add(Variables.activeShape);
                        addLine((Line)Variables.activeShape);
                        Variables.isActive = false;
                        updateBitmap();
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
                        
                    }
                    else
                    {
                        
                    }
                    break;

                case "radioButtonAddCircle":
                    if (Variables.isActive)
                    {
                        ((Circle)Variables.activeShape).addRadius(e.Location);
                        addPoint(((Circle)Variables.activeShape).center, Variables.activeShape.ID);
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                        updateBitmap();
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
                    int brushShapeID = Variables.shapesArray[e.X, e.Y];
                    Shape brushShape = Variables.shapes.Find(x => x.ID == brushShapeID);
                    if (brushShape is not null)
                    {
                        // labelMode.Text = $"{brushShape.ID}";
                        brushShape.color = Variables.brushColor;
                        // Variables.shapes.Remove(brushShape);
                        // Variables.shapes.Add(brushShape);
                        updateBitmap();
                    }
                    break;

                case "radioButtonErase":
                    int eraseShapeID = Variables.shapesArray[e.X, e.Y];
                    Shape eraseShape = Variables.shapes.Find(x => x.ID == eraseShapeID);
                    if (eraseShape is not null)
                    {
                        // labelMode.Text = $"{eraseShape.ID}";
                        Variables.shapes.Remove(eraseShape);
                        updateBitmap();
                    }
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
                            for (int i = 0; i < ((Polygon)shape).vertices.Count; i++)
                            {
                                if (isClosePoint(e.Location, ((Polygon)shape).vertices[i]))
                                {
                                    Variables.activeShape = shape;
                                    Variables.activeElement = i;
                                    Variables.shapes.Remove(shape);
                                    Variables.isActive = true;
                                    break;
                                }
                            }
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
        }

        private void pictureBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (Variables.isActive)
            {
                int xShift = e.X - Variables.downPosition.X;
                int yShift = e.Y - Variables.downPosition.Y;
                Shape shapeCopy = Variables.activeShape.Clone();
                
                switch (Variables.modeName)
                {
                    case "radioButtonAddThinLine":
                        ((Line)shapeCopy).end2 = e.Location;
                        // draw
                        break;

                    case "radioButtonAddThickLine":
                        ((Line)shapeCopy).end2 = e.Location;
                        ((Line)shapeCopy).thickness = (int)numericUpDownLineThickness.Value;
                        // draw
                        break;

                    case "radioButtonAddCircle":
                        ((Circle)shapeCopy).addRadius(e.Location);
                        // draw
                        break;

                    case "radioButtonAddPolygon":
                        ((Polygon)shapeCopy).vertices.Add(e.Location);
                        // draw
                        // check if open
                        break;

                    case "radioButtonResizeCircle":
                        ((Circle)shapeCopy).addRadius(e.Location);
                        // draw
                        break;

                    case "radioButtonMovePoint":
                        if (shapeCopy.GetType().Equals(typeof(Line)))
                        {

                        }
                        else if (shapeCopy.GetType().Equals(typeof(Circle)))
                        {

                        }
                        else // Polygon
                        {

                        }
                        break;

                    case "radioButtonMovePolygon":

                        break;

                    case "radioButtonMoveLine":
                        if (shapeCopy.GetType().Equals(typeof(Line)))
                        {

                        }
                        else if (shapeCopy.GetType().Equals(typeof(Polygon)))
                        {

                        }
                        break;

                    case "radioButtonBrush":
                        shapeCopy.color = Variables.brushColor;
                        //draw
                        break;

                    case "radioButtonErase":

                        break;

                    default:
                        break;
                }
            }
            else
            {
                //labelMode.Text = $"{e.Location.X}, {e.Location.Y}";
            }
        }

        private void pictureBoxMain_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }

    //public class CustomPoint
    //{
    //    public Point point;
    //    public int ownerID;

    //    public CustomPoint(Point newPoint, Shape shape)
    //    {
    //        point = new Point(newPoint.X, newPoint.Y);
    //        ownerID = shape.ID;
    //    }
    //}

    public abstract class Shape
    {
        public int ID;
        public int color;

        public Shape Clone()
        {
            return (Shape)this.MemberwiseClone();
        }
        // drawing?
        public abstract void drawPoints();
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

        public override void drawPoints()
        {

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

        public override void drawPoints()
        {

        }
    }

    public class Polygon : Shape
    {
        public List<Point> vertices;
        public bool isOpen;

        public Polygon(Point newPoint)
        {
            ID = Variables.lastID + 1;
            Variables.lastID = ID;
            color = Variables.brushColor;
            isOpen = true;

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
            //check if close!
        }

        public override void drawPoints()
        {

        }
    }

    public static class Variables
    {
        public static Bitmap bitmap;
        //public static Point mousePosition;
        public static Point downPosition;
        public static int activeElement;
        public static List<Shape> shapes = new List<Shape>();
        // public static List<CustomPoint> points = new List<CustomPoint>();
        public static Shape activeShape;
        public static bool isActive = false;
        public static bool isPointMode = false;
        public static int lastID = 0;
        public static int brushColor = -16777216; // black
        public static string modeName = "radioButtonAddThinLine";
        public static int bitmapHeight = 800;
        public static int bitmapWidth = 600;
        public static int[,] bitmapArray = new int[bitmapWidth, bitmapHeight];
        public static int[,] shapesArray = new int[bitmapWidth, bitmapHeight];
        public static int[,] pointsArray = new int[bitmapWidth, bitmapHeight];
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