namespace Snake_v2
{
    partial class SnakeForm
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
            this.Painel = new System.Windows.Forms.Panel();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ConectarBtn = new System.Windows.Forms.Button();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PlacarPlayer1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.PlacarPlayer2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Painel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Painel
            // 
            this.Painel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel.Controls.Add(this.textBoxPort);
            this.Painel.Controls.Add(this.textBoxServer);
            this.Painel.Controls.Add(this.label2);
            this.Painel.Controls.Add(this.label1);
            this.Painel.Controls.Add(this.ConectarBtn);
            this.Painel.Location = new System.Drawing.Point(32, 12);
            this.Painel.Name = "Painel";
            this.Painel.Size = new System.Drawing.Size(410, 79);
            this.Painel.TabIndex = 4;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(77, 48);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(63, 20);
            this.textBoxPort.TabIndex = 4;
            this.textBoxPort.Text = "9999";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(78, 13);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(136, 20);
            this.textBoxServer.TabIndex = 3;
            this.textBoxServer.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Porta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Servidor";
            // 
            // ConectarBtn
            // 
            this.ConectarBtn.Location = new System.Drawing.Point(303, 42);
            this.ConectarBtn.Name = "ConectarBtn";
            this.ConectarBtn.Size = new System.Drawing.Size(92, 27);
            this.ConectarBtn.TabIndex = 0;
            this.ConectarBtn.Text = "Conectar";
            this.ConectarBtn.UseVisualStyleBackColor = true;
            this.ConectarBtn.Click += new System.EventHandler(this.ConectarBtn_Click);
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 50;
            this.UpdateTimer.Tick += new System.EventHandler(this.Update_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.PlacarPlayer1,
            this.toolStripStatusLabel1,
            this.PlacarPlayer2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 504);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(504, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(54, 17);
            this.toolStripStatusLabel.Text = "Player 1: ";
            // 
            // PlacarPlayer1
            // 
            this.PlacarPlayer1.Name = "PlacarPlayer1";
            this.PlacarPlayer1.Size = new System.Drawing.Size(13, 17);
            this.PlacarPlayer1.Text = "0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(330, 0, 0, 0);
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(384, 17);
            this.toolStripStatusLabel1.Text = "Player 2: ";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PlacarPlayer2
            // 
            this.PlacarPlayer2.Name = "PlacarPlayer2";
            this.PlacarPlayer2.Size = new System.Drawing.Size(13, 17);
            this.PlacarPlayer2.Text = "0";
            // 
            // SnakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(504, 526);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Painel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(510, 550);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(510, 550);
            this.Name = "SnakeForm";
            this.Text = "Snake - Multiplayer";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SnakeForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SnakeForm_KeyDown);
            this.Painel.ResumeLayout(false);
            this.Painel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Painel;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ConectarBtn;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel PlacarPlayer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel PlacarPlayer2;
    }
}

