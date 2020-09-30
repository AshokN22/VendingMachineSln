using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineLib.Entities;
using VendingMachineLib.File;

namespace VendingMachineLib.Processor
{
    public class OrderProcessor : IOrderProcessor
    {
        private IInventoryProcessor inventoryProcessor = null;
        private IOrderFileHandler ordHandler = null;

        public OrderProcessor(IOrderFileHandler handler)
        {
            ordHandler = handler;
        }

        public IInventoryProcessor InventoryProcessor
        {
            get
            {
                return inventoryProcessor;
            }
            set
            {
                inventoryProcessor = value;
            }
        }

        public async Task<Dictionary<string, Order>> GetOrders()
        {
            return await ordHandler.FetchOrders();
        }

        public async Task<string> SaveOrder(Order order)
        {
            var items = await inventoryProcessor.GetItems();
            var orders = await ordHandler.FetchOrders();
            if (items.ContainsKey(order.Item.ID.ToString()))
            {
                Item item = items[order.Item.ID.ToString()];
                if (order.Quantity <= item.Quantity)
                {
                    if (order.Amount == (order.Quantity * item.Price))
                    {
                        order.OID = (orders.Values.Max(o => o.OID) + 1);
                        await ordHandler.SaveOrder(order);
                        return "Your Order submission is successful.";
                    }
                    else
                    {
                        throw new Exception($"Your Order submission is unsuccessful, As order amount is wrong it needs to be {(order.Quantity * item.Price).ToString("c")}");
                    }
                }
                else
                {
                    throw new Exception("Your Order submission is unsuccessful, Due to insufficient inventory of item.");
                }
            }
            else
            {
                throw new Exception("Your Order submission is unsuccessful, As Item is not present in the inventory.");
            }
        }
    }
}
