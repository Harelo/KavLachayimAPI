using Microsoft.AspNetCore.Mvc;
using System;
using KavLachayimAPI.Services;
using System.Linq;
using System.Data.SQLite;

namespace KavLachayimAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/accountlogin")]
    public class AccountLoginController : Controller
    {
        private SQLiteConnection accountsDBConnection;
        [HttpGet]
        public string Login(string username, string password)
        {
            try
            {
                string DBFilePath = Environment.CurrentDirectory + @"\Data\AccountsDB.db3";
                accountsDBConnection = new SQLiteConnection("Data Source=" + DBFilePath + ";Version=3");
                object result = DBService.ExecuteSQLScalar(String.Format(
                    @"SELECT UserPassword
                      FROM Accounts
                      WHERE Username='{0}'", username), accountsDBConnection);
                if (result != null && result.ToString() != "")
                {
                    string userEncryptedPassword = result.ToString();
                    if (TextService.Encrypt(password) == userEncryptedPassword)
                    {
                        string token = AddAccessTokenToUser(username);
                        return token;
                    }
                    else return "incorrect";
                }
                else
                    return "notfound";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("An exception occured: " + ex.Message);
                return "error";
            }
        }

        private string AddAccessTokenToUser(string username)
        {
            Random rnd = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890123456789";
            string token = new string(Enumerable.Repeat(chars, 50).Select(s => s[rnd.Next(s.Length)]).ToArray());
            DBService.ExecuteSQLScalar(String.Format(
                @"UPDATE Accounts
                  SET AccessToken = '{0}'
                  WHERE Username = '{1}'", token, username), accountsDBConnection);
            return token;
        }
    }
}