﻿using MinimalAF;
using MinimalAF.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalAF.VisualTests.Rendering
{
    class GeometryAndTextTest : Element
    {
        List<string> rain = new List<string>();
        Texture _tex;

        public override void OnStart()
        {
            Window w = GetAncestor<Window>();
            w.Size = (800, 600);
            w.Title = "Text and geometry test";

            CTX.SetClearColor(0, 0, 0, 0);

            CTX.SetCurrentFont("Consolas", 24);

            Init();
        }

        public void Init()
        {
            TextureMap.LoadTexture("placeholder", "./Res/settings_icon.png", new TextureImportSettings
            {
                Filtering = FilteringType.NearestNeighbour
            });

            _tex = TextureMap.GetTexture("placeholder");
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
                char c = (char)character;
                if (character > 126)
                    c = ' ';

                sb.Append(c);

                totalLength += CTX.GetCharWidth(c);
            }

            rain.Insert(0, sb.ToString());
            if ((rain.Count - 2) * CTX.GetCharHeight() > Height)
            {
                rain.RemoveAt(rain.Count - 1);
            }
        }

        double timer = 0;
        public override void OnUpdate()
        {
            //*
            timer += Time.DeltaTime;
            a += (float)Time.DeltaTime;
            if (timer < 0.05)
                return;
            //*/
            timer = 0;

            PushGibberish();
        }

        float a = 0;
        public override void OnRender()
        {
            CTX.SetDrawColor(0, 1, 0, 0.8f);
            CTX.SetTexture(null);

            for (int i = 0; i < rain.Count; i++)
            {
                CTX.DrawText(rain[i], Left, Bottom + Height - CTX.GetCharHeight() * i);
            }


            CTX.DrawArc(Width / 2, Height / 2, MathF.Min(Height / 2f, Width / 2f), a, MathF.PI * 2 + a, 6);

            CTX.SetTexture(_tex);
            //RenderingContext.DrawFilledArc(Width / 2, Height / 2, MathF.Min(Height / 2f, Width / 2f)/2f, a/2f, MathF.PI * 2 + a/2f, 6);

            CTX.DrawRect(Width / 2 - 50, Height / 2 - 50, Width / 2 + 50, Height / 2 + 50);

            //RenderingContext.DrawRect(100,100,Width-100, Height-100);
        }
    }
}
