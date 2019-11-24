using System;
using System.Collections.Generic;
using FoodMe.Core;

namespace FoodMe
{
    internal class Policies : IDisposable
    {
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
            throw new NotImplementedException();
        }

        internal void Start()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
