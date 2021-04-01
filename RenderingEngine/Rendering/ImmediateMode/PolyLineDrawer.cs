﻿using RenderingEngine.Rendering.ImmediateMode;
using System;
using System.Collections.Generic;
using System.Text;

namespace RenderingEngine.Rendering.ImmediateMode
{
    //TODO: add support for 3D lines if needed
    class PolyLineDrawer
    {
        IGeometryOutput _geometryOutput;
        LineDrawer _lineDrawer;

        public PolyLineDrawer(LineDrawer lineDrawer, IGeometryOutput geometryOutput)
        {
            _lineDrawer = lineDrawer;
            _geometryOutput = geometryOutput;
        }


        float _thickness = 0;
        float _lastX;
        float _lastY;
        uint _lastV1;
        uint _lastV2;
        Vertex _lastV1Vert;
        Vertex _lastV2Vert;
        uint _lastV3;
        uint _lastV4;
        uint _count = 0;


        CapType _capType;

        public void BeginPolyLine(float x, float y, float thickness, CapType cap)
        {
            _thickness = thickness /= 2;
            _lastX = x;
            _lastY = y;
            _capType = cap;
            _count = 1;
            _geometryOutput.FlushIfRequired(4, 6);
        }

        public void AppendToPolyLine(float x, float y) {
            float dirX = x - _lastX;
            float dirY = y - _lastY;
            float mag = MathF.Sqrt(dirX * dirX + dirY * dirY);

            float perpX = -_thickness * dirY / mag;
            float perpY = _thickness * dirX / mag;

            if (_count == 1)
            {
                StartLineSegment(dirX, dirY, perpX, perpY);
            }
            else
            {
                ExtendLineSegment(perpX, perpY);
            }

            _lastX = x;
            _lastY = y;
            _count++;
        }

        private void StartLineSegment(float dirX, float dirY, float perpX, float perpY)
        {
            Vertex v1 = new Vertex(_lastX + perpX, _lastY + perpY, 0);
            Vertex v2 = new Vertex(_lastX - perpX, _lastY - perpY, 0);

            _lastV1 = _geometryOutput.AddVertex(v1);
            _lastV2 = _geometryOutput.AddVertex(v2);

            _lastV1Vert = v1;
            _lastV2Vert = v2;

            float startAngle = MathF.Atan2(dirX, dirY) + MathF.PI / 2;
            _lineDrawer.DrawCap(_lastX, _lastY, _thickness, _capType, startAngle);
        }

        private void ExtendLineSegment(float perpX, float perpY)
        {
            if(_geometryOutput.FlushIfRequired(4, 6))
            {
                _lastV1 = _geometryOutput.AddVertex(_lastV1Vert);
                _lastV2 = _geometryOutput.AddVertex(_lastV2Vert);
            }

            Vertex v3 = new Vertex(_lastX + perpX, _lastY + perpY, 0);
            Vertex v4 = new Vertex(_lastX - perpX, _lastY - perpY, 0);

            _lastV3 = _geometryOutput.AddVertex(v3);
            _lastV4 = _geometryOutput.AddVertex(v4);

            _geometryOutput.MakeTriangle(_lastV1, _lastV2, _lastV3);
            _geometryOutput.MakeTriangle(_lastV3, _lastV2, _lastV4);

            _lastV1 = _lastV3;
            _lastV2 = _lastV4;
            _lastV1Vert = v3;
            _lastV2Vert = v4;
        }

        public void EndPolyLine(float x, float y)
        {
            float dirX = x - _lastX;
            float dirY = y - _lastY;

            AppendToPolyLine(x, y);
            AppendToPolyLine(x + dirX, y+dirY);
            _lastX = x;
            _lastY = y;

            float startAngle = MathF.Atan2(dirX, dirY) + MathF.PI / 2;
            _lineDrawer.DrawCap(_lastX, _lastY, _thickness, _capType, startAngle + MathF.PI);
        }
    }
}
