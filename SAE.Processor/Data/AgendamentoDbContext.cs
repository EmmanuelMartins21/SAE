using Microsoft.EntityFrameworkCore;
using SAE.Processor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE.Processor.Data
{
    public class AgendamentoDbContext : DbContext
    {
        public DbSet<Agendamento> Agendamentos => Set<Agendamento>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=agendamentos.db");
    }
}
