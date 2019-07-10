using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using RapeEngine.Components;

namespace RapeEngine.Maker {
	/// <summary>
	/// Main form of the Rape Maker.
	/// </summary>
	public partial class MainForm: Form {
		/// <summary>
		/// Directory to store the scripts in.
		/// </summary>
		const string SCRIPTS_DIR = "scripts";
		
		/// <summary>
		/// Main script name. Have a special behaviour.
		/// </summary>
		const string MAIN_SCRIPT = "main";
		
		/// <summary>
		/// Script file extension.
		/// </summary>
		const string SCRIPT_EXT = "rape";
		
		/// <summary>
		/// Script list RMB menu.
		/// </summary>
		readonly ContextMenuStrip script_menu = new ContextMenuStrip();
		
		/// <summary>
		/// Script list RMB menu.
		/// </summary>
		readonly string[] SCRIPT_MENU = {"New script", "Rename", "Delete", "Update"};
		
		/// <summary>
		/// Actions for the script menu.
		/// </summary>
		readonly Action[] SCRIPT_MENU_ACTIONS;
		
		/// <summary>
		/// Currently active script.
		/// </summary>
		Script active_script;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		public MainForm() {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			UpdateScriptsDirectory();
			
			AudioManager.Init(Handle);
			UserVariables.ReadNames();
			ElementManager.Init();
			
			// Initialize menus.
			foreach (string item in SCRIPT_MENU) {
				script_menu.Items.Add(item);
			}
			script_menu.ItemClicked += ScriptMenu;
			scripts_list.ContextMenuStrip = script_menu;
			
			// Initialize menu actions.
			SCRIPT_MENU_ACTIONS = new Action[]{CreateScript, RenameScript, DeleteScript, UpdateScriptsDirectory};
		}
		
		/// <summary>
		/// Makes a full path out of script name.
		/// </summary>
		/// <param name="name">Script name.</param>
		/// <returns>Full path.</returns>
		string MakeScriptName(string name) {
			return String.Format("{0}\\{1}.{2}", SCRIPTS_DIR, name, SCRIPT_EXT);
		}
		
		/// <summary>
		/// Rereads the script's directory structure.
		/// </summary>
		void UpdateScriptsDirectory() {
			scripts_list.Items.Clear();
			try {
				foreach (string file in Directory.GetFiles(SCRIPTS_DIR)) {
					if (Negolib.GetExtension(file) == SCRIPT_EXT) {
						scripts_list.Items.Add(Negolib.MakeKey(file));
					}
				}
			} catch (DirectoryNotFoundException) {
				Directory.CreateDirectory(SCRIPTS_DIR);
				var main = File.Create(MakeScriptName(MAIN_SCRIPT));
				main.Close();
				UpdateScriptsDirectory();
			}
			if (scripts_list.SelectedItem == null) {
				scripts_list.SelectedIndex = 0;
			}
		}
		
