using System;

namespace FutureFlag.Internal
{
    internal class Cached<T>
    {
        private readonly Func<T> _factory;
        private readonly TimeSpan _lifetime;
        private readonly object _valueLock = new object();

        private T _value;
        private bool _hasValue;
        private DateTime _creationTime;

        public Cached(Func<T> factory, TimeSpan lifetime)
        {
            _factory = factory;
            _lifetime = lifetime;
        }

        public T Value
        {
            get
            {
                var now = DateTime.Now;
                lock (_valueLock)
                {
                    if (_hasValue)
                    {
                        if (_creationTime.Add(_lifetime) < now)
                        {
                            _hasValue = false;
                        }
                    }
                    if(_hasValue) return _value;
                    
                    _value = _factory();
                    _hasValue = true;
                    _creationTime = DateTime.Now;

                    return _value;
                }
            }
        }
    }
}