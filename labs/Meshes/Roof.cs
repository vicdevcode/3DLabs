using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Windows;


namespace labs.Meshes
{
    internal class Roof : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public Roof()
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
                    },
                    TriangleIndices = new()
                    {
                        0, 1, 2, 2, 4, 0,
                    },
                    Normals = new()
                    {
                        new Vector3D(0, 0, 1),
                        new Vector3D(0, 0, 1),
                        new Vector3D(0, 0, 1),
                        new Vector3D(0, 0, 1),
                    },
                    TextureCoordinates = new ()
                    {
                        new Point(0, 0),
                        new Point(1, 0),
                        new Point(1, 1),
                        new Point(0, 1),
                        new Point(0, 1),
                        new Point(0, 1),
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
                    },
                    TriangleIndices = new()
                    {
                        0, 1, 2,
                    },
                    Normals = new()
                    {
                        new Vector3D(0, 0, 1),
                        new Vector3D(0, 0, 1),
                        new Vector3D(0, 0, 1),
                        new Vector3D(0, 0, 1),
                    },
                    TextureCoordinates = new()
                    {
                        new Point(0, 0),
                        new Point(2, 0),
                        new Point(1, 1),
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
            ImageBrush roof = new ImageBrush();
            roof.ImageSource = new BitmapImage(new Uri("roof.png", UriKind.Relative));
            ImageBrush wall = new ImageBrush();
            wall.ImageSource = new BitmapImage(new Uri("wall.jpg", UriKind.Relative));
            // 1 Передняя
            DiffuseMaterial material = new(wall);
            GeometryModel3D faceFront = AddFace(
                    v1, v3, v5,
                    material);

            m3dg.Children.Add(faceFront);

            // 2 Верхняя
            material = new(wall);
            GeometryModel3D faceTop =
                AddFace(
                    v6, v4, v2,
                    material);
            m3dg.Children.Add(faceTop);

            // 3 Левая
            material = new(roof);
            GeometryModel3D faceLeft =
                AddSquareFace(
                    v5,
                    v6,
                    v2,
                    v1,
                    material);
            m3dg.Children.Add(faceLeft);

            // 4 Правая
            material = new(roof);
            GeometryModel3D faceRight =
                AddSquareFace(
                    v1,
                    v2,
                    v4,
                    v3,
                    material);
            m3dg.Children.Add(faceRight);

            // 5 Нижняя
            material = new(wall);
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
