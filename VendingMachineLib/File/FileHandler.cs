using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;
using System.IO;
using System.Threading.Tasks;

namespace VendingMachineLib.File
{
    public class FileHandler : IOrderFileHandler, IInventoryFileHandler
    {
        private string invFilePath = null;
        private string orderFilePath = null;

        public string OrderCSVPath
        { 
            get 
            { 
                return orderFilePath; 
            }
            set 
            { 
                orderFilePath = value; 
            }
        }
        public string ItemCSVPath 
        { 
            get 
            {
                return invFilePath;
            }
            set 
            {
                invFilePath = value;
            } 
        }

        public async Task<Dictionary<string, Item>> FetchItems()
        {
            Dictionary<string, Item> items = new Dictionary<string, Item>();
            if(System.IO.File.Exists(invFilePath))
            {
                StreamReader sr = new StreamReader(invFilePath);
                if (!sr.EndOfStream)
                {
                    while (!sr.EndOfStream)
                    {
                        string data = await sr.ReadLineAsync();
                        string[] itmRecord = data.Split(",");
                        
                        Item itm = new Item
                        {
                            ID = int.Parse(itmRecord[0]),
                            Name = itmRecord[1],
                            Quantity = int.Parse(itmRecord[2]),
                            Price = float.Parse(itmRecord[3])
                        };
                        items.Add(itmRecord[0], itm);
                    }
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

        public async Task<Dictionary<string, Order>> FetchOrders()
        {
            Dictionary<string, Order> orders = new Dictionary<string, Order>();
            if (System.IO.File.Exists(orderFilePath))
            {
                StreamReader sr = new StreamReader(orderFilePath);
                if (!sr.EndOfStream)
                {
                    while (!sr.EndOfStream)
                    {
                        string data = await sr.ReadLineAsync();
                        string[] ordRecord = data.Split(",");
                        Order itm = new Order
                        {
                            OID = int.Parse(ordRecord[0]),
                            Amount = float.Parse(ordRecord[1]),
                            Item = new Item { ID = int.Parse(ordRecord[2]) },
                            Quantity = int.Parse(ordRecord[3])
                        };
                        orders.Add(ordRecord[0], itm);
                    }
                    sr.Dispose();
                    sr.Close();
                }
                else
                {
                    throw new Exception("No orders record present in the OrderFile.");
                }
            }
            else
            {
                throw new Exception("System error with orders.csv file. Please contact your adminstrator for further assistance.");
            }
            return orders;
        }

        public async Task SaveOrder(Order order)
        {
            if (System.IO.File.Exists(orderFilePath))
            {
                StreamWriter sw = new StreamWriter(orderFilePath, true);
                string orderRecord = $"{order.OID},{order.Amount},{order.Item.ID},{order.Quantity}";
                await sw.WriteLineAsync(orderRecord);
                sw.Flush();
                sw.Dispose();
                sw.Close();
            }
            else
            {
                throw new Exception("System error with orders.csv file. Please contact your adminstrator for further assistance.");
            }
        }
    }
}
