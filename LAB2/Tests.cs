using Microsoft.Data.Sqlite;
using Minimarket;


namespace MinimarketTests
{
    public class BaseDiscountCardsTest
    {
        protected const string ConnectionString = "Data Source=D:\\BSUIR\\PPOIS\\minimarket_cs\\MinimarketTests\\bin\\Debug\\net6.0\\sample.db;";
        protected const string TableName = "Products";
    }


    [TestClass]
    public class DiscountCardsManagerTests : BaseDiscountCardsTest
    {
        private new const string TableName = "DiscountCards";

        [TestMethod]
        public void TestCreateTable()
        {
            var manager = new DiscountCardsManager(ConnectionString, TableName);

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{TableName}'", connection);

                using (var reader = command.ExecuteReader())
                {
                    Assert.IsTrue(reader.Read(), "Таблица не была создана.");
                }
            }
        }
    }
    
    [TestClass]
    public class DiscountCardsManagerTests1: BaseDiscountCardsTest
    {
        private new const string TableName = "DiscountCards";

        [TestMethod]
        public void TestCreateTable()
        {
            var manager = new DiscountCardsManager(ConnectionString, TableName);

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='Name'", connection);

                using (var reader = command.ExecuteReader())
                {
                    Assert.IsFalse(reader.Read(), "Таблица была создана.");
                }
            }
        }
    }

    [TestClass]
    public class DeleteTableCardsTest: BaseDiscountCardsTest
    {
        private new const string TableName = "DiscountCards";

        [TestMethod]
        public void DeleteTableCards()
        {
            var manager = new DiscountCardsManager(ConnectionString, TableName);
            manager.DeleteTable();

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='Name'", connection);

                using (var reader = command.ExecuteReader())
                {
                    Assert.IsFalse(reader.Read(), "Таблица не была удалена.");
                }

            }
        }
    }

    [TestClass]
    public class CreateMarketFloorDBManagerTest: BaseDiscountCardsTest
    {
        [TestMethod]
        public void TestCreateMarketFloorDBManager()
        {
            var manager = new MarketFloorDBManager(ConnectionString, TableName);

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{TableName}'", connection);

                using (var reader = command.ExecuteReader())
                {
                    Assert.IsTrue(reader.Read(), "Таблица не была создана.");
                }

            }
        }
    }

    [TestClass]
    public class CreateMarketFloorDBManagerTest1: BaseDiscountCardsTest{
        [TestMethod]
        public void TestCreateTable1()
        {
            var manager = new MarketFloorDBManager(ConnectionString, TableName);
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='Name'", connection);

                using (var reader = command.ExecuteReader())
                {
                    Assert.IsFalse(reader.Read(), "Таблица была создана.");
                }

            }
        }
    }

    [TestClass]
    public class DeleteTableProductsTest: BaseDiscountCardsTest{

        [TestMethod]
        public void DeleteTableProducts()
        {
            var manager = new MarketFloorDBManager(ConnectionString, TableName);
            manager.DeleteTable();

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='Name'", connection);

                using (var reader = command.ExecuteReader())
                {
                    Assert.IsFalse(reader.Read(), "Таблица не была удалена.");
                }
            }
        }
    }

    [TestClass]
    public class CashierTest1: BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");
        

        [TestInitialize]
        public void Initialize()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestCashier()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new Product("Product 1", 10.0m, "Description 1");
            marketFloor.AddProductWithQuantity(product1, 2);

            cashier.AddToReceiptByName("Product 1");

            Assert.AreEqual(10.0M, cashier.TotalOrder);
        }
    }

    [TestClass]
    public class CashierTest2 : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");

        [TestInitialize]
        public void Initialize()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestCashier()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new Product("Product 1", 10.0m, "Description 1");
            marketFloor.AddProductWithQuantity(product1, 2);

            
            Assert.ThrowsException<Exception>(() => cashier.AddToReceiptByName("Product 10"));
            Assert.AreEqual(0.0M, cashier.TotalOrder);
        }
    }

    [TestClass]
    public class CashierWeightedTest2 : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");

        [TestInitialize]
        public void Initialize()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestCashier()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            var product1 = new Product("Product 1", 100.0m, "Description 1");
            marketFloor.AddProductWithQuantity(product1, 1);


            Assert.ThrowsException<Exception>(() => cashier.AddToReceiptByName("Wrong Product 1"));
            Assert.AreEqual(0.00M, cashier.TotalOrder);
        }
    }

    [TestClass]
    public class CashierTestByCode1 : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");

        [TestInitialize]
        public void Initialize()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestCashier()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new BarcodeProduct("Barcode Product 1", 454.48m, "Description 1", "487878787887879878");
            marketFloor.AddProductWithQuantity(product1, 2);

            cashier.AddToReceiptByCode("487878787887879878");

            Assert.AreEqual(454.48M, cashier.TotalOrder);
        }
    }

    [TestClass]
    public class CashierTestByCode2 : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");

        [TestInitialize]
        public void Initialize()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestCashier()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new BarcodeProduct("Barcode Product 1", 454.48m, "487878787887879878", "Description 1");
            marketFloor.AddProductWithQuantity(product1, 2);


            Assert.ThrowsException<Exception>(() => cashier.AddToReceiptByCode("Product 10"));
            Assert.ThrowsException<Exception>(() => cashier.AddToReceiptByCode("7887845454654"));
            Assert.AreEqual(0.0M, cashier.TotalOrder);
        }
    }


    [TestClass]
    public class DiscountTestByCode1: BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");

        private const string TableNameCards = "Cards";

        [TestInitialize]
        public void Initialize()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableNameCards}", connection);
                command.ExecuteNonQuery();
            }


        }

        [TestMethod]
        public void TestCashier()
        {
            var discountCardsManager = new DiscountCardsManager(ConnectionString, TableNameCards);
            var card = new DiscountCard("00000000000000000", "Example Example", "000000000000", DateTime.Parse("2023-09-25 22:10:07.6113453"));
            discountCardsManager.AddItem(card);

            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");


            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new Product("Product 1", 100.0m, "Description 1");
            marketFloor.AddProductWithQuantity(product1, 2);

            cashier.AddToReceiptByName("Product 1");

            cashier.ApplyDiscountCard("00000000000000000");

            Assert.AreEqual(95.0M, cashier.TotalOrder);
        }
    }

    [TestClass]
    public class DiscountTestByCode2 : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");
        private const string TableNameCards = "Cards";

        [TestInitialize]
        public void Initialize()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableNameCards}", connection);
                command.ExecuteNonQuery();
            }


        }

        [TestMethod]
        public void TestCashier()
        {
            var discountCardsManager = new DiscountCardsManager(ConnectionString, TableNameCards);
            var card = new DiscountCard("00000000000000000", "Example Example", "000000000000", DateTime.Parse("2021-09-25 22:10:07.6113453"));
            discountCardsManager.AddItem(card);

            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");


            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new Product("Product 1", 200.0m, "Description 1");
            marketFloor.AddProductWithQuantity(product1, 2);

            cashier.AddToReceiptByName("Product 1");

            cashier.ApplyDiscountCard("00000000000000000");

            Assert.AreEqual(90.0M*2, cashier.TotalOrder);
        }
    }

    [TestClass]
    public class DiscountTestByCode3 : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");
        private const string TableNameCards = "Cards";

        [TestInitialize]
        public void Initialize()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);
            // Удалить все данные из таблицы перед выполнением теста
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableNameCards}", connection);
                command.ExecuteNonQuery();
            }


        }

        [TestMethod]
        public void TestCashier()
        {
            var discountCardsManager = new DiscountCardsManager(ConnectionString, TableNameCards);
            var card = new DiscountCard("00000000000000000", "Example Example", "000000000000", DateTime.Parse("2014-09-25 22:10:07.6113453"));
            discountCardsManager.AddItem(card);

            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");


            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new Product("Product 1", 50.0m, "Description 1");
            marketFloor.AddOneProduct(product1);

            cashier.AddToReceiptByName("Product 1");

            cashier.ApplyDiscountCard("00000000000000000");

            Assert.AreEqual(42.50M, cashier.TotalOrder);
        }
    }

    [TestClass]
    public class DiscountCardActivateTest : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");
        private const string TableNameCards = "Cards";

        [TestInitialize]
        public void Initialize()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableNameCards}", connection);
                command.ExecuteNonQuery();
            }


        }

        [TestMethod]
        public void TestCashier()
        {
            var discountCardsManager = new DiscountCardsManager(ConnectionString, TableNameCards);
            var card = new DiscountCard("00000000000000000", "Example Example", "000000000000");
            discountCardsManager.AddItem(card);

            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);
            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");


            Cashier cashier = new Cashier(marketFloor, payment, customersCards);

            cashier.ActivateDiscountCard(card);

            var actualCard = customersCards.GetCardByNumber("00000000000000000");
            Assert.AreEqual(card.Number, actualCard.Number);
            Assert.AreEqual(card.CardHolderName, actualCard.CardHolderName);
            Assert.AreEqual(card.CardHolderPhone, actualCard.CardHolderPhone);
        }
    }

    [TestClass]
    public class EmptyProductTest
    {
        [TestMethod]
        public void TestEmptyProduct()
        {
            var emptyProduct = new Product();

            Assert.AreEqual("", emptyProduct.Name);
            Assert.AreEqual(0.00M, emptyProduct.Price);
            Assert.AreEqual("", emptyProduct.Description);
        }
    }

    [TestClass]
    public class ProductUpdatePriceTest
    {
        [TestMethod]
        public void TestProductUpdatePrice()
        {
            var emptyProduct = new Product();

            emptyProduct.Price = 17.58M;

            Assert.AreEqual(17.58M, emptyProduct.Price);
        }
    }

    [TestClass]
    public class ProductToStringTest
    {
        [TestMethod]
        public void TestProductInfo()
        {
            var product1 = new Product("Product 1", 50.50m, "Description 1");

            var expected = $"{product1.Name} - {product1.Price:C} - {product1.Description}";
            Assert.AreEqual(expected, product1.ToString());
        }
        
        [TestMethod]
        public void TestBarcodeProductInfo()
        {
            var product1 = new BarcodeProduct("Product 1", 50.50m, "Description 1", "48788787494646469456");

            var expected = $"{product1.Name} - {product1.Barcode} - {product1.Price:C} - {product1.Description}";
            Assert.AreEqual(expected, product1.ToString());
        }
    }

    [TestClass]
    public class ProductInfoTest
    {
        [TestMethod]
        public void TestProductInfo()
        {
            var product1 = new Product("Product 1", 50.50m, "Description 1");

            var expected = $"{product1.Name} - {product1.Price:C} - {product1.Description}";
            Assert.AreEqual(expected, product1.ToString());
        }
    }

    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Print_Product_PrintsCorrectly()
        {
            // Arrange
            string expectedOutput = "Название: TestProduct\r\nЦена: 10,00 Br\r\nОписание: TestDescription"; // Замените значения на свои
            Product product = new Product("TestProduct", 10.00M, "TestDescription");

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                product.Print();
                string printedOutput = sw.ToString().Trim();

                // Assert
                StringAssert.Contains(printedOutput, expectedOutput);
            }
        }
    }

    [TestClass]
    public class BarcodeProductTests
    {
        [TestMethod]
        public void Print_BarcodeProduct_PrintsCorrectly()
        {
            // Arrange
            string expectedOutput = "Название: TestProduct\r\nЦена: 10,00 Br\r\nОписание: TestDescription\r\nКод: TestBarcode"; // Замените значения на свои
            BarcodeProduct barcodeProduct = new BarcodeProduct("TestProduct", 10.00M, "TestDescription", "TestBarcode");

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                barcodeProduct.Print();
                string printedOutput = sw.ToString().Trim();

                // Assert
                StringAssert.Contains(printedOutput, expectedOutput);
            }
        }
    }

    [TestClass]
    public class AddItemProducts : BaseDiscountCardsTest
    {

        [TestInitialize]
        public void Initialize()
        {
            // Удалить все данные из таблицы перед выполнением теста
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestAddItemProducts()
        {
            var marketFloorDBManager= new MarketFloorDBManager(ConnectionString, TableName);


            var expected = new Product("Weightded Product 1", 100.0m, "Description 1");
            marketFloorDBManager.AddItem(expected);

            var actual = marketFloorDBManager.Get("Name", "Weightded Product 1");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.Description, actual.Description);


        }
    }

    [TestClass]
    public class DeleteItemProducts : BaseDiscountCardsTest
    {

        [TestInitialize]
        public void Initialize()
        {
            // Удалить все данные из таблицы перед выполнением теста
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestDeleteItemProducts()
        {
            var marketFloorDBManager = new MarketFloorDBManager(ConnectionString, TableName);


            var expected = new Product("Product 1", 100.0m, "Description 1");
            marketFloorDBManager.AddItem(expected);

            var actual = marketFloorDBManager.Get("Name", "Product 1");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.Description, actual.Description);

            marketFloorDBManager.DeleteItem(expected);

            actual = marketFloorDBManager.Get("Name", "Product 1");
            Assert.IsNull(actual);
        }
    }

    [TestClass]
    public class DeleteItemCard : BaseDiscountCardsTest
    {
        private const string TableName = "Cards";

        [TestInitialize]
        public void Initialize()
        {
            // Удалить все данные из таблицы перед выполнением теста
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestDeleteItemCard()
        {
            var discountCardDBManager = new DiscountCardsManager(ConnectionString, TableName);


            var expected = new DiscountCard("00000000000000000", "Example Example", "000000000000");
            discountCardDBManager.AddItem(expected);

            var actual = discountCardDBManager.Get("Number", expected.Number);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Number, actual.Number);
            Assert.AreEqual(expected.CardHolderName, actual.CardHolderName);
            Assert.AreEqual(expected.CardHolderPhone, actual.CardHolderPhone);

            discountCardDBManager.DeleteItem(expected);

            actual = discountCardDBManager.Get("Number", expected.Number);
            Assert.IsNull(actual);
        }
    }


    [TestClass]
    public class GetProduct : BaseDiscountCardsTest
    {

        [TestInitialize]
        public void Initialize()
        {
            // Удалить все данные из таблицы перед выполнением теста
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestGetProductByName()
        {
            var marketFloor = new MarketFloor(ConnectionString, TableName);

            var expected = new Product("Product 1", 100.0m, "Description 1");
            marketFloor.AddOneProduct(expected);

            var actual = marketFloor.GetProductByName("Product 1");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.Description, actual.Description);
        }
    }

    [TestClass]
    public class EmptyBarcodeProduct
    {
        [TestMethod]
        public void TestGetProductByName()
        {
            var barcodeP = new BarcodeProduct();

            Assert.AreEqual(barcodeP.Name, "");
            Assert.AreEqual(barcodeP.Price, 0.00M);
            Assert.AreEqual(barcodeP.Description, "");
            Assert.AreEqual(barcodeP.Barcode, "");
        }
    }

    [TestClass]
    public class CashierTest3 : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");

        [TestInitialize]
        public void Initialize()
        {
            // Удалить все данные из таблицы перед выполнением теста
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestNoMoreProduct()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            Product product1 = new Product("Product 1", 10.0m, "Description 1");
            marketFloor.AddProductWithQuantity(product1, 0);


            Assert.ThrowsException<Exception>(() => cashier.AddToReceiptByName("Product 1"));
            Assert.AreEqual(0.0M, cashier.TotalOrder);
        }
    }

    [TestClass]
    public class Payment : BaseDiscountCardsTest
    {
        private PaymentData payment = new PaymentData(
                "ООО \"Example\"",
                "BY30UNBS31581568200000000784",
                "UNBSBY2X",
                "197845668",
                "Оплата в минимаркете",
                "OTHR",
                "208956");

        [TestInitialize]
        public void Initialize()
        {
            // Удалить все данные из таблицы перед выполнением теста
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqliteCommand($"DELETE FROM {TableName}", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void PaymentTest()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            var product1 = new Product("Product 1", 11.89m, "Description 1");
            var product2 = new BarcodeProduct("Product 2", 20.25m, "Description 2", "545458458455746676746");
            marketFloor.AddProductWithQuantity(product1, 10);
            marketFloor.AddProductWithQuantity(product2, 10);

            cashier.AddToReceiptByName(product1.Name);
            cashier.AddToReceiptByCode(product2.Barcode);
            Assert.AreEqual(11.89m + 20.25m, cashier.TotalOrder);
            cashier.Transaction("1111111111111111", "000");


        }

        [TestMethod]
        public void PaymentUnsuccesfulTest1()
        {
            MarketFloor marketFloor = new MarketFloor(ConnectionString, TableName);

            CustomersCards customersCards = new CustomersCards(ConnectionString, "Cards");

            Cashier cashier = new Cashier(marketFloor, payment, customersCards);
            var product1 = new Product("Product 1", 11.89m, "Description 1");
            var product2 = new BarcodeProduct("Product 2", 20.25m, "Description 2", "545458458455746676746");
            marketFloor.AddProductWithQuantity(product1, 10);
            marketFloor.AddProductWithQuantity(product2, 10);

            cashier.AddToReceiptByName(product1.Name);
            cashier.AddToReceiptByCode(product2.Barcode);

            Assert.AreEqual(11.89m + 20.25m, cashier.TotalOrder);

            Assert.ThrowsException<ArgumentException>(() => cashier.Transaction("11111111aaa1111", "000"));
        }
    }
}