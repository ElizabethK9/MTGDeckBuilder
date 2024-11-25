namespace MTGDeckBuilder.Models
{
    public static class Resources
    {
        // Static contact information properties
        public static string NameOne { get; } = "Elizabeth Proctor";
        public static string EmailOne { get; } = "Elizabethproctor66@gmail.com";
        public static string DiscordIDOne { get; } = "elizabeth_k9 - Message Only!";

        public static string NameTwo { get; } = "Cameron White";
        public static string EmailTwo { get; } = "cameronwhite4121@gmail.com";
        public static string DiscordIDTwo { get; } = "cameronw.beans";

        // Static method to display contact information
        public static void DisplayContactInfo()
        {
            Console.WriteLine($"Name: {NameOne}");
            Console.WriteLine($"Email One: {EmailOne}");
            Console.WriteLine($"Discord ID One: {DiscordIDOne}");

            // Adding a separator line
            Console.WriteLine(new string('-', 30));

            Console.WriteLine($"Name: {NameTwo}");
            Console.WriteLine($"Email Two: {EmailTwo}");
            Console.WriteLine($"Discord ID Two: {DiscordIDTwo}");
        }
    }
}

