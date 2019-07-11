using System;
using System.Drawing;
using System.Windows.Forms;

using Newtonsoft.Json;

using RapeEngine.Components;

namespace RapeEngine.Maker.Conditions {
	/// <summary>
	/// Variable-driven condition.
	/// </summary>
	public sealed class VarVar: BaseScriptCondition {
		/// <summary>
		/// Node format.
		/// </summary>
		const string FORMAT = "If {0} {1} {2}";
		
		/// <summary>
		/// Variable index.
		/// </summary>
		[JsonProperty]
		int index;
		
		/// <summary>
		/// Operation index.
		/// </summary>
		[JsonProperty]
		int operation;
		
		/// <summary>
		/// Either a constant value or a variable index.
		/// </summary>
		[JsonProperty]
		int value;
		
		/// <summary>
		/// If this flag is true, value contain a variable index.
		/// </summary>
		[JsonProperty]
		bool is_variable;
		
		/// <summary>
		/// Visual representation for operation index.
		/// </summary>
		readonly string[] operations = {"==", "!=", "<", "<=", ">", ">="};
		
		/// <summary>
		/// Node for a visual representation.
		/// </summary>
		public override TreeNode Node {
			get {
				string obj = UserVariables.GetVariableName(index);
				string sub = (is_variable)? UserVariables.GetVariableName(value): Convert.ToString(value);
				var node = new TreeNode(String.Format(FORMAT, obj, operations[operation], sub));
				node.ForeColor = Color.Red;
				return node;
			}
		}
		
		/// <summary>
		/// Initialization method.
		/// </summary>
		/// <returns>True, if the initialization was successful, false otherwise.</returns>
		public override bool Initialize() {
			var form = new VarVarForm(VAR_FORM_MODE.CONDITION, index, operation, value, is_variable);
			if (form.ShowDialog() == DialogResult.OK) {
				index = form.Index;
				operation = form.Operation;
				value = form.Value;
				is_variable = form.IsVariable;
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Edit method.
		/// </summary>
		public override void Edit() {
			Initialize();
		}
	}
}
