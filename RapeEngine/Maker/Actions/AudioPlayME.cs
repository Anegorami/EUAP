using System;
using System.Drawing;
using System.Windows.Forms;

using Newtonsoft.Json;

using RapeEngine.Maker;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Play ME action.
	/// </summary>
	public sealed class AudioPlayME: BaseScriptAction {
		/// <summary>
		/// Node title format string.
		/// </summary>
		const string FORMAT = "Play ME: {0}";
		
		/// <summary>
		/// ME to play.
		/// </summary>
		[JsonProperty]
		string key;
		
		/// <summary>
		/// Node for a visual representation.
		/// </summary>
		public override TreeNode Node {
			get {
				var node = new TreeNode(String.Format(FORMAT, key));
				node.ForeColor = Color.DarkCyan;
				return node;
			}
		}
		
		/// <summary>
		/// Initialization method.
		/// </summary>
		/// <returns>True, if the initialization went fine, false otherwise.</returns>
		public override bool Initialize() {
			AudioSampleForm form = AudioSampleForm.GetMEInstance(key);
			if (form.ShowDialog() == DialogResult.OK) {
				key = form.Value;
				return true;
			}
			return false;
		}
	}
}
