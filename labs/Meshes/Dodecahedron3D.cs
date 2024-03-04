using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace labs.Meshes
{
    public class Dodecahedron3D : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public Dodecahedron3D()
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
            Point3D point4,
            Point3D point5,
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
                        point3,
                        point4,
                        point5,
                        point5,
                        point1,
                        point3,
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
            double t = ((1 + Math.Sqrt(5)) / 2);
            double absX = size;
            double absY = size;
            double absZ = size;

            Point3D v1 = new(absX + pos.X, absY + pos.Y, absZ + pos.Z);
            Point3D v2 = new(absX + pos.X, absY + pos.Y, -absZ + pos.Z);
            Point3D v3 = new(absX + pos.X, -absY + pos.Y, absZ + pos.Z);
            Point3D v4 = new(absX + pos.X, -absY + pos.Y, -absZ + pos.Z);
            Point3D v5 = new(-absX + pos.X, absY + pos.Y, absZ + pos.Z);
            Point3D v6 = new(-absX + pos.X, absY + pos.Y, -absZ + pos.Z);
            Point3D v7 = new(-absX + pos.X, -absY + pos.Y, absZ + pos.Z);
            Point3D v8 = new(-absX + pos.X, -absY + pos.Y, -absZ + pos.Z);
            // green
            Point3D v9 = new(pos.X, t * size, (absZ + pos.Z)/t);
            Point3D v10 = new(pos.X, t * size, (-absZ + pos.Z) / t);
            Point3D v11 = new(pos.X, -t * size, (absZ + pos.Z) / t);
            Point3D v12 = new(pos.X, -t * size, (-absZ + pos.Z) / t);
            // blue
            Point3D v13 = new((absX + pos.X) / t, pos.Y, t * size);
            Point3D v14 = new((absX + pos.X) / t, pos.Y, -t * size);
            Point3D v15 = new((-absX + pos.X) / t, pos.Y, t * size);
            Point3D v16 = new((-absX + pos.X) / t, pos.Y, -t * size);
            // red
            Point3D v17 = new(t * size, (absY + pos.Y) / t, pos.Z);
            Point3D v18 = new(t * size, (-absY + pos.Y) / t, pos.Z);
            Point3D v19 = new(-t * size, (absY + pos.Y) / t, pos.Z);
            Point3D v20 = new(-t * size, (-absY + pos.Y) / t, pos.Z);

            Model3DGroup m3dg = new();

            DiffuseMaterial material = new(color);
            GeometryModel3D f1 = AddFace(v18, v17, v1, v13, v3, material);
            m3dg.Children.Add(f1);

            GeometryModel3D f2 = AddFace(v9, v5, v15, v13, v1, material);
            m3dg.Children.Add(f2);

            GeometryModel3D f3 = AddFace(v11, v12, v4, v18, v3, material);
            m3dg.Children.Add(f3);

            GeometryModel3D f4 = AddFace(v3, v13, v15, v7, v11, material);
            m3dg.Children.Add(f4);

            GeometryModel3D f5 = AddFace(v18, v4, v14, v2, v17, material);
            m3dg.Children.Add(f5);

            GeometryModel3D f6 = AddFace(v14, v4, v12, v8, v16, material);
            m3dg.Children.Add(f6);

            GeometryModel3D f7 = AddFace(v8, v12, v11, v7, v20, material);
            m3dg.Children.Add(f7);

            GeometryModel3D f8 = AddFace(v7, v15, v5, v19, v20, material);
            m3dg.Children.Add(f8);

            GeometryModel3D f9 = AddFace(v5, v9, v10, v6, v19, material);
            m3dg.Children.Add(f9);

            GeometryModel3D f10 = AddFace(v2, v10, v9, v1, v17, material);
            m3dg.Children.Add(f10);

            GeometryModel3D f11 = AddFace(v2, v14, v16, v6, v10, material);
            m3dg.Children.Add(f11);

            GeometryModel3D f12 = AddFace(v16, v8, v20, v19, v6, material);
            m3dg.Children.Add(f12);

            Content = m3dg;
        }
    }
}
