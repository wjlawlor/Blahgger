﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blahgger.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
        public string HashedPassword { get; set; }
    }
}