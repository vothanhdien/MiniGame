using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Map : GameVisibleEntity
    {

        private int[,] logicMap;
        Sprite2D[,] TextureMap;

        int _rows;
        int _cols;

        protected float _left;
        protected float _top;

        protected float _height;//rows * height 1 phan tu
        protected float _width;//col * width 1 phan tu
        public float Height
        {
            get
            {
                return _height * _rows;
            }
        }

        public float Width
        {
            get
            {
                return _width * _width;
            }
        }

        public Map(float left, float top, string strResource, int rows, int cols)
        {
            _left = left;
            _top = top;
            _rows = rows;
            _cols = cols;

            logicMap = createMap(_rows, _cols);

            Texture2D tmp = Global.Content.Load<Texture2D>(strResource + "00_00");
            _height = tmp.Height;
            _width = tmp.Width;

            loadTexture(strResource);
        }
        public void loadTexture(string strResource)
        {

            mapMatrix = new MySprite2D[_rows, _cols];
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _cols; c++)
                    mapMatrix[r, c] = new MySprite2D(
                        _left + c * _width,
                        _top + r * _height,
                        CreateListTexture(strResource + r.ToString("00") + "_" + c.ToString("00")));
        }

        private List<Texture2D> CreateListTexture(string str)
        {
            List<Texture2D> ret = new List<Texture2D>();
            ret.Add(Global.Content.Load<Texture2D>(str));
            return ret;
        }





        #region create logic map
        private int[,] createMap(int rows, int cols)
        {
            int[,] matrix = createNewMap(rows,cols);
            initMap(matrix);
            finishMap(matrix);
            //printMap(matrix);
            return matrix;
        }

        private void finishMap(int[,] matrix)
        {
            for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(matrix.Length); j++)
                {
                    //3 la tuong nhu luc khoi tao
                    if (matrix[i, j] == 0 || matrix[i, j] > 2)
                        matrix[i, j] = 0;
                    else if (matrix[i, j] < 0 || matrix[i, j] == 2)
                        matrix[i, j] = 1;
                }
            }
        }

        private void initMap(int[,] matrix)
        {
            Random x = new Random();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int[] entrance = new int[2];
            entrance[0] = x.Next(1, rows - 1);
            entrance[1] = cols - 1;
            int[] exit = new int[2];
            exit[0] = x.Next(1, rows - 1);
            exit[1] = 0;

            //sinh 2 loi di
            matrix[entrance[0], entrance[1]] = 4;
            matrix[exit[0], exit[1]] = 4;

            createWay(matrix, entrance, exit);
        }

        private void createWay(int[,] matrix, int[] entrance, int[] exit)
        {
            int size = (int)Math.Sqrt(matrix.Length);
            int[] currentIn = (int[])entrance.Clone();
            int[] currentOut = (int[])exit.Clone();
            int[] dx = { 0, -1, 0, 1 };
            int[] dy = { -1, 0, 1, 0 };

            bool isFinish = false;
            while (!isFinish)
            {
                //set gia tri 
                matrix[currentIn[0], currentIn[1]] = -1;
                matrix[currentOut[0], currentOut[1]] = -2;


                int minx = 0, miny = 0;
                //di chuyen tu cong vao
                for (int i = 3; i >= 0; i--)
                {
                    int x = currentIn[0] + dx[i];
                    int y = currentIn[1] + dy[i];
                    if (x >= size || x < 0 || y >= size || y < 0)
                        continue;
                    else
                    {
                        if (matrix[x, y] <= 0)
                        {
                            if (matrix[x, y] == 0)//tuong
                                continue;
                            else if (matrix[x, y] == -2)//gap lai thang tu out
                            {
                                isFinish = true;
                            }
                        }
                        else
                        {
                            if (minx == 0 || matrix[x, y] < matrix[minx, miny])
                            {
                                minx = x;
                                miny = y;
                            }
                        }
                    }
                }
                if (minx != 0)
                {
                    //matrix[minx, miny] = -1;
                    currentIn[0] = minx;
                    currentIn[1] = miny;
                }

                //tim tu duong ra
                minx = 0; miny = 0;
                for (int i = 0; i < 4; i++)
                {
                    int x = currentOut[0] + dx[i];
                    int y = currentOut[1] + dy[i];
                    if (x >= size || x < 0 || y >= size || y < 0)
                        continue;
                    else
                    {
                        if (matrix[x, y] > 0)
                        {
                            if (minx == 0 || matrix[x, y] < matrix[minx, miny])
                            {
                                minx = x;
                                miny = y;
                            }
                        }
                        else
                        {
                            if (matrix[x, y] == 0)
                                continue;
                            else if (matrix[x, y] == -1)
                            {
                                isFinish = true;
                            }
                        }
                    }
                }
                if (minx != 0)
                {
                    //tim tu out thi la -2
                    //matrix[minx, miny] = -2;
                    currentOut[0] = minx;
                    currentOut[1] = miny;
                }
                //2 thang bi be tat
                if (matrix[currentIn[0], currentIn[1]] == -1 && matrix[currentOut[0], currentOut[1]] == -2)
                {
                    //printMap(matrix);
                    initInside(matrix);
                    currentIn = (int[])entrance.Clone();
                    currentOut = (int[])exit.Clone();
                }
            }
        }

        private void printMap(int[,] matrix)
        {
            for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(matrix.Length); j++)
                {
                    if (matrix[i, j] < 0)
                        Console.Write(" O");
                    else if (matrix[i, j] == 0)
                        Console.Write(" #");
                    else if (matrix[i, j] == 4)
                        Console.Write(" H");
                    else
                        Console.Write("  ");

                }
                Console.WriteLine();
            }
        }

        private int[,] createNewMap(int rows, int cols)
        {
            int[,] res = new int[rows, cols];
            initInside(res);
            return res;
        }

        private void initInside(int[,] res)
        {
            Random x = new Random();
            int rows = res.GetLength(0);
            int cols = res.GetLength(1);
            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < cols - 1; j++)
                {
                    //sinh tu 1 -> 4(3 la tuong);
                    res[i, j] = x.Next(1, 4);
                }
            }
        }
        #endregion
    }
}