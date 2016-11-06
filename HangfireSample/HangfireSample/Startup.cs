using System;
using System.Diagnostics;
using System.Text;
using Hangfire;
using HangfireSample;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace HangfireSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

            //var options = new DashboardOptions
            //{
            //    AuthorizationFilters = new IAuthorizationFilter[]
            //    {
            //        new AuthorizationFilter {Users = "admin, superuser", Roles = "advanced"},
            //        new ClaimsBasedAuthorizationFilter("name", "value")
            //    }
            //};
            app.UseHangfireDashboard();

            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(
                                    () => Debug.WriteLine("Recurring Job completed successfully!"),
                                    Cron.Minutely);
        }
    }

    public static class TextBuffer
    {
        private static readonly StringBuilder Buffer = new StringBuilder();

        public static void WriteLine(string value)
        {
            lock (Buffer)
            {
                Buffer.AppendLine(string.Format("{0} {1}", DateTime.Now, value));
            }
        }

        public new static string ToString()
        {
            lock (Buffer)
            {
                return Buffer.ToString();
            }
        }
    }
}