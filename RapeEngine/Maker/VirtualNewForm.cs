using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RapeEngine.Maker {
	/// <summary>
	/// Form for adding conditions.
	/// </summary>
	public partial class VirtualNewForm: Form {
		/// <summary>
		/// Enumerator for form modes.
		/// </summary>
		public enum MODE {ACTIONS, CONDITIONS}
		
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
		VirtualNewForm(MODE mode) {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			switch (mode) {
				case (MODE.ACTIONS):
					target = ElementManager.RootActions;
					break;
				case (MODE.CONDITIONS):
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
		/// Method for retrieval a form instance in action selection mode.
		/// </summary>
		/// <returns>Form instance.</returns>
		public static VirtualNewForm GetActionsInstance() {
			return new VirtualNewForm(MODE.ACTIONS);
		}
		
		/// <summary>
		/// Method for retrieval a form instance in condition selection mode.
		/// </summary>
		/// <returns>Form instance.</returns>
		public static VirtualNewForm GetConditionsInstance() {
			return new VirtualNewForm(MODE.CONDITIONS);
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
}
