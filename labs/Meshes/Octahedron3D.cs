using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace labs.Meshes
{
    public class Octahedron3D : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public Octahedron3D()
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
            // Отсчёт точек от левого нижнего угла грани.


            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;


            Point3D midlle_left = new(-absX + pos.X, pos.Y, absZ + pos.Z);
            Point3D middle_front = new(absX + pos.X, pos.Y, absZ + pos.Z);
            Point3D middle_back = new(-absX + pos.X, pos.Y, -absZ + pos.Z);
            Point3D middle_right = new(absX + pos.X, pos.Y, -absZ + pos.Z);
            Point3D _top = new(pos.X, absY * 1.5 + pos.Y, pos.Z);
            Point3D _bottom = new(pos.X, -absY * 1.5 + pos.Y, pos.Z);


            Model3DGroup m3dg = new();

            // 1 Верхняя передняя
            DiffuseMaterial material = new(color);
            GeometryModel3D faceTopFront = AddFace(
                    midlle_left,
                    middle_front,
                    _top,
                    material);
            m3dg.Children.Add(faceTopFront);


            // 2 Верхняя левая
            GeometryModel3D faceTopLeft =
                AddFace(
                    middle_back,
                    midlle_left,
                    _top,
                    material);
            m3dg.Children.Add(faceTopLeft);

            // 3 Верхняя правая
            GeometryModel3D faceTopRight =
                AddFace(
                    middle_front,
                    middle_right,
                    _top,
                    material);
            m3dg.Children.Add(faceTopRight);

            // 4 Верхняя задняя
            GeometryModel3D faceTopBack =
                AddFace(
                    middle_back,
                    middle_right,
                    _top,
                    material);
            m3dg.Children.Add(faceTopBack);

            // 5 Нижняя передняя
            GeometryModel3D faceBottomFront = AddFace(
                    middle_front,
                    midlle_left,
                    _bottom,
                    material);
            m3dg.Children.Add(faceBottomFront);


            // 6 Нижняя левая
            GeometryModel3D faceBottomLeft =
                AddFace(
                    midlle_left,
                    middle_back,
                    _bottom,
                    material);
            m3dg.Children.Add(faceBottomLeft);

            // 7 Нижняя правая
            GeometryModel3D faceBottomRight =
                AddFace(
                    middle_right,
                    middle_front,
                    _bottom,
                    material);
            m3dg.Children.Add(faceBottomRight);

            // 8 Нижняя задняя
            GeometryModel3D faceBottomBack =
                AddFace(
                    middle_back,
                    middle_right,
                    _bottom,
                    material);
            m3dg.Children.Add(faceBottomBack);

            Content = m3dg;
        }
    }
}
