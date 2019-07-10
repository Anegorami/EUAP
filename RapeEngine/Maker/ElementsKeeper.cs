using System;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

using RapeEngine.Maker.Actions;
using RapeEngine.Maker.Conditions;

namespace RapeEngine.Maker {
	/// <summary>
	/// Storage class script elements.
	/// </summary>
	[JsonObject(IsReference = true)]
	public sealed class ElementsKeeper {
		/// <summary>
		/// Data list.
		/// </summary>
		[JsonProperty]
		readonly List<IScriptElement> data = new List<IScriptElement>();
		
		/// <summary>
		/// Direct data indexer.
		/// </summary>
		public IScriptElement this[int index] {
			get {
				return data[index];
			}
		}
		
		/// <summary>
		/// Addition method.
		/// </summary>
		/// <param name="element">Element to add.</param>
		public void Add(IScriptElement element) {
			data.Add(element);
		}
		
		/// <summary>
		/// Edit method.
		/// </summary>
		/// <param name="path">Set of indexes for element identification.</param>
		public void Edit(List<int> path) {
			int root = path[0];
			path.RemoveAt(0);
			
			if (path.Count == 0) {
				data[root].Edit();
			} else {
				data[root].Edit(path);
			}
		}
		
		/// <summary>
		/// Removal method.
		/// </summary>
		/// <param name="element">Element to remove.</param>
		public void Remove(IScriptElement element) {
			data.Remove(element);
		}
		
		/// <summary>
		/// Removal method.
		/// </summary>
		/// <param name="path">Set of indexes for element identification.</param>
		public void Remove(List<int> path) {
			int root = path[0];
			path.RemoveAt(0);
			
			if (path.Count == 0) {
				RemoveAt(root);
			} else {
				data[root].Remove(path);
			}
		}
		
		/// <summary>
		/// Removal method.
		/// </summary>
		/// <param name="index">Index of the element to remove.</param>
		public void RemoveAt(int index) {
			Type type = data[index].GetType();
			if ((type != typeof(VirtualNewAction)) || (type != typeof(VirtualNewCondition))) {
				data.RemoveAt(index);
			}
		}
		
		/// <summary>
		/// GetEnumerator proxy method.
		/// </summary>
		/// <returns>Enumerator.</returns>
		public IEnumerator GetEnumerator() {
			return data.GetEnumerator();
		}
	}
}
