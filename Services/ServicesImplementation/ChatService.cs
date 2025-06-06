using OpenAI.Chat;
using Azure.AI.OpenAI;
using HabitsTracker.Models.Bot;
using HabitsTracker.Services.IServices;
using System.ComponentModel.DataAnnotations;
using HabitsTracker.Repository.GenericRepository;

namespace HabitsTracker.Services.ServicesImplementation
{
    public class ChatService(
        AzureOpenAIClient azureOpenAiClient,
        IChatMessageRepository chatMessageRepository,
        IGenericRepository<ChatMessageEntity> genericRepository,
        ILogger<ChatService> logger)
        : IChatService
    {
        public async Task<string> GetResponse(string prompt, int userId)
        {
            logger.LogDebug("GetResponse called with userId: {UserId} and prompt: {Prompt}", userId, prompt);
            if (userId <= 0)
            {
                logger.LogWarning("Invalid user id provided: {UserId}", userId);
                throw new ValidationException("Invalid user ID.");
            }

            int userMessageCount = await chatMessageRepository.CountUserMessagesAsync(userId);

            if (userMessageCount >= 5)
            {
                logger.LogWarning("User {UserId} has reached the daily rate limit.", userId);
                throw new InvalidOperationException("You've reached today's message limit. Please come back tomorrow.");
            }

            var historyChat = await chatMessageRepository.GetUserMessagesAsync(userId);

            var messages = new List<ChatMessage>();

            if (!historyChat.Any())
            {
                messages.Add(new SystemChatMessage("You are an artificial intelligence assistant specialized exclusively in topics related to healthy habits. You can only answer questions or provide suggestions about physical activity, nutrition, rest, hydration, stress management, and overall well-being. You must not answer questions that are not related to these topics. If the user asks something outside these areas, you must respond politely that you cannot help with that."));
                messages.Add(new AssistantChatMessage("Hello! I'm your healthy habits assistant. I can help with advice on exercise, nutrition, rest, hydration, or overall well-being. How would you like to improve today?"));
            }
            else
            {
                messages.AddRange(historyChat.Select(m =>
                    m.Role == "user"
                        ? (ChatMessage)new UserChatMessage(m.Content)
                        : new AssistantChatMessage(m.Content)
                ));
            }

            messages.Add(new UserChatMessage(prompt));

            var options = new ChatCompletionOptions
            {
                Temperature = (float)0.7,
                MaxOutputTokenCount = 800,
                TopP = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0
            };

            try
            {
                ChatClient chatClient = azureOpenAiClient.GetChatClient("habit-bot");
                ChatCompletion chatCompletionRequest = await chatClient.CompleteChatAsync(messages, options);
                var response = chatCompletionRequest.Content.FirstOrDefault()?.Text;

                if (string.IsNullOrEmpty(response))
                {
                    logger.LogInformation("No response received from the assistant for user {UserId}.", userId);
                    return "No response received from the assistant.";
                }

                await genericRepository.AddAsync(new ChatMessageEntity
                {
                    UserId = userId,
                    Role = "user",
                    Content = prompt,
                    Timestamp = DateTime.UtcNow
                });

                await genericRepository.AddAsync(new ChatMessageEntity
                {
                    UserId = userId,
                    Role = "assistant",
                    Content = response,
                    Timestamp = DateTime.UtcNow
                });
                logger.LogInformation("Stored conversation for user {UserId}.", userId);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while processing GetResponse for user {UserId}", userId);
                return $"Error: {ex.Message}";
            }
        }
    }
}