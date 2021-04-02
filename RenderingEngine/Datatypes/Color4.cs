﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RenderingEngine.Datatypes
{
    public struct Color4
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color4(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}
