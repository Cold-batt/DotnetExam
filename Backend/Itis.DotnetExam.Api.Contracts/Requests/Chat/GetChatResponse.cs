namespace Itis.DotnetExam.Api.Contracts.Requests.Chat;

public class GetChatResponse
{
    public Guid ChatId { get; set; }

    public List<ResponseMessage> Messages { get; set; }
}