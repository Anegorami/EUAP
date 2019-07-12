using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// Base class for all script actions.
	/// </summary>
	public abstract class BaseScriptAction: IScriptElement {
		/// <summary>
		/// Node for visual representation.
		/// </summary>
		[JsonIgnore]
		public abstract TreeNode Node {get;}
		
		/// <summary>
		/// Method to get a sub element.
		/// </summary>
		/// <param name="path">Set of indexes for identification.</param>
		/// <returns>Sub element.</returns>
		public virtual IScriptElement GetSubElement(List<int> path) {
			return null;
		}
		
		/// <summary>
		/// Method that is called when the user adds the action.
		/// </summary>
		/// <returns>True if the action should be added, false otherwise.</returns>
		public virtual bool Initialize() {
			return true;
		}
		
		/// <summary>
		/// Method for adding a sub element.
		/// </summary>
		/// <param name="target">Sub element to add.</param>
		/// <param name="path">Set of indexes for destination identification.</param>
		/// <returns>True, if the addition was successful, false otherwise.</returns>
		public virtual bool AddSubElement(IScriptElement target, List<int> path) {
			return false;
		}
		
		/// <summary>
		/// Method that is called when the user edits the action.
		/// </summary>
		public virtual void Edit() {
			Initialize();
		}
		
		/// <summary>
		/// Method that is called when the user edits a subaction.
		/// </summary>
		/// <param name="path">Set of indexes for subaction identification.</param>
		public virtual void Edit(List<int> path) {}
		
		/// <summary>
		/// Method that is called when the user deletes a subaction.
		/// </summary>
		/// <param name="path">Set of indexes for subaction identification.</param>
		public virtual void Remove(List<int> path) {}
	}
}
