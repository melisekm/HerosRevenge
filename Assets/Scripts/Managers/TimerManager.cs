using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    private ArrayList timers = new();
    private static Stack<Timer> removedTimers = new(); 


    public void AddTimer(Action callback, float duration = 1f)
    {
        var timer = new Timer(duration);
        timer.OnTimerEnd += callback;
        timer.OnTimerEnd += () => RemoveTimer(timer);
        timers.Add(timer);
    }

    public void RemoveTimer(Timer timer)
    {
        removedTimers.Push(timer);
    }

    private void Update()
    {
        foreach (Timer timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
        while (removedTimers.Count != 0 ) {
            Timer t = removedTimers.Pop();
            timers.Remove(t);
        }
    }
}