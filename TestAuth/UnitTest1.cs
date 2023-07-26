using OpenFlowCRMModels;
using OpenFlowCRMModels.Models;
using Konscious.Security.Cryptography;
using System.Text;

namespace TestAuth
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddUser()
        {

            var hashAlgorithm = new HMACBlake2B(512);
            hashAlgorithm.Initialize();

            var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes("admin"));

            var utenti = new Utenti();

            utenti.Username = "admin";
            utenti.PasswordHash = Convert.ToBase64String(hash);

            // Add the user to the Users table in the database
            using (var db = new SQL_TESTContext())
            {
                db.Utenti.Add(utenti);
                db.SaveChanges();
            }
        }
    }
}