using System;
using Login.Models;

namespace Login.Services
{
    public interface IUserDialogs
    {
        void ShowDialog(AccountStatus accountStatus);
    }
}
