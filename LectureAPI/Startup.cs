using LectureAPI.Hubs;
using LectureAPI.Interfaces;
using LectureAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SharedQueueServices.Interfaces;
using SharedQueueServices.QueueServices;
using ConnectionFactory = SharedQueueServices.QueueServices.ConnectionFactory;

namespace LectureAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddScoped<IQueueService, QueueService>();
			services.AddScoped<QueueServiceBeta>();

            services.AddScoped<IMessageQueue, MessageQueue>();
			services.AddSingleton<IConnectionFactory, ConnectionFactory>();

            services.AddScoped<IMessageProducer, MessageProducer>();
			services.AddScoped<IMessageProducerScope, MessageProducerScope>();
			services.AddSingleton<IMessageProducerScopeFactory, MessageProducerScopeFactory>();

			services.AddScoped<IMessageConsumer, MessageConsumer>();
			services.AddScoped<IMessageConsumerScope, MessageConsumerScope>();
			services.AddSingleton<IMessageConsumerScopeFactory, MessageConsumerScopeFactory>();

            services.AddCors();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200"));

            app.UseSignalR(routes =>
            {
                routes.MapHub<LectureHub>("/lecture");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
