namespace DomainService.Entity
{
    /// <summary>
    /// Объявления
    /// </summary>
    public class Advertising
    {
        public int Id { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public string? MainText { get; set; }
        /// <summary>
        /// Основной текст
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Текст на кнопке
        /// </summary>
        public string? ButtonText { get; set; }
        /// <summary>
        /// Ссылка на кнопке
        /// </summary>
        public string? ButtonLink { get; set; }
        /// <summary>
        /// Выделенный текст
        /// </summary>
        public string? BoldTextButtom { get; set; }
        /// <summary>
        /// Изображение
        /// </summary>
        public string? AvatarFileName { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// Должно ли отображаться объявление на главном слайдере на главной странице
        /// </summary>
        public bool? MainSliderIsVisible { get; set; }
        /// <summary>
        /// Отвечает за показ объявления в нижней части главной страницы
        /// Не должен появляться в других частях сайта
        /// </summary>
        public bool? MainPageDownIsVisible { get; set; }
        public bool? IsDeleted { get; set; }

        public Content? Content { get; set; }
    }
}
