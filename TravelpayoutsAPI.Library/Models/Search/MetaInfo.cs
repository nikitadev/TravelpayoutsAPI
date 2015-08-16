namespace TravelpayoutsAPI.Library.Models.Search
{
    public sealed class MetaInfo
    {
        /// <summary>
        /// идентификатор поиска
        /// </summary>
        public string UUID { get; set; }

        /// <summary>
        /// информация о ходе опроса агентств в процессе поиска
        /// </summary>
        public Gate[] Gates { get; set; }
    }
}
