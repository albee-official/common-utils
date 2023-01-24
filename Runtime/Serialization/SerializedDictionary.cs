using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using CommonUtils.Attributes;

namespace CommonUtils.Serialization
{
    /// <summary> Serialized Dictionary... What else can I say? </summary>
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [Serializable]
        public class Pair
        {
            public TKey key;
            public TValue value;

            public static implicit operator KeyValuePair<TKey, TValue>(Pair pair)
            {
                return new KeyValuePair<TKey, TValue>(pair.key, pair.value);
            }

            public static implicit operator Pair(KeyValuePair<TKey, TValue> pair)
            {
                return new Pair
                {
                    key = pair.Key,
                    value = pair.Value
                };
            }
        }

        [SerializeField] [ReadOnly] private List<Pair> entries = new List<Pair>();

        public SerializedDictionary() {
        }

        public SerializedDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) {
        }

        public void OnBeforeSerialize() {
            #if UNITY_EDITOR

            entries.Clear();
            foreach (var pair in this)
                entries.Add(pair);

            #endif
        }

        public void OnAfterDeserialize() {
            #if UNITY_EDITOR

            Clear();
            foreach (var entry in entries)
                this[entry.key] = entry.value;

            #endif
        }
    }
}