namespace Home_Alarm_GUI
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAlarm = new System.Windows.Forms.Label();
            this.lblBuzzer = new System.Windows.Forms.Label();
            this.lblLighting = new System.Windows.Forms.Label();
            this.lblAlarmMode = new System.Windows.Forms.Label();
            this.btnOpenDoor = new System.Windows.Forms.Button();
            this.btnMovement = new System.Windows.Forms.Button();
            this.btnAlarmSwitch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alarm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Buzzer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Lighting";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "AlarmMode";
            // 
            // lblAlarm
            // 
            this.lblAlarm.AutoSize = true;
            this.lblAlarm.Location = new System.Drawing.Point(83, 80);
            this.lblAlarm.Name = "lblAlarm";
            this.lblAlarm.Size = new System.Drawing.Size(35, 13);
            this.lblAlarm.TabIndex = 4;
            this.lblAlarm.Text = "label5";
            // 
            // lblBuzzer
            // 
            this.lblBuzzer.AutoSize = true;
            this.lblBuzzer.Location = new System.Drawing.Point(83, 20);
            this.lblBuzzer.Name = "lblBuzzer";
            this.lblBuzzer.Size = new System.Drawing.Size(35, 13);
            this.lblBuzzer.TabIndex = 5;
            this.lblBuzzer.Text = "label6";
            // 
            // lblLighting
            // 
            this.lblLighting.AutoSize = true;
            this.lblLighting.Location = new System.Drawing.Point(83, 49);
            this.lblLighting.Name = "lblLighting";
            this.lblLighting.Size = new System.Drawing.Size(35, 13);
            this.lblLighting.TabIndex = 6;
            this.lblLighting.Text = "label7";
            // 
            // lblAlarmMode
            // 
            this.lblAlarmMode.AutoSize = true;
            this.lblAlarmMode.Location = new System.Drawing.Point(167, 233);
            this.lblAlarmMode.Name = "lblAlarmMode";
            this.lblAlarmMode.Size = new System.Drawing.Size(35, 13);
            this.lblAlarmMode.TabIndex = 7;
            this.lblAlarmMode.Text = "label8";
            // 
            // btnOpenDoor
            // 
            this.btnOpenDoor.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOpenDoor.Location = new System.Drawing.Point(15, 111);
            this.btnOpenDoor.Name = "btnOpenDoor";
            this.btnOpenDoor.Size = new System.Drawing.Size(75, 23);
            this.btnOpenDoor.TabIndex = 8;
            this.btnOpenDoor.Text = "Open Door";
            this.btnOpenDoor.UseVisualStyleBackColor = true;
            this.btnOpenDoor.Click += new System.EventHandler(this.btnOpenDoor_Click);
            // 
            // btnMovement
            // 
            this.btnMovement.Location = new System.Drawing.Point(15, 140);
            this.btnMovement.Name = "btnMovement";
            this.btnMovement.Size = new System.Drawing.Size(75, 23);
            this.btnMovement.TabIndex = 9;
            this.btnMovement.Text = "Movement";
            this.btnMovement.UseVisualStyleBackColor = true;
            this.btnMovement.Click += new System.EventHandler(this.btnMovement_Click);
            // 
            // btnAlarmSwitch
            // 
            this.btnAlarmSwitch.Location = new System.Drawing.Point(86, 194);
            this.btnAlarmSwitch.Name = "btnAlarmSwitch";
            this.btnAlarmSwitch.Size = new System.Drawing.Size(75, 52);
            this.btnAlarmSwitch.TabIndex = 10;
            this.btnAlarmSwitch.Text = "Toggle Alarm";
            this.btnAlarmSwitch.UseVisualStyleBackColor = true;
            this.btnAlarmSwitch.Click += new System.EventHandler(this.btnAlarmSwitch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 258);
            this.Controls.Add(this.btnAlarmSwitch);
            this.Controls.Add(this.btnMovement);
            this.Controls.Add(this.btnOpenDoor);
            this.Controls.Add(this.lblAlarmMode);
            this.Controls.Add(this.lblLighting);
            this.Controls.Add(this.lblBuzzer);
            this.Controls.Add(this.lblAlarm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAlarm;
        private System.Windows.Forms.Label lblBuzzer;
        private System.Windows.Forms.Label lblLighting;
        private System.Windows.Forms.Label lblAlarmMode;
        private System.Windows.Forms.Button btnOpenDoor;
        private System.Windows.Forms.Button btnMovement;
        private System.Windows.Forms.Button btnAlarmSwitch;
    }
}

