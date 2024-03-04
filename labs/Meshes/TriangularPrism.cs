using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace labs.Meshes
{
    internal class TriangularPrism : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public TriangularPrism()
        {
            DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
        }


        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;

                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;

                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        // Материалы граней
        private Brush _front = _defaultColor;
        public Brush Front
        {
            get => _front;
            set
            {
                _front = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _top = _defaultColor;
        public Brush Top
        {
            get => _top;
            set
            {
                _top = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _left = _defaultColor;
        public Brush Left
        {
            get => _left;
            set
            {
                _left = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _right = _defaultColor;
        public Brush Right
        {
            get => _right;
            set
            {
                _right = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _back = _defaultColor;
        public Brush Back
        {
            get => _back;
            set
            {
                _back = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _bottom = _defaultColor;
        public Brush Bottom
        {
            get => _bottom;
            set
            {
                _bottom = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }


        private static GeometryModel3D AddSquareFace(
            Point3D point1,
            Point3D point2,
            Point3D point3,
            Point3D point4,
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
                        point1
                    }
                },
                Material = material
            };

            return geometryModel3D;
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


        private void DrawCube(
            double size,
            Point3D pos,
            Brush front,
            Brush top,
            Brush left,
            Brush right,
            Brush bottom,
            Brush back)
        {
            // Отсчёт точек от левого нижнего угла грани.


            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;


            Point3D v1 = new(pos.X, pos.Y, pos.Z);
            Point3D v2 = new(pos.X, pos.Y, absZ + pos.Z);
            Point3D v3 = new(pos.X, absY + pos.Y, pos.Z);
            Point3D v4 = new(pos.X, absY + pos.Y, absZ + pos.Z);
            Point3D v5 = new(absX + pos.X, pos.Y, pos.Z);
            Point3D v6 = new(absX + pos.X, pos.Y, absZ + pos.Z);

            Model3DGroup m3dg = new();

            // 1 Передняя
            DiffuseMaterial material = new(front);
            GeometryModel3D faceFront = AddFace(
                    v1, v3, v5,
                    material);

            m3dg.Children.Add(faceFront);

            // 2 Верхняя
            material = new(top);
            GeometryModel3D faceTop =
                AddFace(
                    v6, v4, v2,
                    material);
            m3dg.Children.Add(faceTop);

            // 3 Левая
            material = new(left);
            GeometryModel3D faceLeft =
                AddSquareFace(
                    v5,
                    v6,
                    v2,
                    v1,
                    material);
            m3dg.Children.Add(faceLeft);

            // 4 Правая
            material = new(right);
            GeometryModel3D faceRight =
                AddSquareFace(
                    v1,
                    v2,
                    v4,
                    v3,
                    material);
            m3dg.Children.Add(faceRight);

            // 5 Нижняя
            material = new(bottom);
            GeometryModel3D faceBottom =
                AddSquareFace(
                    v5,
                    v3,
                    v4,
                    v6,
                    material);
            m3dg.Children.Add(faceBottom);


            Content = m3dg;
        }
    }
}
