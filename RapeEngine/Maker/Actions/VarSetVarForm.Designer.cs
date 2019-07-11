﻿// Autogenerated code. Don't touch.
namespace RapeEngine.Maker.Actions
{
	partial class VarSetVarForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel var1;
		private System.Windows.Forms.ComboBox operation_box;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radio_value;
		private System.Windows.Forms.RadioButton radio_var;
		private System.Windows.Forms.LinkLabel var2;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.TextBox value_box;
		
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
			this.label1 = new System.Windows.Forms.Label();
			this.var1 = new System.Windows.Forms.LinkLabel();
			this.operation_box = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.radio_value = new System.Windows.Forms.RadioButton();
			this.radio_var = new System.Windows.Forms.RadioButton();
			this.value_box = new System.Windows.Forms.TextBox();
			this.var2 = new System.Windows.Forms.LinkLabel();
			this.button_cancel = new System.Windows.Forms.Button();
			this.button_ok = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Set variable";
			// 
			// var1
			// 
			this.var1.Location = new System.Drawing.Point(96, 9);
			this.var1.Name = "var1";
			this.var1.Size = new System.Drawing.Size(176, 23);
			this.var1.TabIndex = 1;
			this.var1.TabStop = true;
			this.var1.Text = "linkLabel1";
			this.var1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Var1LinkClicked);
			// 
			// operation_box
			// 
			this.operation_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.operation_box.FormattingEnabled = true;
			this.operation_box.Items.AddRange(new object[] {
			"Set value (=)",
			"Add to value (+)",
			"Substract from value (-)",
			"Multiply value (*)",
			"Divide by value (/)",
			"Get a remainder of division (%)"});
			this.operation_box.Location = new System.Drawing.Point(96, 35);
			this.operation_box.Name = "operation_box";
			this.operation_box.Size = new System.Drawing.Size(176, 21);
			this.operation_box.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "with operation";
			// 
			// radio_value
			// 
			this.radio_value.Location = new System.Drawing.Point(12, 65);
			this.radio_value.Name = "radio_value";
			this.radio_value.Size = new System.Drawing.Size(104, 24);
			this.radio_value.TabIndex = 4;
			this.radio_value.TabStop = true;
			this.radio_value.Text = "Value";
			this.radio_value.UseVisualStyleBackColor = true;
			this.radio_value.CheckedChanged += new System.EventHandler(this.Radio_valueCheckedChanged);
			// 
			// radio_var
			// 
			this.radio_var.Location = new System.Drawing.Point(12, 95);
			this.radio_var.Name = "radio_var";
			this.radio_var.Size = new System.Drawing.Size(104, 24);
			this.radio_var.TabIndex = 5;
			this.radio_var.TabStop = true;
			this.radio_var.Text = "Variable";
			this.radio_var.UseVisualStyleBackColor = true;
			// 
			// value_box
			// 
			this.value_box.Location = new System.Drawing.Point(96, 68);
			this.value_box.Name = "value_box";
			this.value_box.Size = new System.Drawing.Size(176, 20);
			this.value_box.TabIndex = 6;
			// 
			// var2
			// 
			this.var2.Enabled = false;
			this.var2.Location = new System.Drawing.Point(96, 101);
			this.var2.Name = "var2";
			this.var2.Size = new System.Drawing.Size(176, 23);
			this.var2.TabIndex = 7;
			this.var2.TabStop = true;
			this.var2.Text = "linkLabel1";
			this.var2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Var2LinkClicked);
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(197, 127);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 8;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(116, 127);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(75, 23);
			this.button_ok.TabIndex = 9;
			this.button_ok.Text = "OK";
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.Button_okClick);
			// 
			// VarSetVarForm
			// 
			this.AcceptButton = this.button_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(284, 161);
			this.ControlBox = false;
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.var2);
			this.Controls.Add(this.value_box);
			this.Controls.Add(this.radio_var);
			this.Controls.Add(this.radio_value);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.operation_box);
			this.Controls.Add(this.var1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "VarSetVarForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Variable Setup";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
