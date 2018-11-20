using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Model;

namespace DataLayer
{
    public class ServerBinder
    {
        private McDonaldsDataManager _dataManager;

        async public void StartListening()
        {
            _dataManager = new McDonaldsDataManager();
            var ip = Dns.GetHostEntry("localhost").AddressList[0];
            var listener = new TcpListener(ip, 8080);
            var client = default(TcpClient);
            try
            {
                listener.Start();
                Console.WriteLine("listening for client...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            while (true)
            {
                client = listener.AcceptTcpClient();
                byte[] receivedBuffer = new byte[4096];
                NetworkStream stream = client.GetStream();

                stream.Read(receivedBuffer, 0, receivedBuffer.Length);
                var sendingObject = FromByteArray<SendingObject>(receivedBuffer);
                Console.WriteLine("\n###############\n");
                Console.WriteLine(sendingObject.status);
                Console.WriteLine("===============");
                ResponseObject response = new ResponseObject();
                switch (sendingObject.status)
                {
                    case Status.REGISTRATION:
                    {
                        _dataManager.CreateUser(sendingObject.user);
                        response.status = Status.REGISTRATION;
                        Console.WriteLine(sendingObject.user.first_name + " " + sendingObject.user.last_name);
                            break;
                    }
                    case Status.LOGIN:
                    {
                        Model.User user = getUser(sendingObject.email, sendingObject.password);
                        response.status = Status.LOGIN;
                        response.user = user;
                        Console.WriteLine(user.first_name + " " + user.last_name);
                        break;
                    }
                    case Status.MAIN_DISHES:
                    {
                        var dishes = _dataManager.SelectMainDishes();
                        response.MainDishes = dishes;
                        response.status = Status.MAIN_DISHES;
                        foreach (var dish in dishes)
                        {
                            Console.WriteLine(dish.name);
                        }
                        break;
                    }
                    case Status.CATEGORY_DISHES:
                    {
                        var dishes = _dataManager.SelectCategoryDishes(sendingObject.mainDishId);
                        response.CategoryDishes = dishes;
                        response.status = Status.CATEGORY_DISHES;
                        foreach (var dish in dishes)
                        {
                            Console.WriteLine(dish.name);
                        }
                            break;
                    }
                    case Status.CREATE_ORDER:
                    {
                        var order = _dataManager.CreateOrder(sendingObject.sumPrice, sendingObject.eatIn, 
                            sendingObject.firstName, sendingObject.lastName);
                        response.Order = order;
                        response.status = Status.CREATE_ORDER;
                        Console.WriteLine(order.Id);
                        Console.WriteLine(order.order_date);
                        Console.WriteLine(order.sum_price);
                        Console.WriteLine(order.is_payed);
                        break;
                    }
                    case Status.CREATE_ORDERED_FOOD:
                    {
                        _dataManager.CreateOrderedFood(sendingObject.orderedFood);
                        Console.WriteLine("Ordered Food was added to the database");
                        break;
                    }
                    case Status.REMOVE_FOOD_FROM_CATEGORY_DISHES:
                    {
                        _dataManager.RemoveFoodFromCategoryDishes(sendingObject.categoryDishId, sendingObject.number);
                        break;
                    }
                    case Status.CHECK_CREDIT_CARD:
                    {
                        var res = _dataManager.FinishPayment(sendingObject.cardNumber, 
                            sendingObject.cardName, sendingObject.cardDate, sendingObject.orderId);
                        response.ticketNumber = res;
                        response.status = Status.CHECK_CREDIT_CARD;
                        Console.WriteLine(res);
                        break;
                    }
                }

                var bytes = ToByteArray(response);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        private Model.User getUser(string email, string password)
        {
            return _dataManager.SelectUserByEmailAndPassword(email, password);
        }

        private T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

        private byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
