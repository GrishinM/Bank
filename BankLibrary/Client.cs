using System;
using System.Net.Sockets;
using System.Text.Json;
using BankLibrary.Requests;
using BankLibrary.Responses;

namespace BankLibrary
{
    public class Client
    {
        private readonly TcpClient client;

        private readonly NetworkStream stream;

        private readonly Server server;

        private int cardId;

        public event Action<Exception> Error;

        public Client(TcpClient tcpClient, Server server)
        {
            client = tcpClient;
            stream = client.GetStream();
            this.server = server;
        }

        public void MainProcess()
        {
            try
            {
                while (true)
                {
                    var request = JsonSerializer.Deserialize<Request>(GetRequest());
                    var data = request.Data;
                    Response response;
                    switch (request.Type)
                    {
                        case RequestType.Authorization:
                            var authorization = JsonSerializer.Deserialize<Authorization>(data);
                            var authorizationResult = server.Authorize(authorization);
                            if (authorizationResult.Success)
                                cardId = authorization.CardId;
                            response = new Response {Type = ResponseType.AuthorizationResult, Data = JsonSerializer.Serialize(authorizationResult)};
                            break;
                        case RequestType.Transfer:
                            var transfer = JsonSerializer.Deserialize<Transfer>(data);
                            var transferResult = server.Transfer(cardId, transfer);
                            response = new Response {Type = ResponseType.TransferResult, Data = JsonSerializer.Serialize(transferResult)};
                            break;
                        case RequestType.CheckBalance:
                            var balanceResult = server.CheckBalance(cardId);
                            response = new Response {Type = ResponseType.BalanceResult, Data = JsonSerializer.Serialize(balanceResult)};
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    SendResponse(JsonSerializer.Serialize(response));
                }
            }
            catch (Exception e)
            {
                Error?.Invoke(e);
            }
            finally
            {
                Disconnect();
            }
        }

        private string GetRequest()
        {
            return Helper.GetData(stream);
        }

        private void SendResponse(string response)
        {
            Helper.SendData(stream, response);
        }

        private void Disconnect()
        {
            stream?.Close();
            client?.Close();
        }
    }
}