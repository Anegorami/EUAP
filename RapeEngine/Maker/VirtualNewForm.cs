using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RapeEngine.Maker {
	/// <summary>
	/// Form for adding conditions.
	/// </summary>
	public partial class VirtualNewForm: Form {
		/// <summary>
		/// Return value.
		/// </summary>
		public object Value{get; private set;}
		
		/// <summary>
		/// Root for the shown elements.
		/// </summary>
		List<ElementManager.Group> target;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="mode">Form mode.</param>
		public VirtualNewForm(VIRTUAL_NEW_FORM_RETURN mode) {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			switch (mode) {
				case (VIRTUAL_NEW_FORM_RETURN.ACTION):
					target = ElementManager.RootActions;
					break;
				case (VIRTUAL_NEW_FORM_RETURN.CONDITION):
					target = ElementManager.RootConditions;
					Text = "Condition Select";
					button_ok.Text = "Add condition";
					break;
			}
			
			foreach (ElementManager.Group grp in target) {
				var node = new TreeNode(grp.Name);
				foreach (ElementManager.Item item in grp.Items) {
					node.Nodes.Add(item.Name);
				}
				tree.Nodes.Add(node);
			}
			
			tree.SelectedNode = tree.Nodes[0];
		}
		
		/// <summary>
		/// Response for OK button click.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_okClick(object sender, EventArgs e) {
			int item = tree.SelectedNode.Index;
			int grp = tree.SelectedNode.Parent.Index;
			Value = Activator.CreateInstance(target[grp].Items[item].Value);
		}
		
		/// <summary>
		/// Response to node selection.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Selected node.</param>
		void TreeAfterSelect(object sender, TreeViewEventArgs e) {
			if (e.Node.Parent == null) {
				label_name.Text = target[e.Node.Index].Name;
				label_desc.Text = target[e.Node.Index].Description;
				button_ok.Enabled = false;
			} else {
				int item = e.Node.Index;
				int grp = e.Node.Parent.Index;
				label_name.Text = target[grp].Items[item].Name;
				label_desc.Text = target[grp].Items[item].Description;
				button_ok.Enabled = true;
			}
		}
	}
	
	/// <summary>
	/// Enumerator to determine form mode.
	/// </summary>
	public enum VIRTUAL_NEW_FORM_RETURN {ACTION, CONDITION}
}
