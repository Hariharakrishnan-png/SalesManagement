using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Security;
using System.Net.Configuration;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        //Web.database_Access_Layer.db dblayer = new MVCLogin.database_Access_Layer.db();
        public ActionResult Index()
        {
            return View(); 
            //ewy78ywe
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlCommand com = new SqlCommand("Sp_Login", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Admin_id", model.Adminid);
            com.Parameters.AddWithValue("@password", model.Password);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@Isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);
            if (res == 1)
            {                
                return RedirectToAction("Dropdownvalues");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Login/Password!";
                return View();
            }
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Dropdownvalues()
        {
            LoginModel objuser = new LoginModel();
            DataSet ds = new DataSet();
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            using (SqlCommand cmd = new SqlCommand("select * from Department", con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<LoginModel> userlist = new List<LoginModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                    LoginModel uobj = new LoginModel();
                    uobj.DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentId"]);

                    uobj.DepartmentName = ds.Tables[0].Rows[i]["DepartmentName"].ToString();
                       
                        userlist.Add(uobj);
                    }
                    objuser.usersinfo = userlist;
                }
                con.Close();
         
            return View(objuser);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LoginModel model)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                string password = Encrypt(model.UserPassword);
                //string pass = Decrypt(model.UserPassword);
                SqlConnection con = new SqlConnection(cs);
                SqlCommand com = new SqlCommand("sp_credntials", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmailId", model.EmailId);
                com.Parameters.AddWithValue("@Password", password);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                ViewData["message"] = "Login Created successfully";
                SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                string to = model.EmailId;
                string from = section.From;
                int index = to.IndexOf('@');
                string name = to.Substring(0, index);
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Successful Registration";
                message.Body = "Hi " + name + " , you have been registered Successfully!!! The your user name is" + model.EmailId + " and password is " + model.UserPassword;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = section.Network.Port;
                smtp.EnableSsl = section.Network.EnableSsl;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(section.Network.UserName, section.Network.Password);
                smtp.Host = section.Network.Host;
                smtp.Send(message);
                return View();
            }
            catch (Exception ex)
            {
                return View("Errorpage");
            }

           
        }
        public ActionResult ChangePassword()
        {
                   
            

            return View();

        }
        
        #region Engrypt and Decrypt
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
        #endregion

        //[AllowAnonymous]
        //[HttpGet]
        //// GET: Login
        //public ActionResult Indexe()
        //{
        //    return View();
        //}
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult Indexe(LoginModel model)
        //{
        //    string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        //    SqlConnection con = new SqlConnection(cs);
        //    SqlCommand cmd = new SqlCommand("UserLogin");
        //    con.Open();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    DataSet dataSet = new DataSet();
        //    cmd.Parameters.AddWithValue("@EmailId", model.Email_id);
        //    cmd.Parameters.AddWithValue("@password", model.Pass);
        //    SqlDataReader sdr = cmd.ExecuteReader();
        //    if(sdr.Read())
        //    {
        //        FormsAuthentication.SetAuthCookie(model.Email_id, true);
        //        Session["Email_Id"] = model.Email_id.ToString();
        //        return RedirectToAction("Dashboard");
        //    }
        //    else
        //    {
        //        ViewData["message"] = "login failed";
        //    }
        //    con.Close();
        //    return View();

        //}


    }
}