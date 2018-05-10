using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using System.Threading.Tasks;
using System.Net.Http;
using UIKit;
using Foundation;

namespace ExamenTwitter.Models
{
    public class TwitterLazy
    {

        #region Singleton

        static readonly Lazy<TwitterLazy> lazy = new Lazy<TwitterLazy>(() => new TwitterLazy());

        public static TwitterLazy SharedInstance { get => lazy.Value; }

        #endregion


        #region Class Variables

        List<TweetModel> listTweets;
        HttpClient httpClient;

        #endregion


        #region Constructors

        public TwitterLazy()
        {
            httpClient = new HttpClient();
        }

        #endregion


        #region Event Classes

        public class TweetEventArgs : EventArgs
        {
            public List<TweetModel> ListaTweets { get; private set; }

            public TweetEventArgs(List<TweetModel> listaTweets)
            {
                ListaTweets = listaTweets;
            }
        }

        public class ErrorEventArgs : EventArgs
        {
            public string Message { get; private set; }

            public ErrorEventArgs(string message)
            {
                Message = message;
            }
        }

        #endregion


        #region Events

        public event EventHandler<TweetEventArgs> TweetsFetched;
        public event EventHandler<ErrorEventArgs> FetchTweetsFailed;

        #endregion


        #region Async Method

        public void FetchTweets(TwitterContext twitterContext, string strSearch)
        {
            Task.Factory.StartNew(FetchTweetsAsync);



            async Task FetchTweetsAsync()
            {
                try
                {
                    listTweets = new List<TweetModel>();

                    Search tweets = await (from tweet in twitterContext.Search
                                           where tweet.Type == SearchType.Search && tweet.Query == strSearch
                                            && tweet.Count == 15
                                           select tweet).SingleOrDefaultAsync();


                    foreach (var objTweet in tweets.Statuses)
                    {
                        //Cargar la imagen desde la URL.
                        var taskImage = httpClient.GetByteArrayAsync(objTweet.User.ProfileImageUrlHttps);
                        var taskImageContents = await taskImage;
                        var userImage = UIImage.LoadFromData(NSData.FromArray(taskImageContents));


                        TweetModel tweetModel = new TweetModel(objTweet.User.Name, objTweet.Text, int.Parse(objTweet.FavoriteCount.ToString()), int.Parse(objTweet.RetweetCount.ToString()), userImage);
                        listTweets.Add(tweetModel);
                    }

                    var e = new TweetEventArgs(listTweets);
                    TweetsFetched(this, e);
                }
                catch (Exception ex)
                {
                    if (FetchTweetsFailed == null)
                    {
                        return;
                    }
                    else
                    {
                        var e = new ErrorEventArgs(ex.Message);
                        FetchTweetsFailed(this, e);
                    }
                }
            }
        }

        #endregion


    }
}
