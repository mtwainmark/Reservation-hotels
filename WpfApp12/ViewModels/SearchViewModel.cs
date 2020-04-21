using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfApp12.Helpers;
using WpfApp12.Models;
using WpfApp12.Views;

namespace WpfApp12.ViewModels
{
    public class SearchViewModel : BaseModel, IDataErrorInfo
    {
        #region Validation
        private static readonly string[] RequiredProperties = { "City", "FromDate", "ToDate" };
        private readonly HashSet<string> invalidProperty = new HashSet<string>();
        private bool isValid;
        private bool checkOnNull;
        public string this[string propertyName]
        {
            get
            {
                string error = String.Empty;
                string nullCheckErrorMessage = "Не может быть пустым";

                //Диаметр
                if (propertyName == "City")
                {
                    if (!String.IsNullOrEmpty(City))
                    {
                        invalidProperty.Remove(propertyName);
                    }
                    else if (checkOnNull)
                    {
                        error = nullCheckErrorMessage;
                        invalidProperty.Add(propertyName);
                    }
                }
                //Длина
                else if (propertyName == "FromDate")
                {
                    if (FromDate.Date < DateTime.Today)
                    {
                        error = "Нельзя забронировать прошлое";
                        invalidProperty.Add(propertyName);
                    }
                }
                // Каверзность 
                else if (propertyName == "ToDate")
                {
                    if (ToDate.Date < FromDate.Date)
                    {
                        error = "Дата выезда должна быть больше даты заезда";
                        invalidProperty.Add(propertyName);
                    }
                }

                return IsValidationEnabled ? error : string.Empty;
            }
        }
        public string Error => null;
        public bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                OnPropertyChanged();
            }
        }

        public bool IsValidationEnabled { get; set; }

        public void CheckValidityAllProperties() // Проверка валидации
        {

            foreach (string property in RequiredProperties)
            {
                OnPropertyChanged(property);
            }

            checkOnNull = true;
            foreach (string property in RequiredProperties)
            {
                OnPropertyChanged(property);
            }
            checkOnNull = false;

            isValid = invalidProperty.Count == 0 ? true : false;
            if (IsValid)
            {

            }
            else
            {
                invalidProperty.Clear();
                RoomsList.Clear();
            }
        }
        #endregion

        #region Variables
        ApplicationContext db;
        Hotels hotel;
        Rooms room;
        private List<string> cityList;
        SearchModel selectedRoom;
        private ObservableCollection<SearchModel> roomsList;

        private RelayCommand searchCommand;
        private RelayCommand removeGuests;
        private RelayCommand addGuests;
        private RelayCommand commandSelect;

        private bool luxRoom;
        private bool standartRoom;

        Guests guest;
        private DateTime fromDate = DateTime.Now;
        private DateTime toDate = DateTime.Now.AddDays(1);
        #endregion

        #region Properties
        public bool LuxRoom
        {
            get => luxRoom;
            set
            {
                luxRoom = value;
                OnPropertyChanged("LuxRoom");
            }
        }
        public bool StandartRoom
        {
            get => standartRoom;
            set
            {
                standartRoom = value;
                OnPropertyChanged("StandartRoom");
            }
        }
        public string City
        {
            get => hotel.City;
            set
            {
                hotel.City = value;
                OnPropertyChanged("City");
            }
        }
        public int Stars
        {
            get => hotel.Stars;
            set
            {
                hotel.Stars = value;
                OnPropertyChanged("Stars");
            }
        }

        public int CountGuests
        {
            get => room.CountGuests;
            set
            {
                room.CountGuests = value;
                OnPropertyChanged("CountGuests");
            }
        }

        public DateTime FromDate
        {
            get => fromDate;
            set
            {
                fromDate = value;
                OnPropertyChanged("FromDate");
            }
        }

        public DateTime ToDate
        {
            get => toDate;
            set
            {
                toDate = value;
                OnPropertyChanged("ToDate");
            }
        }

        public SearchModel SelectedRoom
        {
            get => selectedRoom;
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }

        public List<string> CityList
        {
            get => cityList;
            set
            {
                cityList = value;
                OnPropertyChanged("CityList");
            }
        }

        public ObservableCollection<SearchModel> RoomsList
        {
            get => roomsList;
            set
            {
                roomsList = value;
                OnPropertyChanged("RoomsList");
            }
        }


        #endregion

        #region Methods

        private string TypesRoom()
        {
            if (StandartRoom)
                return "Стандарт";
            else if (LuxRoom)
                return "Люкс";
            else
                return "";
        }
        //Добавляем в Combobox все города из бд
        private void AddCities()
        {
            var cities = db.Hotels.Select(p => p.City);
            foreach (var item in cities)
            {
                CityList.Add(item);

            }
            CityList = CityList.Distinct().ToList();
        }

        //Выполняем поиск номера по заданным параметрам
        private void Search()
        {
            RoomsList.Clear();
            string typeRoom = TypesRoom();

            //Проверяем отель по параметрам
            var hotels = db.Hotels.Where(p => p.City == City && p.Stars == Stars);
            foreach (var hotel in hotels)
            {
                //Проверяем номер по параметрам
                var rooms = db.Rooms.Where(p => p.IdHotels == hotel.IdHotels && p.CountGuests == CountGuests && p.TypeRoom == typeRoom);
                foreach (var item in rooms)
                {
                    //Не зарезервировано ни одного отеля
                    var notReservationFirst = db.Reservations.FirstOrDefault(p => p.IdRoom != 0);
                    if (notReservationFirst != null)
                    {
                        //Вывод всеъ отелей, которые не забронированы
                        var notReservation = db.Reservations.Where(p => p.IdRoom != item.IdRoom);
                        foreach (var j in notReservation)
                        {
                            SearchModel search = new SearchModel()
                            {
                                IdHotels = hotel.IdHotels,
                                IdRoom = item.IdRoom,
                                Title = hotel.Title,
                                Stars = hotel.Stars,
                                City = hotel.City,
                                TypeRoom = item.TypeRoom,
                                CountGuests = item.CountGuests
                            };
                            RoomsList.Add(search);
                            break;
                        }
                        //Вывод забронированных отелей, которые подходят по выбранным датам
                        var ReservationButDate = db.Reservations.Where(p => (p.IdRoom == item.IdRoom) && ((p.FromDate < FromDate.Date && p.FromDate <= ToDate.Date) && (p.ToDate <= FromDate.Date && p.ToDate < ToDate.Date)) || ((p.IdRoom == item.IdRoom) && (p.FromDate > FromDate.Date && p.FromDate >= ToDate.Date)));
                        foreach (var k in ReservationButDate)
                        {
                            SearchModel search = new SearchModel()
                            {
                                IdHotels = hotel.IdHotels,
                                IdRoom = item.IdRoom,
                                Title = hotel.Title,
                                Stars = hotel.Stars,
                                City = hotel.City,
                                TypeRoom = item.TypeRoom,
                                CountGuests = item.CountGuests
                            };
                            RoomsList.Add(search);
                        }
                    }
                    else
                    {
                        SearchModel search = new SearchModel()
                        {
                            IdHotels = hotel.IdHotels,
                            IdRoom = item.IdRoom,
                            Title = hotel.Title,
                            Stars = hotel.Stars,
                            City = hotel.City,
                            TypeRoom = item.TypeRoom,
                            CountGuests = item.CountGuests
                        };
                        RoomsList.Add(search);
                    }
                }

            }


            if (RoomsList.Count == 0)
            {
                SearchModel searchNone = new SearchModel()
                {
                    Title = "не найден"
                };
                RoomsList.Add(searchNone);
            }



        }

        private void Search2()
        {
            RoomsList.Clear();
            string typeRoom = TypesRoom();

            List<int> index = new List<int>();
            List<int> index2 = new List<int>();

            //Получаем забронированные
            var reserve1 = db.Reservations.Select(p => p.IdRoom);
            foreach (var i in reserve1)
            {
                index.Add(i);
            }
            //Получаем Id номеров
            var room1 = db.Rooms.Select(p => p.IdRoom);
            foreach (var j in room1)
            {
                index2.Add(j);
            }

            //Удаляем забронированные номера
            for (int i = 0; i < index2.Count; i++)
            {
                for (int j = 0; j < index.Count; j++)
                {
                    if (index2[i] == index[j])
                        index2.Remove(index[j]);
                }
            }





            var hotels = db.Hotels.Where(p => p.City == City && p.Stars == Stars);
            foreach (var hotel in hotels)
            {
                for (int i = 0; i < index2.Count; i++)
                {
                    int kk = index2[i];
                    var room = db.Rooms.Where(p => p.IdRoom == kk && p.IdHotels == hotel.IdHotels && p.CountGuests == CountGuests && p.TypeRoom == typeRoom);
                    foreach (var j in room)
                    {
                        SearchModel search = new SearchModel()
                        {
                            IdHotels = hotel.IdHotels,
                            IdRoom = j.IdRoom,
                            Title = hotel.Title,
                            Stars = hotel.Stars,
                            City = hotel.City,
                            TypeRoom = j.TypeRoom,
                            CountGuests = j.CountGuests
                        };
                        RoomsList.Add(search);
                    }
                }
            }

            var result3 = from room in db.Rooms
                          join hotel in db.Hotels on room.IdHotels equals hotel.IdHotels
                          join reserve in db.Reservations on room.IdRoom equals reserve.IdRoom
                          where hotel.City == City && hotel.Stars == Stars && room.CountGuests == CountGuests && room.TypeRoom == typeRoom &&
                          ((reserve.FromDate < FromDate.Date && reserve.FromDate <= ToDate.Date) && (reserve.ToDate <= FromDate.Date && reserve.ToDate < ToDate.Date)) || ((reserve.FromDate > FromDate.Date && reserve.FromDate >= ToDate.Date))
                          select new
                          {
                              IdHotels = room.IdHotels,
                              IdRoom = room.IdRoom,
                              Title = hotel.Title,
                              City = hotel.City,
                              Stars = hotel.Stars,
                              TypeRoom = room.TypeRoom,
                              CountGuests = room.CountGuests,
                          };

            foreach (var item in result3)
            {
                SearchModel search = new SearchModel()
                {
                    IdHotels = item.IdHotels,
                    IdRoom = item.IdRoom,
                    Title = item.Title,
                    Stars = item.Stars,
                    City = item.City,
                    TypeRoom = item.TypeRoom,
                    CountGuests = item.CountGuests
                };
                RoomsList.Add(search);
            }


            if (RoomsList.Count == 0)
            {
                SearchModel searchNone = new SearchModel()
                {
                    Title = "не найден"
                };
                RoomsList.Add(searchNone);
            }


        }
        #endregion

        #region Constructor
        public SearchViewModel()
        {
            db = new ApplicationContext();
            hotel = new Hotels();
            room = new Rooms();
            cityList = new List<string>();
            roomsList = new ObservableCollection<SearchModel>();

            IsValidationEnabled = true;
            StandartRoom = true;
            CountGuests = 1;

            AddCities();
        }
        #endregion

        #region Commands
        public RelayCommand SearchCommand => searchCommand ??
              (searchCommand = new RelayCommand((o) =>
              {
                  CheckValidityAllProperties(); // Проверка валидации 
                  if (isValid)
                      Search2();
              }));

        //Уменьшаем количество гостей
        public RelayCommand RemoveGuests => removeGuests ??
            (removeGuests = new RelayCommand((o) =>
            {
                if (CountGuests > 1)
                {
                    CountGuests--;
                }

            }));
        //Увеличиваем количество гостей
        public RelayCommand AddGuests => addGuests ??
            (addGuests = new RelayCommand((o) =>
            {
                CountGuests++;
            }));

        public RelayCommand CommandSelect => commandSelect ??
            (commandSelect = new RelayCommand((o) =>
            {
                try
                {
                    
                    AddClientView view = new AddClientView(guest = new Guests());
                    if (view.ShowDialog() == true)
                    {
                        Guests clientadd = view.Guest;
                        db.Guests.Add(clientadd);
                        db.SaveChanges();
                    }
                    
                    Reservations res = new Reservations()
                    {
                        IdHotels = SelectedRoom.IdHotels,
                        IdRoom = SelectedRoom.IdRoom,
                        IdGuest = guest.IdGuest,
                        FromDate = FromDate,
                        ToDate = ToDate
                    };
                    db.Reservations.Add(res);
                    db.SaveChanges();

                }
                catch { }
            }));

        #endregion
    }
}
