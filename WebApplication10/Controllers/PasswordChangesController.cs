using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class PasswordChangesController : Controller

    {
        public SqlConnection con;
        public void CONNECTION()
        {
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
            con = new SqlConnection(constr);
        }
        [HttpGet]
        public ActionResult Index11()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index11(UserRoleDetailsModel model)
        { 
             CONNECTION();

        con.Open(); 
            
            SqlCommand cmd = new SqlCommand("sp_password_update", con);
        cmd.CommandType = CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue("@Password", model.NewPassword);
        
            return View();
    }
     
    }
}