using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using Newtonsoft.Json;

using RapeEngine.Maker.Actions;

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
		/// Main actions array.
		/// </summary>
		readonly ElementsKeeper actions = new ElementsKeeper();
		
		/// <summary>
		/// Serialization settings instance.
		/// </summary>
		readonly JsonSerializerSettings settings = new JsonSerializerSettings();
		
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
		/// Method to get a visual representation of the script.
		/// </summary>
		/// <returns>List of nodes.</returns>
		public List<TreeNode> GetNodes() {
			var result = new List<TreeNode>();
			foreach (BaseScriptAction action in actions) {
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
			var file = File.OpenWrite(filename);
			var stream = new StreamWriter(file);
			stream.Write(JsonConvert.SerializeObject(actions, Formatting.None, settings));
			stream.Close();
			file.Close();
		}
	}
}
