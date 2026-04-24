using System;
using System.Collections.Generic;

class MusicPlaylistManager
{
    private LinkedList<string> playlist = new LinkedList<string>();
    private SortedList<int, string> songsByRating = new SortedList<int, string>();
    private SortedDictionary<string, string> artistSongs = new SortedDictionary<string, string>();

    // Add song to playlist
    public void AddSong(string song)
    {
        playlist.AddLast(song);
        Console.WriteLine($"Song added to playlist: {song}");
    }

    // Remove song from playlist
    public void RemoveSong(string song)
    {
        if (playlist.Remove(song))
        {
            Console.WriteLine($"Song removed from playlist: {song}");
        }
        else
        {
            Console.WriteLine($"Song not found in playlist: {song}");
        }
    }

    // Add song with rating
    public void AddSongWithRating(int rating, string song)
    {
        if (!songsByRating.ContainsKey(rating))
        {
            songsByRating.Add(rating, song);
            Console.WriteLine($"Song '{song}' added with rating {rating}");
        }
        else
        {
            Console.WriteLine($"Rating {rating} already used. Choose another rating.");
        }
    }

    // Add artist-song mapping
    public void AddArtistSong(string artist, string song)
    {
        artistSongs[artist] = song;
        Console.WriteLine($"Artist '{artist}' mapped to song '{song}'");
    }

    // Show playlist
    public void ShowPlaylist()
    {
        Console.WriteLine("\nPlaylist:");
        foreach (var song in playlist)
        {
            Console.WriteLine(song);
        }
    }

    // Show songs sorted by rating
    public void ShowSongsByRating()
    {
        Console.WriteLine("\nSongs by Rating:");
        foreach (var kvp in songsByRating)
        {
            Console.WriteLine($"Rating {kvp.Key}: {kvp.Value}");
        }
    }

    // Show songs sorted by artist
    public void ShowSongsByArtist()
    {
        Console.WriteLine("\nSongs by Artist:");
        foreach (var kvp in artistSongs)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}

class Program
{
    static void Main()
    {
        MusicPlaylistManager manager = new MusicPlaylistManager();

        // Add songs to playlist
        manager.AddSong("Shape of You");
        manager.AddSong("Blinding Lights");
        manager.AddSong("Bohemian Rhapsody");

        // Remove a song
        manager.RemoveSong("Blinding Lights");

        // Add songs with ratings
        manager.AddSongWithRating(5, "Bohemian Rhapsody");
        manager.AddSongWithRating(4, "Shape of You");
        manager.AddSongWithRating(3, "Blinding Lights");

        // Add artist-song mappings
        manager.AddArtistSong("Ed Sheeran", "Shape of You");
        manager.AddArtistSong("The Weeknd", "Blinding Lights");
        manager.AddArtistSong("Queen", "Bohemian Rhapsody");

        // Display results
        manager.ShowPlaylist();
        manager.ShowSongsByRating();
        manager.ShowSongsByArtist();
    }
}
