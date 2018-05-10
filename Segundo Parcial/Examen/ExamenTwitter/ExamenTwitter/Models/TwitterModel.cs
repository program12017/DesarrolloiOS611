using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;

namespace ExamenTwitter.Models
{
    public class TwitterModel
    {
        
        public TwitterContext TwitterContext { get; }


        public TwitterModel(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, string screenName, ulong userID)
        {
            ICredentialStore userTwitterCredential = new InMemoryCredentialStore();
            userTwitterCredential.ConsumerKey = consumerKey;
            userTwitterCredential.ConsumerSecret = consumerSecret;
            userTwitterCredential.OAuthToken = accessToken;
            userTwitterCredential.OAuthTokenSecret = accessTokenSecret;
            userTwitterCredential.ScreenName = screenName;
            userTwitterCredential.UserID = userID;


            SingleUserAuthorizer autorizador = new SingleUserAuthorizer();
            autorizador.CredentialStore = userTwitterCredential;

            TwitterContext = new TwitterContext(autorizador);
        }

    }
}
