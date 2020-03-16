using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodWebAPI.Controllers
{
    public class FoodController : ApiController
    {
        [HttpGet]
        public List<Food> GetFoodList()
        {
            DBFoodDataContext db = new DBFoodDataContext();
            return db.Foods.ToList();
        }

        [HttpGet]
        public Food GetFood(int id)
        {
            DBFoodDataContext db = new DBFoodDataContext();
            return db.Foods.FirstOrDefault(x => x.id == id);
        }

        [HttpPost]
        public bool InsertNewFood(string name, string type, int price)
        {
            try
            {
                DBFoodDataContext db = new DBFoodDataContext();
                Food food = new Food();
                food.name = name;
                food.type = type;
                food.price = price;
                db.Foods.InsertOnSubmit(food);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        public bool UpdateFood(int id, string name, string type, int price)
        {
            try
            {
                DBFoodDataContext db = new DBFoodDataContext();
                Food food = db.Foods.FirstOrDefault(x => x.id == id);
                if (food == null) return false;
                food.name = name;
                food.type = type;
                food.price = price;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteFood(int id)
        {
            DBFoodDataContext db = new DBFoodDataContext();
            Food food = db.Foods.FirstOrDefault(x => x.id == id);
            if (food == null) return false;
            db.Foods.DeleteOnSubmit(food);
            db.SubmitChanges();
            return true;
        }
    }
}
