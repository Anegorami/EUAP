using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace RapeEngine.Maker.Conditions {
	/// <summary>
	/// Interface for all script conditions.
	/// </summary>
	public abstract class BaseScriptCondition: IScriptElement {
		/// <summary>
		/// Node for a visual representation.
		/// </summary>
		[JsonIgnore]
		public abstract TreeNode Node {get;}
		
		/// <summary>
		/// Method for sub element retrieval.
		/// </summary>
		/// <param name="path">Set of indexes for identification.</param>
		/// <returns>Sub element.</returns>
		public virtual IScriptElement GetSubElement(List<int> path) {
			return null;
		}
		
		/// <summary>
		/// Initialization method.
		/// </summary>
		/// <returns>True, if the initialization went fine, false otherwise.</returns>
		public virtual bool Initialize() {
			return true;
		}
		
		/// <summary>
		/// Method for sub element addition.
		/// </summary>
		/// <param name="target">Element to add.</param>
		/// <param name="path">Set of indexes for destination identification.</param>
		/// <returns>True, if the addition was successful, false otherwise.</returns>
		public virtual bool AddSubElement(IScriptElement target, List<int> path) {
			return false;
		}
		
		/// <summary>
		/// Edit method.
		/// </summary>
		public virtual void Edit() {}
		
		/// <summary>
		/// Subaction edit method.
		/// </summary>
		/// <param name="path">Set of indexes for subaction identification.</param>
		public virtual void Edit(List<int> path) {}
		
		/// <summary>
		/// Subaction removal method.
		/// </summary>
		/// <param name="path">Set of indexes for subaction identification.</param>
		public virtual void Remove(List<int> path) {}
	}
}
