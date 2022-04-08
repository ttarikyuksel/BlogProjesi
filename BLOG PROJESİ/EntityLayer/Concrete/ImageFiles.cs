﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ImageFiles
    {
        [Key]
        public int ImageID { get; set; }
        [StringLength(100)]
        public string ImageName { get; set; }
        [StringLength(250)]
        public string ImagePat { get; set; }
    }
}
