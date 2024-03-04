using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace labs.Meshes
{
    internal class Torus : ModelVisual3D
    {
        private int segments = 80;
        public Torus()
        {

            DrawTorus(_pos, 0.2, 0.1, segments, segments);

        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;

                DrawTorus(_pos, 0.2, 0.1, segments, segments);
            }
        }
        private void DrawTorus(Point3D pos, double outerRadius, double innerRadius, int torusSegments, int tubeSegments)
        {
            Model3DGroup group = new Model3DGroup();

            double phiStep = 2 * Math.PI / torusSegments;
            double thetaStep = 2 * Math.PI / tubeSegments;

            List<Point3D> vertices = new List<Point3D>();

            // Создаем вершины тора с внутренним диаметром
            for (int i = 0; i < torusSegments; i++)
            {
                double phi = i * phiStep;
                for (int j = 0; j < tubeSegments; j++)
                {
                    double theta = j * thetaStep;

                    double x = (outerRadius + innerRadius * Math.Cos(theta)) * Math.Cos(phi);
                    double y = (outerRadius + innerRadius * Math.Cos(theta)) * Math.Sin(phi);
                    double z = innerRadius * Math.Sin(theta);

                    vertices.Add(new Point3D(x + pos.X, y + pos.Y, z + pos.Z));
                }
            }

            // Создаем полигоны тора с внутренним диаметром
            for (int i = 0; i < torusSegments; i++)
            {
                for (int j = 0; j < tubeSegments; j++)
                {
                    int current = i * tubeSegments + j;
                    int next = (i + 1) % torusSegments * tubeSegments + j;

                    MeshGeometry3D mesh = new MeshGeometry3D();
                    mesh.Positions.Add(vertices[current]);
                    mesh.Positions.Add(vertices[next]);
                    mesh.Positions.Add(vertices[(next + 1) % vertices.Count]);
                    mesh.Positions.Add(vertices[(current + 1) % vertices.Count]);

                    mesh.TriangleIndices.Add(0);
                    mesh.TriangleIndices.Add(1);
                    mesh.TriangleIndices.Add(2);

                    mesh.TriangleIndices.Add(0);
                    mesh.TriangleIndices.Add(2);
                    mesh.TriangleIndices.Add(3);

                    group.Children.Add(new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Red)));
                }
            }

            Content = group;
        }
    }
}