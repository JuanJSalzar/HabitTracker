using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.Models.Bot;
using Swashbuckle.AspNetCore.Filters;

namespace HabitsTracker.SwaggerExamples.ChatBot
{
    public class ChatExample : IExamplesProvider<Message>
    {
        public Message GetExamples()
        {
            return new Message
            {
                Prompt = "What are some healthy habits I can adopt to improve my overall well-being?"
            };
        }
    }
}