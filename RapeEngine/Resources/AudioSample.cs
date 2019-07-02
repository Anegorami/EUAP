using System;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;

namespace RapeEngine.Resources {
	/// <summary>
	/// Audio sample resource.
	/// </summary>
	public class AudioSample {
		/// <summary>
		/// Sample handle.
		/// </summary>
		readonly int handle;
		
		/// <summary>
		/// Looping flag.
		/// </summary>
		readonly bool looped;
		
		/// <summary>
		/// Loop start time.
		/// </summary>
		readonly double loop_start;
		
		/// <summary>
		/// Loop end time.
		/// </summary>
		readonly double loop_end;
		
		/// <summary>
		/// Current sample channel.
		/// </summary>
		int channel;
		
		/// <summary>
		/// Field to check if the sample is playing.
		/// </summary>
		public bool IsPlaying {
			get {
				return Bass.BASS_ChannelIsActive(channel) == BASSActive.BASS_ACTIVE_PLAYING;
			}
		}
		
		/// <summary>
		/// Volume field.
		/// </summary>
		public float Volume {
			get {
				float volume = 0;
				Bass.BASS_ChannelGetAttribute(channel, BASSAttribute.BASS_ATTRIB_VOL, ref volume);
				return volume;
			}
			set {
				Bass.BASS_ChannelSetAttribute(channel, BASSAttribute.BASS_ATTRIB_VOL, value);
			}
		}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="filename">Sample filename.</param>
		/// <param name="_looped">Looping flag.</param>
		/// <param name="channels_max">Maximum channels that this sample can use.</param>
		public AudioSample(string filename, bool _looped = false, int channels_max = 1) {
			// Sample loading.
			// First 0 is the starting point.
			// Second 0 is the length (meaning the whole file).
			handle = Bass.BASS_SampleLoad(filename, 0, 0, channels_max, BASSFlag.BASS_DEFAULT);
			
			// Looping fields initialization.
			// Cannot be moved out of constructor due to "readonly" data modifier.
			looped = _looped;
			loop_start = -1;
			loop_end = Double.PositiveInfinity;
			if (looped) {
				// Extracting loop timepoints from the tags.
				TAG_INFO tag = BassTags.BASS_TAG_GetFromFile(filename);
				string test = tag.NativeTag("loop_start");
				if (test != null) {
					loop_start = Negolib.StringToDouble(test);
					loop_end = Negolib.StringToDouble(tag.NativeTag("loop_end"));
				}
			}
		}
		
		/// <summary>
		/// Update method.
		/// </summary>
		public void Update() {
			if (looped) {
				long current_position_bytes = Bass.BASS_ChannelGetPosition(channel);
				double current_position = Bass.BASS_ChannelBytes2Seconds(channel, current_position_bytes);
				
				if (current_position > loop_end) {
					double new_position = loop_start + (current_position - loop_end);
					Bass.BASS_ChannelSetPosition(channel, new_position);
				}
			}
		}
		
		/// <summary>
		/// A method to play the sample.
		/// </summary>
		public void Play() {
			channel = Bass.BASS_SampleGetChannel(handle, false);
			Bass.BASS_ChannelPlay(channel, false);
			
			// Enable auto looping for looped samples without loop tags.
			if ((looped) && (loop_start < 0)) {
				Bass.BASS_ChannelFlags(channel, BASSFlag.BASS_SAMPLE_LOOP, BASSFlag.BASS_SAMPLE_LOOP);
			}
		}
		
		/// <summary>
		/// A method to stop the sample.
		/// </summary>
		public void Stop() {
			Bass.BASS_ChannelStop(channel);
		}
	}
}
