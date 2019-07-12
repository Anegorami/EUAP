using System;
using System.Windows.Forms;

using RapeEngine.Components;

namespace RapeEngine.Maker {
	/// <summary>
	/// Form to set a flag value.
	/// </summary>
	public partial class VarFlagForm: Form {
		/// <summary>
		/// Index property.
		/// </summary>
		public int Index {get; private set;}
		
		/// <summary>
		/// Value property.
		/// </summary>
		public bool Value {get; private set;}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="index">Flag index.</param>
		/// <param name="value">Flag value.</param>
		/// <param name="text1">First string of text.</param>
		/// <param name="text2">Second string of text.</param>
		public VarFlagForm(int index, bool value, string text1, string text2) {
			// Required.
			InitializeComponent();
			
			//Constructor code goes here...
			Index = index;
			Value = value;
			if (Value) {
				switch_true.Checked = true;
			} else {
				switch_false.Checked = true;
			}
			label1.Text = text1;
			label2.Text = text2;
			UpdateMe();
		}
		
		/// <summary>
		/// Update method.
		/// </summary>
		void UpdateMe() {
			flag.Text = UserVariables.GetFlagName(Index);
		}
		
		/// <summary>
		/// Action for clicking a flag link.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void FlagLinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			VariablesForm form = VariablesForm.GetFlagInstance(Index);
			if (form.ShowDialog() == DialogResult.OK) {
				Index = form.Value;
				UpdateMe();
			}
		}
		
		/// <summary>
		/// Action for form closing.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void VarFlagFormFormClosed(object sender, FormClosedEventArgs e) {
			Value = switch_true.Checked;
		}
	}
}
