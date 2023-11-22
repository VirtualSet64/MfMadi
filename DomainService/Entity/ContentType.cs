using System.Xml.Serialization;

namespace DomainService.Entity
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
        TextWithLink
    }
}
