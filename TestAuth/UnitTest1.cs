using OpenFlowCRMModels;
using OpenFlowCRMModels.Models;
using Konscious.Security.Cryptography;
using System.Text;

namespace TestAuth
{
    [TestClass]
    public class UnitTest1
    {

        private long _key = 9999999;

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

        [TestMethod]
        public void TestInsertClienti()
        {

            var clienteTest = new Clienti();

            clienteTest.Nome= "pippo";

            // Add the user to the Users table in the database
            using (var db = new SQL_TESTContext())
            {
                db.Clienti.Add(clienteTest);
                db.SaveChanges();
            }
            _key = clienteTest.Idcliente;
            Thread.Sleep(500);
            using (var db = new SQL_TESTContext())
            {
                var ordineTrovato = db.Clienti.Find(clienteTest.Idcliente);

                Assert.IsNotNull(ordineTrovato);
            }
        }

        //[TestMethod]
        //public void TestInsertClienti()
        //{

        //    var clienteTest = new Clienti();

        //    clienteTest.Nome= "pippo";

        //    // Add the user to the Users table in the database
        //    using (var db = new SQL_TESTContext())
        //    {
        //        db.Clienti.Add(clienteTest);
        //        db.SaveChanges();
        //    }
        //    _key = clienteTest.Idcliente;
        //    Thread.Sleep(500);
        //    using (var db = new SQL_TESTContext())
        //    {
        //        var ordineTrovato= db.Clienti.Find(clienteTest.Idcliente);

        //        Assert.IsNotNull(ordineTrovato);
        //    }
        //}
        [TestCleanup] 
        public void Cleanup()
        {
            using (var db = new SQL_TESTContext())
            {
                var ordineTrovato = db.Clienti.Find(_key);

                if(ordineTrovato != null)
                    db.Clienti.Remove(ordineTrovato);

            }
        }
    }
}