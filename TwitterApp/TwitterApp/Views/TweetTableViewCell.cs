using System;

using Foundation;
using UIKit;

namespace TwitterApp.Views
{
    public partial class TweetTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString(nameof( TweetTableViewCell));

        public string Tweet
        {
            get => LblTweet.Text;
            set => LblTweet.Text = value;
        }

        public string FavoriteCount
        {
            get => LblFavorite.Text;
            set => LblFavorite.Text = value;
        }
        public string RetweetCount
        {
            get => LblRetweet.Text;
            set => LblRetweet.Text = value;
        }

        #region Constructores
        protected TweetTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        #endregion


    }
}
