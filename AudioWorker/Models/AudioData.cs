using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AudioWorker.Exceptions;
using NAudio.Wave;

namespace AudioWorker.Models
{
    public class AudioData : INotifyPropertyChanged
    {
        public String FilePath { get; private set; }
        public String FileName { get; private set; }
        private TimeSpan? _curentTime;
        public TimeSpan? CurrentTime
        {
            get
            {
                return _curentTime;
            }
            private set
            {
                _curentTime = value;
                NotifyPropertyChanged("CurrentTime");
            }
        }
        public TimeSpan? TotalTime { get; private set; }
        public Single? Volume { get; private set; }

        internal AudioData() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void Init(AudioFileReader audioFileReader)
        {
            if(audioFileReader == null)
            {
                throw new AudioFileReaderNotInitializedException();
            }
            this.FilePath = audioFileReader.FileName;

            var splited = FilePath.Split('\\');

            this.FileName = splited[splited.Length - 1];
            this.CurrentTime = audioFileReader.CurrentTime;
            this.TotalTime = audioFileReader.TotalTime;
            this.Volume = audioFileReader.Volume;
        }
    }
}