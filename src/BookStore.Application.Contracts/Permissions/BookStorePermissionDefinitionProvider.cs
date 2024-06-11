using BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));

        var booksPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books.Delete"));
        var authorsPermission = bookStoreGroup.AddPermission(
    BookStorePermissions.Authors.Default, L("Permission:Authors"));
        authorsPermission.AddChild(
            BookStorePermissions.Authors.Create, L("Permission:Authors.Create"));
        authorsPermission.AddChild(
            BookStorePermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authorsPermission.AddChild(
            BookStorePermissions.Authors.Delete, L("Permission:Authors.Delete"));

        var stockPermission = bookStoreGroup.AddPermission(BookStorePermissions.Stocks.Default, L("Permission:Stocks"));
        stockPermission.AddChild(BookStorePermissions.Stocks.Create, L("Permission:Stocks.Create"));
        stockPermission.AddChild(BookStorePermissions.Stocks.Edit, L("Permission:Stocks.Edit"));
        stockPermission.AddChild(BookStorePermissions.Stocks.Delete, L("Permission:Stocks.Delete"));

        var ReservationPermission = bookStoreGroup.AddPermission(BookStorePermissions.Reservations.Default, L("Permission:Reservations"));
        ReservationPermission.AddChild(BookStorePermissions.Reservations.Create, L("Permission:Reservations.Create"));
        ReservationPermission.AddChild(BookStorePermissions.Reservations.Edit, L("Permission:Reservations.Edit"));
        ReservationPermission.AddChild(BookStorePermissions.Reservations.Delete, L("Permission:Reservations.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
