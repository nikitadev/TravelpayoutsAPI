using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace TravelpayoutsAPI.Library.Infostructures.Interfaces
{
	[ContractClass(typeof(RequestManagerContract))]
    public interface IRequestManager
    {
        /// <summary>
        /// Возвращает текст
        /// </summary>
        /// <param name="uri">путь</param>
        /// <param name="isToken">токен</param>
        /// <param name="isGzip"></param>
        /// <returns></returns>
        Task<string> Get(Uri uri, string token = null, bool isGzip = false);

        /// <summary>
        /// Возвращает объект
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">путь</param>
        /// <param name="isToken">токен</param>
        /// <param name="isGzip"></param>
        /// <returns></returns>
        Task<T> GetObject<T>(Uri uri, string token = null, bool isGzip = false);

        /// <summary>
        /// Возвращает JToken
        /// </summary>
        /// <param name="uri">путь</param>
        /// <param name="isToken">токен</param>
        /// <param name="isGzip"></param>
        /// <returns></returns>
        Task<JToken> GetJToken(Uri uri, string token = null, bool isGzip = false);

        /// <summary>
        /// Отправляет post запрос
        /// </summary>
        /// <param name="uri">путь</param>
        /// <param name="data">данные запроса</param>
        /// <returns></returns>
        Task<string> Post<T>(Uri uri, T data) where T : class;
    }

    [ContractClassFor(typeof(IRequestManager))]
    internal abstract class RequestManagerContract : IRequestManager
    {
        public Task<string> Get(Uri uri, string token, bool isGzip)
        {
            Contract.Requires<ArgumentNullException>(uri != null, "uri is null.");
            Contract.Ensures(Contract.Result<string>() != null);

            return Task.FromResult(default(string));
        }

        public Task<T> GetObject<T>(Uri uri, string token, bool isGzip)
        {
            Contract.Requires<ArgumentNullException>(uri != null, "uri is null.");
            Contract.Ensures(Contract.Result<T>() != null);

            return Task.FromResult(default(T));
        }

        public Task<JToken> GetJToken(Uri uri, string token, bool isGzip)
        {
            Contract.Requires<ArgumentNullException>(uri != null, "uri is null.");
            Contract.Ensures(Contract.Result<JToken>() != null);

            return Task.Factory.StartNew(() => default(JToken));
        }

        public Task<string> Post<T>(Uri uri, T data) where T : class
        {
            Contract.Requires<ArgumentNullException>(uri != null, "uri is null.");
            Contract.Requires<ArgumentNullException>(data != null, "data is null.");

            return Task.FromResult(default(string));
        }
    }
}
