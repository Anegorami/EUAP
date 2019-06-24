using System;
using System.IO;
using System.Collections.Generic;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;

namespace RapeEngine {
	/// <summary>
	/// Singleton for audio-related operations (such as background music playing).
	/// </summary>
	public sealed class Audio {
		/// <summary>
		/// Small structure to keep all of the BGM-related data in one place.
		/// </summary>
		struct Bgm {
			/// <summary>
			/// Sample handle.
			/// </summary>
			public readonly int Sample;
			
			/// <summary>
			/// Position of the beginning of the loop (in seconds).
			/// </summary>
			public readonly double LoopStart;
			
			/// <summary>
			/// Position of the end of the loop (in seconds).
			/// </summary>
			public readonly double LoopEnd;
			
			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="sample">Sample handle.</param>
			/// <param name="loop_start">Loop start point (in seconds).</param>
			/// <param name="loop_end">Loop end point (in seconds).</param>
			public Bgm(int sample, double loop_start, double loop_end) {
				Sample = sample;
				LoopStart = loop_start;
				LoopEnd = loop_end;
			}
		}
		
		/// <summary>
		/// Dictionary for storing the loaded BGM samples.
		/// </summary>
		readonly Dictionary<string, Bgm> bgm = new Dictionary<string, Bgm>();
		
		/// <summary>
		/// Active BGM.
		/// </summary>
		Bgm bgm_current;
		
		/// <summary>
		/// Active BGM channel handle.
		/// </summary>
		int bgm_channel;
		
		/// <summary>
		/// Dictionary to store the loaded SE samples' handles.
		/// </summary>
		readonly Dictionary<string, int> se = new Dictionary<string, int>();
		
		/// <summary>
		/// Static instance poiter.
		/// </summary>
		static Audio instance;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		Audio(IntPtr handle) {
			// BASS library initialization.
			// -1 is the default audio device.
			// 48000 is the sample rate.
			// The "handle" is the main window's handle to bind the audio device to (for Windows Audio Mixer).
			Bass.BASS_Init(-1, 48000, BASSInit.BASS_DEVICE_DEFAULT, handle);
			
			// BGM folder scanning.
			foreach (string filename in Directory.GetFiles("bgm")) {
				// Sample loading.
				// First 0 is the starting point.
				// Second 0 is the length (meaning the whole file).
				// The 1 is the maximum channels playing the same sample simulteniously. For BGM, 1 is a good choice.
				int sample = Bass.BASS_SampleLoad(filename, 0, 0, 1, BASSFlag.BASS_DEFAULT);
				
				// Extracting loop timepoints from the tags.
				// Also, big thanks to Microsoft for overengineering String to Double conversion.
				TAG_INFO tag = BassTags.BASS_TAG_GetFromFile(filename);
				double loop_start = Negolib.S2D(tag.NativeTag("loop_start"));
				double loop_end = Negolib.S2D(tag.NativeTag("loop_end"));
				
				bgm[Negolib.MakeKey(filename)] = new Bgm(sample, loop_start, loop_end);
			}
			
			// SE folder scanning.
			foreach (string filename in Directory.GetFiles("se")) {
				// Sample loading.
				// First 0 is the starting point.
				// Second 0 is the length (meaning the whole file).
				// 10 is an amount af channels that can play the same sample simulteniously.
				se[Negolib.MakeKey(filename)] = Bass.BASS_SampleLoad(filename, 0, 0, 10, BASSFlag.BASS_DEFAULT);
			}
		}
		
		/// <summary>
		/// Initialization function.
		/// Designed to keep the "handle" argument out of getInstance() calls.
		/// </summary>
		/// <param name="handle">Main window handle. Required for the BASS library initialization.</param>
		public static void Init(IntPtr handle) {
			instance = new Audio(handle);
		}
		
		/// <summary>
		/// A method for getting the singletone instance.
		/// </summary>
		/// <returns>Audio object.</returns>
		public static Audio GetInstance() {
			return instance;
		}
		
		/// <summary>
		/// A method to play the background music.
		/// </summary>
		/// <param name="bgmname">Filename without the extension.</param>
		public void PlayBGM(string bgmname) {
			// Stops any current BGM and frees the channel.
			Bass.BASS_ChannelStop(bgm_channel);
			
			bgm_current = bgm[bgmname];
			bgm_channel = Bass.BASS_SampleGetChannel(bgm_current.Sample, false);
			Bass.BASS_ChannelPlay(bgm_channel, false);
		}
		
		/// <summary>
		/// A method to play the sound effect.
		/// </summary>
		/// <param name="sename">Filename without the extension.</param>
		public void PlaySE(string sename) {
			int se_channel = Bass.BASS_SampleGetChannel(se[sename], false);
			Bass.BASS_ChannelPlay(se_channel, false);
		}
		
		/// <summary>
		/// A method to update the state of the Audio System.
		/// </summary>
		public void Update() {
			// Looping.
			long current_position_bytes = Bass.BASS_ChannelGetPosition(bgm_channel);
			double current_position = Bass.BASS_ChannelBytes2Seconds(bgm_channel, current_position_bytes);
			if (current_position > bgm_current.LoopEnd) {
				double new_position = bgm_current.LoopStart + (current_position - bgm_current.LoopEnd);
				Bass.BASS_ChannelSetPosition(bgm_channel, new_position);
			}
		}
	}
}
