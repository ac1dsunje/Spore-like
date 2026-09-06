using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Core.Services
{
public class CoroutineRunner : MonoBehaviour
{
    private readonly Dictionary<object, Coroutine> _coroutines = new();

    public Coroutine Run(object key, IEnumerator routine)
    {
        if (!gameObject.activeInHierarchy)
        {
            return null;
        }

        Stop(key);
        var coroutine = StartCoroutine(RunAndCleanUp(key, routine));
        _coroutines[key] = coroutine;
        return coroutine;
    }

    private IEnumerator RunAndCleanUp(object key, IEnumerator routine)
    {
        yield return routine;
        _coroutines.Remove(key);
    }

    public Coroutine Run(IEnumerator routine)
    {
        return !gameObject.activeInHierarchy ? null : StartCoroutine(routine);
    }

    public void Stop(object key)
    {
        if (_coroutines.TryGetValue(key, out var coroutine))
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            _coroutines.Remove(key);
        }
    }

    public void Stop(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    public void StopAll()
    {
        StopAllCoroutines();
        _coroutines.Clear();
    }

    public bool IsRunning(object key)
    {
        return _coroutines.ContainsKey(key);
    }
}
}