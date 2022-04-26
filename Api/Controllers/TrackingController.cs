using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly ApiDbcontext _Dbcontext;

        public TrackingController(ApiDbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        [HttpGet]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:order:read")]
        [Route("{TrackingId}")]
        public async Task<List<TrackingInfo>> GetTrackingInfos(string TrackingId)
        {
            return await _Dbcontext.TrackingInfos.Where(ti => ti.TrackingInfoId.ToString() == TrackingId).ToListAsync();
        } 
    }
}
