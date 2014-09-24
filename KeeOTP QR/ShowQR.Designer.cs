namespace KeeOTPQR
{
    partial class ShowQR
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.banner = new System.Windows.Forms.PictureBox();
            this.errorMsg = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.keyTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.keyIssuer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.banner)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 260);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // banner
            // 
            this.banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.banner.Location = new System.Drawing.Point(0, 0);
            this.banner.Name = "banner";
            this.banner.Size = new System.Drawing.Size(434, 58);
            this.banner.TabIndex = 1;
            this.banner.TabStop = false;
            // 
            // errorMsg
            // 
            this.errorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorMsg.Location = new System.Drawing.Point(0, 61);
            this.errorMsg.Name = "errorMsg";
            this.errorMsg.Size = new System.Drawing.Size(260, 260);
            this.errorMsg.TabIndex = 2;
            this.errorMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.keyTitle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.keyIssuer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(266, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 104);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "QR Options";
            // 
            // keyTitle
            // 
            this.keyTitle.Location = new System.Drawing.Point(9, 76);
            this.keyTitle.Name = "keyTitle";
            this.keyTitle.Size = new System.Drawing.Size(145, 20);
            this.keyTitle.TabIndex = 3;
            this.keyTitle.TextChanged += new System.EventHandler(this.keyTitle_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Key Title";
            // 
            // keyIssuer
            // 
            this.keyIssuer.Location = new System.Drawing.Point(9, 33);
            this.keyIssuer.Name = "keyIssuer";
            this.keyIssuer.Size = new System.Drawing.Size(145, 20);
            this.keyIssuer.TabIndex = 1;
            this.keyIssuer.TextChanged += new System.EventHandler(this.keyIssuer_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Key Issuer";
            // 
            // ShowQR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 321);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.banner);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.errorMsg);
            this.MaximumSize = new System.Drawing.Size(450, 360);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 360);
            this.Name = "ShowQR";
            this.Text = "QR";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.banner)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox banner;
        private System.Windows.Forms.Label errorMsg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox keyTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox keyIssuer;
        private System.Windows.Forms.Label label2;
    }
}