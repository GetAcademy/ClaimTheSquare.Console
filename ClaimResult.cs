namespace ClaimTheSquareConsole;

public class ClaimResult
{
    public ClaimResult(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
}
