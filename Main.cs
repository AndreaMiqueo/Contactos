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
    public partial class Main : Form
    {
        private CapaLogica _capaLogica;
        public Main()
        {
            _capaLogica = new CapaLogica();
            InitializeComponent();
        }

        #region EVENTS
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailsDialog();
        }
        #endregion

        #region PRIVATE METHODS
        private void OpenContactDetailsDialog()
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails.ShowDialog(this);
        }
        #endregion

        private void Main_Load(object sender, EventArgs e)
        {

            PopulateContacts();
        }

        public void PopulateContacts(string SearchTxt = null)
        {
            List<Contacts> contacts = _capaLogica.GetContacts(SearchTxt);
            gridContacts.DataSource = contacts;
        }

        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if(cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadContact(new Contacts
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = gridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString()
                });
                contactDetails.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContacts();
            }

            
        }

        private void DeleteContact(int id)
        {
            _capaLogica.DeleteContact(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(textSearch.Text);
            textSearch.Text = string.Empty;
        }
    }
}
