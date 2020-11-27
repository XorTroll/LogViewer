namespace LogViewer
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LogBox = new System.Windows.Forms.TextBox();
            this.ProcessComboBox = new System.Windows.Forms.ComboBox();
            this.LogPacketGrid = new System.Windows.Forms.PropertyGrid();
            this.ClickCheckBox = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.OpenButton = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectoryOpenButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutButton = new System.Windows.Forms.ToolStripButton();
            this.ProcessLabel = new System.Windows.Forms.Label();
            this.SerialComboBox = new System.Windows.Forms.ComboBox();
            this.DateComboBox = new System.Windows.Forms.ComboBox();
            this.SerialLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogBox
            // 
            this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox.BackColor = System.Drawing.Color.Black;
            this.LogBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogBox.ForeColor = System.Drawing.SystemColors.Window;
            this.LogBox.Location = new System.Drawing.Point(12, 81);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogBox.Size = new System.Drawing.Size(967, 541);
            this.LogBox.TabIndex = 1;
            this.LogBox.WordWrap = false;
            this.LogBox.Click += new System.EventHandler(this.LogBox_Click);
            // 
            // ProcessComboBox
            // 
            this.ProcessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProcessComboBox.FormattingEnabled = true;
            this.ProcessComboBox.Location = new System.Drawing.Point(12, 48);
            this.ProcessComboBox.Name = "ProcessComboBox";
            this.ProcessComboBox.Size = new System.Drawing.Size(265, 21);
            this.ProcessComboBox.TabIndex = 3;
            this.ProcessComboBox.SelectedIndexChanged += new System.EventHandler(this.ProcessComboBox_SelectedIndexChanged);
            // 
            // LogPacketGrid
            // 
            this.LogPacketGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogPacketGrid.Location = new System.Drawing.Point(985, 81);
            this.LogPacketGrid.Name = "LogPacketGrid";
            this.LogPacketGrid.Size = new System.Drawing.Size(360, 541);
            this.LogPacketGrid.TabIndex = 4;
            // 
            // ClickCheckBox
            // 
            this.ClickCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClickCheckBox.AutoSize = true;
            this.ClickCheckBox.Checked = true;
            this.ClickCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ClickCheckBox.Location = new System.Drawing.Point(1207, 48);
            this.ClickCheckBox.Name = "ClickCheckBox";
            this.ClickCheckBox.Size = new System.Drawing.Size(138, 17);
            this.ClickCheckBox.TabIndex = 5;
            this.ClickCheckBox.Text = "Click selects log packet";
            this.ClickCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.AboutButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1357, 22);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenButton,
            this.DirectoryOpenButton,
            this.ExitButton});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(38, 19);
            this.toolStripButton1.Text = "File";
            // 
            // OpenButton
            // 
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(137, 22);
            this.OpenButton.Text = "Open file";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // DirectoryOpenButton
            // 
            this.DirectoryOpenButton.Name = "DirectoryOpenButton";
            this.DirectoryOpenButton.Size = new System.Drawing.Size(137, 22);
            this.DirectoryOpenButton.Text = "Open folder";
            this.DirectoryOpenButton.Click += new System.EventHandler(this.DirectoryOpenButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(137, 22);
            this.ExitButton.Text = "Exit";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // AboutButton
            // 
            this.AboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AboutButton.Image = ((System.Drawing.Image)(resources.GetObject("AboutButton.Image")));
            this.AboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(44, 19);
            this.AboutButton.Text = "About";
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // ProcessLabel
            // 
            this.ProcessLabel.AutoSize = true;
            this.ProcessLabel.Location = new System.Drawing.Point(9, 30);
            this.ProcessLabel.Name = "ProcessLabel";
            this.ProcessLabel.Size = new System.Drawing.Size(67, 13);
            this.ProcessLabel.TabIndex = 7;
            this.ProcessLabel.Text = "Process logs";
            // 
            // SerialComboBox
            // 
            this.SerialComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SerialComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialComboBox.Enabled = false;
            this.SerialComboBox.FormattingEnabled = true;
            this.SerialComboBox.Location = new System.Drawing.Point(499, 48);
            this.SerialComboBox.Name = "SerialComboBox";
            this.SerialComboBox.Size = new System.Drawing.Size(181, 21);
            this.SerialComboBox.TabIndex = 8;
            this.SerialComboBox.SelectedIndexChanged += new System.EventHandler(this.SerialComboBox_SelectedIndexChanged);
            // 
            // DateComboBox
            // 
            this.DateComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DateComboBox.Enabled = false;
            this.DateComboBox.FormattingEnabled = true;
            this.DateComboBox.Location = new System.Drawing.Point(735, 48);
            this.DateComboBox.Name = "DateComboBox";
            this.DateComboBox.Size = new System.Drawing.Size(181, 21);
            this.DateComboBox.TabIndex = 9;
            this.DateComboBox.SelectedIndexChanged += new System.EventHandler(this.DateComboBox_SelectedIndexChanged);
            // 
            // SerialLabel
            // 
            this.SerialLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SerialLabel.AutoSize = true;
            this.SerialLabel.Location = new System.Drawing.Point(496, 30);
            this.SerialLabel.Name = "SerialLabel";
            this.SerialLabel.Size = new System.Drawing.Size(116, 13);
            this.SerialLabel.TabIndex = 10;
            this.SerialLabel.Text = "Console (serial number)";
            // 
            // DateLabel
            // 
            this.DateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DateLabel.AutoSize = true;
            this.DateLabel.Location = new System.Drawing.Point(732, 30);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(73, 13);
            this.DateLabel.TabIndex = 11;
            this.DateLabel.Text = "Log date/time";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 634);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.SerialLabel);
            this.Controls.Add(this.DateComboBox);
            this.Controls.Add(this.SerialComboBox);
            this.Controls.Add(this.ProcessLabel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ClickCheckBox);
            this.Controls.Add(this.LogPacketGrid);
            this.Controls.Add(this.ProcessComboBox);
            this.Controls.Add(this.LogBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "LogViewer - Binary log viewer";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.ComboBox ProcessComboBox;
        private System.Windows.Forms.PropertyGrid LogPacketGrid;
        private System.Windows.Forms.CheckBox ClickCheckBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem OpenButton;
        private System.Windows.Forms.ToolStripMenuItem DirectoryOpenButton;
        private System.Windows.Forms.ToolStripMenuItem ExitButton;
        private System.Windows.Forms.ToolStripButton AboutButton;
        private System.Windows.Forms.Label ProcessLabel;
        private System.Windows.Forms.ComboBox SerialComboBox;
        private System.Windows.Forms.ComboBox DateComboBox;
        private System.Windows.Forms.Label SerialLabel;
        private System.Windows.Forms.Label DateLabel;
    }
}

