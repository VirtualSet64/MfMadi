namespace DomainService.Entity
{
    /// <summary>
    /// Главное меню
    /// </summary>
    public class MainMenu
    {
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Ссылка
        /// </summary>
        public string? Link { get; set; }
        /// <summary>
        /// Отображение пункта меню в вверху главной страницы
        /// </summary>
        public bool? TopMainPageIsVisible { get; set; }
        /// <summary>
        /// Отображение пункта меню во всплывающем меню
        /// </summary>
        public bool? SideMenuIsVisible { get; set; }
        /// <summary>
        /// Отображать ли меню в строке над главными объявлениями на главной странице
        /// </summary>
        public bool? MenuAboveAdvertisingIsVisible { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public List<Menu>? ChildMenu { get; set; }
    }
}
