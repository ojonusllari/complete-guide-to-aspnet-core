using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using my_books.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "First Title",
                        Description = "First Desc",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biografy",
                        CoverUrl = "https....",
                        DateAdded = DateTime.Now
                        
                    },
                    new Book()
                    {
                        Title = "Second Title",
                        Description = "Second Desc",
                        IsRead = false,
                        Genre = "Biografy",
                        CoverUrl = "https....",
                        DateAdded = DateTime.Now

                    });
                    context.SaveChanges();
                }
            }

        }
    }
}
