using System.Xml.Serialization;

namespace DomainService.Dto.Enums
{
    public enum ContentType
    {
        /// <summary>
        /// Подробнее
        /// </summary>
        [XmlAttribute("Подробнее")]
        Details,
        /// <summary>
        /// Текст со ссылкой
        /// </summary>
        [XmlAttribute("Текст со ссылкой")]
        TextWithLink,
        /// <summary>
        /// Контакты
        /// </summary>
        [XmlAttribute("Контакты")]
        Contacts
    }
}
