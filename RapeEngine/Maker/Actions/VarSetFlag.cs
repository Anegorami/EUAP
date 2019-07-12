using System;
using System.Drawing;
using System.Windows.Forms;

using Newtonsoft.Json;

using RapeEngine.Components;
using RapeEngine.Maker;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Action that can set a value of a flag.
	/// </summary>
	public sealed class VarSetFlag: BaseScriptAction {
		/// <summary>
		/// Node description format.
		/// </summary>
		const string FORMAT = "Set {0} to {1}";
		
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
		/// <returns>True if the initial setup succedded, false otherwise.</returns>
		public override bool Initialize()
		{
			var form = new VarFlagForm(index, value, "Set flag", "to");
			if (form.ShowDialog() == DialogResult.OK) {
				index = form.Index;
				value = form.Value;
				return true;
			}
			return false;
		}
	}
}
