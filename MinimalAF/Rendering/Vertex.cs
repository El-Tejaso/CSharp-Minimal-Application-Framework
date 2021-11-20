﻿using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace MinimalAF.Rendering
{
	[StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vertex(float x, float y, float z)
            : this(new Vector3(x, y, z), new Vector2(x, y))
        {
        }

        public Vertex(Vector3 position, Vector2 uv)
        {
            Position = position;
            Uv = uv;
        }

        public static int SizeOf()
        {
            return 5 * sizeof(float);
        }

        public Vector3 Position;

        public Vector2 Uv;
    }
}
