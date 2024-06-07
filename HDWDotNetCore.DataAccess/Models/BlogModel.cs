﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HDWDotNetCore.NLayer.DataAccess.Models
{
    [Table("dotNet")]
    public class BlogModel
    {
        [Key]
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
    }
}
