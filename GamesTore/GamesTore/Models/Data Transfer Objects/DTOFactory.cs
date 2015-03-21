﻿using GamesToreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace GamesTore.Models.Data_Transfer_Objects
{
    public class DTOFactory
    {
        private UrlHelper urlHelper { get; set; }

        ApiDbContext db = new ApiDbContext();

        public DTOFactory (HttpRequestMessage request)
	    {
            urlHelper = new UrlHelper(request);
	    }

        public GetUserDTO Create(UserModel user)
        {
            return new GetUserDTO()
            {
                URL = urlHelper.Link("GamesToreApi", new { id = user.Id }),
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };
            
        }

        public GetGameDTO Create(GameModel game)
        {
            GetGameDTO retu = new GetGameDTO()
            {
                URL = urlHelper.Link("GameRoute", new { id = game.Id }),
                Id = game.Id,
                GameName = game.GameName,
                ReleaseDate = game.ReleaseDate,
                Price = game.Price,
                InventoryStock = game.InventoryStock                
            };

            if (game.Genres != null)
            {
                retu.Genres = game.Genres.Select(g => Create(g)).ToList();
            }

            if (game.Tags != null)
            {
                retu.Tags = game.Tags.Select(t => Create(t)).ToList();
            }

            return retu;
        }

        public GetGenreDTO Create(GenreModel genres)
        {
            return new GetGenreDTO()
            {
                URL = urlHelper.Link("GenreRoute", new { id = genres.Id }),
                Id = genres.Id,
                Name = genres.Name
            };
        }

        public GetTagDTO Create(TagModel tags)
        {
            return new GetTagDTO()
            {
                URL = urlHelper.Link("TagRoute", new { id = tags.Id }),
                Id = tags.Id,
                Name = tags.Name
            };
        }

        public GetApikeyDTO Create(string apikey, int id)
        {
            return new GetApikeyDTO()
            {
                ApiKey = apikey,
                UserId = id
            };
        }

        public GetCartDTO Create(CartModel cart)
        {
            GetCartDTO retu = new GetCartDTO()
            {
                URL = urlHelper.Link("GamesToreApi", new { id = cart.Id }),
                Id = cart.Id,
                CheckoutReady = cart.CheckoutReady,
                User_Id = cart.User_Id,
                Games = new List<GetGameDTO>()
            };

            foreach (var item in cart.Games)
            {
                retu.Games.Add(Create(item));
            }
            return retu;
        }

        public GetSalesDTO Create(SalesModel sale)
        {
            return new GetSalesDTO()
            {
                URL = urlHelper.Link("GamesToreApi", new { id = sale.Id }),
                Id = sale.Id,
                SalesDate = sale.SalesDate,
                Total = sale.Total,
                Cart = Create(sale.Cart)
            };
        }
    }
   

}
        