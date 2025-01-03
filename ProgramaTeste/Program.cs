using ProgramaTeste.Models;
using ProgramaTeste.Repositories;

namespace ProgramaTeste
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddScoped<PessoaRepository>();
            builder.Services.AddScoped<EnderecoRepository>();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pessoa}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
