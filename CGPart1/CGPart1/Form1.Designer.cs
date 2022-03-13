
namespace CGPart1
{
    partial class CGPart1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupFilters = new System.Windows.Forms.GroupBox();
            this.buttonCorrectGammaDown = new System.Windows.Forms.Button();
            this.buttonCorrectBrightnessDown = new System.Windows.Forms.Button();
            this.labelConvolutionFilters = new System.Windows.Forms.Label();
            this.labelFunctionalFilters = new System.Windows.Forms.Label();
            this.buttonEmboss = new System.Windows.Forms.Button();
            this.buttonDetectEdges = new System.Windows.Forms.Button();
            this.buttonSharpen = new System.Windows.Forms.Button();
            this.buttonGaussianBlur = new System.Windows.Forms.Button();
            this.buttonBlur = new System.Windows.Forms.Button();
            this.buttonCorrectGammaUp = new System.Windows.Forms.Button();
            this.buttonEnhanceContrast = new System.Windows.Forms.Button();
            this.buttonCorrectBrightnessUp = new System.Windows.Forms.Button();
            this.buttonInvert = new System.Windows.Forms.Button();
            this.groupConvolution = new System.Windows.Forms.GroupBox();
            this.labelAnchorRow = new System.Windows.Forms.Label();
            this.labelAnchorColumn = new System.Windows.Forms.Label();
            this.labelSizeColumns = new System.Windows.Forms.Label();
            this.labelSizeRows = new System.Windows.Forms.Label();
            this.labelAnchor = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.numericAnchorColumn = new System.Windows.Forms.NumericUpDown();
            this.numericAnchorRow = new System.Windows.Forms.NumericUpDown();
            this.numericSizeColumns = new System.Windows.Forms.NumericUpDown();
            this.numericSizeRows = new System.Windows.Forms.NumericUpDown();
            this.groupOriginal = new System.Windows.Forms.GroupBox();
            this.pictureOriginal = new System.Windows.Forms.PictureBox();
            this.groupModified = new System.Windows.Forms.GroupBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.pictureModified = new System.Windows.Forms.PictureBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.savePicture = new System.Windows.Forms.SaveFileDialog();
            this.openPicture = new System.Windows.Forms.OpenFileDialog();
            this.groupFilters.SuspendLayout();
            this.groupConvolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnchorColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnchorRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSizeColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSizeRows)).BeginInit();
            this.groupOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOriginal)).BeginInit();
            this.groupModified.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureModified)).BeginInit();
            this.SuspendLayout();
            // 
            // groupFilters
            // 
            this.groupFilters.Controls.Add(this.buttonCorrectGammaDown);
            this.groupFilters.Controls.Add(this.buttonCorrectBrightnessDown);
            this.groupFilters.Controls.Add(this.labelConvolutionFilters);
            this.groupFilters.Controls.Add(this.labelFunctionalFilters);
            this.groupFilters.Controls.Add(this.buttonEmboss);
            this.groupFilters.Controls.Add(this.buttonDetectEdges);
            this.groupFilters.Controls.Add(this.buttonSharpen);
            this.groupFilters.Controls.Add(this.buttonGaussianBlur);
            this.groupFilters.Controls.Add(this.buttonBlur);
            this.groupFilters.Controls.Add(this.buttonCorrectGammaUp);
            this.groupFilters.Controls.Add(this.buttonEnhanceContrast);
            this.groupFilters.Controls.Add(this.buttonCorrectBrightnessUp);
            this.groupFilters.Controls.Add(this.buttonInvert);
            this.groupFilters.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupFilters.Location = new System.Drawing.Point(13, 13);
            this.groupFilters.Name = "groupFilters";
            this.groupFilters.Size = new System.Drawing.Size(581, 280);
            this.groupFilters.TabIndex = 0;
            this.groupFilters.TabStop = false;
            this.groupFilters.Text = "Filters";
            // 
            // buttonCorrectGammaDown
            // 
            this.buttonCorrectGammaDown.Enabled = false;
            this.buttonCorrectGammaDown.Location = new System.Drawing.Point(60, 235);
            this.buttonCorrectGammaDown.Name = "buttonCorrectGammaDown";
            this.buttonCorrectGammaDown.Size = new System.Drawing.Size(212, 32);
            this.buttonCorrectGammaDown.TabIndex = 12;
            this.buttonCorrectGammaDown.Text = "Correct gamma (down)";
            this.buttonCorrectGammaDown.UseVisualStyleBackColor = true;
            this.buttonCorrectGammaDown.Click += new System.EventHandler(this.buttonCorrectGammaDown_Click);
            // 
            // buttonCorrectBrightnessDown
            // 
            this.buttonCorrectBrightnessDown.Enabled = false;
            this.buttonCorrectBrightnessDown.Location = new System.Drawing.Point(60, 121);
            this.buttonCorrectBrightnessDown.Name = "buttonCorrectBrightnessDown";
            this.buttonCorrectBrightnessDown.Size = new System.Drawing.Size(212, 32);
            this.buttonCorrectBrightnessDown.TabIndex = 11;
            this.buttonCorrectBrightnessDown.Text = "Correct Brightness (down)";
            this.buttonCorrectBrightnessDown.UseVisualStyleBackColor = true;
            this.buttonCorrectBrightnessDown.Click += new System.EventHandler(this.buttonCorrectBrightnessDown_Click);
            // 
            // labelConvolutionFilters
            // 
            this.labelConvolutionFilters.AutoSize = true;
            this.labelConvolutionFilters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelConvolutionFilters.Location = new System.Drawing.Point(311, 24);
            this.labelConvolutionFilters.Name = "labelConvolutionFilters";
            this.labelConvolutionFilters.Size = new System.Drawing.Size(182, 15);
            this.labelConvolutionFilters.TabIndex = 10;
            this.labelConvolutionFilters.Text = "Convolution Filters (3x3 kernel)";
            // 
            // labelFunctionalFilters
            // 
            this.labelFunctionalFilters.AutoSize = true;
            this.labelFunctionalFilters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelFunctionalFilters.Location = new System.Drawing.Point(60, 24);
            this.labelFunctionalFilters.Name = "labelFunctionalFilters";
            this.labelFunctionalFilters.Size = new System.Drawing.Size(101, 15);
            this.labelFunctionalFilters.TabIndex = 9;
            this.labelFunctionalFilters.Text = "Functional Filters";
            // 
            // buttonEmboss
            // 
            this.buttonEmboss.Enabled = false;
            this.buttonEmboss.Location = new System.Drawing.Point(311, 198);
            this.buttonEmboss.Name = "buttonEmboss";
            this.buttonEmboss.Size = new System.Drawing.Size(212, 32);
            this.buttonEmboss.TabIndex = 8;
            this.buttonEmboss.Text = "Emboss (south-east type)";
            this.buttonEmboss.UseVisualStyleBackColor = true;
            this.buttonEmboss.Click += new System.EventHandler(this.buttonEmboss_Click);
            // 
            // buttonDetectEdges
            // 
            this.buttonDetectEdges.Enabled = false;
            this.buttonDetectEdges.Location = new System.Drawing.Point(311, 160);
            this.buttonDetectEdges.Name = "buttonDetectEdges";
            this.buttonDetectEdges.Size = new System.Drawing.Size(212, 32);
            this.buttonDetectEdges.TabIndex = 7;
            this.buttonDetectEdges.Text = "Detect Edges (diagonal)";
            this.buttonDetectEdges.UseVisualStyleBackColor = true;
            this.buttonDetectEdges.Click += new System.EventHandler(this.buttonDetectEdges_Click);
            // 
            // buttonSharpen
            // 
            this.buttonSharpen.Enabled = false;
            this.buttonSharpen.Location = new System.Drawing.Point(311, 122);
            this.buttonSharpen.Name = "buttonSharpen";
            this.buttonSharpen.Size = new System.Drawing.Size(212, 32);
            this.buttonSharpen.TabIndex = 6;
            this.buttonSharpen.Text = "Sharpen";
            this.buttonSharpen.UseVisualStyleBackColor = true;
            this.buttonSharpen.Click += new System.EventHandler(this.buttonSharpen_Click);
            // 
            // buttonGaussianBlur
            // 
            this.buttonGaussianBlur.Enabled = false;
            this.buttonGaussianBlur.Location = new System.Drawing.Point(311, 84);
            this.buttonGaussianBlur.Name = "buttonGaussianBlur";
            this.buttonGaussianBlur.Size = new System.Drawing.Size(212, 32);
            this.buttonGaussianBlur.TabIndex = 5;
            this.buttonGaussianBlur.Text = "Gaussian Blur";
            this.buttonGaussianBlur.UseVisualStyleBackColor = true;
            this.buttonGaussianBlur.Click += new System.EventHandler(this.buttonGaussianBlur_Click);
            // 
            // buttonBlur
            // 
            this.buttonBlur.Enabled = false;
            this.buttonBlur.Location = new System.Drawing.Point(311, 46);
            this.buttonBlur.Name = "buttonBlur";
            this.buttonBlur.Size = new System.Drawing.Size(212, 32);
            this.buttonBlur.TabIndex = 4;
            this.buttonBlur.Text = "Blur";
            this.buttonBlur.UseVisualStyleBackColor = true;
            this.buttonBlur.Click += new System.EventHandler(this.buttonBlur_Click);
            // 
            // buttonCorrectGammaUp
            // 
            this.buttonCorrectGammaUp.Enabled = false;
            this.buttonCorrectGammaUp.Location = new System.Drawing.Point(60, 198);
            this.buttonCorrectGammaUp.Name = "buttonCorrectGammaUp";
            this.buttonCorrectGammaUp.Size = new System.Drawing.Size(212, 32);
            this.buttonCorrectGammaUp.TabIndex = 3;
            this.buttonCorrectGammaUp.Text = "Correct gamma (up)";
            this.buttonCorrectGammaUp.UseVisualStyleBackColor = true;
            this.buttonCorrectGammaUp.Click += new System.EventHandler(this.buttonCorrectGammaUp_Click);
            // 
            // buttonEnhanceContrast
            // 
            this.buttonEnhanceContrast.Enabled = false;
            this.buttonEnhanceContrast.Location = new System.Drawing.Point(60, 160);
            this.buttonEnhanceContrast.Name = "buttonEnhanceContrast";
            this.buttonEnhanceContrast.Size = new System.Drawing.Size(212, 32);
            this.buttonEnhanceContrast.TabIndex = 2;
            this.buttonEnhanceContrast.Text = "Enhance contrast";
            this.buttonEnhanceContrast.UseVisualStyleBackColor = true;
            this.buttonEnhanceContrast.Click += new System.EventHandler(this.buttonEnhanceContrast_Click);
            // 
            // buttonCorrectBrightnessUp
            // 
            this.buttonCorrectBrightnessUp.Enabled = false;
            this.buttonCorrectBrightnessUp.Location = new System.Drawing.Point(60, 83);
            this.buttonCorrectBrightnessUp.Name = "buttonCorrectBrightnessUp";
            this.buttonCorrectBrightnessUp.Size = new System.Drawing.Size(212, 32);
            this.buttonCorrectBrightnessUp.TabIndex = 1;
            this.buttonCorrectBrightnessUp.Text = "Correct Brightness (up)";
            this.buttonCorrectBrightnessUp.UseVisualStyleBackColor = true;
            this.buttonCorrectBrightnessUp.Click += new System.EventHandler(this.buttonCorrectBrightnessUp_Click);
            // 
            // buttonInvert
            // 
            this.buttonInvert.Enabled = false;
            this.buttonInvert.Location = new System.Drawing.Point(60, 45);
            this.buttonInvert.Name = "buttonInvert";
            this.buttonInvert.Size = new System.Drawing.Size(212, 32);
            this.buttonInvert.TabIndex = 0;
            this.buttonInvert.Text = "Invert";
            this.buttonInvert.UseVisualStyleBackColor = true;
            this.buttonInvert.Click += new System.EventHandler(this.buttonInvert_Click);
            // 
            // groupConvolution
            // 
            this.groupConvolution.Controls.Add(this.labelAnchorRow);
            this.groupConvolution.Controls.Add(this.labelAnchorColumn);
            this.groupConvolution.Controls.Add(this.labelSizeColumns);
            this.groupConvolution.Controls.Add(this.labelSizeRows);
            this.groupConvolution.Controls.Add(this.labelAnchor);
            this.groupConvolution.Controls.Add(this.labelSize);
            this.groupConvolution.Controls.Add(this.numericAnchorColumn);
            this.groupConvolution.Controls.Add(this.numericAnchorRow);
            this.groupConvolution.Controls.Add(this.numericSizeColumns);
            this.groupConvolution.Controls.Add(this.numericSizeRows);
            this.groupConvolution.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupConvolution.Location = new System.Drawing.Point(13, 299);
            this.groupConvolution.Name = "groupConvolution";
            this.groupConvolution.Size = new System.Drawing.Size(581, 550);
            this.groupConvolution.TabIndex = 1;
            this.groupConvolution.TabStop = false;
            this.groupConvolution.Text = "Custom Convolution Filter";
            // 
            // labelAnchorRow
            // 
            this.labelAnchorRow.AutoSize = true;
            this.labelAnchorRow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelAnchorRow.Location = new System.Drawing.Point(311, 70);
            this.labelAnchorRow.Name = "labelAnchorRow";
            this.labelAnchorRow.Size = new System.Drawing.Size(30, 15);
            this.labelAnchorRow.TabIndex = 10;
            this.labelAnchorRow.Text = "Row";
            // 
            // labelAnchorColumn
            // 
            this.labelAnchorColumn.AutoSize = true;
            this.labelAnchorColumn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelAnchorColumn.Location = new System.Drawing.Point(311, 121);
            this.labelAnchorColumn.Name = "labelAnchorColumn";
            this.labelAnchorColumn.Size = new System.Drawing.Size(50, 15);
            this.labelAnchorColumn.TabIndex = 9;
            this.labelAnchorColumn.Text = "Column";
            // 
            // labelSizeColumns
            // 
            this.labelSizeColumns.AutoSize = true;
            this.labelSizeColumns.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelSizeColumns.Location = new System.Drawing.Point(152, 122);
            this.labelSizeColumns.Name = "labelSizeColumns";
            this.labelSizeColumns.Size = new System.Drawing.Size(91, 15);
            this.labelSizeColumns.TabIndex = 8;
            this.labelSizeColumns.Text = "No. of Columns";
            // 
            // labelSizeRows
            // 
            this.labelSizeRows.AutoSize = true;
            this.labelSizeRows.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelSizeRows.Location = new System.Drawing.Point(152, 71);
            this.labelSizeRows.Name = "labelSizeRows";
            this.labelSizeRows.Size = new System.Drawing.Size(71, 15);
            this.labelSizeRows.TabIndex = 7;
            this.labelSizeRows.Text = "No. of Rows";
            // 
            // labelAnchor
            // 
            this.labelAnchor.AutoSize = true;
            this.labelAnchor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelAnchor.Location = new System.Drawing.Point(311, 50);
            this.labelAnchor.Name = "labelAnchor";
            this.labelAnchor.Size = new System.Drawing.Size(79, 15);
            this.labelAnchor.TabIndex = 6;
            this.labelAnchor.Text = "Anchor Point";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelSize.Location = new System.Drawing.Point(152, 50);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(30, 15);
            this.labelSize.TabIndex = 5;
            this.labelSize.Text = "Size";
            // 
            // numericAnchorColumn
            // 
            this.numericAnchorColumn.Location = new System.Drawing.Point(311, 140);
            this.numericAnchorColumn.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericAnchorColumn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericAnchorColumn.Name = "numericAnchorColumn";
            this.numericAnchorColumn.Size = new System.Drawing.Size(120, 29);
            this.numericAnchorColumn.TabIndex = 4;
            this.numericAnchorColumn.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numericAnchorRow
            // 
            this.numericAnchorRow.Location = new System.Drawing.Point(311, 89);
            this.numericAnchorRow.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericAnchorRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericAnchorRow.Name = "numericAnchorRow";
            this.numericAnchorRow.Size = new System.Drawing.Size(120, 29);
            this.numericAnchorRow.TabIndex = 3;
            this.numericAnchorRow.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numericSizeColumns
            // 
            this.numericSizeColumns.Location = new System.Drawing.Point(152, 140);
            this.numericSizeColumns.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericSizeColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSizeColumns.Name = "numericSizeColumns";
            this.numericSizeColumns.Size = new System.Drawing.Size(120, 29);
            this.numericSizeColumns.TabIndex = 2;
            this.numericSizeColumns.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericSizeColumns.ValueChanged += new System.EventHandler(this.numericSizeColumns_ValueChanged);
            // 
            // numericSizeRows
            // 
            this.numericSizeRows.Location = new System.Drawing.Point(152, 89);
            this.numericSizeRows.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericSizeRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSizeRows.Name = "numericSizeRows";
            this.numericSizeRows.Size = new System.Drawing.Size(120, 29);
            this.numericSizeRows.TabIndex = 1;
            this.numericSizeRows.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericSizeRows.ValueChanged += new System.EventHandler(this.numericSizeRows_ValueChanged);
            // 
            // groupOriginal
            // 
            this.groupOriginal.Controls.Add(this.pictureOriginal);
            this.groupOriginal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupOriginal.Location = new System.Drawing.Point(600, 13);
            this.groupOriginal.Name = "groupOriginal";
            this.groupOriginal.Size = new System.Drawing.Size(572, 386);
            this.groupOriginal.TabIndex = 2;
            this.groupOriginal.TabStop = false;
            this.groupOriginal.Text = "Original Picture";
            // 
            // pictureOriginal
            // 
            this.pictureOriginal.Location = new System.Drawing.Point(6, 22);
            this.pictureOriginal.Name = "pictureOriginal";
            this.pictureOriginal.Size = new System.Drawing.Size(560, 358);
            this.pictureOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureOriginal.TabIndex = 0;
            this.pictureOriginal.TabStop = false;
            // 
            // groupModified
            // 
            this.groupModified.Controls.Add(this.buttonSave);
            this.groupModified.Controls.Add(this.pictureModified);
            this.groupModified.Controls.Add(this.buttonReset);
            this.groupModified.Controls.Add(this.buttonLoad);
            this.groupModified.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupModified.Location = new System.Drawing.Point(600, 405);
            this.groupModified.Name = "groupModified";
            this.groupModified.Size = new System.Drawing.Size(572, 444);
            this.groupModified.TabIndex = 3;
            this.groupModified.TabStop = false;
            this.groupModified.Text = "Modified Picture";
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(361, 399);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(143, 29);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // pictureModified
            // 
            this.pictureModified.Location = new System.Drawing.Point(6, 23);
            this.pictureModified.Name = "pictureModified";
            this.pictureModified.Size = new System.Drawing.Size(560, 358);
            this.pictureModified.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureModified.TabIndex = 1;
            this.pictureModified.TabStop = false;
            // 
            // buttonReset
            // 
            this.buttonReset.Enabled = false;
            this.buttonReset.Location = new System.Drawing.Point(212, 399);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(143, 29);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(63, 399);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(143, 29);
            this.buttonLoad.TabIndex = 4;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // CGPart1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.groupModified);
            this.Controls.Add(this.groupOriginal);
            this.Controls.Add(this.groupConvolution);
            this.Controls.Add(this.groupFilters);
            this.Name = "CGPart1";
            this.Text = "CG Part 1";
            this.groupFilters.ResumeLayout(false);
            this.groupFilters.PerformLayout();
            this.groupConvolution.ResumeLayout(false);
            this.groupConvolution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnchorColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnchorRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSizeColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSizeRows)).EndInit();
            this.groupOriginal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureOriginal)).EndInit();
            this.groupModified.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureModified)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupFilters;
        private System.Windows.Forms.GroupBox groupConvolution;
        private System.Windows.Forms.GroupBox groupOriginal;
        private System.Windows.Forms.GroupBox groupModified;
        private System.Windows.Forms.Button buttonEmboss;
        private System.Windows.Forms.Button buttonDetectEdges;
        private System.Windows.Forms.Button buttonSharpen;
        private System.Windows.Forms.Button buttonGaussianBlur;
        private System.Windows.Forms.Button buttonBlur;
        private System.Windows.Forms.Button buttonCorrectGammaUp;
        private System.Windows.Forms.Button buttonEnhanceContrast;
        private System.Windows.Forms.Button buttonCorrectBrightnessUp;
        private System.Windows.Forms.Button buttonInvert;
        private System.Windows.Forms.Label labelAnchor;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.NumericUpDown numericAnchorColumn;
        private System.Windows.Forms.NumericUpDown numericAnchorRow;
        private System.Windows.Forms.NumericUpDown numericSizeColumns;
        private System.Windows.Forms.NumericUpDown numericSizeRows;
        private System.Windows.Forms.PictureBox pictureOriginal;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.PictureBox pictureModified;
        private System.Windows.Forms.Label labelAnchorRow;
        private System.Windows.Forms.Label labelAnchorColumn;
        private System.Windows.Forms.Label labelSizeColumns;
        private System.Windows.Forms.Label labelSizeRows;
        private System.Windows.Forms.Label labelConvolutionFilters;
        private System.Windows.Forms.Label labelFunctionalFilters;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.SaveFileDialog savePicture;
        private System.Windows.Forms.OpenFileDialog openPicture;
        private System.Windows.Forms.Button buttonCorrectGammaDown;
        private System.Windows.Forms.Button buttonCorrectBrightnessDown;
    }
}

