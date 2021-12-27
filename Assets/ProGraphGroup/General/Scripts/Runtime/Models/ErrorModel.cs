namespace ProGraphGroup.General.Models
{
    public class ErrorModel
    {
        public int Code;
        public string Message;

        public ErrorModel(int code, string message)
        {
            Code = code;
            Message = message;
        }
    
    }
}
