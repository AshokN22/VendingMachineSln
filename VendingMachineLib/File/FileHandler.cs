using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;
using System.IO;

namespace VendingMachineLib.File
{
    public class FileHandler : IOrderFileHandler, IInventoryFileHandler
    {
        private const string InvFilePath = "inventory.csv";
        private const string OrderFilePath = "orders.csv";
        public Dictionary<string, Item> FetchItems()
        {
            Dictionary<string, Item> items = new Dictionary<string, Item>();
            if(System.IO.File.Exists(InvFilePath))
            {
                FileStream fs = new FileStream(InvFilePath, FileMode.Open, FileAccess.Read);
                if (fs.Length == 0)
                {
                    StreamReader sr = new StreamReader(fs);
                    string data = sr.ReadLine();
                    string[] itmRecord = data.Split(",");
                    Item itm = new Item
                    {
                        ID = int.Parse(itmRecord[0]),
                        Name = itmRecord[1],
                        Quantity = int.Parse(itmRecord[2]),
                        Price = float.Parse(itmRecord[3])
                    };
                    items.Add(itmRecord[0], itm);
                    sr.Dispose();
                    sr.Close();
                }
                else
                {
                    throw new Exception("No items record present in the inventory.");
                }
            }
            else
            {
                throw new Exception("System error with inventory.csv file. Please contact your adminstrator for further assistance.");
            }
            return items;
        }

        public Dictionary<string, Order> FetchOrders()
        {
            Dictionary<string, Order> orders = new Dictionary<string, Order>();
            if (System.IO.File.Exists(OrderFilePath))
            {
                FileStream fs = new FileStream(OrderFilePath, FileMode.Open, FileAccess.Read);
                if (fs.Length == 0)
                {
                    StreamReader sr = new StreamReader(fs);
                    string data = sr.ReadLine();
                    string[] ordRecord = data.Split(",");
                    Order itm = new Order
                    {
                        OID = int.Parse(ordRecord[0]),
                        Amount = float.Parse(ordRecord[1]),
                        Item = new Item { ID = int.Parse(ordRecord[2]) },
                        Quantity = int.Parse(ordRecord[3])
                    };
                    orders.Add(ordRecord[0], itm);
                    sr.Dispose();
                    sr.Close();
                }
                else
                {
                    throw new Exception("No order record present in the orders file.");
                }
            }
            else
            {
                throw new Exception("System error with order.csv file. Please contact your adminstrator for further assistance.");
            }
            return orders;
        }

        public void SaveOrder(Order order)
        {
            if (System.IO.File.Exists(OrderFilePath))
            {
                StreamWriter sw = new StreamWriter(OrderFilePath, true);
                string orderRecord = $"{order.OID},{order.Amount},{order.Item.ID},{order.Quantity}";
                sw.WriteLine(orderRecord);
                sw.Flush();
                sw.Dispose();
                sw.Close();
            }
            else
            {
                throw new Exception("System error with order.csv file. Please contact your adminstrator for further assistance.");
            }
        }
    }
}
