namespace OS_Kurs_VynogradovMM
{
    partial class MIDI_player
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Files = new System.Windows.Forms.ListBox();
            this.AddFile = new System.Windows.Forms.Button();
            this.RemoveBtn = new System.Windows.Forms.Button();
            this.PlayBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PlayerBar = new System.Windows.Forms.TrackBar();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.ContinueBtn = new System.Windows.Forms.Button();
            this.SpeedLabelText = new System.Windows.Forms.Label();
            this.SpeedLabChange = new System.Windows.Forms.Label();
            this.AddSpeed = new System.Windows.Forms.Button();
            this.SlowSpeed = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Files
            // 
            this.Files.FormattingEnabled = true;
            this.Files.ItemHeight = 16;
            this.Files.Location = new System.Drawing.Point(12, 210);
            this.Files.Name = "Files";
            this.Files.Size = new System.Drawing.Size(507, 132);
            this.Files.TabIndex = 0;
            this.Files.DoubleClick += new System.EventHandler(this.Files_DoubleClick);
            // 
            // AddFile
            // 
            this.AddFile.Location = new System.Drawing.Point(12, 181);
            this.AddFile.Name = "AddFile";
            this.AddFile.Size = new System.Drawing.Size(75, 23);
            this.AddFile.TabIndex = 1;
            this.AddFile.Text = "Add file";
            this.AddFile.UseVisualStyleBackColor = true;
            this.AddFile.Click += new System.EventHandler(this.AddFile_Click);
            // 
            // RemoveBtn
            // 
            this.RemoveBtn.Location = new System.Drawing.Point(444, 181);
            this.RemoveBtn.Name = "RemoveBtn";
            this.RemoveBtn.Size = new System.Drawing.Size(75, 23);
            this.RemoveBtn.TabIndex = 2;
            this.RemoveBtn.Text = "Remove";
            this.RemoveBtn.UseVisualStyleBackColor = true;
            this.RemoveBtn.Click += new System.EventHandler(this.RemoveBtn_Click);
            // 
            // PlayBtn
            // 
            this.PlayBtn.Location = new System.Drawing.Point(241, 181);
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.Size = new System.Drawing.Size(75, 23);
            this.PlayBtn.TabIndex = 3;
            this.PlayBtn.Text = "Play";
            this.PlayBtn.UseVisualStyleBackColor = true;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // PlayerBar
            // 
            this.PlayerBar.Location = new System.Drawing.Point(12, 119);
            this.PlayerBar.Name = "PlayerBar";
            this.PlayerBar.Size = new System.Drawing.Size(507, 56);
            this.PlayerBar.TabIndex = 7;
            // 
            // PauseBtn
            // 
            this.PauseBtn.Location = new System.Drawing.Point(322, 181);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(66, 23);
            this.PauseBtn.TabIndex = 8;
            this.PauseBtn.Text = "Pause";
            this.PauseBtn.UseVisualStyleBackColor = true;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.Location = new System.Drawing.Point(160, 181);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(75, 23);
            this.ContinueBtn.TabIndex = 9;
            this.ContinueBtn.Text = "Continue";
            this.ContinueBtn.UseVisualStyleBackColor = true;
            this.ContinueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // SpeedLabelText
            // 
            this.SpeedLabelText.AutoSize = true;
            this.SpeedLabelText.Location = new System.Drawing.Point(246, 9);
            this.SpeedLabelText.Name = "SpeedLabelText";
            this.SpeedLabelText.Size = new System.Drawing.Size(49, 17);
            this.SpeedLabelText.TabIndex = 10;
            this.SpeedLabelText.Text = "Speed";
            // 
            // SpeedLabChange
            // 
            this.SpeedLabChange.AutoSize = true;
            this.SpeedLabChange.Location = new System.Drawing.Point(261, 26);
            this.SpeedLabChange.Name = "SpeedLabChange";
            this.SpeedLabChange.Size = new System.Drawing.Size(16, 17);
            this.SpeedLabChange.TabIndex = 11;
            this.SpeedLabChange.Text = "1";
            // 
            // AddSpeed
            // 
            this.AddSpeed.Location = new System.Drawing.Point(444, 6);
            this.AddSpeed.Name = "AddSpeed";
            this.AddSpeed.Size = new System.Drawing.Size(75, 23);
            this.AddSpeed.TabIndex = 12;
            this.AddSpeed.Text = "Faster";
            this.AddSpeed.UseVisualStyleBackColor = true;
            this.AddSpeed.Click += new System.EventHandler(this.AddSpeed_Click);
            // 
            // SlowSpeed
            // 
            this.SlowSpeed.Location = new System.Drawing.Point(12, 6);
            this.SlowSpeed.Name = "SlowSpeed";
            this.SlowSpeed.Size = new System.Drawing.Size(75, 23);
            this.SlowSpeed.TabIndex = 13;
            this.SlowSpeed.Text = "Slow";
            this.SlowSpeed.UseVisualStyleBackColor = true;
            this.SlowSpeed.Click += new System.EventHandler(this.SlowSpeed_Click);
            // 
            // MIDI_player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 352);
            this.Controls.Add(this.SlowSpeed);
            this.Controls.Add(this.AddSpeed);
            this.Controls.Add(this.SpeedLabChange);
            this.Controls.Add(this.SpeedLabelText);
            this.Controls.Add(this.ContinueBtn);
            this.Controls.Add(this.PauseBtn);
            this.Controls.Add(this.PlayerBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayBtn);
            this.Controls.Add(this.RemoveBtn);
            this.Controls.Add(this.AddFile);
            this.Controls.Add(this.Files);
            this.Name = "MIDI_player";
            this.Text = "MIDI player";
            ((System.ComponentModel.ISupportInitialize)(this.PlayerBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Files;
        private System.Windows.Forms.Button AddFile;
        private System.Windows.Forms.Button RemoveBtn;
        private System.Windows.Forms.Button PlayBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar PlayerBar;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Button ContinueBtn;
        private System.Windows.Forms.Label SpeedLabelText;
        private System.Windows.Forms.Label SpeedLabChange;
        private System.Windows.Forms.Button AddSpeed;
        private System.Windows.Forms.Button SlowSpeed;
    }
}

