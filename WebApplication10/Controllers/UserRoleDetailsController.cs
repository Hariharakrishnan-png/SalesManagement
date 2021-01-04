using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class UserRoleDetailsController : Controller
    {
        public SqlConnection con;
        public void CONNECTION()
        {
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
            con = new SqlConnection(constr);
        }
        
        public ActionResult UserRoleIndex()
        {
            var email = Session["EmailId"].ToString();
            var password = Session["Password"].ToString();
            LoginModel objDetails = new LoginModel();
            CONNECTION();         

            con.Open();
            SqlCommand cmd = new SqlCommand("UserLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;           
            cmd.Parameters.AddWithValue("@Emailid", email);
            cmd.Parameters.AddWithValue("@Password", password);                     

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {               
                objDetails.EmailId = Convert.ToString((sdr["Emailid"]));
                objDetails.Password = Convert.ToString(sdr["Password"]);

                objDetails.IsPasswordChanged = (sdr["IsPasswordChanged"] == DBNull.Value ? false : Convert.ToBoolean(sdr["IsPasswordChanged"]));
                //objDetails.IsPasswordChanged = (sdr["IsPasswordChanged"] == 0);


            }
            con.Close();

            if (!objDetails.IsPasswordChanged)
            {
                //return View("ChangePassword");
                return RedirectToAction("Index");



            }

            else
            {
                return View("UserRoleIndex");
            }
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(UserRoleDetailsModel model)
        {
            
            CONNECTION();

            con.Open(); 
            
            SqlCommand cmd = new SqlCommand("sp_password_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue("@Password", model.NewPassword);

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