
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using RAP_Program_WPF;

namespace RAP_WPF.Tests
{
    internal class TestDriver
    {
        public delegate bool RunFunc();

        public class RunType
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

            public string ToString()
            {
                return "for " + name + "\n" + stack.ToString();
            }


        }

        private static List<RunType> CollectAll()
        {
            List<RunType> tests = new List<RunType>();

            PositionTests.Collect(tests);

            return tests;
        }

        public static void Run()
        {
            List<RunType> tests = CollectAll();
            int testnum = 1;

            foreach (RunType test in tests)
            {
                Console.WriteLine("Run test " + testnum);

                if (!test.Run())
                {
                    Console.WriteLine("Test Failed:" + test.ToString());

                    return;
                }

                testnum += 1;
            }
        }
    }
}
