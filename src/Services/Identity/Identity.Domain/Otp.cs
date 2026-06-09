using System;

namespace Identity.Domain
{
    /// <summary>
    /// Represents a one-time password (OTP) issued for a user.
    /// Stored in the database so attempts, expiry and usage can be audited and enforced.
    /// </summary>
    public class Otp
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The numeric/alphanumeric code issued to the user.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// The absolute time when the OTP becomes invalid.
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// When the OTP was created (UTC).
        /// </summary>
        public DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Whether the OTP was consumed or locked out due to too many attempts.
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// Number of failed verification attempts recorded for this OTP.
        /// </summary>
        public int Attempts { get; set; }

        /// <summary>
        /// Maximum allowed failed attempts before the OTP is invalidated.
        /// </summary>
        public int MaxAttempts { get; set; } = 5;

        /// <summary>
        /// FK to the application user owning this OTP.
        /// </summary>
        public string ApplicationUserId { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property for the user.
        /// </summary>
        public ApplicationUser? ApplicationUser { get; set; }

        /// <summary>
        /// True when current time is past the <see cref="Expires"/> time.
        /// </summary>
        public bool IsExpired => DateTime.UtcNow >= Expires;

        /// <summary>
        /// Validates the provided code against this OTP.
        /// Returns false if the OTP is expired or already used.
        /// </summary>
        /// <param name="code">Code provided by the user.</param>
        /// <returns>True if the code is valid and unused.</returns>
        public bool IsValidCode(string code) => !IsUsed && !IsExpired && string.Equals(Code, code, StringComparison.Ordinal);

        /// <summary>
        /// Register an attempt to verify the OTP.
        /// If <paramref name="successful"/> is true the OTP is marked used.
        /// Otherwise the Attempts counter is incremented and the OTP is marked
        /// used (locked out) when Attempts >= MaxAttempts.
        /// </summary>
        /// <param name="successful">True when the verification succeeded.</param>
        public void RegisterAttempt(bool successful)
        {
            if (successful)
            {
                IsUsed = true;
                return;
            }

            Attempts++;
            if (Attempts >= MaxAttempts)
            {
                // Lock out the OTP to prevent brute force attempts
                IsUsed = true;
            }
        }
    }
}
