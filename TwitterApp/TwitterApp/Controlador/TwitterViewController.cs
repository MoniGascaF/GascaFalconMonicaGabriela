using System;
using System.Collections.Generic;
using Foundation;
using LinqToTwitter;
using TwitterApp.Views;
using TwitterExam.Models;
using UIKit;
//
namespace TwitterApp.Controlador
{
    public partial class TwitterViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate, IUISearchResultsUpdating
    {
        //variables
        UISearchController searchController;
        List<Status> tweets;

        public TwitterViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            InitializeComponents();
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        void Linq2TwitterManager_TweetsFetched(object sender, TweetsFetchedEventArgs e)
        {
            tweets = e.Tweets;
            InvokeOnMainThread(() => TweetsTableView.ReloadData());
        }

        void Linq2TwitterManager_FetchTweetFailed(object sender, FetchTweetFailedEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }

		void InitializeComponents()
        {
            tweets = new List<Status> ();
            Linq2TwitterManager.SharedInstance.TweetsEncontrados += Linq2TwitterManager_TweetsFetched;
            Linq2TwitterManager.SharedInstance.TweetFailed += Linq2TwitterManager_FetchTweetFailed;

            searchController = new UISearchController(searchResultsController: null)
            {
                SearchResultsUpdater = this,
                DimsBackgroundDuringPresentation = false
            };

            TweetsTableView.DataSource = this;
            TweetsTableView.Delegate = this;
            TweetsTableView.TableHeaderView = searchController.SearchBar;

            TweetsTableView.RowHeight = UITableView.AutomaticDimension;
            TweetsTableView.EstimatedRowHeight = 50;
        }

        //Data Source
       

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
                return tweets.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            
            var tweet = tweets[indexPath.Row];
            var cell = tableView.DequeueReusableCell(TweetTableViewCell.Key, indexPath) as TweetTableViewCell;
            cell.Tweet = tweet.FullText;
            cell.FavoriteCount = tweet.FavoriteCount.ToString();
            cell.RetweetCount = tweet.RetweetCount.ToString();
            return cell;
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            Linq2TwitterManager.SharedInstance.BuscarTwits(searchController.SearchBar.Text);       
        }
    }
}

