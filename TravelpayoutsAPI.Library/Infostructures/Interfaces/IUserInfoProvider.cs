using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Models.Data;

namespace TravelpayoutsAPI.Library.Infostructures.Interfaces
{
    /// <summary>
    /// Информация о местоположении пользователя
    /// </summary>
    [ContractClass(typeof(UserInfoProviderContract))]
    public interface IUserInfoProvider
    {
        /// <summary>
        /// Возвращает IP пользователя
        /// </summary>
        /// <returns></returns>
        Task<string> UserIP { get; }

        /// <summary>
        /// Данные о местоположении пользователя
        /// </summary>
        /// <param name="ip">ip пользователя</param>
        /// <returns></returns>
        Task<UserLocationInfo> GetUserLocationAsync(string userIP);
    }

    [ContractClassFor(typeof(IUserInfoProvider))]
    internal abstract class UserInfoProviderContract : IUserInfoProvider
    {
        public Task<string> UserIP
        {
            get 
            {
                Contract.Ensures(Contract.Result<string>() != null);

                return Task.FromResult(default(string)); 
            }
        }

        public Task<UserLocationInfo> GetUserLocationAsync(string ip)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(ip), "IP can not null or empty.");
            Contract.Ensures(Contract.Result<UserLocationInfo>() != null);

            return Task.FromResult(default(UserLocationInfo));
        }
    }
}
