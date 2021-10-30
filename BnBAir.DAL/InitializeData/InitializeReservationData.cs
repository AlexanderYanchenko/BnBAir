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
                context.CategoryDates.Add(new CategoryDate()
                {
                   StartDate = new DateTime(2021,10,25),
                   EndDate = new DateTime(2022,05,24),
                   Price = 300
                });
                
                context.CategoryDates.Add(new CategoryDate()
                {
                    StartDate = new DateTime(2021,07,15),
                    EndDate = new DateTime(2022,06,10),
                    Price = 500
                });
                context.SaveChanges();
                
                
                context.Categories.Add(new Category()
                {
                    Bed = 1,
                    Name = "Однокомнатный",
                    CategoryDateId = context.CategoryDates.OrderBy(c=>c.CategoryDateId).First().CategoryDateId
                });
                context.Categories.Add(new Category()
                {
                    Bed = 2,
                    Name = "Двухкомнатный",
                    CategoryDateId = context.CategoryDates.OrderBy(c=>c.CategoryDateId).Last().CategoryDateId
                });
                context.SaveChanges();
                
                
                context.Rooms.Add(new Room()
                {
                    Number = 1,
                    CategoryId = context.Categories.OrderBy(c=>c.CategoryId).First().CategoryId,
                });
                context.Rooms.Add(new Room()
                {
                    Number = 2,
                    CategoryId = context.Categories.OrderBy(c=>c.CategoryId).First().CategoryId,
                });
                context.Rooms.Add(new Room()
                {
                    Number = 3,
                    CategoryId = context.Categories.OrderBy(c=>c.CategoryId).Last().CategoryId,
                });
                context.SaveChanges();
                
                
                context.Guests.Add(new Guest()
                {
                    FirstName = "Tom",
                    LastName = "Mot",
                    Patronymic = "Tommot",
                    BirthDate = new DateTime(2001,01,01),
                    Document = "TM200300"
                });
                context.Guests.Add(new Guest()
                {
                    FirstName = "Rick",
                    LastName = "Kcir",
                    Patronymic = "Rickkcir",
                    BirthDate = new DateTime(2002,02,02),
                    Document = "TM300400"
                });
                context.SaveChanges();

                
                context.Reservations.Add(new Reservation()
                {
                    GuestId = context.Guests.OrderBy(g=>g.GuestId).First().GuestId,
                    RoomId = context.Rooms.OrderBy(r=>r.RoomId).First().RoomId,
                    StartDate = new DateTime(2021,11,05),
                    EndDate = new DateTime(2021,12,02),
                    CheckIn = false,
                    CheckOut = false
                });
                context.Reservations.Add(new Reservation()
                {
                    GuestId = context.Guests.OrderBy(g=>g.GuestId).Last().GuestId,
                    RoomId = context.Rooms.OrderBy(r=>r.RoomId).Last().RoomId,
                    StartDate = new DateTime(2021,11,09),
                    EndDate = new DateTime(2021,11,12),
                    CheckIn = false,
                    CheckOut = false
                });
                context.SaveChanges();
            }
        }
    }
}