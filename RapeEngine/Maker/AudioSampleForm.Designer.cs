﻿// Autogenerated code. Don't touch.
namespace RapeEngine.Maker
{
	partial class AudioSampleForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button button_play_bgm;
		private System.Windows.Forms.Button button_stop_bgm;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.ListBox list_bgm;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tab_bgm;
		private System.Windows.Forms.TabPage tab_bgs;
		private System.Windows.Forms.Button button_stop_bgs;
		private System.Windows.Forms.Button button_play_bgs;
		private System.Windows.Forms.ListBox list_bgs;
		private System.Windows.Forms.TabPage tab_me;
		private System.Windows.Forms.Button button_stop_me;
		private System.Windows.Forms.Button button_play_me;
		private System.Windows.Forms.ListBox list_me;
		private System.Windows.Forms.TabPage tab_vo;
		private System.Windows.Forms.Button button_stop_vo;
		private System.Windows.Forms.Button button_play_vo;
		private System.Windows.Forms.ListBox list_vo;
		private System.Windows.Forms.TabPage tab_se;
		private System.Windows.Forms.Button button_stop_se;
		private System.Windows.Forms.Button button_play_se;
		private System.Windows.Forms.ListBox list_se;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.list_bgm = new System.Windows.Forms.ListBox();
			this.button_play_bgm = new System.Windows.Forms.Button();
			this.button_stop_bgm = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.button_ok = new System.Windows.Forms.Button();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tab_bgm = new System.Windows.Forms.TabPage();
			this.tab_bgs = new System.Windows.Forms.TabPage();
			this.button_stop_bgs = new System.Windows.Forms.Button();
			this.button_play_bgs = new System.Windows.Forms.Button();
			this.list_bgs = new System.Windows.Forms.ListBox();
			this.tab_me = new System.Windows.Forms.TabPage();
			this.button_stop_me = new System.Windows.Forms.Button();
			this.button_play_me = new System.Windows.Forms.Button();
			this.list_me = new System.Windows.Forms.ListBox();
			this.tab_vo = new System.Windows.Forms.TabPage();
			this.button_stop_vo = new System.Windows.Forms.Button();
			this.button_play_vo = new System.Windows.Forms.Button();
			this.list_vo = new System.Windows.Forms.ListBox();
			this.tab_se = new System.Windows.Forms.TabPage();
			this.button_stop_se = new System.Windows.Forms.Button();
			this.button_play_se = new System.Windows.Forms.Button();
			this.list_se = new System.Windows.Forms.ListBox();
			this.tabs.SuspendLayout();
			this.tab_bgm.SuspendLayout();
			this.tab_bgs.SuspendLayout();
			this.tab_me.SuspendLayout();
			this.tab_vo.SuspendLayout();
			this.tab_se.SuspendLayout();
			this.SuspendLayout();
			// 
			// list_bgm
			// 
			this.list_bgm.FormattingEnabled = true;
			this.list_bgm.Location = new System.Drawing.Point(6, 6);
			this.list_bgm.Name = "list_bgm";
			this.list_bgm.Size = new System.Drawing.Size(120, 238);
			this.list_bgm.TabIndex = 0;
			// 
			// button_play_bgm
			// 
			this.button_play_bgm.Location = new System.Drawing.Point(132, 6);
			this.button_play_bgm.Name = "button_play_bgm";
			this.button_play_bgm.Size = new System.Drawing.Size(75, 23);
			this.button_play_bgm.TabIndex = 1;
			this.button_play_bgm.Text = "Play";
			this.button_play_bgm.UseVisualStyleBackColor = true;
			this.button_play_bgm.Click += new System.EventHandler(this.Button_play_bgmClick);
			// 
			// button_stop_bgm
			// 
			this.button_stop_bgm.Location = new System.Drawing.Point(132, 35);
			this.button_stop_bgm.Name = "button_stop_bgm";
			this.button_stop_bgm.Size = new System.Drawing.Size(75, 23);
			this.button_stop_bgm.TabIndex = 2;
			this.button_stop_bgm.Text = "Stop";
			this.button_stop_bgm.UseVisualStyleBackColor = true;
			this.button_stop_bgm.Click += new System.EventHandler(this.Button_stop_bgmClick);
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(159, 298);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 3;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			// 
			// button_ok
			// 
			this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button_ok.Location = new System.Drawing.Point(78, 298);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(75, 23);
			this.button_ok.TabIndex = 4;
			this.button_ok.Text = "OK";
			this.button_ok.UseVisualStyleBackColor = true;
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tab_bgm);
			this.tabs.Controls.Add(this.tab_bgs);
			this.tabs.Controls.Add(this.tab_me);
			this.tabs.Controls.Add(this.tab_vo);
			this.tabs.Controls.Add(this.tab_se);
			this.tabs.Location = new System.Drawing.Point(12, 12);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(222, 280);
			this.tabs.TabIndex = 5;
			// 
			// tab_bgm
			// 
			this.tab_bgm.Controls.Add(this.list_bgm);
			this.tab_bgm.Controls.Add(this.button_play_bgm);
			this.tab_bgm.Controls.Add(this.button_stop_bgm);
			this.tab_bgm.Location = new System.Drawing.Point(4, 22);
			this.tab_bgm.Name = "tab_bgm";
			this.tab_bgm.Padding = new System.Windows.Forms.Padding(3);
			this.tab_bgm.Size = new System.Drawing.Size(214, 254);
			this.tab_bgm.TabIndex = 0;
			this.tab_bgm.Text = "BGM";
			this.tab_bgm.UseVisualStyleBackColor = true;
			// 
			// tab_bgs
			// 
			this.tab_bgs.Controls.Add(this.button_stop_bgs);
			this.tab_bgs.Controls.Add(this.button_play_bgs);
			this.tab_bgs.Controls.Add(this.list_bgs);
			this.tab_bgs.Location = new System.Drawing.Point(4, 22);
			this.tab_bgs.Name = "tab_bgs";
			this.tab_bgs.Padding = new System.Windows.Forms.Padding(3);
			this.tab_bgs.Size = new System.Drawing.Size(214, 254);
			this.tab_bgs.TabIndex = 1;
			this.tab_bgs.Text = "BGS";
			this.tab_bgs.UseVisualStyleBackColor = true;
			// 
			// button_stop_bgs
			// 
			this.button_stop_bgs.Location = new System.Drawing.Point(132, 35);
			this.button_stop_bgs.Name = "button_stop_bgs";
			this.button_stop_bgs.Size = new System.Drawing.Size(75, 23);
			this.button_stop_bgs.TabIndex = 6;
			this.button_stop_bgs.Text = "Stop";
			this.button_stop_bgs.UseVisualStyleBackColor = true;
			this.button_stop_bgs.Click += new System.EventHandler(this.Button_stop_bgsClick);
			// 
			// button_play_bgs
			// 
			this.button_play_bgs.Location = new System.Drawing.Point(132, 6);
			this.button_play_bgs.Name = "button_play_bgs";
			this.button_play_bgs.Size = new System.Drawing.Size(75, 23);
			this.button_play_bgs.TabIndex = 6;
			this.button_play_bgs.Text = "Play";
			this.button_play_bgs.UseVisualStyleBackColor = true;
			this.button_play_bgs.Click += new System.EventHandler(this.Button_play_bgsClick);
			// 
			// list_bgs
			// 
			this.list_bgs.FormattingEnabled = true;
			this.list_bgs.Location = new System.Drawing.Point(6, 6);
			this.list_bgs.Name = "list_bgs";
			this.list_bgs.Size = new System.Drawing.Size(120, 238);
			this.list_bgs.TabIndex = 6;
			// 
			// tab_me
			// 
			this.tab_me.Controls.Add(this.button_stop_me);
			this.tab_me.Controls.Add(this.button_play_me);
			this.tab_me.Controls.Add(this.list_me);
			this.tab_me.Location = new System.Drawing.Point(4, 22);
			this.tab_me.Name = "tab_me";
			this.tab_me.Padding = new System.Windows.Forms.Padding(3);
			this.tab_me.Size = new System.Drawing.Size(214, 254);
			this.tab_me.TabIndex = 2;
			this.tab_me.Text = "ME";
			this.tab_me.UseVisualStyleBackColor = true;
			// 
			// button_stop_me
			// 
			this.button_stop_me.Location = new System.Drawing.Point(132, 35);
			this.button_stop_me.Name = "button_stop_me";
			this.button_stop_me.Size = new System.Drawing.Size(75, 23);
			this.button_stop_me.TabIndex = 6;
			this.button_stop_me.Text = "Stop";
			this.button_stop_me.UseVisualStyleBackColor = true;
			this.button_stop_me.Click += new System.EventHandler(this.Button_stop_meClick);
			// 
			// button_play_me
			// 
			this.button_play_me.Location = new System.Drawing.Point(132, 6);
			this.button_play_me.Name = "button_play_me";
			this.button_play_me.Size = new System.Drawing.Size(75, 23);
			this.button_play_me.TabIndex = 6;
			this.button_play_me.Text = "Play";
			this.button_play_me.UseVisualStyleBackColor = true;
			this.button_play_me.Click += new System.EventHandler(this.Button_play_meClick);
			// 
			// list_me
			// 
			this.list_me.FormattingEnabled = true;
			this.list_me.Location = new System.Drawing.Point(6, 6);
			this.list_me.Name = "list_me";
			this.list_me.Size = new System.Drawing.Size(120, 238);
			this.list_me.TabIndex = 6;
			// 
			// tab_vo
			// 
			this.tab_vo.Controls.Add(this.button_stop_vo);
			this.tab_vo.Controls.Add(this.button_play_vo);
			this.tab_vo.Controls.Add(this.list_vo);
			this.tab_vo.Location = new System.Drawing.Point(4, 22);
			this.tab_vo.Name = "tab_vo";
			this.tab_vo.Padding = new System.Windows.Forms.Padding(3);
			this.tab_vo.Size = new System.Drawing.Size(214, 254);
			this.tab_vo.TabIndex = 3;
			this.tab_vo.Text = "VO";
			this.tab_vo.UseVisualStyleBackColor = true;
			// 
			// button_stop_vo
			// 
			this.button_stop_vo.Location = new System.Drawing.Point(132, 35);
			this.button_stop_vo.Name = "button_stop_vo";
			this.button_stop_vo.Size = new System.Drawing.Size(75, 23);
			this.button_stop_vo.TabIndex = 6;
			this.button_stop_vo.Text = "Stop";
			this.button_stop_vo.UseVisualStyleBackColor = true;
			this.button_stop_vo.Click += new System.EventHandler(this.Button_stop_voClick);
			// 
			// button_play_vo
			// 
			this.button_play_vo.Location = new System.Drawing.Point(132, 6);
			this.button_play_vo.Name = "button_play_vo";
			this.button_play_vo.Size = new System.Drawing.Size(75, 23);
			this.button_play_vo.TabIndex = 6;
			this.button_play_vo.Text = "Play";
			this.button_play_vo.UseVisualStyleBackColor = true;
			this.button_play_vo.Click += new System.EventHandler(this.Button_play_voClick);
			// 
			// list_vo
			// 
			this.list_vo.FormattingEnabled = true;
			this.list_vo.Location = new System.Drawing.Point(6, 6);
			this.list_vo.Name = "list_vo";
			this.list_vo.Size = new System.Drawing.Size(120, 238);
			this.list_vo.TabIndex = 6;
			// 
			// tab_se
			// 
			this.tab_se.Controls.Add(this.button_stop_se);
			this.tab_se.Controls.Add(this.button_play_se);
			this.tab_se.Controls.Add(this.list_se);
			this.tab_se.Location = new System.Drawing.Point(4, 22);
			this.tab_se.Name = "tab_se";
			this.tab_se.Padding = new System.Windows.Forms.Padding(3);
			this.tab_se.Size = new System.Drawing.Size(214, 254);
			this.tab_se.TabIndex = 4;
			this.tab_se.Text = "SE";
			this.tab_se.UseVisualStyleBackColor = true;
			// 
			// button_stop_se
			// 
			this.button_stop_se.Location = new System.Drawing.Point(132, 35);
			this.button_stop_se.Name = "button_stop_se";
			this.button_stop_se.Size = new System.Drawing.Size(75, 23);
			this.button_stop_se.TabIndex = 6;
			this.button_stop_se.Text = "Stop";
			this.button_stop_se.UseVisualStyleBackColor = true;
			this.button_stop_se.Click += new System.EventHandler(this.Button_stop_seClick);
			// 
			// button_play_se
			// 
			this.button_play_se.Location = new System.Drawing.Point(132, 6);
			this.button_play_se.Name = "button_play_se";
			this.button_play_se.Size = new System.Drawing.Size(75, 23);
			this.button_play_se.TabIndex = 6;
			this.button_play_se.Text = "Play";
			this.button_play_se.UseVisualStyleBackColor = true;
			this.button_play_se.Click += new System.EventHandler(this.Button_play_seClick);
			// 
			// list_se
			// 
			this.list_se.FormattingEnabled = true;
			this.list_se.Location = new System.Drawing.Point(6, 6);
			this.list_se.Name = "list_se";
			this.list_se.Size = new System.Drawing.Size(120, 238);
			this.list_se.TabIndex = 6;
			// 
			// AudioSampleForm
			// 
			this.AcceptButton = this.button_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(243, 332);
			this.ControlBox = false;
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.button_cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "AudioSampleForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Sample Select";
			this.tabs.ResumeLayout(false);
			this.tab_bgm.ResumeLayout(false);
			this.tab_bgs.ResumeLayout(false);
			this.tab_me.ResumeLayout(false);
			this.tab_vo.ResumeLayout(false);
			this.tab_se.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
