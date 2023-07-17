using GateKeeper.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace GateKeeper.Hashers
{
    public class HMAC_SHA256Hasher<TUser> : IPasswordHasher<TUser> where TUser : IdentityUser, IUser<Guid>
    {
        #region Constants
        private const string SALT = "SBS_4157";
        private const KeyDerivationPrf KEY_DERIVATION = KeyDerivationPrf.HMACSHA256;
        private const int ITER_COUNT = 1000;
        private const int SUB_KEY_LENGTH = 256 / 8;
        #endregion

        #region Fields
        private int saltSize = Encoding.ASCII.GetByteCount(SALT);
        #endregion

        #region Public Methods
        public string HashPassword(TUser user, string password)
        {
            try
            {
                byte[] salt = Encoding.ASCII.GetBytes(SALT);
                byte[] subKey = KeyDerivation.Pbkdf2(password, salt, KEY_DERIVATION, ITER_COUNT, SUB_KEY_LENGTH);
                var outputBytes = new byte[1 + saltSize + SUB_KEY_LENGTH];
                outputBytes[0] = 0x00;
                Buffer.BlockCopy(salt, 0, outputBytes, 1, saltSize);
                Buffer.BlockCopy(subKey, 0, outputBytes, 1 + saltSize, SUB_KEY_LENGTH);

                return Convert.ToBase64String(outputBytes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            try
            {
                if (hashedPassword == null)
                {
                    throw new ArgumentNullException(nameof(hashedPassword));
                }
                if (providedPassword == null)
                {
                    throw new ArgumentNullException(nameof(providedPassword));
                }

                byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

                if (decodedHashedPassword.Length == 0)
                {
                    return PasswordVerificationResult.Failed;
                }

                if (hashedPassword == HashPassword(user, providedPassword))
                    return PasswordVerificationResult.Success;
                else
                    return PasswordVerificationResult.Failed;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
