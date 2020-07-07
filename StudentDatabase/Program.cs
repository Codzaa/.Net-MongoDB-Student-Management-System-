using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace StudentDatabase
{
    class Program
    {
        
        //Student Structure
        struct STUDENT
        {
            public string name;
            public string last_name;
            public string math_;
            public string chinese_;
            public string passport_id;
            public string heath_check;
            public string nation;
            public bool report_office;
        };
        STUDENT student;

        public bool StudLogin(
            MongoCRUD db,
            ref string stu_name,
            ref string stu_lname
            )
        {
            bool result;
            string u_name;
            string u_namex = "student";
            string p_name;
            string p_namex = "pass";
            //STUDENT student;
            //string p_namex = "admin";
            //
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("===============Student Login Page============");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("Enter Stu Name::-");
            u_name = Console.ReadLine();
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("Enter Password::-");
            p_name = Console.ReadLine();
            var oneRec = db.LoadRecordById<StudentModel>("Students",u_name);
            if (oneRec == null)
            {
                result = false;
            }
            else if (p_name != oneRec.LastName)
            {
                result = false;
            }
            else
            {
                student.name = oneRec.FirstName;
                student.last_name = oneRec.LastName;
                student.nation = oneRec.Nation;
                student.passport_id = oneRec.Passport_id;
                student.report_office = oneRec.Office_report;
                student.heath_check = oneRec.health_check;
                student.math_ = oneRec.StudentGrades.Math;
                student.chinese_ = oneRec.StudentGrades.Chinese;
                //student.math_ = oneRec.StudentGrades.Math;
                //student.chinese_ = oneRec.StudentGrades.Chinese;
                stu_name = oneRec.FirstName;
                stu_lname = oneRec.LastName;
                result = true;
            }
            Console.Clear();
            return result;
        }

        public int StudPanel(MongoCRUD db,ref string stu_name,ref string lname)
        {
            Console.Clear();
            int opt = 0;
            bool correct=false;
            while(!correct)
            {
                try
                {
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Welcome " + " " + stu_name);
                    //
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Press 1: To View Profile");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Press 2: Semester Registration");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Press 3: To Check Results");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Press 4: To Go Back to Main Menu");
                    //
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.Write("Enter Choice:");
                    opt = Convert.ToInt32(Console.ReadLine());
                    correct = true;
                }
                catch
                {
                    correct = false;
                    Console.Clear();
                }
            }
            
           
            return opt;
        }

        public void StudResults()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("::Semester Results::");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Math:: " + student.math_);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Chinese:: " + student.chinese_);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("***Press Any Key to Go Back to Menu***");
            Console.ReadLine();
            Console.Clear();
        }

        public void StudReg(MongoCRUD db)
        {
            Console.Clear();
            string of;
            /*
             * Passport Id
             * Phone Number Id
             * Room Number
             * Question(Have you reported to the office)
             * Question(H)
             */
            var oneRec = db.LoadRecordById<StudentModel>("Students", student.name);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("Passport No.: ");
            oneRec.Passport_id = Convert.ToString(Console.ReadLine());
            student.passport_id = oneRec.Passport_id;
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("Nationality: ");
            oneRec.Nation = Convert.ToString(Console.ReadLine());
            student.nation = oneRec.Nation;

            //
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("Reported To the Office(Enter 'y' for Yes OR 'n' for No: ");
            of = Convert.ToString(Console.ReadLine());
            if (of == "y")
            {
               
                oneRec.Office_report = true;
                student.report_office = oneRec.Office_report;
            }
            if(of == "n")
            {
                oneRec.Office_report = false;
                student.report_office = oneRec.Office_report;
            }
            else
            {
                oneRec.Office_report = false;
                student.report_office = oneRec.Office_report;
            }
            //
            
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("Any Health Conditions:");
            oneRec.health_check = Convert.ToString(Console.ReadLine());
            student.heath_check = oneRec.health_check;
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            db.UpsertRecord("Students", oneRec.Id, oneRec);
            Console.WriteLine("");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("*****SuccessFully**** Press Any key to Go Back To Menu");
            Console.ReadLine();
            Console.Clear();

        }

        public void StudProfile(MongoCRUD db)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Name: " + student.name);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("LastName: " + student.last_name);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Passport No: " + student.passport_id);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Nationality: " + student.nation);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Math Results: " + student.math_);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Chinese Results: " + student.chinese_);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);

            Console.WriteLine("***Press Any Key to Go Back to Menu***");
            Console.ReadLine();
            Console.Clear();
        }

        public bool AdminLogin()
        {
            bool result;
            string u_name;
            string u_namex = "admin";
            string p_name;
            string p_namex = "pass";
            //string p_namex = "admin";
            //
            // Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("===============Teacher & Administrator Login Page============");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("===Enter User Name::-");
            u_name = Console.ReadLine();
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("===Enter Password::-");
            p_name = Console.ReadLine();
            //
            if (u_name == u_namex && p_name == p_namex)
            {
                result = true;
            }
            else
            {
                result = false;
            }

             return result;
        }

        public int AdminPanel()
        {
            int opt = 0; ;
            bool correct = false;
            while (!correct)
            {
                try
                {
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("==============Welcome To The Administrators/Teachers Panel============");
                    //
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("==Press 1: To Insert==");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("==Press 2: To Show all Students==");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("==Press 3: To Search for Record==");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("==Press 4: To Delete Student Record==");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("==Press 5: To Go Back to the Main Page==");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.Write("Enter Choice:");
                    opt = Convert.ToInt32(Console.ReadLine());
                    correct = true;
                }
                catch
                {
                    Console.Clear();
                    correct = false;
                    //opt = 0;

                }
            }
            
            return opt;

            //
        }

        public void WelcomeScreen( ref int opt_main)
        {
            //int opt_main;
            //string s = "Hello|World";
            
            //Welcome Options
            bool correct=false;
            while (!correct)
            {
                try
                {
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    //Welcome Message
                    Console.WriteLine("_____   ___    ____    ___      ___   __   ");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|      |   |  |    |  |     |  |     |  |  |");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|__    |   |  |__ /   |___  |  |__|  |  |  |");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|      |   |  |   |   |     |     |  |  |  |");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|      |___|  |   |   |___  |     |  |  |__|");
                    // 
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine(" ___   _____         ___     ___   __     _____");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|   |    |    |   |  |      |     |  |  |   |  ");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|___     |    |   |  |   |  |___  |  |  |   | ");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("    |    |    |   |  |   |  |     |  |  |   | ");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|___|    |    |___|  |___|  |___  |  |__|   | ");
                    //
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine(" __     ____   _____  ____    ___    ____    ___    ____ ");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|      |    |    |   |    |  |   |  |    |  |   |  |     ");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|   |  |____|    |   |____|  | __|  |____|  |___   |____ ");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|   |  |    |    |   |    |  |    | |    |      |  |     ");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("|___|  |    |    |   |    |  |____| |    |  |___|  |____ ");
                    /*
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("===         Foreign  Student Database                =====");
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("==================Coded By Colin==========================");
                    */
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("==========================================================");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Press 1: Teacher Login");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Press 2: Student Login");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Press 3: To Exit");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.Write("Enter Choice:");
                    opt_main = Convert.ToInt32(Console.ReadLine());
                    correct = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Entered invalid Option");
                    correct = false;
                    Console.Clear();
                }
            }
            

            Console.Clear();
           // Console.ReadLine();
      

        }

        //Show All Records
        public void ShowAllRecords(MongoCRUD db)
        {
            var recs = db.LoadRecords<StudentModel>("Students");

            foreach (var rec in recs)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("First Name: "+ $"{rec.FirstName} " + "     " + "Last Name: "+ $"{rec.LastName}");
                if (rec.StudentGrades != null)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine(":::Grades:::");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Math: " + rec.StudentGrades.Math);
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                    Console.WriteLine("Chinese: " + rec.StudentGrades.Chinese);
                }
                
            }
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("Press Any Key to Go Back to the Menu");
            Console.ReadLine();
            Console.Clear();
        }

        //Insert Record
        public void InsertRecords(MongoCRUD db)
        {
            string fname;
            string lname;
            string math_g;
            string chinese_g;
            //
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("===============================================");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("===============INSERT RECORD===================");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("===============================================");
            //
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("=Enter Student First Name:");
            fname = Convert.ToString(Console.ReadLine());

            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("=Enter Student Last Name:");
            lname = Convert.ToString(Console.ReadLine());

            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("=Enter Student Student Math Score:");
            math_g = Convert.ToString(Console.ReadLine());

            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("=Enter Student Student Chinese Score:");
            chinese_g = Convert.ToString(Console.ReadLine());

            StudentModel student = new StudentModel
            {
                FirstName = fname,
                LastName = lname,
                StudentGrades = new GradesModel
                {
                    Math = math_g,
                    Chinese = chinese_g,
                }
            };
            db.InsertRecord("Students", student);
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Record Saved Successfully....Press Any key to Go back to Menu");
            Console.ReadLine();
            Console.Clear();
        }

        //Search Records
        public void SearchRecords(MongoCRUD db)
        {
            string search;
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("Enter Student first name:");
            search = Convert.ToString(Console.ReadLine());
            var oneRec = db.LoadRecordById<StudentModel>("Students", search);
            if(oneRec == null)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("Student does not Exist");
            }
            else
            {
                Console.WriteLine("");
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("****Student Found****");
                Console.WriteLine("");
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("First Name: " + oneRec.FirstName);
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("Last Name: " + oneRec.LastName);
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine(":::Grades:::");
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("Math: " + oneRec.StudentGrades.Math);
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("Chinese: " + oneRec.StudentGrades.Chinese);
                Console.WriteLine("");
            }
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Press Any key to go back to Menu");
            Console.ReadLine();
            Console.Clear();
        }

        public void DeleteRecords(MongoCRUD db)
        {
            string search;
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.Write("Enter Student first name:");
            search = Convert.ToString(Console.ReadLine());
            var oneRec = db.LoadRecordById<StudentModel>("Students", search);
            if (oneRec == null)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("Student does not Exist");
            }
            else
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("Student "+ oneRec.FirstName + " Deleted from Database");
                db.DeleteRecord<StudentModel>("Students", oneRec.Id);
                
            }
            Console.WriteLine("");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.CursorTop);
            Console.WriteLine("Press Any key to go back to Menu");
            Console.ReadLine();
            Console.Clear();
        }


        //Main Function
        static void Main(string[] args)
        {
          
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

            int opt_main = 0;
            int opt;
            bool result;
            int trys_left = 3;

            //Method for our class
            Program vox = new Program();
            
            //Initialise the Database to use
            MongoCRUD db = new MongoCRUD("StudentDatabase");

            string stu_name = " ";
            string stu_lname = " ";

            //Call Welcome Screen
            vox.WelcomeScreen(ref opt_main);

            //While loop that exists the program when input is 3
            while (opt_main != 3)
            {
                //If input is 1 then Call Admin/Teacher Function
                if (opt_main == 1)
                {
      
                    //Call AdminLogin Function for Authentication
                    //Wait for message to check if user was authenticated
                    result=vox.AdminLogin();
                    bool checker = false;
                    string chec;
                    //If result is True then Enter Show AdminPanel
                    if (result)
                    {
                        //Call Admin Panel
                        Console.Clear();
                        opt = vox.AdminPanel();
                        //
                        if (opt == 1)
                        {
                            Console.Clear();
                            vox.InsertRecords(db);
                            opt = vox.AdminPanel();

                        }
                        if (opt == 2)
                        {
                            Console.Clear();
                            vox.ShowAllRecords(db);
                            opt=vox.AdminPanel();
                        }
                        if (opt == 3)
                        {
                            Console.Clear();
                            vox.SearchRecords(db);
                            opt = vox.AdminPanel();
                        }
                        if (opt == 4)
                        {
                            Console.Clear();
                            vox.DeleteRecords(db);
                            opt = vox.AdminPanel();
                        }
                        if (opt == 5)
                        {
                            Console.Clear();
                            vox.WelcomeScreen(ref opt_main);
                        }
                    }
                    if (trys_left <= 1)
                    {
                        Console.Clear();
                        trys_left = 4;
                        vox.WelcomeScreen(ref opt_main);
                    }
                    if (!result)
                    {
                        Console.Clear();
                        --trys_left;
                        Console.WriteLine("Wrong Password Try Again " + trys_left);
                    }
                 
                    //Console.Write("-::");
                }
                if (opt_main == 2)
                {
                    //Call Student Login
                   
                    result =vox.StudLogin(db,ref stu_name,ref stu_lname);
                    if (result)
                    {
                        //Call Student Panel
                        opt = vox.StudPanel(db,ref stu_name,ref stu_lname);
                        //
                        if (opt == 1)
                        {
                            vox.StudProfile(db);
                            opt = vox.StudPanel(db, ref stu_name, ref stu_lname);
                        }
                        if (opt == 2)
                        {
                            vox.StudReg(db);
                            opt = vox.StudPanel(db, ref stu_name, ref stu_lname);
                        }
                        if (opt == 3)
                        {
                            vox.StudResults();
                            opt = vox.StudPanel(db, ref stu_name, ref stu_lname);
                        }
                        if (opt == 4)
                        {
                            Console.Clear();
                            vox.WelcomeScreen(ref opt_main);
                        }
                        //
                    }

                    if (trys_left <= 1)
                    {
                        Console.Clear();
                        trys_left = 4;
                        vox.WelcomeScreen(ref opt_main);
                    }
                    if (!result)
                    {
                        Console.Clear();
                        --trys_left;
                        Console.WriteLine("Wrong Password Try Again " + trys_left);
                    }

                }
        
            }
            //Clear Screen
            Console.Clear();
            //Exit Message 
            Console.WriteLine("Thank You for Using the Database");
            Console.WriteLine("You can Get this Code on My Github: 'Codzaa'");
            Console.ReadLine();
            //Environment.Exit(0);
        }

        //=========================================MongoDB Section================================================================//

        //Student Model
        public class StudentModel
        {
            [BsonId]
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Nation { get; set; }
            public bool Office_report { get; set; }
            public string health_check { get; set; }
            public string Passport_id { get; set; }
            public string Phone_number { get; set; }
            public GradesModel StudentGrades { get; set; }  

        }

        //Student Model-Minimal STUFF
        public class Student_min_Model
        {
            [BsonId]
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public GradesModel StudentGrades { get; set; }

        }

        //Grades Model
        public class GradesModel
        {
            public string Math { get; set; }
            public string Chinese { get; set; }

        }  

        //MongoDB Class
        public class MongoCRUD
        {
            //Initiliasing an Interface which represents a Database 
            private IMongoDatabase db;
            
            //Initiliasing the database Connection
            public MongoCRUD(String database)
            {
                //Initialising a new instance for the MongoDB Client
                var client = new MongoClient();
                db = client.GetDatabase(database);

            }
            //Insert Record
            public void InsertRecord<T>(string table,T record)
            {
                var collection = db.GetCollection<T>(table);
                collection.InsertOne(record);
            }

            //Fetch all Records
            public List<T> LoadRecords<T>(string table)
            {
                var collection = db.GetCollection<T>(table);
                return collection.Find(new BsonDocument()).ToList();
            }

            //Search for a Record
            public T LoadRecordById<T>(string table, string FirstName)
            {
                var collection = db.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("FirstName", FirstName);
                try
                {
                    var results = collection.Find(filter).First();
                    return results;
                }
                catch
                {
                    return default;
                }
              
                
            }

            //Update or Insert a new Record
            public void UpsertRecord<T>(string table,Guid id,T record)
            {
                var collection = db.GetCollection<T>(table);

                var result = collection.ReplaceOne(
                    new BsonDocument("_id", id),
                    record, 
                    new ReplaceOptions { IsUpsert = true });
            }

            //Delete a Record
            public void DeleteRecord<T>(string table,Guid id)
            {
                var collection = db.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("Id", id);
                collection.DeleteOne(filter);
            }
        }

    
    }
}
