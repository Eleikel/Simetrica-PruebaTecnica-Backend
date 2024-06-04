namespace TaskManager.Core.Application.Dtos;

public class TokenDto<T>
{
    public string Value { get; set; }
    public string Expire { get; set; }
    public T Info { get; set; }
}

