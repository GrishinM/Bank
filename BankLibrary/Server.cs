using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BankLibrary.Requests;
using BankLibrary.Responses;
using Authorization = BankLibrary.Requests.Authorization;

namespace BankLibrary
{
    public class Server
    {
        private readonly TcpListener tcpListener;

        private readonly IDataBase dataBase;

        public event Action Started;

        public event Action Stopped;

        public event Action<Exception> Error;

        public Server()
        {
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            dataBase = new JsonDataBase();
        }

        public void Start()
        {
            try
            {
                tcpListener.Start();
                Started();
                Listen();
            }
            catch (Exception e)
            {
                Error(e);
                Disconnect();
            }
        }

        private void Listen()
        {
            try
            {
                while (true)
                    new Thread(new Client(tcpListener.AcceptTcpClient(), this).MainProcess).Start();
            }
            catch (Exception e)
            {
                Error(e);
                Disconnect();
            }
        }

        public AuthorizationResult Authorize(Authorization authorization)
        {
            AuthorizationResult result;
            var card = dataBase.GetCardById(authorization.CardId);
            if (card.Password == authorization.Password)
                result = new AuthorizationResult {Success = true};
            else
                result = new AuthorizationResult {Success = false, Message = "Неверный пин-код"};
            return result;
        }

        public BalanceResult CheckBalance(int cardId)
        {
            var card = dataBase.GetCardById(cardId);
            var result = new BalanceResult {Balance = card.Balance};
            return result;
        }

        public TransferResult Transfer(int cardFromId, Transfer transfer)
        {
            TransferResult result;
            var cardFrom = dataBase.GetCardById(cardFromId);
            var cardTo = dataBase.GetCardByNumber(transfer.CardToNumber);
            var amount = transfer.Amount;
            if (cardFrom.Balance < amount)
            {
                result = new TransferResult {Success = false, Message = "Недостаточный баланс"};
            }
            else
            {
                cardFrom.Balance -= amount;
                cardTo.Balance += amount;
                dataBase.ApplyChanges(cardFrom);
                dataBase.ApplyChanges(cardTo);
                result = new TransferResult {Success = true};
            }

            return result;
        }

        private void Disconnect()
        {
            tcpListener.Stop();
            Stopped();
        }
    }
}