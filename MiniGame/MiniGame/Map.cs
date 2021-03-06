﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Map : GameVisibleEntity
    {

        private int[,] logicMap;
        Sprite2D[,] textureMap;

        int _rows;
        int _cols;

        protected float _left;
        protected float _top;

        protected float _height;//rows * height 1 phan tu
        protected float _width;//col * width 1 phan tu

        protected Vector2 entrance = Vector2.Zero;
        protected Vector2 exit = Vector2.Zero;

        private List<Vector2> exitWay =null;

        public float Height
        {
            get
            {
                return _height * Rows;
            }
        }

        public float Width
        {
            get
            {
                return _width * _width;
            }
        }

        public int Rows
        {
            get
            {
                return _rows;
            }

            set
            {
                _rows = value;
            }
        }

        public int Cols
        {
            get
            {
                return _cols;
            }

            set
            {
                _cols = value;
            }
        }

        public Map(float left, float top, string strResource, int rows, int cols)
        {
            _left = left;
            _top = top;
            Rows = rows;
            Cols = cols;

            logicMap = createMap(Rows, Cols);
            exitWay = new List<Vector2>();
            initExitWay(getEntrance());
            entrance = Vector2.Zero;
            exit = Vector2.Zero;
            _height = _width = 32;

            loadTexture(strResource);
        }

        public void loadTexture(string strResource)
        {

            textureMap = new Sprite2D[Rows, Cols];
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                {
                    if (logicMap[r, c] == 0)
                        textureMap[r, c] = new Sprite2D(_left + c * _width,_top + r * _height,Global.loadTextures("Wall"), 0.1f);
                    else
                        textureMap[r, c] = new Sprite2D(_left + c * _width,_top + r * _height, Global.loadTextures("Road"), 0.1f);
                    if (exitWay.Contains(new Vector2(c,r)))
                        textureMap[r, c].Color = Color.Red;
                }

        }

        private bool initExitWay(Vector2 start)
        {
            if (start.X == getExit().X && start.Y == getExit().Y)
                return true;
            int[] dx = { -1, 0, 0, 1 };
            int[] dy = { 0, -1, 1, 0 };
            for (int i = 0; i < 4; i++)
            {
                float x = start.X + dx[i];
                float y = start.Y + dy[i];
                if (canGo(x, y) && !exitWay.Contains(new Vector2(x,y)))
                {
                    exitWay.Add(new Vector2(x, y));
                    if (initExitWay(new Vector2(x, y)))
                        return true;
                    exitWay.RemoveAt(exitWay.Count - 1);
                }
            }
            return false;
        }
        
        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    textureMap[r, c].Update(gameTime);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //base.Draw(gameTime, spriteBatch);
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    if (IsVisible(r, c))
                        textureMap[r, c].Draw(gameTime, spriteBatch);

        }

        private bool IsVisible(int r, int c)
        {
            return true;
        }

        public Vector2 getEntrance()
        {
            if (entrance == Vector2.Zero)
            {
                for (int i = 1; i < Rows - 1; i++)
                {
                    if (logicMap[i, Cols - 1] == 1)
                    {
                        entrance.X = Cols - 1;
                        entrance.Y = i;
                        break;
                    }
                }
            }
            return entrance;
        }
        public Vector2 getExit()
        {
            if (exit == Vector2.Zero)
            {
                for (int i = 1; i < Rows - 1; i++)
                {
                    if (logicMap[i, 0] == 1)
                    {
                        exit.X = 0;
                        exit.Y = i;
                        break;
                    }
                }

            }
            return exit;
        }

        public bool canGo(float cols, float rows)
        {
            int row = (int)rows;
            int col = (int)cols;
            if (row < 0 || row >= Rows || col < 0 || col >= Cols)
                return false;
            return logicMap[row, col] == 1;
        }

        public List<Vector2> getListRoad()
        {
            List<Vector2> ret = new List<Vector2>();
            for (int i = 1; i < Rows-1; i++)
            {
                for (int j = 1; j < Cols - 1; j++)
                {
                    if (logicMap[i, j] == 1)
                        ret.Add(new Vector2(j, i));
                }
            }
            return ret;
        }

        public string convertToJson()
        {
            string json = "\"Map\":{\n"
                + "\"Rows\": " + _rows + ",\n"
                + "\"Cols\": " + _cols + ",\n"
                + "\"Matrix\":[";

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    json += logicMap[i, j] + ",";
                }
                json += "\n";
            }
            json += "]}";
            return json;
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
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    //3 la tuong nhu luc khoi tao
                    if (matrix[i, j] == 0 || matrix[i, j] > 2)
                        matrix[i, j] = 0;
                    else if (matrix[i, j] < 0 || matrix[i, j] == 2)
                        matrix[i, j] = 1;
                    //matrix[i, j] = 1;
                }
            }


            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < cols -1 ; j++)
                {
                    if (matrix[i, j] == 1 && matrix[i - 1, j] == 0 && matrix[i + 1, j] == 0 && matrix[i, j - 1] == 0 && matrix[i, j + 1] == 0)
                        matrix[i, j] = 0;
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
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
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
                    if (x >= rows || x < 0 || y >= cols || y < 0)
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
                    if (x >= rows || x < 0 || y >= cols || y < 0)
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