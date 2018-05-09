// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TwitterApp.Views
{
    [Register ("TweetTableViewCell")]
    partial class TweetTableViewCell
    {
        [Outlet]
        UIKit.UIImageView ImgFavorite { get; set; }


        [Outlet]
        UIKit.UIImageView ImgRetweet { get; set; }


        [Outlet]
        UIKit.UILabel LblFavorite { get; set; }


        [Outlet]
        UIKit.UILabel LblRetweet { get; set; }


        [Outlet]
        UIKit.UILabel LblTweet { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImgFavorite != null) {
                ImgFavorite.Dispose ();
                ImgFavorite = null;
            }

            if (ImgRetweet != null) {
                ImgRetweet.Dispose ();
                ImgRetweet = null;
            }

            if (LblFavorite != null) {
                LblFavorite.Dispose ();
                LblFavorite = null;
            }

            if (LblRetweet != null) {
                LblRetweet.Dispose ();
                LblRetweet = null;
            }

            if (LblTweet != null) {
                LblTweet.Dispose ();
                LblTweet = null;
            }
        }
    }
}