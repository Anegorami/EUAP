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
		/// GetEnumerator proxy method.
		/// </summary>
		/// <returns>Enumerator.</returns>
		public IEnumerator GetEnumerator() {
			return data.GetEnumerator();
		}
		
		/// <summary>
		/// Method for element retrieval.
		/// </summary>
		/// <param name="path">Set of indexes for element identification.</param>
		/// <returns>Element.</returns>
		public IScriptElement GetElement(List<int> path) {
			var path_copy = new List<int>(path);
			int root = path_copy[0];
			path_copy.RemoveAt(0);
			
			return (path_copy.Count == 0)? data[root]: data[root].GetSubElement(path_copy);
		}
		
		/// <summary>
		/// Addition method.
		/// </summary>
		/// <param name="element">Element to add.</param>
		public void Add(IScriptElement element) {
			data.Add(element);
		}
		
		/// <summary>
		/// Method for element moving.
		/// </summary>
		/// <param name="target">Element to move.</param>
		/// <param name="path">Set of indexes for destination identification.</param>
		/// <returns>True if the movement was successful, false otherwise.</returns>
		public bool MoveTo(IScriptElement target, List<int> path) {
			int root;
			
			// Check for adding to the root category, append the element in this case.
			if (path.Count == 0) {
				root = data.Count - 1;
			} else {
				// Special root edit for the attempt to put the element into zeroth place (after element -1, which
				// is non-existent).
				root = Math.Max(path[0], 0);
				path.RemoveAt(0);
			}
			
			if (path.Count == 0) {
				Type type = data[root].GetType();
				
				// Special check for an attempt to add an action to a condition array and vise-versa.
				if (!target.GetType().IsSubclassOf(type.BaseType)) {
					return false;
				}
				
				// Another special check for an attempt to put the element after virtual elements.
				if ((type == typeof(VirtualNewAction)) || (type == typeof(VirtualNewCondition))) {
					IScriptElement item = data[root];
					data.Remove(item);
					data.Add(target);
					data.Add(item);
				} else {
					data.Insert(root + 1, target);
				}
				return true;
			}
			
			return data[root].AddSubElement(target, path);
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
			var path_copy = new List<int>(path);
			int root = path_copy[0];
			path_copy.RemoveAt(0);
			
			if (path_copy.Count == 0) {
				RemoveAt(root);
			} else {
				data[root].Remove(path_copy);
			}
		}
		
		/// <summary>
		/// Removal method.
		/// </summary>
		/// <param name="index">Index of the element to remove.</param>
		public void RemoveAt(int index) {
			Type type = data[index].GetType();
			if ((type != typeof(VirtualNewAction)) && (type != typeof(VirtualNewCondition))) {
				data.RemoveAt(index);
			}
		}
	}
}
