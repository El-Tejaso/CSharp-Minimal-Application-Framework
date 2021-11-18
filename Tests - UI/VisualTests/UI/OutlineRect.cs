﻿using MinimalAF.Datatypes;
using MinimalAF.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalAF.VisualTests.UI
{
    public class OutlineRect : Element
    {
        Color4 _col;
        float _thickness;
        public OutlineRect(Color4 col, float thickness)
        {
            _thickness = thickness;
            _col = col;
        }

        public override void OnRender()
        {
            CTX.SetDrawColor(_col);

            CTX.Rect.DrawOutline(_thickness, Rect);

            base.OnRender();
        }
    }
}
