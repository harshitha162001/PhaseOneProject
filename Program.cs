using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase1
{
    class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }

        public Teacher(int id, string name, string className, string sectionName)
        {
            ID = id;
            Name = name;
            Class = className;
            Section = sectionName;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", ID, Name, Class, Section);
        }
    }

    class TeacherData
    {
        private const string FilePath = @"D:\TeacherInfo\Data.txt";

        public static Teacher[] LoadTeachers()
        {
            if (!File.Exists(FilePath))
            {
                return new Teacher[0];
            }

            string[] lines = File.ReadAllLines(FilePath);

            Teacher[] teachers = new Teacher[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    string[] parts = lines[i].Split('\t');
                    int id = int.Parse(parts[0]);
                    string name = parts[1];
                    string className = parts[2];
                    string sectionName = parts[3];
                    Teacher teacher = new Teacher(id, name, className, sectionName);

                    teachers[i] = teacher;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("exception  Occured", ex.Message);
                }
            }
            return teachers;

        }

        public static void SaveTeachers(Teacher[] teachers)
        {
            if (teachers == null)
            {
                Console.WriteLine("teacher array is null");
                return;
            }
            string[] lines = new string[teachers.Length];

            for (int i = 0; i < teachers.Length; i++)
            {
                if (teachers[i] == null)
                {
                    Console.WriteLine("teacher object at index{0} is null", i);
                    continue;
                }

                lines[i] = teachers[i].ToString();
            }

            File.WriteAllLines(FilePath, lines);
        }

        public static void AddTeacher(int id, string name, string className, string sectionName)
        {
            Teacher[] teachers = LoadTeachers();
            Teacher teacher = new Teacher(id, name, className, sectionName);
            Array.Resize(ref teachers, teachers.Length + 1);
            teachers[teachers.Length - 1] = teacher;
            SaveTeachers(teachers);
        }

        public static void UpdateTeacher(int id, string name, string className, string sectionName)
        {
            Teacher[] teachers = LoadTeachers();
            int index = -1;

            if (teachers == null)
            {
                Console.WriteLine("teacher array is null");
                return;
            }

            for (int i = 0; i < teachers.Length; i++)
            {
                if (teachers[i] == null)
                {
                    Console.WriteLine($"teacher object at index{0} is null", i);
                    continue;
                }

                if (teachers[i].ID == id)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                Teacher teacher = new Teacher(id, name, className, sectionName);
                teachers[index] = teacher;
                SaveTeachers(teachers);
            }
            else
            {
                Console.WriteLine("To update A teacher ,TeacherID is not found!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            string  input = "0";
            do
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1.Add a Teacher:");
                Console.WriteLine("2.Updating a Teacher:");
                Console.WriteLine("3.Displaying all teacher");
                Console.WriteLine("4.Exit\n");
                Console.WriteLine("Enter your choice(1-4):");
                input = Console.ReadLine();
                choice = int.Parse(input ?? "0");
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\nAdding A teacher:");
                        Console.WriteLine("enter Id:");
                        input = Console.ReadLine();
                        int id = int.Parse(input ?? "0");
                        Console.WriteLine("Enter the Name of the Teacher");
                        input = Console.ReadLine();
                        string name = input ?? "0";
                        Console.WriteLine("Enter the class ");
                        input = Console.ReadLine();
                        string className = input ?? "0";
                        Console.WriteLine("Enter the section ");
                        input = Console.ReadLine();
                        string sectionName = input ?? "0";
                        TeacherData.AddTeacher(id, name, className, sectionName);
                        Console.WriteLine("Teacher Added Successfully");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("\nUpdating  A teacher:");
                        Console.WriteLine("enter Id:");
                        input = Console.ReadLine();
                        int updateid = int.Parse(input ?? "0");
                        Console.WriteLine("Enter the Name of the Teacher");
                        input = Console.ReadLine();
                        string updatename = input ?? "0";
                        Console.WriteLine("Enter the Class");
                        input = Console.ReadLine();
                        string updateclassName = input ?? "0";
                        Console.WriteLine("Enter the Section");
                        input = Console.ReadLine();
                        string updatesectionName = input ?? "0";

                        TeacherData.UpdateTeacher(updateid, updatename, updateclassName, updatesectionName);
                        Console.WriteLine("Teacher Updated Successfully");

                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("\nDisplaying all teacher:");

                        Teacher[] teachers = TeacherData.LoadTeachers();

                        foreach (Teacher teacher in teachers)
                        {

                            if (teacher != null)
                            {
                                Console.WriteLine(teacher.ToString());

                            }
                            else
                            {

                                Console.WriteLine("teacher object  is null");
                            }
                        }
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("exiting program");
                        break;
                    default:
                        Console.WriteLine("invalid choice");
                        break;
                }

            } while (choice != 4);
        }
    }
}
