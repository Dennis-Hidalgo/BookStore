﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Authors
{
    public class CreateAuthorDto
    {
        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        public string? ShortBio { get; set; }
    }
}
