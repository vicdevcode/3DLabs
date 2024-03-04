using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace labs.Meshes
{
    public class Icosahedron3D : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public Icosahedron3D()
        {
            DrawTetrahedron(_size, _pos, _color);
        }


        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;

                DrawTetrahedron(_size, _pos, _color);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;

                DrawTetrahedron(_size, _pos, _color);
            }
        }

        // Материалы граней
        private Brush _color = _defaultColor;
        public Brush Front
        {
            get => _color;
            set
            {
                _color = value;
                DrawTetrahedron(_size, _pos, _color);
            }
        }

        private static GeometryModel3D AddFace(
            Point3D point1,
            Point3D point2,
            Point3D point3,
            Material material)
        {
            GeometryModel3D geometryModel3D = new()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = new()
                    {
                        point1,
                        point2,
                        point3,
                        point1
                    }
                },
                Material = material
            };

            return geometryModel3D;
        }

        private void DrawTetrahedron(
            double size,
            Point3D pos,
            Brush color)
        {
            float t = ((1.0f + (float)Math.Sqrt(5.0)) / 2.0f) * 0.2f;
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;

            Point3D v1 = new(-absX + pos.X, t + pos.Y, pos.Z);
            Point3D v2 = new(absX + pos.X, t + pos.Y, pos.Z);
            Point3D v3 = new(-absX + pos.X, -t + pos.Y, pos.Z);
            Point3D v4 = new(absX + pos.X, -t + pos.Y, pos.Z);

            Point3D v5 = new(pos.X, -absY + pos.Y, t + pos.Z);
            Point3D v6 = new(pos.X, absY + pos.Y, t + pos.Z);
            Point3D v7 = new(pos.X, -absY + pos.Y, -t + pos.Z);
            Point3D v8 = new(pos.X, absY + pos.Y, -t + pos.Z);

            Point3D v9 = new(t + pos.X, pos.Y, -absZ + pos.Z);
            Point3D v10 = new(t + pos.X, pos.Y, absZ + pos.Z);
            Point3D v11 = new(-t + pos.X, pos.Y, -absZ + pos.Z);
            Point3D v12 = new(-t + pos.X, pos.Y, absZ + pos.Z);

            Model3DGroup m3dg = new();

            DiffuseMaterial material = new(color);
            GeometryModel3D f1 = AddFace(v1, v12, v6, material);
            m3dg.Children.Add(f1);

            GeometryModel3D f2 = AddFace(v1, v6, v2, material);
            m3dg.Children.Add(f2);

            GeometryModel3D f3 = AddFace(v1, v2, v8, material);
            m3dg.Children.Add(f3);

            GeometryModel3D f4 = AddFace(v1, v8, v11, material);
            m3dg.Children.Add(f4);

            GeometryModel3D f5 = AddFace(v1, v11, v12, material);
            m3dg.Children.Add(f5);

            GeometryModel3D f6 = AddFace(v2, v6, v10, material);
            m3dg.Children.Add(f6);

            GeometryModel3D f7 = AddFace(v6, v12, v5, material);
            m3dg.Children.Add(f7);

            GeometryModel3D f8 = AddFace(v12, v11, v3, material);
            m3dg.Children.Add(f8);

            GeometryModel3D f9 = AddFace(v11, v8, v7, material);
            m3dg.Children.Add(f9);

            GeometryModel3D f10 = AddFace(v8, v2, v9, material);
            m3dg.Children.Add(f10);

            GeometryModel3D f11 = AddFace(v4, v10, v5, material);
            m3dg.Children.Add(f11);

            GeometryModel3D f12 = AddFace(v4, v5, v3, material);
            m3dg.Children.Add(f12);

            GeometryModel3D f13 = AddFace(v4, v3, v7, material);
            m3dg.Children.Add(f13);

            GeometryModel3D f14 = AddFace(v4, v7, v9, material);
            m3dg.Children.Add(f14);

            GeometryModel3D f15 = AddFace(v4, v9, v10, material);
            m3dg.Children.Add(f15);

            GeometryModel3D f16 = AddFace(v5, v10, v6, material);
            m3dg.Children.Add(f16);

            GeometryModel3D f17 = AddFace(v3, v5, v12, material);
            m3dg.Children.Add(f17);

            GeometryModel3D f18 = AddFace(v7, v3, v11, material);
            m3dg.Children.Add(f18);

            GeometryModel3D f19 = AddFace(v9, v7, v8, material);
            m3dg.Children.Add(f19);

            GeometryModel3D f20 = AddFace(v10, v9, v2, material);
            m3dg.Children.Add(f20);

            Content = m3dg;
        }
    }
}
