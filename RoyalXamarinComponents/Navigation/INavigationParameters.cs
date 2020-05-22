using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RoyalXamarinComponents.Navigation {
    public interface INavigationParameters : IEnumerable<KeyValuePair<string, object>> { // IDictionary<string, object>, IEnumerable {
        int Count { get; }

        IEnumerable<string> Keys { get; }

        object this[string key] { get; }

        void Add(string key, object value);

        bool ContainsKey(string key);

        T GetValue<T>(string key);

        T GetValueOrDefault<T>(string key, T defaultValue);

        IEnumerable<T> GetValues<T>(string key);

        IEnumerable<T> GetValuesOrDefault<T>(string key, IEnumerable<T> defaultValue);

        bool TryGetValue<T>(string key, out T value);
    }

    public class NavigationParameters : INavigationParameters {
        private List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();

        public object this[string key] => list.Any(w=> w.Key == key) ? list.First(w => w.Key == key).Value : default(object);

        public int Count => list.Count();

        public IEnumerable<string> Keys => list.Select(w => w.Key);

        public void Add(string key, object value) {
            list.Add(new KeyValuePair<string, object>(key, value));
        }

        public bool ContainsKey(string key) {
            return list.Any(w => w.Key == key);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return list.GetEnumerator();
        }

        public T GetValue<T>(string key) {
            return (T)this[key];
        }

        public T GetValueOrDefault<T>(string key, T defaultValue) {
            return ContainsKey(key) && this[key] is T ? (T)this[key] : defaultValue;
        }

        public IEnumerable<T> GetValues<T>(string key) {
            return list.Where(w => w.Key == key).Select(x => x.Value).Cast<T>();
        }

        public IEnumerable<T> GetValuesOrDefault<T>(string key, IEnumerable<T> defaultValue) {
            var result = list.Where(w => w.Key == key).Select(x => x.Value).Cast<T>();
            return result.Any() ? result : defaultValue;
        }

        public bool TryGetValue<T>(string key, out T value) {
            if (!list.Any(w => w.Key == key)) {
                value = default(T);
                return false;
            }

            try {
                value = (T)list.First(w => w.Key == key).Value;
                return true;
            }
            catch (Exception ex) {
                value = default(T);
                return false;
            }
        }
    }
}