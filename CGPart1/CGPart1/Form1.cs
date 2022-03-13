using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Szymon Górski, 298796

namespace CGPart1
{
    public partial class CGPart1 : Form
    {
        public CGPart1()
        {
            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < 9; j++)
            //    {
            //        if (i <= 2 && j <= 2) Variables.kernel[i, j] = 1;
            //        else Variables.kernel[i, j] = 0;
            //    }
            //}

            InitializeComponent();
            savePicture.DefaultExt = "png";
            savePicture.Filter =
                "PNG files (*.png)|*.png|All files (*.*)|*.*";
            openPicture.Filter =
                "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
        }

        //public void createTable()
        //{
        //    DataTable table = new DataTable;
        //    table.Columns.Add
        //    BindingSource bindingSource1 = new BindingSource();
        //    bindingSource1.DataSource = Variables.kernel;
        //    dataGrid.DataSource = bindingSource1;
        //    dataGrid.Refresh();
        //}

        public void lockFn()
        {
            buttonSave.Enabled = false;
            buttonReset.Enabled = false;
            buttonLoad.Enabled = false;
            buttonEmboss.Enabled = false;
            buttonDetectEdges.Enabled = false;
            buttonSharpen.Enabled = false;
            buttonGaussianBlur.Enabled = false;
            buttonBlur.Enabled = false;
            buttonCorrectGammaUp.Enabled = false;
            buttonCorrectGammaDown.Enabled = false;
            buttonEnhanceContrast.Enabled = false;
            buttonCorrectBrightnessUp.Enabled = false;
            buttonCorrectBrightnessDown.Enabled = false;
            buttonInvert.Enabled = false;
            numericAnchorColumn.Enabled = false;
            numericAnchorRow.Enabled = false;
            numericSizeColumns.Enabled = false;
            numericSizeRows.Enabled = false;
            dataGrid.Enabled = false;
        }

        public void unlockFn()
        {
            buttonSave.Enabled = true;
            buttonReset.Enabled = true;
            buttonLoad.Enabled = true;
            buttonEmboss.Enabled = true;
            buttonDetectEdges.Enabled = true;
            buttonSharpen.Enabled = true;
            buttonGaussianBlur.Enabled = true;
            buttonBlur.Enabled = true;
            buttonCorrectGammaUp.Enabled = true;
            buttonCorrectGammaDown.Enabled = true;
            buttonEnhanceContrast.Enabled = true;
            buttonCorrectBrightnessUp.Enabled = true;
            buttonCorrectBrightnessDown.Enabled = true;
            buttonInvert.Enabled = true;
            numericAnchorColumn.Enabled = true;
            numericAnchorRow.Enabled = true;
            numericSizeColumns.Enabled = true;
            numericSizeRows.Enabled = true;
            dataGrid.Enabled = true;
        }

        public int fastMin(int a, int b)
        {
            unsafe
            {
                if (a > b) return b;
                else return a;
            }
        }

        public int fastMax(int a, int b)
        {
            unsafe
            {
                if (a > b) return a;
                else return b;
            }
        }

