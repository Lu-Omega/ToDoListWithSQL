﻿using System;
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
                    case 3:
                        UpdateTasks(); 
                        break;
                    case 4:
                        DeleteTask();
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

        static void UpdateTasks()
        {
            Console.Write("Enter Task ID to update");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter new title: ");
            string title = Console.ReadLine();
            Console.Write("Enter new description: ");
            string description = Console.ReadLine();
            Console.Write("Enter New Due Date (yyyy-mm-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Is the task completed? (yes/no): ");
            bool isCompleted = Console.ReadLine().ToLower() == "yes";
            using (SqlConnection conn = new SqlConnection (connectionString))
            {
                string query = "UPDATE Tasks SET Title = @Title, TaskDescription = @TaskDescription, DueDate = @DueDate, IsCompleted = @IsCompleted WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand (query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@TaskDescription", description);
                cmd.Parameters.AddWithValue("@DueDate", dueDate);
                cmd.Parameters.AddWithValue("@IsCompleted", isCompleted);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            Console.WriteLine("Task updated successfully");
        }

        static void DeleteTask()
        {
            Console.WriteLine("Enter task ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection (connectionString))
            {
                string query = "DELETE FROM Tasks WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            Console.WriteLine("Task deleted successfully");
        }


    }

}
