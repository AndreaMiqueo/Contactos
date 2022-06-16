using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos
{
    class CapaLogica
    {

        private AccesoDatos _accesoDatos;


        public CapaLogica()
        {
            _accesoDatos = new AccesoDatos();
        }
        public Contacts SaveContacts( Contacts contacs)
        {
            if (contacs.Id == 0)
            {
                _accesoDatos.InsertContact(contacs);
            }
            else
            {
                _accesoDatos.UpdateContact(contacs);
            }

            return contacs;
        }

        public List<Contacts> GetContacts(string SearchTxt = null)
        {
           return _accesoDatos.GetContacts(SearchTxt);
        }

        public void DeleteContact(int Id)
        {
            _accesoDatos.DeleteContact(Id);
        }
    }
}
