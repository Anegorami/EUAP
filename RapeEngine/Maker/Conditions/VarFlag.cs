using System;
using System.Drawing;
using System.Windows.Forms;

using Newtonsoft.Json;

using RapeEngine.Components;

namespace RapeEngine.Maker.Conditions {
	/// <summary>
	/// Variable-driven condition.
	/// </summary>
	public class VarFlag: BaseScriptCondition {
		/// <summary>
		/// Node title format.
		/// </summary>
		const string FORMAT = "If {0} is {1}";
		
		/// <summary>
		/// Flag index.
		/// </summary>
		[JsonProperty]
		int index;
		
		/// <summary>
		/// Flag value.
		/// </summary>
		[JsonProperty]
		bool value = true;
		
		/// <summary>
		/// Node for a visual representation.
		/// </summary>
		public override TreeNode Node {
			get {
				var node = new TreeNode(String.Format(FORMAT, UserVariables.GetFlagName(index), value));
				node.ForeColor = Color.Red;
				return node;
			}
		}
		
		/// <summary>
		/// Initialization method.
		/// </summary>
		/// <returns>True if the initialization went fine, false otherwise.</returns>
		public override bool Initialize() {
			var form = new VarFlagForm(index, value, "If", "is set to");
			if (form.ShowDialog() == DialogResult.OK) {
				index = form.Index;
				value = form.Value;
				return true;
			}
			return false;
		}
	}
}
