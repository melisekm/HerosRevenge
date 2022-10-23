using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    private List<Timer> timers = new List<Timer>();

    public void AddTimer(Action callback, float duration = 1f)
    {
        var timer = new Timer(duration);
        timer.OnTimerEnd += callback;
        timers.Add(timer);
    }

    public void RemoveTimer(Timer timer)
    {
        timers.Remove(timer);
    }

    private void Update()
    {
        foreach (var t in timers)
        {
            t.Tick(Time.deltaTime);
        }
    }
}