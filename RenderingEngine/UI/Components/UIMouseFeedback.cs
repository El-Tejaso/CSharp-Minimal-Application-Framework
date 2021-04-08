﻿using RenderingEngine.Datatypes;
using RenderingEngine.Logic;
using RenderingEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderingEngine.UI.Components
{
    public class UIMouseFeedback : UIComponent
    {
        public Color4 HoverColor { get; set; }
        public Color4 ClickedColor { get; set; }

        public UIMouseFeedback(Color4 hoverColor, Color4 clickedColor)
        {
            HoverColor = hoverColor;
            ClickedColor = clickedColor;
        }

        private UIMouseListener _mouseListner;
        private UIHitbox _hitbox;
        private UIRect _bgRect;

        public override void SetParent(UIElement parent)
        {
            base.SetParent(parent);

            _hitbox = _parent.GetComponentOfType<UIRectHitbox>();
            _mouseListner = _parent.GetComponentOfType<UIMouseListener>();
            _bgRect = _parent.GetComponentOfType<UIRect>();

            _mouseListner.OnMouseEnter += _mouseListner_OnMouseOver;
            _mouseListner.OnMouseOver += _mouseListner_OnMouseOver;
            _mouseListner.OnMouseLeave += _mouseListner_OnMouseLeave;
        }

        private void _mouseListner_OnMouseOver()
        {
            if (Input.MouseHeld(MouseButton.Left))
            {
                _bgRect.Color = ClickedColor;
            }
            else
            {
                _bgRect.Color = HoverColor;
            }
        }

        private void _mouseListner_OnMouseLeave()
        {
            _bgRect.Color = _bgRect.InitialColor;
        }
    }
}