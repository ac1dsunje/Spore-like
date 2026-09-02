using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
    public class CoroutineRunner : MonoBehaviour
    {
        private readonly Dictionary<object, Coroutine> _coroutines = new();

        public Coroutine Run(object key, IEnumerator routine)
        {
            Stop(key);
            var coroutine = StartCoroutine(routine);
            _coroutines[key] = coroutine;
            return coroutine;
        }
        
        public Coroutine Run(IEnumerator routine)
        {
            return StartCoroutine(routine);
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