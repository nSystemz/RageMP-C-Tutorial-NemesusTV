using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorial.Models
{
    class Stats
    {
        public string name { get; set; }
        public int level { get; set; }
        public long money { get; set; }
        public int payday { get; set; }

        public Stats(string name, int level, int money, int payday)
        {
            this.name = name;
            this.level = level;
            this.money = money;
            this.payday = payday;
        }

    }
}
