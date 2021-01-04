using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class LoginController : Controller
    {
        public SqlConnection con;
        public void CONNECTION()
        {
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
            con = new SqlConnection(constr);                   
        }

        // GET: Login
        [AllowAnonymous]
        [HttpGet]
        // GET: Login
        public ActionResult Indexer()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Indexer(log model)
        {
            
            CONNECTION();
            SqlCommand cmd = new SqlCommand("UserLogin", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet dataSet = new DataSet();
            cmd.Parameters.AddWithValue("@EmailId", model.Email_id);
            string Passw = Encrypt(model.Pass);
            cmd.Parameters.AddWithValue("@Password", Passw);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                FormsAuthentication.SetAuthCookie(model.Email_id, true);

                Session["EmailId"] = model.Email_id;

                Session["Password"] = model.Pass;
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewData["message"] = "login failed";
            }

            con.Close();
            return View();

        }

        public ActionResult Welcome()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Welcome(log model)
        {
            string password = Decrypt(model.Pass);
            CONNECTION();
            SqlCommand cmd = new SqlCommand("UserLogin", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet dataSet = new DataSet();
            cmd.Parameters.AddWithValue("@EmailId", model.Email_id);

            cmd.Parameters.AddWithValue("@Password", model.Pass);

            
            return View();

        }
        public string Encrypt(string sData)
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                string message = "Error in encrypt" + ex.Message + " Value: " + sData;
                return message;
            }
        }

        public string Decrypt(string sData)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception ex)
            {
                string message = "error in decrypt: " + ex.Message + " Value: " + sData;
                return message;
            }
        }
    }
}
