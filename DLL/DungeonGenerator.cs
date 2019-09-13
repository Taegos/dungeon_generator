using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DLL
{
    public static class DungeonGenerator
    {
        public static bool[,] Generate(Settings settings)
        {

            bool[,] wallMap = new bool[400, 400];
            float wallChance = .51f;
            RandomFill(wallMap, wallChance);
            ApplyAutomata(wallMap);
            DefineAreas(wallMap, 90);
            return Crop(wallMap);
        }

        private static bool[,] Crop(bool[,] wallmap)
        {
            int minX = wallmap.GetLength(0);
            int maxX = 0;
            int minY = wallmap.GetLength(1);
            int maxY = 0;
            for (int x = 0; x < wallmap.GetLength(0); x++)
            {
                for (int y = 0; y < wallmap.GetLength(1); y++) {
                    if (!wallmap[x, y])
                    {
                        if (x < minX)
                        {
                            minX = x;
                        }
                        else if (x > maxX)
                        {
                            maxX = x;
                        }
                        if (y < minY) {
                            minY = y;
                        } 
                        else if (y > maxY) {
                            maxY = y;
                        }
                    }
                }
            }

            maxX+=2;
            maxY+=2;
            minX--;
            minY--;

            bool[,] cropped = new bool[maxX - minX, maxY - minY];
            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    cropped[x - minX, y - minY] = wallmap[x, y];
                }
            }
            return cropped;
        }

        private static void RandomFill(bool[,] wallMap, float baseWallChance)
        {
            Random random = new Random();          
            for (int x = 0; x < wallMap.GetLength(0); x++)
            {
                float xFactor = (float)x / wallMap.GetLength(0);
                double xChance = -Math.Pow(2 * xFactor - 1, 2) + 1;
                for (int y = 0; y < wallMap.GetLength(1); y++)
                {
                    if (x == 0 || y == 0 || x == wallMap.GetLength(0) - 1 || y == wallMap.GetLength(1) - 1)
                    {
                        wallMap[x, y] = true;
                    }
                    else
                    {
                        float yFactor = (float) y / wallMap.GetLength(1);
                        double yChance = -Math.Pow(2 * yFactor - 1, 2) + 1;
                        double factor = (xChance + yChance) / 2;
                        double wallChance = baseWallChance * factor;
                        if (random.NextDouble() >= wallChance)
                        {
                            wallMap[x, y] = true;
                        }
                    }
                }
            }
        }

        private static void DefineAreas(bool[,] wallMap, int minAreaSize) {
            bool[,] visited = new bool[wallMap.GetLength(0), wallMap.GetLength(1)];
            for (int x = 0; x < wallMap.GetLength(0); x++)
            {
                for (int y = 0; y < wallMap.GetLength(1); y++)
                {
                    if (!visited[x, y] && !wallMap[x, y])
                    {
                        List<Vector> area = DefineArea(wallMap, visited, new Vector(x,y));
                        Trace.WriteLine(area.Count);
                        if (area.Count < minAreaSize) {
                            FillArea(wallMap, area);
                        }
                    }
                }
            }
        }

        private static void FillArea(bool[,] wallMap, List<Vector> area)
        {
            foreach (Vector vector in area)
            {
                wallMap[vector.X, vector.Y] = true;
            }
        }

        private static List<Vector> DefineArea(bool[,] wallMap, bool[,] visited, Vector start)
        {
            Stack<Vector> points = new Stack<Vector>();
            List<Vector> area = new List<Vector>();
            points.Push(start);
            while (points.Count > 0) {
                Vector nextPoint = points.Pop();
                if (InsideMap(nextPoint, wallMap) && !visited[nextPoint.X, nextPoint.Y] && !wallMap[nextPoint.X, nextPoint.Y]) {
                    area.Add(nextPoint);
                    points.Push(new Vector(nextPoint.X - 1, nextPoint.Y));
                    points.Push(new Vector(nextPoint.X + 1, nextPoint.Y));
                    points.Push(new Vector(nextPoint.X, nextPoint.Y - 1));
                    points.Push(new Vector(nextPoint.X, nextPoint.Y + 1));
                    visited[nextPoint.X, nextPoint.Y] = true;
                }
            }
            return area;
        }

        private static bool InsideMap(Vector point, bool[,] map)
        {
            if (point.X < 0 || point.X >= map.GetLength(0) || point.Y < 0 || point.Y >= map.GetLength(1))
            {
                return false;
            }
            return true;
        }

        private static void ApplyAutomata(bool[,] wallMap)
        {
            for (int i = 0; i < 13; i++)
            {
                for (int x = 1; x < wallMap.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < wallMap.GetLength(1) - 1; y++)
                    {
                        int neighbours = GetWallNeighbours(wallMap, x, y);
                        if (neighbours > 4)
                        {
                            wallMap[x, y] = true;
                        }
                        else if (neighbours < 4)
                        {
                            wallMap[x, y] = false;
                        }
                    }
                }
            }
        }

        private static int GetWallNeighbours(bool[,] wallMap, int x, int y)
        {
            int neighbours = 0;
            if (wallMap[x - 1, y - 1])
            {
                neighbours++;
            }

            if (wallMap[x, y - 1])
            {
                neighbours++;
            }

            if (wallMap[x + 1, y - 1])
            {
                neighbours++;
            }

            if (wallMap[x - 1, y])
            {
                neighbours++;
            }

            if (wallMap[x + 1, y])
            {
                neighbours++;
            }
            if (wallMap[x - 1, y + 1])
            {
                neighbours++;
            }

            if (wallMap[x, y + 1])
            {
                neighbours++;
            }

            if (wallMap[x + 1, y + 1])
            {
                neighbours++;
            }
            return neighbours;
        }
    }
}