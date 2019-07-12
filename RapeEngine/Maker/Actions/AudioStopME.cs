using System;

using System.Drawing;
using System.Windows.Forms;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Stop ME action.
	/// </summary>
	public class AudioStopME: BaseScriptAction {
		public override TreeNode Node {
			get {
				var node = new TreeNode("Stop ME");
				node.ForeColor = Color.DarkCyan;
				return node;
			}
		}
	}
}
