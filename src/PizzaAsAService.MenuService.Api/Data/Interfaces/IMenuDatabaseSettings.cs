namespace PizzaAsAService.MenuService.Api.Data.Interfaces;

public interface IMenuDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
