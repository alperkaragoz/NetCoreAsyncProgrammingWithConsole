using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // CancellationToken

        // Örn: Controller içerisindeki metod yaklaşık 10 dk sürdüğünü varsayarsak, CancellationToken kullanılarak iptal edilebilir.Mesela kullanıcı request yaptıktan sonra sayfayı kapattığında veya refresh yaptığında CancellationToken devreye girerek ilgili requesti sonlandırır.Eğer CancellationToken kullanılmazsa kullanıcı request yaptıktan sonra refresh veya sayfayı kapatma yapsa bile o request arka planda kaynak tüketmeye devam edecektir.

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> GetContentAsync(CancellationToken token)
        {
            try
            {
                _logger.LogInformation("Request started");
                await Task.Delay(4000, token);
                //Şayet asenkron değil de senkron metod kullanıyorsak CancellationToken kullanıldığında hatayı aşağıdaki gibi manuel olarak kullanarakta fırlatabiliriz.
                //token.ThrowIfCancellationRequested();

                var task = new HttpClient().GetStringAsync("https://www.google.com");

                var response = await task;
                _logger.LogInformation("Request ended");
                return Ok(response);
            }
            catch (TaskCanceledException tex)
            {
                _logger.LogInformation($"Task Cancelled : {tex.Message}");
                return BadRequest(tex.Message);
            }
        }
    }
}
