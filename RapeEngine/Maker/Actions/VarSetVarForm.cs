﻿using System;
using System.Windows.Forms;

using RapeEngine.Components;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Form for setting up a variable.
	/// </summary>
	public partial class VarSetVarForm: Form {
		/// <summary>
		/// Error message when the entered value cannot be converted to int;
		/// </summary>
		const string ERROR_NOT_A_NUMBER = "Entered value isn't a number!";
		
		/// <summary>
		/// Error message when the user tries to setup a division by zero.
		/// </summary>
		const string ERROR_DIVIDE_BY_ZERO = "You cannot divide by zero, you idiot.";
		
		/// <summary>
		/// Variable index.
		/// </summary>
		public int Index {get; private set;}
		
		/// <summary>
		/// Choosen operation.
		/// </summary>
		public int Operation {
			get {
				return operation_box.SelectedIndex;
			}
		}
		
		/// <summary>
		/// Either a constant value or a variable index.
		/// </summary>
		public int Value {get; private set;}
		
		/// <summary>
		/// If this flag is true, Value contain a variable index.
		/// </summary>
		public bool IsVariable {
			get {
				return radio_var.Checked;
			}
		}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="index">Variable index.</param>
		/// <param name="operation">Operation index.</param>
		/// <param name="value">Either a constant value or a variable index.</param>
		/// <param name="is_variable">If this flag is true, Value contain a variable index.</param>
		public VarSetVarForm(int index = 0, int operation = 0, int value = 0, bool is_variable = false) {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			var1.Text = UserVariables.GetVariableName(index);
			operation_box.SelectedIndex = operation;
			value_box.Text = (is_variable)? "0": Convert.ToString(value);
			var2.Text = UserVariables.GetVariableName((is_variable)? value: 0);
			radio_value.Checked = !is_variable;
			radio_var.Checked = is_variable;
			CheckerChanged();
			
			Index = index;
			Value = value;
		}
		
		/// <summary>
		/// Small method to apply radio button changes.
		/// </summary>
		void CheckerChanged() {
			value_box.Enabled = radio_value.Checked;
			var2.Enabled = !radio_value.Checked;
		}
		
		/// <summary>
		/// Response for variable link clicking.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Var1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var form = new VariablesForm(VARIABLES_FORM_RETURN.VARIABLE, Index);
			if (form.ShowDialog() == DialogResult.OK) {
				Index = form.Value;
				var1.Text = UserVariables.GetVariableName(Index);
			}
		}
		
		/// <summary>
		/// Response for radio button state changing.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Radio_valueCheckedChanged(object sender, EventArgs e) {
			CheckerChanged();
		}
		
		/// <summary>
		/// Response for variable link clicking.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Var2LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var form = new VariablesForm(VARIABLES_FORM_RETURN.VARIABLE, Index);
			if (form.ShowDialog() == DialogResult.OK) {
				Value = form.Value;
				var2.Text = UserVariables.GetVariableName(Value);
			}
		}
		
		/// <summary>
		/// Response for OK button clicking.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_okClick(object sender, EventArgs e) {
			if (!IsVariable) {
				try {
					Value = Convert.ToInt32(value_box.Text);
				} catch (FormatException) {
					MessageBox.Show(ERROR_NOT_A_NUMBER, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				
				// Check for an attempt to divide by zero.
				if (((operation_box.SelectedIndex == 4) || (operation_box.SelectedIndex == 5)) && (Value == 0)) {
					MessageBox.Show(ERROR_DIVIDE_BY_ZERO, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
