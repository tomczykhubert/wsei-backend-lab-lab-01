using System.Runtime.Serialization;

namespace Infrastructure.Services
{
    [Serializable]
    internal class QuizNotFoundException : Exception
    {
        public QuizNotFoundException()
        {
        }

        public QuizNotFoundException(string? message) : base(message)
        {
        }

        public QuizNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QuizNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}