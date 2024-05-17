using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APDotNetTrainingBatch4.RestApiWithNLayer.Features.BurmeseRecipes
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurmeseRecipesController : ControllerBase
    {
        private async Task<List<BurmeseRecipes>> GetDataAsync()
        {
            string BRJsonstr = await System.IO.File.ReadAllTextAsync("BurmeseRecipes_data.json");
            var model = JsonConvert.DeserializeObject<List<BurmeseRecipes>>(BRJsonstr);
            return model;
        }
        [HttpGet]
        public async Task<IActionResult> GetBurmeseRecipes()
        {
            var model = await GetDataAsync();
            return Ok(model);
        }
        //api/BurmeseRecipes/Name
        [HttpGet("Name")]
        public async Task<IActionResult> Name(string Name)
        {
            var model = await GetDataAsync();
            BurmeseRecipes result = new BurmeseRecipes();
            foreach (var item in model)
            {
                if (item.Name == Name) {
                    result =item;
                }               
            }
            if(result is null)
            {
                return NotFound("No Burmese Recipes!");
            }
            return Ok(result);
        }
        [HttpGet("Guid")]
        public async Task<IActionResult> Guid(string Guid)
        {
            var model = await GetDataAsync();
            BurmeseRecipes result = new BurmeseRecipes();
            foreach (var item in model)
            {
                if (item.Guid == Guid)
                {
                    result = item;
                }
            }
            if (result is null)
            {
                return NotFound("No Burmese Recipes!");
            }
            return Ok(result);
        }
        [HttpGet("UserType")]
        public async Task<IActionResult> Type(string UserType)
        {
            var model = await GetDataAsync();
            List<BurmeseRecipes> result = new List<BurmeseRecipes>();
            foreach (var item in model)
            {
                if (item.UserType == UserType)
                {
                    result.Add(item);
                }
            }
            if (result is null)
            {
                return NotFound("No Burmese Recipes!");
            }
            return Ok(result);
        }
    }  
    public class BurmeseRecipes
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string CookingInstructions { get; set; }
        public string UserType { get; set; }
    }

}
