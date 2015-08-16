using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Search
{
    public class GateError
    {
        public int Code { get; set; }

        /// <summary>
        /// содержимое ошибки
        /// </summary>
        public string Tos { get; set; }
    }

    /// <summary>
    /// информация о ходе опроса агентств
    /// </summary>
    public sealed class Gate
    {
        /// <summary>
        /// id агентства
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// кол-во билетов отданное агентством
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// время обработки запроса агентством
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// информация об ошибке
        /// </summary>
        public GateError Error { get; set; }

        /// <summary>
        /// количество билетов соответствующих поиску 
        /// (если агентство вернуло билет на неправильные даты или с ошибками, они отфильтровываются системой)
        /// </summary>
        [JsonProperty("good_count")]
        public int GoodCount { get; set; }
    }
}
