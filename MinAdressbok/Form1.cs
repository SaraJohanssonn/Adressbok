using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MinAdressbok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ContactListbox.Items.Clear();
            LoadContacts();
            ContactListbox.Items.AddRange(contactList.ToArray());

        }

        class Person
        {
            public string Namn { get; set; }
            public string Telefon { get; set; }
            public string Epost { get; set; }
            public string Gatuadress { get; set; }
            public string Postnummer { get; set; }
            public string Postort { get; set; }

            public override string ToString()
            {
                return $"{Namn}\t{Telefon}\t{Epost}\t{Gatuadress}\t{Postnummer}\t{Postort}";
            }
        }

        
        List<Person> contactList = new List<Person>();
        string path = "adresser.txt";

        private void btnAdd_Click(object sender, EventArgs e)
        {   
            if (txtName.Text == "" || txtPhone.Text == "" || txtEmail.Text == "" || txtStreeAdress.Text == "" || txtZipCode.Text == "" || txtCity.Text == "")
            {
                MessageBox.Show("Alla fällt måste vara ifyllda");
            }
            else
            {
                Person contact = (Person)ContactListbox.SelectedItem;

                if (contact == null)
                {
                    contact = new Person
                    {
                        Namn = txtName.Text,
                        Telefon = txtPhone.Text,
                        Epost = txtEmail.Text,
                        Gatuadress = txtStreeAdress.Text,
                        Postnummer = txtZipCode.Text,
                        Postort = txtCity.Text
                        
                        
                    };
                    contactList.Add(contact);
                }
                else
                {
                    contact.Namn = txtName.Text;
                    contact.Telefon = txtPhone.Text;
                    contact.Epost = txtEmail.Text;
                    contact.Gatuadress = txtStreeAdress.Text;
                    contact.Postnummer = txtZipCode.Text;
                    contact.Postort = txtCity.Text;
                }
                
                ContactListbox.Items.Clear(); 
                ContactListbox.Items.AddRange(contactList.ToArray());

                SaveContacts();

                
                
                txtStreeAdress.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
                txtName.Text = "";
                txtZipCode.Text = "";
                txtCity.Text = "";

                txtSearch.Text = "";
            }

          
        }

        private void btnSearch_Click(object sender, EventArgs e) 
        {
            
            List<Person> matchedContacts = new List<Person>();                  
            foreach (Person contact in contactList)
            {
                if (contact.Postort.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    matchedContacts.Add(contact);
                }
            }

            foreach (Person contact in contactList)
            {
                if (contact.Namn.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    matchedContacts.Add(contact);
                }
            }

            if (matchedContacts.Count > 0)
            {
                ContactListbox.Items.Clear();
                ContactListbox.Items.AddRange(matchedContacts.ToArray());
            }
            else
            {
                MessageBox.Show("Finns inte!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) 
        {
            
            if (ContactListbox.SelectedItems.Count > 0)
            {
                
                foreach (Person contact in ContactListbox.SelectedItems)
                {
                    
                    contactList.Remove(contact);
                }

                SaveContacts();   
                ContactListbox.Items.Clear();
                ContactListbox.Items.AddRange(contactList.ToArray());
                txtSearch.Clear();

                txtStreeAdress.Text = ""; 
                txtPhone.Text = "";
                txtEmail.Text = "";
                txtName.Text = "";
                txtZipCode.Text = "";
                txtCity.Text = "";
            }
            else
            {
                
                MessageBox.Show("Välj kontakt att radera");
            }
        }
        
       
        private void SaveContacts() 
        {
            try //för att programmet ej ska krascha om det inte finns en fil att hämta data ifrån
            {
                using StreamWriter fileWriter = new StreamWriter(path);

                foreach (Person contact in contactList)
                {
                    fileWriter.WriteLine(contact.Namn);
                    fileWriter.WriteLine(contact.Telefon);
                    fileWriter.WriteLine(contact.Epost);
                    fileWriter.WriteLine(contact.Gatuadress);
                    fileWriter.WriteLine(contact.Postnummer);
                    fileWriter.WriteLine(contact.Postort);
                }
            }
            catch (Exception)
            {

            }
               
           

        }

        private void LoadContacts() 
        {
            contactList.Clear();

            try 
            {
                using StreamReader fileReader = new(path);

                string firstName = fileReader.ReadLine();

                while (firstName != null)
                {
                    
                    contactList.Add(new Person()
                    {
                        Namn = firstName,
                        Telefon = fileReader.ReadLine(),
                        Epost = fileReader.ReadLine(),
                        Gatuadress = fileReader.ReadLine(),
                        Postnummer = fileReader.ReadLine(),
                        Postort = fileReader.ReadLine()
                    });

                    firstName = fileReader.ReadLine();
                }
            }
            catch(IOException)
            {

            }
            
            
                     


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            txtStreeAdress.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtName.Text = "";
            txtZipCode.Text = "";
            txtCity.Text = "";

            
            ContactListbox.Items.Clear();
            ContactListbox.Items.AddRange(contactList.ToArray());
        }

        private void ContactListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            if (ContactListbox.SelectedIndex == -1)
                return;
            var contact = (Person)ContactListbox.SelectedItem;
             
                      
            txtName.Text = contact.Namn;
            txtPhone.Text = contact.Telefon;
            txtEmail.Text = contact.Epost;
            txtStreeAdress.Text = contact.Gatuadress;
            txtZipCode.Text = contact.Postnummer;
            txtCity.Text = contact.Postort;

        }
    }
}
