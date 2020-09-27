using System;
using System.Collections.Generic;
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

        public OrderProcessor(IInventoryProcessor invProcessor)
        {
            inventoryProcessor = invProcessor;
            ordHandler = new FileHandler();
        }

        public async Task<Dictionary<string, Order>> GetOrders()
        {
            return await ordHandler.FetchOrders();
        }

        public async Task<string> SaveOrder(Order order)
        {
            var items = await inventoryProcessor.GetItems();
            if (items.ContainsKey(order.Item.ID.ToString()))
            {
                int aqty = items[order.Item.ID.ToString()].Quantity;
                if (order.Quantity <= aqty)
                {
                    await ordHandler.SaveOrder(order);
                    return "Your Order submission is successful.";
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
