using System;
using System.Data.SqlClient;

namespace ToDoListApp
{
    class Program
    {
        private static string connectionString = "Server=THANDO-LT\\SQLEXPRESS;Database=ToDoListDB;Trusted_Connection=True;";
        
        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("To-Do List Application");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Update Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: 
                        AddTask();
                        break;

                }

            }while (choice != 5);
        }

        static void AddTask()
        {
            Console.Write("Enter Your Task Title: ");
            string taskTitle = Console.ReadLine();

            Console.Write("Enter Your Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Your Due Date (yyyy-mm-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TASKS (Title, TaskDescription, DueDate) VALUES (@Title, @TaskDescription, @DueDate)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Title", taskTitle);
                cmd.Parameters.AddWithValue("@TaskDescription", description);
                cmd.Parameters.AddWithValue("@DueDate", dueDate);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            Console.WriteLine("Task Added Successfully");
        }
    }

}
