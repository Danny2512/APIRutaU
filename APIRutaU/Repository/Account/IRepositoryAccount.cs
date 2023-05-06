using APIRutaU.Models;

namespace APIRutaU.Repository.Account
{
    public interface IRepositoryAccount
    {
        Task<dynamic> Login(LoginViewModel model);
    }
}
