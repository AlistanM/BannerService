using BannerService.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace BannerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        [Route("user_banner")]
        public async Task GetBanner([FromQuery] int tag_id, [FromQuery] int feature_id, [FromQuery] bool use_last_revision = false)
        {
            
        }

        [HttpGet]
        [Route("banner")]
        public async Task GetBanners([FromQuery] int tag_id, [FromQuery] int feature_id, [FromQuery] int limit = 15, [FromQuery] int offset = 0)
        {

        }

        [HttpPost]
        [Route("banner")]
        public async Task CreateBanner([FromBody] BannerDto banner)
        {
            
        }

        [HttpPatch]
        [Route("banner/{id}")]
        public async Task UpdateBanner([FromBody] BannerDto banner, int id)
        {

        }

        [HttpDelete]
        [Route("banner/{id}")]
        public async Task DeleteBanner(int id)
        {

        }
    }
}
