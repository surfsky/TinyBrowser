namespace TinyBrowser
{
    partial class FormWeb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWeb));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbUrl = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.btnFullScreen = new System.Windows.Forms.ToolStripButton();
            this.btnDebug = new System.Windows.Forms.ToolStripButton();
            this.btnLoadHtml = new System.Windows.Forms.ToolStripButton();
            this.btnLoadFile = new System.Windows.Forms.ToolStripButton();
            this.btnLoadServer = new System.Windows.Forms.ToolStripButton();
            this.btnGo = new System.Windows.Forms.ToolStripButton();
            this.webView = new CefSharp.WinForms.ChromiumWebBrowser();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbHtml = new ICSharpCode.TextEditor.TextEditorControl();
            this.toolStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBack,
            this.btnForward,
            this.btnRefresh,
            this.toolStripSeparator1,
            this.tbUrl,
            this.toolStripSeparator2,
            this.btnAbout,
            this.btnFullScreen,
            this.btnDebug,
            this.btnLoadHtml,
            this.btnLoadFile,
            this.btnLoadServer,
            this.btnGo});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1355, 44);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            this.toolStrip.DoubleClick += new System.EventHandler(this.toolStrip_DoubleClick);
            // 
            // btnBack
            // 
            this.btnBack.Image = global::TinyBrowser.Properties.Resources.reverse_green;
            this.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(24, 41);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForward
            // 
            this.btnForward.Image = global::TinyBrowser.Properties.Resources.forward;
            this.btnForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(24, 41);
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::TinyBrowser.Properties.Resources.arrow_refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(24, 41);
            this.btnRefresh.ToolTipText = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 44);
            // 
            // tbUrl
            // 
            this.tbUrl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(676, 44);
            this.tbUrl.Text = "https://lbs.qq.com/webDemoCenter/javascriptV2/mapOperate/createMap";
            this.tbUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbUrl_KeyUp);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 44);
            // 
            // btnAbout
            // 
            this.btnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAbout.Image = global::TinyBrowser.Properties.Resources.information;
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(36, 41);
            this.btnAbout.Text = "关于";
            this.btnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnFullScreen.Image = global::TinyBrowser.Properties.Resources.arrow_nw_ne_sw_se;
            this.btnFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(36, 41);
            this.btnFullScreen.Text = "全屏";
            this.btnFullScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnDebug.Image = global::TinyBrowser.Properties.Resources.bug;
            this.btnDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(36, 41);
            this.btnDebug.Text = "调试";
            this.btnDebug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnLoadHtml
            // 
            this.btnLoadHtml.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnLoadHtml.Image = global::TinyBrowser.Properties.Resources.html;
            this.btnLoadHtml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadHtml.Name = "btnLoadHtml";
            this.btnLoadHtml.Size = new System.Drawing.Size(70, 41);
            this.btnLoadHtml.Text = "加载HTML";
            this.btnLoadHtml.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoadHtml.Click += new System.EventHandler(this.btnLoadHtml_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnLoadFile.Image = global::TinyBrowser.Properties.Resources.folder;
            this.btnLoadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(60, 41);
            this.btnLoadFile.Text = "加载文件";
            this.btnLoadFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnLoadServer
            // 
            this.btnLoadServer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnLoadServer.Image = global::TinyBrowser.Properties.Resources.folder_home;
            this.btnLoadServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadServer.Name = "btnLoadServer";
            this.btnLoadServer.Size = new System.Drawing.Size(60, 41);
            this.btnLoadServer.Text = "加载网站";
            this.btnLoadServer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoadServer.ToolTipText = "加载网站";
            this.btnLoadServer.Click += new System.EventHandler(this.btnLoadServer_Click);
            // 
            // btnGo
            // 
            this.btnGo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnGo.Image = global::TinyBrowser.Properties.Resources.world_go;
            this.btnGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(60, 41);
            this.btnGo.Text = "浏览网页";
            this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // webView
            // 
            this.webView.ActivateBrowserOnCreation = false;
// TODO: “”的代码生成失败，原因是出现异常“无效的基元类型: System.IntPtr。请考虑使用 CodeObjectCreateExpression。”。
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(0, 0);
            this.webView.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(1347, 779);
            this.webView.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 44);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1355, 805);
            this.tabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.webView);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1347, 779);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Web";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbHtml);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(1128, 626);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTML";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbHtml
            // 
            this.tbHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHtml.IsReadOnly = false;
            this.tbHtml.Location = new System.Drawing.Point(3, 3);
            this.tbHtml.Name = "tbHtml";
            this.tbHtml.Size = new System.Drawing.Size(1122, 620);
            this.tbHtml.TabIndex = 0;
            // 
            // FormWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 849);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWeb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TinyBrowser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWeb_FormClosing);
            this.Load += new System.EventHandler(this.FormPosition_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private CefSharp.WinForms.ChromiumWebBrowser webView;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.ToolStripButton btnForward;
        private System.Windows.Forms.ToolStripTextBox tbUrl;
        private System.Windows.Forms.ToolStripButton btnGo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ICSharpCode.TextEditor.TextEditorControl tbHtml;
        private System.Windows.Forms.ToolStripButton btnLoadHtml;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnLoadServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnLoadFile;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripButton btnDebug;
        private System.Windows.Forms.ToolStripButton btnFullScreen;
    }
}