using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace ExamenTwitter.Models
{
    public class TweetModel
    {
        public string Name { get; }
        public string Text { get; }
        public int FavoriteCount { get; }
        public int RetweetedCount { get; }
        public UIImage UserImage { get; }


        public TweetModel(string name, string text, int favoriteCount, int retweetedCount, UIImage userImage)
        {
            Name = name;
            Text = text;
            FavoriteCount = favoriteCount;
            RetweetedCount = retweetedCount;
            UserImage = userImage;
        }
    }
}
