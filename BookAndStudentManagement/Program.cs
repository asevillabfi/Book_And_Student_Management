using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BookAndStudentManagement
{
    // Book class 
    public class Book
    {
        public string Title { get; set; }  
        public string Author { get; set; } 
        public int Pages { get; set; }  
        public string Genre { get; set; }  
        public string Publisher { get; set; }  

        private string isbn;
        // Property for ISBN with validation using Regex
        public string ISBN
        {
            get => isbn;
            set
            {
                if (!Regex.IsMatch(value, @"^(97(8|9))?\d{9}(\d|X)$"))
                    throw new ArgumentException("Invalid ISBN.");
                isbn = value;
            }
        }

        // Method to write book details to a file
        public void WriteToFile(string filePath)
        {
            File.WriteAllLines(filePath, new[]
            {
                $"Title: {Title}",
                $"Author: {Author}",
                $"Pages: {Pages}",
                $"Genre: {Genre}",
                $"Publisher: {Publisher}",
                $"ISBN: {ISBN}"
            });
        }

        // Method to read book details from a file and create a Book object
        public static Book ReadFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            return new Book
            {
                Title = lines[0].Split(": ")[1],
                Author = lines[1].Split(": ")[1],
                Pages = int.Parse(lines[2].Split(": ")[1]),
                Genre = lines[3].Split(": ")[1],
                Publisher = lines[4].Split(": ")[1],
                ISBN = lines[5].Split(": ")[1]
            };
        }
    }

    // Student class 
    public class Student
    {
        public string Name { get; set; }  
        public int Grade { get; set; }  
        public Student(string name, int grade) => (Name, Grade) = (name, grade);  
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Book operations
            var book = new Book
            {
                Title = "Sample Book",
                Author = "John Doe",
                Pages = 300,
                Genre = "Fiction",
                Publisher = "Sample Publisher",
                ISBN = "9781234567890"
            };

            // Write book details to a file
            book.WriteToFile("book.txt");

            // Read book details from the file
            var readBook = Book.ReadFromFile("book.txt");

            // Print book details
            Console.WriteLine($"Title: {readBook.Title}");
            Console.WriteLine($"Author: {readBook.Author}");
            Console.WriteLine($"Pages: {readBook.Pages}");
            Console.WriteLine($"Genre: {readBook.Genre}");
            Console.WriteLine($"Publisher: {readBook.Publisher}");
            Console.WriteLine($"ISBN: {readBook.ISBN}");

            // Email extraction example
            string text = "Contact us at support@example.com or admin@example.org.";
            // Use Regex to find email addresses in the text
            foreach (Match match in Regex.Matches(text, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}"))
                Console.WriteLine(match.Value);

            // Student data operations
            var studentData = new Dictionary<string, Student>
            {
                { "A001", new Student("Alice", 85) }, 
                { "B002", new Student("Bob", 92) }  
            };

            // Print student data
            foreach (var student in studentData)
                Console.WriteLine($"ID: {student.Key}, Name: {student.Value.Name}, Grade: {student.Value.Grade}");
        }
    }
}
