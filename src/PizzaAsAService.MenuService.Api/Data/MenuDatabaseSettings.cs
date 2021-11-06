using PizzaAsAService.MenuService.Api.Data.Interfaces;

namespace PizzaAsAService.MenuService.Api.Data
{
    public class MenuDatabaseSettings : IMenuDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
