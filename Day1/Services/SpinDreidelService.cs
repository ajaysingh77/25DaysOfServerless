using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DaysOfServerless.Day1.Services
{
    public class SpinDreidelService : ISpinDreidel
    {
        private readonly ILogger _logger;
        private static Random _getRandomDreidels = new Random();
        private readonly DreidelOptions _dreidelOptions;
        public SpinDreidelService(IOptions<DreidelOptions> options, ILogger<SpinDreidelService> logger)
        {
            _dreidelOptions = options.Value;
            _logger = logger;

        }
        public Task<string> Spin()
        {
            var index = _getRandomDreidels.Next(_dreidelOptions.Dreidels.Count);
            var value = _dreidelOptions.Dreidels[index];
            return Task.FromResult<string>(value);
        }
    }
}
