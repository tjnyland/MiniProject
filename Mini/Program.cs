using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini
{
    //Mini Project - Jacob, Toby and Mikey
    //Version 3
    internal class Program
    {
        struct Homework // Structure for homework
        {
            public string subject; // Subject
            public string name; // Name of homework
            public DateTime DueDate; // Date homework is due
        }
        static void Main(string[] args)
        {
            bool loop = true; // Loop for menu options
            Homework[] HomeworkList = new Homework[20]; // homework array
            int homeworkCount = LoadFile(ref HomeworkList); // Load existing homework entries from file to the array and returns the amount of homeworks in the array. This took a while to do but pretty proud of it.
            while (loop) // keeps displaying menu
            {

                // displays menu
                Console.WriteLine("Welcome to the Aquinas Homework Diary:");
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1) Add Homework \n2) View Homework \n3) Complete Homework \n4) Quit");

                try // error checks users menu input
                {
                    int userInput = int.Parse(Console.ReadLine()); //Stores user answer as an Int value
                    switch (userInput)
                    {
                        case 1: //1 - add homework
                            AddHomework(ref HomeworkList, ref homeworkCount); //add homework function with HomeworkList and homeworkCount
                            break;
                        case 2: //View homework
                            ViewHomework(HomeworkList, homeworkCount); //loads in the HomeworkList and homeworkCount
                            break;
                        case 3:// Completed Homework
                            CompleteHomework(ref HomeworkList, ref homeworkCount); //Complete Homework method with HomeworkList and homework count being run through it
                            break;
                        case 4:// Quit menu
                            // needs to write the array to the text file before ending program - Don't think it does Jacob, I could do it anyways if you want
                            loop = false; //breaks the loop
                            break;
                        default: //If int that isn't
                            Console.WriteLine("Invalid input Please try again"); //Output invaid option
                            break;
                    }
                }
                catch //catches if the wrong data type (like a string) is entered
                {
                    Console.WriteLine("Invalid input"); //Output invaild input
                }
            }
        }

        // Method to load homework from the file
        static int LoadFile(ref Homework[] HomeworkList) //With the homework list
        {
            int i = 0;
            try //tries to open the file
            {
                using (StreamReader file = new StreamReader("mini.txt")) //opens/ uses the mini.txt file
                {
                    while (!file.EndOfStream && i < 20) //while not at the end of file and count being lower than 20 as that is the max we have in our HomeworkList array
                    {
                        //Goes through index and adds all the subjects/names/due dates in the correct parts of the array
                        HomeworkList[i].subject = file.ReadLine(); 
                        HomeworkList[i].name = file.ReadLine();
                        HomeworkList[i].DueDate = DateTime.Parse(file.ReadLine());
                        i++; //Adds onto the account, like a fancy for loop but we needed the file to make the not the EndOfStream work.
                    }
                }
            }
            catch // if file cant be loaded there is an error message
            {
                Console.WriteLine("Error loading file"); //output error message!
            }

            return i; //returns the amount of homeworks in the array now.
        }

        // Method to add a new homework entry
        static void AddHomework(ref Homework[] HomeworkList, ref int homeworkCount) //Got The list and the homework count
        {
            if (homeworkCount >= 20) //if the homework count is less than or greater than 20, then its too high and will mess up the array.
            {
                Console.WriteLine("Maximum homework entries reached!"); //output max homeworks reached
                return;
            }
            else
            {
             Console.WriteLine("Enter the subject of the homework:"); //enter sunject
             string subject = Console.ReadLine(); //saves as string
              Console.WriteLine("Enter the name of the homework:"); //enter name
              string name = Console.ReadLine();//saves as string
              Console.WriteLine("Enter the due date (DD/MM/YYYY):");//enter due date 
              DateTime dueDate = DateTime.Parse(Console.ReadLine()); //saves that as a date time

                //This part I did use the internet a bit to help, but I have learnt it and fully understand it 
              Homework newHomework = new Homework //creates a new part of structure (not sure how to explain it but I get it)
              {
                subject = subject, //sets sunject to the user entered subject above
                name = name, //sets same to name entered above
                DueDate = dueDate //sets the due date to the date above
              };
                        HomeworkList[homeworkCount] = newHomework; //adds this new type of homework structure thingy to the homework count of the array. (Bloody smart I am)
                        homeworkCount++; //adds to the homework count
                        SaveFile(HomeworkList, homeworkCount);//Sends HomeworkList and Count to SaveFile() method
            }  
        }
        // Method to view all of homework 
        static void ViewHomework(Homework[] HomeworkList, int homeworkCount) //view homework method
        {
            if (homeworkCount == 0) //0 homeworks in list
            {
                Console.WriteLine("No homework entries found.");//output no homeworks
                return;
            }
            else //more than 0 homeworks in the list
            {
             Console.WriteLine("Current Homework List:");
             for (int i = 0; i < homeworkCount; i++) //for loop to the homework count
                {
                 Console.WriteLine($"{i + 1}. {HomeworkList[i].subject} - {HomeworkList[i].name} (Due: {HomeworkList[i].DueDate.ToString()})");//Outputs the homeworks (based of index in HomeworkList array)
                }
            }
           
        }

        // Method to complete homork
        static void CompleteHomework(ref Homework[] HomeworkList, ref int homeworkCount) //completed homework method
        {
            Console.WriteLine("Enter which homework you want to mark as complete (1 - " + homeworkCount + "):"); //Enter which piece of homework you want to set as complete
            int index = int.Parse(Console.ReadLine()) - 1; //-1 to find the index in the homeworkList
            if (index >= 0 && index < homeworkCount) //Making sure the index entered is a postive so doesn't mess all this stuff up and is in the list.
            {
                Console.WriteLine($"You have completed: {HomeworkList[index].subject} - {HomeworkList[index].name}"); //Outputs homework has been complete
                for (int i = index; i < homeworkCount - 1; i++)
                {
                    HomeworkList[i] = HomeworkList[i + 1]; // moves all homework indexes up
                }
                homeworkCount--; //Removes one from the homeworkCount
                SaveFile(HomeworkList, homeworkCount); // sends homework list and count to SaveFile method
            }
            else //if number is greater than homeworkCount or less than 1
            {
                Console.WriteLine("Invalid homework number."); //Output invaild entered
            }
        }

        // Method to save homework back to the file
        static void SaveFile(Homework[] HomeworkList, int homeworkCount) //The SaveFile method with homework list and count (updated from one of the methods above)
        {
            try //tries to file write
            {
                using (StreamWriter writer = new StreamWriter("mini.txt")) //Uses file to write with
                {
                    for (int i = 0; i < homeworkCount; i++) //for loop with the homeworkCount
                    {
                        writer.WriteLine(HomeworkList[i].subject);
                        writer.WriteLine(HomeworkList[i].name);
                        writer.WriteLine(HomeworkList[i].DueDate.ToShortDateString());
                        //adds all of homeworkCount in (overwrites old data)
                    }
                }
            }
            catch //Cannot file write for some reason
            {
                Console.WriteLine("Error saving file"); //outputs an error
            }
        }
    }
}
