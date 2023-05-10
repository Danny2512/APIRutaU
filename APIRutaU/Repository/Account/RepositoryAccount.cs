using APIRutaU.Models;
using Dapper;
using System.Data.SqlClient;
using System.Text.Json;

namespace APIRutaU.Repository.Account
{
    public class RepositoryAccount : IRepositoryAccount
    {
        private readonly string connectioString;

        public RepositoryAccount(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<dynamic> Login(LoginViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_Login @Email, @Password",
                                                                new
                                                                { Email = model.User, Password = model.Pass });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
        public async Task<dynamic> ValidateUserById(ValidateUserByIdViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_ValidateUserById @User_Id", new { User_Id = model.User_Id });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
        public async Task<dynamic> ValidateOTP(ValidateOTPViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_ValidateOTP @StrEmail, @StrOTP;",
                                                        new { StrEmail = model.Email, StrOTP = model.Cod });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
        public async Task<dynamic> ChangePasswordByOTP(ChangePasswordByOTPViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_ChangePasswordByOTP @User_Id, @StrPassword, @StrOTP;",
                                                        new { User_Id = model.User_Id, StrPassword = model.Password, StrOTP = model.OTP });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
        public async Task<dynamic> GetOTP(GetOTPViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_GetOtpByUser @Email;",
                                                        new { Email = model.Email });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
    }
}