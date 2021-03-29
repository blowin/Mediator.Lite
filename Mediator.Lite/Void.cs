using System;

namespace Mediator.Lite
{
    public sealed class Void : IEquatable<Void>
    {
        public static Void Instance => new Void();

        private Void()
        {
        }

        public bool Equals(Void other) => other != null;

        public override bool Equals(object obj) => obj is Void v && Equals(v);

        public override int GetHashCode() => 2151125;

        public override string ToString() => "Void";
    }
}