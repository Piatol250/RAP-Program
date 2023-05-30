
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace RAP_WPF.Tests
{
    internal class TestDriver
    {
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
