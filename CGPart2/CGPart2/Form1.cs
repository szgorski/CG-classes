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

namespace CGPart2
{
    public partial class CGPart2 : Form
    {
        public CGPart2()
        {
            InitializeComponent();

            Variables.customKernel = new int[9, 9];
            setKernel();

            savePicture.DefaultExt = "png";
            savePicture.Filter =
                "PNG files (*.png)|*.png|All files (*.*)|*.*";
            openPicture.Filter =
                "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
        }

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
            buttonMedianFilter.Enabled = false;
            buttonApplyConvolution.Enabled = false;
            buttonAtkinson.Enabled = false;
            buttonBurkes.Enabled = false;
            buttonFloydAndSteinberg.Enabled = false;
            buttonSierra.Enabled = false;
            buttonStucky.Enabled = false;
            buttonApplyQuantization.Enabled = false;
            buttonGreyscale.Enabled = false;
            numericAnchorColumn.Enabled = false;
            numericAnchorRow.Enabled = false;
            numericSizeColumns.Enabled = false;
            numericSizeRows.Enabled = false;
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
            buttonMedianFilter.Enabled = true;
            buttonApplyConvolution.Enabled = true;
            buttonAtkinson.Enabled = true;
            buttonBurkes.Enabled = true;
            buttonFloydAndSteinberg.Enabled = true;
            buttonSierra.Enabled = true;
            buttonStucky.Enabled = true;
            buttonApplyQuantization.Enabled = true;
            buttonGreyscale.Enabled = true;
            numericAnchorColumn.Enabled = true;
            numericAnchorRow.Enabled = true;
            numericSizeColumns.Enabled = true;
            numericSizeRows.Enabled = true;
        }

        // Part 1

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

