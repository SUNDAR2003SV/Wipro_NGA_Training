using System;
using System.Collections.Generic;

class SocialMediaPlatform
{
    private List<string> posts = new List<string>();
    private Dictionary<string, int> likes = new Dictionary<string, int>();
    private HashSet<int> users = new HashSet<int>();
    private Stack<string> recentActions = new Stack<string>();
    private Queue<string> notifications = new Queue<string>();

    // Add a new user
    public void AddUser(int userId)
    {
        if (users.Add(userId))
        {
            Console.WriteLine($"User {userId} added.");
            recentActions.Push($"User {userId} added.");
        }
        else
        {
            Console.WriteLine($"User {userId} already exists.");
        }
    }

    // Add a new post
    public void AddPost(string post)
    {
        posts.Add(post);
        likes[post] = 0;
        Console.WriteLine($"Post added: {post}");
        recentActions.Push($"Post added: {post}");
        notifications.Enqueue($"New post: {post}");
    }

    // Like a post
    public void LikePost(string post)
    {
        if (likes.ContainsKey(post))
        {
            likes[post]++;
            Console.WriteLine($"Post liked: {post} (Likes: {likes[post]})");
            recentActions.Push($"Post liked: {post}");
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
    }

    // Undo last action
    public void UndoLastAction()
    {
        if (recentActions.Count > 0)
        {
            string action = recentActions.Pop();
            Console.WriteLine($"Undo action: {action}");
        }
        else
        {
            Console.WriteLine("No actions to undo.");
        }
    }

    // Process notifications in FIFO
    public void ProcessNotifications()
    {
        Console.WriteLine("\nProcessing Notifications:");
        while (notifications.Count > 0)
        {
            Console.WriteLine(notifications.Dequeue());
        }
    }

    // Show all posts and likes
    public void ShowPosts()
    {
        Console.WriteLine("\nPosts and Likes:");
        foreach (var post in posts)
        {
            Console.WriteLine($"{post} - Likes: {likes[post]}");
        }
    }
}

class Program
{
    static void Main()
    {
        SocialMediaPlatform platform = new SocialMediaPlatform();

        // Add users
        platform.AddUser(1);
        platform.AddUser(2);
        platform.AddUser(1); // Duplicate user

        // Add posts
        platform.AddPost("Hello World!");
        platform.AddPost("Learning C# Collections.");

        // Like posts
        platform.LikePost("Hello World!");
        platform.LikePost("Hello World!");
        platform.LikePost("Learning C# Collections.");

        // Undo last action
        platform.UndoLastAction();

        // Show posts and likes
        platform.ShowPosts();

        // Process notifications
        platform.ProcessNotifications();
    }
}
