using Quartz.Impl;
using Quartz;
using Quartz.Spi;

namespace BannerService.Jobs
{
    public class JobsConfigurator
    {
        public static async Task Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            scheduler.JobFactory = new JobFactory(serviceProvider);

            IJobDetail updateJob = JobBuilder.Create<UpdateJob>().Build();
            ITrigger updateTrigger = TriggerBuilder.Create()
                .WithIdentity("EveryTenSecondsTrigger", "UpdateGroup")
                .WithCronSchedule("10 * * * * ?", x => x
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time")))
                .Build();
            await scheduler.ScheduleJob(updateJob, updateTrigger);
        }
    }
}
