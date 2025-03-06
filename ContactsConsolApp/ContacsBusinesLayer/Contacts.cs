using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using ContactsDataAccessLayer;

namespace ContactsBusinesLayer
{
    public class clsContact
    {
        public enum enMode { AddNew = 0, Update = 11 }
        public enMode Mode = enMode.AddNew;
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }

        public int CountryID { get; set; }

        private clsContact(int ID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime BirthDate, int CountryID) {

            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.BirthDate = BirthDate;
            this.CountryID = CountryID;
            Mode = enMode.Update;

        }
        public clsContact()
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.BirthDate = DateTime.Now;
            this.CountryID = -1;
            this.Phone = "";
            this.Address = "";
            Mode = enMode.AddNew;
        }
        public static clsContact Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "";
            int CountryID = -1;
            DateTime BirthDate = DateTime.Now;
            if (clsContactDataAccess.GetContactInfoByID(ID, ref FirstName, ref LastName, ref Email, ref Phone, ref Address, ref BirthDate, ref CountryID))
            {
                return new clsContact(ID, FirstName, LastName, Email, Phone, Address, BirthDate, CountryID);
            } else
            { return null; }
        }

        private bool _AddNewContact()
        {
            this.ID = clsContactDataAccess.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.BirthDate, this.CountryID);
            return this.ID != -1;
        }
        private bool _UpdateContact()
        {
            return clsContactDataAccess.UpdateContact(this.ID,this.FirstName,this.LastName,this.Email,this.Phone,this.Address,this.BirthDate,this.CountryID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else { return false; }
                case enMode.Update:

                    return _UpdateContact();
            }
            return true;
        }

        public static bool DeleteContact(int ID)
        {
            return clsContactDataAccess.DeleteContact(ID);
        }

        public static DataTable GetAllContacts()
        {
            return clsContactDataAccess.GetAllContacts();
        }
        public static bool IsContactExists(int ID)
        {
            return clsContactDataAccess.IsContactExists(ID);
        }
    }
}
