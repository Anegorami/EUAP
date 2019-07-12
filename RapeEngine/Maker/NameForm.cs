using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RapeEngine.Maker {
	/// <summary>
	/// Form that is used to provide a name for the script.
	/// </summary>
	public partial class NameForm: Form {
		/// <summary>
		/// A field for quick value obtaining.
		/// </summary>
		public string Value {get; private set;}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="value">Initial value.</param>
		public NameForm(string value = "") {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			input.Text = value;
		}
		
		/// <summary>
		/// OK button click response.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_okClick(object sender, EventArgs e) {
			if (Regex.IsMatch(input.Text, @"[\w]+")) {
				DialogResult = DialogResult.OK;
				Value = input.Text;
				Close();
			} else {
				MessageBox.Show("Invalid script name!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
