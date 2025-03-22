
using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Data;
using NotesAppWebAPI.Repositories.Interfaces;
using NotesAppWebAPI.Repositories;
using NotesAppWebAPI.Services.Interfaces;
using NotesAppWebAPI.Services;
using MediatR;
using FluentValidation;
using NotesAppWebAPI.Validators.NoteValidators;
using NotesAppWebAPI.Validators.TagValidators;
using NotesAppWebAPI.Validators.ReminderValidators;

namespace NotesAppWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            // Add services to the container.

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddValidatorsFromAssemblyContaining<CreateNoteCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<GetNoteCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateNoteCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<DeleteNoteCommandValidator>();

            builder.Services.AddValidatorsFromAssemblyContaining<CreateReminderCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<GetReminderCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateReminderCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<DeleteReminderCommandValidator>();

            builder.Services.AddValidatorsFromAssemblyContaining<CreateTagCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<GetTagCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateTagCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<DeleteTagCommandValidator>();

            builder.Services.AddScoped<INoteRepository, NoteRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IReminderRepository, ReminderRepository>();

            builder.Services.AddScoped<INoteService, NoteService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<IReminderService, ReminderService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
