using System;
using System.Drawing;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace RapeEngine.Maker.Conditions {
	/// <summary>
	/// Virtual "Add new condition" condition.
	/// </summary>
	public class VirtualNewCondition: BaseScriptCondition {
		/// <summary>
		/// Pointer to the condition array to add conditions to.
		/// </summary>
		[JsonProperty]
		readonly ElementsKeeper parent;
		
		/// <summary>
		/// Node for a visual representation.
		/// </summary>
		public override TreeNode Node {
			get {
				var node = new TreeNode("> Add a new condition");
				node.ForeColor = Color.Purple;
				return node;
			}
		}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="_parent">Pointer to the condition array to add conditions to.</param>
		public VirtualNewCondition(ElementsKeeper _parent) {
			parent = _parent;
		}
		
		/// <summary>
		/// Edit method.
		/// </summary>
		public override void Edit() {
			bool enough = false;
			while (!enough) {
				var form = new VirtualNewForm(VIRTUAL_NEW_FORM_RETURN.CONDITION);
				if (form.ShowDialog() == DialogResult.OK) {
					var action = (BaseScriptCondition)form.Value;
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
