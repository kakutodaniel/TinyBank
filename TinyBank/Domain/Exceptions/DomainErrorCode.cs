namespace Domain.Exceptions
{
    public class DomainErrorCode
    {
        public int ErrorCode { get; }

        public string ErrorMessage { get; }

        public DomainErrorCode(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public static DomainErrorCode InvalidUserDocument => new(1000, "Invalid user document");

        public static DomainErrorCode ValueMustBeGreaterThanZero => new(1500, "Value must be greater than zero");

        public static DomainErrorCode InsufficientFunds => new(2000, "Insufficient funds to perform this transaction");

        public static DomainErrorCode AccountIsRequired => new(2500, "Account is required");

        public static DomainErrorCode InactiveUserCanNotPerformTransactions => new(3000, "Inactive user can not perform transactions");

    }
}
