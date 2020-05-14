using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ABSAPhoneBook
{
    public class Globals
    {
        private IConfiguration _configuration;

        public Globals(IConfiguration config)
        {
            _configuration = config;
        }

        public string Path
        {
            get
            {
                return System.IO.Directory.GetCurrentDirectory() + "\\" + _configuration.GetValue<string>("EntryFileName"); 
            } 
        }
    }
}

