using ApiDataLayer;
using ApiDataLayer.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TopicFramework.Attributes;
using TopicFramework.Controllers;

namespace Api.TopicControllers
{
    [TopicController("Track")]
    public class TrackingTopicController : TopicControllerBase
    {

        private ApiDbcontext _Dbcontext = default!;
        private ILogger<TrackingTopicController> _Logger = default!;

        public override void OnInitialize(IServiceProvider serviceProvider)
        {
            _Dbcontext = serviceProvider.GetRequiredService<ApiDbcontext>();
            _Logger = serviceProvider.GetRequiredService<ILogger<TrackingTopicController>>();
        }

        [TopicHandler("Location")]
        public async Task LogTrackingAsync()
        {
            _Logger.LogInformation($"Payload: {Message.Payload}");

            string[] data = Message.Payload.Split(' ');

            TrackingInfo info = new TrackingInfo()
            {
                TrackingCode = new Guid(data[0]),
                CreationDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(data[3])).DateTime,
                Longitude = data[2],
                Latitude = data[1],
            };

            try
            {
                _Dbcontext.TrackingInfos.Add(info);
                await _Dbcontext.SaveChangesAsync();
                _Logger.LogInformation($"[{info.CreationDate.ToLongDateString()}] Recieved data from {info.TrackingCode}");
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message);
            }
        }
    }
}
