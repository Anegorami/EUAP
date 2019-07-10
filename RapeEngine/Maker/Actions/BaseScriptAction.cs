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
		/// Method that is called when the user adds the action.
		/// </summary>
		/// <returns>True if the action should be added, false otherwise.</returns>
		public virtual bool Initialize() {
			return true;
		}
		
		/// <summary>
		/// Method that is called when the user edits the action.
		/// </summary>
		public virtual void Edit() {}
		
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
