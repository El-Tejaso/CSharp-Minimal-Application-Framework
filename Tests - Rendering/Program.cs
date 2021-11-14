﻿using MinimalAF;
using MinimalAF.VisualTests.Rendering;
using RenderingEngineRenderingTests.VisualTests.Rendering;

namespace RenderingEngineVisualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Element[] tests =
            {
                new GeometryAndTextTest(),
                new StencilTest(),
                new FramebufferTest(),
                new TextureTest(),
                new PolylineSelfIntersectionAlgorithmTest(),
                new PolylineSelfIntersectionAlgorithmTest(),
                new PolylineTest(),
                new KeyboardTest(),
                new Benchmark(5),
                new GeometryOutlineTest(),
                new ArcTest(),
                new TextTest(),
            };


            foreach (Element entryPoint in tests)
            {
                new Window(entryPoint).Run();
            }
        }
    }
}
