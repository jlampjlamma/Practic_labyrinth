using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace labyrinth
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //1. генерируем массив алгоритмом эллера для создания лабиринта
        //2. приводим массив к виду, по которому можно будет определять препядствия
        //создать класс ячейки, которая будет хранить в себе инфу о закрытых 
        //3. отрисовка
        //4. перемещение

        //медлено цифры
        //тестирвоание - 5 функций (Ден)
        //диаграммы (титов)
        //гид

        //отписание алгоритма https://habr.com/ru/post/176671/ 

        //массив лабиринта
        List<Cell[]> cells;
        Random rand = new Random((int)DateTime.Now.Ticks); //Нужен для решения случайностей 

        bool game = false;

        /// <summary>
        /// размеры поля
        /// </summary>
        int sizePole = 8;

        /// <summary>
        /// размер клетки 
        /// </summary>
        int size = 60;
        int sizeHero = 47;

        int maxNum;

        /// <summary>
        /// позиция на поле
        /// </summary>
        int X = 0, Y = 0;

        int lvl = 0;

        private void NextLevel()
        {
            lvl++;
            if (lvl == 1 && sizePole < 32)
            {
                sizePole *= 2;
                lvl = 0;
                size /= 2;
                sizeHero = size - 8 + 1;
            }
        }

        /// <summary>
        /// Обновление отрисовки
        /// </summary>
        private void DrawUpdate(int x, int y)
        {
            if (game)
            {
                bool go = false;
                Rectangle myRectangle = Rectangle.Empty;
                if (x == 1)
                {
                    //право
                    go = cells[Y][X].Right;
                    myRectangle = new Rectangle(X * size + 4, Y * size + 4, sizeHero * 2 + 4, sizeHero);
                }
                else if (x == -1)
                {
                    //лево
                    go = cells[Y][X].Left;
                    myRectangle = new Rectangle((X - 1) * size + 4, Y * size + 4, sizeHero * 2 + 7, sizeHero);
                }
                else if (y == 1)
                {
                    //низ
                    go = cells[Y][X].Down;
                    myRectangle = new Rectangle(X * size + 4, Y * size + 4, sizeHero, sizeHero * 2 + 4);
                }
                else if (y == -1)
                {
                    //верх
                    go = cells[Y][X].Top;
                    myRectangle = new Rectangle(X * size + 4, (Y - 1) * size + 4, sizeHero, sizeHero * 2 + 7);
                }
                if (go)
                {
                    Graphics g = PB_Game.CreateGraphics();
                    Brush b = new SolidBrush(Color.Black);
                    g.FillRectangle(b, myRectangle);
                    X += x;
                    Y += y;
                    DrawGamer();
                    if (X == sizePole - 1 && Y == sizePole - 1)
                    {
                        X = 0;
                        Y = 0;
                        NextLevel();
                        NewGame();
                    }
                }
            }
        }

        /// <summary>
        /// отрисовка персонажа
        /// </summary>
        private void DrawGamer()
        {
            Graphics g = PB_Game.CreateGraphics();
            Brush b = new SolidBrush(Color.Red);
            Rectangle myRectangle = new Rectangle(X * size + 4, Y * size + 4, sizeHero, sizeHero);
            g.FillRectangle(b, myRectangle);
        }

        /// <summary>
        /// создание новой игры
        /// </summary>
        private void NewGame()
        {
            game = true;
            bool test = false;
            maxNum = sizePole * 2 + 10;
            labyrint lab = new labyrint(sizePole);
            cells = lab.Generation(!test);
            DrawingLab();
            if (test)
                CreateGraf();
            DrawGamer();
        }

        private void CreateGraf()
        {
            for (int i = 0; i < sizePole; i++)
            {
                for (int j = 0; j < sizePole; j++)
                {
                    cells[i][j].Num = sizePole * sizePole;
                }
            }
            cells[sizePole - 1][sizePole - 1].Num = 0;
            Graphics g = PB_Game.CreateGraphics();
            g.DrawString(cells[sizePole - 1][sizePole - 1].Num.ToString(), new Font("Arial", 8), Brushes.Black,
                new Point(2 + size * (sizePole - 1), 2 + size * (sizePole - 1)));
            RecCreateGraf(sizePole - 1, sizePole - 1, 0);
        }



        /// <summary>
        /// вырисосвывем лабиринт
        /// </summary>
        private void DrawingLab()
        {
            Graphics g = PB_Game.CreateGraphics();
            g.Clear(Color.White);
            //создаем ручку черного цвета и толщиной в 1 (чего конкретно 1 хз, но буква f указывает что это переменная типа float)
            Pen p = new Pen(Color.Black, 1.0f);

            //сначала рисуем квадрат sizePoleхsizePole
            Point startHorizontal = new Point(0, 0);    //верхняя горизонталь
            Point stopHorizontal = new Point(0, size * sizePole); //нижняя горизонталь
            g.DrawLine(p, startHorizontal, stopHorizontal);

            startHorizontal = new Point(size * sizePole, 0);    //верхняя горизонталь
            stopHorizontal = new Point(size * sizePole, size * sizePole); //нижняя горизонталь
            g.DrawLine(p, startHorizontal, stopHorizontal);

            startHorizontal = new Point(0, 0);     //левая вертикаль
            stopHorizontal = new Point(size * sizePole, 0);    // правая вертикаль
            g.DrawLine(p, startHorizontal, stopHorizontal);

            startHorizontal = new Point(0, size * sizePole);     //левая вертикаль
            stopHorizontal = new Point(size * sizePole, size * sizePole);    // правая вертикаль
            g.DrawLine(p, startHorizontal, stopHorizontal);

            //а теперь внутренность квадрата (сам лабиринт)
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    if (!cells[i][j].Right)
                    {

                        startHorizontal = new Point(size * (j + 1), size * i);    //верхняя горизонталь
                        stopHorizontal = new Point(size * (j + 1), size * i + size); //нижняя горизонталь
                        g.DrawLine(p, startHorizontal, stopHorizontal);
                    }

                    if (!cells[i][j].Down)
                    {
                        startHorizontal = new Point(size * j, size * (i + 1));    //верхняя вертикаль
                        stopHorizontal = new Point(size * j + size, size * (i + 1)); //нижняя вертикаль
                        g.DrawLine(p, startHorizontal, stopHorizontal);
                    }
                }
            }
        }

        private void B_Top_Click(object sender, EventArgs e)
        {
            DrawUpdate(0, -1);
        }

        private void B_Right_Click(object sender, EventArgs e)
        {
            DrawUpdate(1, 0);
        }

        private void B_Buttom_Click(object sender, EventArgs e)
        {
            DrawUpdate(0, 1);
        }

        private void B_Left_Click(object sender, EventArgs e)
        {
            DrawUpdate(-1, 0);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    DrawUpdate(-1, 0);
                    break;
                case Keys.Up:
                    DrawUpdate(0, -1);
                    break;
                case Keys.Right:
                    DrawUpdate(1, 0);
                    break;
                case Keys.Down:
                    DrawUpdate(0, 1);
                    break;
                default:
                    break;
            }
            return true;
        }

        private void B_StartGame_Click(object sender, EventArgs e)
        {
            lvl = 0;
            sizePole = 8;
            size = 60;
            sizeHero = size - 8 + 1;
            X = 0;
            Y = 0;
            NewGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //порядок проверки идет снизу против часовой стрелки, т.к. большая вероятность что нам не придеться все проверять
            if (game)
            {
                if (cells[Y][X].Down && cells[Y][X].Num > cells[Y + 1][X].Num)
                {
                    DrawUpdate(0, 1);
                }
                else if (cells[Y][X].Right && cells[Y][X].Num > cells[Y][X + 1].Num)
                {
                    DrawUpdate(1, 0);
                }
                else if (cells[Y][X].Top && cells[Y][X].Num > cells[Y - 1][X].Num)
                {
                    DrawUpdate(0, -1);
                }
                else if (cells[Y][X].Left && cells[Y][X].Num > cells[Y][X - 1].Num)
                {
                    DrawUpdate(-1, 0);
                }
            }
        }

        #region testGraf

        int sizeFont = 6;
        int sizeClaer = 13;
        int pause = 100;
        Brush bw = new SolidBrush(Color.White);
        Brush br = new SolidBrush(Color.Red);

        private void RecCreateGraf(int x, int y, int num)
        {
            Graphics g = PB_Game.CreateGraphics();

            if (CheckWay(x, y, x, y - 1) && cells[x][y - 1].Num > num + 1) //влево
            {
                cells[x][y - 1].Num = num + 1;

                Rectangle myRectangle = new Rectangle((y - 1) * size + 2, x * size + 2, sizeClaer, sizeClaer);
                g.FillRectangle(br, myRectangle);
                g.DrawString(cells[x][y - 1].Num.ToString(), new Font("Arial", sizeFont), Brushes.Black, new Point(1 + size * (y - 1), 2 + size * x));
                System.Threading.Thread.Sleep(pause);

                myRectangle = new Rectangle((y - 1) * size + 2, x * size + 2, sizeClaer, sizeClaer);
                g.FillRectangle(bw, myRectangle);
                g.DrawString(cells[x][y - 1].Num.ToString(), new Font("Arial", sizeFont), Brushes.Black, new Point(1 + size * (y - 1), 2 + size * x));

                if (maxNum > num)
                    RecCreateGraf(x, y - 1, num + 1);
            }
            if (CheckWay(x, y, x - 1, y) && cells[x - 1][y].Num > num + 1) //вверх
            {
                cells[x - 1][y].Num = num + 1;

                Rectangle myRectangle = new Rectangle(y * size + 2, (x - 1) * size + 2, sizeClaer, sizeClaer);
                g.FillRectangle(br, myRectangle);
                g.DrawString(cells[x - 1][y].Num.ToString(), new Font("Arial", sizeFont), Brushes.Black, new Point(1 + size * y, 2 + size * (x - 1)));
                System.Threading.Thread.Sleep(pause);

                myRectangle = new Rectangle(y * size + 2, (x - 1) * size + 2, sizeClaer, sizeClaer);
                g.FillRectangle(bw, myRectangle);
                g.DrawString(cells[x - 1][y].Num.ToString(), new Font("Arial", sizeFont), Brushes.Black, new Point(1 + size * y, 2 + size * (x - 1)));

                if (maxNum > num)
                    RecCreateGraf(x - 1, y, num + 1);
            }
            if (CheckWay(x, y, x, y + 1) && cells[x][y + 1].Num > num + 1) //вправо
            {
                cells[x][y + 1].Num = num + 1;

                Rectangle myRectangle = new Rectangle((y + 1) * size + 2, x * size + 2, sizeClaer, sizeClaer);
                g.FillRectangle(br, myRectangle);
                g.DrawString(cells[x][y + 1].Num.ToString(), new Font("Arial", sizeFont), Brushes.Black, new Point(1 + size * (y + 1), 2 + size * x));
                System.Threading.Thread.Sleep(pause);

                myRectangle = new Rectangle((y + 1) * size + 2, x * size + 2, sizeClaer, sizeClaer);
                g.FillRectangle(bw, myRectangle);
                g.DrawString(cells[x][y + 1].Num.ToString(), new Font("Arial", sizeFont), Brushes.Black, new Point(1 + size * (y + 1), 2 + size * x));

                if (maxNum > num)
                    RecCreateGraf(x, y + 1, num + 1);
            }
            if (CheckWay(x, y, x + 1, y) && cells[x + 1][y].Num > num + 1) //вниз
            {
                cells[x + 1][y].Num = num + 1;

                Rectangle myRectangle = new Rectangle(y * size + 2, (x + 1) * size + 2, sizeClaer, sizeClaer);
                g.FillRectangle(br, myRectangle);
                g.DrawString(cells[x + 1][y].Num.ToString(), new Font("Arial", sizeFont), Brushes.Black, new Point(1 + size * y, 2 + size * (x + 1)));
                System.Threading.Thread.Sleep(pause);

                myRectangle = new Rectangle(y * size + 2, (x + 1) * size + 2, sizeClaer, sizeClaer);
                g.FillRectangle(bw, myRectangle);
                g.DrawString(cells[x + 1][y].Num.ToString(), new Font("Arial", sizeFont), Brushes.Black, new Point(1 + size * y, 2 + size * (x + 1)));

                if (maxNum > num)
                    RecCreateGraf(x + 1, y, num + 1);
            }
        }

        private bool CheckWay(int i, int j, int x, int y)
        {
            if (x >= 0 && x < sizePole && y >= 0 && y < sizePole)
            {
                int xi = x - i;
                int yj = y - j;
                if (xi == -1 && cells[i][j].Top)
                    return true;
                if (xi == 1 && cells[i][j].Down)
                    return true;
                if (yj == -1 && cells[i][j].Left)
                    return true;
                if (yj == 1 && cells[i][j].Right)
                    return true;

            }
            return false;
        }

        #endregion
    }

}
