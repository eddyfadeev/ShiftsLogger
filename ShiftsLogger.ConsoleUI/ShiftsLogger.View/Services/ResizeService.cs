namespace ShiftsLogger.View.Services;

public static class ResizeService
{
    private static int _lastWindowHeight;
    private static int _lastWindowWidth;

    private static CancellationTokenSource? _cancellationTokenSource;

    public static event Action ConsoleResized;

    public static void Start()
    {
        if (_cancellationTokenSource != null)
        {
            return;
        }

        _lastWindowHeight = Console.WindowHeight;
        _lastWindowWidth = Console.WindowWidth;

        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        Task.Run(() => WatchConsoleResize(token), token);
    }

    public static void Stop()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = null;
    }

    private static async Task WatchConsoleResize(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            int currentHeight = Console.WindowHeight;
            int currentWidth = Console.WindowWidth;

            if (currentHeight != _lastWindowHeight || currentWidth != _lastWindowWidth)
            {
                _lastWindowHeight = currentHeight;
                _lastWindowWidth = currentWidth;
                
                ConsoleResized?.Invoke();
            }

            await Task.Delay(250, token);
        }
    }
}