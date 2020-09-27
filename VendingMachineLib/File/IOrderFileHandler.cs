using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.File
{
    public interface IOrderFileHandler
    {
        Dictionary<string, Order> FetchOrders();
        void SaveOrder(Order order);

    }
}
