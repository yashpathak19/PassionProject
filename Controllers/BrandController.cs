using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PassionProject_PhoneBlog_n01364240.Data;
using PassionProject_PhoneBlog_n01364240.Models;
using System.Diagnostics;

namespace PassionProject_PhoneBlog_n01364240.Controllers
{
    public class BrandController : Controller
    {
        private PassionProjectContext db = new PassionProjectContext();
        // GET: Brand
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string searchbrandkey)
        {
            Debug.WriteLine("searching for " + searchbrandkey);
            // selecting all the brands
            string query = "select * from brands";
            //if searchkey is not empty then query can be added
            if (searchbrandkey != "")
            {
                query += " where BrandName like '%" + searchbrandkey + "%'";
            }
            //list of brands
            List<Brand> brands = db.Brands.SqlQuery(query).ToList();
            return View(brands);
        }

        [HttpPost]
        public ActionResult Add(string BrandName)
        {
            //inserting a record of brand
            string query = "insert into brands (BrandName) values (@BrandName)";
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@BrandName", BrandName);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            Debug.WriteLine("Adding the record in Brand/Add with query: " + query + sqlparams);
            //redirecting the view to list
            return RedirectToAction("List");
        }

        public ActionResult Add()
        {
            //nothing is needed to display brand
            return View();
        }

        public ActionResult Show(int id)
        {
            //displaying the brands
            string query = "select * from brands where BrandID = @id";
            var parameter = new SqlParameter("@id", id);
            Brand selectedbrand = db.Brands.SqlQuery(query, parameter).FirstOrDefault();
            Debug.WriteLine("Showing the record in Brand/Show with query: " + query);
            return View(selectedbrand);
        }

        public ActionResult Update(int id)
        {
            //getting the data of the selected brand
            string query = "select * from brands where BrandID = @id";
            var parameter = new SqlParameter("@id", id);
            Brand selectedbrand = db.Brands.SqlQuery(query, parameter).FirstOrDefault();
            Debug.WriteLine("Getting the record in Brand/Update with query: " + query);
            return View(selectedbrand);
        }
        [HttpPost]
        public ActionResult Update(int id, string BrandName)
        {
            //updating the brand
            string query = "update brands set BrandName = @BrandName where BrandID = @id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@BrandName", BrandName);
            sqlparams[1] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            Debug.WriteLine("Updating the record in Brand/Update with query: " + query + sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            //getting the record for the confirmation of deletion
            string query = "select * from brands where BrandID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Brand selectedspecies = db.Brands.SqlQuery(query, param).FirstOrDefault();
            Debug.WriteLine("Selecting the record in Brand/DeleteConfirm with query: " + query);
            return View(selectedspecies);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //to do: ref delete
            SqlParameter param = new SqlParameter("@id", id);
            string query = "delete from brands where BrandID=@id";
            db.Database.ExecuteSqlCommand(query, param);
            Debug.WriteLine("Deleting the record in Brand/Delete with query: " + query, param);
            return RedirectToAction("List");
        }

    }
}