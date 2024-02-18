using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Day05ToDos
{
    public class Todo
    {
        public Todo() { }

        public Todo(string task, int difficulty, DateTime dueDate,  StatusEnum status)
        {
            Task = task;
            Difficulty = difficulty;
            DueDate = dueDate;
            Status = status;
        }
        public int Id { get; set; }

        private string _task;

        [Required]
        [StringLength(100)]
        public string Task
        {
            get { return _task; }
            set
            {
                if (value.Length < 1 || value.Length > 100 || value.Contains(";"))
                {
                    throw new ArgumentException("Task description length must be 1-100 characters long");
                }
                _task = value;
            }
        }
        private int _difficulty;

        [Required]
        public int Difficulty
        {
            get { return _difficulty; }
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException("Difficulty must fall be 1-5");
                }
                _difficulty = value;
            }
        }

        private DateTime _dueDate;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                if (value.Year < 1900 || value.Year > 2099)
                {
                    throw new ArgumentException("Invalid year.");
                }
                _dueDate = value;
            }
        }

        public enum StatusEnum
        {
            Pending = 0,
            Done = 1,
            Delegated = 2,
        }

        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }



    }
}
