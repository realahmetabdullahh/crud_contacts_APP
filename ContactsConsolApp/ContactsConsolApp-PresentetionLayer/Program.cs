using System;
using System.Data;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Services;
using ContactsBusinesLayer;
using ContactsBusinessLayer;


namespace ContactsConsolApp
{
    internal class Program

    {

        enum enPerform { enFind = 0, enAdd= 1, enUpdate = 2, enDelete = 3, enListAll = 4, enIsContactExists = 5 }
        static void FindContactByID(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);
            if (Contact1 != null)
            {
                Console.WriteLine("ID: " + Contact1.ID);
                Console.WriteLine("NAME: " + Contact1.FirstName + " " + Contact1.LastName);
                Console.WriteLine("PHONE: " + Contact1.Phone);
                Console.WriteLine("EMAIL: " + Contact1.Email);
                Console.WriteLine("ADDRESS: " + Contact1.Address);
                Console.WriteLine("BIRTH DATE: " + Contact1.BirthDate);
                Console.WriteLine("COUNTRY ID: " + Contact1.CountryID);

            }
            else
            {
                Console.WriteLine("COULD'NT FIND CONTACT WITH ID " + ID);
            }
        }
        static void AddNewContact()
        {
         
            clsContact contact1 = new clsContact();
            Console.WriteLine("Enter First Name");
            string FirstName = Console.ReadLine();
            contact1.FirstName = FirstName;
            Console.WriteLine("Enter Last Name");
            string LastName = Console.ReadLine();
            contact1.LastName = LastName;
            Console.WriteLine("Enter Address");
            string Address = Console.ReadLine();
            contact1.Address = Address;
            Console.WriteLine("Enter birthdate (MM/DD/YYYY)! ");
            DateTime birthdate = DateTime.TryParse(Console.ReadLine(), out DateTime parsedDate) ? parsedDate : DateTime.Now;
            Console.WriteLine("Enter Phone");
            string Phone = Console.ReadLine();
            contact1.Phone = Phone;
            Console.WriteLine("Enter Email");
            string Email = Console.ReadLine();
            contact1.Email = Email;
            Console.WriteLine("Enter CountryID");
            int CountryID = int.TryParse(Console.ReadLine(), out int parsedID) ? parsedID : -1;
            contact1.CountryID = CountryID;
            if (contact1.Save())
            {
                Console.WriteLine("Contact Added Succesfully");
            }
            else
            {
                Console.WriteLine("Contact Could Not Added");
            }
        }
        static void UpdateContact(int ID)
        {
            clsContact contact1 = clsContact.Find(ID);
            if (contact1 != null)
            {
                contact1.FirstName = "Cristiano ";
                contact1.LastName = "Ronaldo";
                contact1.Address = "Saudi Arabia";
                contact1.BirthDate = new DateTime(1980, 1, 1);
                contact1.Phone = "000";
                contact1.Email = "ronaldo@gmail.com";
                contact1.CountryID = 0;
                if (contact1.Save())
                {
                    Console.WriteLine("Contact Updated Succesfully");
                }
            }
            else
            {
                Console.WriteLine("Contact Not Found");
            }
        }
        static void DeleteContact(int ID) {
            if (clsContact.IsContactExists(ID))
            {
                if (clsContact.DeleteContact(ID))
                {
                    Console.WriteLine("Contact Deleted Succesfully");
                }
                else
                { Console.WriteLine("Contact Could Not Deleted"); }

            }
            else
            {
                Console.WriteLine("Contacts Not Exist");
            }


        }
        static void GetAllContacts()
        {
            DataTable dt = clsContact.GetAllContacts();
            Console.WriteLine("Contacts");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["ContactID"]},{row["FirstName"]} {row["LastName"]} ");
            }
        }

        static void IsContactExists(int ID) {
            if (clsContact.IsContactExists(ID)) {
                Console.WriteLine("Contact Exists");
            }
            else
            {
                Console.WriteLine("Contact Not Exists");
            }
        }



        static void FindCountryByID(int ID)

        {
            clsCountry Country1 = clsCountry.Find(ID);

            if (Country1 != null)
            {
                Console.WriteLine("Name: " + Country1.CountryName);
                Console.WriteLine("Code: " + Country1.Code);
                Console.WriteLine("PhoneCode: " + Country1.PhoneCode);

            }

            else
            {
                Console.WriteLine("Country [" + ID + "] Not found!");
            }
        }


        static void FindCountryByName(string CountryName)

        {
            clsCountry Country1 = clsCountry.Find(CountryName);

            if (Country1 != null)
            {
                Console.WriteLine("Country [" + CountryName + "] isFound with ID = " + Country1.ID);
                Console.WriteLine("Name: " + Country1.CountryName);
                Console.WriteLine("Code: " + Country1.Code);
                Console.WriteLine("PhoneCode: " + Country1.PhoneCode);
            }

            else
            {
                Console.WriteLine("Country [" + CountryName + "] Is Not found!");
            }
        }


        static void IsCountryExistByID(int ID)

        {

            if (clsCountry.isCountryExist(ID))

                Console.WriteLine("Yes, Country is there.");

            else
                Console.WriteLine("No, Country Is not there.");

        }

        static void IsCountryExistByName(string CountryName)

        {

            if (clsCountry.isCountryExist(CountryName))

                Console.WriteLine("Yes, Country is there.");

            else
                Console.WriteLine("No, Country Is not there.");

        }


        static void AddNewCountry(string CountryName, string Code, string PhoneCode)


        {
            clsCountry Country1 = new clsCountry();

            Country1.CountryName = CountryName;
            Country1.Code = Code;
            Country1.PhoneCode = PhoneCode;


            if (Country1.Save())
            {

                Console.WriteLine("Country Added Successfully with id=" + Country1.ID);
            }

        }

        static void UpdateCountry(int ID, string CountryName, string Code, string PhoneCode)

        {
            clsCountry Country1 = clsCountry.Find(ID);

            if (Country1 != null)
            {
               
                Country1.CountryName = CountryName;
                Country1.Code = Code;
                Country1.PhoneCode = PhoneCode;


                if (Country1.Save())
                {

                    Console.WriteLine("Country updated Successfully ");
                }

            }
            else
            {
                Console.WriteLine("Country is you want to update is Not found!");
            }
        }

        static void DeleteCountry(int ID)

        {

            if (clsCountry.isCountryExist(ID))

                if (clsCountry.DeleteCountry(ID))

                    Console.WriteLine("Country Deleted Successfully.");
                else
                    Console.WriteLine("Faild to delete Country.");

            else
                Console.WriteLine("Faild to delete: The Country with id = " + ID + " is not found");

        }

        static void ListCountries()
        {

            DataTable dataTable = clsCountry.GetAllCountries();

            Console.WriteLine("Coutries Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["CountryID"]},  {row["CountryName"]} , {row["Code"]}, {row["PhoneCode"]}");
            }

        }
                    
        static void PerformMainMenue(enPerform perform)
        {

            switch (perform) {
                    case enPerform.enListAll:
                    {
                        GetAllContacts();
                        break;
                    }
                    case enPerform.enDelete: {
                        Console.WriteLine("Enter Contact ID To Delete!");
                        if(int.TryParse(Console.ReadLine(), out int ID))
                        {
                            DeleteContact(ID);

                        }
                        else
                        {
                            Console.WriteLine("Invalid ID");
                        }
                       

                        break;
                    }

                    case enPerform.enFind: {

                        Console.WriteLine("Enter Contact ID To Find!");
                        if (int.TryParse(Console.ReadLine(),out int ID))
                        {
                            FindContactByID(ID);
                        }
                       
                        break;
                    }

                    case enPerform.enUpdate: {
                        Console.WriteLine("Enter Contact ID To Delete!");
                        if(!int.TryParse(Console.ReadLine(), out int ID))
                        {
                            UpdateContact(ID);
                        }break;
                    }

                    case enPerform.enAdd: {
                        AddNewContact();

                        break;
                    }

                    case enPerform.enIsContactExists: {
                        Console.WriteLine("Enter ID");
                        int ID = int.Parse(Console.ReadLine());
                        IsContactExists(ID);
                        break;
                    }
            
            }           


        }

        static void showMainMenue()
        {
            Console.WriteLine("Find = 0, Add= 1, Update = 2, Delete = 3, ListAll = 4, IsContactExists = 5");
            PerformMainMenue((enPerform)int.Parse(Console.ReadLine()));
        }

        static void Main(string[] args)
        {
            
          showMainMenue();
        }
    }
}
