using System.Collections.Generic;

namespace Somi.OpenTK
{
    internal abstract class Cache<UnloadedType, LoadedType>
    {
        protected readonly Dictionary<UnloadedType, LoadedType> loaded = new Dictionary<UnloadedType, LoadedType>();

        public virtual LoadedType Load(UnloadedType obj)
        {
            if (loaded.TryGetValue(obj, out var v))
                return v;

            v = CreateNew(obj);
            loaded.Add(obj, v);
            return v;
        }

        protected abstract LoadedType CreateNew(UnloadedType raw);

        protected abstract void DisposeOf(LoadedType loaded);

        public void Unload(UnloadedType obj)
        {
            if (!loaded.TryGetValue(obj, out var loadedObj))
                throw new System.Exception($"Attempt to unload a(n) {typeof(UnloadedType).Name} that isn't loaded");

            DisposeOf(loadedObj);
            loaded.Remove(obj);
        }

        public bool Has(UnloadedType obj) => loaded.ContainsKey(obj);

        public void UnloadAll()
        {
            foreach (var entry in loaded)
                DisposeOf(entry.Value);
            loaded.Clear();
        }
    }
}
