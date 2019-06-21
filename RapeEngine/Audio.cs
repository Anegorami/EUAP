using System;
using System.IO;
using System.Globalization;
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
			public readonly int sample;
			
			/// <summary>
			/// Position of the beginning of the loop (in seconds).
			/// </summary>
			public readonly double loopStart;
			
			/// <summary>
			/// Position of the end of the loop (in seconds).
			/// </summary>
			public readonly double loopEnd;
			
			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="_sample">Sample handle.</param>
			/// <param name="_loop_start">Loop start point (in seconds).</param>
			/// <param name="_loop_end">Loop end point (in seconds).</param>
			public Bgm(int _sample, double _loop_start, double _loop_end) {
				sample = _sample;
				loopStart = _loop_start;
				loopEnd = _loop_end;
			}
		}
		
		/// <summary>
		/// Directory with the background music (in Ogg Vorbis format) to scan.
		/// </summary>
		const string BGM_DIR = "bgm";
		
		/// <summary>
		/// Dictionary for storing the loaded samples.
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
			// The "handle" is the main window's handle to bind audio device to (for Windows Audio Mixer).
			Bass.BASS_Init(-1, 48000, BASSInit.BASS_DEVICE_DEFAULT, handle);
			
			// BGM folder scanning.
			foreach (string filename in Directory.GetFiles(BGM_DIR)) {
				// Getting the key out of the filename (so, "bgm/test.ogg" would become "test").
				int origin = BGM_DIR.Length + 1;
				int length = filename.Length - (origin) - 4;
				string key = filename.Substring(origin, length);
				
				// Sample loading.
				// First 0 is the starting point.
				// Second 0 is the length (meaning the whole file).
				// The 1 is the maximum channels playing the same sample simulteniously. For BGM, 1 is a good choice.
				int sample = Bass.BASS_SampleLoad(filename, 0, 0, 1, BASSFlag.BASS_DEFAULT);
				
				TAG_INFO tag = BassTags.BASS_TAG_GetFromFile(filename);
				
				// Extracting loop timepoints from the tags.
				// Also, big thanks to Microsoft for overengineering String to Double conversion.
				double loop_start = double.Parse(tag.NativeTag("loop_start"), CultureInfo.InvariantCulture);
				double loop_end = double.Parse(tag.NativeTag("loop_end"), CultureInfo.InvariantCulture);
				
				bgm[key] = new Bgm(sample, loop_start, loop_end);
			}
		}
		
		/// <summary>
		/// Initialization function.
		/// Designed to keep the "handle" argument out of getInstance() calls.
		/// </summary>
		/// <param name="handle">Main window handle. Required for the BASS library initialization.</param>
		public static void init(IntPtr handle) {
			instance = new Audio(handle);
		}
		
		/// <summary>
		/// A method for getting the singletone instance.
		/// </summary>
		/// <returns>Audio object.</returns>
		public static Audio getInstance() {
			return instance;
		}
		
		/// <summary>
		/// A method to play the background music.
		/// </summary>
		/// <param name="bgmname">Filename without the extension.</param>
		public void playBGM(string bgmname) {
			// Stops any current BGM and frees the channel.
			Bass.BASS_ChannelStop(bgm_channel);
			
			bgm_current = bgm[bgmname];
			bgm_channel = Bass.BASS_SampleGetChannel(bgm_current.sample, false);
			Bass.BASS_ChannelPlay(bgm_channel, false);
		}
		
		/// <summary>
		/// A method to update the state of the Audio System.
		/// </summary>
		public void update() {
			// Looping.
			long current_position_bytes = Bass.BASS_ChannelGetPosition(bgm_channel);
			double current_position = Bass.BASS_ChannelBytes2Seconds(bgm_channel, current_position_bytes);
			if (current_position > bgm_current.loopEnd) {
				double new_position = bgm_current.loopStart + (current_position - bgm_current.loopEnd);
				Bass.BASS_ChannelSetPosition(bgm_channel, new_position);
			}
		}
	}
}
