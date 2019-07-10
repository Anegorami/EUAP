using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

using Newtonsoft.Json;

using RapeEngine.Maker.Conditions;

namespace RapeEngine.Maker.Actions {
	/// <summary>
	/// IF implementation.
	/// </summary>
	public sealed class FlowIf: BaseScriptAction {
		/// <summary>
		/// AND mode description.
		/// </summary>
		const string AND_LOGIC_DESC = "If all of the [Conditions] are true," +
			" then [True Branch], else [False Branch]";
		
		/// <summary>
		/// OR mode description.
		/// </summary>
		const string OR_LOGIC_DESC = "If any of the [Conditions] is true," +
			" then [True Branch], else [False Branch]";
		
		/// <summary>
		/// Array of conditions.
		/// </summary>
		[JsonProperty]
		readonly ElementsKeeper conditions = new ElementsKeeper();
		
		/// <summary>
		/// Array of actions for a true result.
		/// </summary>
		[JsonProperty]
		readonly ElementsKeeper true_branch = new ElementsKeeper();
		
		/// <summary>
		/// Array of actions for a false result.
		/// </summary>
		[JsonProperty]
		readonly ElementsKeeper false_branch = new ElementsKeeper();
		
		/// <summary>
		/// Mode flag.
		/// </summary>
		[JsonProperty]
		bool or_logic;
		
		/// <summary>
		/// Node for a visual representation.
		/// </summary>
		public override TreeNode Node {
			get {
				var node = new TreeNode((or_logic)? OR_LOGIC_DESC: AND_LOGIC_DESC);
				
				var cnode = node.Nodes.Add("Conditions");
				foreach (BaseScriptCondition condition in conditions) {
					cnode.Nodes.Add(condition.Node);
				}
				
				var tnode = node.Nodes.Add("True Branch");
				foreach (BaseScriptAction action in true_branch) {
					tnode.Nodes.Add(action.Node);
				}
				
				var fnode = node.Nodes.Add("False Branch");
				foreach (BaseScriptAction action in false_branch) {
					fnode.Nodes.Add(action.Node);
				}
				
				node.ForeColor = Color.Green;
				
				return node;
			}
		}
		
		/// <summary>
		/// Initialization method.
		/// </summary>
		/// <returns>True, since no GUI is present.</returns>
		public override bool Initialize() {
			conditions.Add(new VirtualNewCondition(conditions));
			true_branch.Add(new VirtualNewAction(true_branch));
			false_branch.Add(new VirtualNewAction(false_branch));
			return true;
		}
		
		/// <summary>
		/// Edit method.
		/// </summary>
		public override void Edit() {
			or_logic = !or_logic;
		}
		
		/// <summary>
		/// Subaction edit method.
		/// </summary>
		/// <param name="path">Set of indexes for subaction identification.</param>
		public override void Edit(List<int> path) {
			if (path.Count >= 2) {
				int root = path[0];
				path.RemoveAt(0);
				
				switch (root) {
					case 0:
						conditions.Edit(path);
						break;
					case 1:
						true_branch.Edit(path);
						break;
					case 2:
						false_branch.Edit(path);
						break;
				}
			}
		}
		
		/// <summary>
		/// Subaction removal method.
		/// </summary>
		/// <param name="path">Set of indexes for subaction identification.</param>
		public override void Remove(List<int> path) {
			if (path.Count >= 2) {
				int root = path[0];
				path.RemoveAt(0);
				
				switch (root) {
					case 0:
						conditions.Remove(path);
						return;
					case 1:
						true_branch.Remove(path);
						break;
					case 2:
						false_branch.Remove(path);
						break;
				}
			}
		}
	}
}
