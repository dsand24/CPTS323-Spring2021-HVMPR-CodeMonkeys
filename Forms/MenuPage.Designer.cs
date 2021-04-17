
namespace CodeMonkeys___HVMPR_Project.Forms
{
    partial class MenuPage
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
            this.addDriverPress = new System.Windows.Forms.Button();
            this.logOut = new System.Windows.Forms.Button();
            this.addVanPress = new System.Windows.Forms.Button();
            this.mapPress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addDriverPress
            // 
            this.addDriverPress.Location = new System.Drawing.Point(477, 97);
            this.addDriverPress.Name = "addDriverPress";
            this.addDriverPress.Size = new System.Drawing.Size(159, 83);
            this.addDriverPress.TabIndex = 0;
            this.addDriverPress.Text = "Add Driver";
            this.addDriverPress.UseVisualStyleBackColor = true;
            this.addDriverPress.Click += new System.EventHandler(this.addDriverPress_Click);
            // 
            // logOut
            // 
            this.logOut.Location = new System.Drawing.Point(477, 261);
            this.logOut.Name = "logOut";
            this.logOut.Size = new System.Drawing.Size(171, 91);
            this.logOut.TabIndex = 1;
            this.logOut.Text = "Log Out";
            this.logOut.UseVisualStyleBackColor = true;
            this.logOut.Click += new System.EventHandler(this.logOut_Click);
            // 
            // addVanPress
            // 
            this.addVanPress.Location = new System.Drawing.Point(88, 269);
            this.addVanPress.Name = "addVanPress";
            this.addVanPress.Size = new System.Drawing.Size(149, 75);
            this.addVanPress.TabIndex = 2;
            this.addVanPress.Text = "Add Van";
            this.addVanPress.UseVisualStyleBackColor = true;
            this.addVanPress.Click += new System.EventHandler(this.addVanPress_Click);
            // 
            // mapPress
            // 
            this.mapPress.Location = new System.Drawing.Point(88, 97);
            this.mapPress.Name = "mapPress";
            this.mapPress.Size = new System.Drawing.Size(149, 81);
            this.mapPress.TabIndex = 3;
            this.mapPress.Text = "Map";
            this.mapPress.UseVisualStyleBackColor = true;
            this.mapPress.Click += new System.EventHandler(this.button4_Click);
            // 
            // MenuPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mapPress);
            this.Controls.Add(this.addVanPress);
            this.Controls.Add(this.logOut);
            this.Controls.Add(this.addDriverPress);
            this.Name = "MenuPage";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addDriverPress;
        private System.Windows.Forms.Button logOut;
        private System.Windows.Forms.Button addVanPress;
        private System.Windows.Forms.Button mapPress;
    }
}