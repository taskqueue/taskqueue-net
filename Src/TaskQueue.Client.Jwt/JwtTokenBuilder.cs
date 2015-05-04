using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;

namespace TaskQueue.Client.Jwt
{
    public sealed class JwtTokenBuilder
    {
        string _issuer;
        string _audience = "all";
        byte[] _symmetricKey;
        List<Claim> _claims = new List<Claim>();
        DateTime _issuedAt = DateTime.UtcNow;
        TimeSpan _expiresIn = TimeSpan.FromMinutes(60);
        string _signatureAlgorithim = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
        string _digestAlgorithim = "http://www.w3.org/2001/04/xmlenc#sha256";

        /// <summary>
        /// Build and return the token.
        /// </summary>
        /// <returns>The token that was created.</returns>
        public string Build()
        {
            var signingCredentials = new SigningCredentials(
                new InMemorySymmetricSecurityKey(_symmetricKey),
                _signatureAlgorithim,
                _digestAlgorithim);

            var token = new JwtSecurityToken(
                _issuer, 
                _audience, 
                _claims, 
                _issuedAt,
                _issuedAt.Add(_expiresIn), 
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Sets the issuer of the token.
        /// </summary>
        /// <param name="issuer">The issuer of the token.</param>
        /// <returns>The token builder to continue building upon.</returns>
        public JwtTokenBuilder Issuer(string issuer)
        {
            _issuer = issuer;
            return this;
        }

        /// <summary>
        /// Sets the audience of the token.
        /// </summary>
        /// <param name="audience">The audience of the token.</param>
        /// <returns>The token builder to continue building upon.</returns>
        public JwtTokenBuilder Audience(string audience)
        {
            _audience = audience;
            return this;
        }

        /// <summary>
        /// Sets the base64 secret of the token.
        /// </summary>
        /// <param name="base64Secret">The Base64 encoded secure of the token.</param>
        /// <returns>The token builder to continue building upon.</returns>
        public JwtTokenBuilder Secret(string base64Secret)
        {
            var text = base64Secret.Replace('-', '+').Replace('_', '/');

            var count = 3 - (text.Length + 3) % 4;

            if (count > 0)
            {
                text += new string('=', count);
            }

            _symmetricKey = Convert.FromBase64String(text);

            return this;
        }

        /// <summary>
        /// Sets the time that the token was issued at.
        /// </summary>
        /// <param name="issuedAt">The time that the token was issued at.</param>
        /// <returns>The token builder to continue building upon.</returns>
        public JwtTokenBuilder IssueAt(DateTime issuedAt)
        {
            _issuedAt = issuedAt;
            return this;
        }

        /// <summary>
        /// Sets the time past the issued time that the token expires at.
        /// </summary>
        /// <param name="expiresIn">The time that the token will expire.</param>
        /// <returns>The token builder to continue building upon.</returns>
        public JwtTokenBuilder ExpiresIn(TimeSpan expiresIn)
        {
            _expiresIn = expiresIn;
            return this;
        }

        /// <summary>
        /// Adds the list of claims to the token.
        /// </summary>
        /// <param name="claims">The claims to add to the token.</param>
        /// <returns>The token builder to continue building upon.</returns>
        public JwtTokenBuilder Claims(params Claim[] claims)
        {
            _claims.AddRange(claims ?? Enumerable.Empty<Claim>());
            return this;
        }

        /// <summary>
        /// Sets the signature algorithim to sign the token with.
        /// </summary>
        /// <param name="algorithim">The signature algorithim of the token.</param>
        /// <returns>The token builder to continue building upon.</returns>
        public JwtTokenBuilder SignatureAlgorithim(string algorithim)
        {
            _signatureAlgorithim = algorithim;
            return this;
        }

        /// <summary>
        /// Sets the digest algorithim to sign the token with.
        /// </summary>
        /// <param name="algorithim">The digest algorithim of the token.</param>
        /// <returns>The token builder to continue building upon.</returns>
        public JwtTokenBuilder DigestAlgorithim(string algorithim)
        {
            _digestAlgorithim = algorithim;
            return this;
        }
    }
}
