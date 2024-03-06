using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryConsoleApp.Model
{
    public class Draw
    {
        [Key]
        public int Id { get; set; }
        public string Numbers { get; set; }
        public DateTime DrawDateTime { get; set; }

        //this constructor will be used from EF to add a new draw
        public Draw(DateTime drawDateTime, string numbers)
        {
            DrawDateTime = drawDateTime;
            Numbers = numbers;
        }

        //this constructor will be used within the program to store new draw inside OldDraws
        public Draw(int id, DateTime drawDateTime, string numbers)
        {
            Id = id;
            DrawDateTime = drawDateTime;
            Numbers = numbers;
        }
    }
}
