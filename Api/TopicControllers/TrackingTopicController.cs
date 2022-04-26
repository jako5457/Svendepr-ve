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
                TrackingInfoId = new Guid(data[0]),
                CreationDate = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(data[3])).DateTime,
                Longitude = data[1],
                Latitude = data[2],
            };

            try
            {
                _Dbcontext.TrackingInfos.Add(info);
                await _Dbcontext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message);
            }
        }
    }
}
