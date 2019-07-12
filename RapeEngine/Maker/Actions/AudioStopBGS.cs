using System;

using System.Drawing;
using System.Windows.Forms;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Stop BGS action.
	/// </summary>
	public class AudioStopBGS: BaseScriptAction {
		public override TreeNode Node {
			get {
				var node = new TreeNode("Stop BGS");
				node.ForeColor = Color.DarkCyan;
				return node;
			}
		}
	}
}
