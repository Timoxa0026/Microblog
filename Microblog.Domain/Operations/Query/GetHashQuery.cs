namespace Microblog.Domain.Operations.Query
{
    #region << Using >>

    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Incoding.CQRS;

    #endregion

    internal class GetHashQuery : QueryBase<string>
    {
        #region Properties

        public string Value { get; set; }

        #endregion

        protected override string ExecuteResult()
        {
            if (string.IsNullOrWhiteSpace(Value))
                return null;

            var hash = string.Empty;
            byte[] hashBytes;
            var bytes = Encoding.UTF8.GetBytes(Value);

            using (SHA512 shaM = new SHA512Managed())
                hashBytes = shaM.ComputeHash(bytes);

            return hashBytes.Aggregate(hash, (current, b) => current + String.Format("{0:x2}", b));
        }
    }
}