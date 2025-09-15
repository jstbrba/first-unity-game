using System;
namespace Utilities
{
    public class Observable<T>
    {
        private T _value;
        public event Action<T, T> onValueChanged;
        public event Action onChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(value, _value))
                {
                    var old = _value;
                    _value = value;
                    onValueChanged?.Invoke(old, value);
                    onChanged?.Invoke();
                }
            }
        }
    }
}