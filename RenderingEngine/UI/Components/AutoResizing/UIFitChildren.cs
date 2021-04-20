﻿using RenderingEngine.Datatypes;
using RenderingEngine.Datatypes.Geometric;
using RenderingEngine.Logic;
using RenderingEngine.Rendering;
using RenderingEngine.UI.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderingEngine.UI.Components.AutoResizing
{
    public class UIFitChildren : UIComponent
    {
        bool _horizontal;
        bool _vertical;
        bool _debug;
        Rect2D _margin;
        public Rect2D Margin { get => _margin; set => _margin = value; }

        public UIFitChildren(bool horizontal, bool vertical, Rect2D margin, bool debug = false)
        {
            _horizontal = horizontal;
            _vertical = vertical;
            _margin = margin;
            _debug = debug;
        }

        float x0, y0, x1, y1;
        Rect2D _rect;

        //visuialize resizeing
        double timer = 0;
        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            if (!_debug)
                return;

            timer += deltaTime;

            if(timer > 1)
            {
                timer = 0;
                Tick();
            }
        }

        int childIndex = -1;

        private void Tick()
        {
            childIndex++;
            if (childIndex >= _parent.Count)
                childIndex = -1;

            if (childIndex == -1)
            {
                x0 = Window.Rect.Width;
                y0 = Window.Rect.Height;
                x1 = 0;
                y1 = 0;
                return;
            }

            ExpandRectToFitChld(childIndex);
        }

        public override void AfterDraw(double deltaTime)
        {
            if (_debug)
            {
                CTX.SetDrawColor(1, 0, 0, 0.5f);
                CTX.DrawRect(_rect);

                CTX.SetDrawColor(1, 0, 1, 0.5f);
                CTX.DrawRect(x0, y0, x1, y1);
            }
        }


        private Rect2D GetWantedRect()
        {
            Rect2D wantedRect = _parent.Rect;


            if (_horizontal && _vertical)
            {
                wantedRect = new Rect2D(x0, y0, x1, y1);
            }
            else if (_horizontal)
            {
                wantedRect.X0 = x0;
                wantedRect.X1 = x1;
            }
            else
            {
                wantedRect.Y0 = y0;
                wantedRect.Y1 = y1;
            }

            return wantedRect;
        }

        private void ExpandRectToFitChld(int i)
        {
            Rect2D rect = _parent[i].Rect;

            x0 = MathF.Min(x0, rect.X0 - _margin.X0);
            y0 = MathF.Min(y0, rect.Y0 - _margin.Y0);

            x1 = MathF.Max(x1, rect.X1 + _margin.X1);
            y1 = MathF.Max(y1, rect.Y1 + _margin.Y1);
        }

        protected override void OnRectTransformResize(UIRectTransform obj)
        {
            if (!(_horizontal || _vertical))
                return;

            _parent.UpdateRect();
            _parent.ResizeChildren();

            UpdateWantedRect();

            var wantedRect = GetWantedRect();

            if (_vertical && _horizontal)
            {
                _parent.SetAbsPositionSize(_parent.AnchoredPositionAbs.X, _parent.AnchoredPositionAbs.X,
                    wantedRect.Width, wantedRect.Height);
            }
            else if(_vertical)
            {
                _parent.SetAbsPositionSizeY(_parent.AnchoredPositionAbs.Y + _margin.Y1, wantedRect.Height);
            }
            else
            {
                _parent.SetAbsPositionSizeX(_parent.AnchoredPositionAbs.X + _margin.X1, wantedRect.Width);
            }
        }

        private void UpdateWantedRect()
        {
            x0 = _parent.Rect.X0;
            y0 = _parent.Rect.Y0;
            x1 = _parent.Rect.X1;
            y1 = _parent.Rect.Y1;

            for (int i = 0; i < _parent.Count; i++)
            {
                ExpandRectToFitChld(i);
            }
        }
    }
}