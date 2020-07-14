namespace Server
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.onServerButton = new System.Windows.Forms.Button();
            this.offServerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // onServerButton
            // 
            this.onServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.onServerButton.Location = new System.Drawing.Point(12, 12);
            this.onServerButton.Name = "onServerButton";
            this.onServerButton.Size = new System.Drawing.Size(254, 71);
            this.onServerButton.TabIndex = 0;
            this.onServerButton.Text = "Turn On";
            this.onServerButton.UseVisualStyleBackColor = true;
            this.onServerButton.Click += new System.EventHandler(this.onServerButton_Click);
            // 
            // offServerButton
            // 
            this.offServerButton.Enabled = false;
            this.offServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.offServerButton.Location = new System.Drawing.Point(12, 89);
            this.offServerButton.Name = "offServerButton";
            this.offServerButton.Size = new System.Drawing.Size(254, 71);
            this.offServerButton.TabIndex = 1;
            this.offServerButton.Text = "Turn Off";
            this.offServerButton.UseVisualStyleBackColor = true;
            this.offServerButton.Click += new System.EventHandler(this.offServerButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 170);
            this.Controls.Add(this.offServerButton);
            this.Controls.Add(this.onServerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button onServerButton;
        private System.Windows.Forms.Button offServerButton;
    }
}

