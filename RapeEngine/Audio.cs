using System;
using System.IO;
using System.Collections.Generic;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;

namespace RapeEngine {
	/// <summary>
	/// Singleton for audio-related operations (such as background music playing).
	/// </summary>
	public static class Audio {
		/// <summary>
		/// Small structure to keep all of the BGM-related data in one place.
		/// </summary>
		struct LoopedSample {
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
			/// <param name="filename">File name.</param>
			public LoopedSample(string filename) {
				// Sample loading.
				// First 0 is the starting point.
				// Second 0 is the length (meaning the whole file).
				// The 1 is the maximum channels playing the same sample simulteniously.
				// For looped samples, 1 is a good choice.
				Sample = Bass.BASS_SampleLoad(filename, 0, 0, 1, BASSFlag.BASS_DEFAULT);
				
				// Extracting loop timepoints from the tags.
				TAG_INFO tag = BassTags.BASS_TAG_GetFromFile(filename);
				string test = tag.NativeTag("loop_start");
				if (test != null) {
					LoopStart = Negolib.StringToDouble(test);
					LoopEnd = Negolib.StringToDouble(tag.NativeTag("loop_end"));
				} else {
					LoopStart = 0;
					LoopEnd = Bass.BASS_ChannelBytes2Seconds(Sample, Bass.BASS_SampleGetInfo(Sample).length);
				}
			}
		}
		
		/// <summary>
		/// Dictionary for storing the loaded BGM samples.
		/// </summary>
		static readonly Dictionary<string, LoopedSample> bgm = new Dictionary<string, LoopedSample>();
		
		/// <summary>
		/// Active BGM.
		/// </summary>
		static LoopedSample bgm_current;
		
		/// <summary>
		/// Active BGM channel handle.
		/// </summary>
		static int bgm_channel;
		
		/// <summary>
		/// Fade timer. Used in FadeIn/FadeOut methods.
		/// </summary>
		static double bgm_fade_timer;
		
		/// <summary>
		/// Initial value of fade timer. Used in FadeIn/FadeOut methods.
		/// </summary>
		static double bgm_fade_timer_max;
		
		/// <summary>
		/// Dictionary for storing the loaded BGS samples.
		/// </summary>
		static readonly Dictionary<string, LoopedSample> bgs = new Dictionary<string, LoopedSample>();
		
		/// <summary>
		/// Active BGS.
		/// </summary>
		static LoopedSample bgs_current;
		
		/// <summary>
		/// Active BGS channel handle.
		/// </summary>
		static int bgs_channel;
		
		/// <summary>
		/// Fade timer. Used in FadeIn/FadeOut methods.
		/// </summary>
		static double bgs_fade_timer;
		
		/// <summary>
		/// Initial value of fade timer. Used in FadeIn/FadeOut methods.
		/// </summary>
		static double bgs_fade_timer_max;
		
		/// <summary>
		/// Dictionary to store the loaded SE samples' handles.
		/// </summary>
		static readonly Dictionary<string, int> se = new Dictionary<string, int>();
		
		/// <summary>
		/// Delegate for an update action.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		delegate void UpdateAction(double dt);
		
		/// <summary>
		/// Update event. Called in Update method.
		/// </summary>
		static event UpdateAction OnUpdate;
		
		/// <summary>
		/// Initialization function.
		/// </summary>
		/// <param name="handle">Main window handle. Required for the BASS library initialization.</param>
		public static void Init(IntPtr handle) {
			// BASS library initialization.
			// -1 is the default audio device.
			// 48000 is the sample rate.
			// The "handle" is the main window's handle to bind the audio device to (for Windows Audio Mixer).
			Bass.BASS_Init(-1, 48000, BASSInit.BASS_DEVICE_DEFAULT, handle);
			
			// BGM folder scanning.
			foreach (string filename in Directory.GetFiles("bgm")) {
				bgm[Negolib.MakeKey(filename)] = new LoopedSample(filename);
			}
			
			// BGS folder scanning.
			foreach (string filename in Directory.GetFiles("bgs")) {
				bgs[Negolib.MakeKey(filename)] = new LoopedSample(filename);
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
		/// An update method. Should be bound to Graphic.OnUpdate event.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		public static void Update(double dt) {
			Looping();
			if (OnUpdate != null) {
				OnUpdate(dt);
			}
		}
		
		/// <summary>
		/// A method to loop active BGM and BGS. A permanent part of Update.
		/// </summary>
		static void Looping() {
			long current_position_bytes = Bass.BASS_ChannelGetPosition(bgm_channel);
			double current_position = Bass.BASS_ChannelBytes2Seconds(bgm_channel, current_position_bytes);
			if (current_position > bgm_current.LoopEnd) {
				double new_position = bgm_current.LoopStart + (current_position - bgm_current.LoopEnd);
				Bass.BASS_ChannelSetPosition(bgm_channel, new_position);
			}
		}
		
		/// <summary>
		/// A method to fadeout the BGM.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		static void BGMFadeOut(double dt) {
			bgm_fade_timer = Math.Max(bgm_fade_timer - dt, 0);
			double volume = bgm_fade_timer / bgm_fade_timer_max;
			Bass.BASS_ChannelSetAttribute(bgm_channel, BASSAttribute.BASS_ATTRIB_VOL, (float) volume);
			
			if (Math.Abs(volume) < Double.Epsilon) {
				Bass.BASS_ChannelStop(bgm_channel);
				OnUpdate -= BGMFadeOut;
			}
		}
		
