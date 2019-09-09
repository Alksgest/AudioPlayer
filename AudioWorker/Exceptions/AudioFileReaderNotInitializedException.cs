using System;
using System.Runtime.Serialization;

namespace AudioWorker.Exceptions
{
    [Serializable]
    internal class AudioFileReaderNotInitializedException : Exception
    {
        public AudioFileReaderNotInitializedException()
        {
        }

        public AudioFileReaderNotInitializedException(string message) : base(message)
        {
        }

        public AudioFileReaderNotInitializedException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        protected AudioFileReaderNotInitializedException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}