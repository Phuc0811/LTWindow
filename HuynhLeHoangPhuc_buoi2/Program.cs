using System;
using System.Collections.Generic;
using System.Linq;

namespace HuynhLeHoangPhuc_buoi2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student(1, "Phúc", 23),
                new Student(2, "Phú", 24),
                new Student(3, "Phụng", 22),
                new Student(4, "Phi", 25),
                new Student(5, "Phương", 15),
                new Student(6, "Phong", 20),
                new Student(7, "An", 21),
            };

            Console.WriteLine("Danh sách toàn bộ học sinh:");
            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }

            var ageGroup = students.Where(s => s.Age >= 15 && s.Age <= 18).ToList();
            Console.WriteLine("\nDanh sách học sinh có tuổi từ 15 đến 18:");
            foreach (var student in ageGroup)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }

            var nameStartsWithA = students.Where(s => s.Name.StartsWith("A")).ToList();
            Console.WriteLine("\nHọc sinh có tên bắt đầu bằng chữ 'A':");
            foreach (var student in nameStartsWithA)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }


            var totalAge = students.Sum(s => s.Age);
            Console.WriteLine($"\nTổng tuổi tất cả học sinh: {totalAge}");

            var oldestStudent = students.OrderByDescending(s => s.Age).FirstOrDefault();
            Console.WriteLine("\nHọc sinh có tuổi đời lớn nhất:");
            if (oldestStudent != null)
            {
                Console.WriteLine($"Id: {oldestStudent.Id}, Name: {oldestStudent.Name}, Age: {oldestStudent.Age}");
            }

            var sortedByAge = students.OrderBy(s => s.Age).ToList();
            Console.WriteLine("\nSắp xếp tuổi học sinh tăng dần:");
            foreach (var student in sortedByAge)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }
        }
    }
}
