using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace CGPart4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeBitmap();
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

        public unsafe static float CalculatePointToSegmentDistance(Point point, Point seg1, Point seg2, int thickness)
        {
            float thick = 0;
            int ps1x = point.X - seg1.X;
            int ps1y = point.Y - seg1.Y;
            int dx = seg2.X - seg1.X;
            int dy = seg2.Y - seg1.Y;

            int dot = ps1x * dx + ps1y * dy;
            int len2 = dx * dx + dy * dy;
            float param = -1;

            if (len2 != 0)
                param = (float)dot / len2;

            float projx, projy;
            if (param < 0)
            {
                projx = seg1.X;
                projy = seg1.Y;
            }
            else if (param > 1)
            {
                projx = seg2.X;
                projy = seg2.Y;
            }
            else
            {
                projx = param * dx + seg1.X;
                projy = param * dy + seg1.Y;
                thick = (float)thickness / 2;
            }

            float distx = point.X - projx;
            float disty = point.Y - projy;
            float dist = (float)Math.Sqrt(distx * distx + disty * disty) - thick;
            if (dist < 0)
                dist = 0;

            return dist;
        }

        public unsafe static float CalculatePointToCircleDistance(Point point, Circle circle)
        {
            float rad = CalculatePointToPointDistance(point, circle.center);
            float dist = (float)circle.radius - rad;

            if (dist < 0)
                dist = -dist;
            dist -= (float)circle.thickness / 2;

            if (dist < 0)
                dist = 0;
            return dist;
        }

        public unsafe static float CalculatePointToPointDistance(Point point1, Point point2)
        {
            return (float)Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X)
                    + (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }

        public unsafe static (int, int) GetClosestPoint(Point point)
        {
            int index, bestIndex = 0, bestObject = 0;
            float distance, bestDistance = float.PositiveInfinity;
            for (int i = 0; i < Variables.shapes.Count; i++)
            {
                (index, distance) = Variables.shapes[i].GetPointDistance(point);
                if (distance < bestDistance)
                {
                    bestIndex = index;
                    bestObject = Variables.shapes[i].ID;
                    bestDistance = distance;
                }
            }

            if (bestDistance <= 5)
                return (bestObject, bestIndex);
            else
                return (0, 0);
        }

        public unsafe static (int, int) GetClosestLine(Point point)
        {
            int index, bestIndex = 0, bestObject = 0;
            float distance, bestDistance = float.PositiveInfinity;
            for (int i = 0; i < Variables.shapes.Count; i++)
            {
                (index, distance) = Variables.shapes[i].GetLineDistance(point);
                if (distance < bestDistance)
                {
                    bestIndex = index;
                    bestObject = Variables.shapes[i].ID;
                    bestDistance = distance;
                }
            }

            if (bestDistance <= 5)
                return (bestObject, bestIndex);
            else
                return (0, 0);
        }

        [Flags]
        public enum csCode
        {
            LEFT = 1,
            RIGHT = 2,
            BOTTOM = 4,
            TOP = 8
        };

        public unsafe static csCode ComputeCohenSutherlandCodes(Point point, Rectang rectangle)
        {
            csCode outcode = 0;
            if (point.X > FastMax(rectangle.end1.X, rectangle.end2.X)) 
                outcode |= csCode.RIGHT;
            else if (point.X < FastMin(rectangle.end1.X, rectangle.end2.X)) 
                outcode |= csCode.LEFT;

            if (point.Y > FastMax(rectangle.end1.Y, rectangle.end2.Y)) 
                outcode |= csCode.TOP;
            else if (point.Y < FastMin(rectangle.end1.Y, rectangle.end2.Y)) 
                outcode |= csCode.BOTTOM;  // reversed meaning

            return outcode;
        }

        // DRAWING FUNCTIONS
        //          BRUSH
        public unsafe static bool[,] CreateBrush(int diagonal)
        {   // assuming diagonal is odd
            int radius = diagonal / 2;
            bool[,] circle = new bool[diagonal, diagonal];

            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    if (FastRound((float)Math.Sqrt(i * i + j * j)) <= radius)
                        circle[radius + i, radius + j] = true;
                }
            }
            return circle;
        }

        public unsafe static void ApplyBrush(int x, int y, bool[,] brush, uint colour, int thickness)
        {
            int radius = thickness / 2;
            for (int i = FastMax(x - radius, 0); i <= FastMin(x + radius, Variables.bitmapWidth - 1); i++)
            {
                for (int j = FastMax(y - radius, 0); j <= FastMin(y + radius, Variables.bitmapHeight - 1); j++)
                {
                    if (brush[i - x + radius, j - y + radius])
                        Variables.bitmapArray[i, j] = colour;
                }
            }
        }

        //          LINE
        public unsafe static void MidpointHorizontalLine(int x1, int y1, int x2, int y2, uint colour)
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
            }
        }

        public unsafe static void MidpointVerticalLine(int x1, int y1, int x2, int y2, uint colour)
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
            }
        }

        public unsafe static void MidpointLine(Point point1, Point point2, uint colour)
        {
            if (FastAbs(point2.X - point1.X) > FastAbs(point2.Y - point1.Y))
            {
                if (point2.X > point1.X)
                    MidpointHorizontalLine(point1.X, point1.Y, point2.X, point2.Y, colour);
                else
                    MidpointHorizontalLine(point2.X, point2.Y, point1.X, point1.Y, colour);
            }
            else
            {
                if (point2.Y > point1.Y)
                    MidpointVerticalLine(point1.X, point1.Y, point2.X, point2.Y, colour);
                else
                    MidpointVerticalLine(point2.X, point2.Y, point1.X, point1.Y, colour);
            }
        }

        public unsafe static void BrushHorizontalLine(int x1, int y1, int x2, int y2, uint colour, int thickness)
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
            ApplyBrush(x, y, brush, colour, thickness);
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
                ApplyBrush(x, y, brush, colour, thickness);
            }
        }

        public unsafe static void BrushVerticalLine(int x1, int y1, int x2, int y2, uint colour, int thickness)
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
            ApplyBrush(x, y, brush, colour, thickness);
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
                ApplyBrush(x, y, brush, colour, thickness);
            }
        }

        public unsafe static void BrushLine(Point point1, Point point2, uint colour, int thickness)
        {
            if (FastAbs(point2.X - point1.X) > FastAbs(point2.Y - point1.Y))
            {
                if (point2.X > point1.X)
                    BrushHorizontalLine(point1.X, point1.Y, point2.X, point2.Y, colour, thickness);
                else
                    BrushHorizontalLine(point2.X, point2.Y, point1.X, point1.Y, colour, thickness);
            }
            else
            {
                if (point2.Y > point1.Y)
                    BrushVerticalLine(point1.X, point1.Y, point2.X, point2.Y, colour, thickness);
                else
                    BrushVerticalLine(point2.X, point2.Y, point1.X, point1.Y, colour, thickness);
            }
        }

        //          CIRCLE
        public unsafe static void MidpointCircle(Point point, int radius, uint colour)
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
                Variables.bitmapArray[x + xc, y + yc] = colour;
            if (caseA && caseH)
                Variables.bitmapArray[x + xc, y - yc] = colour;
            if (caseB && caseG)
                Variables.bitmapArray[x - xc, y + yc] = colour;
            if (caseB && caseH)
                Variables.bitmapArray[x - xc, y - yc] = colour;
            if (caseC && caseE)
                Variables.bitmapArray[x + yc, y + xc] = colour;
            if (caseC && caseF)
                Variables.bitmapArray[x + yc, y - xc] = colour;
            if (caseD && caseE)
                Variables.bitmapArray[x - yc, y + xc] = colour;
            if (caseD && caseF)
                Variables.bitmapArray[x - yc, y - xc] = colour;
            
            while (yc > xc)
            {
                if (d < 0)
                {
                    d += dI;
                    dI += 2;
                    dD += 2;
                }
                else
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
                    Variables.bitmapArray[x + xc, y + yc] = colour;
                if (caseA && caseH)
                    Variables.bitmapArray[x + xc, y - yc] = colour;
                if (caseB && caseG)
                    Variables.bitmapArray[x - xc, y + yc] = colour;
                if (caseB && caseH)
                    Variables.bitmapArray[x - xc, y - yc] = colour;
                if (caseC && caseE)
                    Variables.bitmapArray[x + yc, y + xc] = colour;
                if (caseC && caseF)
                    Variables.bitmapArray[x + yc, y - xc] = colour;
                if (caseD && caseE)
                    Variables.bitmapArray[x - yc, y + xc] = colour;
                if (caseD && caseF)
                    Variables.bitmapArray[x - yc, y - xc] = colour;
            }
        }

        public unsafe static void BrushCircle(Point point, int radius, uint colour, int thickness)
        {
            bool[,] brush = CreateBrush(thickness);

            int x = point.X;
            int y = point.Y;
            int d = 1 - radius;
            int dI = 3;                  // identity axis
            int dD = 5 - 2 * radius;     // diagonal axis
            int xc = 0;         // relative circumference position
            int yc = radius;    // relative circumference position

            ApplyBrush(x + xc, y + yc, brush, colour, thickness);
            ApplyBrush(x + xc, y - yc, brush, colour, thickness);
            ApplyBrush(x - xc, y + yc, brush, colour, thickness);
            ApplyBrush(x - xc, y - yc, brush, colour, thickness);
            ApplyBrush(x + yc, y + xc, brush, colour, thickness);
            ApplyBrush(x + yc, y - xc, brush, colour, thickness);
            ApplyBrush(x - yc, y + xc, brush, colour, thickness);
            ApplyBrush(x - yc, y - xc, brush, colour, thickness);

            while (yc > xc)
            {
                if (d < 0)
                {
                    d += dI;
                    dI += 2;
                    dD += 2;
                }
                else
                {
                    d += dD;
                    dI += 2;
                    dD += 4;
                    yc--;
                }
                xc++;

                ApplyBrush(x + xc, y + yc, brush, colour, thickness);
                ApplyBrush(x + xc, y - yc, brush, colour, thickness);
                ApplyBrush(x - xc, y + yc, brush, colour, thickness);
                ApplyBrush(x - xc, y - yc, brush, colour, thickness);
                ApplyBrush(x + yc, y + xc, brush, colour, thickness);
                ApplyBrush(x + yc, y - xc, brush, colour, thickness);
                ApplyBrush(x - yc, y + xc, brush, colour, thickness);
                ApplyBrush(x - yc, y - xc, brush, colour, thickness);
            }
        }

        //          LINE ANTI-ALIASING
        public unsafe static uint InterpolateColour(uint backColour, uint frontColour, float coverage)
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

        public unsafe static float CoverageFunction(float distance)
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

        public unsafe static float CalculateLineCoverage(int thickness, float distance)
        {   // assuming all lines are at least one pixel thick
            if (0.5f * thickness < distance)
                return CoverageFunction(distance - 0.5f * thickness);
            else
                return 1f - CoverageFunction(0.5f * thickness - distance);

        }

        public unsafe static float AddPixelIntensity(int x, int y, uint colour, int thickness, float distance)
        {
            float coverage = CalculateLineCoverage(thickness, distance);
            if (coverage > 0)
                Variables.bitmapArray[x, y] = InterpolateColour(Variables.bitmapArray[x, y], colour, coverage);

            return coverage;
        }

        public unsafe static void AntialiasedHorizontalLine(int x1, int y1, int x2, int y2, uint colour, int thickness)
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
                AddPixelIntensity(x, y, colour, thickness, 0);
                for (int i = 1; y + i < Variables.bitmapHeight && 
                    AddPixelIntensity(x, y + i, colour, thickness, i * two_dx_invDenominator) > 0; ++i);
                for (int i = 1; y - i >= 0 && 
                    AddPixelIntensity(x, y - i, colour, thickness, i * two_dx_invDenominator) > 0; ++i);
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
                        AddPixelIntensity(x, y, colour, thickness, two_v_dx * invDenominator);
                        for (int i = 1; y + i < Variables.bitmapHeight && AddPixelIntensity(x, y + i, colour, thickness,
                            i * two_dx_invDenominator + two_v_dx * invDenominator) > 0; ++i) ;
                        for (int i = 1; y - i >= 0 && AddPixelIntensity(x, y - i, colour, thickness,
                            i * two_dx_invDenominator - two_v_dx * invDenominator) > 0; ++i) ;
                    }

                    // TODO: brush to array of thickness, change thickness dynamically
                    // x = x2;
                    // while (x < Variables.bitmapWidth && x < x2 + radius)
                    // {
                    //     if (d < 0)
                    //     {
                    //         two_v_dx = d + dx;
                    //         d += dI;
                    //     }
                    //     else
                    //     {
                    //         two_v_dx = d - dx;
                    //         d += dD;
                    //         y += ys;
                    //     }
                    //     AddPixelIntensity(x, y, ID, colour, thickness, 
                    //         (float)Math.Sqrt(FastAbs(x - x2) * FastAbs(x - x2) + FastAbs(y - y2) * FastAbs(y - y2)));
                    //     for (int i = 1; y + i < Variables.bitmapHeight && AddPixelIntensity(x, y + i, ID, colour, thickness,
                    //         (float)Math.Sqrt(FastAbs(x - x2) * FastAbs(x - x2) + FastAbs(y + i - y2) * FastAbs(y + i - y2))) > 0; ++i) ;
                    //     for (int i = 1; y - i >= 0 && AddPixelIntensity(x, y - i, ID, colour, thickness,
                    //         (float)Math.Sqrt(FastAbs(x - x2) * FastAbs(x - x2) + FastAbs(y - i - y2) * FastAbs(y - i - y2))) > 0; ++i) ;
                    //     x++;
                    // }
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
                        AddPixelIntensity(x, y, colour, thickness, two_v_dx * invDenominator);
                        for (int i = 1; y + i < Variables.bitmapHeight && AddPixelIntensity(x, y + i, colour, thickness,
                            i * two_dx_invDenominator - two_v_dx * invDenominator) > 0; ++i) ;
                        for (int i = 1; y - i >= 0 && AddPixelIntensity(x, y - i, colour, thickness,
                            i * two_dx_invDenominator + two_v_dx * invDenominator) > 0; ++i) ;
                    }
                }

            }
        }

        public unsafe static void AntialiasedVerticalLine(int x1, int y1, int x2, int y2, uint colour, int thickness)
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
                AddPixelIntensity(x, y, colour, thickness, 0);
                for (int i = 1; x + i < Variables.bitmapWidth && 
                    AddPixelIntensity(x + i, y, colour, thickness, i * two_dy_invDenominator) > 0; ++i) ;
                for (int i = 1; x - i >= 0 && 
                    AddPixelIntensity(x - i, y, colour, thickness, i * two_dy_invDenominator) > 0; ++i) ;
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
                        AddPixelIntensity(x, y, colour, thickness, two_v_dy * invDenominator);
                        for (int i = 1; x + i < Variables.bitmapWidth && AddPixelIntensity(x + i, y, colour, thickness,
                            i * two_dy_invDenominator + two_v_dy * invDenominator) > 0; ++i) ;
                        for (int i = 1; x - i >= 0 && AddPixelIntensity(x - i, y, colour, thickness,
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
                        AddPixelIntensity(x, y, colour, thickness, two_v_dy * invDenominator);
                        for (int i = 1; x + i < Variables.bitmapWidth && AddPixelIntensity(x + i, y, colour, thickness,
                            i * two_dy_invDenominator - two_v_dy * invDenominator) > 0; ++i) ;
                        for (int i = 1; x - i >= 0 && AddPixelIntensity(x - i, y, colour, thickness,
                            i * two_dy_invDenominator + two_v_dy * invDenominator) > 0; ++i) ;
                    }
                }
            }
        }

        public unsafe static void AntialiasedLine(Point point1, Point point2, uint colour, int thickness)
        {
            if (FastAbs(point2.X - point1.X) > FastAbs(point2.Y - point1.Y))
            {
                if (point2.X > point1.X)
                    AntialiasedHorizontalLine(point1.X, point1.Y, point2.X, point2.Y, colour, thickness);
                else
                    AntialiasedHorizontalLine(point2.X, point2.Y, point1.X, point1.Y, colour, thickness);
            }
            else
            {
                if (point2.Y > point1.Y)
                    AntialiasedVerticalLine(point1.X, point1.Y, point2.X, point2.Y, colour, thickness);
                else
                    AntialiasedVerticalLine(point2.X, point2.Y, point1.X, point1.Y, colour, thickness);
            }
        }

        //          LINE CLIPPED
        public unsafe static (Point, Point) DrawCohenSutherlandLine(Point point1, Point point2, Rectang rectangle)
        {
            bool accept = false, done = false;
            csCode outcode1 = ComputeCohenSutherlandCodes(point1, rectangle);
            csCode outcode2 = ComputeCohenSutherlandCodes(point2, rectangle);
            do
            {
                if ((outcode1 | outcode2) == 0)
                {
                    accept = true;
                    done = true;
                }
                else if ((outcode1 & outcode2) != 0)
                {
                    accept = false;
                    done = true;
                }
                else
                {
                    csCode outcodeOut = (outcode1 != 0) ? outcode1 : outcode2;
                    Point point = new Point();

                    if ((outcodeOut & csCode.TOP) != 0)
                    {
                        point.X = point1.X + (point2.X - point1.X) * (FastMax(rectangle.end1.Y, rectangle.end2.Y) - point1.Y) / (point2.Y - point1.Y);
                        point.Y = FastMax(rectangle.end1.Y, rectangle.end2.Y);
                    }
                    else if ((outcodeOut & csCode.BOTTOM) != 0)
                    {
                        point.X = point1.X + (point2.X - point1.X) * (FastMin(rectangle.end1.Y, rectangle.end2.Y) - point1.Y) / (point2.Y - point1.Y);
                        point.Y = FastMin(rectangle.end1.Y, rectangle.end2.Y);
                    }
                    else if ((outcodeOut & csCode.RIGHT) != 0)
                    {
                        point.Y = point1.Y + (point2.Y - point1.Y) * (FastMax(rectangle.end1.X, rectangle.end2.X) - point1.X) / (point2.X - point1.X);
                        point.X = FastMax(rectangle.end1.X, rectangle.end2.X);
                    }
                    else if ((outcodeOut & csCode.LEFT) != 0)
                    {
                        point.Y = point1.Y + (point2.Y - point1.Y) * (FastMin(rectangle.end1.X, rectangle.end2.X) - point1.X) / (point2.X - point1.X);
                        point.X = FastMin(rectangle.end1.X, rectangle.end2.X);
                    }

                    if (outcodeOut == outcode1)
                    {
                        point1 = point;
                        outcode1 = ComputeCohenSutherlandCodes(point1, rectangle);
                    }
                    else
                    {
                        point2 = point;
                        outcode2 = ComputeCohenSutherlandCodes(point2, rectangle);
                    }
                }
            } while (!done);
            if (accept)
                return (point1, point2);
            //BrushLine(point1, point2, 4278190335, rectangle.thickness + 2);
            else
                return (new Point(-1, -1), new Point(-1, -1));
            //BrushLine(point1, point2, 4294901760, rectangle.thickness + 2);
        }

        //          POLYGON
        public unsafe struct AET
        {
            public int x;
            public int yMax;
            public float xFloat;
            public float mInverse;
            //public AET* next;

            public AET(int x, int yMax, float mInverse)
            {
                this.x = x;
                this.xFloat = (float)x;
                this.yMax = yMax;
                this.mInverse = mInverse;
                //this.next = null;
            }

            //public void Add(AET* aet)
            //{
            //    if (next is not null)
            //    {
            //        AET* elems = next;
            //        while (elems->next is not null)
            //            elems = elems->next;
            //        elems->next = aet;
            //    }
            //    else
            //        next = aet;
            //}
        }

        //public unsafe static AET* sortAET(AET* aet)
        //{
        //    AET* root = aet;
        //    if (aet is not null)
        //    {
        //        int count = 1;
        //        while (aet->next is not null)
        //        {
        //            count++;
        //            aet = aet->next;
        //        }

        //        aet = root;
        //        AET*[] address = new AET*[count];
        //        List<(int, int)> sortedList = new List<(int, int)>();

        //        address[0] = root;
        //        sortedList.Add((0, address[0]->x));
        //        for (int i = 0; i < count - 1; i++)
        //        {
        //            address[i + 1] = address[i]->next;
        //            sortedList.Add((i + 1, address[i + 1]->x));
        //        }

        //        sortedList = sortedList.OrderBy(s => s.Item2).ToList();

        //        root = address[sortedList[0].Item1];
        //        aet = root;
        //        for (int i = 1; i < count; i++)
        //        {
        //            aet->next = address[sortedList[i].Item1];
        //            aet = aet->next;
        //        }
        //        aet->next = null;
        //        return root;
        //    }
        //    else
        //        return null;
        //}

        public unsafe static void sortAET(ref List<AET> aet)
        {
            aet = aet.OrderBy(aet => aet.xFloat).ToList();
        }

        //public unsafe static AET* cleanAET(AET* aet, int y)
        //{
        //    AET* root = aet;
        //    while (root is not null && root->yMax == y)
        //    {
        //        root = root->next;
        //    }
        //    if (root is not null)
        //    {
        //        aet = root;
        //        while (aet->next is not null)
        //        {
        //            if (aet->next->yMax == y)
        //                aet->next = aet->next->next;
        //            else aet = aet->next;
        //        }
        //        return root;
        //    }
        //    else return null;
        //}

        public unsafe static void cleanAET(ref List<AET> aet, int y)
        {
            for (int i = 0; i < aet.Count; i++)
            {
                if (aet[i].yMax == y)
                {
                    aet.RemoveAt(i);
                    i--;
                }
            }
        }

        //public unsafe static AET* updateAET(AET* aet)
        //{
        //    AET* root = aet;
        //    if (root is not null)
        //    {
        //        while (aet->next is not null)
        //        {
        //            aet->xFloat += aet->mInverse;
        //            aet->x = FastRound(aet->xFloat);
        //            aet = aet->next;
        //        }

        //        aet->xFloat += aet->mInverse;
        //        aet->x = FastRound(aet->xFloat);
        //        return root;
        //    }
        //    else
        //        return null;
        //}

        public unsafe static void updateAET(ref List<AET> aet)
        {
            for (int i = 0; i < aet.Count; i++)
            {
                AET copy = aet[i];
                copy.xFloat += copy.mInverse;
                copy.x = FastRound(copy.xFloat);
                aet[i] = copy;
            }
        }

        //public unsafe static void fillLine(AET* aet, int y)
        //{
        //    AET* pointer = aet;
        //    while (pointer is not null && pointer->next is not null)
        //    {
        //        MidpointLine(new Point(pointer->x, y), new Point(pointer->next->x, y), 4278190335);  // blue
        //        pointer = pointer->next->next;
        //    }
        //}

        public unsafe static Point calculateOrigin(Polygon polygon)
        {
            int xMin = int.MaxValue;
            int yMin = int.MaxValue;
            for (int i = 0; i < polygon.vertices.Count; i++)
            {
                if (polygon.vertices[i].X < xMin)
                    xMin = polygon.vertices[i].X;
                if (polygon.vertices[i].Y < yMin)
                    yMin = polygon.vertices[i].Y;
            }
            return new Point(xMin, yMin);
        }

        public unsafe static void fillLineWithImage(List<AET> aet, int y, Point origin, int xSize, int ySize, uint[,] image)
        {
            int yMod = (y - origin.Y) - ((y - origin.Y) / ySize) * ySize;
            int count = aet.Count;
            for (int i = 0; i <= count - 2; i += 2)
            {
                for (int j = aet[i].x; j <= aet[i + 1].x; j++)
                {
                    Variables.bitmapArray[j, y] = image[yMod, ((j - origin.X) - ((j - origin.X) / xSize) * xSize)];
                }
            }
        }

        public unsafe static void fillLineWithColor(List<AET> aet, int y, uint color)
        {
            int count = aet.Count;
            for (int i = 0; i <= count - 2; i += 2)
            {
                MidpointLine(new Point(aet[i].x, y), new Point(aet[i + 1].x, y), color);
            }
        }

        public unsafe static int[] getSortedVertices(Polygon polygon)
        {
            int[] vertices = new int[polygon.vertices.Count];
            List<Point> sortedPoints = polygon.vertices.OrderBy(vertices => vertices.Y).ToList();
            for (int i = 0; i < polygon.vertices.Count; i++)
            {
                vertices[i] = polygon.vertices.IndexOf(sortedPoints[i]);
            }
            return vertices;
        }

        //public unsafe static void fillPolygon(Polygon polygon)
        //{
        //    AET* root = null;
        //    int[] vSorted = getSortedVertices(polygon);
        //    int y, yMin, yMax, i = 0;
        //    int vBefore, vAfter, vNumber = vSorted[i];
        //    y = yMin = polygon.vertices[vSorted[0]].Y;
        //    yMax = polygon.vertices[vSorted[vSorted.Length - 1]].Y;

        //    while (y < yMax)
        //    {
        //        while (polygon.vertices[vNumber].Y == y)
        //        {
        //            if (vNumber == 0)
        //                vBefore = polygon.vertices.Count - 1;
        //            else
        //                vBefore = vNumber - 1;

        //            if (vNumber == polygon.vertices.Count - 1)
        //                vAfter = 0;
        //            else
        //                vAfter = vNumber + 1;

        //            if (polygon.vertices[vBefore].Y > polygon.vertices[vNumber].Y)
        //            {
        //                float inverse = (float)(polygon.vertices[vBefore].X - polygon.vertices[vNumber].X) /
        //                    (float)(polygon.vertices[vBefore].Y - polygon.vertices[vNumber].Y);
        //                AET aet = new AET(polygon.vertices[vNumber].X, polygon.vertices[vBefore].Y, inverse);
        //                if (root is null)
        //                    root = &aet;
        //                else
        //                    root->Add(&aet);
        //            }

        //            if (polygon.vertices[vAfter].Y > polygon.vertices[vNumber].Y)
        //            {
        //                float inverse = (float)(polygon.vertices[vAfter].X - polygon.vertices[vNumber].X) /
        //                    (float)(polygon.vertices[vAfter].Y - polygon.vertices[vNumber].Y);
        //                AET aet = new AET(polygon.vertices[vNumber].X, polygon.vertices[vAfter].Y, inverse);
        //                if (root is null)
        //                    root = &aet;
        //                else
        //                    root->Add(&aet);
        //            }

        //            i++;
        //            vNumber = vSorted[i];
        //        }

        //        root = sortAET(root);
        //        fillLineWithColor(root, y);

        //        y++;
        //        root = cleanAET(root, y);
        //        root = updateAET(root);
        //    }
        //}

        public unsafe static void fillPolygon(Polygon polygon)
        {
            Point origin = calculateOrigin(polygon);
            List<AET> aet = new List<AET>();

            int[] vSorted = getSortedVertices(polygon);
            int y, yMin, yMax, i = 0;
            int vBefore, vAfter, vNumber = vSorted[i];
            y = yMin = polygon.vertices[vSorted[0]].Y;
            yMax = polygon.vertices[vSorted[vSorted.Length - 1]].Y;

            while (y < yMax)
            {
                while (polygon.vertices[vNumber].Y == y)
                {
                    if (vNumber == 0)
                        vBefore = polygon.vertices.Count - 1;
                    else
                        vBefore = vNumber - 1;

                    if (vNumber == polygon.vertices.Count - 1)
                        vAfter = 0;
                    else
                        vAfter = vNumber + 1;

                    if (polygon.vertices[vBefore].Y > polygon.vertices[vNumber].Y)
                    {
                        float inverse = (float)(polygon.vertices[vBefore].X - polygon.vertices[vNumber].X) /
                            (float)(polygon.vertices[vBefore].Y - polygon.vertices[vNumber].Y);
                        AET newAET = new AET(polygon.vertices[vNumber].X, polygon.vertices[vBefore].Y, inverse);
                        aet.Add(newAET);
                    }

                    if (polygon.vertices[vAfter].Y > polygon.vertices[vNumber].Y)
                    {
                        float inverse = (float)(polygon.vertices[vAfter].X - polygon.vertices[vNumber].X) /
                            (float)(polygon.vertices[vAfter].Y - polygon.vertices[vNumber].Y);
                        AET newAET = new AET(polygon.vertices[vNumber].X, polygon.vertices[vAfter].Y, inverse);
                        aet.Add(newAET);
                    }

                    i++;
                    vNumber = vSorted[i];
                }

                sortAET(ref aet);
                if (polygon.fillMode == 2 && polygon.image is not null)
                    fillLineWithImage(aet, y, origin, polygon.image.GetLength(1), polygon.image.GetLength(0), polygon.image);
                else
                    fillLineWithColor(aet, y, polygon.color);
                
                y++;
                cleanAET(ref aet, y);
                updateAET(ref aet);
            }
        }

        //          POINT
        public unsafe static void DrawPoint(Point point)
        {
            for (int i = FastMax(point.X - 4, 0); i <= FastMin(point.X + 4, Variables.bitmapWidth - 1); i++)
            {
                for (int j = FastMax(point.Y - 4, 0); j <= FastMin(point.Y + 4, Variables.bitmapHeight - 1); j++)
                {
                    if (Variables.pointsKernel[i - point.X + 4, j - point.Y + 4])
                    {
                        Variables.bitmapArray[i, j] = 4294901760;   // red
                    }
                }
            }
        }

        //          BITMAP & GUI
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

        public Bitmap generateImage(uint[,] image)
        {
            if (image is not null)
            {
                Bitmap bmp = new Bitmap(image.GetLength(1), image.GetLength(0));
                BitmapData bits = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadWrite, bmp.PixelFormat);

                unsafe
                {
                    for (int i = 0; i < image.GetLength(0); i++)
                    {
                        uint* row = (uint*)((byte*)bits.Scan0 + (i * bits.Stride));
                        for (int j = 0; j < image.GetLength(1); j++)
                        {
                            row[j] = image[i, j];
                        }
                    }
                }

                bmp.UnlockBits(bits);
                return bmp;
            }
            else
                return null;
        }

        private void buttonPolygonImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap img = new Bitmap(Image.FromFile(openFileDialog.FileName));
                Variables.drawingImage = new uint[img.Height, img.Width];

                BitmapData bits = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),
                    ImageLockMode.ReadOnly, img.PixelFormat);

                unsafe
                {
                    uint conv; // this method allows to work with both RGB and ARGB files
                               // however, the alpha layer is not modified by the program
                    for (int i = 0; i < img.Height; i++)
                    {
                        uint* row = (uint*)((byte*)bits.Scan0 + (i * bits.Stride));
                        for (int j = 0; j < img.Width; j++)
                        {
                            conv = row[j];
                            Variables.drawingImage[i, j] = conv;
                        }
                    }
                }

                img.UnlockBits(bits);

                pictureBoxPolygonImage.Image = generateImage(Variables.drawingImage);
                img.Dispose();
            }
        }

        private void buttonObjectImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap img = new Bitmap(Image.FromFile(openFileDialog.FileName));
                Variables.objectImage = new uint[img.Height, img.Width];

                BitmapData bits = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),
                    ImageLockMode.ReadOnly, img.PixelFormat);

                unsafe
                {
                    uint conv; // this method allows to work with both RGB and ARGB files
                               // however, the alpha layer is not modified by the program
                    for (int i = 0; i < img.Height; i++)
                    {
                        uint* row = (uint*)((byte*)bits.Scan0 + (i * bits.Stride));
                        for (int j = 0; j < img.Width; j++)
                        {
                            conv = row[j];
                            Variables.objectImage[i, j] = conv;
                        }
                    }
                }

                img.UnlockBits(bits);

                pictureBoxObjectImage.Image = generateImage(Variables.objectImage);
                img.Dispose();
            }
        }

        private int getPolygonMode()
        {
            if (radioButtonPolygonEmpty.Checked == true)
                return 0;
            else if (radioButtonPolygonColour.Checked == true)
                return 1;
            else
                return 2;
        }

        private int getObjectMode()
        {
            if (radioButtonObjectEmpty.Checked == true)
                return 0;
            else if (radioButtonObjectColour.Checked == true)
                return 1;
            else
                return 2;
        }

        private void disableObjectSettings()
        {
            numericUpDownObjectThickness.Enabled = false;
            buttonObjectColor.Enabled = false;
            buttonObjectImage.Enabled = false;
            buttonSaveObject.Enabled = false;
            labelObjectColor.BackColor = Color.Silver;

            radioButtonObjectEmpty.Checked = false;
            radioButtonObjectColour.Checked = false;
            radioButtonObjectImage.Checked = false;

            radioButtonObjectEmpty.Enabled = false;
            radioButtonObjectColour.Enabled = false;
            radioButtonObjectImage.Enabled = false;
            pictureBoxObjectImage.Image = null;
        }

        private void enableObjectSettings(Shape shape)
        {
            numericUpDownObjectThickness.Value = shape.thickness;
            labelObjectColor.BackColor = Color.FromArgb((int)shape.color);

            numericUpDownObjectThickness.Enabled = true;
            buttonObjectColor.Enabled = true;
            buttonSaveObject.Enabled = true;

            if (shape is Polygon)
            {
                radioButtonObjectColour.Enabled = true;
                radioButtonObjectEmpty.Enabled = true;
                radioButtonObjectImage.Enabled = true;

                if (((Polygon)shape).fillMode == 0)
                    radioButtonObjectEmpty.Checked = true;
                else if (((Polygon)shape).fillMode == 1)
                    radioButtonObjectColour.Checked = true;
                else
                    radioButtonObjectImage.Checked = true;

                buttonObjectImage.Enabled = true;
                pictureBoxObjectImage.Image = generateImage(((Polygon)shape).image);
                if (((Polygon)shape).image is not null)
                    Variables.objectImage = (uint[,])((Polygon)shape).image.Clone();
                else
                    Variables.objectImage = null;
            }
            else
            {
                buttonObjectImage.Enabled = false;
                radioButtonObjectEmpty.Checked = false;
                radioButtonObjectColour.Checked = false;
                radioButtonObjectImage.Checked = false;

                radioButtonObjectEmpty.Enabled = false;
                radioButtonObjectColour.Enabled = false;
                radioButtonObjectImage.Enabled = false;
                pictureBoxObjectImage.Image = null;
            }
        }

        // NAVIGATION LOGIC
        private void numericUpDownLineThickness_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownLineThickness.Value % 2 == 0)
                numericUpDownLineThickness.Value -= 1;
        }

        private void numericUpDownObjectThickness_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownObjectThickness.Value % 2 == 0)
                numericUpDownObjectThickness.Value -= 1;
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

        private void buttonObjectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
                labelObjectColor.BackColor = colorDialog.Color;
        }

        private void buttonSaveObject_Click(object sender, EventArgs e)
        {
            Shape shape = Variables.shapes.Find(x => x.ID == Variables.activeElement);
            shape.thickness = (int)numericUpDownObjectThickness.Value;
            shape.color = ((uint)labelObjectColor.BackColor.A << 24) + ((uint)labelObjectColor.BackColor.R << 16)
                        + ((uint)labelObjectColor.BackColor.G << 8) + (uint)labelObjectColor.BackColor.B;
            if (shape is Polygon)
            {
                ((Polygon)shape).fillMode = getObjectMode();
                if (Variables.objectImage is not null)
                    ((Polygon)shape).image = (uint[,])Variables.objectImage.Clone();
                else
                    ((Polygon)shape).image = null;
            }
            UpdateBitmap();
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
                case "radioButtonAddLine":
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
                        Variables.activeShape = new Circle(e.Location, (int)numericUpDownLineThickness.Value);
                        Variables.isActive = true;
                    }
                    break;

                case "radioButtonAddRectangle":
                    if (Variables.isActive)
                    {
                        ((Rectang)Variables.activeShape).end2 = e.Location;
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                        UpdateBitmap();
                    }
                    else
                    {
                        Variables.activeShape = new Rectang(e.Location,
                            (int)numericUpDownLineThickness.Value);
                        Variables.isActive = true;
                    }
                    break;

                case "radioButtonAddPolygon":
                    if (Variables.isActive)
                    {
                        if (CalculatePointToPointDistance(((Polygon)Variables.activeShape).vertices[0], e.Location) <= 5)
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
                        Variables.activeShape = new Polygon(e.Location, (int)numericUpDownLineThickness.Value, getPolygonMode());
                        Variables.isActive = true;
                    }
                    break;

                case "radioButtonBrush":
                    (int brushShapeID, int _ ) = GetClosestLine(e.Location);
                    Shape brushShape = Variables.shapes.Find(x => x.ID == brushShapeID);
                    if (brushShape is not null)
                    {
                        if (brushShape is ClippedPolygon)
                        {
                            (_, float distance) = ((ClippedPolygon)brushShape).polygon.GetLineDistance(e.Location);
                            if (distance <= 5)
                            {
                                ((ClippedPolygon)brushShape).color = Variables.brushColor;
                                UpdateBitmap();
                            }
                        }
                        else
                        {
                            brushShape.color = Variables.brushColor;
                            UpdateBitmap();
                        }
                        
                    }
                    break;

                case "radioButtonErase":
                    (int eraseShapeID, _ ) = GetClosestLine(e.Location);
                    Shape eraseShape = Variables.shapes.Find(x => x.ID == eraseShapeID);
                    if (eraseShape is not null)
                    {
                        Variables.shapes.Remove(eraseShape);
                        UpdateBitmap();
                    }
                    break;

                case "radioButtonSelectObject":
                    (int changeShapeID, _ ) = GetClosestLine(e.Location);
                    Shape changeShape = Variables.shapes.Find(x => x.ID == changeShapeID);
                    if (changeShape is not null)
                    {
                        enableObjectSettings(changeShape);
                        Variables.activeElement = changeShapeID;
                    }
                    else
                    {
                        disableObjectSettings();
                        Variables.activeElement = 0;
                    }
                    break;

                case "radioButtonAddClipping":
                    if (Variables.isActive)
                    {
                        if (((ClippedPolygon)Variables.activeShape).activeMode == 1)
                        {
                            ((ClippedPolygon)Variables.activeShape).rectangle.end2 = e.Location;
                            ((ClippedPolygon)Variables.activeShape).activeMode = 2;
                        }
                        else if (((ClippedPolygon)Variables.activeShape).activeMode == 2)
                        {
                            ((ClippedPolygon)Variables.activeShape).polygon = 
                                new Polygon(e.Location, ((ClippedPolygon)Variables.activeShape).thickness, 0);
                            ((ClippedPolygon)Variables.activeShape).activeMode = 3;
                        }
                        else if (((ClippedPolygon)Variables.activeShape).activeMode == 3)
                        {
                            if (CalculatePointToPointDistance(((ClippedPolygon)Variables.activeShape).polygon.vertices[0], e.Location) <= 5)
                            {
                                ((ClippedPolygon)Variables.activeShape).activeMode = 0;
                                Variables.shapes.Add(Variables.activeShape);
                                Variables.isActive = false;
                                UpdateBitmap();
                            }
                            else
                                ((ClippedPolygon)Variables.activeShape).polygon.AddVertex(e.Location);
                        }
                    }
                    else
                    {
                        Variables.activeShape = new ClippedPolygon(e.Location, (int)numericUpDownLineThickness.Value);
                        Variables.isActive = true;
                    }
                    break;

                default:
                    break;
            }
        }

        private void pictureBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            int index, shapeID;
            Shape shape;
            Variables.positionDown = CleanLocation(e.Location);
            switch (Variables.modeName)
            {
                case "radioButtonResizeCircle":
                    (shapeID, index) = GetClosestLine(e.Location);
                    shape = Variables.shapes.Find(x => x.ID == shapeID);
                    if (shape is not null && shape is Circle)
                    {
                        Variables.activeShape = shape;
                        Variables.shapes.Remove(shape);
                        Variables.isActive = true;
                        UpdateBitmap();
                    }
                    break;

                case "radioButtonMovePoint":
                    (shapeID, index) = GetClosestPoint(e.Location);
                    shape = Variables.shapes.Find(x => x.ID == shapeID);
                    if (shape is not null)
                    {
                        Variables.activeElement = index;
                        Variables.activeShape = shape;
                        Variables.shapes.Remove(shape);
                        Variables.isActive = true;
                        UpdateBitmap();
                    }
                    break;

                case "radioButtonMovePolygon":
                    (shapeID, index) = GetClosestLine(e.Location);
                    shape = Variables.shapes.Find(x => x.ID == shapeID);
                    if (shape is not null)
                    {
                        if (shape is Polygon or Rectang)
                        {
                            Variables.activeShape = shape;
                            Variables.shapes.Remove(shape);
                            Variables.isActive = true;
                            UpdateBitmap();
                        }
                        else if (shape is ClippedPolygon)
                        {
                            if (index < 0)
                                ((ClippedPolygon)Variables.activeShape).activeMode = -1;
                            else
                                ((ClippedPolygon)Variables.activeShape).activeMode = -2;
                            Variables.activeShape = shape;
                            Variables.shapes.Remove(shape);
                            Variables.isActive = true;
                            UpdateBitmap();
                        }
                    }
                    break;

                case "radioButtonMoveLine":
                    (shapeID, index) = GetClosestLine(e.Location);
                    shape = Variables.shapes.Find(x => x.ID == shapeID);
                    if (shape is not null)
                    {
                        if (shape is Line)
                        {
                            Variables.activeShape = shape;
                            Variables.shapes.Remove(shape);
                            Variables.isActive = true;
                            UpdateBitmap();
                        }
                        else if (shape is Polygon or Rectang or ClippedPolygon)
                        {
                            Variables.activeShape = shape;
                            Variables.activeElement = index;
                            Variables.shapes.Remove(shape);
                            Variables.isActive = true;
                            UpdateBitmap();
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void pictureBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (Variables.isActive)
            {
                Variables.positionNow = CleanLocation(e.Location);
                switch (Variables.modeName)
                {
                    case "radioButtonAddLine":
                    case "radioButtonAddCircle":
                    case "radioButtonAddRectangle":
                    case "radioButtonAddPolygon":
                    case "radioButtonAddClipping":
                    case "radioButtonResizeCircle":
                        UpdateBitmap();
                        break;

                    case "radioButtonMovePoint":
                        Variables.activeShape.UpdatePoint(Variables.positionNow);
                        UpdateBitmap();
                        break;

                    case "radioButtonMovePolygon":
                        if (Variables.activeShape is ClippedPolygon)
                            ((ClippedPolygon)Variables.activeShape).MoveShape();
                        else if (Variables.activeShape is Polygon)
                            ((Polygon)Variables.activeShape).MoveShape();
                        else if (Variables.activeShape is Rectang)  // double-check
                            ((Rectang)Variables.activeShape).MoveShape();
                        UpdateBitmap();
                        break;

                    case "radioButtonMoveLine":
                        if (Variables.activeShape is ClippedPolygon)
                            ((ClippedPolygon)Variables.activeShape).MoveLine();
                        else if (Variables.activeShape is Polygon)
                            ((Polygon)Variables.activeShape).MoveLine();
                        else if (Variables.activeShape is Rectang)
                            ((Rectang)Variables.activeShape).MoveLine();
                        else if (Variables.activeShape is Line)       // double-check
                            ((Line)Variables.activeShape).MoveLine();
                        UpdateBitmap();
                        break;

                    default:
                        break;
                }
            }
        }

        private void pictureBoxMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (Variables.isActive)
            {
                Point position = CleanLocation(e.Location);
                switch (Variables.modeName)
                {
                    case "radioButtonResizeCircle":
                        ((Circle)Variables.activeShape).radius =
                            ((Circle)Variables.activeShape).CalculateRadius(position);
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                        UpdateBitmap();
                        break;

                    case "radioButtonMovePoint":
                        Variables.activeShape.UpdatePoint(position);
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                        UpdateBitmap();
                        break;

                    case "radioButtonMovePolygon":
                        if (Variables.activeShape is ClippedPolygon)
                        {
                            ((ClippedPolygon)Variables.activeShape).MoveShape();
                            ((ClippedPolygon)Variables.activeShape).activeMode = 0;
                        }
                        else if (Variables.activeShape is Polygon)
                            ((Polygon)Variables.activeShape).MoveShape();
                        else if (Variables.activeShape is Rectang)
                            ((Rectang)Variables.activeShape).MoveShape();

                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                        UpdateBitmap();
                        break;

                    case "radioButtonMoveLine":
                        if (Variables.activeShape is ClippedPolygon)
                            ((ClippedPolygon)Variables.activeShape).MoveLine();
                        else if (Variables.activeShape is Polygon)
                            ((Polygon)Variables.activeShape).MoveLine();
                        else if (Variables.activeShape is Rectang)
                            ((Rectang)Variables.activeShape).MoveLine();
                        else if (Variables.activeShape is Line)
                            ((Line)Variables.activeShape).MoveLine();

                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                        UpdateBitmap();
                        break;

                    default:
                        break;
                }
            }
        }

        private void pictureBoxMain_MouseLeave(object sender, EventArgs e)
        {
            if (Variables.isActive)
            {
                switch (Variables.modeName)
                {
                    case "radioButtonResizeCircle":
                    case "radioButtonMovePoint":
                    case "radioButtonMoveLine":
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                        UpdateBitmap();
                        break;

                    case "radioButtonMovePolygon":
                        if (Variables.activeShape is ClippedPolygon)
                            ((ClippedPolygon)Variables.activeShape).activeMode = 0;
                        Variables.shapes.Add(Variables.activeShape);
                        Variables.isActive = false;
                        UpdateBitmap();
                        break;

                    default:
                        break;
                }
            }
        }

        private void mode_CheckedChanged(object sender, EventArgs e)
        {
            Variables.modeName = ((RadioButton)sender).Name;
            Variables.isActive = false;
            disableObjectSettings();
            UpdateBitmap();
        }
    }

    // DRAWING OBEJCTS
    public abstract class Shape
    {
        public int ID;
        public uint color;
        public int thickness;

        public Shape(int newThickness)
        {
            ID = Variables.lastID + 1;
            Variables.lastID = ID;
            color = Variables.brushColor;
            thickness = newThickness;
        }

        public Shape Clone()
        {
            return (Shape)this.MemberwiseClone();
        }

        public abstract unsafe void DrawShape();
        public abstract unsafe void DrawActiveShape();

        public abstract unsafe void DrawPoints();
        public abstract unsafe void UpdatePoint(Point point);

        public abstract unsafe (int, float) GetPointDistance(Point point);
        public abstract unsafe (int, float) GetLineDistance(Point point);
    }

    public class Line : Shape
    {
        public Point end1;
        public Point end2;

        public Line(Point newEnd, int newThickness) : base(newThickness)
        {
            end1 = newEnd;
        }

        public override unsafe void DrawShape()
        {
            if (Variables.isAntiAliased)
                Form1.AntialiasedLine(end1, end2, color, thickness);
            else if (thickness == 1)
                Form1.MidpointLine(end1, end2, color);
            else
                Form1.BrushLine(end1, end2, color, thickness);
        }

        public override unsafe void DrawActiveShape()
        {
            if (Variables.modeName == "radioButtonMovePoint" 
                || Variables.modeName == "radioButtonMoveLine")
            {
                if (Variables.isAntiAliased)
                    Form1.AntialiasedLine(end1, end2, color, thickness);
                else if (thickness == 1)
                    Form1.MidpointLine(end1, end2, color);
                else
                    Form1.BrushLine(end1, end2, color, thickness);
            }
            else
            {
                if (Variables.isAntiAliased)
                    Form1.AntialiasedLine(end1, Variables.positionNow, color, thickness);
                else if (thickness == 1)
                    Form1.MidpointLine(end1, Variables.positionNow, color);
                else
                    Form1.BrushLine(end1, Variables.positionNow, color, thickness);
            }
        }

        public override unsafe void DrawPoints()
        {
            Form1.DrawPoint(end1);
            Form1.DrawPoint(end2);
        }

        public override unsafe void UpdatePoint(Point point)
        {
            if (Variables.activeElement == 1) 
                end1 = point;
            else 
                end2 = point;
        }

        public override unsafe (int, float) GetPointDistance(Point point)
        {
            float distance1 = Form1.CalculatePointToPointDistance(point, end1);
            float distance2 = Form1.CalculatePointToPointDistance(point, end2);
            if (distance1 < distance2)
                return (1, distance1);
            else
                return (2, distance2);
        }

        public override unsafe (int, float) GetLineDistance(Point point)
        {
            return (1, Form1.CalculatePointToSegmentDistance(point, end1, end2, thickness));
        }

        public unsafe void MoveLine()
        {
            int xShift = Variables.positionNow.X - Variables.positionDown.X;
            int yShift = Variables.positionNow.Y - Variables.positionDown.Y;
            bool caseA = false, caseB = false;
            if (end1.X + xShift >= 0 && end1.X + xShift < Variables.bitmapWidth 
                && end1.Y + yShift >= 0 && end1.Y + yShift < Variables.bitmapHeight) caseA = true;
            if (end2.X + xShift >= 0 && end2.X + xShift < Variables.bitmapWidth 
                && end2.Y + yShift >= 0 && end2.Y + yShift < Variables.bitmapHeight) caseB = true;
            if (caseA && caseB)
            {
                end1.X += xShift;
                end1.Y += yShift;
                end2.X += xShift;
                end2.Y += yShift;
                Variables.positionDown.X = Variables.positionNow.X;
                Variables.positionDown.Y = Variables.positionNow.Y;
            }
        }
    }

    public class Circle : Shape
    {
        public Point center;
        public int radius;

        public Circle(Point newCenter, int newThickness) : base(newThickness)
        {
            center = newCenter;
        }

        public Circle(Point newCenter, Point radiusPoint, int newThickness) : base(newThickness)
        {
            center = newCenter;
            radius = CalculateRadius(radiusPoint);
        }

        public unsafe int CalculateRadius(Point radiusPoint)
        {
            return (int)Math.Sqrt((center.X - radiusPoint.X) * (center.X - radiusPoint.X)
                + (center.Y - radiusPoint.Y) * (center.Y - radiusPoint.Y));
        }

        public override unsafe void DrawShape()
        {
            if (thickness == 1)
                Form1.MidpointCircle(center, radius, color);
            else
                Form1.BrushCircle(center, radius, color, thickness);
        }

        public override unsafe void DrawActiveShape()
        {
            if (Variables.modeName == "radioButtonMovePoint")
            {
                if (thickness == 1)
                    Form1.MidpointCircle(center, radius, color);
                else
                    Form1.BrushCircle(center, radius, color, thickness);
            }
            else
            {
                int currentRadius = CalculateRadius(Variables.positionNow);
                if (thickness == 1)
                    Form1.MidpointCircle(center, currentRadius, color);
                else
                    Form1.BrushCircle(center, currentRadius, color, thickness);
            }
        }

        public override unsafe void DrawPoints()
        {
            Form1.DrawPoint(center);
        }

        public override unsafe void UpdatePoint(Point point)
        {
            center = point;
        }

        public override unsafe (int, float) GetPointDistance(Point point)
        {
            return (1, Form1.CalculatePointToPointDistance(point, center));
        }

        public override unsafe (int, float) GetLineDistance(Point point)
        {
            return (1, Form1.CalculatePointToCircleDistance(point, this));
        }
    }

    public class Rectang : Shape
    {
        public Point end1;
        public Point end2;

        public Rectang(Point newEnd, int newThickness) : base(newThickness)
        {
            end1 = newEnd;
        }

        public Rectang(Point newEnd, int newThickness, uint newColor) : base(newThickness)
        {
            end1 = newEnd;
            color = newColor;
        }

        public override unsafe void DrawShape()
        {
            if (Variables.isAntiAliased)
            {
                Form1.AntialiasedLine(end1, new Point(end1.X, end2.Y), color, thickness);
                Form1.AntialiasedLine(end1, new Point(end2.X, end1.Y), color, thickness);
                Form1.AntialiasedLine(end2, new Point(end1.X, end2.Y), color, thickness);
                Form1.AntialiasedLine(end2, new Point(end2.X, end1.Y), color, thickness);
            }
            else if (thickness == 1)
            {
                Form1.MidpointLine(end1, new Point(end1.X, end2.Y), color);
                Form1.MidpointLine(end1, new Point(end2.X, end1.Y), color);
                Form1.MidpointLine(end2, new Point(end1.X, end2.Y), color);
                Form1.MidpointLine(end2, new Point(end2.X, end1.Y), color);
            }   
            else
            {
                Form1.BrushLine(end1, new Point(end1.X, end2.Y), color, thickness);
                Form1.BrushLine(end1, new Point(end2.X, end1.Y), color, thickness);
                Form1.BrushLine(end2, new Point(end1.X, end2.Y), color, thickness);
                Form1.BrushLine(end2, new Point(end2.X, end1.Y), color, thickness);
            }
        }

        public override unsafe void DrawActiveShape()
        {
            if (Variables.modeName == "radioButtonMovePoint"
                || Variables.modeName == "radioButtonMoveLine"
                || Variables.modeName == "radioButtonMovePolygon")
            {
                DrawShape();
            }
            else
            {
                if (Variables.isAntiAliased)
                {
                    Form1.AntialiasedLine(end1, new Point(end1.X, Variables.positionNow.Y), color, thickness);
                    Form1.AntialiasedLine(end1, new Point(Variables.positionNow.X, end1.Y), color, thickness);
                    Form1.AntialiasedLine(Variables.positionNow, new Point(end1.X, Variables.positionNow.Y), color, thickness);
                    Form1.AntialiasedLine(Variables.positionNow, new Point(Variables.positionNow.X, end1.Y), color, thickness);
                }
                else if (thickness == 1)
                {
                    Form1.MidpointLine(end1, new Point(end1.X, Variables.positionNow.Y), color);
                    Form1.MidpointLine(end1, new Point(Variables.positionNow.X, end1.Y), color);
                    Form1.MidpointLine(Variables.positionNow, new Point(end1.X, Variables.positionNow.Y), color);
                    Form1.MidpointLine(Variables.positionNow, new Point(Variables.positionNow.X, end1.Y), color);
                }
                else
                {
                    Form1.BrushLine(end1, new Point(end1.X, Variables.positionNow.Y), color, thickness);
                    Form1.BrushLine(end1, new Point(Variables.positionNow.X, end1.Y), color, thickness);
                    Form1.BrushLine(Variables.positionNow, new Point(end1.X, Variables.positionNow.Y), color, thickness);
                    Form1.BrushLine(Variables.positionNow, new Point(Variables.positionNow.X, end1.Y), color, thickness);
                }
            }
        }

        public override unsafe void DrawPoints()
        {
            Form1.DrawPoint(end1);
            Form1.DrawPoint(end2);
        }

        public override unsafe void UpdatePoint(Point point)
        {
            if (Variables.activeElement == 1 || Variables.activeElement == -1)
                end1 = point;
            else
                end2 = point;
        }

        public override unsafe (int, float) GetPointDistance(Point point)
        {
            float distance1 = Form1.CalculatePointToPointDistance(point, end1);
            float distance2 = Form1.CalculatePointToPointDistance(point, end2);
            if (distance1 < distance2)
                return (1, distance1);
            else
                return (2, distance2);
        }

        public override unsafe (int, float) GetLineDistance(Point point)
        {
            int bestVertex = 0;
            float distance, bestDistance = float.PositiveInfinity;

            distance = Form1.CalculatePointToSegmentDistance(point, end1, new Point(end1.X, end2.Y), thickness);
            if (distance < bestDistance)
            {
                bestVertex = 0;
                bestDistance = distance;
            }
            distance = Form1.CalculatePointToSegmentDistance(point, end2, new Point(end1.X, end2.Y), thickness);
            if (distance < bestDistance)
            {
                bestVertex = 1;
                bestDistance = distance;
            }
            distance = Form1.CalculatePointToSegmentDistance(point, end2, new Point(end2.X, end1.Y), thickness);
            if (distance < bestDistance)
            {
                bestVertex = 2;
                bestDistance = distance;
            }
            distance = Form1.CalculatePointToSegmentDistance(point, end1, new Point(end2.X, end1.Y), thickness);
            if (distance < bestDistance)
            {
                bestVertex = 3;
                bestDistance = distance;
            }

            return (bestVertex, bestDistance);
        }

        public unsafe void MoveLine()
        {
            int shift, index = Variables.activeElement;
            if (index < 0)
                index = -1 - index;
            if (index == 0 || index == 2)
                shift = Variables.positionNow.X - Variables.positionDown.X;
            else
                shift = Variables.positionNow.Y - Variables.positionDown.Y;

            bool cases = false;
            switch (index)
            {
                case 0: 
                    if (end1.X + shift >= 0 && end1.X + shift < Variables.bitmapWidth) cases = true;
                    break;
                case 1:
                    if (end2.Y + shift >= 0 && end2.Y + shift < Variables.bitmapHeight) cases = true;
                    break;
                case 2:
                    if (end2.X + shift >= 0 && end2.X + shift < Variables.bitmapWidth) cases = true;
                    break;
                case 3:
                    if (end1.Y + shift >= 0 && end1.Y + shift < Variables.bitmapHeight) cases = true;
                    break;
            }

            if (cases)
            {
                switch (index)
                {
                    case 0:
                        end1 = new Point(end1.X + shift, end1.Y);
                        break;
                    case 1:
                        end2 = new Point(end2.X, end2.Y + shift);
                        break;
                    case 2:
                        end2 = new Point(end2.X + shift, end2.Y);
                        break;
                    case 3:
                        end1 = new Point(end1.X, end1.Y + shift);
                        break;
                }
                Variables.positionDown.X = Variables.positionNow.X;
                Variables.positionDown.Y = Variables.positionNow.Y;
            }
        }

        public unsafe void MoveShape()
        {
            int xShift = Variables.positionNow.X - Variables.positionDown.X;
            int yShift = Variables.positionNow.Y - Variables.positionDown.Y;
            bool caseA = false, caseB = false;

            if (end1.X + xShift >= 0 && end1.X + xShift < Variables.bitmapWidth
                && end1.Y + yShift >= 0 && end1.Y + yShift < Variables.bitmapHeight) caseA = true;
            if (end2.X + xShift >= 0 && end2.X + xShift < Variables.bitmapWidth
                && end2.Y + yShift >= 0 && end2.Y + yShift < Variables.bitmapHeight) caseB = true;
            if (caseA && caseB)
            {
                end1.X += xShift;
                end1.Y += yShift;
                end2.X += xShift;
                end2.Y += yShift;
                Variables.positionDown.X = Variables.positionNow.X;
                Variables.positionDown.Y = Variables.positionNow.Y;
            }
        }
    }

    public class Polygon : Shape
    {
        public List<Point> vertices;
        public uint[,] image;
        public int fillMode;

        public Polygon(Point newPoint, int newThickness, int fillMode = 0) : base(newThickness)
        {
            vertices = new List<Point>();
            vertices.Add(newPoint);

            this.fillMode = fillMode;
            if (fillMode == 2)
            {
                if (Variables.drawingImage is not null)
                    image = (uint[,])Variables.drawingImage.Clone();
                else
                    image = null;
            }
        }

        public void AddVertex(Point newPoint)
        {
            vertices.Add(newPoint);
        }
// change
        public override unsafe void DrawShape()
        {
            if (Variables.isAntiAliased)
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.AntialiasedLine(vertices[i], vertices[i + 1], color, thickness);
            }
            else if (thickness == 1)
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.MidpointLine(vertices[i], vertices[i + 1], color);
            }
            else
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.BrushLine(vertices[i], vertices[i + 1], color, thickness);
            }

            if (Variables.isAntiAliased)
                Form1.AntialiasedLine(vertices[vertices.Count() - 1], vertices[0], color, thickness);
            else if (thickness == 1)
                Form1.MidpointLine(vertices[vertices.Count() - 1], vertices[0], color);
            else
                Form1.BrushLine(vertices[vertices.Count() - 1], vertices[0], color, thickness);

            // IF MODEPOLYGON
            if (fillMode > 0)
            {
                Form1.fillPolygon(this);
            }
        }
