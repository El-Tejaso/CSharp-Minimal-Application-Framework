﻿using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;

namespace MinimalAF.Audio {
    /// <summary>
    /// Merely an AudioData that has been loaded into an OpenAL buffer.
    /// 
    /// Currently I have chosen to only support Mono16 and Stereo16 formats for simplicity
    /// </summary>
    public class AudioClipOneShot : IDisposable {
        private int _ALBuffer = -1;
        internal int ALBuffer {
            get {
                return _ALBuffer;
            }
        }

        public AudioData Data {
            get; private set;
        }


        private static Dictionary<AudioData, AudioClipOneShot> _audioClipCache = new Dictionary<AudioData, AudioClipOneShot>();
        public static AudioClipOneShot FromAudioData(AudioData data) {
            if (_audioClipCache.ContainsKey(data)) {
                return _audioClipCache[data];
            }

            AudioClipOneShot clip = new AudioClipOneShot(data);
            _audioClipCache[data] = clip;
            return clip;
        }

        private AudioClipOneShot(AudioData audioData) {
            AudioCTX.ALCall(out _ALBuffer, () => { return AL.GenBuffer(); });

            short[] data = audioData.RawData;
            int sampleRate = audioData.SampleRate;
            int channels = audioData.Channels;

            if (channels == 1) {
                AL.BufferData(_ALBuffer, ALFormat.Mono16, data, sampleRate);
            } else {
                AL.BufferData(_ALBuffer, ALFormat.Stereo16, data, sampleRate);
            }

            Data = audioData;
        }

        public static AudioClipOneShot FromFile(string filepath, AudioDataImportSettings settings = null) {
            //TODO: Cache this as well as audiodata
            AudioData d = AudioData.FromFile(filepath, settings);
            if (d == null)
                return null;

            return new AudioClipOneShot(d);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                }

                Data = null;
                //also detatch from source if possible
                AL.DeleteBuffer(_ALBuffer);

                disposedValue = true;
            }
        }

        ~AudioClipOneShot() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
