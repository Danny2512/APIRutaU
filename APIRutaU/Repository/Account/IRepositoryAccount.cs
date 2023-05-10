using APIRutaU.Models;

namespace APIRutaU.Repository.Account
{
    public interface IRepositoryAccount
    {
        Task<dynamic> ChangePasswordByOTP(ChangePasswordByOTPViewModel model);
        Task<dynamic> GetOTP(GetOTPViewModel model);
        Task<dynamic> Login(LoginViewModel model);
        Task<dynamic> ValidateOTP(ValidateOTPViewModel model);
        Task<dynamic> ValidateUserById(ValidateUserByIdViewModel model);
    }
}
