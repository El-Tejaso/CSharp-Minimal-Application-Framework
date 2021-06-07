﻿using MinimalAF.UI.Core;
using MinimalAF.UI.Property;
using MinimalAF.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalAF.UI.Components.DataInput
{
    public class UITextFloatInput : UITextInput<float>
    {
        public UITextFloatInput(Property<float> property, bool shouldClear)
            : base(property.Value, false, shouldClear)
        {
            _property = property;
        }

        public override UIComponent Copy()
        {
            return new UITextFloatInput(_property.Copy(), _shouldClear);
        }

        protected override bool TryParseText(string s, out float val)
        {
            return float.TryParse(s, out val);
        }
    }
}