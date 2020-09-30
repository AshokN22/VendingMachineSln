using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineLib.Entities;
using VendingMachineLib.File;
using VendingMachineLib.Processor;
using Xunit;

namespace VendingMachineLib.Tests
{
    public class InventoryProcessorTests
    {
        [Fact]
        public async void Can_GetItems_Without_Order_Test()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\eorders.csv";
            Item t1 = new Item { ID = 1, Name = "Mouse", Price = 200.0f, Quantity = 20 };
            Item t2 = new Item { ID = 2, Name = "Ram", Price = 2500.0f, Quantity = 10 };
            Item t3 = new Item { ID = 3, Name = "Keyboard", Price = 800.0f, Quantity = 5 };
            IInventoryProcessor invProc = new InventoryProcessor(handler);
            IOrderProcessor ordProc = new OrderProcessor(handler);
            invProc.OrderProdcessor = ordProc;

            //Act
            var result = await invProc.GetItems();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Dictionary<string, Item>>(result);
            Assert.Equal(t1, result["1"], new ItemComparer());
            Assert.Equal(t2, result["2"], new ItemComparer());
            Assert.Equal(t3, result["3"], new ItemComparer());
        }

        [Fact]
        public async void Can_GetItems_With_Order_Test()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\OrderExist.csv";
            Item t1 = new Item { ID = 1, Name = "Mouse", Price = 200.0f, Quantity = 16};
            Item t2 = new Item { ID = 2, Name = "Ram", Price = 2500.0f, Quantity = 6 };
            Item t3 = new Item { ID = 3, Name = "Keyboard", Price = 800.0f, Quantity = 5 };
            IInventoryProcessor invProc = new InventoryProcessor(handler);
            IOrderProcessor ordProc = new OrderProcessor(handler);
            invProc.OrderProdcessor = ordProc;

            //Act
            var result = await invProc.GetItems();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Dictionary<string, Item>>(result);
            Assert.Equal(t1, result["1"], new ItemComparer());
            Assert.Equal(t2, result["2"], new ItemComparer());
            Assert.Equal(t3, result["3"], new ItemComparer());
        }

        [Fact]
        public async void Cannot_GetItems_With_emptyfile_Test()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\einventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
            IInventoryProcessor invProc = new InventoryProcessor(handler);
            IOrderProcessor ordProc = new OrderProcessor(handler);
            invProc.OrderProdcessor = ordProc;
            string expectedErrorMsg = "No items record present in the inventory.";

            //Act
            Func<Task<Dictionary<string,Item>>> ifct = () => invProc.GetItems();

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ifct);
            Assert.Equal(expectedErrorMsg, ex.Message);
        }

        [Fact]
        public async void Cannot_GetItems_As_File_Doesnot_Exist_Test()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventorynotexist.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
            IInventoryProcessor invProc = new InventoryProcessor(handler);
            IOrderProcessor ordProc = new OrderProcessor(handler);
            invProc.OrderProdcessor = ordProc;
            string expectedErrorMsg = "System error with inventory.csv file. Please contact your adminstrator for further assistance.";

            //Act
            Func<Task<Dictionary<string, Item>>> ifct = () => invProc.GetItems();

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ifct);
            Assert.Equal(expectedErrorMsg, ex.Message);
        }


    }
}
