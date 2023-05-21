﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GameNightSignInDTO
    {

        [Required]
        public int UserId { get; set; }
        [Required]
        public int GameNightId { get; set; }
    }
}
