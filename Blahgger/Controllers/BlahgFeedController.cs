using Blahgger.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blahgger.Controllers
{
    public class BlahgFeedController : Controller
    {
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlahggerDatabase"].ConnectionString;
            List<BlahgPost> blahgPosts = new List<BlahgPost>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"
                        SELECT BlahgPost.Id, [User].FirstName, [User].LastName, Text, CreatedOn
                        FROM BlahgPost
                        JOIN [User] ON [User].Id = BlahgPost.UserId
                        ORDER BY CreatedOn DESC
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BlahgPost blahgPost = new BlahgPost();
                    blahgPost.Id = reader.GetInt32(0);
                    blahgPost.Name = reader.GetString(1) + ' ' + reader.GetString(2);
                    blahgPost.Text = reader.GetString(3);
                    blahgPost.CreatedOn = reader.GetDateTimeOffset(4);
                    blahgPosts.Add(blahgPost);
                }
            }

            return View(blahgPosts);
        }

        public ActionResult Comment(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlahggerDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"
                        SELECT BlahgPost.Id, [User].FirstName, [User].LastName, Text, CreatedOn
                        FROM BlahgPost
                        JOIN [User] ON [User].Id = BlahgPost.UserId
                        WHERE BlahgPost.Id = @Id
                        ORDER BY CreatedOn DESC
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                Comment comment = new Comment();

                while (reader.Read())
                {
                    blahgPost.Id = reader.GetInt32(0);
                    blahgPost.Name = reader.GetString(1) + ' ' + reader.GetString(2);
                    blahgPost.Text = reader.GetString(3);
                    blahgPost.CreatedOn = reader.GetDateTimeOffset(4);
                }

                return View(blahgPost);
            }
        }
    }
}