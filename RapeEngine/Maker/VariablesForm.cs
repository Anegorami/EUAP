using System;
using System.Collections.Generic;
using System.Windows.Forms;

using RapeEngine.Components;

namespace RapeEngine.Maker {
	/// <summary>
	/// Form to keep track of variables.
	/// </summary>
	public partial class VariablesForm: Form {
		/// <summary>
		/// Enumerator for form return value.
		/// </summary>
		enum MODE {ALL, FLAGS, VARIABLES}
		
		/// <summary>
		/// Amount of items per group.
		/// </summary>
		const int ITEMS_PER_GROUP = 20;
		
		/// <summary>
		/// Format of the group list.
		/// </summary>
		const string GROUP_FORMAT = "{0:D4} - {1:D4}";
		
		/// <summary>
		/// Format of the items list.
		/// </summary>
		const string ITEM_FORMAT = "{0:D4}: {1}";
		
		/// <summary>
		/// Active flag index.
		/// </summary>
		int flag_name_index = -1;
		
		/// <summary>
		/// Active variable index.
		/// </summary>
		int var_name_index = -1;
		
		/// <summary>
		/// Form mode.
		/// </summary>
		readonly MODE mode;
		
		/// <summary>
		/// Return value.
		/// </summary>
		public int Value {get; private set;}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="_mode">Form mode.</param>
		/// <param name="index">Index of the selected item.</param>
		VariablesForm(MODE _mode = MODE.ALL, int index = 0) {
			// Required.
			InitializeComponent();
			
			// Fill the lists.
			FillGroup(flags_list_group, UserVariables.FLAGS_MAX);
			FillGroup(vars_list_group, UserVariables.VARIABLES_MAX);
			
			// Set the mode.
			mode = _mode;
			switch (mode) {
				case (MODE.ALL):
					button_ok.Enabled = false;
					button_ok.Text = "";
					button_close.Text = "Close";
					break;
				case (MODE.FLAGS):
					tabs.TabPages.Remove(tab_vars);
					flags_list_group.SelectedIndex = index / ITEMS_PER_GROUP;
					flags_list_item.SelectedIndex = index % ITEMS_PER_GROUP;
					break;
				case (MODE.VARIABLES):
					tabs.TabPages.Remove(tab_flags);
					vars_list_group.SelectedIndex = index / ITEMS_PER_GROUP;
					vars_list_item.SelectedIndex = index % ITEMS_PER_GROUP;
					break;
			}
		}
		
		/// <summary>
		/// Method for retrieving a basic instance of the form.
		/// </summary>
		/// <returns>Form instance.</returns>
		public static VariablesForm GetInstance() {
			return new VariablesForm();
		}
		
		/// <summary>
		/// Method for retrieving form in a flag selection mode.
		/// </summary>
		/// <param name="index">Initialy selected element.</param>
		/// <returns>Form instance.</returns>
		public static VariablesForm GetFlagInstance(int index) {
			return new VariablesForm(MODE.FLAGS, index);
		}
		
		/// <summary>
		/// Method for retrieving form in a variable selection mode.
		/// </summary>
		/// <param name="index">Initialy selected element.</param>
		/// <returns>Form instance.</returns>
		public static VariablesForm GetVariableInstance(int index) {
			return new VariablesForm(MODE.VARIABLES, index);
		}
		
		/// <summary>
		/// Method to fill group lists.
		/// </summary>
		/// <param name="target">Target list.</param>
		/// <param name="index_max">Amount of elements.</param>
		void FillGroup(ListBox target, int index_max) {
			for (int index = 0; index < index_max; index += ITEMS_PER_GROUP) {
				target.Items.Add(String.Format(GROUP_FORMAT, index + 1, index + ITEMS_PER_GROUP));
			}
			target.SelectedIndex = 0;
		}
		
		/// <summary>
		/// Method to fill items list.
		/// </summary>
		/// <param name="target">Target list.</param>
		/// <param name="starting_index">Starting index.</param>
		/// <param name="names">List to read items' names from.</param>
		void FillItems(ListBox target, int starting_index, IReadOnlyList<string> names) {
			int selection = Math.Max(target.SelectedIndex, 0);
			target.Items.Clear();
			for (int index = starting_index; index < starting_index + ITEMS_PER_GROUP; index++) {
				target.Items.Add(String.Format(ITEM_FORMAT, index + 1, names[index]));
			}
			target.SelectedIndex = selection;
		}
		
		/// <summary>
		/// Response for group selection.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Flags_list_groupSelectedIndexChanged(object sender, EventArgs e) {
			int index = flags_list_group.SelectedIndex * ITEMS_PER_GROUP;
			FillItems(flags_list_item, index, UserVariables.FlagNames);
		}
		
		/// <summary>
		/// Response for group selection.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Vars_list_groupSelectedIndexChanged(object sender, EventArgs e) {
			int index = vars_list_group.SelectedIndex * ITEMS_PER_GROUP;
			FillItems(vars_list_item, index, UserVariables.VariableNames);
		}
		
		/// <summary>
		/// Response for item selection.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Flags_list_itemSelectedIndexChanged(object sender, EventArgs e) {
			flag_name_index = flags_list_item.SelectedIndex + flags_list_group.SelectedIndex * ITEMS_PER_GROUP;
			flag_name.Text = UserVariables.FlagNames[flag_name_index];
		}
		
		/// <summary>
		/// Response for item selection.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Vars_list_itemSelectedIndexChanged(object sender, EventArgs e) {
			var_name_index = vars_list_item.SelectedIndex + vars_list_group.SelectedIndex * ITEMS_PER_GROUP;
			var_name.Text = UserVariables.VariableNames[var_name_index];
		}
		
		/// <summary>
		/// Response for renaming the item.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Flag_nameTextChanged(object sender, EventArgs e) {
			UserVariables.FlagNames[flag_name_index] = flag_name.Text;
			
			int index = flags_list_group.SelectedIndex * ITEMS_PER_GROUP;
			FillItems(flags_list_item, index, UserVariables.FlagNames);
		}
		
		/// <summary>
		/// Response for renaming the item.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Var_nameTextChanged(object sender, EventArgs e) {
			UserVariables.VariableNames[var_name_index] = var_name.Text;
			
			int index = vars_list_group.SelectedIndex * ITEMS_PER_GROUP;
			FillItems(vars_list_item, index, UserVariables.VariableNames);
		}
		
		/// <summary>
		/// Response for form closing.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void VariablesFormFormClosed(object sender, FormClosedEventArgs e) {
			switch (mode) {
				case (MODE.FLAGS):
					Value = flag_name_index;
					break;
				case (MODE.VARIABLES):
					Value = var_name_index;
					break;
			}
		}
	}
}
