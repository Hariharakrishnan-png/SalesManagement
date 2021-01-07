using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10;

namespace WebApplication10.Controllers
{
    public class LeadController : Controller
    {
        private SqlConnection con;
        //To Switch Connection String................................//
        private void Connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["LeadConnection"].ToString();
            con = new SqlConnection(constring);
        }
        //CREATE LEAD
        [HttpGet]
        public ActionResult Create()
        {
            LeadsModel DropdownList = new LeadsModel()
            {
                LeadSourceList = GetLeadSourceList(),
                CountryList = GetCountryList(),
                StateList = GetStateList(),
                CityList = GetCityList(),
                TypeList = GetTypeList()
            };
            return View(DropdownList);
        }

        //DropdownList Cities
        public List<Cities> GetCityList()
        {
            // Cities DropdownList = new Cities();

            List<Cities> CityList = new List<Cities>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("USP_SALES_MANAGEMENT_SelectAllCityName", con);
                Com.CommandType = CommandType.StoredProcedure;
                SqlDataReader Sqlreader = Com.ExecuteReader();

                while (Sqlreader.Read())
                {
                    CityList.Add(new Cities
                    {
                        Id = Convert.ToInt32(Sqlreader["Id"]),
                        CityName = Convert.ToString(Sqlreader["CityName"]),

                    });
                }

                con.Close();
                return CityList;

            }
        }
        [HttpGet]
        public JsonResult GetCityList(long? StateId)
        {
            // Cities DropdownList = new Cities();

            List<Cities> CityList = new List<Cities>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("USP_SALES_MANAGEMENT_FilterByCityName", con);
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.AddWithValue("@StateId", StateId);
                SqlDataReader Sqlreader = Com.ExecuteReader();

                while (Sqlreader.Read())
                {
                    CityList.Add(new Cities
                    {
                        Id = Convert.ToInt32(Sqlreader["Id"]),
                        CityName = Convert.ToString(Sqlreader["CityName"]),

                    });
                }

                con.Close();
                return Json(CityList.ToList(), JsonRequestBehavior.AllowGet);

            }
        }
        //DropdownList States
        public List<States> GetStateList()
        {
            //States DropdownList = new States();
            List<States> StateList = new List<States>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("USP_SALES_MANAGEMENT_SelectAllStateName", con);
                Com.CommandType = CommandType.StoredProcedure;
                SqlDataReader Sqlreader = Com.ExecuteReader();
                while (Sqlreader.Read())
                {
                    StateList.Add(new States
                    {
                        Id = Convert.ToInt32(Sqlreader["Id"]),
                        StateName = Convert.ToString(Sqlreader["StateName"]),

                    });
                }
                con.Close();

                return StateList;

            }
        }
        [HttpGet]
        public JsonResult GetStateList(long? CountryId)
        {
            //States DropdownList = new States();
            List<States> StateList = new List<States>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("USP_SALES_MANAGEMENT_FilterByStateName", con);
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.AddWithValue("@CountryId", CountryId);
                SqlDataReader Sqlreader = Com.ExecuteReader();
                while (Sqlreader.Read())
                {
                    StateList.Add(new States
                    {
                        Id = Convert.ToInt32(Sqlreader["Id"]),
                        StateName = Convert.ToString(Sqlreader["StateName"]),

                    });
                }
                con.Close();

                return Json(StateList.ToList(), JsonRequestBehavior.AllowGet);

            }
        }

        //DropdownList Countries
        public List<Countries> GetCountryList()
        {
            List<Countries> CountryList = new List<Countries>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("USP_SALES_MANAGEMENT_FilterByCountryName", con);
                SqlDataReader Sqlreader = Com.ExecuteReader();
                Com.CommandType = CommandType.StoredProcedure;
                while (Sqlreader.Read())
                {
                    CountryList.Add(new Countries
                    {
                        Id = Convert.ToInt32(Sqlreader["Id"]),
                        CountryName = Convert.ToString(Sqlreader["CountryName"]),
                    });
                }

                con.Close();
                return CountryList;
            }
        }

        //DropdownList LeadSources
        public List<LeadSources> GetLeadSourceList()
        {
            List<LeadSources> LeadSourceList = new List<LeadSources>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("USP_SALES_MANAGEMENT_SelectAllLeadSource", con);
                Com.CommandType = CommandType.StoredProcedure;
                SqlDataReader Sqlreader = Com.ExecuteReader();
                while (Sqlreader.Read())
                {
                    LeadSourceList.Add(new LeadSources
                    {
                        Id = Convert.ToInt32(Sqlreader["Id"]),
                        LeadSource = Convert.ToString(Sqlreader["LeadSource"]),
                    });
                }
                con.Close();
                return LeadSourceList;
            }
        }


        //DropdownList Type
        public List<Types> GetTypeList()
        {
            List<Types> TypeList = new List<Types>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("SP_SALES_MANAGEMENT_Lead_Type", con);
                Com.CommandType = CommandType.StoredProcedure;
                SqlDataReader Sqlreader = Com.ExecuteReader();
                while (Sqlreader.Read())
                {
                    TypeList.Add(new Types
                    {
                        TypeId = Convert.ToInt32(Sqlreader["TypeId"]),
                        TypeName = Convert.ToString(Sqlreader["TypeName"]),
                    });
                }
                con.Close();
                return TypeList;
            }
        }

        //CREATE LEADS 
        [HttpPost]
        public ActionResult Create(LeadsModel model, HttpPostedFileBase file)
        {
            LeadsModel DropdownList = new LeadsModel()
            {
                LeadSourceList = GetLeadSourceList(),
                CountryList = GetCountryList(),
                StateList = GetStateList(),
                CityList = GetCityList(),
                TypeList = GetTypeList()
            };
            {
                Connection();
                SqlCommand Command = new SqlCommand("SP_Lead_Insert", con);
                Command.CommandType = CommandType.StoredProcedure;
                con.Open();
                //Command.Parameters.AddWithValue("@Photo", model.Photo);
                Command.Parameters.AddWithValue("@FirstName", model.FirstName);
                Command.Parameters.AddWithValue("@LastName", model.LastName);
                Command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                Command.Parameters.AddWithValue("@Gender", model.Gender);
                Command.Parameters.AddWithValue("@CurrentAddress", model.CurrentAddress);
                Command.Parameters.AddWithValue("@PermanentAddress", model.PermanentAddress);
                Command.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                Command.Parameters.AddWithValue("@EmailId", model.EmailId);
                Command.Parameters.AddWithValue("@Country", model.Country);
                Command.Parameters.AddWithValue("@State", model.State);
                Command.Parameters.AddWithValue("@City", model.City);
                Command.Parameters.AddWithValue("@Title", model.Title);
                Command.Parameters.AddWithValue("@LeadSource", model.LeadSource);
                Command.Parameters.AddWithValue("@MeetingDate", model.MeetingDate);
                Command.Parameters.AddWithValue("@Type", model.Type);
                Command.Parameters.AddWithValue("@JobTitle", model.JobTitle);
                Command.Parameters.AddWithValue("@CompanyName", model.CompanyName);
                Command.Parameters.AddWithValue("@CompanyWebSite", model.CompanyWebsite);
                if (file != null && file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string imgpath = Path.Combine(Server.MapPath("~/Lead-Images/"), filename);
                    file.SaveAs(imgpath);
                }
                Command.Parameters.AddWithValue("@Photo", "~/Lead-Images/" + file.FileName);
                Command.ExecuteNonQuery();
                con.Close();
                ViewBag.Message = "LEAD CREATE SUCCESSFULLY :)";
                return View(DropdownList);
            }

        }

        //LIST INDEX
        public ActionResult Index(string SortingCol, string SortType)
        {
            List<LeadsModel> LeadList = new List<LeadsModel>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("USP_SALES_MANAGEMENT_SelectAll", con);
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.AddWithValue("@SortingCol", SortingCol);
                Com.Parameters.AddWithValue("@SortType", SortType);

                if (SortType == "ASC")
                {
                    SortType = "DESC";
                }
                else
                {
                    SortType = "ASC";
                }
                ViewBag.sorttype = SortType;

                SqlDataReader Sqlreader = Com.ExecuteReader();
                while (Sqlreader.Read())
                {
                    var customer = new LeadsModel();
                    customer.LeadId = Convert.ToInt32(Sqlreader["LeadId"]);
                    customer.Photo = Sqlreader["Photo"].ToString();
                    customer.FirstName = Sqlreader["FirstName"].ToString();
                    customer.LastName = Sqlreader["LastName"].ToString();
                    customer.DateOfBirth = Convert.ToDateTime(Sqlreader["DateOfBirth"]);
                    customer.Gender = Sqlreader["Gender"].ToString();
                    customer.CurrentAddress = Sqlreader["CurrentAddress"].ToString();
                    customer.PermanentAddress = Sqlreader["PermanentAddress"].ToString();
                    customer.MobileNumber = Convert.ToInt64(Sqlreader["MobileNumber"]);
                    customer.EmailId = Sqlreader["EmailId"].ToString();
                    customer.City = Sqlreader["City"].ToString();
                    customer.State = Sqlreader["State"].ToString();
                    customer.Country = Sqlreader["Country"].ToString();
                    customer.Title = Sqlreader["Title"].ToString();
                    customer.LeadSource = Sqlreader["LeadSource"].ToString();
                    customer.MeetingDate = Convert.ToDateTime(Sqlreader["MeetingDate"]);
                    customer.Type = Sqlreader["Type"].ToString();
                    customer.JobTitle = Sqlreader["Job Title"].ToString();
                    customer.CompanyWebsite = Sqlreader["CompanyWebsite"].ToString();
                    customer.CompanyName = Sqlreader["CompanyName"].ToString();
                    LeadList.Add(customer);
                }

                return View(LeadList);
            }
        }

        //
        [HttpGet]
        public ActionResult Edit(int? LeadId)
        {
            LeadsModel customer = new LeadsModel();
            List<LeadsModel> LeadList = new List<LeadsModel>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("USP_SALES_MANAGEMENT_SelectAllbyId", con);
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.AddWithValue("@LeadId", LeadId);
                SqlDataReader Sqlreader = Com.ExecuteReader();
                while (Sqlreader.Read())
                {
                    customer.LeadId = Convert.ToInt32(Sqlreader["LeadId"]);
                    customer.Photo = Sqlreader["Photo"].ToString();
                    customer.FirstName = Sqlreader["FirstName"].ToString();
                    customer.LastName = Sqlreader["LastName"].ToString();
                    customer.DateOfBirth = Convert.ToDateTime(Sqlreader["DateOfBirth"]);
                    customer.Gender = Sqlreader["Gender"].ToString();
                    customer.CurrentAddress = Sqlreader["CurrentAddress"].ToString();
                    customer.PermanentAddress = Sqlreader["PermanentAddress"].ToString();
                    customer.MobileNumber = Convert.ToInt64(Sqlreader["MobileNumber"]);
                    customer.EmailId = Sqlreader["EmailId"].ToString();
                    customer.City = Sqlreader["City"].ToString();
                    customer.State = Sqlreader["State"].ToString();
                    customer.Country = Sqlreader["Country"].ToString();
                    customer.Title = Sqlreader["Title"].ToString();
                    customer.LeadSource = Sqlreader["LeadSource"].ToString();
                    customer.MeetingDate = Convert.ToDateTime(Sqlreader["MeetingDate"]);
                    customer.Type = Sqlreader["Type"].ToString();
                    customer.JobTitle = Sqlreader["JobTitle"].ToString();
                    customer.CompanyWebsite = Sqlreader["CompanyWebsite"].ToString();
                    customer.CompanyName = Sqlreader["CompanyName"].ToString();
                    LeadList.Add(customer);
                }

                return View(customer);
            }
        }


        [HttpPost]
        public ActionResult Edit(int? LeadId, LeadsModel model, HttpPostedFileBase file)
        {
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))

            {
                con.Open();
                SqlCommand Command = new SqlCommand("USP_SALES_MANAGEMENT_Update", con);
                Command.CommandType = CommandType.StoredProcedure;
                //Command.Parameters.AddWithValue("@Photo", model.Photo);
                Command.Parameters.AddWithValue("@LeadId", model.LeadId);
                Command.Parameters.AddWithValue("@FirstName", model.FirstName);
                Command.Parameters.AddWithValue("@LastName", model.LastName);
                Command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                Command.Parameters.AddWithValue("@Gender", model.Gender);
                Command.Parameters.AddWithValue("@CurrentAddress", model.CurrentAddress);
                Command.Parameters.AddWithValue("@PermanentAddress", model.PermanentAddress);
                Command.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                Command.Parameters.AddWithValue("@EmailId", model.EmailId);
                Command.Parameters.AddWithValue("@Country", model.Country);
                Command.Parameters.AddWithValue("@State", model.State);
                Command.Parameters.AddWithValue("@City", model.City);
                Command.Parameters.AddWithValue("@Title", model.Title);
                Command.Parameters.AddWithValue("@LeadSource", model.LeadSource);
                Command.Parameters.AddWithValue("@MeetingDate", model.MeetingDate);
                Command.Parameters.AddWithValue("@Type", model.Type);
                Command.Parameters.AddWithValue("@Job Title", model.JobTitle);
                Command.Parameters.AddWithValue("@CompanyName", model.CompanyName);
                Command.Parameters.AddWithValue("@CompanyWebSite", model.CompanyWebsite);
                if (file != null && file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string imgpath = Path.Combine(Server.MapPath("~/Lead-Images/"), filename);
                    file.SaveAs(imgpath);
                }
                Command.Parameters.AddWithValue("@Photo", "~/Lead-Images/" + file.FileName);
                Command.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Delete(int? LeadId)
        {
            LeadsModel customer = new LeadsModel();
            List<LeadsModel> LeadList = new List<LeadsModel>();
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))
            {
                con.Open();
                SqlCommand Com = new SqlCommand("USP_SALES_MANAGEMENT_SelectAllbyId", con);
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.AddWithValue("@LeadId", LeadId);
                SqlDataReader Sqlreader = Com.ExecuteReader();
                while (Sqlreader.Read())
                {
                    customer.LeadId = Convert.ToInt32(Sqlreader["LeadId"]);
                    customer.Photo = Sqlreader["Photo"].ToString();
                    customer.FirstName = Sqlreader["FirstName"].ToString();
                    customer.LastName = Sqlreader["LastName"].ToString();
                    customer.DateOfBirth = Convert.ToDateTime(Sqlreader["DateOfBirth"]);
                    customer.Gender = Sqlreader["Gender"].ToString();
                    customer.CurrentAddress = Sqlreader["CurrentAddress"].ToString();
                    customer.PermanentAddress = Sqlreader["PermanentAddress"].ToString();
                    customer.MobileNumber = Convert.ToInt64(Sqlreader["MobileNumber"]);
                    customer.EmailId = Sqlreader["EmailId"].ToString();
                    customer.City = Sqlreader["City"].ToString();
                    customer.State = Sqlreader["State"].ToString();
                    customer.Country = Sqlreader["Country"].ToString();
                    customer.Title = Sqlreader["Title"].ToString();
                    customer.LeadSource = Sqlreader["LeadSource"].ToString();
                    customer.MeetingDate = Convert.ToDateTime(Sqlreader["MeetingDate"]);
                    LeadList.Add(customer);
                }

                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult Delete(int? LeadId, LeadsModel model)
        {
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))

            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_SALES_MANAGEMENT_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeadId", LeadId);
                cmd.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Qualify(int? Id)
        {
            string Dbconnection = ConfigurationManager.ConnectionStrings["LeadConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(Dbconnection))

            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_Qialify_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeadId", Id);
                cmd.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("Index");
            }
        }
    }
}