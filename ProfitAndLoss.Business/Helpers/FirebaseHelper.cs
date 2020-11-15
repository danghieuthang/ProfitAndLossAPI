using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Helpers
{
    public class FirebaseHelper
    {
        private static readonly string ApiKey = "AIzaSyB0bAvWYtuR-EP0YiultKtT2yhdW40HgMw";
        private static readonly string Bucket = "swdk13.appspot.com";
        private static readonly string AuthEmail = "dhthang1998@gmail.com";
        private static readonly string AuthPassword = "anhthangdepZai123";

        public FirebaseHelper()
        {

        }

        public async Task<string> PushNotificationAsync(string data)
        {
            // The topic name can be optionally prefixed with "/topics/".
            var topic = "Created Tranasction";

            // See documentation on defining a message payload.
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                    {
                        { "score", data },
                        { "time", DateTime.Now.ToString() },
                    },
                Topic = topic,
            };
            // Send a message to the devices subscribed to the provided topic.
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return response;
        }
    }
}
