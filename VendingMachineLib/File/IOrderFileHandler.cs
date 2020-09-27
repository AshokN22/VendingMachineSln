using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachineLib.Entities;

namespace VendingMachineLib.File
{
    public interface IOrderFileHandler
    {
        async Task<Dictionary<string, Order>> FetchOrders();
        Task SaveOrder(Order order);

    }
}
