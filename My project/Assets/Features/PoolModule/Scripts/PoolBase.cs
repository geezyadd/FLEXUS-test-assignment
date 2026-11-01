using System.Collections.Generic;

namespace Features.PoolModule.Scripts
{
    public abstract class PoolBase<T> where T : class
    {
        private readonly Queue<T> _free = new();
        private readonly List<T> _active = new();
        private readonly List<T> _all = new();

        protected IReadOnlyList<T> Active => _active;
        protected IReadOnlyList<T> All => _all;
        protected abstract int MaxSize { get; }

        protected void Prewarm(int count)
        {
            for (int i = 0; i < count && _all.Count < MaxSize; i++)
                _free.Enqueue(CreateItem());
        }

        protected T Get()
        {
            T item = GetFree();
            _active.Add(item);
            OnGet(item);
            return item;
        }

        protected void Release(T item)
        {
            if (_active.Remove(item) == false)
                return;

            OnRelease(item);
            _free.Enqueue(item);
        }

        protected void Clear()
        {
            _free.Clear();
            _active.Clear();
            _all.Clear();
        }

        protected abstract T Create();

        protected virtual void OnGet(T item)
        {
        }

        protected virtual void OnRelease(T item)
        {
        }

        private T GetFree()
        {
            if (_free.Count > 0)
                return _free.Dequeue();

            if (_all.Count < MaxSize)
                return CreateItem();

            Release(_active[GetRecycleIndex()]);
            return _free.Dequeue();
        }

        protected virtual int GetRecycleIndex() => 0;

        private T CreateItem()
        {
            T item = Create();
            _all.Add(item);
            return item;
        }
    }
}
