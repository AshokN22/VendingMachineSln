using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineLib.Entities;
using VendingMachineLib.File;

namespace VendingMachineLib.Processor
{
    public class InventoryProcessor : IInventoryProcessor
    {
        private IOrderProcessor orderProcessor = null;
        private IInventoryFileHandler invhandler = null;

        public InventoryProcessor(IOrderProcessor ordProcessor)
        {
            orderProcessor = ordProcessor;
            invhandler = new FileHandler();
        }

        public async Task<Dictionary<string, Item>> GetItems()
        {
            var orders = await orderProcessor.GetOrders();
            var items = await invhandler.FetchItems();
            foreach(var itmKey in items.Keys)
            {
                int sum = orders?.Values.Where(o => o.Item.ID == int.Parse(itmKey)).Sum(o => o.Item.Quantity)??0;
                items[itmKey].Quantity -= sum;
            }
            return items;
        }
    }
}
