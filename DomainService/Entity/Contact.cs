using DomainService.Dto.Enums;

namespace DomainService.Entity
{
    /// <summary>
    /// Контактная информация
    /// </summary>
    public class Contact
    {
        public int Id { get; set; }
        /// <summary>
        /// Название контактной информации
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Значение контактной информации
        /// </summary>
        public string? Value{ get; set; }
        public ContactType? ContactType { get; set; }
        /// <summary>
        /// Флаг отображающий контакт в строчке вверху главной страницы
        /// </summary>
        public bool? IsTopMainPageVisible { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }
}
