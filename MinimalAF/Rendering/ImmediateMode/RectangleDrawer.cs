namespace MinimalAF.Rendering {
    public class RectangleDrawer<V> where V : struct, IVertexUV, IVertexPosition {
        ImmediateMode2DDrawer<V> immediateModeDrawer;
        public RectangleDrawer(ImmediateMode2DDrawer<V> immediateModeDrawer) {
            this.immediateModeDrawer = immediateModeDrawer;
        }

        public void Draw(float x0, float y0, float x1, float y1, float u0 = 0, float v0 = 0, float u1 = 1, float v1 = 1) {
            immediateModeDrawer.Quad.Draw2D(
                x0, y0,
                x1, y0,
                x1, y1,
                x0, y1,
                u0, v1,
                u1, v1,
                u1, v0,
                u0, v0
            );
        }

        public void Draw(Rect rect, Rect uvs) {
            Draw(rect.X0, rect.Y0, rect.X1, rect.Y1, uvs.X0, uvs.Y0, uvs.X1, uvs.Y1);
        }

        public void Draw(Rect rect) {
            Draw(rect.X0, rect.Y0, rect.X1, rect.Y1);
        }

        public void DrawOutline(float thickness, Rect rect) {
            DrawOutline(thickness, rect.X0, rect.Y0, rect.X1, rect.Y1);
        }

        public void DrawOutline(float thickness, float x0, float y0, float x1, float y1) {
            Draw(x0 - thickness, y0 - thickness, x1, y0);
            Draw(x0, y1, x1 + thickness, y1 + thickness);

            Draw(x0 - thickness, y0, x0, y1 + thickness);
            Draw(x1, y0 - thickness, x1 + thickness, y1);
        }
    }
}
