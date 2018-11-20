using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Model;

namespace ServerConnection
{
    public class ClientBinder
    {
        private static ClientBinder instance;

        private string serverIp = "localhost";
        private int port = 8080;

        public static ClientBinder GetInstance()
        {
            if (instance == null)
            {
                instance = new ClientBinder();
            }

            return instance;
        }

        private ClientBinder()
        {
            
        }

        public List<Model.MainDish> SelectMainDishes()
        {
            List<Model.MainDish> dishes = null;
            try
            {
                TcpClient msgClient = new TcpClient(serverIp, port);
                NetworkStream msgStream = msgClient.GetStream();

                SendingObject sending = new SendingObject
                {
                    status = Status.MAIN_DISHES
                };
                byte[] msgData = ToByteArray(sending);

                msgStream.Write(msgData, 0, msgData.Length);

                byte[] responseData = new byte[msgClient.ReceiveBufferSize];
                msgStream.Read(responseData, 0, responseData.Length);
                ResponseObject response = FromByteArray<ResponseObject>(responseData);
                dishes = response.MainDishes;
                msgStream.Close();
                msgClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return dishes;
        }

        public List<Model.CategoryDish> SelectCategoryDishes(int mainDishId)
        {
            List<Model.CategoryDish> dishes = null;
            try
            {
                TcpClient msgClient = new TcpClient(serverIp, port);
                NetworkStream msgStream = msgClient.GetStream();

                SendingObject sending = new SendingObject
                {
                    status = Status.CATEGORY_DISHES,
                    mainDishId = mainDishId
                };
                byte[] msgData = ToByteArray(sending);

                msgStream.Write(msgData, 0, msgData.Length);

                byte[] responseData = new byte[msgClient.ReceiveBufferSize];
                msgStream.Read(responseData, 0, responseData.Length);
                ResponseObject response = FromByteArray<ResponseObject>(responseData);
                dishes = response.CategoryDishes;
                msgStream.Close();
                msgClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return dishes;
        }

        public Model.User SelectUserByEmailAndPassword(string email, string password)
        {
            User user = null;
            try
            {
                TcpClient msgClient = new TcpClient(serverIp, port);
                NetworkStream msgStream = msgClient.GetStream();

                SendingObject sending = new SendingObject
                {
                    email = email,
                    password = password,
                    status = Status.LOGIN
                };
                byte[] msgData = ToByteArray(sending);

                msgStream.Write(msgData, 0, msgData.Length);

                byte[] responseData = new byte[msgClient.ReceiveBufferSize];
                msgStream.Read(responseData, 0, responseData.Length);
                ResponseObject response = FromByteArray<ResponseObject>(responseData);
                user = response.user;
                msgStream.Close();
                msgClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return user;
        }

        public void RemoveFoodFromCategoryDishes(int categoryDishId, int number)
        {
            try
            {
                TcpClient msgClient = new TcpClient(serverIp, port);
                SendingObject sending = new SendingObject
                {
                    categoryDishId = categoryDishId,
                    number = number,
                    status = Status.REMOVE_FOOD_FROM_CATEGORY_DISHES
                };
                byte[] msgData = ToByteArray(sending);

                NetworkStream msgStream = msgClient.GetStream();
                msgStream.Write(msgData, 0, msgData.Length);
                msgStream.Close();
                msgClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Order CreateOrder(decimal sumPrice, bool eatIn, string firstName, string lastName)
        {
            Order order = null;
            try
            {
                TcpClient msgClient = new TcpClient(serverIp, port);
                NetworkStream msgStream = msgClient.GetStream();

                SendingObject sending = new SendingObject
                {
                    sumPrice = sumPrice,
                    eatIn = eatIn,
                    firstName = firstName,
                    lastName = lastName,
                    status = Status.CREATE_ORDER
                };
                byte[] msgData = ToByteArray(sending);

                msgStream.Write(msgData, 0, msgData.Length);

                byte[] responseData = new byte[msgClient.ReceiveBufferSize];
                msgStream.Read(responseData, 0, responseData.Length);
                ResponseObject response = FromByteArray<ResponseObject>(responseData);
                order = response.Order;
                msgStream.Close();
                msgClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return order;
        }

        public void CreateOrderedFood(List<Model.OrderedFood> orderedFood)
        {
            try
            {
                TcpClient msgClient = new TcpClient(serverIp, port);
                SendingObject sending = new SendingObject { orderedFood = orderedFood, status = Status.CREATE_ORDERED_FOOD };
                byte[] msgData = ToByteArray(sending);

                NetworkStream msgStream = msgClient.GetStream();
                msgStream.Write(msgData, 0, msgData.Length);
                msgStream.Close();
                msgClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CreateUser(Model.User user)
        {
            try
            {
                TcpClient msgClient = new TcpClient(serverIp, port);
                SendingObject sending = new SendingObject {user = user, status = Status.REGISTRATION};
                byte[] msgData = ToByteArray(sending);

                NetworkStream msgStream = msgClient.GetStream();
                msgStream.Write(msgData, 0, msgData.Length);
                msgStream.Close();
                msgClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public int FinishPayment(string cardNumber, string cardName, string cardDate, int orderId)
        {
            try
            {
                TcpClient msgClient = new TcpClient(serverIp, port);
                NetworkStream msgStream = msgClient.GetStream();

                SendingObject sending = new SendingObject
                {
                    orderId = orderId,
                    cardNumber = cardNumber,
                    cardName = cardName,
                    cardDate = cardDate,
                    status = Status.CHECK_CREDIT_CARD
                };
                byte[] msgData = ToByteArray(sending);

                msgStream.Write(msgData, 0, msgData.Length);

                byte[] responseData = new byte[msgClient.ReceiveBufferSize];
                msgStream.Read(responseData, 0, responseData.Length);
                ResponseObject response = FromByteArray<ResponseObject>(responseData);
                msgStream.Close();
                msgClient.Close();
                return response.ticketNumber;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return 0;
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
