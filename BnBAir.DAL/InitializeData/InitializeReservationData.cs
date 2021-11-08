using System;
using System.Linq;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.InitializeData
{
    public class InitializeReservationData
    {
        public static void InitData(ReservationContext context)
        {
            if (!context.CategoryDates.Any())
            {
                Category oneBed = new Category()
                {
                    Bed = 1,
                    Name = "Однокомнатный",
                } ;
                Category twoBed = new Category()
                {
                    Bed = 2,
                    Name = "Двухкомнатный",
                };
                context.Categories.Add(oneBed);
                context.Categories.Add(twoBed);
                context.SaveChanges();
                
                CategoryDate categoryDate1 = new CategoryDate()
                {
                    StartDate = new DateTime(2021, 10, 25),
                    EndDate = new DateTime(2022, 05, 24),
                    Price = 300,
                    Category = oneBed
                };
                CategoryDate categoryDate2 = new CategoryDate()
                {
                    StartDate = new DateTime(2021, 07, 15),
                    EndDate = new DateTime(2022, 06, 10),
                    Price = 500,
                    Category = twoBed
                };
                
                context.CategoryDates.Add(categoryDate1);
                context.CategoryDates.Add(categoryDate2);
                context.SaveChanges();

                Room room1 = new Room()
                {
                    Number = 1,
                    Category = oneBed
                };
                Room room2 = new Room()
                {
                    Number = 2,
                    Category = twoBed
                };
                Room room3 = new Room()
                {
                    Number = 3,
                    Category = twoBed
                };
                context.Rooms.Add(room1);
                context.Rooms.Add(room2);
                context.Rooms.Add(room3);
                context.SaveChanges();

                Guest tom = new Guest()
                {
                    FirstName = "Tom",
                    LastName = "Mot",
                    Patronymic = "Tommot",
                    BirthDate = new DateTime(2001,01,01),
                    Document = "TM200300"
                };
                Guest rick = new Guest()
                {
                    FirstName = "Rick",
                    LastName = "Kcir",
                    Patronymic = "Rickkcir",
                    BirthDate = new DateTime(2002,02,02),
                    Document = "TM300400"
                };
                context.Guests.Add(tom);
                context.Guests.Add(rick);
                context.SaveChanges();
                
                /*context.Reservations.Add(new Reservation()
                {
                    Guest = tom,
                    Room = room1,
                    StartDate = new DateTime(2021,11,05),
                    EndDate = new DateTime(2021,12,02),
                    CheckIn = false,
                    CheckOut = false
                });
                context.Reservations.Add(new Reservation()
                {
                    Guest = rick,
                    Room = room2,
                    StartDate = new DateTime(2021,11,09),
                    EndDate = new DateTime(2021,11,12),
                    CheckIn = false,
                    CheckOut = false
                });*/
                context.SaveChanges();
                
                
            }
        }
    }
}