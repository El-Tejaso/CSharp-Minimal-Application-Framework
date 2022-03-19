﻿using MinimalAF.Rendering;
using OpenTK.Mathematics;
using System;

namespace MinimalAF {
    public partial class Element {
        protected void SetDrawColor(float r, float g, float b, float a) {
            CTX.SetDrawColor(r, g, b, a);
        }

        protected void SetDrawColor(Color4 col) {
            SetDrawColor(col.R, col.G, col.B, col.A);
        }

        protected Texture GetTexture() {
            return CTX.Texture.Get();
        }

        protected void SetTexture(Texture texture) {
            CTX.Texture.Set(texture);
        }

        protected void SetFont(string name, int size) {
            CTX.Text.SetFont(name, size);
        }

        public float GetStringHeight(string s) {
            return CTX.Text.GetStringHeight(s);
        }

        public float GetStringHeight(string s, int start, int end) {
            return CTX.Text.GetStringHeight(s, start, end);
        }

        public float GetStringWidth(string s) {
            return CTX.Text.GetStringWidth(s);
        }

        public float GetStringWidth(string s, int start, int end) {
            return CTX.Text.GetStringWidth(s, start, end);
        }

        public float GetCharWidth(char c) {
            return CTX.Text.GetWidth(c);
        }

        public float GetCharHeight(char c) {
            return CTX.Text.GetHeight(c);
        }

        public float GetCharWidth() {
            return CTX.Text.GetWidth();
        }

        public float GetCharHeight() {
            return CTX.Text.GetHeight();
        }

        public IDisposable PushMatrix(Matrix4 value) {
            return CTX.PushMatrix(CTX.MODEL_MATRIX, value);
        }

        public Matrix4 GetModelMatrix() {
            return CTX.GetMatrix(CTX.MODEL_MATRIX);
        }

        public Matrix4 GetViewMatrix() {
            return CTX.GetMatrix(CTX.VIEW_MATRIX);
        }

        public Matrix4 GeProjectionMatrix() {
            return CTX.GetMatrix(CTX.PROJECTION_MATRIX);
        }
    }
}
