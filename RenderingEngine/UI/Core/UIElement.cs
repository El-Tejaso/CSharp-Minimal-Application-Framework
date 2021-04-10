﻿using RenderingEngine.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using RenderingEngine.Datatypes;
using RenderingEngine.Rendering;
using System.Drawing;

namespace RenderingEngine.UI.Core
{
    public class UIElement
    {
        protected List<UIComponent> _components = new List<UIComponent>();
        protected List<UIElement> _children = new List<UIElement>();
        protected UIRectTransform _rectTransform = new UIRectTransform();

        /// <summary>
        /// Should not be used over the other wrapped getters/setters if possible.
        /// mainly for components to make some modification for example after a Resize()
        /// </summary>
        public UIRectTransform RectTransform { get { return _rectTransform; } }

        public Rect2D Rect {
            get { return _rectTransform.Rect; }
        }

        public Rect2D RectOffset {
            get { return _rectTransform.AbsoluteOffset; }
        }

        public Rect2D Anchoring {
            get { return _rectTransform.NormalizedAnchoring; }
        }

        public UIElement this[int index] {
            get {
                return _children[index];
            }
        }

        public int Count {
            get {
                return _children.Count;
            }
        }

        public UIElement AddChildren(params UIElement[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                AddChild(elements[i]);
            }
            return this;
        }

        public UIElement AddChild(UIElement element)
        {
            _children.Add(element);
            element.Parent = this;
            element.Resize();
            return this;
        }

        public void RemoveChild(UIElement element)
        {
            if (_children.Contains(element))
            {
                _children.Remove(element);
                element.Parent = null;
            }
        }

        public void RemoveChild(int index)
        {
            if (index >= 0 && index < _children.Count)
            {
                _children[index].Parent = null;
                _children.RemoveAt(index);
            }
        }


