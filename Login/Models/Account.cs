using System;
using Newtonsoft.Json;

namespace Login.Models
{
    public class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public string ToJson()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);
                return json;
            }
            catch
            {
                return "";
            }
        }

        public static Account FromJson(string json)
        {
            try
            {
                Account retVal = JsonConvert.DeserializeObject<Account>(json);
                return retVal;
            }
            catch
            {
                return null;
            }
        }
    }
}
