﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SnakeGame.Model
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Instance.Run();
        }
    }
}
