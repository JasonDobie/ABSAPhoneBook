using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ABSAPhoneBook.Models
{
    public class Entry
    {
        [DisplayName("Phone Book Entry Name")]
        [DisallowNull]
        public string Name { get; set; }
        [DisplayName("Phone Number")]
        [DisallowNull]
        public string PhoneNumber { get; set; }
    }
}
