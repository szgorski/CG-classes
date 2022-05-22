
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
            this.buttonSaveObject = new System.Windows.Forms.Button();
            this.labelObjectColor = new System.Windows.Forms.Label();
            this.buttonObjectColor = new System.Windows.Forms.Button();
            this.numericUpDownObjectThickness = new System.Windows.Forms.NumericUpDown();
            this.labelObjectThickness = new System.Windows.Forms.Label();
            this.labelObject = new System.Windows.Forms.Label();
            this.labelGeneral = new System.Windows.Forms.Label();
            this.checkBoxAntiAliasing = new System.Windows.Forms.CheckBox();
            this.labelMode = new System.Windows.Forms.Label();
            this.buttonClearAll = new System.Windows.Forms.Button();
            this.labelBrushColor = new System.Windows.Forms.Label();
            this.buttonBrushColor = new System.Windows.Forms.Button();
            this.numericUpDownLineThickness = new System.Windows.Forms.NumericUpDown();
            this.labelLineThickness = new System.Windows.Forms.Label();
            this.panelMode = new System.Windows.Forms.Panel();
            this.radioButtonSelectObject = new System.Windows.Forms.RadioButton();
            this.radioButtonErase = new System.Windows.Forms.RadioButton();
            this.radioButtonBrush = new System.Windows.Forms.RadioButton();
            this.radioButtonResizeCircle = new System.Windows.Forms.RadioButton();
            this.radioButtonMovePolygon = new System.Windows.Forms.RadioButton();
            this.radioButtonMoveLine = new System.Windows.Forms.RadioButton();
            this.radioButtonMovePoint = new System.Windows.Forms.RadioButton();
            this.radioButtonAddPolygon = new System.Windows.Forms.RadioButton();
            this.radioButtonAddCircle = new System.Windows.Forms.RadioButton();
            this.radioButtonAddLine = new System.Windows.Forms.RadioButton();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.groupBoxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObjectThickness)).BeginInit();
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
            this.groupBoxSettings.Controls.Add(this.buttonSaveObject);
            this.groupBoxSettings.Controls.Add(this.labelObjectColor);
            this.groupBoxSettings.Controls.Add(this.buttonObjectColor);
            this.groupBoxSettings.Controls.Add(this.numericUpDownObjectThickness);
            this.groupBoxSettings.Controls.Add(this.labelObjectThickness);
            this.groupBoxSettings.Controls.Add(this.labelObject);
            this.groupBoxSettings.Controls.Add(this.labelGeneral);
            this.groupBoxSettings.Controls.Add(this.checkBoxAntiAliasing);
            this.groupBoxSettings.Controls.Add(this.labelMode);
            this.groupBoxSettings.Controls.Add(this.buttonClearAll);
            this.groupBoxSettings.Controls.Add(this.labelBrushColor);
            this.groupBoxSettings.Controls.Add(this.buttonBrushColor);
            this.groupBoxSettings.Controls.Add(this.numericUpDownLineThickness);
            this.groupBoxSettings.Controls.Add(this.labelLineThickness);
            this.groupBoxSettings.Controls.Add(this.panelMode);
            this.groupBoxSettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxSettings.Location = new System.Drawing.Point(13, 16);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(392, 811);
            this.groupBoxSettings.TabIndex = 1;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // buttonSaveObject
            // 
            this.buttonSaveObject.Enabled = false;
            this.buttonSaveObject.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSaveObject.Location = new System.Drawing.Point(85, 649);
            this.buttonSaveObject.Name = "buttonSaveObject";
            this.buttonSaveObject.Size = new System.Drawing.Size(222, 32);
            this.buttonSaveObject.TabIndex = 17;
            this.buttonSaveObject.Text = "Save changes";
            this.buttonSaveObject.UseVisualStyleBackColor = true;
            this.buttonSaveObject.Click += new System.EventHandler(this.buttonSaveObject_Click);
            // 
            // labelObjectColor
            // 
            this.labelObjectColor.AutoSize = true;
            this.labelObjectColor.BackColor = System.Drawing.Color.Silver;
            this.labelObjectColor.Location = new System.Drawing.Point(85, 603);
            this.labelObjectColor.MaximumSize = new System.Drawing.Size(30, 30);
            this.labelObjectColor.MinimumSize = new System.Drawing.Size(30, 30);
            this.labelObjectColor.Name = "labelObjectColor";
            this.labelObjectColor.Size = new System.Drawing.Size(30, 30);
            this.labelObjectColor.TabIndex = 16;
            this.labelObjectColor.Text = "   ";
            // 
            // buttonObjectColor
            // 
            this.buttonObjectColor.Enabled = false;
            this.buttonObjectColor.Location = new System.Drawing.Point(124, 602);
            this.buttonObjectColor.Name = "buttonObjectColor";
            this.buttonObjectColor.Size = new System.Drawing.Size(183, 32);
            this.buttonObjectColor.TabIndex = 15;
            this.buttonObjectColor.Text = "Set brush colour";
            this.buttonObjectColor.UseVisualStyleBackColor = true;
            this.buttonObjectColor.Click += new System.EventHandler(this.buttonObjectColor_Click);
            // 
            // numericUpDownObjectThickness
            // 
            this.numericUpDownObjectThickness.Enabled = false;
            this.numericUpDownObjectThickness.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownObjectThickness.Location = new System.Drawing.Point(214, 558);
            this.numericUpDownObjectThickness.Maximum = new decimal(new int[] {
            49,
            0,
            0,
            0});
            this.numericUpDownObjectThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownObjectThickness.Name = "numericUpDownObjectThickness";
            this.numericUpDownObjectThickness.Size = new System.Drawing.Size(93, 29);
            this.numericUpDownObjectThickness.TabIndex = 14;
            this.numericUpDownObjectThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownObjectThickness.ValueChanged += new System.EventHandler(this.numericUpDownObjectThickness_ValueChanged);
            // 
            // labelObjectThickness
            // 
            this.labelObjectThickness.AutoSize = true;
            this.labelObjectThickness.Location = new System.Drawing.Point(85, 560);
            this.labelObjectThickness.Name = "labelObjectThickness";
            this.labelObjectThickness.Size = new System.Drawing.Size(107, 21);
            this.labelObjectThickness.TabIndex = 13;
            this.labelObjectThickness.Text = "Line thickness";
            // 
            // labelObject
            // 
            this.labelObject.AutoSize = true;
            this.labelObject.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelObject.Location = new System.Drawing.Point(85, 533);
            this.labelObject.Name = "labelObject";
            this.labelObject.Size = new System.Drawing.Size(91, 15);
            this.labelObject.TabIndex = 12;
            this.labelObject.Text = "Object settings";
            // 
            // labelGeneral
            // 
            this.labelGeneral.AutoSize = true;
            this.labelGeneral.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelGeneral.Location = new System.Drawing.Point(85, 311);
            this.labelGeneral.Name = "labelGeneral";
            this.labelGeneral.Size = new System.Drawing.Size(98, 15);
            this.labelGeneral.TabIndex = 11;
            this.labelGeneral.Text = "General settings";
            // 
            // checkBoxAntiAliasing
            // 
            this.checkBoxAntiAliasing.AutoSize = true;
            this.checkBoxAntiAliasing.Location = new System.Drawing.Point(88, 338);
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
            this.buttonClearAll.Location = new System.Drawing.Point(85, 460);
            this.buttonClearAll.Name = "buttonClearAll";
            this.buttonClearAll.Size = new System.Drawing.Size(222, 32);
            this.buttonClearAll.TabIndex = 0;
            this.buttonClearAll.Text = "Clear the board";
            this.buttonClearAll.UseVisualStyleBackColor = true;
            this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
            // 
            // labelBrushColor
            // 
            this.labelBrushColor.AutoSize = true;
            this.labelBrushColor.BackColor = System.Drawing.Color.Black;
            this.labelBrushColor.Location = new System.Drawing.Point(85, 414);
            this.labelBrushColor.MaximumSize = new System.Drawing.Size(30, 30);
            this.labelBrushColor.MinimumSize = new System.Drawing.Size(30, 30);
            this.labelBrushColor.Name = "labelBrushColor";
            this.labelBrushColor.Size = new System.Drawing.Size(30, 30);
            this.labelBrushColor.TabIndex = 6;
            this.labelBrushColor.Text = "   ";
            // 
            // buttonBrushColor
            // 
            this.buttonBrushColor.Location = new System.Drawing.Point(124, 413);
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
            this.numericUpDownLineThickness.Location = new System.Drawing.Point(214, 369);
            this.numericUpDownLineThickness.Maximum = new decimal(new int[] {
            49,
            0,
            0,
            0});
            this.numericUpDownLineThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLineThickness.Name = "numericUpDownLineThickness";
            this.numericUpDownLineThickness.Size = new System.Drawing.Size(93, 29);
            this.numericUpDownLineThickness.TabIndex = 3;
            this.numericUpDownLineThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLineThickness.ValueChanged += new System.EventHandler(this.numericUpDownLineThickness_ValueChanged);
            // 
            // labelLineThickness
            // 
            this.labelLineThickness.AutoSize = true;
            this.labelLineThickness.Location = new System.Drawing.Point(85, 371);
            this.labelLineThickness.Name = "labelLineThickness";
            this.labelLineThickness.Size = new System.Drawing.Size(107, 21);
            this.labelLineThickness.TabIndex = 2;
            this.labelLineThickness.Text = "Line thickness";
            // 
            // panelMode
            // 
            this.panelMode.Controls.Add(this.radioButtonSelectObject);
            this.panelMode.Controls.Add(this.radioButtonErase);
            this.panelMode.Controls.Add(this.radioButtonBrush);
            this.panelMode.Controls.Add(this.radioButtonResizeCircle);
            this.panelMode.Controls.Add(this.radioButtonMovePolygon);
            this.panelMode.Controls.Add(this.radioButtonMoveLine);
            this.panelMode.Controls.Add(this.radioButtonMovePoint);
            this.panelMode.Controls.Add(this.radioButtonAddPolygon);
            this.panelMode.Controls.Add(this.radioButtonAddCircle);
            this.panelMode.Controls.Add(this.radioButtonAddLine);
            this.panelMode.Location = new System.Drawing.Point(6, 52);
            this.panelMode.Name = "panelMode";
            this.panelMode.Size = new System.Drawing.Size(380, 152);
            this.panelMode.TabIndex = 1;
            // 
            // radioButtonSelectObject
            // 
            this.radioButtonSelectObject.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonSelectObject.AutoSize = true;
            this.radioButtonSelectObject.Image = global::CGPart3.Properties.Resources.settings;
            this.radioButtonSelectObject.Location = new System.Drawing.Point(307, 3);
            this.radioButtonSelectObject.Name = "radioButtonSelectObject";
            this.radioButtonSelectObject.Size = new System.Drawing.Size(70, 70);
            this.radioButtonSelectObject.TabIndex = 11;
            this.radioButtonSelectObject.TabStop = true;
            this.radioButtonSelectObject.UseVisualStyleBackColor = true;
            this.radioButtonSelectObject.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonErase
            // 
            this.radioButtonErase.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonErase.AutoSize = true;
            this.radioButtonErase.Image = global::CGPart3.Properties.Resources.trash;
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
            this.radioButtonResizeCircle.Location = new System.Drawing.Point(231, 3);
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
            this.radioButtonAddPolygon.Location = new System.Drawing.Point(155, 3);
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
            this.radioButtonAddCircle.Location = new System.Drawing.Point(79, 3);
            this.radioButtonAddCircle.Name = "radioButtonAddCircle";
            this.radioButtonAddCircle.Size = new System.Drawing.Size(70, 70);
            this.radioButtonAddCircle.TabIndex = 2;
            this.radioButtonAddCircle.UseVisualStyleBackColor = true;
            this.radioButtonAddCircle.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
            // 
            // radioButtonAddLine
            // 
            this.radioButtonAddLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAddLine.AutoSize = true;
            this.radioButtonAddLine.Image = global::CGPart3.Properties.Resources.add_line;
            this.radioButtonAddLine.Location = new System.Drawing.Point(3, 3);
            this.radioButtonAddLine.Name = "radioButtonAddLine";
            this.radioButtonAddLine.Size = new System.Drawing.Size(70, 70);
            this.radioButtonAddLine.TabIndex = 0;
            this.radioButtonAddLine.UseVisualStyleBackColor = true;
            this.radioButtonAddLine.CheckedChanged += new System.EventHandler(this.mode_CheckedChanged);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObjectThickness)).EndInit();
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
        private System.Windows.Forms.RadioButton radioButtonAddLine;
        private System.Windows.Forms.Button buttonClearAll;
        private System.Windows.Forms.RadioButton radioButtonBrush;
        private System.Windows.Forms.RadioButton radioButtonResizeCircle;
        private System.Windows.Forms.RadioButton radioButtonMovePolygon;
        private System.Windows.Forms.RadioButton radioButtonMoveLine;
        private System.Windows.Forms.RadioButton radioButtonMovePoint;
        private System.Windows.Forms.RadioButton radioButtonAddPolygon;
        private System.Windows.Forms.RadioButton radioButtonErase;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.Label labelBrushColor;
        private System.Windows.Forms.Button buttonBrushColor;
        private System.Windows.Forms.NumericUpDown numericUpDownLineThickness;
        private System.Windows.Forms.Label labelLineThickness;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.CheckBox checkBoxAntiAliasing;
        private System.Windows.Forms.Button buttonSaveObject;
        private System.Windows.Forms.Label labelObjectColor;
        private System.Windows.Forms.Button buttonObjectColor;
        private System.Windows.Forms.NumericUpDown numericUpDownObjectThickness;
        private System.Windows.Forms.Label labelObjectThickness;
        private System.Windows.Forms.Label labelObject;
        private System.Windows.Forms.Label labelGeneral;
        private System.Windows.Forms.RadioButton radioButtonSelectObject;
    }
}

