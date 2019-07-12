using System;

using System.Drawing;
using System.Windows.Forms;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Stop SE action.
	/// </summary>
	public class AudioStopSE: BaseScriptAction {
		public override TreeNode Node {
			get {
				var node = new TreeNode("Stop SE");
				node.ForeColor = Color.DarkCyan;
				return node;
			}
		}
	}
}
