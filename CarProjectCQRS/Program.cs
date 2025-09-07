using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.CarHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.EmployeeHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.SliderHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.ReservationHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.TurkeyAirportHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.MessageHandlers;
using CarProjectCQRS.ViewComponents.MainUiViewComponents;
using Microsoft.EntityFrameworkCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// MediatR servisi ekle
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// HttpClient servisi ekle (FuelPrice/AdminController için)
builder.Services.AddHttpClient();

// Entity Framework DbContext Configuration
builder.Services.AddDbContext<CarProjectDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarProjectDatabase")));

// About Handlers
builder.Services.AddScoped<GetAboutQueryHandler>();
builder.Services.AddScoped<GetAboutByIdQueryHandler>();
builder.Services.AddScoped<CreateAboutCommandHandler>();
builder.Services.AddScoped<UpdateAboutCommandHandler>();
builder.Services.AddScoped<RemoveAboutCommandHandler>();

// Car Handlers
builder.Services.AddScoped<GetCarQueryHandler>();
builder.Services.AddScoped<GetCarByIdQueryHandler>();
builder.Services.AddScoped<CreateCarCommandHandler>();
builder.Services.AddScoped<UpdateCarCommandHandler>();
builder.Services.AddScoped<RemoveCarCommandHandler>();

// Employee Handlers
builder.Services.AddScoped<GetEmployeeQueryHandler>();
builder.Services.AddScoped<GetEmployeeByIdQueryHandler>();
builder.Services.AddScoped<CreateEmployeeCommandHandler>();
builder.Services.AddScoped<UpdateEmployeeCommandHandler>();
builder.Services.AddScoped<RemoveEmployeeCommandHandler>();

// Feature Handlers
builder.Services.AddScoped<GetFeatureQueryHandler>();
builder.Services.AddScoped<GetFeatureByIdQueryHandler>();
builder.Services.AddScoped<CreateFeatureCommandHandler>();
builder.Services.AddScoped<UpdateFeatureCommandHandler>();
builder.Services.AddScoped<RemoveFeatureCommandHandler>();

// Service Handlers
builder.Services.AddScoped<GetServiceQueryHandler>();
builder.Services.AddScoped<GetServiceByIdQueryHandler>();
builder.Services.AddScoped<CreateServiceCommandHandler>();
builder.Services.AddScoped<UpdateServiceCommandHandler>();
builder.Services.AddScoped<RemoveServiceCommandHandler>();

// Slider Handlers
builder.Services.AddScoped<GetSliderQueryHandler>();
builder.Services.AddScoped<GetSliderByIdQueryHandler>();
builder.Services.AddScoped<CreateSliderCommandHandler>();
builder.Services.AddScoped<UpdateSliderCommandHandler>();
builder.Services.AddScoped<RemoveSliderCommandHandler>();

// Testimonial Handlers
builder.Services.AddScoped<GetTestimonialQueryHandler>();
builder.Services.AddScoped<GetTestimonialByIdQueryHandler>();
builder.Services.AddScoped<CreateTestimonialCommandHandler>();
builder.Services.AddScoped<UpdateTestimonialCommandHandler>();
builder.Services.AddScoped<RemoveTestimonialCommandHandler>();

// Reservation Handlers
builder.Services.AddScoped<GetReservationQueryHandler>();
builder.Services.AddScoped<GetReservationByIdQueryHandler>();
builder.Services.AddScoped<CreateReservationCommandHandler>();
builder.Services.AddScoped<UpdateReservationCommandHandler>();
builder.Services.AddScoped<RemoveReservationCommandHandler>();

// TurkeyAirport Handlers
builder.Services.AddScoped<GetTurkeyAirportQueryHandler>();
builder.Services.AddScoped<GetTurkeyAirportByIdQueryHandler>();
builder.Services.AddScoped<CreateTurkeyAirportCommandHandler>();
builder.Services.AddScoped<UpdateTurkeyAirportCommandHandler>();
builder.Services.AddScoped<RemoveTurkeyAirportCommandHandler>();

// Distance Handlers
builder.Services.AddScoped<GetDistanceQueryHandler>();
builder.Services.AddScoped<GetDistanceByIdQueryHandler>();
builder.Services.AddScoped<CreateDistanceCommandHandler>();
builder.Services.AddScoped<UpdateDistanceCommandHandler>();
builder.Services.AddScoped<RemoveDistanceCommandHandler>();

// Message Handlers
builder.Services.AddScoped<CreateMessageCommandHandler>();
builder.Services.AddScoped<GetMessageQueryHandler>();
builder.Services.AddScoped<UpdateMessageCommandHandler>();
builder.Services.AddScoped<RemoveMessageCommandHandler>();

// ViewComponents
builder.Services.AddScoped<CarouselComponentPartial>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
