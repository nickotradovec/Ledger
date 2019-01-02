using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDatabase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace Ledger {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        private string connectionString = @"Server=localhost;Port=3306;Database=ledger;Uid=testapp1;Pwd=nopassword";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ();
            services.AddTransient<AppDb> (_ => new AppDb());
            TestConnection ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseWebpackDevMiddleware (new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            } else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute (
                    name: "spa-fallback",
                    defaults : new { controller = "Home", action = "Index" });
            });
        }
        private Boolean TestConnection () {
            var conn_info = @connectionString;
            bool isConn = false;
            MySqlConnection conn = null;
            try {
                conn = new MySqlConnection (conn_info);
                conn.Open ();
                isConn = true;
            } catch (ArgumentException a_ex) {
                
                Console.WriteLine("Check the Connection String.");
                Console.WriteLine(a_ex.Message);
                Console.WriteLine(a_ex.ToString());
                
            } catch (MySqlException ex) {
                string sqlErrorMessage = "Message: " + ex.Message + "\n" +
                "Source: " + ex.Source + "\n" +
                "Number: " + ex.Number;
                Console.WriteLine(sqlErrorMessage);
                
                isConn = false;
                switch (ex.Number) {
                    //http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
                    case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        break;
                    case 0: // Access denied (Check DB name,username,password)
                        break;
                    default:
                        break;
                }
            } finally {
                conn.Close ();
                //if (String.Equals(conn.State., "Open", StringComparison.OrdinalIgnoreCase) {            
                //}
            }
            return isConn;
        }
    }
}