using System;
using System.Threading;

using RemoteBotClient;

namespace Collabitama.Client.Helpers
{
    public class AsyncBotInterface
    {
        private readonly IBotInterface botInterface;
        private readonly AutoResetEvent getInput;
        private readonly AutoResetEvent gotInput;
        private readonly int timeOutMillisecs;
        private string input;

        public AsyncBotInterface(string apiKey, int timeOutMillisecs)
        {
            this.botInterface = RemoteBotClientInitializer.Init(apiKey, false);
            this.getInput = new AutoResetEvent(false);
            this.gotInput = new AutoResetEvent(false);
            this.timeOutMillisecs = timeOutMillisecs;

            var inputThread = new Thread(Reader) { IsBackground = true };

            inputThread.Start();
        }

        public string ReadLine()
        {
            this.getInput.Set();

            if (this.gotInput.WaitOne(this.timeOutMillisecs))
            {
                return this.input;
            }

            throw new TimeoutException("Server did not provide data within the timelimit.");
        }

        public void WriteLine(string botInput)
        {
            this.botInterface.WriteLine(botInput);
        }

        private void Reader()
        {
            while (true)
            {
                this.getInput.WaitOne();
                this.input = this.botInterface.ReadLine();
                this.gotInput.Set();
            }
        }
    }
}
