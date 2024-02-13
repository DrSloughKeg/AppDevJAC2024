using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04FirstEFConsole
{
    public class Person
    {
        //[key]
        public int Id { get; set; }

        [Index] // wont help with LIKE '%jerry%' === contains("Jerry") will help with LIKE "Jerry%"
        [Required] // not null
        [StringLength(50)] //nvarchar 50
        public string Name { get; set; }

        [Required]
        [Index]
        public int Age { get; set; }

        //TODO find out how to set up a unique index

        /*
         * [NotMapped] //in memory only (e.g. computer), not in database
         * public string Comment {get;set;}
         * 
         * [EnumDataType(typeof(GenderEnum))]
         * public GenderEnum Gender { get; set; }
         * public enum GenderEnum { Male = 1, Female = 2, Other = 3, NA = 4}
         */
    }
}
