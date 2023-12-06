using System.Collections.Generic;
using Newtonsoft.Json;

public class LineBotWebhookRequest
{
    [JsonProperty("events")]
    public List<WebhookEvent> Events { get; set; }
}

public class WebhookEvent
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("message")]
    public WebhookEventMessage Message { get; set; }
    [JsonProperty("replyToken")]
    public string ReplyToken { get; set; } // 添加 ReplyToken 属性

}

public class WebhookEventMessage
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}
