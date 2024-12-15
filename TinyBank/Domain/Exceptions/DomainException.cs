namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainErrorCode DomainErrorCode { get; }

        public DomainException(DomainErrorCode domainErrorCode)
            : base(domainErrorCode.ErrorMessage)
        {
            DomainErrorCode = domainErrorCode;
        }
    }
}
