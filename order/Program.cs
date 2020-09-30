using System;
using VendingMachineLib.Entities;
using VendingMachineLib.File;
using VendingMachineLib.Processor;

namespace order
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FileHandler fh = new FileHandler();
                fh.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
                fh.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
                IOrderProcessor ordproc = new OrderProcessor(fh);
                ordproc.InventoryProcessor = new InventoryProcessor(fh);
                VendingMachineLib.Entities.Order ord = new VendingMachineLib.Entities.Order
                {
                    OID = 0,
                    Amount = float.Parse(args[0]),
                    Item = new Item
                    {
                        ID = int.Parse(args[1])
                    },
                    Quantity = int.Parse(args[2])
                };
                Console.WriteLine(ordproc.SaveOrder(ord).Result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
