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
        // Property for ISBN with validation using Regex and error handling
        public string ISBN
        {
            get => isbn;
            set
            {
                if (IsValidISBN(value))
                {
                    isbn = value;
                }
                else
                {
                    if (isbn != value)
                    {
                        Console.WriteLine("Invalid ISBN.");
                    }
                }
            }
        }

        // Method to validate ISBN using Regex
        private bool IsValidISBN(string isbn)
        {
            return Regex.IsMatch(isbn, @"^(97(8|9))?\d{9}(\d|X)$");
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
            try
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
            catch (Exception e)
            {
                Console.WriteLine("Error reading book details: " + e.Message);
                return null; // Return null if an error occurs
            }
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
                Title = "All The Bright Places",
                Author = "Jennifer Niven",
                Pages = 388,
                Genre = "Novel",
                Publisher = "Knopf Publishing Group",
                ISBN = "9780385755887"
            };

            // Write book details to a file
            book.WriteToFile("book.txt");

            // Read book details from the file
            var readBook = Book.ReadFromFile("book.txt");

            if (readBook != null)
    {
        // Print book details
        Console.WriteLine($"Title: {readBook.Title}");
        Console.WriteLine($"Author: {readBook.Author}");
        Console.WriteLine($"Pages: {readBook.Pages}");
        Console.WriteLine($"Genre: {readBook.Genre}");
        Console.WriteLine($"Publisher: {readBook.Publisher}");
        Console.WriteLine($"ISBN: {readBook.ISBN}");
    }
    else
    {
        Console.WriteLine("Book details could not be read.");
    }

            // Email extraction example
            string text = "Contact us at library@gmail.com or bookadmin@mail.org.";
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
