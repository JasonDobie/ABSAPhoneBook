using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ABSAPhoneBook.Models
{
    public class PhoneBook
    {
        [DisplayName("Phone Book Name")]
        [DisallowNull]
        public string Name { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
