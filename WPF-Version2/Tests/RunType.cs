using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Tests
{
    public delegate bool RunFunc();

    internal class RunType
    {
        RunFunc func;
        StackTrace stack;
        string name;

        public RunType(string name, RunFunc func)
        {
            this.name = name;
            stack = new StackTrace();
            this.func = func;
        }

        public bool Run()
        {
            return func();
        }

        override public string ToString()
        {
            return "for " + name + "\n" + stack.ToString();
        }
    }
}
