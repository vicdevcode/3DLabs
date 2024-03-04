using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace labs.Meshes
{
    internal class Cone : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public Cone()
        {
            DrawCube(_height, _pos, _radius, _slices);
        }
        private double _height = 0.5;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;

                DrawCube(_height, _pos, _radius, _slices);
            }
        }
        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;

                DrawCube(_height, _pos, _radius, _slices);

            }
        }
        private double _radius = 0.5;
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;

                DrawCube(_height, _pos, _radius, _slices);

            }
        }
        private int _slices = 10;
        public double Slices
        {
            get => _slices;
            set
            {
                _slices = (int)value;

                DrawCube(_height, _pos, _radius, _slices);

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

        private void DrawCube(
            double height,
            Point3D pos,
            double radius,
            int slices
            )
        {
            Model3DGroup m3dg = new();

            List<Point3D> points = new List<Point3D>();
            points.Add(new Point3D(0, pos.Y + height, 0));

            for (int i  = 0; i < slices; i++)
            {
                double t = (2 * Math.PI * i) / slices;
                points.Add(new Point3D(radius * Math.Cos(t) + pos.X, pos.Y, radius * Math.Sin(t) + pos.Z));
            }

            DiffuseMaterial material = new(_defaultColor);

            for (int i = 1; i < slices; i++) 
            {
                m3dg.Children.Add(AddFace(
                    points[i + 1],
                    points[i],
                    points[0],
                    material));

                m3dg.Children.Add(AddFace(
                    points[i],
                    points[i + 1],
                    pos,
                    material));
            }

            m3dg.Children.Add(AddFace(
                    points[1],
                    points[slices],
                    points[0],
                    material));

            m3dg.Children.Add(AddFace(
                    points[slices],
                    points[1],
                    pos,
                    material));

            Content = m3dg;
        }
    }
}
