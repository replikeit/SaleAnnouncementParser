namespace Parser
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
            this.songsDataGridView = new System.Windows.Forms.DataGridView();
            this.gameNamesCombo = new System.Windows.Forms.ComboBox();
            this.gameCategoryCombo = new System.Windows.Forms.ComboBox();
            this.serverLocationCombo = new System.Windows.Forms.ComboBox();
            this.underCategoryCombo1 = new System.Windows.Forms.ComboBox();
            this.underCategoryCombo2 = new System.Windows.Forms.ComboBox();
            this.showOnlineCheckBox = new System.Windows.Forms.CheckBox();
            this.refreshDataButton = new System.Windows.Forms.Button();
            this.loadingLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.songsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // songsDataGridView
            // 
            this.songsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.songsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.songsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.songsDataGridView.Location = new System.Drawing.Point(12, 86);
            this.songsDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.songsDataGridView.Name = "songsDataGridView";
            this.songsDataGridView.RowHeadersWidth = 51;
            this.songsDataGridView.RowTemplate.Height = 24;
            this.songsDataGridView.Size = new System.Drawing.Size(1488, 543);
            this.songsDataGridView.StandardTab = true;
            this.songsDataGridView.TabIndex = 8;
            // 
            // gameNamesCombo
            // 
            this.gameNamesCombo.FormattingEnabled = true;
            this.gameNamesCombo.Location = new System.Drawing.Point(12, 9);
            this.gameNamesCombo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gameNamesCombo.Name = "gameNamesCombo";
            this.gameNamesCombo.Size = new System.Drawing.Size(329, 24);
            this.gameNamesCombo.TabIndex = 9;
            this.gameNamesCombo.Text = "Game Names";
            this.gameNamesCombo.SelectedIndexChanged += new System.EventHandler(this.GamesWowCombo_SelectedIndexChanged);
            // 
            // gameCategoryCombo
            // 
            this.gameCategoryCombo.FormattingEnabled = true;
            this.gameCategoryCombo.Location = new System.Drawing.Point(347, 9);
            this.gameCategoryCombo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gameCategoryCombo.Name = "gameCategoryCombo";
            this.gameCategoryCombo.Size = new System.Drawing.Size(185, 24);
            this.gameCategoryCombo.TabIndex = 10;
            this.gameCategoryCombo.Text = "Category";
            this.gameCategoryCombo.Visible = false;
            this.gameCategoryCombo.SelectedIndexChanged += new System.EventHandler(this.gameCategoryCombo_SelectedIndexChanged);
            // 
            // serverLocationCombo
            // 
            this.serverLocationCombo.FormattingEnabled = true;
            this.serverLocationCombo.Location = new System.Drawing.Point(538, 9);
            this.serverLocationCombo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.serverLocationCombo.Name = "serverLocationCombo";
            this.serverLocationCombo.Size = new System.Drawing.Size(202, 24);
            this.serverLocationCombo.TabIndex = 11;
            this.serverLocationCombo.Text = "Server Location";
            this.serverLocationCombo.Visible = false;
            this.serverLocationCombo.SelectedIndexChanged += new System.EventHandler(this.serverLocationCombo_SelectedIndexChanged);
            // 
            // underCategoryCombo1
            // 
            this.underCategoryCombo1.FormattingEnabled = true;
            this.underCategoryCombo1.Location = new System.Drawing.Point(156, 44);
            this.underCategoryCombo1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.underCategoryCombo1.Name = "underCategoryCombo1";
            this.underCategoryCombo1.Size = new System.Drawing.Size(275, 24);
            this.underCategoryCombo1.TabIndex = 13;
            this.underCategoryCombo1.Tag = "1";
            this.underCategoryCombo1.Text = "Server Location";
            this.underCategoryCombo1.Visible = false;
            this.underCategoryCombo1.SelectedIndexChanged += new System.EventHandler(this.underCategoryCombo_SelectedIndexChanged);
            // 
            // underCategoryCombo2
            // 
            this.underCategoryCombo2.FormattingEnabled = true;
            this.underCategoryCombo2.Location = new System.Drawing.Point(437, 44);
            this.underCategoryCombo2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.underCategoryCombo2.Name = "underCategoryCombo2";
            this.underCategoryCombo2.Size = new System.Drawing.Size(185, 24);
            this.underCategoryCombo2.TabIndex = 14;
            this.underCategoryCombo2.Tag = "2";
            this.underCategoryCombo2.Text = "Server Location";
            this.underCategoryCombo2.Visible = false;
            this.underCategoryCombo2.SelectedIndexChanged += new System.EventHandler(this.underCategoryCombo_SelectedIndexChanged);
            // 
            // showOnlineCheckBox
            // 
            this.showOnlineCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showOnlineCheckBox.AutoSize = true;
            this.showOnlineCheckBox.Location = new System.Drawing.Point(1403, 59);
            this.showOnlineCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.showOnlineCheckBox.Name = "showOnlineCheckBox";
            this.showOnlineCheckBox.Size = new System.Drawing.Size(104, 21);
            this.showOnlineCheckBox.TabIndex = 15;
            this.showOnlineCheckBox.Text = "Online Only";
            this.showOnlineCheckBox.UseVisualStyleBackColor = true;
            this.showOnlineCheckBox.CheckedChanged += new System.EventHandler(this.showOnlineCheckBox_CheckedChanged);
            // 
            // refreshDataButton
            // 
            this.refreshDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshDataButton.Location = new System.Drawing.Point(1403, 9);
            this.refreshDataButton.Name = "refreshDataButton";
            this.refreshDataButton.Size = new System.Drawing.Size(97, 43);
            this.refreshDataButton.TabIndex = 16;
            this.refreshDataButton.Text = "Refresh Data";
            this.refreshDataButton.UseVisualStyleBackColor = true;
            this.refreshDataButton.Click += new System.EventHandler(this.refreshDataButton_Click);
            // 
            // loadingLabel
            // 
            this.loadingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadingLabel.Location = new System.Drawing.Point(1287, 32);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(53, 20);
            this.loadingLabel.TabIndex = 17;
            this.loadingLabel.Text = "label1";
            this.loadingLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1512, 640);
            this.Controls.Add(this.loadingLabel);
            this.Controls.Add(this.refreshDataButton);
            this.Controls.Add(this.showOnlineCheckBox);
            this.Controls.Add(this.underCategoryCombo2);
            this.Controls.Add(this.underCategoryCombo1);
            this.Controls.Add(this.serverLocationCombo);
            this.Controls.Add(this.gameCategoryCombo);
            this.Controls.Add(this.gameNamesCombo);
            this.Controls.Add(this.songsDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Games Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.songsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView songsDataGridView;
        private System.Windows.Forms.ComboBox gameNamesCombo;
        private System.Windows.Forms.ComboBox gameCategoryCombo;
        private System.Windows.Forms.ComboBox serverLocationCombo;
        private System.Windows.Forms.ComboBox underCategoryCombo1;
        private System.Windows.Forms.ComboBox underCategoryCombo2;
        private System.Windows.Forms.CheckBox showOnlineCheckBox;
        private System.Windows.Forms.Button refreshDataButton;
        private System.Windows.Forms.Label loadingLabel;
    }
}

