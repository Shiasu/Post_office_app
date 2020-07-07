using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostOffice_Application.Models
{
    public static class StatusAutoChanger
    {
        public static void StatusTask(Parcel parcel)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=parcelsdb;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            ApplicationContext context = new ApplicationContext(optionsBuilder.Options);

            // Создаем последовательные задачи с условно рандомным диапазоном смены статуса
            var taskOnSorting = Task.Factory.StartNew(() =>
            {
                Random random = new Random();
                int miliseconds = random.Next(15, 60) * 1000;
                Thread.Sleep(miliseconds);
                ParcelStatusChanger(parcel);
                context.Update(parcel);
                context.SaveChanges();

                var taskOnSent = Task.Factory.StartNew(() =>
                {
                    miliseconds = random.Next(15, 60) * 1000;
                    Thread.Sleep(miliseconds);
                    ParcelStatusChanger(parcel);
                    context.Update(parcel);
                    context.SaveChanges();

                    var taskOnDelivered = Task.Factory.StartNew(() =>
                    {
                        miliseconds = random.Next(15, 60) * 1000;
                        Thread.Sleep(miliseconds);
                        ParcelStatusChanger(parcel);
                        context.Update(parcel);
                        context.SaveChanges();
                        context.Dispose();
                    });
                });
            });
        }
        public static void ParcelStatusChanger(Parcel parcel)
        {
            switch (parcel.Status)
            {
                case Statuses.sorting:
                    parcel.Status = Statuses.sent;
                    break;
                case Statuses.sent:
                    parcel.Status = Statuses.delivered;
                    break;
                case Statuses.delivered:
                    parcel.Status = Statuses.received;
                    break;
            }
        }
    }
}
