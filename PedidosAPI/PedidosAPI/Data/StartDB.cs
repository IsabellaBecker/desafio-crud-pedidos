using PedidosAPI.DataBase;

namespace PedidosAPI.Data
{
    public static class StartDB
    {
        public static async void CreateDBIfNotExistis(this IHost host) 
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DataContext>();
            context.Database.EnsureCreated();

            DBInitializerData.InitData(context);
        }
    }
}
