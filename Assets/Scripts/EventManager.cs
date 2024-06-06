using System;

public static class EventManager
{
    public static event Action<JoinableView> OnPlayerGetDamage;
    public static event Action OnLose;
    public static event Action OnWin;

    public static void AddPlayerDamage(JoinableView viewToRemove)
    {
        OnPlayerGetDamage?.Invoke(viewToRemove);
    }

    public static void Lose()
    {
        OnLose?.Invoke();
    }

    public static void Win()
    {
        OnWin?.Invoke();
    }
}