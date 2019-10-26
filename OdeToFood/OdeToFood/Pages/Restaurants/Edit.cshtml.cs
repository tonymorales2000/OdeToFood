﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);
                if (Restaurant == null)
                {
                    return RedirectToPage("./NotFound");
                }
            }
            else
            {
                Restaurant = new Restaurant();
                
            }
            
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
                
            }

            if(Restaurant.Id > 0)
            {
                Restaurant = restaurantData.Update(Restaurant);
                if (Restaurant == null)
                {
                    return RedirectToPage("./NotFound");
                }

            }
            else
            {
                Restaurant = restaurantData.Add(Restaurant);
            }

            restaurantData.Commit();
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });

        }
    }
}