        private int ComponentOfTypeIndex(Type t)
        {
            int index = -1;
            for (int i = 0; i < _components.Count; i++)
            {
                Type ti = _components[i].GetType();
                if (ti.IsSubclassOf(t) || ti == t)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }


        public T GetComponentOfType<T>() where T : UIComponent
        {
            int i = ComponentOfTypeIndex(typeof(T));

            if (i != -1)
                return (T)_components[i];

            return default;
        }

        public UIElement AddComponent<T>(T comp) where T : UIComponent
        {
            int i = ComponentOfTypeIndex(comp.GetType());

            //var t = comp.GetType();
            //var t2 = typeof(T);

            if (i == -1)
            {
                _components.Add(comp);
                comp.SetParent(this);
                return this;
            }

            return null;
        }


        protected UIElement _parent = null;
        public UIElement Parent {
            get {
                return _parent;
            }
            set {
                _parent = value;
                _dirty = true;
            }
        }

        protected bool _isVisible = true;
        public bool IsVisible { get => _isVisible; set => _isVisible = value; }


        protected bool _dirty = true;


        public UIElement()
        {
            SetNormalizedAnchoring(new Rect2D(0, 0, 1, 1));
            SetAbsoluteOffset(new Rect2D(0, 0, 0, 0));
        }


        public UIElement SetAbsOffsetsX(float left, float right)
        {
            _rectTransform.SetAbsOffsetsX(left, right);
            _dirty = true;
            return this;
        }

        public UIElement SetAbsOffsetsY(float bottom, float top)
        {
            _rectTransform.SetAbsOffsetsY(bottom, top);
            _dirty = true;
            return this;
        }

        public UIElement SetAbsoluteOffset(float offset)
        {
            return SetAbsoluteOffset(new Rect2D(offset, offset, offset, offset));
        }

        public UIElement SetAbsoluteOffset(Rect2D pos)
        {
            _rectTransform.SetAbsoluteOffset(pos);
            _dirty = true;
            return this;
        }

        public UIElement SetAbsPositionSizeX(float x, float width)
        {
            _rectTransform.SetAbsPositionSizeX(x, width);
            _dirty = true;
            return this;
        }

        public UIElement SetAbsPositionSizeY(float y, float height)
        {
            _rectTransform.SetAbsPositionSizeY(y, height);
            _dirty = true;
            return this;
        }


        public UIElement SetAbsPositionSize(float x, float y, float width, float height)
        {
            _rectTransform.SetAbsPositionSize(x, y, width, height);
            _dirty = true;
            return this;
        }

        public UIElement SetNormalizedAnchoringX(float left, float right)
        {
            _rectTransform.SetNormalizedAnchoringX(left, right);
            _dirty = true;
            return this;
        }

        public UIElement SetNormalizedAnchoringY(float bottom, float top)
        {
            _rectTransform.SetNormalizedAnchoringY(bottom, top);
            _dirty = true;
            return this;
        }


        public UIElement SetNormalizedAnchoring(Rect2D anchor)
        {
            _rectTransform.SetNormalizedAnchoring(anchor);
            _dirty = true;
            return this;
        }

        public UIElement SetNormalizedPositionCenterX(float x, float centreX = 0.5f)
        {
            _rectTransform.SetNormalizedPositionCenterX(x, centreX);
            _dirty = true;
            return this;
        }

        public UIElement SetNormalizedPositionCenterY(float y, float centreY = 0.5f)
        {
            _rectTransform.SetNormalizedPositionCenterY(y, centreY);
            _dirty = true;
            return this;
        }

        public UIElement SetNormalizedPositionCenter(float x, float y, float centreX = 0.5f, float centreY = 0.5f)
        {
            _rectTransform.SetNormalizedPositionCenter(x, y, centreX, centreY);
            _dirty = true;
            return this;
        }

        public UIElement SetNormalizedCenter(float centerX = 0.5f, float centerY = 0.5f)
        {
            _rectTransform.Center = new PointF(centerX, centerY);
            _dirty = true;
            return this;
        }

        public virtual void Update(double deltaTime)
        {
            if (_dirty)
            {
                _dirty = false;
                Resize();
            }

            UpdateChildren(deltaTime);

            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Update(deltaTime);
            }

            if (_parent == null)
            {
                ProcessChildEvents();
            }
        }

        public virtual void UpdateChildren(double deltaTime)
        {
            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].Update(deltaTime);
            }
        }

        public virtual void DrawIfVisible(double deltaTime)
        {
            if (!_isVisible)
                return;

            Draw(deltaTime);

            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].DrawIfVisible(deltaTime);
            }
        }

        public virtual void Draw(double deltaTime)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Draw(deltaTime);
            }
        }

        internal virtual bool ProcessChildEvents()
        {
            bool hasProcessed = false;
            for (int i = 0; i < _children.Count; i++)
            {
                // Fun fact:
                // if I write this as hasProcessed || _children[i].ProcessChildEvent(), then the second
                // half of the or statement won't execute if hasProcessed is true

                bool hasChildProcessed = _children[i].ProcessChildEvents();
                hasProcessed = hasProcessed || hasChildProcessed;
            }

            if (hasProcessed)
                return true;

            return ProcessComponentEvents();
        }

        public virtual bool ProcessComponentEvents()
        {
            bool res = false;
            for (int i = 0; i < _components.Count; i++)
            {
                bool componentRes = _components[i].ProcessEvents();
                res = res || componentRes;
            }

            return res;
        }

        public virtual void Resize()
        {
            _rectTransform.UpdateRect(GetParentRect());

            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].OnResize();
            }

            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].Resize();
            }
        }

        public virtual void SetDirty()
        {
            _dirty = true;
        }

        private Rect2D GetParentRect()
        {
            Rect2D parentRect;

            if (Parent != null)
            {
                parentRect = Parent.Rect;
            }
            else
            {
                parentRect = Window.Rect;
            }

            return parentRect;
        }
    }
}
