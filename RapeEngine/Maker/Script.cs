using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using Newtonsoft.Json;

using RapeEngine.Maker.Actions;
using RapeEngine.Maker.Conditions;

namespace RapeEngine.Maker {
	/// <summary>
	/// User script class.
	/// </summary>
	public sealed class Script {
		/// <summary>
		/// Script file name.
		/// </summary>
		readonly string filename;
		
		/// <summary>
		/// Serialization settings instance.
		/// </summary>
		readonly JsonSerializerSettings settings = new JsonSerializerSettings();
		
		/// <summary>
		/// Main actions array.
		/// </summary>
		readonly ElementsKeeper actions = new ElementsKeeper();
		
		/// <summary>
		/// Clipboard element.
		/// </summary>
		IScriptElement clipboard_item;
		
		/// <summary>
		/// Path of the clipboard element.
		/// Used for restoration in case of an error.
		/// </summary>
		List<int> clipboard_path;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="_filename">Script file name.</param>
		public Script(string _filename) {
			filename = _filename;
			settings.TypeNameHandling = TypeNameHandling.Auto;
			
			if (new FileInfo(filename).Length != 0) {
				var file = File.OpenText(filename);
				actions = JsonConvert.DeserializeObject<ElementsKeeper>(file.ReadToEnd(), settings);
				file.Close();
			} else {
				actions.Add(new VirtualNewAction(actions));
			}
		}
		
		/// <summary>
		/// Method to restore the clipboard item.
		/// </summary>
		void RestoreClipboard() {
			actions.MoveTo(clipboard_item, clipboard_path);
		}
		
		/// <summary>
		/// Method to get a visual representation of the script.
		/// </summary>
		/// <returns>List of nodes.</returns>
		public List<TreeNode> GetNodes() {
			var result = new List<TreeNode>();
			foreach (IScriptElement action in actions) {
				result.Add(action.Node);
			}
			return result;
		}
		
		/// <summary>
		/// Method for action editing.
		/// </summary>
		/// <param name="path">Set of indexes for action identification.</param>
		public void Edit(List<int> path) {
			int root = path[0];
			var target = (BaseScriptAction)actions[root];
			path.RemoveAt(0);
			
			if (path.Count == 0) {
				target.Edit();
			} else {
				target.Edit(path);
			}
		}
		
		/// <summary>
		/// Method for action removal.
		/// </summary>
		/// <param name="path">Set of indexes for action identification.</param>
		public void Remove(List<int> path) {
			actions.Remove(path);
		}
		
		/// <summary>
		/// Method for saving the script.
		/// </summary>
		public void Save() {
			File.Delete(filename);
			var file = File.OpenWrite(filename);
			var stream = new StreamWriter(file);
			stream.Write(JsonConvert.SerializeObject(actions, Formatting.None, settings));
			stream.Close();
			file.Close();
		}
		
		/// <summary>
		/// Method to move the item into the clipboard.
		/// </summary>
		/// <param name="path"></param>
		public void MoveToClipboard(List<int> path) {
			clipboard_item = actions.GetElement(path);
			
			actions.Remove(path);
			
			clipboard_path = path;
			clipboard_path[clipboard_path.Count - 1] -= 1;
		}
		
		/// <summary>
		/// Method to move the item from the clipboard.
		/// </summary>
		/// <param name="destination"></param>
		public void MoveFromClipboard(List<int> destination) {
			if (clipboard_item != null) {
				Type type = clipboard_item.GetType();
				bool accepted_type = (type != typeof(VirtualNewAction)) && (type != typeof(VirtualNewCondition));
				if (accepted_type) {
					if (!actions.MoveTo(clipboard_item, destination)) {
						RestoreClipboard();
					}
				}
			}
		}
	}
}
