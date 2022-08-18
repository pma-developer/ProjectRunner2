using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Utils
{
    [Serializable]
    public class ReadonlyRuntimeDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey,TValue>>
    {
        [SerializeField] private List<Pair> _pairsList;
        private Dictionary<TKey, TValue> _actualDictionary;

        private void InitDictionary()
        {
            if (_actualDictionary is not null)
            {
                Debug.LogError("Dictionary is already initialized!");
                return;
            }

            _actualDictionary = new Dictionary<TKey, TValue>();
            foreach (var pair in _pairsList)
            {
                _actualDictionary.Add(pair.Key, pair.Value);
            }
        }

        public TValue GetValue(TKey projectileType)
        {
            if (_actualDictionary is null)
            {
                InitDictionary();
            }


            return _actualDictionary[projectileType];
        }

        [Serializable]
        private class Pair
        {
            [SerializeField] internal TKey Key;
            [SerializeField] internal TValue Value;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (_actualDictionary is null)
            {
                InitDictionary();
            }

            return _actualDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}