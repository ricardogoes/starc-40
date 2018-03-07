using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.Models;
using System;
using System.Security.Cryptography;

namespace STARC.Application
{
    public class PasswordAppService : IPasswordAppService
    {
        private readonly IPasswordRepository __repository;

        public PasswordAppService(IPasswordRepository repository)
        {
            __repository = repository;
        }

        public HashPassword SetCriptoPassword(string password)
        {
            try
            {
                // generate a 128-bit salt using a secure PRNG
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                return new HashPassword { Password = hashedPassword, Salt = salt };
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public string GetCriptoPassword(string password, byte[] salt)
        {
            if (string.IsNullOrEmpty(password) || salt == null)
                throw new ArgumentException("Invalid parameters");

            try
            {
                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                return hashedPassword;
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public byte[] GetHashPassword(long userId)
        {
            try
            {
                return __repository.GetHashPassword(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
