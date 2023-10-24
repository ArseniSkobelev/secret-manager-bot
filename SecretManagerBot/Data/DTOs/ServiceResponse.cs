namespace SecretManagerBot.Data.DTOs;

public class ServiceResponse
{
    public ServiceResponse(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
}

public class ServiceResponse<T>
{
    public ServiceResponse(bool isSuccess, T data)
    {
        IsSuccess = isSuccess;
        Data = data;
    }

    public bool IsSuccess { get; set; }
    public T Data { get; set; }
}