		/// <summary>
		/// Shows a very special error message.
		/// </summary>
		void MainScriptError() {
			const string message = "You cannot rename or delete the main script!";
			MessageBox.Show(message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		
		/// <summary>
		/// Method to create an empty script.
		/// </summary>
		void CreateScript() {
			var name = new NameForm();
			if (name.ShowDialog() == DialogResult.OK) {
				var script = File.Create(MakeScriptName(name.Value));
				script.Close();
				UpdateScriptsDirectory();
			}
		}
		
		/// <summary>
		/// Method to rename the script.
		/// </summary>
		void RenameScript() {
			string script = scripts_list.SelectedItem.ToString();
			if (script != MAIN_SCRIPT) {
				var name = new NameForm(script);
				if (name.ShowDialog() == DialogResult.OK) {
					File.Move(MakeScriptName(script), MakeScriptName(name.Value));
					UpdateScriptsDirectory();
				}
			} else {
				MainScriptError();
			}
		}
		
		/// <summary>
		/// Method to delete the script.
		/// </summary>
		void DeleteScript() {
			string script = scripts_list.SelectedItem.ToString();
			if (script != MAIN_SCRIPT) {
				File.Delete(MakeScriptName(script));
				UpdateScriptsDirectory();
			} else {
				MainScriptError();
			}
		}
		
		/// <summary>
		/// Method to get full path to the target node.
		/// </summary>
		/// <param name="target">Target node.</param>
		/// <returns>Set of indexes.</returns>
		List<int> getFullPath(TreeNode target) {
			var selection = new List<int>();
			while (target != null) {
				selection.Add(target.Index);
				target = target.Parent;
			}
			selection.Reverse();
			return selection;
		}
		
		/// <summary>
		/// Method to add the expanded state of the target node.
		/// </summary>
		/// <param name="node">Target node.</param>
		/// <param name="path">Path to the target node.</param>
		/// <param name="result">Dictionary to store th state in.</param>
		void SaveExpandedStateNode(TreeNode node, List<int> path, ref Dictionary<List<int>, bool> result) {
			result[new List<int>(path)] = node.IsExpanded;
			foreach (TreeNode subnode in node.Nodes) {
				path.Add(subnode.Index);
				SaveExpandedStateNode(subnode, path, ref result);
				path.RemoveAt(path.Count - 1);
			}
		}
		
		/// <summary>
		/// Method to save the nodes' expanded state in a dictionary.
		/// </summary>
		/// <returns>Dictionary of states.</returns>
		Dictionary<List<int>, bool> SaveExpandedState() {
			var result = new Dictionary<List<int>, bool>();
			var path = new List<int>();
			foreach (TreeNode node in actions_tree.Nodes) {
				path.Add(node.Index);
				SaveExpandedStateNode(node, path, ref result);
				path.RemoveAt(path.Count - 1);
			}
			return result;
		}
		
		/// <summary>
		/// Method to load the nodes' expanded state from a dictionary.
		/// </summary>
		/// <param name="data">Dictionary of states.</param>
		void LoadExpandedState(IReadOnlyDictionary<List<int>, bool> data) {
			foreach (List<int> path in data.Keys) {
				bool value = data[path];
				if (value) {
					try {
						TreeNode target = actions_tree.Nodes[path[0]];
						path.RemoveAt(0);
						while (path.Count > 0) {
							target = target.Nodes[path[0]];
							path.RemoveAt(0);
						}
						target.Expand();
					} catch (IndexOutOfRangeException) {
						// Empty on purpose.
						// Should catch any leftovers after node deletion.
					}
				}
			}
		}
		
		/// <summary>
		/// Method to update script's action tree.
		/// </summary>
		void UpdateScriptView() {
			List<int> selection = getFullPath(actions_tree.SelectedNode);
			
			Dictionary<List<int>, bool> expanded = SaveExpandedState();
			
			actions_tree.Nodes.Clear();
			foreach (TreeNode node in active_script.GetNodes()) {
				actions_tree.Nodes.Add(node);
			}
			
			LoadExpandedState(expanded);
			
			TreeNode target = null;
			foreach (int index in selection) {
				target = (target == null)? actions_tree.Nodes[index]: target.Nodes[index];
			}
			actions_tree.SelectedNode = target;
			
			if (actions_tree.SelectedNode == null) {
				actions_tree.SelectedNode = actions_tree.Nodes[0];
			}
		}
		
		/// <summary>
		/// Script menu response.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Selected option.</param>
		void ScriptMenu(object sender, ToolStripItemClickedEventArgs e) {
			script_menu.Close();
			for (int item = 0; item < SCRIPT_MENU.Length; item++) {
				if (e.ClickedItem.ToString() == SCRIPT_MENU[item]) {
					SCRIPT_MENU_ACTIONS[item]();
				}
			}
		}
		
		/// <summary>
		/// Response for active script changing.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Scripts_listSelectedValueChanged(object sender, EventArgs e) {
			if (active_script != null) {
				active_script.Save();
			}
			active_script = new Script(MakeScriptName(scripts_list.SelectedItem.ToString()));
			UpdateScriptView();
		}
		
		/// <summary>
		/// Response for edit action/condition command.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Action's node.</param>
		void Actions_treeNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
			active_script.Edit(getFullPath(e.Node));
			UpdateScriptView();
		}
		
		/// <summary>
		/// Response for clicking Vars Window button.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void Button_varsClick(object sender, EventArgs e) {
			var form = new VariablesForm();
			form.ShowDialog();
		}
		
		/// <summary>
		/// Response for closing the main form.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		void MainFormFormClosed(object sender, FormClosedEventArgs e) {
			if (active_script != null) {
				active_script.Save();
			}
			UserVariables.WriteNames();
		}
		
		/// <summary>
		/// Response for keypress when the node is selected.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Pressed key.</param>
		void Actions_treeKeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Delete) {
				active_script.Remove(getFullPath(actions_tree.SelectedNode));
				UpdateScriptView();
			}
		}
	}
}
