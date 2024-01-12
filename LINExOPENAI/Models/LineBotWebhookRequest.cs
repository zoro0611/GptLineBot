using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    //[JsonProperty("type")]
    //public string Type { get; set; }

    //[JsonProperty("text")]
    //public string Text { get; set; }
    public string Id { get; set; }
    public string Type { get; set; }

    // Text Message Event
    public string? Text { get; set; }
    public List<TextMessageEventEmojiDto>? Emojis { get; set; }
    public TextMessageEventMentionDto? Mention { get; set; }

    // Image & Video & Audio Message Event
    public ContentProviderDto? ContentProvider { get; set; }
    public ImageMessageEventImageSetDto? ImageSet { get; set; }
    public int? Duration { get; set; }

    //File Message Event
    public string? FileName { get; set; }
    public int? FileSize { get; set; }

    //Location Message Event
    public string? Title { get; set; }
    public string? Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    // Sticker Message Event
    public string? PackageId { get; set; }
    public string? StickerId { get; set; }
    public string? StickerResourceType { get; set; }
    public List<string>? Keywords { get; set; }


}

public class TextMessageEventEmojiDto
{
    public int Index { get; set; }
    public int Length { get; set; }
    public string ProductId { get; set; }
    public string EmojiId { get; set; }
}
public class TextMessageEventMentionDto
{
    public List<TextMessageEventMentioneeDto> Mentionees { get; set; }
}
public class TextMessageEventMentioneeDto
{
    public int Index { get; set; }
    public int Length { get; set; }
    public string UserId { get; set; }
}
public class ContentProviderDto
{
    public string Type { get; set; }
    public string? OriginalContentUrl { get; set; }
    public string? PreviewImageUrl { get; set; }
}
public class ImageMessageEventImageSetDto
{
    public string Id { get; set; }
    public string Index { get; set; }
    public string Total { get; set; }
}

