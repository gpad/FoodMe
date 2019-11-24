using System;
using System.Collections.Generic;
using FoodMe.Core;

namespace FoodMe
{
    internal class Policies : IDisposable
    {
        List<Policy> policies = new List<Policy>();

        public Policies()
        {
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        internal void Add(IEnumerable<Policy> policies)
        {
            this.policies.AddRange(policies);
        }

        internal void Start()
        {
            this.policies.ForEach(policy => policy.Start());
        }
        #endregion
    }
}
