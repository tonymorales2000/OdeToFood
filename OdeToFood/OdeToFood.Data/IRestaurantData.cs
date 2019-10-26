using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int Id);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant{ Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian},
                new Restaurant{ Id = 2, Name = "Cinnamon Club", Location = "London", Cuisine = CuisineType.None},
                new Restaurant{ Id = 3, Name = "La Costa", Location = "California", Cuisine = CuisineType.Mexican},
            };
        }

        public Restaurant GetById(int Id)
        {
            return restaurants.SingleOrDefault(a=>a.Id == Id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {

            return from r in restaurants
                   where string.IsNullOrEmpty(name) ||  r.Name.ToLower().Contains(name.ToLower())
                   orderby r.Name
                   select r;
        }
    }
}
