namespace labyrinth
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
            this.PB_Game = new System.Windows.Forms.PictureBox();
            this.B_Left = new System.Windows.Forms.Button();
            this.B_Buttom = new System.Windows.Forms.Button();
            this.B_Right = new System.Windows.Forms.Button();
            this.B_Top = new System.Windows.Forms.Button();
            this.B_StartGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.B_Helper = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Game)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_Game
            // 
            this.PB_Game.Location = new System.Drawing.Point(12, 17);
            this.PB_Game.Name = "PB_Game";
            this.PB_Game.Size = new System.Drawing.Size(481, 481);
            this.PB_Game.TabIndex = 1;
            this.PB_Game.TabStop = false;
            // 
            // B_Left
            // 
            this.B_Left.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_Left.Location = new System.Drawing.Point(509, 184);
            this.B_Left.Name = "B_Left";
            this.B_Left.Size = new System.Drawing.Size(35, 35);
            this.B_Left.TabIndex = 0;
            this.B_Left.Text = "←";
            this.B_Left.UseVisualStyleBackColor = true;
            this.B_Left.Click += new System.EventHandler(this.B_Left_Click);
            // 
            // B_Buttom
            // 
            this.B_Buttom.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.B_Buttom.Location = new System.Drawing.Point(550, 225);
            this.B_Buttom.Name = "B_Buttom";
            this.B_Buttom.Size = new System.Drawing.Size(35, 35);
            this.B_Buttom.TabIndex = 0;
            this.B_Buttom.Text = "↓";
            this.B_Buttom.UseVisualStyleBackColor = true;
            this.B_Buttom.Click += new System.EventHandler(this.B_Buttom_Click);
            // 
            // B_Right
            // 
            this.B_Right.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.B_Right.Location = new System.Drawing.Point(591, 184);
            this.B_Right.Name = "B_Right";
            this.B_Right.Size = new System.Drawing.Size(35, 35);
            this.B_Right.TabIndex = 0;
            this.B_Right.Text = "→";
            this.B_Right.UseVisualStyleBackColor = true;
            this.B_Right.Click += new System.EventHandler(this.B_Right_Click);
            // 
            // B_Top
            // 
            this.B_Top.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.B_Top.Location = new System.Drawing.Point(550, 143);
            this.B_Top.Name = "B_Top";
            this.B_Top.Size = new System.Drawing.Size(35, 35);
            this.B_Top.TabIndex = 0;
            this.B_Top.Text = "↑";
            this.B_Top.UseVisualStyleBackColor = true;
            this.B_Top.Click += new System.EventHandler(this.B_Top_Click);
            // 
            // B_StartGame
            // 
            this.B_StartGame.Location = new System.Drawing.Point(535, 474);
            this.B_StartGame.Name = "B_StartGame";
            this.B_StartGame.Size = new System.Drawing.Size(75, 23);
            this.B_StartGame.TabIndex = 0;
            this.B_StartGame.Text = "Старт";
            this.B_StartGame.UseVisualStyleBackColor = true;
            this.B_StartGame.Click += new System.EventHandler(this.B_StartGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дойди до правого нижнего края";
            // 
            // button1
            // 
            this.B_Helper.Location = new System.Drawing.Point(535, 403);
            this.B_Helper.Name = "button1";
            this.B_Helper.Size = new System.Drawing.Size(75, 53);
            this.B_Helper.TabIndex = 3;
            this.B_Helper.Text = "Я слабак, пробеги за меня";
            this.B_Helper.UseVisualStyleBackColor = true;
            this.B_Helper.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 509);
            this.Controls.Add(this.B_Helper);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.B_Right);
            this.Controls.Add(this.B_Top);
            this.Controls.Add(this.B_Buttom);
            this.Controls.Add(this.B_Left);
            this.Controls.Add(this.B_StartGame);
            this.Controls.Add(this.PB_Game);
            this.Name = "Form1";
            this.Text = "Лабиринт";
            ((System.ComponentModel.ISupportInitialize)(this.PB_Game)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox PB_Game;
        private System.Windows.Forms.Button B_Left;
        private System.Windows.Forms.Button B_Buttom;
        private System.Windows.Forms.Button B_Right;
        private System.Windows.Forms.Button B_Top;
        private System.Windows.Forms.Button B_StartGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_Helper;
    }
}

