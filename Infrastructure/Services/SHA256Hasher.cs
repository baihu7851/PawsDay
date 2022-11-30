using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SHA256Hasher : IAppPasswordHasher
    {
        public string HashPasseword(string password)
        {
            return password.ToSHA256();
        }
    }
}