// change
        public override unsafe void DrawActiveShape()
        {
            if (Variables.isAntiAliased)
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.AntialiasedLine(vertices[i], vertices[i + 1], color, thickness);
            } 
            else if (thickness == 1)
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.MidpointLine(vertices[i], vertices[i + 1], color);
            }
            else
            {
                for (int i = 0; i < vertices.Count() - 1; i++)
                    Form1.BrushLine(vertices[i], vertices[i + 1], color, thickness);
            }

            if (Variables.modeName == "radioButtonMovePoint" 
                || Variables.modeName == "radioButtonMoveLine"
                || Variables.modeName == "radioButtonMovePolygon")
            {
                if (Variables.isAntiAliased)
                    Form1.AntialiasedLine(vertices[vertices.Count() - 1], vertices[0], color, thickness);
                else if (thickness == 1)
                    Form1.MidpointLine(vertices[vertices.Count() - 1], vertices[0], color);
                else
                    Form1.BrushLine(vertices[vertices.Count() - 1], vertices[0], color, thickness);

                // IF MODEPOLYGON
                if (fillMode > 0)
                {
                    Form1.fillPolygon(this);
                }
            }
            else
            {
                if (Variables.isAntiAliased)
                    Form1.AntialiasedLine(vertices[vertices.Count() - 1], Variables.positionNow, color, thickness);
                else if (thickness == 1)
                    Form1.MidpointLine(vertices[vertices.Count() - 1], Variables.positionNow, color);
                else
                    Form1.BrushLine(vertices[vertices.Count() - 1], Variables.positionNow, color, thickness);

                Form1.DrawPoint(vertices[0]);
            }
        }
