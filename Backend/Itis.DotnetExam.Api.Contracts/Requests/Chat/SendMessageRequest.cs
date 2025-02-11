namespace Itis.DotnetExam.Api.Contracts.Requests.Chat;

public class SendMessageRequest
{
    public Guid ChatId { get; set; }
    public string Text { get; set; }
}