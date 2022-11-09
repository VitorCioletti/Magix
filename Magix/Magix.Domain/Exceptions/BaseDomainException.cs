namespace Magix.Domain.Exceptions
{
    using System;

    public class BaseDomainException : Exception
    {
        public string Id { get; private set; }

        protected BaseDomainException(string id, string message) : base(message)
        {
            Id = id;
        }
    }
}
