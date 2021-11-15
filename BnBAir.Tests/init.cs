using System;
using BnBAir.API.Models;
using BnBAir.BLL.DTO;

namespace BnBAir.Tests
{
    public class Init
    {
                public static CategoryDTO oneBed = new CategoryDTO()
                {
                    Bed = 1,
                    Name = "Однокомнатный",
                } ;

                public static CategoryDTO twoBed = new CategoryDTO()
                {
                    Bed = 2,
                    Name = "Двухкомнатный",
                };
                public CategoryDateDTO categoryDate1 = new CategoryDateDTO()
                {
                    StartDate = new DateTime(2021, 10, 25),
                    EndDate = new DateTime(2022, 05, 24),
                    Price = 300,
                    Category = oneBed
                };
                public  CategoryDateDTO categoryDate2 = new CategoryDateDTO()
                {
                    StartDate = new DateTime(2021, 07, 15),
                    EndDate = new DateTime(2022, 06, 10),
                    Price = 500,
                    Category = twoBed
                };

                public RoomDTO room1 = new RoomDTO()
                {
                    Number = 1,
                    Category = oneBed
                };

                public RoomDTO room2 = new RoomDTO()
                {
                    Number = 2,
                    Category = twoBed
                };
                public  RoomDTO room3 = new RoomDTO()
                {
                    Number = 3,
                    Category = twoBed
                };

                public  GuestDTO tom = new GuestDTO()
                {
                    FirstName = "Tom",
                    LastName = "Mot",
                    Patronymic = "Tommot",
                    BirthDate = new DateTime(2001,01,01),
                    Document = "TM200300"
                };

                public GuestDTO rick = new GuestDTO()
                {
                    FirstName = "Rick",
                    LastName = "Kcir",
                    Patronymic = "Rickkcir",
                    BirthDate = new DateTime(2002,02,02),
                    Document = "TM300400"
                };
    }
}