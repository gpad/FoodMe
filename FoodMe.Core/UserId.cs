using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodMe.Core
{
    public class UserId : IEquatable<UserId>
    {
        private Guid guid;

        public static UserId New()
        {
            return new UserId(Guid.NewGuid());
        }

        protected UserId(Guid guid)
        {
            this.guid = guid;
        }

        public bool Equals([AllowNull] UserId other)
        {
            return other != null && guid == other.guid;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as UserId);
        }

        public override int GetHashCode()
        {
            return guid.GetHashCode();
        }
    }
}
