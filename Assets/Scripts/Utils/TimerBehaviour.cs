using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveTimer
{
    public ActiveTimer(GameObject parent, Action onTimerEnd, float time)
    {
        var timer = parent.AddComponent<TimerBehaviour>();
        timer.duration = time;
        timer.OnTimerEnd += onTimerEnd;
    }
    public class TimerBehaviour : MonoBehaviour
    {
        public float duration = 1f;
        public event Action OnTimerEnd;

        private Timer timer;

        private void Start()
        {
            // Create a new timer and initialise it
            timer = new Timer(duration);

            // Subscribe to the OnTimerEnd event to be able to handle that scenario
            timer.OnTimerEnd += HandleTimerEnd;
        }

        private void HandleTimerEnd()
        {
            Debug.Log("Firing");
            // Alert any listeners that the timer has ended
            OnTimerEnd?.Invoke();

            // Remove this component
            Destroy(this);
        }

        private void Update() => timer.Tick(Time.deltaTime);
    }

}
