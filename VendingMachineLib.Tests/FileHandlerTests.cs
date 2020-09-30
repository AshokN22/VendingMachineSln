using Moq;
using System;
using System.IO;
using Xunit;
using VendingMachineLib.File;
using System.Collections.Generic;
using VendingMachineLib.Entities;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VendingMachineLib.Tests
{
    public class FileHandlerTests
    {
        [Fact]
        public async void Can_FetchItems_Test()
        {
            //Arrange
            IInventoryFileHandler invfh = new FileHandler();
            invfh.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            Item t1 = new Item { ID = 1, Name = "Mouse", Quantity = 20, Price = 200.0f };
            Item t2 = new Item { ID = 2, Name = "Ram", Quantity = 10, Price = 2500.0f };
            Item t3 = new Item { ID = 3, Name = "Keyboard", Quantity = 5, Price = 800.0f };

            //Act
            var result = await invfh.FetchItems();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal<Item>(t1, result["1"], new ItemComparer());
            Assert.Equal<Item>(t2, result["2"], new ItemComparer());
            Assert.Equal<Item>(t3, result["3"], new ItemComparer());
        }

        [Fact]
        public async void Cannot_FetchItems_with_emptyfile_Test()
        {
            //Arrange
            IInventoryFileHandler invfh = new FileHandler();
            invfh.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\einventory.csv";
            string expectedErrorMsg = "No items record present in the inventory.";

            //Act
            Func<Task<Dictionary<string, Item>>> fnct = () =>  invfh.FetchItems(); 

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(fnct);
            Assert.Equal(expectedErrorMsg,ex.Message);            
        }

        [Fact]
        public async void Cannot_FetchItems_As_File_Doesnot_Exist_Test()
        {
            //Arrange
            IInventoryFileHandler invfh = new FileHandler();
            invfh.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventorynotExist.csv";
            string expectedErrorMsg = "System error with inventory.csv file. Please contact your adminstrator for further assistance.";

            //Act
            Func<Task<Dictionary<string, Item>>> fnct = () => invfh.FetchItems(); ;

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(fnct);
            Assert.Equal(expectedErrorMsg, ex.Message);
        }

        [Fact]
        public async void Cannot_FetchOrders_As_File_Doesnot_Exist_Tests() 
        {
            //Arrange
            IOrderFileHandler ordfh = new FileHandler();
            ordfh.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\ordersNotExist.csv";
            string expectedErrorMsg = "System error with orders.csv file. Please contact your adminstrator for further assistance.";

            //Act
            Func<Task<Dictionary<string, Order>>> ofct = () => ordfh.FetchOrders();

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ofct);
            Assert.Equal(expectedErrorMsg,ex.Message);
        }

        [Fact]
        public async void Cannot_FetchOrders_with_emptyfile_Tests()
        {
            //Arrange
            IOrderFileHandler ordfh = new FileHandler();
            ordfh.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\eorders.csv";
            string expectedErrorMsg = "No orders record present in the OrderFile.";

            //Act
            Func<Task<Dictionary<string, Order>>> ofct = () => ordfh.FetchOrders();

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ofct);
            Assert.Equal(expectedErrorMsg, ex.Message);
        }

        [Fact]
        public async void Can_FetchOrders_Tests()
        {
            //Arrange
            IOrderFileHandler ordfh = new FileHandler();
            ordfh.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
            Order ord1 = new Order { OID = 1, Amount = 400.0f, Item = new Item { ID = 1 }, Quantity = 2 };
            Order ord2 = new Order { OID = 2, Amount = 10000.0f, Item = new Item { ID = 2 }, Quantity = 4 };

            //Act
            var result = (await ordfh.FetchOrders()).Values.Take(2).ToList();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal<Order>(ord1, result[0], new OrderComparer());
            Assert.Equal<Order>(ord2, result[1], new OrderComparer());
        }

        [Fact]
        public async void Can_SaveOrder_Tests()
        {
            //Arrange
            IOrderFileHandler ordfh = new FileHandler();
            ordfh.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
            var orders = await ordfh.FetchOrders();
            Order order = new Order { OID = (orders.Values.Max(o => o.OID) + 1), Amount = 400.0f, Item = new Item { ID = 1 }, Quantity = 2 };

            //Act
            await ordfh.SaveOrder(order);

            //Assert
            var porders = await ordfh.FetchOrders();
            var dorders = porders.Values.Except(orders.Values, new OrderComparer()).FirstOrDefault();

            Assert.NotNull(dorders);
            Assert.IsType<Order>(dorders);
            Assert.Equal(order, dorders,new OrderComparer());
        }

        [Fact]
        public async void Cannot_SaveOrder_As_File_Doesnot_Exist_Tests()
        {
            //Arrange
            IOrderFileHandler ordfh = new FileHandler();
            ordfh.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\ordersNotExist.csv";
            Order order = new Order { OID = 1, Amount = 400.0f, Item = new Item { ID = 1 }, Quantity = 2 };
            string expectedErrorMsg = "System error with orders.csv file. Please contact your adminstrator for further assistance.";

            //Act
            Func<Task> ofct = () => ordfh.SaveOrder(order);

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ofct);
            Assert.Equal(expectedErrorMsg, ex.Message);
        }
    }
}
