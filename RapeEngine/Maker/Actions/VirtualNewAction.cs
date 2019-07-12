using System;
using System.Drawing;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Virtual "Add new action" action.
	/// </summary>
	public class VirtualNewAction: BaseScriptAction {
		/// <summary>
		/// Reference to a parent Actions array.
		/// </summary>
		[JsonProperty]
		readonly ElementsKeeper parent;
		
		/// <summary>
		/// Node for a visual representation.
		/// </summary>
		public override TreeNode Node {
			get {
				var node = new TreeNode("> Add a new action");
				node.ForeColor = Color.Blue;
				return node;
			}
		}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="_parent">A pointer to a Action array to add an action to.</param>
		public VirtualNewAction(ElementsKeeper _parent) {
			parent = _parent;
		}
		
		/// <summary>
		/// Edit method.
		/// </summary>
		public override void Edit() {
			// While the new item cannot be initialized - let the user pick another one or cancel the addition.
			bool enough = false;
			while (!enough) {
				VirtualNewForm form = VirtualNewForm.GetActionsInstance();
				if (form.ShowDialog() == DialogResult.OK) {
					var action = (BaseScriptAction)form.Value;
					if (action.Initialize()) {
						parent.Remove(this);
						parent.Add(action);
						parent.Add(this);
						enough = true;
					}
				} else {
					enough = true;
				}
			}
		}
	}
}
