﻿using MinimalAF.Logic;
using MinimalAF.VisualTests;
using MinimalAF.VisualTests.UI;
using System;

namespace RenderingEngineVisualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            EntryPoint[] tests =
            {
                new UISplittingTest(),
                new UIFitChildrenTest(),
                new UITest(),
                new UILinearArrangeNestedTest(),
                new UILinearArrangeTest(),
                new UIEdgeSnapTest(),
                new UITextInputTest(),
                new UITextNumberInputTest()
            };


            foreach (EntryPoint entryPoint in tests)
            {
                Window.RunProgram(entryPoint);
            }
        }
    }
}