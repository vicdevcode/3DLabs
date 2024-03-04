using System;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;

namespace labs.Meshes
{
    internal class Sphere : ModelVisual3D
    {
        private const int max_i = 60; //на сколько полос по долготе (longitudes)
        private const int max_j = 60; //на сколько полос по широте 
        private Point3D[,] position = new Point3D[max_i + 1, max_j]; //массив для точек
        private Point[,] texture = new Point[max_i + 1, max_j]; //массив для полос текстуры
        private DiffuseMaterial[] frontMaterial = new DiffuseMaterial[max_j - 1]; //массив для материалов, т.е. кисти для текстуры

        public Sphere()
        {
            GenerateImageMaterials();
            GenerateSphere(max_i, max_j);
            GenerateAllCylinders();
        }

        private void GenerateImageMaterials()
        {
            ImageBrush imageBrush;
            double flatThickness = 1.0 / (max_i - 1); // ширина полосы
            double minus = (double)(max_i);//???

            for (int i = 0; i < max_i - 1; i++)
            {

                frontMaterial[i] = new DiffuseMaterial(Brushes.Red);
            }
        }

        private void GenerateSphere(int longitudes, int latitudes)
        {
            double latitudeArcusIncrement = Math.PI / (latitudes - 1);
            double longitudeArcusIncrement = 2.0 * Math.PI / longitudes;
            for (int lat = 0; lat < latitudes; lat++)
            {
                double latitudeArcus = lat * latitudeArcusIncrement;
                double radius = Math.Sin(latitudeArcus);
                double y = Math.Cos(latitudeArcus);
                double textureY = (double)lat / (latitudes - 1);
                for (int lon = 0; lon <= longitudes; lon++)
                {
                    double longitudeArcus = lon * longitudeArcusIncrement;
                    position[lon, lat].X = radius * Math.Cos(longitudeArcus);
                    position[lon, lat].Y = y;
                    position[lon, lat].Z = -radius * Math.Sin(longitudeArcus);
                    texture[lon, lat].X = (double)lon / longitudes;
                    texture[lon, lat].Y = textureY;
                }
            }
        }

        private void GenerateAllCylinders()
        {
            Model3DGroup model3DGroup = new Model3DGroup();
            for (int lat = 0; lat < max_j - 1; lat++)//цикл по всем полосам
            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();//создаем объект GeometryModel3D для полосы
                geometryModel3D.Geometry = GenerateCylinder(lat); //заполнение треугольников полосы
                geometryModel3D.Material = frontMaterial[lat];//назначение текстурной кисти
                model3DGroup.Children.Add(geometryModel3D);
            }
            Content = model3DGroup;
        }

        private MeshGeometry3D GenerateCylinder(int lat)
        {
            MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
            for (int lon = 0; lon <= max_i; lon++)    //create a zigzaging point collection
            {
                Point3D p0 = position[lon, lat];                           //on the ceiling
                Point3D p1 = position[lon, lat + 1];                           //on the floor
                meshGeometry3D.Positions.Add(p0);                          //on the ceiling
                meshGeometry3D.Positions.Add(p1);                          //on the floor
                meshGeometry3D.Normals.Add((Vector3D)p0);                  //ceiling normal
                meshGeometry3D.Normals.Add((Vector3D)p1);                  //floor normal
                meshGeometry3D.TextureCoordinates.Add(texture[lon, lat]); //on the ceiling
                meshGeometry3D.TextureCoordinates.Add(texture[lon, lat + 1]); //on the floor
            }
            for (int lon = 1; lon < meshGeometry3D.Positions.Count - 2; lon += 2)
            {
                meshGeometry3D.TriangleIndices.Add(lon - 1); //left  upper point
                meshGeometry3D.TriangleIndices.Add(lon); //left  lower point
                meshGeometry3D.TriangleIndices.Add(lon + 1); //right upper point
                                                             //second triangle = right lower part of the rectangle
                meshGeometry3D.TriangleIndices.Add(lon + 1); //right upper point
                meshGeometry3D.TriangleIndices.Add(lon); //left  lower point
                meshGeometry3D.TriangleIndices.Add(lon + 2); //right lower point
            }
            return meshGeometry3D;
        }

    }
}
