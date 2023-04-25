﻿namespace ServerCore;

public interface IJobQueue
{
    void Push(Action job);
}

public class JobQueue : IJobQueue
{
    private Queue<Action> _jobQueue = new Queue<Action>();
    private object _lock = new object();
    private bool _flush = false;


    public void Push(Action job)
    {
        bool flush = false;

        lock (_lock)
        {
            _jobQueue.Enqueue(job);
            if (_flush == false)
                flush = _flush = true;
        }

        if (flush)
            Flush();
    }

    void Flush()
    {
        while (true)
        {
            Action action = Pop();
            if (action == null)
                return;
            
            action.Invoke();
        }
    }

    Action Pop()
    {
        lock (_lock)
        {
            if (_jobQueue.Count == 0)
            {
                _flush = false;
                return null;
            }

            return _jobQueue.Dequeue();
        }
    }
}