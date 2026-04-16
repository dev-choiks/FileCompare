namespace FileCompare
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            spcMain = new SplitContainer();
            pnlLeftList = new Panel();
            lvwLeftDir = new ListView();
            pnlLeftMenu = new Panel();
            btnLeftDir = new Button();
            txtLeftDir = new TextBox();
            pnlLeftMain = new Panel();
            label1 = new Label();
            btnCopyFromLeft = new Button();
            pnlRightList = new Panel();
            lvwrightDir = new ListView();
            pnlRightMenu = new Panel();
            txtRightDir = new TextBox();
            btnRightDir = new Button();
            pnlRightMain = new Panel();
            btnCopyFromRight = new Button();
            ((System.ComponentModel.ISupportInitialize)spcMain).BeginInit();
            spcMain.Panel1.SuspendLayout();
            spcMain.Panel2.SuspendLayout();
            spcMain.SuspendLayout();
            pnlLeftList.SuspendLayout();
            pnlLeftMenu.SuspendLayout();
            pnlLeftMain.SuspendLayout();
            pnlRightList.SuspendLayout();
            pnlRightMenu.SuspendLayout();
            pnlRightMain.SuspendLayout();
            SuspendLayout();
            // 
            // spcMain
            // 
            spcMain.Dock = DockStyle.Fill;
            spcMain.Location = new Point(10, 10);
            spcMain.Name = "spcMain";
            // 
            // spcMain.Panel1
            // 
            spcMain.Panel1.Controls.Add(pnlLeftList);
            spcMain.Panel1.Controls.Add(pnlLeftMenu);
            spcMain.Panel1.Controls.Add(pnlLeftMain);
            // 
            // spcMain.Panel2
            // 
            spcMain.Panel2.Controls.Add(pnlRightList);
            spcMain.Panel2.Controls.Add(pnlRightMenu);
            spcMain.Panel2.Controls.Add(pnlRightMain);
            spcMain.Size = new Size(1030, 527);
            spcMain.SplitterDistance = 512;
            spcMain.TabIndex = 0;
            // 
            // pnlLeftList
            // 
            pnlLeftList.Controls.Add(lvwLeftDir);
            pnlLeftList.Dock = DockStyle.Fill;
            pnlLeftList.Location = new Point(0, 140);
            pnlLeftList.Name = "pnlLeftList";
            pnlLeftList.Size = new Size(512, 387);
            pnlLeftList.TabIndex = 2;
            // 
            // lvwLeftDir
            // 
            lvwLeftDir.Dock = DockStyle.Fill;
            lvwLeftDir.Location = new Point(0, 0);
            lvwLeftDir.Name = "lvwLeftDir";
            lvwLeftDir.Size = new Size(512, 387);
            lvwLeftDir.TabIndex = 0;
            lvwLeftDir.UseCompatibleStateImageBehavior = false;
            // 
            // pnlLeftMenu
            // 
            pnlLeftMenu.Controls.Add(btnLeftDir);
            pnlLeftMenu.Controls.Add(txtLeftDir);
            pnlLeftMenu.Dock = DockStyle.Top;
            pnlLeftMenu.Location = new Point(0, 97);
            pnlLeftMenu.Name = "pnlLeftMenu";
            pnlLeftMenu.Size = new Size(512, 43);
            pnlLeftMenu.TabIndex = 1;
            // 
            // btnLeftDir
            // 
            btnLeftDir.Dock = DockStyle.Right;
            btnLeftDir.Font = new Font("한컴 말랑말랑 Bold", 10.1999989F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnLeftDir.Location = new Point(402, 0);
            btnLeftDir.Name = "btnLeftDir";
            btnLeftDir.Size = new Size(110, 43);
            btnLeftDir.TabIndex = 1;
            btnLeftDir.Text = "폴더선택";
            btnLeftDir.UseVisualStyleBackColor = true;
            // 
            // txtLeftDir
            // 
            txtLeftDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtLeftDir.Font = new Font("한컴 말랑말랑 Bold", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtLeftDir.Location = new Point(5, 7);
            txtLeftDir.Name = "txtLeftDir";
            txtLeftDir.Size = new Size(390, 33);
            txtLeftDir.TabIndex = 0;
            // 
            // pnlLeftMain
            // 
            pnlLeftMain.Controls.Add(label1);
            pnlLeftMain.Controls.Add(btnCopyFromLeft);
            pnlLeftMain.Dock = DockStyle.Top;
            pnlLeftMain.Location = new Point(0, 0);
            pnlLeftMain.Name = "pnlLeftMain";
            pnlLeftMain.Size = new Size(512, 97);
            pnlLeftMain.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("한컴 말랑말랑 Bold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(220, 43);
            label1.TabIndex = 3;
            label1.Text = "FileCompare";
            // 
            // btnCopyFromLeft
            // 
            btnCopyFromLeft.Dock = DockStyle.Right;
            btnCopyFromLeft.Font = new Font("한컴 말랑말랑 Bold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromLeft.Location = new Point(402, 0);
            btnCopyFromLeft.Name = "btnCopyFromLeft";
            btnCopyFromLeft.Size = new Size(110, 97);
            btnCopyFromLeft.TabIndex = 2;
            btnCopyFromLeft.Text = ">>>";
            btnCopyFromLeft.UseVisualStyleBackColor = true;
            // 
            // pnlRightList
            // 
            pnlRightList.Controls.Add(lvwrightDir);
            pnlRightList.Dock = DockStyle.Fill;
            pnlRightList.Location = new Point(0, 137);
            pnlRightList.Name = "pnlRightList";
            pnlRightList.Size = new Size(514, 390);
            pnlRightList.TabIndex = 3;
            // 
            // lvwrightDir
            // 
            lvwrightDir.Dock = DockStyle.Fill;
            lvwrightDir.Location = new Point(0, 0);
            lvwrightDir.Name = "lvwrightDir";
            lvwrightDir.Size = new Size(514, 390);
            lvwrightDir.TabIndex = 1;
            lvwrightDir.UseCompatibleStateImageBehavior = false;
            // 
            // pnlRightMenu
            // 
            pnlRightMenu.Controls.Add(txtRightDir);
            pnlRightMenu.Controls.Add(btnRightDir);
            pnlRightMenu.Dock = DockStyle.Top;
            pnlRightMenu.Location = new Point(0, 94);
            pnlRightMenu.Name = "pnlRightMenu";
            pnlRightMenu.Size = new Size(514, 43);
            pnlRightMenu.TabIndex = 2;
            // 
            // txtRightDir
            // 
            txtRightDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRightDir.Font = new Font("한컴 말랑말랑 Bold", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtRightDir.Location = new Point(7, 7);
            txtRightDir.Name = "txtRightDir";
            txtRightDir.Size = new Size(390, 33);
            txtRightDir.TabIndex = 2;
            // 
            // btnRightDir
            // 
            btnRightDir.Dock = DockStyle.Right;
            btnRightDir.Font = new Font("한컴 말랑말랑 Bold", 10.1999989F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnRightDir.Location = new Point(404, 0);
            btnRightDir.Name = "btnRightDir";
            btnRightDir.Size = new Size(110, 43);
            btnRightDir.TabIndex = 2;
            btnRightDir.Text = "폴더선택";
            btnRightDir.UseVisualStyleBackColor = true;
            // 
            // pnlRightMain
            // 
            pnlRightMain.Controls.Add(btnCopyFromRight);
            pnlRightMain.Dock = DockStyle.Top;
            pnlRightMain.Location = new Point(0, 0);
            pnlRightMain.Name = "pnlRightMain";
            pnlRightMain.Size = new Size(514, 94);
            pnlRightMain.TabIndex = 1;
            // 
            // btnCopyFromRight
            // 
            btnCopyFromRight.Dock = DockStyle.Left;
            btnCopyFromRight.Font = new Font("한컴 말랑말랑 Bold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromRight.Location = new Point(0, 0);
            btnCopyFromRight.Name = "btnCopyFromRight";
            btnCopyFromRight.Size = new Size(110, 94);
            btnCopyFromRight.TabIndex = 3;
            btnCopyFromRight.Text = "<<<";
            btnCopyFromRight.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 547);
            Controls.Add(spcMain);
            Name = "Form1";
            Padding = new Padding(10);
            Text = "FileCompare v1.0";
            spcMain.Panel1.ResumeLayout(false);
            spcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spcMain).EndInit();
            spcMain.ResumeLayout(false);
            pnlLeftList.ResumeLayout(false);
            pnlLeftMenu.ResumeLayout(false);
            pnlLeftMenu.PerformLayout();
            pnlLeftMain.ResumeLayout(false);
            pnlLeftMain.PerformLayout();
            pnlRightList.ResumeLayout(false);
            pnlRightMenu.ResumeLayout(false);
            pnlRightMenu.PerformLayout();
            pnlRightMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer spcMain;
        private Panel pnlLeftList;
        private Panel pnlLeftMenu;
        private Panel pnlLeftMain;
        private Panel pnlRightList;
        private Panel pnlRightMenu;
        private Panel pnlRightMain;
        private Label label1;
        private TextBox txtLeftDir;
        private Button btnLeftDir;
        private TextBox txtRightDir;
        private Button btnRightDir;
        private ListView lvwLeftDir;
        private Button btnCopyFromLeft;
        private ListView lvwrightDir;
        private Button btnCopyFromRight;
    }
}
