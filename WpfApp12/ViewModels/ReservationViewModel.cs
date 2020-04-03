using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp12.Helpers;
using WpfApp12.Models;

namespace WpfApp12.ViewModels
{
    public class ReservationViewModel : BaseModel
    {
        ApplicationContext db;
        private ObservableCollection<ReservationModel> reservationList;

        private RelayCommand reservationAddCommand;
        private RelayCommand reservationEditCommand;
        private RelayCommand reservationDeleteCommand;

        public ObservableCollection<ReservationModel> ReservationList
        {
            get => reservationList;
            set
            {
                reservationList = value;
                OnPropertyChanged("ReservationList");
            }
        }


        public ReservationViewModel()
        {
            db = new ApplicationContext();
            reservationList = new ObservableCollection<ReservationModel>();

         
            var result = from reserve in db.Reservations
                         join guest in db.Guests on reserve.IdGuest equals guest.IdGuest
                         join hotel in db.Hotels on reserve.IdHotels equals hotel.IdHotels
                         join room in db.Rooms on reserve.IdRoom equals room.IdRoom
                         select new
                         {
                             IdReservation = reserve.IdReservation,
                             City = hotel.City,
                             Hotel = hotel.Title,
                             Room = room.TypeRoom,
                             Client = guest.Surname + " " + guest.Name,
                             FromDate = reserve.FromDate,
                             ToDate = reserve.ToDate
                         };

            foreach (var item in result)
            {
                ReservationModel reservationModel = new ReservationModel()
                {
                    IdReservation = item.IdReservation,
                    City = item.City,
                    Hotel = item.Hotel,
                    Room = item.Room,
                    Client = item.Client,
                    FromDate = item.FromDate.ToString("d"),
                    ToDate = item.ToDate.ToString("d")
                };
                ReservationList.Add(reservationModel);
            }
        }

        //Добавить клиента
        public RelayCommand ReservationAddCommand => reservationAddCommand ??
               (reservationAddCommand = new RelayCommand((o) =>
               {

               }));
        //Сохранить изменения
        public RelayCommand ReservationEditCommand => reservationEditCommand ??
              (reservationEditCommand = new RelayCommand((selectedItem) =>
              {
                  if (selectedItem == null)
                  {
                      return;
                  }

                  Reservations clientedit = selectedItem as Reservations;
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
        public RelayCommand ReservationDeleteCommand => reservationDeleteCommand ??
              (reservationDeleteCommand = new RelayCommand((selectedItem) =>
              {
                  if (selectedItem == null)
                  {
                      return;
                  }

                  ReservationModel reserveremove = selectedItem as ReservationModel;
                  Reservations res = db.Reservations.FirstOrDefault(p => p.IdReservation == reserveremove.IdReservation);
                  ReservationList.Remove(reserveremove);
                  db.Reservations.Remove(res);
                  db.SaveChanges();

                 

              }));
    }
}
