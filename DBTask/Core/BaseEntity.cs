﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTask.Core
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
