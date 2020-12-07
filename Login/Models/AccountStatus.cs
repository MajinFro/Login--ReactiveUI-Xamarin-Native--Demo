using System;
namespace Login.Models
{
    public enum Status
    {
        undefined,
        success,
        error
    }

    public class AccountStatus
    {
        public Status Status { get; set; }
        public string Message { get; set; }

        public AccountStatus(Status status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
    }
}
