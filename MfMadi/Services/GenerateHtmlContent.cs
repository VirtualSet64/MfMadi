using Infrastructure.Repository.Interfaces;
using MfMadi.Services.Interfaces;

namespace MfMadi.Services
{
    public class GenerateHtmlContent : IGenerateHtmlContent
    {
        private readonly IMainMenuRepository _mainMenuRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string TemplateHtmlFilePathForTable { get; set; }
        private string TableHtmlFilePath { get; set; }

        public GenerateHtmlContent(IMainMenuRepository mainMenuRepository, IContactRepository contactRepository, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _mainMenuRepository = mainMenuRepository;
            _contactRepository = contactRepository;
            TemplateHtmlFilePathForTable = configuration["TemplateHtmlFilePathForTable"];
            TableHtmlFilePath = configuration["TableHtmlFilePath"];
            _webHostEnvironment = webHostEnvironment;
        }

        public void GenerateHtml(string path, string contentTitle, string contentHtml)
        {            
            path = path[path.Length - 1].Equals('/') ? path : path + "/";

            string templateHtmlContent = File.ReadAllText(TemplateHtmlFilePathForTable);
            int nestingLevel = path.Split("/").Where(path => path != "").Count();
            templateHtmlContent = templateHtmlContent.Replace("../", string.Join("", Enumerable.Repeat("../", nestingLevel)));

            var menuList = _mainMenuRepository.GetMainMenu();

            //Вставка заголовка страницы
            templateHtmlContent = templateHtmlContent.Replace("<!-- Заголовок страницы -->", $"{contentTitle} - МФ МАДИ");

            //Генерация контаков в шапке
            var contactsForHeader = _contactRepository.GetContacts().Where(x => x.IsTopMainPageVisible == true);
            string contactHtml = "";
            foreach (var contact in contactsForHeader)
            {
                contactHtml += $"<span class='header-top__wrapper-item'>{contact.Value}</span>";
            }
            templateHtmlContent = templateHtmlContent.Replace("<!-- Контакты в шапке -->", contactHtml);

            //Генерация меню напротив логотипа для шапки и подвала
            var menuTop = menuList.Where(m => m.TopMainPageIsVisible == true);
            string menuTopHeaderHtml = "";
            string menuTopFooterHtml = "";
            foreach (var menu in menuTop)
            {
                menuTopHeaderHtml += $"<li class='header-main__item'>" +
                                      $"<a href='{menu.Link}' class='header-main__link'>{menu.Name}</a></li>";
                menuTopFooterHtml += $"<li class='footer-top__item'>" +
                                      $"<a href='{menu.Link}' class='footer-top__link'>{menu.Name}</a></li>";
            }
            templateHtmlContent = templateHtmlContent.Replace("<!-- Меню напротив логотипа -->", menuTopHeaderHtml);
            templateHtmlContent = templateHtmlContent.Replace("<!-- Меню напротив логотипа в подвале -->", menuTopFooterHtml);

            //Генерация меню над слайдером объявлений для шапки и подвала
            var menuAboveAdvertising = menuList.Where(m => m.MenuAboveAdvertisingIsVisible == true).ToList();
            string menuAboveAdvHeaderHtml = "";
            string menuAboveAdvFooterHtml = "";
            for (int i = 0; i < menuAboveAdvertising.Count; i++)
            {
                menuAboveAdvHeaderHtml += $"<li class='header-bottom__item {(i % 2 != 0 ? "header-bottom__item_dark" : "")}'>" +
                                      $"<a href='{menuAboveAdvertising[i].Link}' class='header-bottom__link' target='_blank'>{menuAboveAdvertising[i].Name}</a></li>";
                menuAboveAdvFooterHtml += $"<li class='footer__menu-item {(i % 2 != 0 ? "footer__menu-item_dark" : "")}'>" +
                                      $"<a href='{menuAboveAdvertising[i].Link}' class='footer__menu-link' target='_blank'>{menuAboveAdvertising[i].Name}</a></li>";
            }
            templateHtmlContent = templateHtmlContent.Replace("<!-- Меню над главными объявлениями -->", menuAboveAdvHeaderHtml);
            templateHtmlContent = templateHtmlContent.Replace("<!-- Меню над главными объявлениями для подвала -->", menuAboveAdvFooterHtml);

            //Генерация бокового меню
            var sideMenu = menuList.Where(m => m.SideMenuIsVisible == true);
            string sideMenuHtml = "";
            foreach (var sideMenuItem in sideMenu)
            {
                if (sideMenuItem.ChildMenu != null && sideMenuItem.ChildMenu.Count > 0)
                {
                    sideMenuHtml += "<li class='header-menu__item has-submenu'>" +
                        "<div class='header-menu__submenu-wrapper submenu-wrapper'>" +
                           $"<a href='{sideMenuItem.Link}' class='submenu-wrapper__link'>{sideMenuItem.Name}</a>" +
                           "<button class='submenu-wrapper__btn submenu-btn'>" +
                               "<div class='submenu-btn__icon'>" +
                                   "<svg width='18' height='11' viewBox='0 0 18 11' fill='none' xmlns='http://www.w3.org/2000/svg'>" +
                                       "<path d='M1 1L9 9L17 1' stroke='#4a27c9' stroke-width='2'/>" +
                                   "</svg>" +
                               "</div>" +
                           "</button>" +
                       "</div>" +
                       "<div class='submenu'><ul class='submenu__list'>";

                    foreach (var childItem in sideMenuItem.ChildMenu)
                    {
                        sideMenuHtml += "<li class='submenu__item'>" +
                                         $"<a href='{childItem.Link}' class='submenu__link'>{childItem.Name}</a>" +
                                        "</li>";
                    }
                    sideMenuHtml += "</ul></div></li>";
                }
                else
                {
                    sideMenuHtml += "<li class='header-menu__item'>" +
                               $"<a href='{sideMenuItem.Link}' class='header-menu__link'>{sideMenuItem.Name}</a>" +
                                "</li>";
                }
            }
            templateHtmlContent = templateHtmlContent.Replace("<!-- Боковое меню -->", sideMenuHtml);

            //Генерация меню в подвале
            var menuFooter = menuList.Where(m => m.MenuAboveAdvertisingIsVisible == false && m.SideMenuIsVisible == false && m.TopMainPageIsVisible == false && m.ChildMenu.Count() > 0);
            string menuFooterHtml = "";
            foreach (var menuFooterItem in menuFooter)
            {
                menuFooterHtml += "<li class='footer-bottom__item footer-item'>" +
                   $"<a href='{menuFooterItem.Link}' class='footer-item__title'>{menuFooterItem.Name}</a>" +
                    "<ul class='footer-item__elements'>";
                foreach (var childMenu in menuFooterItem?.ChildMenu)
                {
                    menuFooterHtml += "<li class='footer-item__element'>" +
                                $"<a href='{childMenu.Link}' class='footer-item__link'>{childMenu.Name}</a></li>";
                }
                menuFooterHtml += "</ul></li>";
            }
            templateHtmlContent = templateHtmlContent.Replace("<!-- Меню для подвала -->", menuFooterHtml);

            //Генерация ссылок в низу подвала
            var menuFooterLinks = menuList.Where(m => m.MenuAboveAdvertisingIsVisible == false && m.SideMenuIsVisible == false && m.TopMainPageIsVisible == false && m.ChildMenu.Count() == 0);
            string menuFooterLinksHtml = "";
            foreach (var menuLink in menuFooterLinks)
            {
                menuFooterLinksHtml += $"<a href='{menuLink.Link}' class='footer-bottom__link'>{menuLink.Name}</a>";
            }
            templateHtmlContent = templateHtmlContent.Replace("<!-- Ссылки внизу подвала -->", menuFooterLinksHtml);

            //Вставка контента
            templateHtmlContent = templateHtmlContent.Replace("<!-- Заголовок контента -->", contentTitle);
            templateHtmlContent = templateHtmlContent.Replace("<!-- Контент -->", contentHtml);

            //Генерация html файла
            string pathToFileDirectory = _webHostEnvironment.ContentRootPath + path;
            string pathToFile = pathToFileDirectory + TableHtmlFilePath;

            if (!File.Exists(pathToFile))
            {
                Directory.CreateDirectory(pathToFileDirectory);
                var createdFile = File.Create(pathToFile);
                createdFile.Close();
            }

            File.WriteAllText(pathToFile, templateHtmlContent);
        }

        public void DeleteGeneratedHtmlContent(string path)
        {
            path = path[path.Length - 1].Equals('/') ? path : path + "/";

            string pathToFileDirectory = _webHostEnvironment.ContentRootPath + path;
            Directory.Delete(pathToFileDirectory, true);
        }
    }
}
