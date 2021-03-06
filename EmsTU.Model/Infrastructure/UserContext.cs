﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using EmsTU.Model.Models;

namespace EmsTU.Model.Infrastructure
{
    public class UserContext
    {
        private int userId;
        private string fullName;
        private bool hasPassword;
        private string[] permissions;

        public UserContext(User user)
        {
            this.userId = user.UserId;
            this.fullName = user.Fullname;
            this.permissions = user.Role.Permissions.Split(',').Select(s => s.Trim()).ToArray();
        }

        public UserContext(int userId, string fullName, string[] permissions)
        {
            this.fullName = fullName;
            this.userId = userId;
            this.permissions = permissions;
        }

        public int UserId
        {
            get
            {
                return this.userId;
            }
        }

        public string FullName
        {
            get
            {
                if (this.fullName == null)
                {
                    return "";
                }

                return this.fullName;
            }
        }

        public string[] Permissions
        {
            get
            {
                return this.permissions;
            }
        }

        public bool Can(string action, string permissionObject)
        {
            return this.permissions.Contains(permissionObject + "#*") ||
                this.permissions.Contains(permissionObject + "#" + action);
        }

        public bool Can(string action, string[] permissionObjects)
        {
            if (permissionObjects.Length == 0)
            {
                return false;
            }

            foreach (var permissionObject in permissionObjects)
            {
                if (!this.Can(action, permissionObject))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanAll(string permissionObject)
        {
            return this.permissions.Contains(permissionObject + "#*");
        }

        public void Assert(string action, string permissionObject)
        {
            if (!this.Can(action, permissionObject))
            {
                throw new SecurityException("Access denied - insufficient privileges.");
            }
        }

        public void Assert(string action, string[] permissionObjects)
        {
            if (!this.Can(action, permissionObjects))
            {
                throw new SecurityException("Access denied - insufficient privileges.");
            }
        }
    }
}