		/// <summary>
		/// A method to fadeout the BGS.
		/// </summary>
		/// <param name="dt">Time taken to draw the current frame.</param>
		static void BGSFadeOut(double dt) {
			bgs_fade_timer = Math.Max(bgs_fade_timer - dt, 0);
			double volume = bgs_fade_timer / bgs_fade_timer_max;
			Bass.BASS_ChannelSetAttribute(bgs_channel, BASSAttribute.BASS_ATTRIB_VOL, (float) volume);
			
			if (Math.Abs(volume) < Double.Epsilon) {
				Bass.BASS_ChannelStop(bgs_channel);
				OnUpdate -= BGSFadeOut;
			}
		}
		
		/// <summary>
		/// A method to check if the BGM is playing.
		/// </summary>
		/// <returns>True, if the BGM is playing, false otherwise.</returns>
		public static bool IsBGMPlaying() {
			return Bass.BASS_ChannelIsActive(bgm_channel) == BASSActive.BASS_ACTIVE_PLAYING;
		}
		
		/// <summary>
		/// A method to play the background music.
		/// </summary>
		/// <param name="bgmname">Filename without the extension.</param>
		public static void PlayBGM(string bgmname) {
			// Stops any current BGM and frees the channel.
			Bass.BASS_ChannelStop(bgm_channel);
			
			bgm_current = bgm[bgmname];
			bgm_channel = Bass.BASS_SampleGetChannel(bgm_current.Sample, false);
			Bass.BASS_ChannelPlay(bgm_channel, false);
		}
		
		/// <summary>
		/// A method to stop currently playing BGM.
		/// </summary>
		/// <param name="time">Fade time (in seconds).</param>
		public static void StopBGM (double time = 1) {
			bgm_fade_timer = time;
			bgm_fade_timer_max = time;
			OnUpdate += BGMFadeOut;
		}
		
		/// <summary>
		/// A method to check if the BGS is playing.
		/// </summary>
		/// <returns>True, if the BGS is playing, false otherwise.</returns>
		public static bool IsBGSPlaying() {
			return Bass.BASS_ChannelIsActive(bgs_channel) == BASSActive.BASS_ACTIVE_PLAYING;
		}
		
		/// <summary>
		/// A method to play the background music.
		/// </summary>
		/// <param name="bgsname">Filename without the extension.</param>
		public static void PlayBGS(string bgsname) {
			// Stops any current BGM and frees the channel.
			Bass.BASS_ChannelStop(bgs_channel);
			
			bgs_current = bgs[bgsname];
			bgs_channel = Bass.BASS_SampleGetChannel(bgs_current.Sample, false);
			Bass.BASS_ChannelPlay(bgs_channel, false);
		}
		
		/// <summary>
		/// A method to stop currently playing BGS.
		/// </summary>
		/// <param name="time">Fade time (in seconds).</param>
		public static void StopBGS (double time = 1) {
			bgs_fade_timer = time;
			bgs_fade_timer_max = time;
			OnUpdate += BGSFadeOut;
		}
		
		/// <summary>
		/// A method to play the sound effect.
		/// </summary>
		/// <param name="sename">Filename without the extension.</param>
		public static void PlaySE(string sename) {
			int se_channel = Bass.BASS_SampleGetChannel(se[sename], false);
			Bass.BASS_ChannelPlay(se_channel, false);
		}
	}
}
