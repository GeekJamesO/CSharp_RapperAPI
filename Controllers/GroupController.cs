using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers
{
    public class GroupController : Controller
    {
        List<Group> allGroups { get; set; }
        List<Artist> allArtists { get; set; }

        public GroupController()
        {
            allGroups = JsonToFile<Group>.ReadJson();
            allArtists = JsonToFile<Artist>.ReadJson();
        }

        // Create a route /groups that returns all groups as JSON
        [Route("groups/{members?}")]
        [HttpGet]
        public JsonResult getAllGroups(bool members = false)
        {
                var result = allGroups.OrderBy(a => a.GroupName).ToList();
                if (members) 
                {
                    foreach (var g in result) 
                    {
                        g.Members = allArtists.Where(a => a.GroupId == g.Id).ToList();
                    }
                }
                return Json(result);
        }

        // Create a route /groups/name/{name} that returns all groups that match the provided name
        [Route("groups/name/{name}/{members?}")]
        [HttpGet]
        public JsonResult groupsByName(string name, bool members = false)
        {
            List<Group> result = allGroups.Where(b => b.GroupName.Contains(name)).OrderBy(a => a.GroupName).ToList();
            if (members)
            {
                foreach (var g in result)
                {
                    g.Members = allArtists.Where(a => a.GroupId == g.Id).ToList();
                }
            }
            return Json(result);
        }

        // Create a route /groups/id/{id} that returns all groups with the provided Id value
        [Route("groups/id/{id}/{members?}")]
        [HttpGet]
        public JsonResult groupsById(int id, bool members = false)
        {
            var result = allGroups.Where(b => b.Id == id).ToList();
            if (members)
            {
                foreach (var g in result)
                {
                    g.Members = allArtists.Where(a => a.GroupId == g.Id).ToList();
                }
            }
            return Json(result);
        }

        // (Optional) Add an extra boolean parameter to the group routes called displayArtists that will include members for all Group JSON responses
    }
}