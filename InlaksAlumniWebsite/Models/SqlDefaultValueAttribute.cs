using System;

namespace InlaksAlumniWebsite.Models
{
    internal class SqlDefaultValueAttribute : Attribute
    {
        public string DefaultValue { get; set; }
    }
}