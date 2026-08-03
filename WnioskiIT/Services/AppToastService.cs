namespace WnioskiIT.Services;

public class AppToastService
{
    public event Action<string>? OnShow;

    public void Show(string message) => OnShow?.Invoke(message);
}
