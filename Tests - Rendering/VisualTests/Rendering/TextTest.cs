using MinimalAF;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderingEngineVisualTests
{
	[VisualTest(
        description: @"Test the text rendering, and other functions that detect the text width and height",
        tags: "2D, text"
    )]
	class TextTest : Element
    {
        List<string> rain;

        string font;
        public TextTest(string font = "Consolas") {
            this.font = font;
        }

        public override void OnMount(Window w)
        {
            w.Size = (800, 600);
            w.Title = "Matrix rain test";

           SetClearColor(Color.White);

            SetFont("Consolas", 24);

            rain = new List<string>();
        }

        StringBuilder sb = new StringBuilder();
        Random rand = new Random();
        void PushGibberish()
        {
            sb.Clear();

            float totalLength = 0;
            while (totalLength < Width)
            {
                int character = rand.Next(0, 512);
				//int character = rand.Next(0, 144697);
				char c = (char)character;
                if (character > 126)
                    c = ' ';

                sb.Append(c);

                totalLength += GetCharWidth(c);
            }

            rain.Insert(0, sb.ToString());
            if ((rain.Count - 2) * GetCharHeight() > Height)
            {
                rain.RemoveAt(rain.Count - 1);
            }
        }


        double timer = 0;
        public override void OnUpdate()
        {
            //*
            timer += Time.DeltaTime;
            if (timer < 0.05)
                return;
            //*/
            timer = 0;

            SetFont(font, 16);

            PushGibberish();
        }

        public override void OnRender()
        {
            SetFont(font, 16);
            SetDrawColor(0, 1, 0, 0.8f);

            for (int i = 0; i < rain.Count; i++)
            {
                DrawText(rain[i], 0, Height - GetCharHeight() * i);
            }

            SetDrawColor(1, 0, 0, 1);
        }
    }
}
