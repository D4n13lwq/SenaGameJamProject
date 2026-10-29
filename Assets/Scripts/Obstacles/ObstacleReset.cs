using System;
using UnityEngine;

public static class ObstacleReset
{
    public static event Action OnReset;

    public static void ResetAll()
    {
        OnReset?.Invoke();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ClearOnPlay()
    {
        OnReset = null;
    }
}
