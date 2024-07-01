using System;
using System.Data.SqlClient;

namespace ToDoListApp
{
    class Program
    {
        private static string connectionString = "Server=THANDO-LT\\SQLEXPRESS;Database=ToDoListDB;Trusted_Connection=True;";
        //my table in my database
        /*Create Table Tasks(
         * Id INT Primary Key Identity (1,1),
         * Title NVARCHAR(100),
         * TaskDescription NVARCHAR(255),
         * DueDate DATETIME,
         * IsCompleted BIT Default 0
         )*/
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
                    case 2:
                        ViewTasks();
                        break;

                }

            } while (choice != 5);
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

        static void ViewTasks()
        {
            using (SqlConnection  connection = new SqlConnection (connectionString))
            {
                string query = "SELECT * FROM TASKS";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Title: {reader["Title"]}, Description: {reader["TaskDescription"]}, DueDate: {reader["DueDate"]}, Is Completed: {reader["IsCompleted"]}");
                }
                connection.Close();
            }
        }
    }

}
