using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RapeEngine.Maker {
	/// <summary>
	/// Interface for all script elements.
	/// </summary>
	public interface IScriptElement {
		/// <summary>
		/// Node for a visual representation.
		/// </summary>
		TreeNode Node {get;}
		
		/// <summary>
		/// Initialization method.
		/// </summary>
		/// <returns></returns>
		bool Initialize();
		
		/// <summary>
		/// Edit method.
		/// </summary>
		void Edit();
		
		/// <summary>
		/// Subaction edit method.
		/// </summary>
		/// <param name="path">Set of indexes for subaction identification.</param>
		void Edit(List<int> path);
		
		/// <summary>
		/// Subaction removal method.
		/// </summary>
		/// <param name="path">Set of indexes for subaction identification.</param>
		void Remove(List<int> path);
	}
}
