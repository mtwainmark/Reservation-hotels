using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp12.Helpers;
using WpfApp12.Models;
using WpfApp12.Views;

namespace WpfApp12.ViewModels
{
    public class ClientViewModel : BaseModel
    {
        #region Variables
        ApplicationContext db;
        Guests Client;

        private RelayCommand clientAddCommand;
        private RelayCommand clientEditCommand;
        private RelayCommand clientDeleteCommand;

        private IEnumerable<Guests> clients;
        #endregion

        #region Properties
        public IEnumerable<Guests> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged("Clients");
            }
        }
       
        #endregion

        #region Methods
        private async void ConnectingDatabase()
        {
            await db.Guests.LoadAsync();
            Clients = db.Guests.Local.ToBindingList();

        }
        #endregion

        #region Constructor
        public ClientViewModel()
        {
            db = new ApplicationContext();
            Client = new Guests();
            ConnectingDatabase();
        }
        #endregion

        #region Commands
        //Добавить клиента
        public RelayCommand ClientAddCommand => clientAddCommand ??
               (clientAddCommand = new RelayCommand((o) =>
               {
                   AddClientView view = new AddClientView(new Guests());
                   if (view.ShowDialog() == true)
                   {
                       Guests clientadd = view.Guest;

                       var suruse = db.Guests.FirstOrDefault(p => p.Pasport == clientadd.Pasport);
                       if (suruse == null)
                       {
                           db.Guests.Add(clientadd);
                           db.SaveChanges();
                       }
                   }
               }));
        //Сохранить изменения
        public RelayCommand ClientEditCommand => clientEditCommand ??
              (clientEditCommand = new RelayCommand((selectedItem) =>
              {
                  if (selectedItem == null)
                  {
                      return;
                  }

                  Guests clientedit = selectedItem as Guests;
                  try
                  {
                      db.Entry(clientedit).State = EntityState.Modified;
                      db.SaveChanges();
                  }
                  catch
                  {
                      db.SaveChanges();
                  }
              }));
        //Удалить клиента
        public RelayCommand ClientDeleteCommand => clientDeleteCommand ??
              (clientDeleteCommand = new RelayCommand((selectedItem) =>
              {
                  if (selectedItem == null)
                  {
                      return;
                  }

                  Guests clientremove = selectedItem as Guests;
                  db.Entry(clientremove).State = EntityState.Deleted;
                  db.Guests.Remove(clientremove);
                  db.SaveChanges();
              }));
        #endregion
    }
}
