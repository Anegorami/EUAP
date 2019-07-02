using System;
using System.IO;
using System.Windows.Forms;

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
		/// Script list RMB menu.
		/// </summary>
		readonly string[] SCRIPT_MENU = {"New script", "Rename", "Delete", "Update"};
		
		/// <summary>
		/// Actions for the script menu.
		/// </summary>
		readonly Action[] SCRIPT_MENU_ACTIONS;
		
		/// <summary>
		/// Script list RMB menu.
		/// </summary>
		readonly ContextMenuStrip script_menu = new ContextMenuStrip();
		
		/// <summary>
		/// Makes a full path out of script name.
		/// </summary>
		/// <param name="name">Script name.</param>
		/// <returns>Full path.</returns>
		string MakeScriptName(string name) {
			return String.Format("{0}\\{1}.rape", SCRIPTS_DIR, name);
		}
		
		/// <summary>
		/// Rereads the script's directory structure.
		/// </summary>
		void UpdateScriptsDirectory() {
			scripts_list.Items.Clear();
			try {
				foreach (string file in Directory.GetFiles(SCRIPTS_DIR)) {
					scripts_list.Items.Add(Negolib.MakeKey(file));
				}
			} catch (DirectoryNotFoundException) {
				Directory.CreateDirectory(SCRIPTS_DIR);
				var main = File.Create(MakeScriptName(MAIN_SCRIPT));
				main.Close();
				UpdateScriptsDirectory();
			}
			scripts_list.SelectedIndex = 0;
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
			string script = scripts_list.Items[scripts_list.SelectedIndex].ToString();
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
			string script = scripts_list.Items[scripts_list.SelectedIndex].ToString();
			if (script != MAIN_SCRIPT) {
				File.Delete(MakeScriptName(script));
				UpdateScriptsDirectory();
			} else {
				MainScriptError();
			}
		}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		public MainForm() {
			// Required.
			InitializeComponent();
			
			// Constructor code goes here...
			UpdateScriptsDirectory();
			
			// Initialize menus.
			foreach (string item in SCRIPT_MENU) {
				script_menu.Items.Add(item);
			}
			script_menu.ItemClicked += ScriptMenu;
			
			// Initialize menu actions.
			SCRIPT_MENU_ACTIONS = new Action[]{CreateScript, RenameScript, DeleteScript, UpdateScriptsDirectory};
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
	}
}
