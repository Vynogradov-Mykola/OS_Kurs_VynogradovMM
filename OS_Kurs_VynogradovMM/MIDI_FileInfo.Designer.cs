namespace OS_Kurs_VynogradovMM
{
    partial class MIDI_FileInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MIDI = new System.Windows.Forms.ListBox();
            this.LeftBtn = new System.Windows.Forms.Button();
            this.RigthBtn = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.PathBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.PathLabel = new System.Windows.Forms.Label();
            this.DeltaTimeLab = new System.Windows.Forms.Label();
            this.DeltaTimeText = new System.Windows.Forms.TextBox();
            this.ChangeInfoBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MIDI
            // 
            this.MIDI.FormattingEnabled = true;
            this.MIDI.ItemHeight = 16;
            this.MIDI.Location = new System.Drawing.Point(250, 1);
            this.MIDI.Name = "MIDI";
            this.MIDI.Size = new System.Drawing.Size(325, 260);
            this.MIDI.TabIndex = 0;
            // 
            // LeftBtn
            // 
            this.LeftBtn.Location = new System.Drawing.Point(250, 267);
            this.LeftBtn.Name = "LeftBtn";
            this.LeftBtn.Size = new System.Drawing.Size(75, 23);
            this.LeftBtn.TabIndex = 1;
            this.LeftBtn.Text = "<===";
            this.LeftBtn.UseVisualStyleBackColor = true;
            this.LeftBtn.Click += new System.EventHandler(this.LeftBtn_Click);
            // 
            // RigthBtn
            // 
            this.RigthBtn.Location = new System.Drawing.Point(500, 267);
            this.RigthBtn.Name = "RigthBtn";
            this.RigthBtn.Size = new System.Drawing.Size(75, 23);
            this.RigthBtn.TabIndex = 2;
            this.RigthBtn.Text = "===>";
            this.RigthBtn.UseVisualStyleBackColor = true;
            this.RigthBtn.Click += new System.EventHandler(this.RigthBtn_Click);
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(50, 1);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(194, 22);
            this.NameBox.TabIndex = 3;
            // 
            // PathBox
            // 
            this.PathBox.Location = new System.Drawing.Point(50, 29);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(194, 22);
            this.PathBox.TabIndex = 4;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(-1, 1);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(45, 17);
            this.NameLabel.TabIndex = 5;
            this.NameLabel.Text = "Name";
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.Location = new System.Drawing.Point(-1, 29);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(37, 17);
            this.PathLabel.TabIndex = 6;
            this.PathLabel.Text = "Path";
            // 
            // DeltaTimeLab
            // 
            this.DeltaTimeLab.AutoSize = true;
            this.DeltaTimeLab.Location = new System.Drawing.Point(-1, 85);
            this.DeltaTimeLab.Name = "DeltaTimeLab";
            this.DeltaTimeLab.Size = new System.Drawing.Size(71, 17);
            this.DeltaTimeLab.TabIndex = 10;
            this.DeltaTimeLab.Text = "Delta time";
            // 
            // DeltaTimeText
            // 
            this.DeltaTimeText.Location = new System.Drawing.Point(88, 85);
            this.DeltaTimeText.Name = "DeltaTimeText";
            this.DeltaTimeText.Size = new System.Drawing.Size(156, 22);
            this.DeltaTimeText.TabIndex = 9;
            // 
            // ChangeInfoBtn
            // 
            this.ChangeInfoBtn.Location = new System.Drawing.Point(0, 113);
            this.ChangeInfoBtn.Name = "ChangeInfoBtn";
            this.ChangeInfoBtn.Size = new System.Drawing.Size(244, 23);
            this.ChangeInfoBtn.TabIndex = 11;
            this.ChangeInfoBtn.Text = "Change";
            this.ChangeInfoBtn.UseVisualStyleBackColor = true;
            this.ChangeInfoBtn.Click += new System.EventHandler(this.ChangeInfoBtn_Click);
            // 
            // MIDI_FileInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 296);
            this.Controls.Add(this.ChangeInfoBtn);
            this.Controls.Add(this.DeltaTimeLab);
            this.Controls.Add(this.DeltaTimeText);
            this.Controls.Add(this.PathLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.PathBox);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.RigthBtn);
            this.Controls.Add(this.LeftBtn);
            this.Controls.Add(this.MIDI);
            this.Name = "MIDI_FileInfo";
            this.Text = "MIDI_FileInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox MIDI;
        private System.Windows.Forms.Button LeftBtn;
        private System.Windows.Forms.Button RigthBtn;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.Label DeltaTimeLab;
        private System.Windows.Forms.TextBox DeltaTimeText;
        private System.Windows.Forms.Button ChangeInfoBtn;
    }
}