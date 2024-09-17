namespace Application.Responses;

public class Response<T>
{
    public string Message { get; set; }
    public int Status { get; set; }
    public T? Data { get; set; }

    public Response(string message, int status)
    {
        Message = message;
        Status = status;
    }

    public Response(string message, int status, T? data)
    {
        Message = message;
        Status = status;
        Data = data;
    }
}