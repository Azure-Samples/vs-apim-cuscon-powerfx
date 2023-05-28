using InventoryApi.Models;

namespace InventoryApi.Repositories
{
    public class InventoryDB
    {
        private static List<Warehouse> _warehouses = new()
        {
            new Warehouse { City = "Seattle" },
            new Warehouse { City = "Redmond" },
            new Warehouse { City = "Tacoma" },
            new Warehouse { City = "Issaquah" },
            new Warehouse { City = "Everett" }
        };

        public static List<Warehouse> GetWarehouses()
        {
            return _warehouses;
        }

        private static List<Item> _items = new()
        {
            new Item() { ItemId = "1", Name = "Pumped Water Controller", Price = 45.9900, Description = "Water pump controller for combination boiler" },
            new Item() { ItemId = "2", Name = "3.5 W / S Heater", Price = 125.5000, Description = "Small heat exchanger for domestic boiler" },
            new Item() { ItemId = "3", Name = "Inlet Valve", Price = 120.2000, Description = "Water inlet valve with one - way operation" }
        };

        public static List<Item> GetItems()
        {
            return _items.ToList();
        }

        private static List<ItemOnHand> _itemsOnHand = new()
        {
            new ItemOnHand { ItemId = "1", City = "Seattle", NumberInStock = 3 },
            new ItemOnHand { ItemId = "2", City = "Seattle", NumberInStock = 2 },
            new ItemOnHand { ItemId = "3", City = "Seattle", NumberInStock = 1 },

            new ItemOnHand { ItemId = "1", City = "Redmond", NumberInStock = 0 },
            new ItemOnHand { ItemId = "2", City = "Redmond", NumberInStock = 0 },
            new ItemOnHand { ItemId = "3", City = "Redmond", NumberInStock = 3 },

            new ItemOnHand { ItemId = "1", City = "Tacoma", NumberInStock = 1 },
            new ItemOnHand { ItemId = "2", City = "Tacoma", NumberInStock = 0 },
            new ItemOnHand { ItemId = "3", City = "Tacoma", NumberInStock = 4 },

            new ItemOnHand { ItemId = "1", City = "Issaquah", NumberInStock = 8 },
            new ItemOnHand { ItemId = "2", City = "Issaquah", NumberInStock = 7 },
            new ItemOnHand { ItemId = "3", City = "Issaquah", NumberInStock = 0 },

            new ItemOnHand { ItemId = "1", City = "Everett", NumberInStock = 0 },
            new ItemOnHand { ItemId = "2", City = "Everett", NumberInStock = 5 },
            new ItemOnHand { ItemId = "3", City = "Everett", NumberInStock = 2 }
        };

        public static List<ItemOnHand> GetItemsOnHand(string ItemId)
        {
            return _itemsOnHand.Where(i => i.ItemId == ItemId).ToList();
        }
    }
}