using System;
using Havit.Diagnostics.Contracts;

namespace Havit.GoranG3.Facades.Infrastructure.Security.Claims
{
    public class UserContextInfo
    {
        private readonly string username;

        public string Username => username;

        public UserContextInfo(string username)
        {
            this.username = username;
        }

        public override int GetHashCode()
        {
            return username.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            UserContextInfo userContextInfoObj = (UserContextInfo)obj;
            if (userContextInfoObj == null)
            {
                return false;
            }

            return this.username == userContextInfoObj.username;
        }
    }
}