using CorporateQnA.Services.Interfaces;
using CorporateQnA.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CorporateQnA.Extensions
{
	public static class SimpleInjector
	{
		public static void AddServices(this IServiceCollection services)
		{
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IQuestionServices, QuestionServices>();
            services.AddScoped<IAnswerServices, AnswerServices>();
            services.AddScoped<IQuestionActivityServices, QuestionActivityServices>();
            services.AddScoped<IAnswerActivityServices, AnswerActivityServices>();
            services.AddScoped<IQuestionActivityServices, QuestionActivityServices>();
        }
	}
}

