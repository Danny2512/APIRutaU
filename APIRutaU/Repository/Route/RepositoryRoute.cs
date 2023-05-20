using APIRutaU.Models;
using Dapper;
using System.Data.SqlClient;

namespace APIRutaU.Repository.Route
{
    public class RepositoryRoute : IRepositoryRoute
    {
        private readonly string connectioString;

        public RepositoryRoute(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<dynamic> GetNearbyRoutes(GetNearbyRoutesViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_GetRoutesByStartPoint @latitude, @longitude",
                                                                new
                                                                { latitude = model.latitude, longitude = model.longitude });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
    }
}
