using System;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Diagnostics;
// I am not sure what directories I need to include

namespace ObtainNamesFromSQLite
{

    class Program
    {
    void Start()
        {

            // Open connection
            IDbConnection dbcon = new SqliteConnection(connection);
            dbcon.Open();


            // Read and print all values in table
            IDbCommand cmnd_read = dbcon.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM my_table";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();

            while (reader.Read())
            {
                Debug.Log("id: " + reader[0].ToString());
                Debug.Log("val: " + reader[1].ToString());
            }

            // Close connection
            dbcon.Close();
        }     // Create database
            string connection = "URI=file:" + Application.persistentDataPath + "/" + "My_Database";


        string ChooseRandomFirstName(string chosenRace, bool gender) // Choose a random First name based on gender and race
        {
            Random rnd = new Random();
            string sql;
            string chosenName = "";
            int randomNumber = rnd.Next(10);


            if (chosenRace == "Human")
            {
                if (gender == true)
                {
                    sql = "SELECT HumanNames.First FROM HumanNames WHERE HumanNames.Gender = 'Male' ORDER BY random() LIMIT 1; ";
                    chosenName = "Greg";
                }
                else
                {
                    sql = "SELECT HumanNames.First FROM HumanNames WHERE HumanNames.Gender = 'Female' ORDER BY random() LIMIT 1; ";
                    chosenName = "Susan";
                }
            }
            else if (chosenRace == "Dwarf")
            {
                if (gender == true)
                {
                    sql = "SELECT DwarfNames.First FROM DwarfNames WHERE DwarfNames.Gender = 'Male' ORDER BY random() LIMIT 1; ";
                    chosenName = "Adrik";
                }
                else
                {
                    sql = "SELECT DwarfNames.First FROM DwarfNames WHERE DwarfNames.Gender = 'Female' ORDER BY random() LIMIT 1; ";
                    chosenName = "Amber";
                }
            }
            else if (chosenRace == "Elf")
            {
                if (gender == true)
                {
                    sql = "SELECT ElfNames.First FROM ElfNames WHERE ElfNames.Gender = 'Male' ORDER BY random() LIMIT 1; ";
                    chosenName = "Aust";
                }
                else
                {
                    sql = "SELECT ElfNames.First FROM ElfNames WHERE ElfNames.Gender = 'Female' ORDER BY random() LIMIT 1; ";
                    chosenName = "Lia";
                }
            }
            else if (chosenRace == "HalfElf")
            {
                // If random number is even choose an elf name. If random number is odd chose a human name
                if (randomNumber % 2 == 0)
                {
                    if (gender == true)
                        {
                            sql = "SELECT ElfNames.First FROM ElfNames WHERE ElfNames.Gender = 'Male' ORDER BY random() LIMIT 1; ";
                            chosenName = "Peren";
                        }
                    else
                        {
                            sql = "SELECT ElfNames.First FROM ElfNames WHERE ElfNames.Gender = 'Female' ORDER BY random() LIMIT 1; ";
                            chosenName = "Sariel";
                        }
                }
                else
                {
                    if (gender == true)
                    {
                        sql = "SELECT HumanNames.First FROM HumanNames WHERE HumanNames.Gender = 'Male' ORDER BY random() LIMIT 1; ";
                        chosenName = "Evender";
                    }
                    else
                    {
                        sql = "SELECT HumanNames.First FROM HumanNames WHERE HumanNames.Gender = 'Female' ORDER BY random() LIMIT 1; ";
                        chosenName = "Hama";
                    }
                }
            }
            else if(chosenRace == "HalfOrc")
            {
                if (gender == true)
                {
                    sql = "SELECT HalfOrc.First FROM HalfOrc WHERE HalfOrc.Gender = 'Male' ORDER BY random() LIMIT 1; ";
                    chosenName = "Evender";
                }
                else
                {
                    sql = "SELECT HalfOrc.First FROM HalfOrc WHERE HalfOrc.Gender = 'Female' ORDER BY random() LIMIT 1; ";
                    chosenName = "Hama";
                }
            }
            else if (chosenRace == "Halfling")
            {
                if (gender == true)
                {
                    sql = "SELECT HalflingNames.First FROM HalflingNames WHERE HalflingNames.Gender = 'Male' ORDER BY random() LIMIT 1; ";
                    chosenName = "Milo";
                }
                else
                {
                    sql = "SELECT HalflingNames.First FROM HalflingNames WHERE HalflingNames.Gender = 'Female' ORDER BY random() LIMIT 1; ";
                    chosenName = "Bree";
                }
            }
            else if (chosenRace == "Gnome")
            {
                if (gender == true)
                {
                    sql = "SELECT GnomeNames.First FROM GnomeNames WHERE GnomeNames.Gender = 'Male' ORDER BY random() LIMIT 1; ";
                    chosenName = "Wrenn";
                }
                else
                {
                    sql = "SELECT GnomeNames.First FROM GnomeNames WHERE GnomeNames.Gender = 'Female' ORDER BY random() LIMIT 1; ";
                    chosenName = "Nyx";
                }
            }
            else if (chosenRace == "Dragonborn")
            {
                if (gender == true)
                {
                    sql = "SELECT DragonbornNames.First FROM DragonbornNames WHERE DragonbornNames.Gender = 'Male' ORDER BY random() LIMIT 1; ";
                    chosenName = "Mehen";
                }
                else
                {
                    sql = "SELECT DragonbornNames.First FROM DragonbornNames WHERE DragonbornNames.Gender = 'Female' ORDER BY random() LIMIT 1; ";
                    chosenName = "Thava";
                }
            }
            return chosenName;
        }
        string ChooseRandomLastName(string chosenRace) // choose a random last name based on race
        {
            Random rnd = new Random();
            string sql;
            string chosenName = "";
            int randomNumber = rnd.Next(10);

            if (chosenRace == "Human")
            {
                    sql = "SELECT HumanNames.Last FROM HumanNames ORDER BY random() LIMIT 1; ";
                    chosenName = "Windriver";
            }
            else if (chosenRace == "Dwarf")
            {
                sql = "SELECT DwarfNames.Last FROM DwarfNames ORDER BY random() LIMIT 1; ";
                chosenName = "Torbera";
            }
            else if (chosenRace == "Elf")
            {
                sql = "SELECT ElfNames.Last FROM ElfNames ORDER BY random() LIMIT 1; ";
                chosenName = "Holimion";
            }
            else if (chosenRace == "HalfElf")
            {
                if(randomNumber % 2 == 0 )
                {
                    sql = "SELECT ElfNames.Last FROM ElfNames ORDER BY random() LIMIT 1; ";
                    chosenName = "Ilphelkiir";
                }
                else
                {
                    sql = "SELECT HumanNames.Last FROM HumanNames ORDER BY random() LIMIT 1; ";
                    chosenName = "Romondo";
                }
            }
            else if (chosenRace == "HalfOrc")
            {
                sql = "SELECT HalfOrc.Last FROM HalfOrc ORDER BY random() LIMIT 1;" ;
                chosenName = "Kansif";
            }
            else if (chosenRace == "Halfling")
            {
                sql = "SELECT HalflingNames.Last FROM HalflingNames ORDER BY random() LIMIT 1; ";
                chosenName = "Tosscobble";
            }
            else if (chosenRace == "Gnome")
            {
                sql = "SELECT GnomeNames.Last FROM GnomeNames ORDER BY random() LIMIT 1; ";
                chosenName = "Roywyn";
            }
            else if (chosenRace == "Dragonborn")
            {
                sql = "SELECT DragonbornNames.Last FROM DragonbornNames ORDER BY random() LIMIT 1; ";
                chosenName = "Biri";
            }
            return chosenName;
        }

        static void Main(string[] args)
        {
            bool isMale = true; //If isMale is false the character is female
            string race = "Human";
            string chosenFirstName = ChooseRandomFirstName(race, isMale);
            string chosenLastName = ChooseRandomLastName(race);
            Console.WriteLine(chosenFirstName);
            Console.WriteLine(chosenLastName);
        }

    }
}
