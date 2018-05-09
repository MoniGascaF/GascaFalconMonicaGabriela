using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToTwitter;


namespace TwitterExam.Models
{
    public class Linq2TwitterManager
    {
        static Lazy<Linq2TwitterManager> lazy = new Lazy<Linq2TwitterManager>(() => new Linq2TwitterManager());
        public static Linq2TwitterManager SharedInstance { get => lazy.Value; }

        //eventos
        public event EventHandler<TweetsFetchedEventArgs> TweetsEncontrados;
        public event EventHandler<FetchTweetFailedEventArgs> TweetFailed;
        //variables
        SingleUserAuthorizer autorizar;
        TwitterContext twitterContext;
        CancellationTokenSource cancellationTokenSource;

        Linq2TwitterManager()
        {
            autorizar = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    //app.twitter
                    ConsumerKey = "s7GhiVjtkx0j1W9LVcGiEU35S",
                    ConsumerSecret = "0qRPG9vUCI8SLvifvUSEB0P2iJ33kphSX8yyojZf3fgo8Qcn8t",
                    AccessToken = "190819968-MLbnxxURRBELLITbKX9AA1mdh7fLjcmBqiExYUe8",
                    AccessTokenSecret = "fs30Ymyh2BGkesXj3WwIwj0BQccSoRRKAnzaosxBUKRsl"
                }
            };
            twitterContext = new TwitterContext(autorizar);
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void BuscarTwits(string query)
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
            }

            cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            Task.Factory.StartNew(async () => {
                try
                {
                    var tweets = await BuscarTweetsAsync(query, cancellationToken);

                    var e = new TweetsFetchedEventArgs(tweets);

                    TweetsEncontrados?.Invoke(this, e);
                }
                catch (Exception ex)
                {
                    var e = new FetchTweetFailedEventArgs(ex.Message);
                    TweetFailed?.Invoke(this, e);
                }
            }, cancellationToken);

        }

        async Task<List<Status>> BuscarTweetsAsync(string query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Status>();
            }

            cancellationToken.ThrowIfCancellationRequested();

            Search searchResponse = await
                (from search in twitterContext.Search
                 where search.Type == SearchType.Search &&
                 search.Query == query &&
                 search.TweetMode == TweetMode.Extended
                 select search)
                .SingleOrDefaultAsync();

            cancellationToken.ThrowIfCancellationRequested();

            return searchResponse?.Statuses;
        }

        }

        public class TweetsFetchedEventArgs:EventArgs
        {
            public List<Status> Tweets { get; private set; }
            public TweetsFetchedEventArgs(List<Status> tweets)
            {
                Tweets = tweets;
            }
        }
    public class FetchTweetFailedEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }

        public FetchTweetFailedEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

    }

}
