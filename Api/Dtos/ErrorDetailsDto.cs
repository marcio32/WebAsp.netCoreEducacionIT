namespace Api.Dtos
{
    public class ErrorDetailsDto
    {
        public int StatusCodes { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
