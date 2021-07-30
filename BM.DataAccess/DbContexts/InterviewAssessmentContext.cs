using System;
using BM.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BM.DataAccess.DbContexts
{
    public class BMContext : DbContext
    {
        public BMContext(DbContextOptions<BMContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Slot> Slots { get; set; }

        ////protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ////{
        ////    optionsBuilder.UseSqlite($"Data Source=BM.db");
        ////}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Seeding the [Role] data set with dummy data
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                    Name = "Interviewer"
                },
                new Role
                {
                    RoleId = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                    Name = "Candidate"
                }
            );

            /* Seeding data set with dummy data
            /// Interviewer Mary is available
            /// 2021-07-19 09:00 - 2021-07-19 10:00
            /// 2021-07-19 10:00 - 2021-07-19 11:00
            ///
            /// Candidate John is available
            /// 2021-07-19 09:00 - 2021-07-19 10:00
            /// 2021-07-20 09:00 - 2021-07-19 10:00
            ////
            /// The only availability slot is 2021-07-19 09:00 - 2021-07-19 10:00
            */
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Name = "Mary",
                    RoleId = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b")
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.Parse("44fe7cef-31d0-41ea-a113-41d0de131f97"),
                    Name = "John",
                    RoleId = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee")
                }
            );

            modelBuilder.Entity<Slot>().HasData(
               new Slot
               {
                   SlotId = Guid.Parse("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
                   DateStart = DateTime.Parse("2021-07-19 09:00"),
                   DateEnd = DateTime.Parse("2021-07-19 10:00"),
                   UserId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35")
               },
               new Slot
               {
                   SlotId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                   DateStart = DateTime.Parse("2021-07-19 10:00"),
                   DateEnd = DateTime.Parse("2021-07-19 11:00"),
                   UserId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35")
               }
            );
            modelBuilder.Entity<Slot>().HasData(
                new Slot
                {
                    SlotId = Guid.Parse("52c5b675-319f-4302-8076-1fd5d8eb9ccc"),
                    DateStart = DateTime.Parse("2021-07-19 09:00"),
                    DateEnd = DateTime.Parse("2021-07-19 10:00"),
                    UserId = Guid.Parse("44fe7cef-31d0-41ea-a113-41d0de131f97")
                },
                new Slot
                {
                    SlotId = Guid.Parse("ad1afa51-0514-427b-a53a-790b28b0168f"),
                    DateStart = DateTime.Parse("2021-07-20 09:00"),
                    DateEnd = DateTime.Parse("2021-07-20 10:00"),
                    UserId = Guid.Parse("44fe7cef-31d0-41ea-a113-41d0de131f97")
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
