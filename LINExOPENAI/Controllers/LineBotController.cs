using Line;
using LINExOPENAI.ApplicationService.ADProduct;
using LINExOPENAI.ApplicationService.Interfaces;
using LINExOPENAI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LINExOPENAI.Controllers
{
    [Route("/Line/[action]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LineBotConfig _lineBotConfig;
        private readonly IADProductService _adProduct;

        public LineBotController(IHttpContextAccessor httpContextAccessor, LineBotConfig lineBotConfig, IADProductService adProduct)
        {
            _httpContextAccessor = httpContextAccessor;
            _lineBotConfig = lineBotConfig;
            _adProduct = adProduct;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> UserInput(LineBotWebhookRequest request)
        {
            ADProductResponseModel response = new ADProductResponseModel();
            ILineConfiguration config = new LineConfiguration()
            {
                ChannelAccessToken = _lineBotConfig.accessToken,
                ChannelSecret = _lineBotConfig.channelSecret
            };
            var bot = new LineBot(config);
            string replytoken = String.Empty;
            //change git 
            try
            {
                var events = request.Events;

                foreach (var ev in events)
                {
                    if (ev.Type.ToUpper() == LineEventType.Message.ToString().ToUpper())
                    {
                        switch (ev.Message.Type.ToUpper())
                        {
                            case "TEXT":
                                string textMessage = ev.Message.Text;
                                CustomerRequestModel req = new CustomerRequestModel()
                                {
                                    Message = textMessage
                                };
                                response = await _adProduct.GenerateAdContent(req);
                                replytoken = ev.ReplyToken;
                                var replyMessage = new TextMessage(response.ADContent.First().Trim());
                                await bot.Reply(replytoken, replyMessage);
                                break;
                            case "STICKER":
                                string stickerId = "52002738"; // 貼圖 ID
                                string packageId = "11537"; // 貼圖套件 ID
                                //string stickerId = ev.Message.StickerId;
                                //string packageId = ev.Message.PackageId;
                                var stickerMessage = new StickerMessage(packageId, stickerId);
                                replytoken = ev.ReplyToken;
                                await bot.Reply(replytoken, stickerMessage);
                                break;

                            default:
                                break;
                        }


                        

                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                // 需要 Log 可自行加入
                //_logger.LogError(JsonConvert.SerializeObject(ex));
                var replyMessage = new TextMessage($"失敗，{ex.Message.ToString()}");
                await bot.Reply(replytoken, replyMessage);
            }
            return Ok();
        }

    }
}
