using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace labs.Meshes
{
    public class Tetrahedron3D : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public Tetrahedron3D()
        {
            DrawTetrahedron(_size, _pos, _front, _left, _right, _bottom);
        }


        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;

                DrawTetrahedron(_size, _pos, _front, _left, _right, _bottom);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;

                DrawTetrahedron(_size, _pos, _front, _left, _right, _bottom);
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
                DrawTetrahedron(_size, _pos, _front, _left, _right, _bottom);
            }
        }

        private Brush _left = _defaultColor;
        public Brush Left
        {
            get => _left;
            set
            {
                _left = value;
                DrawTetrahedron(_size, _pos, _front, _left, _right, _bottom);
            }
        }

        private Brush _right = _defaultColor;
        public Brush Right
        {
            get => _right;
            set
            {
                _right = value;
                DrawTetrahedron(_size, _pos, _front, _left, _right, _bottom);
            }
        }

        private Brush _bottom = _defaultColor;
        public Brush Bottom
        {
            get => _bottom;
            set
            {
                _bottom = value;
                DrawTetrahedron(_size, _pos, _front, _left, _right, _bottom);
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
            Brush front,
            Brush left,
            Brush right,
            Brush bottom)
        {
            // Отсчёт точек от левого нижнего угла грани.


            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;


            Point3D front_left_bottom = new(-absX + pos.X, -absY + pos.Y, absZ + pos.Z);
            Point3D front_right_bottom = new(absX + pos.X, -absY + pos.Y, absZ + pos.Z);
            Point3D top = new(absX + pos.X, absY + pos.Y, absZ + pos.Z);
            Point3D back = new(absX + pos.X, -absY + pos.Y, -absZ + pos.Z);


            Model3DGroup m3dg = new();

            // 1 Передняя
            DiffuseMaterial material = new(front);
            GeometryModel3D faceFront = AddFace(
                    front_left_bottom,
                    front_right_bottom,
                    top,
                    material);
            m3dg.Children.Add(faceFront);


            // 3 Левая
            material = new(left);
            GeometryModel3D faceLeft =
                AddFace(
                    back,
                    front_left_bottom,
                    top,
                    material);
            m3dg.Children.Add(faceLeft);

            // 4 Правая
            material = new(right);
            GeometryModel3D faceRight =
                AddFace(
                    top,
                    front_right_bottom,
                    back,
                    material);
            m3dg.Children.Add(faceRight);

            // 5 Нижняя
            material = new(bottom);
            GeometryModel3D faceBottom =
                AddFace(
                    front_right_bottom,
                    front_left_bottom,
                    back,
                    material);
            m3dg.Children.Add(faceBottom);

            Content = m3dg;
        }
    }
}
