
namespace CGPart3
{
    partial class Form1
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
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.labelAntiAliasing = new System.Windows.Forms.Label();
            this.checkBoxAntiAliasing = new System.Windows.Forms.CheckBox();
            this.labelMode = new System.Windows.Forms.Label();
            this.buttonClearAll = new System.Windows.Forms.Button();
            this.labelBrush = new System.Windows.Forms.Label();
            this.labelThickLine = new System.Windows.Forms.Label();
            this.labelBrushColor = new System.Windows.Forms.Label();
            this.buttonBrushColor = new System.Windows.Forms.Button();
            this.numericUpDownLineThickness = new System.Windows.Forms.NumericUpDown();
            this.labelLineThickness = new System.Windows.Forms.Label();
            this.panelMode = new System.Windows.Forms.Panel();
            this.radioButtonErase = new System.Windows.Forms.RadioButton();
            this.radioButtonBrush = new System.Windows.Forms.RadioButton();
            this.radioButtonResizeCircle = new System.Windows.Forms.RadioButton();
            this.radioButtonMovePolygon = new System.Windows.Forms.RadioButton();
            this.radioButtonMoveLine = new System.Windows.Forms.RadioButton();
            this.radioButtonMovePoint = new System.Windows.Forms.RadioButton();
            this.radioButtonAddPolygon = new System.Windows.Forms.RadioButton();
            this.radioButtonAddCircle = new System.Windows.Forms.RadioButton();
            this.radioButtonAddThickLine = new System.Windows.Forms.RadioButton();
            this.radioButtonAddThinLine = new System.Windows.Forms.RadioButton();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.groupBoxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineThickness)).BeginInit();
            this.panelMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Location = new System.Drawing.Point(411, 27);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(600, 800);
            this.pictureBoxMain.TabIndex = 0;
            this.pictureBoxMain.TabStop = false;
            this.pictureBoxMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseClick);
            this.pictureBoxMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseDown);
            this.pictureBoxMain.MouseLeave += new System.EventHandler(this.pictureBoxMain_MouseLeave);
            this.pictureBoxMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseMove);
            this.pictureBoxMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseUp);
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.labelAntiAliasing);
            this.groupBoxSettings.Controls.Add(this.checkBoxAntiAliasing);
            this.groupBoxSettings.Controls.Add(this.labelMode);
            this.groupBoxSettings.Controls.Add(this.buttonClearAll);
            this.groupBoxSettings.Controls.Add(this.labelBrush);
            this.groupBoxSettings.Controls.Add(this.labelThickLine);
            this.groupBoxSettings.Controls.Add(this.labelBrushColor);
            this.groupBoxSettings.Controls.Add(this.buttonBrushColor);
            this.groupBoxSettings.Controls.Add(this.numericUpDownLineThickness);
            this.groupBoxSettings.Controls.Add(this.labelLineThickness);
            this.groupBoxSettings.Controls.Add(this.panelMode);
            this.groupBoxSettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxSettings.Location = new System.Drawing.Point(13, 13);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(392, 494);
            this.groupBoxSettings.TabIndex = 1;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // labelAntiAliasing
            // 
            this.labelAntiAliasing.AutoSize = true;
            this.labelAntiAliasing.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelAntiAliasing.Location = new System.Drawing.Point(85, 368);
            this.labelAntiAliasing.Name = "labelAntiAliasing";
            this.labelAntiAliasing.Size = new System.Drawing.Size(77, 15);
            this.labelAntiAliasing.TabIndex = 11;
            this.labelAntiAliasing.Text = "Anti-Aliasing";
            // 
            // checkBoxAntiAliasing
            // 
            this.checkBoxAntiAliasing.AutoSize = true;
            this.checkBoxAntiAliasing.Location = new System.Drawing.Point(88, 386);
            this.checkBoxAntiAliasing.Name = "checkBoxAntiAliasing";
            this.checkBoxAntiAliasing.Size = new System.Drawing.Size(179, 25);
            this.checkBoxAntiAliasing.TabIndex = 10;
            this.checkBoxAntiAliasing.Text = "Enable AA (lines only)";
            this.checkBoxAntiAliasing.UseVisualStyleBackColor = true;
            this.checkBoxAntiAliasing.CheckedChanged += new System.EventHandler(this.checkBoxAntiAliasing_CheckedChanged);
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelMode.Location = new System.Drawing.Point(6, 34);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(39, 15);
            this.labelMode.TabIndex = 9;
            this.labelMode.Text = "Mode";
            // 
            // buttonClearAll
            // 
            this.buttonClearAll.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonClearAll.Location = new System.Drawing.Point(85, 435);
            this.buttonClearAll.Name = "buttonClearAll";
            this.buttonClearAll.Size = new System.Drawing.Size(222, 32);
            this.buttonClearAll.TabIndex = 0;
            this.buttonClearAll.Text = "Clear the board";
            this.buttonClearAll.UseVisualStyleBackColor = true;
            this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
            // 
            // labelBrush
            // 
            this.labelBrush.AutoSize = true;
            this.labelBrush.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelBrush.Location = new System.Drawing.Point(85, 296);
            this.labelBrush.Name = "labelBrush";
            this.labelBrush.Size = new System.Drawing.Size(39, 15);
            this.labelBrush.TabIndex = 8;
            this.labelBrush.Text = "Brush";
            // 
            // labelThickLine
            // 
            this.labelThickLine.AutoSize = true;
            this.labelThickLine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelThickLine.Location = new System.Drawing.Point(85, 240);
            this.labelThickLine.Name = "labelThickLine";
            this.labelThickLine.Size = new System.Drawing.Size(60, 15);
            this.labelThickLine.TabIndex = 7;
            this.labelThickLine.Text = "Thick line";
            // 
            // labelBrushColor
            // 
            this.labelBrushColor.AutoSize = true;
            this.labelBrushColor.BackColor = System.Drawing.Color.Black;
            this.labelBrushColor.Location = new System.Drawing.Point(88, 315);
            this.labelBrushColor.MaximumSize = new System.Drawing.Size(30, 30);
            this.labelBrushColor.MinimumSize = new System.Drawing.Size(30, 30);
            this.labelBrushColor.Name = "labelBrushColor";
            this.labelBrushColor.Size = new System.Drawing.Size(30, 30);
            this.labelBrushColor.TabIndex = 6;
            this.labelBrushColor.Text = "   ";
            // 
            // buttonBrushColor
            // 
            this.buttonBrushColor.Location = new System.Drawing.Point(124, 314);
            this.buttonBrushColor.Name = "buttonBrushColor";
            this.buttonBrushColor.Size = new System.Drawing.Size(183, 32);
            this.buttonBrushColor.TabIndex = 5;
            this.buttonBrushColor.Text = "Set brush colour";
            this.buttonBrushColor.UseVisualStyleBackColor = true;
            this.buttonBrushColor.Click += new System.EventHandler(this.buttonBrushColor_Click);
            // 
            // numericUpDownLineThickness
            // 
            this.numericUpDownLineThickness.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownLineThickness.Location = new System.Drawing.Point(214, 253);
            this.numericUpDownLineThickness.Maximum = new decimal(new int[] {
            49,
            0,
            0,
            0});
            this.numericUpDownLineThickness.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownLineThickness.Name = "numericUpDownLineThickness";
            this.numericUpDownLineThickness.Size = new System.Drawing.Size(93, 29);
            this.numericUpDownLineThickness.TabIndex = 3;
            this.numericUpDownLineThickness.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownLineThickness.ValueChanged += new System.EventHandler(this.numericUpDownLineThickness_ValueChanged);
            // 
            // labelLineThickness
            // 
            this.labelLineThickness.AutoSize = true;
            this.labelLineThickness.Location = new System.Drawing.Point(85, 255);
            this.labelLineThickness.Name = "labelLineThickness";
            this.labelLineThickness.Size = new System.Drawing.Size(107, 21);
            this.labelLineThickness.TabIndex = 2;
            this.labelLineThickness.Text = "Line thickness";
            // 
            // panelMode
            // 
            this.panelMode.Controls.Add(this.radioButtonErase);
            this.panelMode.Controls.Add(this.radioButtonBrush);
            this.panelMode.Controls.Add(this.radioButtonResizeCircle);
            this.panelMode.Controls.Add(this.radioButtonMovePolygon);
            this.panelMode.Controls.Add(this.radioButtonMoveLine);
            this.panelMode.Controls.Add(this.radioButtonMovePoint);
            this.panelMode.Controls.Add(this.radioButtonAddPolygon);
            this.panelMode.Controls.Add(this.radioButtonAddCircle);
            this.panelMode.Controls.Add(this.radioButtonAddThickLine);
            this.panelMode.Controls.Add(this.radioButtonAddThinLine);
            this.panelMode.Location = new System.Drawing.Point(6, 52);
            this.panelMode.Name = "panelMode";
            this.panelMode.Size = new System.Drawing.Size(380, 153);
            this.panelMode.TabIndex = 1;
            // 
            // radioButtonErase
            // 
            this.radioButtonErase.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonErase.AutoSize = true;
            this.radioButtonErase.Image = global::CGPart3.Properties.Resources.erase;
            this.radioButtonErase.Location = new System.Drawing.Point(307, 79);
            this.radioButtonErase.Name = "radioButtonErase";
            this.radioButtonErase.Size = new System.Drawing.Size(70, 70);
            this.radioButtonErase.TabIndex = 9;
            this.radioButtonErase.TabStop = true;
            this.radioButtonErase.UseVisualStyleBackColor = true;
            this.radioButtonErase.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonBrush
            // 
            this.radioButtonBrush.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBrush.AutoSize = true;
            this.radioButtonBrush.Image = global::CGPart3.Properties.Resources.colour;
            this.radioButtonBrush.Location = new System.Drawing.Point(231, 79);
            this.radioButtonBrush.Name = "radioButtonBrush";
            this.radioButtonBrush.Size = new System.Drawing.Size(70, 70);
            this.radioButtonBrush.TabIndex = 8;
            this.radioButtonBrush.UseVisualStyleBackColor = true;
            this.radioButtonBrush.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonResizeCircle
            // 
            this.radioButtonResizeCircle.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonResizeCircle.AutoSize = true;
            this.radioButtonResizeCircle.Image = global::CGPart3.Properties.Resources.resize_circle;
            this.radioButtonResizeCircle.Location = new System.Drawing.Point(307, 3);
            this.radioButtonResizeCircle.Name = "radioButtonResizeCircle";
            this.radioButtonResizeCircle.Size = new System.Drawing.Size(70, 70);
            this.radioButtonResizeCircle.TabIndex = 7;
            this.radioButtonResizeCircle.UseVisualStyleBackColor = true;
            this.radioButtonResizeCircle.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonMovePolygon
            // 
            this.radioButtonMovePolygon.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonMovePolygon.AutoSize = true;
            this.radioButtonMovePolygon.Image = global::CGPart3.Properties.Resources.move_polygon;
            this.radioButtonMovePolygon.Location = new System.Drawing.Point(79, 79);
            this.radioButtonMovePolygon.Name = "radioButtonMovePolygon";
            this.radioButtonMovePolygon.Size = new System.Drawing.Size(70, 70);
            this.radioButtonMovePolygon.TabIndex = 6;
            this.radioButtonMovePolygon.UseVisualStyleBackColor = true;
            this.radioButtonMovePolygon.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonMoveLine
            // 
            this.radioButtonMoveLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonMoveLine.AutoSize = true;
            this.radioButtonMoveLine.Image = global::CGPart3.Properties.Resources.move_line;
            this.radioButtonMoveLine.Location = new System.Drawing.Point(155, 79);
            this.radioButtonMoveLine.Name = "radioButtonMoveLine";
            this.radioButtonMoveLine.Size = new System.Drawing.Size(70, 70);
            this.radioButtonMoveLine.TabIndex = 5;
            this.radioButtonMoveLine.UseVisualStyleBackColor = true;
            this.radioButtonMoveLine.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonMovePoint
            // 
            this.radioButtonMovePoint.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonMovePoint.AutoSize = true;
            this.radioButtonMovePoint.Image = global::CGPart3.Properties.Resources.move_edge;
            this.radioButtonMovePoint.Location = new System.Drawing.Point(3, 79);
            this.radioButtonMovePoint.Name = "radioButtonMovePoint";
            this.radioButtonMovePoint.Size = new System.Drawing.Size(70, 70);
            this.radioButtonMovePoint.TabIndex = 4;
            this.radioButtonMovePoint.UseVisualStyleBackColor = true;
            this.radioButtonMovePoint.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonAddPolygon
            // 
            this.radioButtonAddPolygon.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAddPolygon.AutoSize = true;
            this.radioButtonAddPolygon.Image = global::CGPart3.Properties.Resources.add_polygon;
            this.radioButtonAddPolygon.Location = new System.Drawing.Point(231, 3);
            this.radioButtonAddPolygon.Name = "radioButtonAddPolygon";
            this.radioButtonAddPolygon.Size = new System.Drawing.Size(70, 70);
            this.radioButtonAddPolygon.TabIndex = 3;
            this.radioButtonAddPolygon.UseVisualStyleBackColor = true;
            this.radioButtonAddPolygon.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonAddCircle
            // 
            this.radioButtonAddCircle.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAddCircle.AutoSize = true;
            this.radioButtonAddCircle.Image = global::CGPart3.Properties.Resources.add_circle;
            this.radioButtonAddCircle.Location = new System.Drawing.Point(155, 3);
            this.radioButtonAddCircle.Name = "radioButtonAddCircle";
            this.radioButtonAddCircle.Size = new System.Drawing.Size(70, 70);
            this.radioButtonAddCircle.TabIndex = 2;
            this.radioButtonAddCircle.UseVisualStyleBackColor = true;
            this.radioButtonAddCircle.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonAddThickLine
            // 
            this.radioButtonAddThickLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAddThickLine.AutoSize = true;
            this.radioButtonAddThickLine.Image = global::CGPart3.Properties.Resources.add_thick_line;
            this.radioButtonAddThickLine.Location = new System.Drawing.Point(79, 3);
            this.radioButtonAddThickLine.Name = "radioButtonAddThickLine";
            this.radioButtonAddThickLine.Size = new System.Drawing.Size(70, 70);
            this.radioButtonAddThickLine.TabIndex = 1;
            this.radioButtonAddThickLine.UseVisualStyleBackColor = true;
            this.radioButtonAddThickLine.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonAddThinLine
            // 
            this.radioButtonAddThinLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAddThinLine.AutoSize = true;
            this.radioButtonAddThinLine.Image = global::CGPart3.Properties.Resources.add_thin_line;
            this.radioButtonAddThinLine.Location = new System.Drawing.Point(3, 3);
            this.radioButtonAddThinLine.Name = "radioButtonAddThinLine";
            this.radioButtonAddThinLine.Size = new System.Drawing.Size(70, 70);
            this.radioButtonAddThinLine.TabIndex = 0;
            this.radioButtonAddThinLine.UseVisualStyleBackColor = true;
            this.radioButtonAddThinLine.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 836);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.pictureBoxMain);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineThickness)).EndInit();
            this.panelMode.ResumeLayout(false);
            this.panelMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Panel panelMode;
        private System.Windows.Forms.RadioButton radioButtonAddCircle;
        private System.Windows.Forms.RadioButton radioButtonAddThickLine;
        private System.Windows.Forms.RadioButton radioButtonAddThinLine;
        private System.Windows.Forms.Button buttonClearAll;
        private System.Windows.Forms.RadioButton radioButtonBrush;
        private System.Windows.Forms.RadioButton radioButtonResizeCircle;
        private System.Windows.Forms.RadioButton radioButtonMovePolygon;
        private System.Windows.Forms.RadioButton radioButtonMoveLine;
        private System.Windows.Forms.RadioButton radioButtonMovePoint;
        private System.Windows.Forms.RadioButton radioButtonAddPolygon;
        private System.Windows.Forms.RadioButton radioButtonErase;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.Label labelBrush;
        private System.Windows.Forms.Label labelThickLine;
        private System.Windows.Forms.Label labelBrushColor;
        private System.Windows.Forms.Button buttonBrushColor;
        private System.Windows.Forms.NumericUpDown numericUpDownLineThickness;
        private System.Windows.Forms.Label labelLineThickness;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label labelAntiAliasing;
        private System.Windows.Forms.CheckBox checkBoxAntiAliasing;
    }
}

