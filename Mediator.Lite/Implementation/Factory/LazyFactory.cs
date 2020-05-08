using System;
using System.Runtime.CompilerServices;
using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Implementation.Factory
{
    public struct LazyFactory<T> : IFactory<T>
    {
        private Lazy<T> _lazy;

        public LazyFactory(Lazy<T> lazy) => _lazy = lazy;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Create() => _lazy.Value;
    }
}