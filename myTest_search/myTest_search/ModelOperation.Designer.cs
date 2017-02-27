namespace myTest_search
{
    partial class ModelOperation
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
            this.hWindowControl2 = new HalconDotNet.HWindowControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.deleteLittleCameraModel = new System.Windows.Forms.Button();
            this.CreatCameraModelbutton00 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hWindowControl2
            // 
            this.hWindowControl2.BackColor = System.Drawing.Color.Black;
            this.hWindowControl2.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl2.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl2.Location = new System.Drawing.Point(26, 29);
            this.hWindowControl2.Name = "hWindowControl2";
            this.hWindowControl2.Size = new System.Drawing.Size(731, 519);
            this.hWindowControl2.TabIndex = 0;
            this.hWindowControl2.WindowSize = new System.Drawing.Size(731, 519);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(798, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "创建主相机模板";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.CreatModel);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(944, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 50);
            this.button2.TabIndex = 2;
            this.button2.Text = "删除模板";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.DeleateModel);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(912, 446);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 50);
            this.button3.TabIndex = 3;
            this.button3.Text = "操作完成";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // deleteLittleCameraModel
            // 
            this.deleteLittleCameraModel.Location = new System.Drawing.Point(944, 185);
            this.deleteLittleCameraModel.Name = "deleteLittleCameraModel";
            this.deleteLittleCameraModel.Size = new System.Drawing.Size(109, 50);
            this.deleteLittleCameraModel.TabIndex = 5;
            this.deleteLittleCameraModel.Text = "删除小相机模板";
            this.deleteLittleCameraModel.UseVisualStyleBackColor = true;
            this.deleteLittleCameraModel.Click += new System.EventHandler(this.button4_Click);
            // 
            // CreatCameraModelbutton00
            // 
            this.CreatCameraModelbutton00.Location = new System.Drawing.Point(798, 185);
            this.CreatCameraModelbutton00.Name = "CreatCameraModelbutton00";
            this.CreatCameraModelbutton00.Size = new System.Drawing.Size(101, 50);
            this.CreatCameraModelbutton00.TabIndex = 4;
            this.CreatCameraModelbutton00.Text = "创建小相机模板";
            this.CreatCameraModelbutton00.UseVisualStyleBackColor = true;
            this.CreatCameraModelbutton00.Click += new System.EventHandler(this.CreatCameraModelbutton00_Click);
            // 
            // ModelOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 577);
            this.Controls.Add(this.deleteLittleCameraModel);
            this.Controls.Add(this.CreatCameraModelbutton00);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hWindowControl2);
            this.Name = "ModelOperation";
            this.Text = "模板操作";
            this.Load += new System.EventHandler(this.ModelOperation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button deleteLittleCameraModel;
        private System.Windows.Forms.Button CreatCameraModelbutton00;
    }
}