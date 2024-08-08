namespace SistemaLogin.Errors
{
    public class APIException
    {
        public APIException(string statusCode, string errorMessage, string details)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            Details = details;
        }

        public string StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Details { get; set; }
           
    }
}
