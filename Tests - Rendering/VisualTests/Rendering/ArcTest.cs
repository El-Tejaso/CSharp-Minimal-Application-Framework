using MinimalAF;
using System;

namespace RenderingEngineVisualTests
{
    [VisualTest(
        description: @"Two hands that rotate at different speeds. This is a test of the arg dawing algorithm.",
        tags: "2D"
    )]
    class ArcTest : Element
    {
        public override void OnMount(Window w)
        {
            w.Size = (800, 600);
            w.Title = "Arc Test";

           SetClearColor(Color.RGBA(1, 1, 1, 1));
        }

        public override void OnUpdate()
        {
        }

        float a;
        float b;

        public override void OnRender()
        {
            SetDrawColor(1, 0, 0, 0.5f);

            float x0 = VW(0.5f);
            float y0 = VH(0.5f);
            float r = MathF.Min(Height, Width) * 0.45f;

            DrawArc(x0, y0, r, a, b);

            SetDrawColor(0, 0, 0, 0.5f);
            DrawHand(x0, y0, r, a);
            DrawHand(x0, y0, r, b);

            SetDrawColor(0, 0, 0, 1);
            DrawText("Angle a: " + a, 0, Height - 30);
            DrawText("Angle b: " + b, 0, Height - 50);

            a += Time.DeltaTime;
            b += Time.DeltaTime * 2f;
        }

        private void DrawHand(float x0, float y0, float r, float angle)
        {
            DrawLine(x0, y0, x0 + r * MathF.Sin(angle), y0 + r * MathF.Cos(angle), 15f, CapType.Circle);
        }
    }
}
