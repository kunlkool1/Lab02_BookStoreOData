﻿using System.ComponentModel;

namespace ODataBookStoreWebClient.Models
{
    public class Press
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
