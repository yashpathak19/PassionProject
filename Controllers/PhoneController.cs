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
using PassionProject_PhoneBlog_n01364240.Models.ViewModels;
using System.Diagnostics;
using System.IO;

namespace PassionProject_PhoneBlog_n01364240.Controllers
{
    public class PhoneController : Controller
    {
        private PassionProjectContext db = new PassionProjectContext();
        // GET: Phone
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string searchphonekey)
        {
            //getting the list of phones
            Debug.WriteLine("searching for " + searchphonekey);
            //getting all the phones
            string query = "Select * from Phones";
            //if the search string is not empty we can add the search string in query
            if (searchphonekey != "")
            {
                query = query + " where phonename like '%" + searchphonekey + "%'";
                Debug.WriteLine("The query is " + query);
            }
            Debug.WriteLine("Seledting the records in Phone/List with query: " + query);
            List<Phone> phones = db.Phones.SqlQuery(query).ToList();
            return View(phones);

        }

        public ActionResult Show(int? id)
        {

            //selecting the phone
            string main_query = "select * from Phones where PhoneID = @id";
            var pk_parameter = new SqlParameter("@id", id);
            Phone Phone = db.Phones.SqlQuery(main_query, pk_parameter).FirstOrDefault();
            //getting the associated feature with the phone
            string aside_query = "select * from Features inner join FeaturePhones on Features.FeatureID = FeaturePhones.Feature_FeatureID where FeaturePhones.Phone_PhoneID=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<Feature> PhoneFeatures = db.Features.SqlQuery(aside_query, fk_parameter).ToList();
            Debug.WriteLine("getting the records of associated feature with Phone/Show with query: " + aside_query);
            string all_features_query = "select * from Features";
            List<Feature> AllFeatures = db.Features.SqlQuery(all_features_query).ToList();
            ShowPhone viewmodel = new ShowPhone();
            viewmodel.phone = Phone;
            viewmodel.features = PhoneFeatures;
            viewmodel.all_features = AllFeatures;

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult AttachFeature(int id, int FeatureID)
        {
            //attaching the feature
            Debug.WriteLine("Phone id is" + id + " is attaching to featureid which is " + FeatureID);

            //checking if it's attached to phone already
            string check_query = "select * from Features inner join FeaturePhones on FeaturePhones.Feature_FeatureID = Features.FeatureID where Feature_FeatureID=@FeatureID and Phone_PhoneID=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@FeatureID", FeatureID);
            List<Feature> features = db.Features.SqlQuery(check_query, check_params).ToList();
            //if count is 0 then we can add the feature
            if (features.Count == 0)
            {
                string query = "insert into FeaturePhones (Feature_FeatureID, Phone_PhoneID) values (@FeatureID, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@FeatureID", FeatureID);


                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            return RedirectToAction("Show/" + id);

        }

        public ActionResult DetachFeature(int id, int FeatureID)
        {
            //detaching the feature from phone
            Debug.WriteLine("phone id is" + id + " and feature id is " + FeatureID);

            string query = "delete from FeaturePhones where Feature_FeatureID=@FeatureID and Phone_PhoneID=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@FeatureID", FeatureID);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            Debug.WriteLine("detaching the feature with Phone/DetachFeature with query: " + query);
            return RedirectToAction("Show/" + id);
        }


        // Address: /Phone/Add
        [HttpPost]
        public ActionResult Add(string PhoneName, DateTime PhoneReleaseDate, String PhoneBattery, int PhoneWeight, int BrandID)
        {
            //this method will add the phone to the database
            string query = "insert into Phones (PhoneName, PhoneReleaseDate, PhoneBattery, PhoneWeight, BrandID) values (@PhoneName,@PhoneReleaseDate,@PhoneBattery,@PhoneWeight,@BrandID)";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@PhoneName", PhoneName);
            sqlparams[1] = new SqlParameter("@PhoneReleaseDate", PhoneReleaseDate);
            sqlparams[2] = new SqlParameter("@PhoneBattery", PhoneBattery);
            sqlparams[3] = new SqlParameter("@PhoneWeight", PhoneWeight);
            sqlparams[4] = new SqlParameter("@BrandID", BrandID);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            Debug.WriteLine("Inserting the Phone record with Phone/Add with query: " + query + sqlparams);
            //redirecting the view to Phone/List
            return RedirectToAction("List");
        }

        public ActionResult Add()
        {
            //getting all the brands for to add view
            List<Brand> brand = db.Brands.SqlQuery("select * from Brands").ToList();
            return View(brand);
        }



        public ActionResult Update(int id)
        {
            //getting the selected phone for updating
            Phone selectedphone = db.Phones.SqlQuery("select * from phones where phoneid = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Brand> Brands = db.Brands.SqlQuery("select * from brands").ToList();
            Debug.WriteLine("Selected Phone is: " + selectedphone);
            UpdatePhone UpdatePhoneModel = new UpdatePhone();
            UpdatePhoneModel.Phone = selectedphone;
            UpdatePhoneModel.Brands = Brands;
            return View(UpdatePhoneModel);
        }
        [HttpPost]
        public ActionResult Update(int id, string PhoneName, DateTime PhoneReleaseDate, String PhoneBattery, int PhoneWeight, int BrandID)
        {
            //updating the phone: /URL: Phone/Update
            string query = "update phones set PhoneName=@PhoneName, PhoneReleaseDate=@PhoneReleaseDate, PhoneBattery=@PhoneBattery, PhoneWeight=@PhoneWeight, BrandID=@BrandID where PhoneID = @id";

            SqlParameter[] sqlparams = new SqlParameter[6];

            sqlparams[0] = new SqlParameter("@PhoneName", PhoneName);
            sqlparams[1] = new SqlParameter("@PhoneReleaseDate", PhoneReleaseDate);
            sqlparams[2] = new SqlParameter("@PhoneBattery", PhoneBattery);
            sqlparams[3] = new SqlParameter("@PhoneWeight", PhoneWeight);
            sqlparams[4] = new SqlParameter("@BrandID", BrandID);
            sqlparams[5] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            Debug.WriteLine("Updating the Phone record with Phone/Update with query: " + query + sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            //confirmation for deletion of the phone
            string query = "select * from phones where PhoneID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            Phone selectedphone = db.Phones.SqlQuery(query, param).FirstOrDefault();
            Debug.WriteLine("Delete confrimation for the Phone record with Phone/DeleteConfirm query: " + query);
            return View(selectedphone);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //deleting the phone record
            //to do: ref deletion
            string query = "delete from phones where phoneid = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            Debug.WriteLine("Deleting the Phone record with Phone/Delete with query: " + query);
            return RedirectToAction("List");
        }


    }
}