using System;
using System.Collections.Generic;
using System.Text;

namespace practic4
{
    class DateList
    {
        public List<Button> Pages { get; set; }
        
        public DateList(List<Button> pages)
        {
            Pages = pages;
        }
    }
}
