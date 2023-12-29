﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class RegistParameters
    {
        public int? RFS { get; set; }

        public int? AFS { get; set; }

        public int? IMU { get; set; }

        public int? AFP { get; set; }

        public int? GND { get; set; }

        public int? SCD { get; set; }

        public int? STD { get; set; }

        public int? OBC { get; set; }

        public int? MLC { get; set; }

        public int? FMC { get; set; }

        public int? TGC { get; set; }
    }
}
