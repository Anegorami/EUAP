using System;

using System.Drawing;
using System.Windows.Forms;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Stop BGM action.
	/// </summary>
	public class AudioStopBGM: BaseScriptAction {
		public override TreeNode Node {
			get {
				var node = new TreeNode("Stop BGM");
				node.ForeColor = Color.DarkCyan;
				return node;
			}
		}
	}
}
