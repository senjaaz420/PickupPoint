using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DDDiplom.Models;
using Microsoft.EntityFrameworkCore;

namespace DDDiplom.Services.GenerationRandomOrders
{
    public class GenerationRandomOrders
    {
        private Random r = new Random();


        public GenerationRandomOrders()
        {

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(10);

            var timer = new System.Threading.Timer((e) =>
            {
                Generate();
            }, null, startTimeSpan, periodTimeSpan);
        }



        public void Generate()
        {

            List<Product> goodsList = new List<Product>();
            Product randomItem;

            using (DDDiplomContext db = new DDDiplomContext())
            {
                int _Id = 0;


                var buf = db.Orders.LastOrDefault();
                if (buf != null)
                    _Id = buf.Id;
                else
                    _Id = 1;

                for (int i = 0; i < r.Next(1,9); i++)
                {
                    var count = db.Products.Count() > 0 ? db.Products.Count() : 2;
                    var randromId = r.Next(db.Products.FirstOrDefault().Id, db.Products.FirstOrDefault().Id + count);
                    randomItem = db.Products.FirstOrDefault(x=>x.Id == randromId);
                    goodsList.Add(randomItem);
                }


                int clientId = r.Next(1, 99999);
                Client client = new Client
                {
                    //Id = clientId,
                    Name = "Clien" + clientId.ToString(),
                    Surname = "bb",
                    Secondname = "tt",
                    PhoneNumber = "+37529" + r.Next(1111111, 9999999).ToString()
                };

                var workPaceBuf = db.WorkPlaces.FirstOrDefault();
                int firstWorkPlaceId = workPaceBuf != null ? workPaceBuf.Id : 1;
                int lastWorkPlaceId = firstWorkPlaceId + db.WorkPlaces.Count();
                var randomId = r.Next(firstWorkPlaceId, lastWorkPlaceId);

                WorkPlace workPlace = db.WorkPlaces.FirstOrDefault(x=> x.Id == randomId);

                Order order = new Order
                {
                    //Id = db.Orders.LastOrDefault().Id + 1,
                    IsPaid = "нет",
                    OrderTime = DateTime.Now,
                    OrderProducts = goodsList,
                    ClientId = client.Id,
                    Client = client,
                    Summary = goodsList.Sum(x => x.Price),
                    WorkPlace = workPlace,
                    WorkPlaceId = workPlace.Id

                };
                db.Orders.Add(order);

                Debug.WriteLine(DateTime.Now + " New Order was added");

                db.SaveChanges();
            }



        }

    }
}