// TODO
        public unsafe void FillWithColour()
        {

        }
// TODO
        public unsafe void FillWithImage()
        {

        }

        public override unsafe void DrawPoints()
        {
            for (int i = 0; i < vertices.Count(); i++)
            {
                Form1.DrawPoint(vertices[i]);
            }
        }

        public override unsafe void UpdatePoint(Point point)
        {
            vertices[Variables.activeElement] = point;
        }

        public override unsafe (int, float) GetPointDistance(Point point)
        {
            int bestVertex = 0;
            float distance;
            float bestDistance = float.PositiveInfinity;
            for (int i = 0; i < vertices.Count; i++)
            {
                distance = Form1.CalculatePointToPointDistance(point, vertices[i]);
                if (distance < bestDistance)
                {
                    bestVertex = i;
                    bestDistance = distance;
                }
            }
            return (bestVertex, bestDistance);
        }

        public override unsafe (int, float) GetLineDistance(Point point)
        {
            int bestVertex = 0;
            float distance;
            float bestDistance = float.PositiveInfinity;
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                distance = Form1.CalculatePointToSegmentDistance(point, vertices[i], vertices[i + 1], thickness);
                if (distance < bestDistance)
                {
                    bestVertex = i;
                    bestDistance = distance;
                }
            }

            distance = Form1.CalculatePointToSegmentDistance(point, vertices[vertices.Count - 1], vertices[0], thickness);
            if (distance < bestDistance)
            {
                bestVertex = vertices.Count - 1;
                bestDistance = distance;
            }
            return (bestVertex, bestDistance);
        }

        public unsafe void MoveLine()
        {
            int xShift = Variables.positionNow.X - Variables.positionDown.X;
            int yShift = Variables.positionNow.Y - Variables.positionDown.Y;
            int indexA = Variables.activeElement;
            int indexB;
            if (indexA == vertices.Count() - 1)
                indexB = 0;
            else indexB = indexA + 1;

            bool caseA = false, caseB = false;
            if (vertices[indexA].X + xShift >= 0 && vertices[indexA].X + xShift < Variables.bitmapWidth
                && vertices[indexA].Y + yShift >= 0 && vertices[indexA].Y + yShift < Variables.bitmapHeight) caseA = true;
            if (vertices[indexB].X + xShift >= 0 && vertices[indexB].X + xShift < Variables.bitmapWidth
                && vertices[indexB].Y + yShift >= 0 && vertices[indexB].Y + yShift < Variables.bitmapHeight) caseB = true;
            if (caseA && caseB)
            {
                vertices[indexA] = new Point(vertices[indexA].X + xShift, vertices[indexA].Y + yShift);
                vertices[indexB] = new Point(vertices[indexB].X + xShift, vertices[indexB].Y + yShift);
                Variables.positionDown.X = Variables.positionNow.X;
                Variables.positionDown.Y = Variables.positionNow.Y;
            }
        }

        public unsafe void MoveShape()
        {
            int xShift = Variables.positionNow.X - Variables.positionDown.X;
            int yShift = Variables.positionNow.Y - Variables.positionDown.Y;
            bool cases = true;
            for (int i = 0; i < vertices.Count(); i++)
            {
                if (vertices[i].X + xShift < 0 || vertices[i].X + xShift >= Variables.bitmapWidth
                    || vertices[i].Y + yShift < 0 || vertices[i].Y + yShift >= Variables.bitmapHeight)
                {
                    cases = false;
                    break;
                }
            }
            if (cases)
            {
                for (int i = 0; i < vertices.Count(); i++)
                {
                    vertices[i] = new Point(vertices[i].X + xShift, vertices[i].Y + yShift);
                }
                Variables.positionDown.X = Variables.positionNow.X;
                Variables.positionDown.Y = Variables.positionNow.Y;
            }
        }
    }

    public class ClippedPolygon : Shape
    {
        public Rectang rectangle;
        public Polygon polygon;
        public int activeMode;

        public ClippedPolygon(Point newPoint, int newThickness) : base(newThickness)
        {
            rectangle = new Rectang(newPoint, 1, 4278190335);
            activeMode = 1;
        }

        public unsafe (List<(Point, Point)>, List<(Point, Point)>) GetCSLines()
        {
            Point point1, point2;
            List<(Point, Point)> inside = new List<(Point, Point)>();
            List<(Point, Point)> outside = new List<(Point, Point)>();

            for (int i = 0; i < polygon.vertices.Count - 1; i++)
            {
                (point1, point2) = Form1.DrawCohenSutherlandLine(polygon.vertices[i], polygon.vertices[i + 1], rectangle);
                if (point1.X == -1)
                    outside.Add((polygon.vertices[i], polygon.vertices[i + 1]));
                else
                {
                    if (!point1.Equals(polygon.vertices[i]))
                        outside.Add((polygon.vertices[i], point1));
                    if (!point2.Equals(polygon.vertices[i + 1]))
                        outside.Add((polygon.vertices[i + 1], point2));
                    inside.Add((point1, point2));
                }
            }

            (point1, point2) = Form1.DrawCohenSutherlandLine(polygon.vertices[polygon.vertices.Count - 1], 
                polygon.vertices[0], rectangle);
            if (point1.X == -1)
                outside.Add((polygon.vertices[polygon.vertices.Count - 1], polygon.vertices[0]));
            else
            {
                if (!point1.Equals(polygon.vertices[polygon.vertices.Count - 1]))
                    outside.Add((polygon.vertices[polygon.vertices.Count - 1], point1));
                if (!point2.Equals(polygon.vertices[0]))
                    outside.Add((polygon.vertices[0], point2));
                inside.Add((point1, point2));
            }

            return (inside, outside);
        }

        public override unsafe void DrawShape()
        {
            polygon.thickness = thickness;
            polygon.color = color;
            rectangle.DrawShape();

            //polygon.DrawShape() alternative:
            List<(Point, Point)> inside;
            List<(Point, Point)> outside;
            (inside, outside) = GetCSLines();

            foreach((Point point1, Point point2) in outside)
            {
                if (Variables.isAntiAliased)
                    Form1.AntialiasedLine(point1, point2, color, thickness);
                else if (thickness == 1)
                    Form1.MidpointLine(point1, point2, color);
                else
                    Form1.BrushLine(point1, point2, color, thickness);
            }

            foreach ((Point point1, Point point2) in inside)
            {
                if (Variables.isAntiAliased)
                    Form1.AntialiasedLine(point1, point2, 4278190335, thickness);
                else if (thickness == 1)
                    Form1.MidpointLine(point1, point2, 4278190335);
                else
                    Form1.BrushLine(point1, point2, 4278190335, thickness);  // blue
            }

        }

        public override unsafe void DrawActiveShape()
        {
            switch (activeMode)
            {
                case -2:
                case -1:
                case 0:
                    DrawShape();
                    break;
                case 1:
                    rectangle.DrawActiveShape();
                    break;
                case 2:
                    rectangle.DrawShape();
                    break;
                case 3:
                    rectangle.DrawShape();

                    polygon.color = color;
                    polygon.DrawActiveShape();
                    break;
            }
        }

        public override unsafe void DrawPoints()
        {
            rectangle.DrawPoints();
            polygon.DrawPoints();
        }

        public override unsafe void UpdatePoint(Point point)
        {
            if (Variables.activeElement < 0)
                rectangle.UpdatePoint(point);
            else
                polygon.UpdatePoint(point);
        }

        public override unsafe (int, float) GetPointDistance(Point point)
        {
            (int rIndex, float rDistance) = rectangle.GetPointDistance(point);
            (int pIndex, float pDistance) = polygon.GetPointDistance(point);
            if (rDistance < pDistance)
                return (-rIndex, rDistance);
            else
                return (pIndex, pDistance);
        }

        public override unsafe (int, float) GetLineDistance(Point point)
        {
            (int rIndex, float rDistance) = rectangle.GetLineDistance(point);
            (int pIndex, float pDistance) = polygon.GetLineDistance(point);
            if (rDistance < pDistance)
                return (-rIndex - 1, rDistance);
            else
                return (pIndex, pDistance);
        }

        public unsafe void MoveLine()
        {
            int index = Variables.activeElement;
            if (index < 0)
                rectangle.MoveLine();
            else
                polygon.MoveLine();
        }

        public unsafe void MoveShape()
        {
            if (activeMode == -1)
                rectangle.MoveShape();
            else if (activeMode == -2)
                polygon.MoveShape();
        }
    }

    public unsafe static class Variables
    {
        public unsafe static Bitmap bitmap;
        public unsafe static Point positionDown;
        public unsafe static Point positionNow;
        public unsafe static uint brushColor = 4278190080;  // black
        public unsafe static string modeName = "radioButtonAddLine";
        public unsafe static string modePolygon = "Empty";
        public unsafe static string modeobject;
        public unsafe static bool isAntiAliased = false;
        public unsafe static bool isActive = false;
        public unsafe static int lastID = 0;

        public unsafe static List<Shape> shapes = new List<Shape>();
        public unsafe static Shape activeShape;
        public unsafe static int activeElement;

        public unsafe static int bitmapHeight = 800;
        public unsafe static int bitmapWidth = 600;
        public unsafe static uint[,] bitmapArray = new uint[bitmapWidth, bitmapHeight];
        public unsafe static uint[,] drawingImage;
        public unsafe static uint[,] objectImage;
        public unsafe static bool[,] pointsKernel = new bool[9, 9]
         {
            { false, false, true, true, true, true, true, false, false },
            { false, true, true, true, true, true, true, true, false },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true, true },
            { false, true, true, true, true, true, true, true, false },
            { false, false, true, true, true, true, true, false, false }
         };
    }
}
