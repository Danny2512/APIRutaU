using APIRutaU.Models;

namespace APIRutaU.Repository.Route
{
    public interface IRepositoryRoute
    {
        Task<dynamic> GetNearbyRoutes(GetNearbyRoutesViewModel model);
    }
}
