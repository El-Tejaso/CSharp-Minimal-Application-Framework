﻿using RenderingEngine.Logic;
using RenderingEngine.Rendering;
using RenderingEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderingEngine.UI.Components
{
    public class UIMouseListener : UIComponent
    {
        public event Action OnMouseOver;
        public event Action OnMouseEnter;
        public event Action OnMouseLeave;

        public event Action OnMousePressed;
        public event Action OnMouseReleased;
        public event Action OnMouseHeld;

        public bool IsMouseOver { get { return _isMouseOver; } }
        public bool WasMouseOver { get { return _wasMouseOver; } }

        bool _wasMouseOver = false;
        bool _isMouseOver;

        private UIHitbox _hitbox;

        public override void SetParent(UIElement parent)
        {
            base.SetParent(parent);
            _hitbox = _parent.GetComponentOfType<UIHitbox>();
        }

        public override void Update(double deltaTime)
        {
            if(_wasProcessingEvents && !_isProcessingEvents)
            {
                _isMouseOver = false;
                _wasMouseOver = false;
                OnMouseLeave?.Invoke();
            }

            _wasProcessingEvents = _isProcessingEvents;
            _isProcessingEvents = false;
        }

        bool _wasProcessingEvents = false;
        bool _isProcessingEvents;

        public override void Draw(double deltaTime)
        {
            /*
            if (_isProcessingEvents)
            {
                CTX.SetDrawColor(1, 0, 0, 1);
                CTX.DrawRect(_parent.Rect.Left + 5, _parent.Rect.Top - 5, _parent.Rect.Left + 15, _parent.Rect.Top - 15);
            }
            */
        }

        public override bool ProcessEvents()
        {
            _isProcessingEvents = true;

            _wasMouseOver = _isMouseOver;
            _isMouseOver = false;

            if (_hitbox.PointIsInside(Input.MouseX, Input.MouseY))
            {
                _isMouseOver = true;
                if (_wasMouseOver)
                {
                    OnMouseOver?.Invoke();
                }
                else
                {
                    OnMouseEnter?.Invoke();
                }

                if (Input.MouseClickedAny)
                {
                    OnMousePressed?.Invoke();
                }

                if (Input.MouseHeldAny)
                {
                    OnMouseHeld?.Invoke();
                }

                if (Input.MouseReleasedAny)
                {
                    OnMouseReleased?.Invoke();
                }
            }
            else if (_wasMouseOver)
            {
                _wasMouseOver = false;
                OnMouseLeave?.Invoke();
            }

            return _isMouseOver;
        }
    }
}