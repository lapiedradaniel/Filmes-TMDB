namespace FilmesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Carregue as configurações do appsettings.json
            var configuration = builder.Configuration;

            // Obtém as chaves da API TMDB do appsettings.json
            var apiKey = configuration["ApiSettings:ApiKey"];
            var token = configuration["ApiSettings:Token"];

            // Adiciona as chaves como serviços para que possam ser injetadas onde necessário
            builder.Services.AddSingleton(new ApiKeyService(apiKey, token));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }

}
