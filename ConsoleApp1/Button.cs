using System;
using System.Collections.Generic;
using System.Text;

namespace practic4
{
    class Button
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Button2> Buttons { get; set; }
        public Button(string name, string type, List<Button2> buttons)
        {
            Name = name;
            Type = type;
            Buttons = buttons;
        }
    }
}
