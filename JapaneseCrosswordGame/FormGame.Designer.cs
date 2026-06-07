namespace JapaneseCrosswordGame
{
    partial class FormGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.lblLevelTitle = new System.Windows.Forms.Label();
            this.lblLives = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutGrid = new System.Windows.Forms.TableLayoutPanel();
            this.btnBackToMenu = new System.Windows.Forms.Button();
            this.btnGiveUp = new System.Windows.Forms.Button();
            this.btnHint = new System.Windows.Forms.Button();
            this.lblHintsRemaining = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLevelTitle
            // 
            this.lblLevelTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblLevelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLevelTitle.Location = new System.Drawing.Point(350, 20);
            this.lblLevelTitle.Name = "lblLevelTitle";
            this.lblLevelTitle.Size = new System.Drawing.Size(300, 35);
            this.lblLevelTitle.TabIndex = 0;
            this.lblLevelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLives
            // 
            this.lblLives.BackColor = System.Drawing.Color.Transparent;
            this.lblLives.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLives.ForeColor = System.Drawing.Color.Crimson;
            this.lblLives.Location = new System.Drawing.Point(146, 20);
            this.lblLives.Name = "lblLives";
            this.lblLives.Size = new System.Drawing.Size(198, 30);
            this.lblLives.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(59, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Жизни:";
            // 
            // tableLayoutGrid
            // 
            this.tableLayoutGrid.BackColor = System.Drawing.Color.White;
            this.tableLayoutGrid.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutGrid.ColumnCount = 2;
            this.tableLayoutGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutGrid.Location = new System.Drawing.Point(200, 80);
            this.tableLayoutGrid.Name = "tableLayoutGrid";
            this.tableLayoutGrid.RowCount = 2;
            this.tableLayoutGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutGrid.Size = new System.Drawing.Size(500, 500);
            this.tableLayoutGrid.TabIndex = 3;
            // 
            // btnBackToMenu
            // 
            this.btnBackToMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBackToMenu.Location = new System.Drawing.Point(64, 729);
            this.btnBackToMenu.Name = "btnBackToMenu";
            this.btnBackToMenu.Size = new System.Drawing.Size(100, 40);
            this.btnBackToMenu.TabIndex = 4;
            this.btnBackToMenu.Text = "В меню";
            this.btnBackToMenu.UseVisualStyleBackColor = true;
            this.btnBackToMenu.Click += new System.EventHandler(this.btnBackToMenu_Click);
            // 
            // btnGiveUp
            // 
            this.btnGiveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGiveUp.Location = new System.Drawing.Point(431, 729);
            this.btnGiveUp.Name = "btnGiveUp";
            this.btnGiveUp.Size = new System.Drawing.Size(100, 40);
            this.btnGiveUp.TabIndex = 5;
            this.btnGiveUp.Text = "Сдаться";
            this.btnGiveUp.UseVisualStyleBackColor = true;
            this.btnGiveUp.Click += new System.EventHandler(this.btnGiveUp_Click);
            // 
            // btnHint
            // 
            this.btnHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnHint.Location = new System.Drawing.Point(815, 729);
            this.btnHint.Name = "btnHint";
            this.btnHint.Size = new System.Drawing.Size(140, 40);
            this.btnHint.TabIndex = 6;
            this.btnHint.Text = "Подсказка";
            this.btnHint.UseVisualStyleBackColor = true;
            this.btnHint.Click += new System.EventHandler(this.btnHint_Click);
            // 
            // lblHintsRemaining
            // 
            this.lblHintsRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblHintsRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHintsRemaining.Location = new System.Drawing.Point(700, 25);
            this.lblHintsRemaining.Name = "lblHintsRemaining";
            this.lblHintsRemaining.Size = new System.Drawing.Size(255, 30);
            this.lblHintsRemaining.TabIndex = 7;
            this.lblHintsRemaining.Click += new System.EventHandler(this.lblHintsRemaining_Click);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(982, 803);
            this.Controls.Add(this.lblHintsRemaining);
            this.Controls.Add(this.btnHint);
            this.Controls.Add(this.btnGiveUp);
            this.Controls.Add(this.btnBackToMenu);
            this.Controls.Add(this.tableLayoutGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLives);
            this.Controls.Add(this.lblLevelTitle);
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Японский кроссворд - Игра";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLevelTitle;
        private System.Windows.Forms.Label lblLives;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutGrid;
        private System.Windows.Forms.Button btnBackToMenu;
        private System.Windows.Forms.Button btnGiveUp;
        private System.Windows.Forms.Button btnHint;
        private System.Windows.Forms.Label lblHintsRemaining;
    }
}