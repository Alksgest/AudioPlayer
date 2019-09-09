using System;
using AudioWorker.Exceptions;
using NAudio.Wave;

namespace AudioWorker.Models
{
    public class AudioData
    {
        public String FileName { get; private set; }
        public TimeSpan? CurrentTime { get; private set; }
        public TimeSpan? TotalTime { get; private set; }
        public Single? Volume { get; private set; }

        internal AudioData() { }

        internal void Init(AudioFileReader audioFileReader)
        {
            if(audioFileReader == null)
            {
                throw new AudioFileReaderNotInitializedException();
            }
            this.FileName = audioFileReader.FileName;
            this.CurrentTime = audioFileReader.CurrentTime;
            this.TotalTime = audioFileReader.TotalTime;
            this.Volume = audioFileReader.Volume;
        }
    }
}