        public void convolutionFn(int height, int width, int[,] kernel, 
            int kHeight, int kWidth, int aRow, int aColumn, int customWeight, int offset)
        {                                                                // kHeight - kernel height, aRow - anchor's row
            unsafe
            {
                int[,,] colorsCopy = new int[3, height, width];
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
                            colorsCopy[0, i, j] = offset + colorsCopy[0, i, j] / weight;
                            colorsCopy[1, i, j] = offset + colorsCopy[1, i, j] / weight;
                            colorsCopy[2, i, j] = offset + colorsCopy[2, i, j] / weight;
                        }
                    }
                }

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = (byte)fastMin(255, fastMax(0, colorsCopy[0, i, j]));
                        Variables.colors[1, i, j] = (byte)fastMin(255, fastMax(0, colorsCopy[1, i, j]));
                        Variables.colors[2, i, j] = (byte)fastMin(255, fastMax(0, colorsCopy[2, i, j]));
                    }
                }
            }
        }

        // might be merged into one function
        public void blur()
        {
            int[,] kernel = new int[3, 3]
                {
                    {1, 1, 1},
                    {1, 1, 1},
                    {1, 1, 1}
                };

            lockFn();
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0, 0);
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
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0, 0);
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
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0, 0);
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
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0, 127);
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
            convolutionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, 0, 0);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonEmboss_Click(object sender, EventArgs e)
        {
            emboss();
        }

        public void medianFilter(int height, int width, int kHeight, int kWidth, int aRow, int aColumn)
        {                                                // kHeight - kernel height, aRow - anchor's row
            lockFn();
            unsafe
            {
                int counter;
                byte[][] medianTable = new byte[3][];
                medianTable[0] = new byte[kHeight * kWidth];
                medianTable[1] = new byte[kHeight * kWidth];
                medianTable[2] = new byte[kHeight * kWidth];
                byte[,,] colorsCopy = new byte[3, height, width];

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        counter = 0;
                        for (int ki = fastMax(0, aRow - 1 - i); ki < fastMin(kHeight, height - i + aRow - 1); ki++)
                        {
                            for (int kj = fastMax(0, aColumn - 1 - j); kj < fastMin(kWidth, width - j + aColumn - 1); kj++)
                            {
                                medianTable[0][counter] = Variables.colors[0, i + ki - aRow + 1, j + kj - aColumn + 1];
                                medianTable[1][counter] = Variables.colors[1, i + ki - aRow + 1, j + kj - aColumn + 1];
                                medianTable[2][counter] = Variables.colors[2, i + ki - aRow + 1, j + kj - aColumn + 1];
                                counter++;
                            }
                        }
                        Array.Sort(medianTable[0], 0, counter);
                        Array.Sort(medianTable[1], 0, counter);
                        Array.Sort(medianTable[2], 0, counter);

                        colorsCopy[0, i, j] = medianTable[0][counter / 2];
                        colorsCopy[1, i, j] = medianTable[1][counter / 2];
                        colorsCopy[2, i, j] = medianTable[2][counter / 2];
                    }
                }

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = colorsCopy[0, i, j];
                        Variables.colors[1, i, j] = colorsCopy[1, i, j];
                        Variables.colors[2, i, j] = colorsCopy[2, i, j];
                    }
                }
            }

            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonMedianFilter_Click(object sender, EventArgs e)
        {
            medianFilter(Variables.P_Height, Variables.P_Width, 3, 3, 2, 2);
        }

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
                            row[j] = 0xFF << 24 | (Variables.colorsSave[0, i, j] << 16) | (Variables.colorsSave[1, i, j] << 8) | Variables.colorsSave[2, i, j];
                        }
                    }
                    else if (bmp.PixelFormat == PixelFormat.Format32bppRgb || bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    {
                        for (int j = 0; j < Variables.P_Width; j++)
                        {
                            row[j] = (Variables.colorsSave[0, i, j] << 16) | (Variables.colorsSave[1, i, j] << 8) | Variables.colorsSave[2, i, j];
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
                            row[j] = 0xFF << 24 | (Variables.colors[0, i, j] << 16) | (Variables.colors[1, i, j] << 8) | Variables.colors[2, i, j];
                        }
                    }
                    else if (bmp.PixelFormat == PixelFormat.Format32bppRgb || bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    {
                        for (int j = 0; j < Variables.P_Width; j++)
                        {
                            row[j] = (Variables.colors[0, i, j] << 16) | (Variables.colors[1, i, j] << 8) | Variables.colors[2, i, j];
                        }
                    }
                }
            }
            bmp.UnlockBits(bits);
            Variables.bitmap = bmp;
        }

        public void loadPicture()
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
                            conv = (uint)row[j];
                            Variables.colorsSave[2, i, j] = (byte)(conv - ((conv >> 8) << 8)); conv >>= 8;
                            Variables.colorsSave[1, i, j] = (byte)(conv - ((conv >> 8) << 8)); conv >>= 8;
                            Variables.colorsSave[0, i, j] = (byte)(conv - ((conv >> 8) << 8)); conv >>= 8;
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

        public void resetBitmap()
        {
            lockFn();
            Variables.bitmap.Dispose();
            loadOriginal();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        public void saveBitmap()
        {
            if (savePicture.ShowDialog() == DialogResult.OK)
            {
                lockFn();
                pictureModified.Image.Save(savePicture.FileName);
                unlockFn();
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            loadPicture();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            resetBitmap();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveBitmap();
        }

        public void setKernel()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    NumericUpDown c = (NumericUpDown)tableLayoutPanel.GetControlFromPosition(j, i);
                    Variables.customKernel[i, j] = (int)c.Value;
                }
            }
        }

        public void updateTable()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    NumericUpDown c = (NumericUpDown)tableLayoutPanel.GetControlFromPosition(j, i);
                    if (i < numericSizeRows.Value && j < numericSizeColumns.Value) c.Enabled = true;
                    else
                    {
                        c.Enabled = false;
                        c.Value = 0;
                    }
                }
            }
        }

        public void updateDivisor()
        {
            int divisor = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    divisor += Variables.customKernel[i, j];
                }
            }
            if (divisor <= 0) numericUpDownDivisor.Value = 1;
            else numericUpDownDivisor.Value = divisor;
        }

        private void numericSizeRows_ValueChanged(object sender, EventArgs e)
        {
            if (numericSizeRows.Value < numericAnchorRow.Value)
            {
                numericAnchorRow.Value = numericSizeRows.Value;
            }
            numericAnchorRow.Maximum = numericSizeRows.Value;
            updateTable();
        }

        private void numericSizeColumns_ValueChanged(object sender, EventArgs e)
        {
            if (numericSizeColumns.Value < numericAnchorColumn.Value)
            {
                numericAnchorColumn.Value = numericSizeColumns.Value;
            }
            numericAnchorColumn.Maximum = numericSizeColumns.Value;
            updateTable();
        }

        private void checkBoxAutoDivisor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoDivisor.Checked == true)
            {
                numericUpDownDivisor.Enabled = false;
            }
            else numericUpDownDivisor.Enabled = true;
        }

        public void applyConvolution(int height, int width, int kHeight, int kWidth, int aRow, int aColumn, int customWeight, int offset)
        {
            lockFn();
            setKernel();
            updateDivisor();

            int[,] copyKernel = new int[(int)numericSizeRows.Value, (int)numericSizeColumns.Value];
            for (int i = 0; i < (int)numericSizeRows.Value; i++)
            {
                for (int j = 0; j < (int)numericSizeColumns.Value; j++)
                {
                    copyKernel[i, j] = Variables.customKernel[i, j];
                }
            }

            convolutionFn(height, width, copyKernel, kHeight, kWidth, aRow, aColumn, customWeight, offset);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonApplyConvolution_Click(object sender, EventArgs e)
        {
            applyConvolution(Variables.P_Height, Variables.P_Width, (int)numericSizeRows.Value, (int)numericSizeColumns.Value,
                (int)numericAnchorRow.Value, (int)numericAnchorColumn.Value, (int)numericUpDownDivisor.Value, (int)numericUpDownOffset.Value);
        }

        // Part 2

        public void quickSort(int[] cubeCount, int[] positions, int left, int right)
        {
            unsafe
            {
                int i = left;
                int j = right;
                int pivot = cubeCount[positions[(left + right) / 2]];
                while (i < j)
                {
                    while (cubeCount[positions[i]] < pivot) i++;
                    while (cubeCount[positions[j]] > pivot) j--;
                    if (i <= j)
                    {
                        int tmp = positions[i];
                        positions[i++] = positions[j];
                        positions[j--] = tmp;
                    }
                }
                if (left < j) quickSort(cubeCount, positions, left, j);
                if (i < right) quickSort(cubeCount, positions, i, right);
            }
        }

        public void reverseArray(int[] array)
        {
            unsafe
            {
                for (int i = 0; i < array.Length / 2; i++)
                {
                    int tmp = array[i];
                    array[i] = array[array.Length - i - 1];
                    array[array.Length - i - 1] = tmp;
                }
            }
        }

        public byte[,] countSmallCubes(int height, int width, int noCubes)
        {
            unsafe
            {
                int index;
                int[] cubeCount = new int[512];
                List<int>[] subcubeLists = new List<int>[512];
                for (int i = 0; i < 512; i++)
                    subcubeLists[i] = new List<int>();

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        index = ((Variables.colors[0, i, j] >> 5) << 6) 
                            + ((Variables.colors[1, i, j] >> 5) << 3) 
                            + (Variables.colors[2, i, j] >> 5);
                        subcubeLists[index].Add(((Variables.colors[0, i, j] - ((Variables.colors[0, i, j] >> 5) << 5)) << 10)
                            + ((Variables.colors[1, i, j] - ((Variables.colors[1, i, j] >> 5) << 5)) << 5)
                            + (Variables.colors[2, i, j] - ((Variables.colors[2, i, j] >> 5) << 5)));
                        cubeCount[index]++;
                    }
                }

                int[] positions = new int[512];
                for (int i = 0; i < 512; i++)
                    positions[i] = i;

                quickSort(cubeCount, positions, 0, 511);
                reverseArray(positions);

                int[,] colors = new int[3, noCubes];
                int[] subpositions = new int[32768];
                int maxOccurence;
                for (int i = 0; i < noCubes; i++)
                {
                    maxOccurence = 16192;
                    for (int k = 0; k < 32768; k++)
                        subpositions[k] = 0;

                    for (int j = 0; j < subcubeLists[positions[i]].Count; j++)
                        subpositions[subcubeLists[positions[i]][j]]++;

                    for (int k = 0; k < 32768; k++)
                    {
                        if (subpositions[k] > subpositions[maxOccurence])
                            maxOccurence = k;
                    }

                    colors[0, i] = ((positions[i] >> 6) << 5) + (maxOccurence >> 10);
                    colors[1, i] = (((positions[i] >> 3) % 8) << 5) + ((maxOccurence >> 5) % 32);
                    colors[2, i] = ((positions[i] % 8) << 5) + (maxOccurence % 32);
                }

                byte[,] byteColors = new byte[3, noCubes];
                for (int i = 0; i < noCubes; i++)
                {
                    byteColors[0, i] = (byte)colors[0, i];
                    byteColors[1, i] = (byte)colors[1, i];
                    byteColors[2, i] = (byte)colors[2, i];
                }
                return byteColors;
            }
        }

        public byte[,] countBigCubes(int height, int width, int noCubes)
        {
            unsafe
            {
                int index;
                int[] cubeCount = new int[64];
                List<int>[] subcubeLists = new List<int>[64];
                for (int i = 0; i < 64; i++)
                    subcubeLists[i] = new List<int>();

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        index = ((Variables.colors[0, i, j] >> 6) << 4)
                            + ((Variables.colors[1, i, j] >> 6) << 2) 
                            + (Variables.colors[2, i, j] >> 6);
                        subcubeLists[index].Add(((Variables.colors[0, i, j] - ((Variables.colors[0, i, j] >> 6) << 6)) << 12)
                            + ((Variables.colors[1, i, j] - ((Variables.colors[1, i, j] >> 6) << 6)) << 6)
                            + (Variables.colors[2, i, j] - ((Variables.colors[2, i, j] >> 6) << 6)));
                        cubeCount[index]++;
                    }
                }

                int[] positions = new int[64];
                for (int i = 0; i < 64; i++)
                    positions[i] = i;

                quickSort(cubeCount, positions, 0, 63);
                reverseArray(positions);

                int[,] colors = new int[3, noCubes];
                int[] subpositions = new int[262144];
                int maxOccurence;
                for (int i = 0; i < noCubes; i++)
                {
                    maxOccurence = 133152;
                    for (int k = 0; k < 262144; k++)
                        subpositions[k] = 0;

                    for (int j = 0; j < subcubeLists[positions[i]].Count; j++)
                        subpositions[subcubeLists[positions[i]][j]]++;

                    for (int k = 0; k < 262144; k++)
                    {
                        if (subpositions[k] > subpositions[maxOccurence])
                            maxOccurence = k;
                    }

                    colors[0, i] = ((positions[i] >> 4) << 6) + (maxOccurence >> 12);
                    colors[1, i] = (((positions[i] >> 2) % 4) << 6) + ((maxOccurence >> 6) % 64);
                    colors[2, i] = ((positions[i] % 4) << 6) + (maxOccurence % 64);
                }

                byte[,] byteColors = new byte[3, noCubes];
                for (int i = 0; i < noCubes; i++)
                {
                    byteColors[0, i] = (byte)colors[0, i];
                    byteColors[1, i] = (byte)colors[1, i];
                    byteColors[2, i] = (byte)colors[2, i];
                }
                return byteColors;
            }
        }

        public void quantizeColors(int height, int width, int noCubes)
        {
            unsafe
            {
                byte[,] colors;
                if (noCubes <= 32)
                    colors = countBigCubes(height, width, noCubes);
                else 
                    colors = countSmallCubes(height, width, noCubes);

                listViewColors.BeginUpdate();
                listViewColors.Items.Clear();
                for (int i = 0; i < colors.Length / 3; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.BackColor = Color.FromArgb(colors[0, i], colors[1, i], colors[2, i]);
                    item.Text = "⠀⠀⠀⠀⠀";
                    listViewColors.Items.Add(item);
                }
                listViewColors.EndUpdate();

                int errorMinPosition, errorMinValue, errorValue;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        errorMinPosition = 0;
                        errorMinValue = 2147483647;
                        for (int k = 0; k < noCubes; k++)
                        {
                            errorValue = 0;
                            if (Variables.colors[0, i, j] > colors[0, k])                 // alternative - squared distances from the Manhattan norm
                                errorValue += (Variables.colors[0, i, j] - colors[0, k]); // * (Variables.colors[0, i, j] - colors[0, k]);
                            else
                                errorValue += (colors[0, k] - Variables.colors[0, i, j]); // * (colors[0, k] - Variables.colors[0, i, j]);
                            if (Variables.colors[1, i, j] > colors[1, k])
                                errorValue += (Variables.colors[1, i, j] - colors[1, k]); // * (Variables.colors[1, i, j] - colors[1, k]);
                            else
                                errorValue += (colors[1, k] - Variables.colors[1, i, j]); // * (colors[1, k] - Variables.colors[1, i, j]);
                            if (Variables.colors[2, i, j] > colors[2, k])
                                errorValue += (Variables.colors[2, i, j] - colors[2, k]); // * (Variables.colors[2, i, j] - colors[2, k]);
                            else
                                errorValue += (colors[2, k] - Variables.colors[2, i, j]); // * (colors[2, k] - Variables.colors[2, i, j]);
                
                            if (errorValue < errorMinValue)
                            {
                                errorMinPosition = k;
                                errorMinValue = errorValue;
                            }
                        }
                        Variables.colors[0, i, j] = colors[0, errorMinPosition];
                        Variables.colors[1, i, j] = colors[1, errorMinPosition];
                        Variables.colors[2, i, j] = colors[2, errorMinPosition];
                    }
                }
            }
        }

        public void applyQuantization(int height, int width, int noCubes)
        {
            lockFn();
            quantizeColors(height, width, noCubes);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonApplyQuantization_Click(object sender, EventArgs e)
        {
            applyQuantization(Variables.P_Height, Variables.P_Width, (int)numericUpDownQuantization.Value);
        }

        public int fastRound(float number)
        {
            int floor = (int)number;
            if (number - floor > 0.5)
                return floor;
            else 
                return floor + 1;
        }

        public void errorDiffusionFn(int height, int width, float[,] kernel, int kHeight, int kWidth, 
            int aRow, int aColumn, int noRed, int noGreen, int noBlue)        // kHeight - kernel height
        {    // aRow - anchor's row                                                                
            unsafe
            {
                float[,,] colorsCopy = new float[3, height, width];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        colorsCopy[0, i, j] = Variables.colors[0, i, j];
                        colorsCopy[1, i, j] = Variables.colors[1, i, j];
                        colorsCopy[2, i, j] = Variables.colors[2, i, j];
                    }
                }

                float[] approximation = new float[3];
                float[] error = new float[3];
                float[] errorBase = new float[3] { 256F / (noRed - 1), 256F / (noGreen - 1), 256F / (noBlue - 1) };
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        // quantization
                        approximation[0] = errorBase[0] * fastRound(colorsCopy[0, i, j] / errorBase[0]);
                        approximation[1] = errorBase[1] * fastRound(colorsCopy[1, i, j] / errorBase[1]);
                        approximation[2] = errorBase[2] * fastRound(colorsCopy[2, i, j] / errorBase[2]);

                        error[0] = colorsCopy[0, i, j] - approximation[0];
                        error[1] = colorsCopy[1, i, j] - approximation[1];
                        error[2] = colorsCopy[2, i, j] - approximation[2];

                        colorsCopy[0, i, j] = approximation[0];
                        colorsCopy[1, i, j] = approximation[1];
                        colorsCopy[2, i, j] = approximation[2];

                        // adding error
                        for (int ki = 0; ki < fastMin(height - i, kHeight - aRow + 1); ki++)
                        {     // assuming non-zero values olny after the anchor
                            for (int kj = fastMax(1 - aColumn, - j); kj < fastMin(kWidth - aColumn + 1, width - j); kj++)
                            {
                                if (kernel[aRow + ki - 1, aColumn + kj - 1] != 0) // assuming no rounding error 
                                {                                                 // (in future modifications)
                                    colorsCopy[0, i + ki, j + kj] += error[0] * kernel[aRow + ki - 1, aColumn + kj - 1];
                                    colorsCopy[1, i + ki, j + kj] += error[1] * kernel[aRow + ki - 1, aColumn + kj - 1];
                                    colorsCopy[2, i + ki, j + kj] += error[2] * kernel[aRow + ki - 1, aColumn + kj - 1];
                                }
                            }
                        }
                    }
                }
                
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Variables.colors[0, i, j] = (byte)fastMin(255, fastMax(0, fastRound(colorsCopy[0, i, j])));
                        Variables.colors[1, i, j] = (byte)fastMin(255, fastMax(0, fastRound(colorsCopy[1, i, j])));
                        Variables.colors[2, i, j] = (byte)fastMin(255, fastMax(0, fastRound(colorsCopy[2, i, j])));
                    }
                }
            }
        }

        // might be merged into one function
        private void buttonAtkinson_Click(object sender, EventArgs e)
        {
            float[,] kernel = new float[5, 5]
            {
                    {0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0},
                    {0, 0, 0, 0.125F, 0.125F},
                    {0, 0.125F, 0.125F, 0.125F, 0},
                    {0, 0, 0.125F, 0, 0}
            };

            lockFn();
            errorDiffusionFn(Variables.P_Height, Variables.P_Width, kernel, 5, 5, 3, 3, (int)numericUpDownDiffusionRed.Value, 
                (int)numericUpDownDiffusionGreen.Value, (int)numericUpDownDiffusionBlue.Value);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonBurkes_Click(object sender, EventArgs e)
        {
            float[,] kernel = new float[3, 5]
            {
                    {0, 0, 0, 0, 0},
                    {0, 0, 0, 0.25F, 0.125F},
                    {0.0625F, 0.125F, 0.25F, 0.125F, 0.0625F}
            };

            lockFn();
            errorDiffusionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 5, 2, 3, (int)numericUpDownDiffusionRed.Value,
                (int)numericUpDownDiffusionGreen.Value, (int)numericUpDownDiffusionBlue.Value);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonFloydAndSteinberg_Click(object sender, EventArgs e)
        {
            float[,] kernel = new float[3, 3]
            {
                    {0, 0, 0},
                    {0, 0, 0.4375F},
                    {0.1875F, 0.3125F, 0.0625F}
            };

            lockFn();
            errorDiffusionFn(Variables.P_Height, Variables.P_Width, kernel, 3, 3, 2, 2, (int)numericUpDownDiffusionRed.Value,
                (int)numericUpDownDiffusionGreen.Value, (int)numericUpDownDiffusionBlue.Value);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonSierra_Click(object sender, EventArgs e)
        {
            float[,] kernel = new float[5, 5]
            {
                    {0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0},
                    {0, 0, 0, 0.15625F, 0.09375F},
                    {0.0625F, 0.125F, 0.15625F, 0.125F, 0.0625F},
                    {0, 0.0625F, 0.09375F, 0.0625F, 0}
            };

            lockFn();
            errorDiffusionFn(Variables.P_Height, Variables.P_Width, kernel, 5, 5, 3, 3, (int)numericUpDownDiffusionRed.Value,
                (int)numericUpDownDiffusionGreen.Value, (int)numericUpDownDiffusionBlue.Value);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonStucky_Click(object sender, EventArgs e)
        {
            float[,] kernel = new float[5, 5]
            {
                    {0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0},
                    {0, 0, 0, 8F / 42, 4F / 42},
                    {2F / 42, 4F / 42, 8F / 42, 4F / 42, 2F / 42},
                    {1F / 42, 2F / 42, 4F / 42, 2F / 42, 1F / 42}
            };

            lockFn();
            errorDiffusionFn(Variables.P_Height, Variables.P_Width, kernel, 5, 5, 3, 3, (int)numericUpDownDiffusionRed.Value,
                (int)numericUpDownDiffusionGreen.Value, (int)numericUpDownDiffusionBlue.Value);
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        public void transformToGreyscale(int height, int width)
        {
            lockFn();
            unsafe
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        // CIE 1931: Y = 0.2126 * R + 0.7152 * G + 0.0722 * B
                        byte shade = (byte)(fastMin(255, fastRound(0.2126F * Variables.colors[0, i, j] 
                            + 0.7152F * Variables.colors[1, i, j] + 0.0722F * Variables.colors[2, i, j])));
                        Variables.colors[0, i, j] = shade;
                        Variables.colors[1, i, j] = shade;
                        Variables.colors[2, i, j] = shade;
                    }
                }
            }
            Variables.bitmap.Dispose();
            loadModification();
            pictureModified.Image = Variables.bitmap;
            unlockFn();
        }

        private void buttonGreyscale_Click(object sender, EventArgs e)
        {
            transformToGreyscale(Variables.P_Height, Variables.P_Width);
        }

        public void bindDiffusion(NumericUpDown numeric)
        {
            numericUpDownDiffusionRed.Value = numeric.Value;
            numericUpDownDiffusionGreen.Value = numeric.Value;
            numericUpDownDiffusionBlue.Value = numeric.Value;

        }

        // might be merged into one function (if possible)
        private void numericUpDownDiffusionRed_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxGreyscale.Checked == true)
                bindDiffusion(numericUpDownDiffusionRed);
        }

        private void numericUpDownDiffusionGreen_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxGreyscale.Checked == true)
                bindDiffusion(numericUpDownDiffusionGreen);
        }

        private void numericUpDownDiffusionBlue_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxGreyscale.Checked == true)
                bindDiffusion(numericUpDownDiffusionBlue);
        }
    }

    static class Variables
    {
        // Part 1 variables
        public static byte BC_Constant = 5;             // brightness correction constant (>= 1)
        public static double GCDown_Constant = 1.05;    // decoding gamma correction contrast (> 1.0)
        public static double GCUp_Constant = 0.95;      // encoding gamma correction contrast (< 1.0)
        public static double CE_Constant = 1.1;         // contrast enhancement slope (> 1.0)
        public static int P_Height;                     // picture height
        public static int P_Width;                      // picture width
        public static byte[,,] colors;
        public static byte[,,] colorsSave;
        public static Bitmap bitmap;
        public static PixelFormat pixelFormat;
        public static int[,] customKernel;

        // Part 2 variables
    }
}