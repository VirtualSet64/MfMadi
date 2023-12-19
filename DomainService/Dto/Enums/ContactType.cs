using System.ComponentModel;
using System.Xml.Serialization;

namespace DomainService.Dto.Enums
{
    public enum ContactType
    {
        /// <summary>
        /// Адрес
        /// </summary>
        [XmlAttribute("Адрес")]
        Address,
        /// <summary>
        /// Номер телефона
        /// </summary>
        [XmlAttribute("Номер телефона")]
        PhoneNumber,
        /// <summary>
        /// График работы
        /// </summary>
        [XmlAttribute("График работы")]
        WorkShedule,
        /// <summary>
        /// Приемная
        /// </summary>
        [XmlAttribute("Приемная")]
        Reception
    }
}