        public void invert(int height, int width)
        {
            lockFn();
            unsafe
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = (byte)(255 - Variables.colors[0, i, j]);
                        Variables.colors[1, i, j] = (byte)(255 - Variables.colors[1, i, j]);
                        Variables.colors[2, i, j] = (byte)(255 - Variables.colors[2, i, j]);
                    }
                }
            }
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonInvert_Click(object sender, EventArgs e)
        {
            invert(Variables.P_Height, Variables.P_Width);
        }

        public void correctBrightnessUp(int height, int width)
        {
            lockFn();
            unsafe
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = (byte)fastMin(255, Variables.colors[0, i, j] + Variables.BC_Constant);
                        Variables.colors[1, i, j] = (byte)fastMin(255, Variables.colors[1, i, j] + Variables.BC_Constant);
                        Variables.colors[2, i, j] = (byte)fastMin(255, Variables.colors[2, i, j] + Variables.BC_Constant);
                    }
                }
            }
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonCorrectBrightnessUp_Click(object sender, EventArgs e)
        {
            correctBrightnessUp(Variables.P_Height, Variables.P_Width);
        }

        public void correctBrightnessDown(int height, int width)
        {
            lockFn();
            unsafe
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = (byte)fastMax(0, Variables.colors[0, i, j] - Variables.BC_Constant);
                        Variables.colors[1, i, j] = (byte)fastMax(0, Variables.colors[1, i, j] - Variables.BC_Constant);
                        Variables.colors[2, i, j] = (byte)fastMax(0, Variables.colors[2, i, j] - Variables.BC_Constant);
                    }
                }
            }
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonCorrectBrightnessDown_Click(object sender, EventArgs e)
        {
            correctBrightnessDown(Variables.P_Height, Variables.P_Width);
        }

        public void enhanceContrast(int height, int width)
        {
            lockFn();
            unsafe
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = (byte)fastMin(255, fastMax(0, (int)((Variables.colors[0, i, j] - 127) * Variables.CE_Constant + 127)));
                        Variables.colors[1, i, j] = (byte)fastMin(255, fastMax(0, (int)((Variables.colors[1, i, j] - 127) * Variables.CE_Constant + 127)));
                        Variables.colors[2, i, j] = (byte)fastMin(255, fastMax(0, (int)((Variables.colors[2, i, j] - 127) * Variables.CE_Constant + 127)));
                    }
                }
            }
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonEnhanceContrast_Click(object sender, EventArgs e)
        {
            enhanceContrast(Variables.P_Height, Variables.P_Width);
        }

        public void correctGammaUp(int height, int width)
        {
            lockFn();
            unsafe
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = (byte)(255 * Math.Pow((double)Variables.colors[0, i, j] / 255, Variables.GCUp_Constant));
                        Variables.colors[1, i, j] = (byte)(255 * Math.Pow((double)Variables.colors[1, i, j] / 255, Variables.GCUp_Constant));
                        Variables.colors[2, i, j] = (byte)(255 * Math.Pow((double)Variables.colors[2, i, j] / 255, Variables.GCUp_Constant));
                    }
                }
            }
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonCorrectGammaUp_Click(object sender, EventArgs e)
        {
            correctGammaUp(Variables.P_Height, Variables.P_Width);
        }

        public void correctGammaDown(int height, int width)
        {
            lockFn();
            unsafe
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = (byte)(255 * Math.Pow((double)Variables.colors[0, i, j] / 255, Variables.GCDown_Constant));
                        Variables.colors[1, i, j] = (byte)(255 * Math.Pow((double)Variables.colors[1, i, j] / 255, Variables.GCDown_Constant));
                        Variables.colors[2, i, j] = (byte)(255 * Math.Pow((double)Variables.colors[2, i, j] / 255, Variables.GCDown_Constant));
                    }
                }
            }
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonCorrectGammaDown_Click(object sender, EventArgs e)
        {
            correctGammaDown(Variables.P_Height, Variables.P_Width);
        }

        public void convolutionFn(int height, int width, int[,] kernel, int kHeight, int kWidth, int aRow, int aColumn, int customWeight)
        {                                                                // kHeight - kernel height, aRow - anchor's row
            unsafe
            {
                int[,,] colorsCopy = new int[3, height, width]; // declare zeros?
                int weight;

                if (customWeight != 0)
                {
                    weight = customWeight;
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            for (int ki = fastMax(0, aRow - 1 - i); ki < fastMin(kHeight, height - i + aRow - 1); ki++)
                            {
                                for (int kj = fastMax(0, aColumn - 1 - j); kj < fastMin(kWidth, width - j + aColumn - 1); kj++)
                                {
                                    colorsCopy[0, i, j] += Variables.colors[0, i + ki - aRow + 1, j + kj - aColumn + 1] * kernel[ki, kj];
                                    colorsCopy[1, i, j] += Variables.colors[1, i + ki - aRow + 1, j + kj - aColumn + 1] * kernel[ki, kj];
                                    colorsCopy[2, i, j] += Variables.colors[2, i + ki - aRow + 1, j + kj - aColumn + 1] * kernel[ki, kj];
                                }
                            }
                            colorsCopy[0, i, j] /= weight;
                            colorsCopy[1, i, j] /= weight;
                            colorsCopy[2, i, j] /= weight;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            weight = 0;
                            for (int ki = fastMax(0, aRow - 1 - i); ki < fastMin(kHeight, height - i + aRow - 1); ki++)
                            {
                                for (int kj = fastMax(0, aColumn - 1 - j); kj < fastMin(kWidth, width - j + aColumn - 1); kj++)
                                {
                                    colorsCopy[0, i, j] += Variables.colors[0, i + ki - aRow + 1, j + kj - aColumn + 1] * kernel[ki, kj];
                                    colorsCopy[1, i, j] += Variables.colors[1, i + ki - aRow + 1, j + kj - aColumn + 1] * kernel[ki, kj];
                                    colorsCopy[2, i, j] += Variables.colors[2, i + ki - aRow + 1, j + kj - aColumn + 1] * kernel[ki, kj];
                                    weight += kernel[ki, kj];
                                }
                            }
                            if (weight == 0) weight = 1;
                            colorsCopy[0, i, j] /= weight;
                            colorsCopy[1, i, j] /= weight;
                            colorsCopy[2, i, j] /= weight;
                        }
                    }
                }
                
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = (byte)colorsCopy[0, i, j];
                        Variables.colors[1, i, j] = (byte)colorsCopy[1, i, j];
                        Variables.colors[2, i, j] = (byte)colorsCopy[2, i, j];
                    }
                }
            }
        }

        public void blur()
        {
            int[,] kernel = new int[3, 3]
                {
                    {1, 1, 1},
                    {1, 1, 1},
                    {1, 1, 1}
                };

            lockFn();
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonBlur_Click(object sender, EventArgs e)
        {
            blur();
        }

        public void gaussianBlur()
        {
            int[,] kernel = new int[3, 3]
                {
                    {0, 1, 0},
                    {1, 4, 1},
                    {0, 1, 0}
                };

            lockFn();
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonGaussianBlur_Click(object sender, EventArgs e)
        {
            gaussianBlur();
        }

        public void sharpen()
        {
            int[,] kernel = new int[3, 3]
                {
                    {0, -1, 0},
                    {-1, 5, -1},
                    {0, -1, 0}
                };

            lockFn();
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonSharpen_Click(object sender, EventArgs e)
        {
            sharpen();
        }

        public void detectEdges()
        {
            int[,] kernel = new int[3, 3]
                {
                    {-1, 0, 0},
                    {0, 1, 0},
                    {0, 0, 0}
                };

            lockFn();
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonDetectEdges_Click(object sender, EventArgs e)
        {
            detectEdges();
        }

        public void emboss()
        {
            int[,] kernel = new int[3, 3]
                {
                    {-1, -1, 0},
                    {-1, 1, 1},
                    {0, 1, 1}
                };

            lockFn();
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonEmboss_Click(object sender, EventArgs e)
        {
            emboss();
        }

        // values outside of edges are ignored to obtain better performance
        //public void custom(int height, int width, byte[][] red, byte[][] green, byte[][] blue)
        //{
        //    unsafe
        //    {
        //        double result;
        //        int kiStart, kiEnd, kjStart, kjEnd;
        //        // byte newRed[][];
        //        for (int i = 0; i < height; i++)
        //        {
        //            for (int j = 0; j < width; i++)
        //            {
        //                result = 0;

        //                if (i + 1 - (int)numericAnchorRow.Value >= 0) kiStart = 0;
        //                else kiStart = (int)numericAnchorRow.Value - i - 1;
        //                if (j + 1 - (int)numericAnchorColumn.Value >= 0) kjStart = 0;
        //                else kjStart = (int)numericAnchorColumn.Value - j - 1;
        //                if (i + (int)numericSizeRows.Value - (int)numericAnchorRow.Value < height) kiEnd = (int)numericSizeRows.Value;
        //                else kiEnd = (int)numericAnchorRow.Value + height - i - 1;
        //                if (j + (int)numericSizeColumns.Value - (int)numericAnchorColumn.Value < width) kjEnd = (int)numericSizeColumns.Value;
        //                else kjEnd = (int)numericAnchorColumn.Value + width - j - 1;

        //                for (int ki = kiStart; ki < kiEnd; ki++)
        //                {
        //                    for (int kj = kjStart; kj < kjEnd; kj++)
        //                    {
        //                        result += red[i + ki + 1 - (int)numericAnchorRow.Value][j + kj + 1 - (int)numericAnchorColumn.Value]; // * sth
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        public void loadOriginal()
        {
            Bitmap bmp = new Bitmap(Variables.P_Width, Variables.P_Height, Variables.pixelFormat);
            BitmapData bits = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);
            unsafe
            {
                for (int i = 0; i < Variables.P_Height; i++)
                {
                    int* row = (int*)((byte*)bits.Scan0 + (i * bits.Stride));

                    if (bmp.PixelFormat == PixelFormat.Format32bppArgb)
                    {
                        for (int j = 0; j < Variables.P_Width; j++)
                        {
                            row[j] = 0xFF << 24 | (Variables.colorsSave[2, i, j] << 16) | (Variables.colorsSave[1, i, j] << 8) | Variables.colorsSave[0, i, j];
                        }
                    }
                    else if (bmp.PixelFormat == PixelFormat.Format32bppRgb || bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    {
                        for (int j = 0; j < Variables.P_Width; j++)
                        {
                            row[j] = (Variables.colorsSave[2, i, j] << 16) | (Variables.colorsSave[1, i, j] << 8) | Variables.colorsSave[0, i, j];
                        }
                    }
                }
            }
            bmp.UnlockBits(bits);

            unsafe
            {
                for (int i = 0; i < Variables.P_Height; i++)
                {
                    for (int j = 0; j < Variables.P_Width; j++)
                    {
                        Variables.colors[0, i, j] = Variables.colorsSave[0, i, j];
                        Variables.colors[1, i, j] = Variables.colorsSave[1, i, j];
                        Variables.colors[2, i, j] = Variables.colorsSave[2, i, j];
                    }
                }
            }
            Variables.bitmap = bmp;
        }

        public void loadModification()
        {
            Bitmap bmp = new Bitmap(Variables.P_Width, Variables.P_Height, Variables.pixelFormat);
            BitmapData bits = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);
            unsafe
            {
                for (int i = 0; i < Variables.P_Height; i++)
                {
                    int* row = (int*)((byte*)bits.Scan0 + (i * bits.Stride));

                    if (bmp.PixelFormat == PixelFormat.Format32bppArgb)
                    {
                        for (int j = 0; j < Variables.P_Width; j++)
                        {
                            row[j] = 0xFF << 24 | (Variables.colors[2, i, j] << 16) | (Variables.colors[1, i, j] << 8) | Variables.colors[0, i, j];
                        }
                    }
                    else if (bmp.PixelFormat == PixelFormat.Format32bppRgb || bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    {
                        for (int j = 0; j < Variables.P_Width; j++)
                        {
                            row[j] = (Variables.colors[2, i, j] << 16) | (Variables.colors[1, i, j] << 8) | Variables.colors[0, i, j];
                        }
                    }
                }
            }
            bmp.UnlockBits(bits);
            Variables.bitmap = bmp;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (openPicture.ShowDialog() == DialogResult.OK)
            {
                lockFn();
                Bitmap img = new Bitmap(Image.FromFile(openPicture.FileName));
                Variables.colors = new byte[3, img.Height, img.Width];
                Variables.colorsSave = new byte[3, img.Height, img.Width];

                BitmapData bits = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),
                    ImageLockMode.ReadOnly, img.PixelFormat);

                unsafe
                {
                    uint conv; // this method allows to work with both RGB and ARGB files
                               // however, the alpha layer is not modified by the program
                    for (int i = 0; i < img.Height; i++)
                    {
                        int* row = (int*)((byte*)bits.Scan0 + (i * bits.Stride));
                        for (int j = 0; j < img.Width; j++)
                        {
                            // row[x] = data[y * width + x];
                            conv = (uint)row[j];
                            Variables.colorsSave[0, i, j] = (byte)(conv - ((conv >> 8) << 8)); conv >>= 8;
                            Variables.colorsSave[1, i, j] = (byte)(conv - ((conv >> 8) << 8)); conv >>= 8;
                            Variables.colorsSave[2, i, j] = (byte)(conv - ((conv >> 8) << 8)); conv >>= 8;
                        }
                    }
                }
                img.UnlockBits(bits);

                Variables.P_Height = img.Height;
                Variables.P_Width = img.Width;
                Variables.pixelFormat = img.PixelFormat;

                loadOriginal();
                img.Dispose();
                
                pictureOriginal.Image = Variables.bitmap;
                pictureModified.Image = Variables.bitmap;
                unlockFn();
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            lockFn();
            Variables.bitmap.Dispose();
            loadOriginal();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(savePicture.ShowDialog() == DialogResult.OK)
            {
                lockFn();
                pictureModified.Image.Save(savePicture.FileName);
                unlockFn();
            }
        }

        private void numericSizeRows_ValueChanged(object sender, EventArgs e)
        {
            if (numericSizeRows.Value < numericAnchorRow.Value)
            {
                numericAnchorRow.Value = numericSizeRows.Value;
            }
            numericAnchorRow.Maximum = numericSizeRows.Value;
        }

        private void numericSizeColumns_ValueChanged(object sender, EventArgs e)
        {
            if (numericSizeColumns.Value < numericAnchorColumn.Value)
            {
                numericAnchorColumn.Value = numericSizeColumns.Value;
            }
            numericAnchorColumn.Maximum = numericSizeColumns.Value;
        }
    }

    static class Variables
    {
        public static byte BC_Constant = 5;             // brightness correction constant (>=1)
        public static double GCDown_Constant = 1.05;    // decoding gamma correction contrast (>1.0)
        public static double GCUp_Constant = 0.95;      // encoding gamma correction contrast (<1.0)
        public static double CE_Constant = 1.1;         // contrast enhancement slope (>1.0)
        public static int P_Height;                     // picture height
        public static int P_Width;                      // picture width
        public static byte[,,] colors;
        public static byte[,,] colorsSave;
        public static Bitmap bitmap;
        public static PixelFormat pixelFormat;
    }
}