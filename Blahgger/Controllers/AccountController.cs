using Blahgger.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;

namespace Blahgger.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            // string hashedPassword = BCrypt.Net.BCrypt.HashPassword("password", 12);

            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser user)
        {
            // Did the user provide values?
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Password"))
            {
                // TODO get the user record from DB.
                string connectionString = ConfigurationManager.ConnectionStrings["BlahggerDatabase"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"
                            SELECT HashedPassword
                            FROM [User]
                            WHERE Email = @Email
                        ";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    SqlDataReader reader = command.ExecuteReader();

                    string hashedPassword = null;

                    while (reader.Read())
                    {
                        hashedPassword = reader.GetString(0);
                    }

                    // Check if the password matches.
                    if (string.IsNullOrEmpty(hashedPassword) || 
                        !BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword))
                    {
                        // Failed to find a record with the provided email.
                        ModelState.AddModelError("", "Login failed.");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                // Login the User.
                FormsAuthentication.SetAuthCookie(user.Email, false);

                // Send to homepage.
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Register(RegisterUser user)
        {
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("FirstName")
                && ModelState.IsValidField("LastName") && ModelState.IsValidField("Password"))
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, 12);

                string connectionString = ConfigurationManager.ConnectionStrings["BlahggerDatabase"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"
                            INSERT INTO [User] (Email, FirstName, LastName, HashedPassword)
                            VALUES (@Email, @FirstName, @LastName, @HashedPassword)
                        ";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    command.ExecuteNonQuery();
                }
            }

            if (ModelState.IsValid)
            {
                // Login the User.
                FormsAuthentication.SetAuthCookie(user.Email, false);

                // Send to homepage.
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
