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
    public class FeatureController : Controller
    {
        private PassionProjectContext db = new PassionProjectContext();
        // GET: Feature
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string searchfeaturekey)
        {

            Debug.WriteLine("searching for " + searchfeaturekey);
            string query = "select * from features";
            //checking if the string is not empty
            if (searchfeaturekey != "")
            {
                query += " where FeatureName like '%" + searchfeaturekey + "%'";
            }
            List<Feature> features = db.Features.SqlQuery(query).ToList();
            return View(features);
        }

        [HttpPost]
        public ActionResult Add(string FeatureName, string FeatureDetails)
        {
            //adding the feature
            string query = "insert into features (FeatureName, FeatureDetails) values (@FeatureName, @FeatureDetails)";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@FeatureName", FeatureName);
            sqlparams[1] = new SqlParameter("@FeatureDetails", FeatureDetails);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            Debug.WriteLine("Adding the record in Feature/Add with query: " + query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult Add()
        {
            //no information is needed to display feature
            return View();
        }

        public ActionResult Show(int id)
        {
            //showing the record
            string query = "select * from features where FeatureID = @id";
            var parameter = new SqlParameter("@id", id);
            Feature selectedfeature = db.Features.SqlQuery(query, parameter).FirstOrDefault();
            Debug.WriteLine("Shwoing the record in Feature/Show with query: " + query, parameter);
            return View(selectedfeature);
        }

        public ActionResult Update(int id)
        {
            //getting the details of the record that is needed to update
            string query = "select * from features where FeatureID = @id";
            var parameter = new SqlParameter("@id", id);
            Feature selectedfeature = db.Features.SqlQuery(query, parameter).FirstOrDefault();
            Debug.WriteLine("Selecting the record in Feature/Update with query: " + query, parameter);
            return View(selectedfeature);
        }
        [HttpPost]
        public ActionResult Update(int id, string FeatureName, string FeatureDetails)
        {
            //updating the feature record
            string query = "update features set FeatureName = @FeatureName, FeatureDetails = @FeatureDetails where FeatureID = @id";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@FeatureName", FeatureName);
            sqlparams[1] = new SqlParameter("@FeatureDetails", FeatureDetails);
            sqlparams[2] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            Debug.WriteLine("Updating the record in Feature/Update with query: " + query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            //confirmation of deletion of a feature
            string query = "select * from features where FeatureID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Feature selectedfeature = db.Features.SqlQuery(query, param).FirstOrDefault();
            Debug.WriteLine("Confirming the record for deletion in Feature/DeletConfirm with query: " + query, param);
            return View(selectedfeature);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //deleting the record
            SqlParameter param = new SqlParameter("@id", id);

            string query = "delete from features where FeatureID=@id";
            db.Database.ExecuteSqlCommand(query, param);
            Debug.WriteLine("Deleting the record in Feature/Delete with query: " + query, param);
            return RedirectToAction("List");
        }

    }
}