using System;


namespace Application.Interfaces
{
    public interface ICacheService
    {
        public T Get<T>(string key);

        public T Set<T>(
            string key,
            T value,
            TimeSpan? absoluteExpiration = null,
            TimeSpan? slidingExpiration = null
        );

        public void Remove(string key);
    }

}
