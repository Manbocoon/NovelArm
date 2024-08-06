
namespace NovelArm
{
    partial class CharDisplayConfig
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
            this.setTextColor = new System.Windows.Forms.Button();
            this.setOutlineColor = new System.Windows.Forms.Button();
            this.setFont = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // setTextColor
            // 
            this.setTextColor.BackColor = System.Drawing.Color.White;
            this.setTextColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setTextColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.setTextColor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.setTextColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.setTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setTextColor.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.setTextColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.setTextColor.Location = new System.Drawing.Point(124, 0);
            this.setTextColor.Name = "setTextColor";
            this.setTextColor.Size = new System.Drawing.Size(125, 23);
            this.setTextColor.TabIndex = 16;
            this.setTextColor.Text = "글자 색상 설정";
            this.setTextColor.UseVisualStyleBackColor = false;
            this.setTextColor.Click += new System.EventHandler(this.setTextColor_Click);
            // 
            // setOutlineColor
            // 
            this.setOutlineColor.BackColor = System.Drawing.Color.White;
            this.setOutlineColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setOutlineColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.setOutlineColor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.setOutlineColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.setOutlineColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setOutlineColor.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.setOutlineColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.setOutlineColor.Location = new System.Drawing.Point(124, 22);
            this.setOutlineColor.Name = "setOutlineColor";
            this.setOutlineColor.Size = new System.Drawing.Size(125, 23);
            this.setOutlineColor.TabIndex = 17;
            this.setOutlineColor.Text = "외곽선 설정";
            this.setOutlineColor.UseVisualStyleBackColor = false;
            this.setOutlineColor.Click += new System.EventHandler(this.setOutlineColor_Click);
            // 
            // setFont
            // 
            this.setFont.BackColor = System.Drawing.Color.White;
            this.setFont.Cursor = System.Windows.Forms.Cursors.Hand;
            this.setFont.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.setFont.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.setFont.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.setFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setFont.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.setFont.Image = global::NovelArm.Properties.Resources.Text;
            this.setFont.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.setFont.Location = new System.Drawing.Point(0, 0);
            this.setFont.Name = "setFont";
            this.setFont.Size = new System.Drawing.Size(125, 45);
            this.setFont.TabIndex = 15;
            this.setFont.Text = "글꼴 설정";
            this.setFont.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.setFont.UseVisualStyleBackColor = false;
            this.setFont.Click += new System.EventHandler(this.setFont_Click);
            // 
            // CharDisplayConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(249, 45);
            this.Controls.Add(this.setOutlineColor);
            this.Controls.Add(this.setTextColor);
            this.Controls.Add(this.setFont);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CharDisplayConfig";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CharDisplayConfig";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.CharDisplayConfig_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button setFont;
        private System.Windows.Forms.Button setTextColor;
        private System.Windows.Forms.Button setOutlineColor;
    }
}