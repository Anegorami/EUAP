using System;
using System.Collections.Generic;

using RapeEngine.Maker.Actions;
using RapeEngine.Maker.Conditions;

namespace RapeEngine.Maker {
	/// <summary>
	/// Static class to store maker elements in.
	/// </summary>
	public static class ElementManager {
		/// <summary>
		/// Structure for a single item.
		/// </summary>
		public struct Item {
			/// <summary>
			/// Item name.
			/// </summary>
			public readonly string Name;
			
			/// <summary>
			/// Item description.
			/// </summary>
			public readonly string Description;
			
			/// <summary>
			/// Item itself.
			/// </summary>
			public readonly Type Value;
			
			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="name">Item name.</param>
			/// <param name="description">Item description.</param>
			/// <param name="value">Item itself.</param>
			public Item (string name, string description, Type value) {
				Name = name;
				Description = description;
				Value = value;
			}
		}
		
		/// <summary>
		/// Structure for an item group.
		/// </summary>
		public struct Group {
			/// <summary>
			/// Group name.
			/// </summary>
			public readonly string Name;
			
			/// <summary>
			/// Group description.
			/// </summary>
			public readonly string Description;
			
			/// <summary>
			/// Group items.
			/// </summary>
			public readonly List<Item> Items;
			
			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="name">Group name.</param>
			/// <param name="description">Group description.</param>
			public Group(string name, string description) {
				Name = name;
				Description = description;
				Items = new List<Item>();
			}
		}
		
		/// <summary>
		/// Main list for actions.
		/// </summary>
		public static readonly List<Group> RootActions = new List<Group>();
		
		/// <summary>
		/// Main list for groups.
		/// </summary>
		public static readonly List<Group> RootConditions = new List<Group>();
		
		/// <summary>
		/// Initialization submethod for "Flow of control" actions.
		/// </summary>
		static void InitActionsFlow() {
			string name;
			string description;
			
			name = "Flow of control";
			description = "Contains the elements to change the actions' flow, like conditional branches and loops.";
			var actions_flow = new Group(name, description);
			
			name = "Conditional branch";
			description = "Add a new conditional branch (IF statement).";
			actions_flow.Items.Add(new Item(name, description, typeof(FlowIf)));
			
			RootActions.Add(actions_flow);
		}
		
		/// <summary>
		/// Initialization submethod for "Flags and Variables" actions.
		/// </summary>
		static void InitActionsVars() {
			string name;
			string description;
			
			name = "Flags and Variables";
			description = "Contains the actions that changes a value of a flag or a variable.";
			var actions_vars = new Group(name, description);
			
			name = "Set a flag";
			description = "Set a flag to True or False.";
			actions_vars.Items.Add(new Item(name, description, typeof(VarSetFlag)));
			
			name = "Set a variable";
			description = "Set a value for a variable.";
			actions_vars.Items.Add(new Item(name, description, typeof(VarSetVar)));
			
			RootActions.Add(actions_vars);
		}
		
		/// <summary>
		/// Initialization submethod for "Audio" actions.
		/// </summary>
		static void InitActionsAudio() {
			string name;
			string description;
			
			name = "Audio";
			description = "Contains audio-related actions.";
			var actions_audio = new Group(name, description);
			
			name = "Play BGM";
			description = "Play the background music.";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioPlayBGM)));
			
			name = "Play BGS";
			description = "Play the background sounds (ambient).";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioPlayBGS)));
			
			name = "Play ME";
			description = "Play the music effect.";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioPlayME)));
			
			name = "Play VO";
			description = "Play the voice over.";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioPlayVO)));
			
			name = "Play SE";
			description = "Play the sound effect.";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioPlaySE)));
			
			name = "Stop BGM";
			description = "Stop the background music.";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioStopBGM)));
			
			name = "Stop BGS";
			description = "Stop the background sounds (ambient).";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioStopBGS)));
			
			name = "Stop ME";
			description = "Stop the music effect.";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioStopME)));
			
			name = "Stop VO";
			description = "Stop the voice over.";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioStopVO)));
			
			name = "Stop SE";
			description = "Stop all sound effects.";
			actions_audio.Items.Add(new Item(name, description, typeof(AudioStopSE)));
			
			RootActions.Add(actions_audio);
		}
		
		/// <summary>
		/// Initialization submethod for "Flags and Variables" conditions.
		/// </summary>
		static void InitConditionsVars() {
			string name;
			string description;
			
			name = "Flags and Variables";
			description = "Contains the variable-driven conditions.";
			var conditions_vars = new Group(name, description);
			
			name = "Flag condition";
			description = "Condition that checks the flag state.";
			conditions_vars.Items.Add(new Item(name, description, typeof(VarFlag)));
			
			name = "Variable condition";
			description = "Condition that checks the variable value.";
			conditions_vars.Items.Add(new Item(name, description, typeof(VarVar)));
			
			RootConditions.Add(conditions_vars);
		}
		
		/// <summary>
		/// Initialization method.
		/// </summary>
		public static void Init() {
			InitActionsFlow();
			InitActionsVars();
			InitActionsAudio();
			
			InitConditionsVars();
		}
	}
}
