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

        protected void SetFont(string name, int size=12) {
            CTX.Text.SetFont(name, size);
        }
        protected void SetClearColor(Color4 value) {
            CTX.SetClearColor(value);
        }

        protected float GetStringHeight(string s) {
            return CTX.Text.GetStringHeight(s);
        }

        protected float GetStringHeight(string s, int start, int end) {
            return CTX.Text.GetStringHeight(s, start, end);
        }

        protected float GetStringWidth(string s) {
            return CTX.Text.GetStringWidth(s);
        }

        protected float GetStringWidth(string s, int start, int end) {
            return CTX.Text.GetStringWidth(s, start, end);
        }

        protected float GetCharWidth(char c) {
            return CTX.Text.GetWidth(c);
        }

        protected float GetCharHeight(char c) {
            return CTX.Text.GetHeight(c);
        }

        protected float GetCharWidth() {
            return CTX.Text.GetWidth();
        }

        protected float GetCharHeight() {
            return CTX.Text.GetHeight();
        }

        protected Matrix4 GetModelMatrix() {
            return CTX.Shader.Model;
        }

        protected Matrix4 GetViewMatrix() {
            return CTX.Shader.View;
        }

        protected Matrix4 GetProjectionMatrix() {
            return CTX.Shader.Projection;
        }
    }
}
