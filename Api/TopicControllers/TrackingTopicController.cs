namespace Api.TopicControllers
{
    [TopicController("Track")]
    public class TrackingTopicController : TopicControllerBase
    {

        private ApiDbcontext _Dbcontext;
        private ILogger<TrackingTopicController> _Logger;

        public override void OnInitialize(IServiceProvider serviceProvider)
        {
            _Dbcontext = serviceProvider.GetRequiredService<ApiDbcontext>();
            _Logger = serviceProvider.GetRequiredService<ILogger<TrackingTopicController>>();
        }

        [TopicHandler("Location")]
        public Task LogTrackingAsync()
        {
            _Logger.LogInformation($"Payload: {Message.Payload}");
            return Task.CompletedTask;
        }
    }
}
