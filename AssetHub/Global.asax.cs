using AssetHub.DAL;
using AssetHub.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AssetHub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private class LocationUpdateJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                try
                {
                    using (var db = new AssetHubContext())
                    {
                        var x = (from a in db.Assets
                                 join l in db.Loans on a.Id equals l.AssetId
                                 where DateTime.Now >= l.TimeFrom && DateTime.Now <= l.TimeTo
                                 && l.RoomId != a.Locations.Where(al => al.TimeTo == null).Select(al => al.RoomId).FirstOrDefault()

                                 select new
                                 {
                                     AssetId = a.Id,
                                     RoomId = l.RoomId
                                 }).ToList();

                        foreach (var y in x)
                        {
                            var currentLocation = (from l in db.AssetLocations
                                                   where l.TimeTo == null && l.AssetId == y.AssetId
                                                   select l).FirstOrDefault();
                            if (currentLocation != null)
                            {
                                currentLocation.TimeTo = DateTime.Now;
                            }

                            db.AssetLocations.Add(new AssetLocation
                            {
                                AssetId = y.AssetId,
                                RoomId = y.RoomId,
                                TimeFrom = DateTime.Now,
                                TimeTo = null,
                            });
                        }

                        db.SaveChanges();

                    }
                }
                catch (Exception e)
                {
                    ;
                }
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            LaunchScheduler();
        }

        private void LaunchScheduler()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<LocationUpdateJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(3)
                  .RepeatForever())
              .Build();

            sched.ScheduleJob(job, trigger);
        }
    }


}
