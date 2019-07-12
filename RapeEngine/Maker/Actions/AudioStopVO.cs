using System;

using System.Drawing;
using System.Windows.Forms;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Stop VO action.
	/// </summary>
	public class AudioStopVO: BaseScriptAction {
		public override TreeNode Node {
			get {
				var node = new TreeNode("Stop VO");
				node.ForeColor = Color.DarkCyan;
				return node;
			}
		}
	}
}
