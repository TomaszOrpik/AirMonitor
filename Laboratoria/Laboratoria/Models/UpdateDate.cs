using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Laboratoria.Models
{
    class UpdateDate
    {
        public UpdateDate()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
