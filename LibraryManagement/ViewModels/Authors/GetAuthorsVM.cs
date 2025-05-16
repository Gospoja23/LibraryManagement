using LibraryManagement.ViewModels.Users;

namespace LibraryManagement.ViewModels.Authors
{
    //мы возвращаем весь класс вместо списка авторов для того чтобы в случае необходимости изменения содержимого(пейджер) данного класса не пришлось менять структуру в нескольких местах
    public class GetAuthorsVM
    {
        public List<GetAuthorVM> Items { get; set; }=default!;
    }
}
