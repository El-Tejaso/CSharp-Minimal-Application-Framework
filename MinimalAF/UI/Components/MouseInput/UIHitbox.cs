﻿using MinimalAF.UI.Core;

namespace MinimalAF.UI.Components.MouseInput
{
    public abstract class UIHitbox : UIComponent
    {
        public bool PointIsInside(float x, float y)
        {
            return PointIsInsideInternal(x, y);
        }

        protected abstract bool PointIsInsideInternal(float x, float y);
    }
}