using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {

    
    public class ArtistController : Controller {

        private List<Artist> allArtists;

        public ArtistController() {
            allArtists = JsonToFile<Artist>.ReadJson();
        }

        //This method is shown to the user navigating to the default API domain name
        //It just display some basic information on how this API functions
        [Route("")]
        [HttpGet]
        public string Index() {
            //String describing the API functionality
            string instructions = "Welcome to the Music API~~\n========================\n";
            instructions += "    Use the route /artists/ to get artist info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *RealName/{string}\n";
            instructions += "       *Hometown/{string}\n";
            instructions += "       *GroupId/{int}\n\n";
            instructions += "    Use the route /groups/ to get group info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *GroupId/{int}\n";
            instructions += "       *ListArtists=?(true/false)\n";
            return instructions;
        }

        // Create a route for /artists that returns all artists as JSON        
        [Route("artists")]
        [HttpGet]
        public JsonResult root()
        {
            List<Artist> result = allArtists.OrderBy(k => k.ArtistName).ToList();
            return Json(result);
        }

        // Create a route /artists/name/{name} that returns all artists that match the provided name
        [Route("artists/name/{name}")]
        [HttpGet]
        public JsonResult artistsByName(string name) 
        {
            List<Artist> result = allArtists.Where(k => k.ArtistName.Contains(name)).OrderBy(k => k.ArtistName).ToList();
            return Json(result);
        }

        // Create a route /artists/realname/{name} that returns all artists who real names match the provided name
        [Route("artists/realname/{name}")]
        [HttpGet]
        public JsonResult artistsByRealname(string name)
        {
            List<Artist> result = allArtists.Where(k => k.RealName.Contains(name)).OrderBy(k => k.RealName).ToList();
            return Json(result);
        }

        // Create a route /artists/hometown/{town} that returns all artists who are from the provided town
        [Route("artists/hometown/{town}")]
        [HttpGet]
        public JsonResult artistsByHometown(string town) 
        {
            List<Artist> result = allArtists.Where( k => k.Hometown.Contains(town)).OrderBy(m => m.Hometown).ToList();
            return Json(result);
        }

        // Create a route /artists/groupid/{id} that returns all artists who have a GroupId that matches id
        [Route("/artists/groupid/{id}")]
        [HttpGet]
        public JsonResult artistsByGroupId(int id)
        {
            List<Artist> result = allArtists.Where(k => k.GroupId == id).OrderBy(m => m.GroupId).ToList();
            return Json(result);
        }
    }
}