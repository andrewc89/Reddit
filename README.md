# C# Reddit API Wrapper

## Setup

<pre>
	var r = new Reddit("your personal user agent here");
	r.Login("username", "password");
</pre>

## Basic Usage

<pre>
	var RedditDev = r.GetSubreddit("redditdev");
	var TopPosts = RedditDev.Top(From.ThisWeek);
	TopPosts.First().Comment("idk what's going on but this is my opinion!");		
</pre>