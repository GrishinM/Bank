using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using BankLibrary.Requests;
using BankLibrary.Responses;
using Authorization = BankLibrary.Requests.Authorization;

namespace BankLibrary
{
    public class ATM
    {
        private readonly TcpClient client;

        private NetworkStream stream;

        private PhysicalCard card;

        public int CardId => card.Chip.CardId;

        private bool isConnected => client.Connected;

        public event Action AuthorizationSucceeded;

        public event Action<string> AuthorizationFailed;

        public event Action TransferSucceeded;

        public event Action<string> TransferFailed;

        public event Action<double> BalanceChanged;

        public event Action<Exception> Error;

        public ATM()
        {
            client = new TcpClient();
        }

        public void AcceptCard(PhysicalCard physicalCard)
        {
            card = physicalCard;
        }

        public void ThrowCard()
        {
            card = null;
        }

        private void Connect()
        {
            if (isConnected)
                return;
            client.Connect(IPAddress.Parse("127.0.0.1"), 8888);
            stream = client.GetStream();
            new Thread(Listen).Start();
        }

        private void Listen()
        {
            try
            {
                while (true)
                {
                    var response = JsonSerializer.Deserialize<Response>(GetResponse());
                    var data = response.Data;
                    switch (response.Type)
                    {
                        case ResponseType.AuthorizationResult:
                            var authorizationResult = JsonSerializer.Deserialize<AuthorizationResult>(data);
                            if (authorizationResult.Success)
                                AuthorizationSucceeded();
                            else
                                AuthorizationFailed(authorizationResult.Message);
                            break;
                        case ResponseType.TransferResult:
                            var transferResult = JsonSerializer.Deserialize<TransferResult>(data);
                            if (transferResult.Success)
                                TransferSucceeded();
                            else
                                TransferFailed(transferResult.Message);
                            break;
                        case ResponseType.BalanceResult:
                            var balanceResult = JsonSerializer.Deserialize<BalanceResult>(data);
                            BalanceChanged(balanceResult.Balance);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            catch (Exception e)
            {
                Error(e);
            }
            finally
            {
                Disconnect();
            }
        }

        public void Authorization(Authorization authorization)
        {
            try
            {
                Connect();
            }
            catch (Exception e)
            {
                Error(e);
                Disconnect();
                return;
            }

            var request = JsonSerializer.Serialize(new Request {Type = RequestType.Authorization, Data = JsonSerializer.Serialize(authorization)});
            SendRequest(request);
        }

        public void CheckBalance()
        {
            var request = JsonSerializer.Serialize(new Request {Type = RequestType.CheckBalance});
            SendRequest(request);
        }

        public void Transfer(Transfer transfer)
        {
            var request = JsonSerializer.Serialize(new Request {Type = RequestType.Transfer, Data = JsonSerializer.Serialize(transfer)});
            SendRequest(request);
        }

        private string GetResponse()
        {
            return Helper.GetData(stream);
        }

        private void SendRequest(string data)
        {
            Helper.SendData(stream, data);
        }

        private void Disconnect()
        {
            stream?.Close();
            client?.Close();
        }
    }
}