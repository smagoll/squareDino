using System;

public static class GameEvents
{
    public static event Action OnLose;
    public static event Action OnComplete;
    
    public static void RaiseLose() => OnLose?.Invoke();
    public static void RaiseComplete() => OnComplete?.Invoke();
}