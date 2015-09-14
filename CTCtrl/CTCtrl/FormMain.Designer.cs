namespace CTCtrl
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPortSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(737, 26);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuMain";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(85, 22);
            this.menuFile.Text = global::XBee.Properties.Resources.MENU_FILE;
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuExit.Size = new System.Drawing.Size(166, 22);
            this.menuExit.Text = "終了(&X)";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStart,
            this.menuEnd,
            this.toolStripMenuItem1,
            this.menuPortSetting});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(61, 22);
            this.menuEdit.Text = "編集(&E)";
            // 
            // menuStart
            // 
            this.menuStart.Name = "menuStart";
            this.menuStart.Size = new System.Drawing.Size(165, 22);
            this.menuStart.Text = "開始(&S)";
            this.menuStart.Click += new System.EventHandler(this.menuStart_Click);
            // 
            // menuEnd
            // 
            this.menuEnd.Enabled = false;
            this.menuEnd.Name = "menuEnd";
            this.menuEnd.Size = new System.Drawing.Size(165, 22);
            this.menuEnd.Text = "終了(&E)";
            this.menuEnd.Click += new System.EventHandler(this.menuEnd_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 6);
            // 
            // menuPortSetting
            // 
            this.menuPortSetting.Name = "menuPortSetting";
            this.menuPortSetting.Size = new System.Drawing.Size(165, 22);
            this.menuPortSetting.Text = "ポート設定(&P)...";
            this.menuPortSetting.Click += new System.EventHandler(this.menuPortSetting_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(75, 22);
            this.menuHelp.Text = "ヘルプ(&H)";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(178, 22);
            this.menuAbout.Text = "バージョン情報(&A)";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 26);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(737, 407);
            this.splitContainer1.SplitterDistance = 202;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(202, 407);
            this.treeView1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 433);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.MinimumSize = new System.Drawing.Size(249, 89);
            this.Name = "FormMain";
            this.Text = "CTCtrl";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuStart;
        private System.Windows.Forms.ToolStripMenuItem menuEnd;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuPortSetting;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
    }
}

