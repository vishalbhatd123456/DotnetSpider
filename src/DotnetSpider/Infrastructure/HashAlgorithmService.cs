using System;
using System.Security.Cryptography;
using System.Threading;

namespace DotnetSpider.Infrastructure
{
    public abstract class HashAlgorithmService : IHashAlgorithmService
    {
        private static readonly ThreadLocal<HashAlgorithm> _threadLocalHashAlgorithm = new ThreadLocal<HashAlgorithm>(() => null);

        protected abstract HashAlgorithm CreateHashAlgorithm();

        public byte[] ComputeHash(byte[] bytes)
        {
            var hashAlgorithm = _threadLocalHashAlgorithm.Value ?? (_threadLocalHashAlgorithm.Value = CreateHashAlgorithm());

            lock (hashAlgorithm) // Assuming hashAlgorithm is not inherently thread-safe
            {
                return hashAlgorithm.ComputeHash(bytes);
            }
        }
    }
}