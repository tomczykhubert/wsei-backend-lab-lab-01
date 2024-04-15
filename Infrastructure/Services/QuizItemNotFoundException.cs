using System.Runtime.Serialization;

namespace Infrastructure.Services
{
    [Serializable]
    internal class QuizItemNotFoundException : Exception
    {
        public QuizItemNotFoundException()
        {
        }

        public QuizItemNotFoundException(string? message) : base(message)
        {
        }

        public QuizItemNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QuizItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}