﻿using System;
using System.Collections.Generic;

namespace BusinessLight.Domain.Application
{
    public interface IApplicationUser<TLogin, TRole, TClaim> : IEntity
        where TLogin : IApplicationUserLogin
        where TRole : IApplicationUserRole
        where TClaim : IApplicationUserClaim
    {
        string UserName
        {
            get;
            set;
        }

        string Language { get; set; }

        /// <summary>Email</summary>
        string Email { get; set; }

        /// <summary>True if the email is confirmed, default is false</summary>
        bool EmailConfirmed { get; set; }

        /// <summary>The salted/hashed form of the user password</summary>
        string PasswordHash { get; set; }

        /// <summary>
        ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        string SecurityStamp { get; set; }

        /// <summary>PhoneNumber for the user</summary>
        string PhoneNumber { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        bool PhoneNumberConfirmed { get; set; }

        /// <summary>Is two factor enabled for the user</summary>
        bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>Is lockout enabled for this user</summary>
        bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        int AccessFailedCount { get; set; }

        /// <summary>Navigation property for user roles</summary>
        ICollection<TRole> Roles { get; }

        /// <summary>Navigation property for user claims</summary>
        ICollection<TClaim> Claims { get; }

        /// <summary>Navigation property for user logins</summary>
        ICollection<TLogin> Logins { get; }

    }
}
