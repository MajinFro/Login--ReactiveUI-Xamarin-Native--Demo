using System;
using Xamarin.Essentials;
using Login.Models;

namespace Login.Services
{
    public interface IAccountService
    {
        AccountStatus SignIn(string username, string password);
        AccountStatus SignUp(Account account);
    }
    
    public class AccountService : IAccountService
    {
        public AccountStatus SignIn(string username, string password)
        {
            string json = Preferences.Get(username, "");
            if (String.IsNullOrEmpty(json)) { return new AccountStatus(Status.error, "Account Does Not Exist"); }

            Account account = Account.FromJson(json);
            if (account == null) { return new AccountStatus(Status.error, "Account Does Not Exist"); }

            bool success = account.Password == password;
            Status status = success ? Status.success : Status.error;
            string message = success ? "" : "Password Is Incorrect";
            return new AccountStatus(status, message);
        }

        public AccountStatus SignUp(Account account)
        {
            if(Preferences.ContainsKey(account.Username)) { return new AccountStatus(Status.error, "Account Already Exists"); }

            string json = account.ToJson();
            if(string.IsNullOrEmpty(json)) { return new AccountStatus(Status.error, "Error Serializing The Account"); }

            Preferences.Set(account.Username, json);
            return new AccountStatus(Status.success, "");
        }
    }
}
