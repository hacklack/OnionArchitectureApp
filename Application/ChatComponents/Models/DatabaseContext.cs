using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace Application.ChatComponents.Models
{
    public class DatabaseContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseSqlite("Data Source=chat.db");

            options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\diljo\\source\\repos\\Chat Application\\Database\\DataBB.mdf\";Integrated Security=True;Connect Timeout=30");

        }
    }
}
