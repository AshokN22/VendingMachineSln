using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineLib.Entities;
using VendingMachineLib.File;
using VendingMachineLib.Processor;
using Xunit;

namespace VendingMachineLib.Tests
{
    
    public class OrderProcessorTests
    {
        [Fact]
        public async void Can_GetOrders_Test() 
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\OrderExist.csv";
            IOrderProcessor oproc = new OrderProcessor(handler);
            IInventoryProcessor invproc = new InventoryProcessor(handler);
            Order ord1 = new Order { OID = 1, Amount = 400.0f, Item = new Item { ID = 1 }, Quantity = 2 };
            Order ord2 = new Order { OID = 2, Amount = 10000.0f, Item = new Item { ID = 2 }, Quantity = 4 };
            Order ord3 = new Order { OID = 3, Amount = 400.0f, Item = new Item { ID = 1 }, Quantity = 2 };

            //Act
            var result = await oproc.GetOrders();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<Dictionary<string, Order>>(result);
            Assert.Equal(ord1, result["1"],new OrderComparer());
            Assert.Equal(ord2, result["2"], new OrderComparer());
            Assert.Equal(ord3, result["3"], new OrderComparer());
        }

        [Fact]
        public async void Cannot_GetOrders_with_emptyfiles_Test()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\eorders.csv";
            IOrderProcessor oproc = new OrderProcessor(handler);
            IInventoryProcessor invproc = new InventoryProcessor(handler);
            string expectedErrorMsg = "No orders record present in the OrderFile.";

            //Act
            Func<Task<Dictionary<string,Order>>> ofct = () => oproc.GetOrders();

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ofct);
            
            Assert.Equal(expectedErrorMsg, ex.Message);
        }

        [Fact]
        public async void Cannot_GetOrders_As_File_Doesnot_Exist_Test()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\ordernotexist.csv";
            IOrderProcessor oproc = new OrderProcessor(handler);
            IInventoryProcessor invproc = new InventoryProcessor(handler);
            string expectedErrorMsg = "System error with orders.csv file. Please contact your adminstrator for further assistance.";

            //Act
            Func<Task<Dictionary<string, Order>>> ofct = () => oproc.GetOrders();

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ofct);

            Assert.Equal(expectedErrorMsg, ex.Message);
        }

        [Fact]
        public async void Can_SaveOrder_Tests()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
            IOrderProcessor oproc = new OrderProcessor(handler);
            IInventoryProcessor invproc = new InventoryProcessor(handler);
            oproc.InventoryProcessor = invproc;
            Order order = new Order { Amount = 400.0f, Item = new Item { ID = 1 }, Quantity = 2 };
            var orders = (await oproc.GetOrders());
            int newID = orders.Values.Max(o => o.OID) + 1;
            string successMsg = "Your Order submission is successful.";

            //Act
            string message = await oproc.SaveOrder(order);

            //Assert
            var porders = await oproc.GetOrders();
            var dorders = porders.Values.Except(orders.Values, new OrderComparer()).FirstOrDefault();
            order.OID = newID;
            Assert.NotNull(dorders);
            Assert.IsType<Order>(dorders);
            Assert.Equal(order, dorders, new OrderComparer());
            Assert.Equal(successMsg, message);
        }

        [Fact]
        public async void Cannot_SaveOrder_As_File_Doesnot_Exist_Tests()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\ordersnotexist.csv";
            IOrderProcessor oproc = new OrderProcessor(handler);
            IInventoryProcessor invproc = new InventoryProcessor(handler);
            oproc.InventoryProcessor = invproc;
            Order order = new Order { Amount = 400.0f, Item = new Item { ID = 1 }, Quantity = 2 };
            string expectedErrorMsg = "System error with orders.csv file. Please contact your adminstrator for further assistance.";

            //Act
            Func<Task> ofct = () => oproc.SaveOrder(order);

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ofct);
            Assert.Equal(expectedErrorMsg, ex.Message);
        }

        [Fact]
        public async void Cannot_SaveOrder_As_Item_Doesnot_Exist_Tests()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
            IOrderProcessor oproc = new OrderProcessor(handler);
            IInventoryProcessor invproc = new InventoryProcessor(handler);
            oproc.InventoryProcessor = invproc;
            Order order = new Order { Amount = 400.0f, Item = new Item { ID = 6 }, Quantity = 2 };
            string expectedErrorMsg = "Your Order submission is unsuccessful, As Item is not present in the inventory.";

            //Act
            Func<Task> ofct = () => oproc.SaveOrder(order);

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ofct);
            Assert.Equal(expectedErrorMsg, ex.Message);
        }

        [Fact]
        public async void Cannot_SaveOrder_As_Item_Quantity_Is_UnAvailable_Tests()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
            IOrderProcessor oproc = new OrderProcessor(handler);
            IInventoryProcessor invproc = new InventoryProcessor(handler);
            oproc.InventoryProcessor = invproc;
            Order order = new Order { Amount = 400.0f, Item = new Item { ID = 1 }, Quantity = 50 };
            string expectedErrorMsg = "Your Order submission is unsuccessful, Due to insufficient inventory of item.";

            //Act
            Func<Task> ofct = () => oproc.SaveOrder(order);

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ofct);
            Assert.Equal(expectedErrorMsg, ex.Message);
        }

        [Fact]
        public async void Cannot_SaveOrder_As_Amount_Is_Wrong_Tests()
        {
            //Arrange
            FileHandler handler = new FileHandler();
            handler.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
            handler.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
            IOrderProcessor oproc = new OrderProcessor(handler);
            IInventoryProcessor invproc = new InventoryProcessor(handler);
            oproc.InventoryProcessor = invproc;
            Order order = new Order { Amount = 100.0f, Item = new Item { ID = 1 }, Quantity = 2 };
            string expectedErrorMsg = $"Your Order submission is unsuccessful, As order amount is wrong it needs to be $400.00";

            //Act
            Func<Task> ofct = () => oproc.SaveOrder(order);

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(ofct);
            Assert.Equal(expectedErrorMsg, ex.Message);
        }

    }
}
