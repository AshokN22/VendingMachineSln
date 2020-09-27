using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.File
{
    public class FileHandler : IOrderFileHandler, IInventoryFileHandler
    {
        private const string InvFilePath = "inventory.csv";
        private const string OrderFilePath = "orders.csv";
        public Dictionary<string, Item> FetchItems()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, Order> FetchOrders()
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
