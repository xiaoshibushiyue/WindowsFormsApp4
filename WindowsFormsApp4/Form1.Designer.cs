namespace WindowsFormsApp4
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.预览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印机打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.预览ToolStripMenuItem,
            this.打印机打印ToolStripMenuItem,
            this.打印ToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 预览ToolStripMenuItem
            // 
            this.预览ToolStripMenuItem.Name = "预览ToolStripMenuItem";
            this.预览ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.预览ToolStripMenuItem.Text = "预览";
            this.预览ToolStripMenuItem.Click += new System.EventHandler(this.打印ToolStripMenuItem_Click);
            // 
            // 打印机打印ToolStripMenuItem
            // 
            this.打印机打印ToolStripMenuItem.Name = "打印机打印ToolStripMenuItem";
            this.打印机打印ToolStripMenuItem.Size = new System.Drawing.Size(109, 24);
            this.打印机打印ToolStripMenuItem.Text = "PDF文档输出";
            this.打印机打印ToolStripMenuItem.Click += new System.EventHandler(this.打印机打印ToolStripMenuItem_Click);
            // 
            // 打印ToolStripMenuItem1
            // 
            this.打印ToolStripMenuItem1.Name = "打印ToolStripMenuItem1";
            this.打印ToolStripMenuItem1.Size = new System.Drawing.Size(51, 24);
            this.打印ToolStripMenuItem1.Text = "打印";
            this.打印ToolStripMenuItem1.Click += new System.EventHandler(this.打印ToolStripMenuItem1_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 10);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1200, 753);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

}

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 预览ToolStripMenuItem;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStripMenuItem 打印机打印ToolStripMenuItem;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripMenuItem 打印ToolStripMenuItem1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

