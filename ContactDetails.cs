using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contactos
{
    public partial class ContactDetails : Form
    {
        private CapaLogica _capaLogica;
        private Contacts _contacts;

        public ContactDetails()
        {
            InitializeComponent();
            _capaLogica = new CapaLogica();
        }

        private void ContactDetails_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Main)this.Owner).PopulateContacts();


        }

        private void SaveContact()
        {
            Contacts contacs = new Contacts();
            contacs.FirstName = txtFirstName.Text;
            contacs.LastName = txtLastName.Text;
            contacs.Phone = txtPhone.Text;
            contacs.Address = txtAddress.Text;

            contacs.Id = _contacts != null ? _contacts.Id : 0;
            _capaLogica.SaveContacts(contacs);

        }


        public void LoadContact(Contacts contact)
        {
            _contacts = contact;
            if (contact != null)
            {
                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtAddress.Text = contact.Address;   
            }
            

        }

        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;

        }
    }
}
