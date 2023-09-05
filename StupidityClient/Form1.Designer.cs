namespace StupidityClient
{
    partial class Bot
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
            this.components = new System.ComponentModel.Container();
            this.tickTimer = new System.Windows.Forms.Timer(this.components);
            this.alertPicture = new System.Windows.Forms.PictureBox();
            this.alertLabel = new System.Windows.Forms.Label();
            this.alertDuration = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.alertPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // tickTimer
            // 
            this.tickTimer.Enabled = true;
            this.tickTimer.Interval = 250;
            this.tickTimer.Tick += new System.EventHandler(this.tickTimer_Tick);
            // 
            // alertPicture
            // 
            this.alertPicture.Location = new System.Drawing.Point(100, 10);
            this.alertPicture.Name = "alertPicture";
            this.alertPicture.Size = new System.Drawing.Size(600, 600);
            this.alertPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.alertPicture.TabIndex = 0;
            this.alertPicture.TabStop = false;
            // 
            // alertLabel
            // 
            this.alertLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.84906F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alertLabel.Location = new System.Drawing.Point(2, 652);
            this.alertLabel.Name = "alertLabel";
            this.alertLabel.Size = new System.Drawing.Size(780, 80);
            this.alertLabel.TabIndex = 1;
            this.alertLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // alertDuration
            // 
            this.alertDuration.Interval = 5000;
            this.alertDuration.Tick += new System.EventHandler(this.alertDuration_Tick);
            // 
            // Bot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(784, 759);
            this.Controls.Add(this.alertLabel);
            this.Controls.Add(this.alertPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Bot";
            this.Text = "Alerts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.alertPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tickTimer;
        private System.Windows.Forms.Label alertLabel;
        private System.Windows.Forms.PictureBox alertPicture;
        private System.Windows.Forms.Timer alertDuration;
    }
}

