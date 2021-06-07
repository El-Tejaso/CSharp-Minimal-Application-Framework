﻿using MinimalAF.Datatypes;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Audio.OpenAL;

namespace MinimalAF.Audio
{
    public class AudioData
    {
        short[] _rawData;
        public short[] RawData {
            get {
                return _rawData;
            }
        }

        private int _sampleRate;
        private int _channels;
        private int _len;
        private ALFormat _format;

        public ALFormat Format {
            get { return _format; }
        }


        public int Length => _len;

        public int SampleRate {
            get => _sampleRate;
        }

        public int Channels {
            get => _channels;
        }

        public double Duration {
            get => SampleToSeconds(Length);
        }

        public int DurationSamples {
            get => _rawData.Length / Channels;
        }


        public Slice<short> GetChannel(int c)
        {
            return new Slice<short>(_rawData, c, _rawData.Length - _channels + c + 1, _channels);
        }

        public double SampleToSeconds(int sample)
        {
            return sample / (double)SampleRate;
        }

        public int ToSample(double seconds)
        {
            return (int)(seconds * SampleRate);
        }

        public float GetSample(int sample, int channel)
        {
            return _rawData[channel % Channels + sample * Channels];
        }

        public AudioData(short[] rawInterleavedData, int sampleRate, int numChannels)
        {
            _sampleRate = sampleRate;
            _channels = numChannels;
            _rawData = rawInterleavedData;
            _len = _rawData.Length / numChannels;

            if (_channels == 2)
            {
                _format = ALFormat.Stereo16;
            }
            else
            {
                _format = ALFormat.Stereo8;
            }
        }

        public static AudioData FromFile(string filepath)
        {
            try
            {
                return LoadAudioClip(filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:\n {e.ToString()}");
                return null;
            }
        }

        //Code partially taken from https://stackoverflow.com/questions/42483778/how-to-get-float-array-of-samples-from-audio-file
        private static AudioData LoadAudioClip(string filepath)
        {
            using (MediaFoundationReader media = new MediaFoundationReader(filepath))
            {
                WaveFormat metadata = media.WaveFormat;
                int sampleRate = metadata.SampleRate;
                int channels = metadata.Channels;

                ISampleProvider isp = media.ToSampleProvider();

                int numSamples = (int)(media.TotalTime.TotalSeconds * sampleRate * channels);

                //TODO: consider using a less memory intensive storage method for audio
                float[] rawData = new float[numSamples];
                isp.Read(rawData, 0, rawData.Length);

                short[] rawData16bit = new short[rawData.Length];
                for (int i = 0; i < rawData.Length; i++)
                {
                    rawData16bit[i] = (short)(rawData[i] * short.MaxValue);
                }

                Console.WriteLine("Opened [" + filepath + "]");
                Console.WriteLine("Sample rate: [" + sampleRate + "]");
                Console.WriteLine("Duration: [" + media.TotalTime.TotalSeconds + "]");

                return new AudioData(rawData16bit, sampleRate, channels);
            }
        }
    }
}