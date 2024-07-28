namespace EpsilonEngine
{
    public delegate void MessageRecievedEvent(string message);
    public delegate bool ConnectionRequestRecievedEvent(string clientName);
    public sealed class LocalMessagingClient
    {
        public readonly string SenderName;
        public readonly string RecipientName;
        public MessageRecievedEvent OnMessageRecievedEvent;

        private System.IO.Pipes.NamedPipeClientStream _clientPipe;
        private System.IO.StreamReader _clientStreamReader;

        private System.IO.Pipes.NamedPipeServerStream _serverPipe;
        private System.IO.StreamWriter _serverStreamWriter;

        public LocalMessagingClient(string senderName, string recipientName)
        {
            if (senderName is null)
            {
                throw new System.Exception("senderName cannot be null.");
            }
            if (senderName is "")
            {
                throw new System.Exception("senderName cannot be empty.");
            }
            SenderName = senderName;

            if (recipientName is null)
            {
                throw new System.Exception("recipientName cannot be null.");
            }
            if (recipientName is "")
            {
                throw new System.Exception("recipientName cannot be empty.");
            }
            RecipientName = recipientName;

            _clientPipe = new System.IO.Pipes.NamedPipeClientStream(".", recipientName, System.IO.Pipes.PipeDirection.In);
            _clientStreamReader = new System.IO.StreamReader(_clientPipe);

            _serverPipe = new System.IO.Pipes.NamedPipeServerStream(senderName, System.IO.Pipes.PipeDirection.Out);
            _serverStreamWriter = new System.IO.StreamWriter(_serverPipe);

            System.Threading.Thread clientThead = new System.Threading.Thread(() =>
            {
                _clientPipe.Connect();
                while (true)
                {
                    string message = _clientStreamReader.ReadLine();
                    OnMessageRecievedEvent.Invoke(message);
                }
            });
            clientThead.Start();

            _serverPipe.WaitForConnection();
            _serverStreamWriter.AutoFlush = true;
        }
        public void SendMessage(string message)
        {
            _serverStreamWriter.WriteLine(message);
        }
    }
}