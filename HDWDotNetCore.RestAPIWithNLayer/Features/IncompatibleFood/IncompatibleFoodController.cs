using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static HDWDotNetCore.RestAPIWithNLayer.Models.IncompatibleFoodModel;

namespace HDWDotNetCore.RestAPIWithNLayer.Features.IncompatibleFood
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncompatibleFoodController : ControllerBase
    {
        private async Task<Incompatiblefood> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("IncompatibleFood.json");
            var model = JsonConvert.DeserializeObject<Incompatiblefood>(jsonStr);
            return model!;
        }
        [HttpGet]
        public async Task<IActionResult> IncompatibleFood()
        {
            var model = await GetDataAsync();
            return Ok(model);
        }
        [HttpGet("Categories")]
        public async Task<IActionResult> Categories()
        {
            var model = await GetDataAsync();
            var categories = model!.Tbl_IncompatibleFood
                                    .DistinctBy(x => x.Description)
                                    .Select(x => new IncompatibleFoodCategory()
                                    {
                                        Description = x.Description
                                    }).ToList();

            return Ok(categories);
        }
        [HttpGet("SearchByCategory")]

        public async Task<IActionResult> ListByCategory(string category)
        {
            var model = await GetDataAsync();
            var list = model!.Tbl_IncompatibleFood
                              .Where(x => x.Description == category)
                              .Select(x => new IncompatibleFoodRespondData()
                              {
                                  Id = x.Id,
                                  FoodA = x.FoodA,
                                  FoodB = x.FoodB,
                                  Description = x.Description
                              }).ToList();
            return Ok(list);
        }

    }
}

