using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace aws_secrets_manager.Controllers
{
    public class KralizekController : ControllerBase
    {
        private readonly IOptionsMonitor<DatabaseSettings>? _databaseSettings;
        private readonly IOptionsMonitor<KeySettings>? _keySettings;


        public KralizekController(IOptionsMonitor<DatabaseSettings>? databaseSettings, IOptionsMonitor<KeySettings>? keySettings)
        {
            _databaseSettings = databaseSettings;
            _keySettings = keySettings;
        }
        [HttpGet("settings")]
        public IActionResult GetSettings()
        {
            var settings = _databaseSettings.CurrentValue;
            return Ok(settings);
        }
        [HttpGet("keys")]
        public IActionResult GetKeys()
        {
            var settings = _keySettings.CurrentValue;
            return Ok(settings);
        }

    }
}
