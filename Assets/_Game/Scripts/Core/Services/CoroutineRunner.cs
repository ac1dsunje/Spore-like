using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Core.Services
{
public class CoroutineRunner : MonoBehaviour
{
    private readonly Dictionary<(object owner, string coroutineKey), Coroutine> _coroutines = new();

    public Coroutine Run(object owner, string coroutineKey, IEnumerator routine)
    {
        if (!gameObject.activeInHierarchy)
        {
            return null;
        }

        Stop(owner, coroutineKey);
        var coroutine = StartCoroutine(RunAndCleanUp(owner, coroutineKey, routine));
        _coroutines[(owner, coroutineKey)] = coroutine;
        return coroutine;
    }

    private IEnumerator RunAndCleanUp(object owner, string coroutineKey, IEnumerator routine)
    {
        yield return routine;
        _coroutines.Remove((owner, coroutineKey));
    }

    public void Stop(object owner, string coroutineKey)
    {
        var key = (owner, coroutineKey);
        if (_coroutines.TryGetValue(key, out var coroutine))
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            _coroutines.Remove(key);
        }
    }

    public void Stop(object owner)
    {
        var keysToRemove = new List<(object owner, string coroutineKey)>();
        foreach (var key in _coroutines.Keys)
        {
            if (key.owner == owner)
            {
                keysToRemove.Add(key);
            }
        }

        foreach (var key in keysToRemove)
        {
            Stop(key.owner, key.coroutineKey);
        }
    }

    public void StopAll()
    {
        StopAllCoroutines();
        _coroutines.Clear();
    }

    public bool IsRunning(object owner, string coroutineKey)
    {
        return _coroutines.ContainsKey((owner, coroutineKey));
    }

    private void OnDestroy()
    {
        StopAll();
    }
}
}