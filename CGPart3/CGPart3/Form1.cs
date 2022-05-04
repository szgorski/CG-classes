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
            InitializeBitmap();
            //labelMode.Text = $"{Color.Red.ToArgb()}";
        }

        // MATH FUNCTIONS
        public unsafe static int FastMin(int a, int b)
        {
            unsafe
            {
                if (a > b) return b;
                else return a;
            }
        }

        public unsafe static int FastMax(int a, int b)
        {
            unsafe
            {
                if (a > b) return a;
                else return b;
            }
        }

        // CORRECT THIS FUNCTION!
        public unsafe static int FastRound(float number)
        {
            int floor = (int)number;
            if (number - floor > 0.5)
                return floor + 1;
            else
                return floor;
        }

        public unsafe static int FastAbs(int a)
        {
            if (a < 0)
                return -a;
            else
                return a;
        }

        public unsafe static bool IsClosePoint(Point mouse, Point point)
        {
            int diffX = FastAbs(mouse.X - point.X);
            int diffY = FastAbs(mouse.Y - point.Y);
            if (diffX < 4 && diffY < 4 && diffX + diffY < 5)
                return true;
            else
                return false;
        }

        public unsafe static Point CleanLocation(Point mouse)
        {
            if (mouse.X < 0) mouse.X = 0;
            else if (mouse.X >= Variables.bitmapWidth) 
                mouse.X = Variables.bitmapWidth - 1;

            if (mouse.Y < 0) mouse.Y = 0;
            else if (mouse.Y >= Variables.bitmapHeight) 
                mouse.Y = Variables.bitmapHeight - 1;

            return mouse;
        }

        // DRAWING FUNCTIONS
        //          THIN LINE
        public static void MidpointHorizontalLine(int x1, int y1, int x2, int y2, int ID, uint colour)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int ys = 1;
            if (dy < 0)
            {
                ys = -1;
                dy = -dy;
            }
            int d = 2 * dy - dx;
            int dI = 2 * dy;            // identity axis
            int dD = 2 * (dy - dx);     // diagonal axis
            int x = x1;
            int y = y1;
            Variables.bitmapArray[x, y] = colour;
            Variables.shapesArray[x, y] = ID;
            while (x < x2)
            {
                if (d < 0)
                {
                    d += dI;
                    x++;
                }
                else
                {
                    d += dD;
                    y += ys;
                    x++;
                }
                Variables.bitmapArray[x, y] = colour;
                Variables.shapesArray[x, y] = ID;
            }
        }

        public static void MidpointVerticalLine(int x1, int y1, int x2, int y2, int ID, uint colour)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int xs = 1;
            if (dx < 0)
            {
                xs = -1;
                dx = -dx;
            }
            int d = 2 * dx - dy;
            int dI = 2 * dx;            // identity axis
            int dD = 2 * (dx - dy);     // diagonal axis
            int x = x1;
            int y = y1;
            Variables.bitmapArray[x, y] = colour;
            Variables.shapesArray[x, y] = ID;
            while (y < y2)
            {
                if (d < 0)
                {
                    d += dI;
                    y++;
                }
                else
                {
                    d += dD;
                    x += xs;
                    y++;
                }
                Variables.bitmapArray[x, y] = colour;
                Variables.shapesArray[x, y] = ID;
            }
        }

        public static void MidpointLine(Point point1, Point point2, int ID, uint colour)
        {
            if (FastAbs(point2.X - point1.X) > FastAbs(point2.Y - point1.Y))
            {
                if (point2.X > point1.X)
                    MidpointHorizontalLine(point1.X, point1.Y, point2.X, point2.Y, ID, colour);
                else
                    MidpointHorizontalLine(point2.X, point2.Y, point1.X, point1.Y, ID, colour);
            }
            else
            {
                if (point2.Y > point1.Y)
                    MidpointVerticalLine(point1.X, point1.Y, point2.X, point2.Y, ID, colour);
                else
                    MidpointVerticalLine(point2.X, point2.Y, point1.X, point1.Y, ID, colour);
            }
        }

        //          THICK LINE
        public static bool[,] CreateBrush(int diagonal)
        {   // assuming diagonal is odd
            int radius = diagonal / 2;
            bool[,] circle = new bool[diagonal, diagonal];

            //Variables.valueCopy = new int[diagonal, diagonal];
            //Variables.floatCopy = new float[diagonal, diagonal];

            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    //Variables.valueCopy[radius + i, radius + j] = FastRound((float)Math.Sqrt(i * i + j * j));
                    //Variables.floatCopy[radius + i, radius + j] = (float)Math.Sqrt(i * i + j * j);
                    if (FastRound((float)Math.Sqrt(i * i + j * j)) <= radius)
                        circle[radius + i, radius + j] = true;
                }
            }
            return circle;
        }

        public static void ApplyBrush(int x, int y, bool[,] brush, int ID, uint colour, int thickness)
        {
            int radius = thickness / 2;
            for (int i = FastMax(x - radius, 0); i <= FastMin(x + radius, Variables.bitmapWidth - 1); i++)
            {
                for (int j = FastMax(y - radius, 0); j <= FastMin(y + radius, Variables.bitmapHeight - 1); j++)
                {
                    if (brush[i - x + radius, j - y + radius])
                    {
                        Variables.bitmapArray[i, j] = colour;
                        Variables.shapesArray[i, j] = ID;
                    }
                }
            }
        }

        public static void BrushHorizontalLine(int x1, int y1, int x2, int y2, int ID, uint colour, int thickness)
        {
            bool[,] brush = CreateBrush(thickness);
            int dx = x2 - x1;
            int dy = y2 - y1;
            int ys = 1;
            if (dy < 0)
            {
                ys = -1;
                dy = -dy;
            }
            int d = 2 * dy - dx;
            int dI = 2 * dy;            // identity axis
            int dD = 2 * (dy - dx);     // diagonal axis
            int x = x1;
            int y = y1;
            ApplyBrush(x, y, brush, ID, colour, thickness);
            while (x < x2)
            {
                if (d < 0)
                {
                    d += dI;
                    x++;
                }
                else
                {
                    d += dD;
                    y += ys;
                    x++;
                }
                ApplyBrush(x, y, brush, ID, colour, thickness);
            }
        }

        public static void BrushVerticalLine(int x1, int y1, int x2, int y2, int ID, uint colour, int thickness)
        {
            bool[,] brush = CreateBrush(thickness);
            int dx = x2 - x1;
            int dy = y2 - y1;
            int xs = 1;
            if (dx < 0)
            {
                xs = -1;
                dx = -dx;
            }
            int d = 2 * dx - dy;
            int dI = 2 * dx;            // identity axis
            int dD = 2 * (dx - dy);     // diagonal axis
            int x = x1;
            int y = y1;
            ApplyBrush(x, y, brush, ID, colour, thickness);
            while (y < y2)
            {
                if (d < 0)
                {
                    d += dI;
                    y++;
                }
                else
                {
                    d += dD;
                    x += xs;
                    y++;
                }
                ApplyBrush(x, y, brush, ID, colour, thickness);
            }
        }

        public static void BrushLine(Point point1, Point point2, int ID, uint colour, int thickness)
        {
            if (FastAbs(point2.X - point1.X) > FastAbs(point2.Y - point1.Y))
            {
                if (point2.X > point1.X)
                    BrushHorizontalLine(point1.X, point1.Y, point2.X, point2.Y, ID, colour, thickness);
                else
                    BrushHorizontalLine(point2.X, point2.Y, point1.X, point1.Y, ID, colour, thickness);
            }
            else
            {
                if (point2.Y > point1.Y)
                    BrushVerticalLine(point1.X, point1.Y, point2.X, point2.Y, ID, colour, thickness);
                else
                    BrushVerticalLine(point2.X, point2.Y, point1.X, point1.Y, ID, colour, thickness);
            }
        }

        //          CIRCLE
        public static void MidpointCircle(Point point, int radius, int ID, uint colour)
        {
            int x = point.X;
            int y = point.Y;
            int d = 1 - radius;
            int dI = 3;                  // identity axis
            int dD = 5 - 2 * radius;     // diagonal axis
            int xc = 0;         // relative circumference position
            int yc = radius;    // relative circumference position

            bool caseA, caseB, caseC, caseD, caseE, caseF, caseG, caseH;
            caseA = false; if (x + xc >= 0 && x + xc < Variables.bitmapWidth) caseA = true;
            caseB = false; if (x - xc >= 0 && x - xc < Variables.bitmapWidth) caseB = true;
            caseC = false; if (x + yc >= 0 && x + yc < Variables.bitmapWidth) caseC = true;
            caseD = false; if (x - yc >= 0 && x - yc < Variables.bitmapWidth) caseD = true;
            caseE = false; if (y + xc >= 0 && y + xc < Variables.bitmapHeight) caseE = true;
            caseF = false; if (y - xc >= 0 && y - xc < Variables.bitmapHeight) caseF = true;
            caseG = false; if (y + yc >= 0 && y + yc < Variables.bitmapHeight) caseG = true;
            caseH = false; if (y - yc >= 0 && y - yc < Variables.bitmapHeight) caseH = true;

            if (caseA && caseG)
            {
                Variables.bitmapArray[x + xc, y + yc] = colour;
                Variables.shapesArray[x + xc, y + yc] = ID;
            }
            if (caseA && caseH)
            {
                Variables.bitmapArray[x + xc, y - yc] = colour;
                Variables.shapesArray[x + xc, y - yc] = ID;
            }
            if (caseB && caseG)
            {
                Variables.bitmapArray[x - xc, y + yc] = colour;
                Variables.shapesArray[x - xc, y + yc] = ID;
            }
            if (caseB && caseH)
            {
                Variables.bitmapArray[x - xc, y - yc] = colour;
                Variables.shapesArray[x - xc, y - yc] = ID;
            }
            if (caseC && caseE)
            {
                Variables.bitmapArray[x + yc, y + xc] = colour;
                Variables.shapesArray[x + yc, y + xc] = ID;
            }
            if (caseC && caseF)
            {
                Variables.bitmapArray[x + yc, y - xc] = colour;
                Variables.shapesArray[x + yc, y - xc] = ID;
            }
            if (caseD && caseE)
            {
                Variables.bitmapArray[x - yc, y + xc] = colour;
                Variables.shapesArray[x - yc, y + xc] = ID;
            }
            if (caseD && caseF)
            {
                Variables.bitmapArray[x - yc, y - xc] = colour;
                Variables.shapesArray[x - yc, y - xc] = ID;
            }
            
            while (yc > xc)
            {
                if (d < 0) //move to E
                {
                    d += dI;
                    dI += 2;
                    dD += 2;
                }
                else //move to SE
                {
                    d += dD;
                    dI += 2;
                    dD += 4;
                    yc--;
                }
                xc++;

                caseA = false; if (x + xc >= 0 && x + xc < Variables.bitmapWidth) caseA = true;
                caseB = false; if (x - xc >= 0 && x - xc < Variables.bitmapWidth) caseB = true;
                caseC = false; if (x + yc >= 0 && x + yc < Variables.bitmapWidth) caseC = true;
                caseD = false; if (x - yc >= 0 && x - yc < Variables.bitmapWidth) caseD = true;
                caseE = false; if (y + xc >= 0 && y + xc < Variables.bitmapHeight) caseE = true;
                caseF = false; if (y - xc >= 0 && y - xc < Variables.bitmapHeight) caseF = true;
                caseG = false; if (y + yc >= 0 && y + yc < Variables.bitmapHeight) caseG = true;
                caseH = false; if (y - yc >= 0 && y - yc < Variables.bitmapHeight) caseH = true;

                if (caseA && caseG)
                {
                    Variables.bitmapArray[x + xc, y + yc] = colour;
                    Variables.shapesArray[x + xc, y + yc] = ID;
                }
                if (caseA && caseH)
                {
                    Variables.bitmapArray[x + xc, y - yc] = colour;
                    Variables.shapesArray[x + xc, y - yc] = ID;
                }
                if (caseB && caseG)
                {
                    Variables.bitmapArray[x - xc, y + yc] = colour;
                    Variables.shapesArray[x - xc, y + yc] = ID;
                }
                if (caseB && caseH)
                {
                    Variables.bitmapArray[x - xc, y - yc] = colour;
                    Variables.shapesArray[x - xc, y - yc] = ID;
                }
                if (caseC && caseE)
                {
                    Variables.bitmapArray[x + yc, y + xc] = colour;
                    Variables.shapesArray[x + yc, y + xc] = ID;
                }
                if (caseC && caseF)
                {
                    Variables.bitmapArray[x + yc, y - xc] = colour;
                    Variables.shapesArray[x + yc, y - xc] = ID;
                }
                if (caseD && caseE)
                {
                    Variables.bitmapArray[x - yc, y + xc] = colour;
                    Variables.shapesArray[x - yc, y + xc] = ID;
                }
                if (caseD && caseF)
                {
                    Variables.bitmapArray[x - yc, y - xc] = colour;
                    Variables.shapesArray[x - yc, y - xc] = ID;
                }
            }
        }

        //          ANTI-ALIASING
        public static uint InterpolateColour(uint backColour, uint frontColour, float coverage)
        {   // assuming coverage belongs to [0, 1]
            uint backA = (backColour >> 24);
            uint backR = (backColour >> 16) - ((backColour >> 24) << 8);
            uint backG = (backColour >> 8) - ((backColour >> 16) << 8);
            uint backB = backColour - ((backColour >> 8) << 8);
            
            uint frontA = (frontColour >> 24);
            uint frontR = (frontColour >> 16) - ((frontColour >> 24) << 8);
            uint frontG = (frontColour >> 8) - ((frontColour >> 16) << 8);
            uint frontB = frontColour - ((frontColour >> 8) << 8);
            
            uint A = (uint)(coverage * frontA + (1 - coverage) * backA);
            uint R = (uint)(coverage * frontR + (1 - coverage) * backR);
            uint G = (uint)(coverage * frontG + (1 - coverage) * backG);
            uint B = (uint)(coverage * frontB + (1 - coverage) * backB);

            return (A << 24) + (R << 16) + (G << 8) + B;
        }

        public static float CoverageFunction(float distance)
        {
            if (distance < 0.5)
            {   // assuming radius is always 0.5
                float coverage = 0.31830988f * (float)Math.Acos(distance * 2f) - distance * 1.27323954f 
                    * (float)Math.Sqrt(0.25f - distance * distance);

                if (coverage > 1f)
                    return 1f;
                else if (coverage < 0f) 
                    return 0f;
                else 
                    return coverage;
            }
            else
                return 0f;
        }

        public static float CalculateLineCoverage(int thickness, float distance)
        {   // assuming all lines are at least one pixel thick
            if (0.5f * thickness < distance)
                return CoverageFunction(distance - 0.5f * thickness);
            else
                return 1f - CoverageFunction(0.5f * thickness - distance);

        }

        public static float AddPixelIntensity(int x, int y, int ID, uint colour, int thickness, float distance)
        {
            float coverage = CalculateLineCoverage(thickness, distance);
            if (coverage > 0)
            {
                Variables.bitmapArray[x, y] = InterpolateColour(Variables.bitmapArray[x, y], colour, coverage);
                Variables.shapesArray[x, y] = ID;
            }
            return coverage;
        }

        public static void AntialiasedHorizontalLine(int x1, int y1, int x2, int y2, int ID, uint colour, int thickness)
        {
            int radius = thickness / 2;
            int dx = x2 - x1;
            int dy = y2 - y1;
            int xs = 1;
            int ys = 1;
            if (dy < 0)
            {
                ys = -1;
                dy = -dy;
            }
            if (dx < 0)
                xs = -1;
            int d = 2 * dy - dx;
            int dI = 2 * dy;
            int dD = 2 * (dy - dx);
            int two_v_dx;                                               // numerator
            float invDenominator = 1f / (2f * (float)Math.Sqrt(dx * dx + dy * dy));
                                                                        // inverted denominator
            if (invDenominator < float.PositiveInfinity)
            {
                float two_dx_invDenominator = 2 * dx * invDenominator;
                int x = x1;
                int y = y1;
                AddPixelIntensity(x, y, ID, colour, thickness, 0);
                for (int i = 1; y + i < Variables.bitmapHeight && 
                    AddPixelIntensity(x, y + i, ID, colour, thickness, i * two_dx_invDenominator) > 0; ++i);
                for (int i = 1; y - i >= 0 && 
                    AddPixelIntensity(x, y - i, ID, colour, thickness, i * two_dx_invDenominator) > 0; ++i);
                if (xs != ys)
                {
                    while (x < x2)
                    {
                        x++;
                        if (d < 0)
                        {
                            two_v_dx = d + dx;
                            d += dI;
                        }
                        else
                        {
                            two_v_dx = d - dx;
                            d += dD;
                            y += ys;
                        }
                        AddPixelIntensity(x, y, ID, colour, thickness, two_v_dx * invDenominator);
                        for (int i = 1; y + i < Variables.bitmapHeight && AddPixelIntensity(x, y + i, ID, colour, thickness,
                            i * two_dx_invDenominator + two_v_dx * invDenominator) > 0; ++i) ;
                        for (int i = 1; y - i >= 0 && AddPixelIntensity(x, y - i, ID, colour, thickness,
                            i * two_dx_invDenominator - two_v_dx * invDenominator) > 0; ++i) ;
                    }

                    //x = x2; - brush to array of thickness, change thickness dynamically
                    //while (x < Variables.bitmapWidth && x < x2 + radius)
                    //{
                    //    if (d < 0)
                    //    {
                    //        two_v_dx = d + dx;
                    //        d += dI;
                    //    }
                    //    else
                    //    {
                    //        two_v_dx = d - dx;
                    //        d += dD;
                    //        y += ys;
                    //    }
                    //    AddPixelIntensity(x, y, ID, colour, thickness, 
                    //        (float)Math.Sqrt(FastAbs(x - x2) * FastAbs(x - x2) + FastAbs(y - y2) * FastAbs(y - y2)));
                    //    for (int i = 1; y + i < Variables.bitmapHeight && AddPixelIntensity(x, y + i, ID, colour, thickness,
                    //        (float)Math.Sqrt(FastAbs(x - x2) * FastAbs(x - x2) + FastAbs(y + i - y2) * FastAbs(y + i - y2))) > 0; ++i) ;
                    //    for (int i = 1; y - i >= 0 && AddPixelIntensity(x, y - i, ID, colour, thickness,
                    //        (float)Math.Sqrt(FastAbs(x - x2) * FastAbs(x - x2) + FastAbs(y - i - y2) * FastAbs(y - i - y2))) > 0; ++i) ;
                    //    x++;
                    //}
                }
                else
                {
                    while (x < x2)
                    {
                        x++;
                        if (d < 0)
                        {
                            two_v_dx = d + dx;
                            d += dI;
                        }
                        else
                        {
                            two_v_dx = d - dx;
                            d += dD;
                            y += ys;
                        }
                        AddPixelIntensity(x, y, ID, colour, thickness, two_v_dx * invDenominator);
                        for (int i = 1; y + i < Variables.bitmapHeight && AddPixelIntensity(x, y + i, ID, colour, thickness,
                            i * two_dx_invDenominator - two_v_dx * invDenominator) > 0; ++i) ;
                        for (int i = 1; y - i >= 0 && AddPixelIntensity(x, y - i, ID, colour, thickness,
                            i * two_dx_invDenominator + two_v_dx * invDenominator) > 0; ++i) ;
                    }
                }

            }
        }

        public static void AntialiasedVerticalLine(int x1, int y1, int x2, int y2, int ID, uint colour, int thickness)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int xs = 1;
            int ys = 1;
            if (dx < 0)
            {
                xs = -1;
                dx = -dx;
            }
            if (dy < 0)
                ys = -1;
            int d = 2 * dx - dy;
            int dI = 2 * dx;
            int dD = 2 * (dx - dy);
            int two_v_dy;                                               // numerator
            float invDenominator = 1f / (2f * (float)Math.Sqrt(dx * dx + dy * dy));
                                                                        // inverted denominator
            if (invDenominator < float.PositiveInfinity)
            {
                float two_dy_invDenominator = 2 * dy * invDenominator;
                int x = x1;
                int y = y1;
                AddPixelIntensity(x, y, ID, colour, thickness, 0);
                for (int i = 1; x + i < Variables.bitmapWidth && 
                    AddPixelIntensity(x + i, y, ID, colour, thickness, i * two_dy_invDenominator) > 0; ++i) ;
                for (int i = 1; x - i >= 0 && 
                    AddPixelIntensity(x - i, y, ID, colour, thickness, i * two_dy_invDenominator) > 0; ++i) ;
                if (xs != ys)
                {
                    while (y < y2)
                    {
                        y++;
                        if (d < 0)
                        {
                            two_v_dy = d + dy;
                            d += dI;
                        }
                        else
                        {
                            two_v_dy = d - dy;
                            d += dD;
                            x += xs;
                        }
                        AddPixelIntensity(x, y, ID, colour, thickness, two_v_dy * invDenominator);
                        for (int i = 1; x + i < Variables.bitmapWidth && AddPixelIntensity(x + i, y, ID, colour, thickness,
                            i * two_dy_invDenominator + two_v_dy * invDenominator) > 0; ++i) ;
                        for (int i = 1; x - i >= 0 && AddPixelIntensity(x - i, y, ID, colour, thickness,
                            i * two_dy_invDenominator - two_v_dy * invDenominator) > 0; ++i) ;
                    }
                }
                else
                {
                    while (y < y2)
                    {
                        y++;
                        if (d < 0)
                        {
                            two_v_dy = d + dy;
                            d += dI;
                        }
                        else
                        {
                            two_v_dy = d - dy;
                            d += dD;
                            x += xs;
                        }
                        AddPixelIntensity(x, y, ID, colour, thickness, two_v_dy * invDenominator);
                        for (int i = 1; x + i < Variables.bitmapWidth && AddPixelIntensity(x + i, y, ID, colour, thickness,
                            i * two_dy_invDenominator - two_v_dy * invDenominator) > 0; ++i) ;
                        for (int i = 1; x - i >= 0 && AddPixelIntensity(x - i, y, ID, colour, thickness,
                            i * two_dy_invDenominator + two_v_dy * invDenominator) > 0; ++i) ;
                    }
                }
            }
        }

        public static void AntialiasedLine(Point point1, Point point2, int ID, uint colour, int thickness)
        {
            if (FastAbs(point2.X - point1.X) > FastAbs(point2.Y - point1.Y))
            {
                if (point2.X > point1.X)
                    AntialiasedHorizontalLine(point1.X, point1.Y, point2.X, point2.Y, ID, colour, thickness);
                else
                    AntialiasedHorizontalLine(point2.X, point2.Y, point1.X, point1.Y, ID, colour, thickness);
            }
            else
            {
                if (point2.Y > point1.Y)
                    AntialiasedVerticalLine(point1.X, point1.Y, point2.X, point2.Y, ID, colour, thickness);
                else
                    AntialiasedVerticalLine(point2.X, point2.Y, point1.X, point1.Y, ID, colour, thickness);
            }
        }

        //          POINT
        public static void AddPoint(Point point, int ID)
        {
            for (int i = FastMax(point.X - 3, 0); i <= FastMin(point.X + 3, Variables.bitmapWidth - 1); i++)
            {
                for (int j = FastMax(point.Y - 3, 0); j <= FastMin(point.Y + 3, Variables.bitmapHeight - 1); j++)
                {
                    if (Variables.pointsKernel[i - point.X + 3, j - point.Y + 3])
                    {
                        Variables.pointsArray[i, j] = ID;
                    }
                }
            }
        }

        public static void DrawPoint(Point point)
        {
            for (int i = FastMax(point.X - 3, 0); i <= FastMin(point.X + 3, Variables.bitmapWidth - 1); i++)
            {
                for (int j = FastMax(point.Y - 3, 0); j <= FastMin(point.Y + 3, Variables.bitmapHeight - 1); j++)
                {
                    if (Variables.pointsKernel[i - point.X + 3, j - point.Y + 3])
                    {
                        Variables.bitmapArray[i, j] = 4294901760;   // red
                    }
                }
            }
        }

        //          BITMAP
        public void InitializeBitmap()
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
                        Variables.bitmapArray[j, i] = 4294967295;   // white
                        Variables.shapesArray[j, i] = 0;
                        Variables.pointsArray[j, i] = 0;
                    }
                }
            }
            bmp.UnlockBits(bits);
            Variables.bitmap = bmp;
            pictureBoxMain.Image = Variables.bitmap;
        }

        public void UpdateBitmap()
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
                        Variables.bitmapArray[j, i] = 4294967295;   // white
                        Variables.shapesArray[j, i] = 0;
                        Variables.pointsArray[j, i] = 0;
                    }
                }

                for (int i = 0; i < Variables.shapes.Count; i++)
                {
                    Variables.shapes[i].DrawShape();
                }
                if (Variables.modeName == "radioButtonMovePoint")
                {
                    for (int i = 0; i < Variables.shapes.Count; i++)
                    {
                        Variables.shapes[i].DrawPoints();
                    }
                }
                if (Variables.isActive)
                {
                    Variables.activeShape.DrawActiveShape();
                }

                for (int i = 0; i < Variables.bitmapHeight; i++)
                {
                    uint* row = (uint*)((byte*)bits.Scan0 + (i * bits.Stride));

                    for (int j = 0; j < Variables.bitmapWidth; j++)
                        row[j] = Variables.bitmapArray[j, i];
                }
            }
            bmp.UnlockBits(bits);
            Variables.bitmap = bmp;
            pictureBoxMain.Image = Variables.bitmap;
        }

        // NAVIGATION LOGIC
        private void numericUpDownLineThickness_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownLineThickness.Value % 2 == 0)
                numericUpDownLineThickness.Value -= 1;
        }

        private void checkBoxAntiAliasing_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAntiAliasing.Checked) 
                Variables.isAntiAliased = true;
            else
                Variables.isAntiAliased = false;
            UpdateBitmap();
        }

        private void buttonBrushColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                labelBrushColor.BackColor = colorDialog.Color;
                Variables.brushColor = ((uint)colorDialog.Color.A << 24) + ((uint)colorDialog.Color.R << 16) 
                    + ((uint)colorDialog.Color.G << 8) + (uint)colorDialog.Color.B;
            }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            Variables.shapes.Clear();
            Variables.isActive = false;
            Variables.lastID = 0;
            Variables.bitmap.Dispose();
            InitializeBitmap();
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
                        UpdateBitmap();
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
                        UpdateBitmap();
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
                        ((Circle)Variables.activeShape).radius =
                            ((Circle)Variables.activeShape).CalculateRadius(e.Location);
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                        UpdateBitmap();
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
                        if (IsClosePoint(((Polygon)Variables.activeShape).vertices[0], e.Location))
                        {
                            Variables.shapes.Add(Variables.activeShape);
                            Variables.isActive = false;
                            UpdateBitmap();
                        }
                        else
                            ((Polygon)Variables.activeShape).AddVertex(e.Location);
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
                        brushShape.color = Variables.brushColor;
                        UpdateBitmap();
                    }
                    break;

                case "radioButtonErase":
                    int eraseShapeID = Variables.shapesArray[e.X, e.Y];
                    Shape eraseShape = Variables.shapes.Find(x => x.ID == eraseShapeID);
                    if (eraseShape is not null)
                    {
                        Variables.shapes.Remove(eraseShape);
                        UpdateBitmap();
                    }
                    break;

                default:
                    break;
            }
        }

        private void pictureBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            Variables.positionDown = e.Location;
            //int shapeID;
            //Shape shape;
            //if (Variables.pointsArray[e.X, e.Y] != 0)
            //{
                switch (Variables.modeName)
                {
                    //        case "radioButtonResizeCircle":
                    //            shapeID = Variables.shapesArray[e.X, e.Y];
                    //            shape = Variables.shapes.Find(x => x.ID == shapeID);
                    //
                    //            if (shape.GetType().Equals(typeof(Circle)))
                    //            {
                    //                Variables.activeShape = shape;
                    //                Variables.shapes.Remove(shape);
                    //                Variables.isActive = true;
                    //            }
                    //            break;
                    //
                    //        case "radioButtonMovePoint":
                    //            shapeID = Variables.pointsArray[e.X, e.Y];
                    //            shape = Variables.shapes.Find(x => x.ID == shapeID);
                    //
                    //            if (shape.GetType().Equals(typeof(Circle)))
                    //            {
                    //                Variables.activeElement = 1;
                    //            }
                    //            else if (shape.GetType().Equals(typeof(Polygon)))
                    //            {
                    //                for (int i = 0; i < ((Polygon)shape).vertices.Count; i++)
                    //                {
                    //                    if (IsClosePoint(e.Location, ((Polygon)shape).vertices[i]))
                    //                    {
                    //                        Variables.activeElement = i;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (IsClosePoint(e.Location, ((Line)shape).end1))
                    //                    Variables.activeElement = 1;
                    //                else
                    //                    Variables.activeElement = 2;
                    //            }
                    //
                    //            Variables.activeShape = shape;
                    //            Variables.shapes.Remove(shape);
                    //            Variables.isActive = true;
                    //            break;
                    //
                    //        case "radioButtonMovePolygon":
                    //            shapeID = Variables.shapesArray[e.X, e.Y];
                    //            shape = Variables.shapes.Find(x => x.ID == shapeID);
                    //
                    //            if (shape.GetType().Equals(typeof(Polygon)))
                    //            {
                    //                Variables.activeShape = shape;
                    //                Variables.shapes.Remove(shape);
                    //                Variables.isActive = true;
                    //            }
                    //            break;
                    //
                    //        case "radioButtonMoveLine":
                    //            shapeID = Variables.shapesArray[e.X, e.Y];
                    //            shape = Variables.shapes.Find(x => x.ID == shapeID);
                    //
                    //            if (shape.GetType().Equals(typeof(Polygon)))
                    //            {
                    //                for (int i = 0; i < ((Polygon)shape).vertices.Count; i++)
                    //                {
                    //                    if (IsClosePoint(e.Location, ((Polygon)shape).vertices[i]))
                    //                    {
                    //                        Variables.activeShape = shape;
                    //                        Variables.activeElement = i;
                    //                        Variables.shapes.Remove(shape);
                    //                        Variables.isActive = true;
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //            else if (shape.GetType().Equals(typeof(Line)))
                    //            {
                    //                Variables.activeShape = shape;
                    //                Variables.shapes.Remove(shape);
                    //                Variables.isActive = true;
                    //            }
                    //            break;

                    default:
                        break;
                }
            //}
        }

        private void pictureBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (Variables.isActive)
            {
                //int xShift = e.X - Variables.downPosition.X;
                //int yShift = e.Y - Variables.downPosition.Y;
                Variables.positionNow = CleanLocation(e.Location);
                switch (Variables.modeName)
                {
                    case "radioButtonAddThinLine":
                    case "radioButtonAddThickLine":
                    case "radioButtonAddCircle":
                    case "radioButtonAddPolygon":
                        UpdateBitmap();
                        break;

//                    case "radioButtonResizeCircle":
//                        ((Circle)shapeCopy).AddRadius(e.Location);
//                        // draw
//                        break;
//
//                    case "radioButtonMovePoint":
//                        if (shapeCopy.GetType().Equals(typeof(Line)))
//                        {
//
//                        }
//                        else if (shapeCopy.GetType().Equals(typeof(Circle)))
//                        {
//
//                        }
//                        else // Polygon
//                        {
//
//                        }
//                        break;
//
//                    case "radioButtonMovePolygon":
//
//                        break;
//
//                    case "radioButtonMoveLine":
//                        if (shapeCopy.GetType().Equals(typeof(Line)))
//                        {
//
//                        }
//                        else if (shapeCopy.GetType().Equals(typeof(Polygon)))
//                        {
//
//                        }
//                        break;

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
            switch (Variables.modeName)
            {
                case "radioButtonResizeCircle":

                    break;

                case "radioButtonMovePoint":

                    break;

                case "radioButtonMovePolygon":

                    break;

                case "radioButtonMoveLine":

                    break;

                default:
                    break;
            }
        }

        private void pictureBoxMain_MouseLeave(object sender, EventArgs e)
        {
            switch (Variables.modeName)
            {
                case "radioButtonResizeCircle":

                    break;

                case "radioButtonMovePoint":

                    break;

                case "radioButtonMovePolygon":

                    break;

                case "radioButtonMoveLine":

                    break;

                default:
                    break;
            }
        }

        private void mode_CheckedChanged(object sender, EventArgs e)
        {
            Variables.modeName = ((RadioButton)sender).Name;
            Variables.isActive = false;
            // switch (Variables.modeName)
            // {
            //     case "radioButtonAddThinLine":
            //     case "radioButtonAddThickLine":
            //     case "radioButtonAddCircle":
            //     case "radioButtonAddPolygon":
            //     case "radioButtonResizeCircle":
            //     case "radioButtonMovePolygon":
            //     case "radioButtonMoveLine":
            //     case "radioButtonBrush":
            //     case "radioButtonErase":
            //     case "radioButtonMovePoint":
            //         break;
            // }
            UpdateBitmap();
        }
    }

    // DRAWING OBEJCTS
    public abstract class Shape
    {
        public int ID;
        public uint color;

        public Shape()
        {
            ID = Variables.lastID + 1;
            Variables.lastID = ID;
            color = Variables.brushColor;
        }

        public Shape Clone()
        {
            return (Shape)this.MemberwiseClone();
        }

        public abstract void DrawShape();
        public abstract void DrawActiveShape();
        public abstract void DrawPoints();
    }

    public class Line : Shape
    {
        public Point end1;
        public Point end2;
        public int thickness;

        public Line(Point newEnd, int newThickness = 1) : base()
        {
            end1 = newEnd;
            thickness = newThickness;
        }

        public override void DrawShape()
        {
            if (Variables.isAntiAliased)
                Form1.AntialiasedLine(end1, end2, ID, color, thickness);
            else if (thickness == 1)
                Form1.MidpointLine(end1, end2, ID, color);
            else
                Form1.BrushLine(end1, end2, ID, color, thickness);
            Form1.AddPoint(end1, ID);
            Form1.AddPoint(end2, ID);
        }

        public override void DrawActiveShape()
        {
            if (Variables.isAntiAliased)
                Form1.AntialiasedLine(end1, Variables.positionNow, ID, color, thickness);
            else if (thickness == 1)
                Form1.MidpointLine(end1, Variables.positionNow, ID, color);
            else
                Form1.BrushLine(end1, Variables.positionNow, ID, color, thickness);
        }

        public override void DrawPoints()
        {
            Form1.DrawPoint(end1);
            Form1.DrawPoint(end2);
        }
    }

    public class Circle : Shape
    {
        public Point center;
        public int radius;

        public Circle(Point newCenter) : base()
        {
            center = newCenter;
            // if (radius > 0)
            //     radius = newRadius;
        }

        public Circle(Point newCenter, Point radiusPoint) : base()
        {
            center = newCenter;
            radius = CalculateRadius(radiusPoint);
        }

        public int CalculateRadius(Point radiusPoint)
        {
            return (int)Math.Sqrt((center.X - radiusPoint.X) * (center.X - radiusPoint.X)
                + (center.Y - radiusPoint.Y) * (center.Y - radiusPoint.Y));
        }

        public override void DrawShape()
        {
            Form1.MidpointCircle(center, radius, ID, color);
            Form1.AddPoint(center, ID);
        }

        public override void DrawActiveShape()
        {
            int currentRadius = CalculateRadius(Variables.positionNow);
            Form1.MidpointCircle(center, currentRadius, ID, color);
        }

        public override void DrawPoints()
        {
            Form1.DrawPoint(center);
        }
    }

    public class Polygon : Shape
    {
        public List<Point> vertices;

        public Polygon(Point newPoint) : base()
        {
            vertices = new List<Point>();
            vertices.Add(newPoint);
        }

        public void AddVertex(Point newPoint)
        {
            vertices.Add(newPoint);
        }

        public override void DrawShape()
        {
            if (Variables.isAntiAliased)
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.AntialiasedLine(vertices[i], vertices[i + 1], ID, color, 1);
            }
            else
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.MidpointLine(vertices[i], vertices[i + 1], ID, color);
            }
            if (Variables.isAntiAliased)
                Form1.AntialiasedLine(vertices[vertices.Count() - 1], vertices[0], ID, color, 1);
            else
                Form1.MidpointLine(vertices[vertices.Count() - 1], vertices[0], ID, color);
            for (int i = 0; i < vertices.Count(); i++)
                Form1.AddPoint(vertices[i], ID);
        }

        public override void DrawActiveShape()
        {
            if (Variables.isAntiAliased)
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.AntialiasedLine(vertices[i], vertices[i + 1], ID, color, 1);
            } 
            else
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.MidpointLine(vertices[i], vertices[i + 1], ID, color);
            }
            if (Variables.isAntiAliased)
                Form1.AntialiasedLine(vertices[vertices.Count() - 1], Variables.positionNow, ID, color, 1);
            else
                Form1.MidpointLine(vertices[vertices.Count() - 1], Variables.positionNow, ID, color);
            Form1.DrawPoint(vertices[0]);
        }

        public override void DrawPoints()
        {
            for (int i = 0; i < vertices.Count(); i++)
            {
                Form1.DrawPoint(vertices[i]);
            }
        }
    }

    public static class Variables
    {
        //public static List<uint> colourDebug = new List<uint>();
        //public static int[,] valueCopy;
        //public static float[,] floatCopy;
        public static Bitmap bitmap;
        public static Point positionDown;
        public static Point positionNow;
        public static uint brushColor = 4278190080;       // black
        public static string modeName = "radioButtonAddThinLine";
        public static bool isAntiAliased = false;
        public static bool isActive = false;
        public static int lastID = 0;

        public static List<Shape> shapes = new List<Shape>();
        public static Shape activeShape;
        //public static Shape copyShape;
        public static int activeElement;

        public static int bitmapHeight = 800;
        public static int bitmapWidth = 600;
        public static uint[,] bitmapArray = new uint[bitmapWidth, bitmapHeight];
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

//Variables.bitmapArray[i, line.end1.Y] = line.color;
//Variables.shapesArray[i, line.end1.Y] = line.ID;

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