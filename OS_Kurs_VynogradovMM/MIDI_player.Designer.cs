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
            // MIDI_player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 343);
            this.Controls.Add(this.AddFile);
            this.Controls.Add(this.Files);
            this.Name = "MIDI_player";
            this.Text = "MIDI player";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Files;
        private System.Windows.Forms.Button AddFile;
    }
}

