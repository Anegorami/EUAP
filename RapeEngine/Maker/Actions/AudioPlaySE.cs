using System;
using System.Drawing;
using System.Windows.Forms;

using Newtonsoft.Json;

using RapeEngine.Maker;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Play SE action.
	/// </summary>
	public sealed class AudioPlaySE: BaseScriptAction {
		/// <summary>
		/// Node title string format.
		/// </summary>
		const string FORMAT = "Play SE: {0}";
		
		/// <summary>
		/// SE to play.
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
		/// <returns>True, if the initialization went fine, false othewise.</returns>
		public override bool Initialize() {
			AudioSampleForm form = AudioSampleForm.GetSEInstance(key);
			if (form.ShowDialog() == DialogResult.OK) {
				key = form.Value;
				return true;
			}
			return false;
		}
	}
}
