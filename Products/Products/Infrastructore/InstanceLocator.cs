using Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Infrastructore
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
