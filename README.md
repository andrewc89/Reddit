# C# Reddit API Wrapper

## Setup

<pre>
	var reddit = new Reddit("your personal user agent here");
	reddit.Login("username", "password");
</pre>

## Basic Usage

<pre>
	var redditDev = reddit.r("redditdev");
	var topPosts = redditDev.Top(From.ThisWeek);
	topPosts.First().Comment("idk what's going on but this is my opinion!");		
</pre>