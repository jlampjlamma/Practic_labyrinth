using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labyrinth
{
    public class labyrint
    {
        List<Cell[]> cells;
        Random rand = new Random((int)DateTime.Now.Ticks); //Нужен для решения случайностей 
        int[] temploid;
        int size;


        public labyrint(int size)
        {
            this.size = size;
        }


        /// <summary>
        /// Алгоритм генерации лабиринта
        /// </summary>
        /// <param name="graf">true - если создавать нумерацию для нахождения кратчайшего пути, false - если нет</param>
        /// <returns>список из массивов строк лабиринта</returns>
        public List<Cell[]> Generation(bool graf = false)
        {
            //генерируем новый лабиринт
            cells = new List<Cell[]>();
            //кофициент, на который будет каждый раз уменьшаться, чтобы к последней клетке он был практически 0
            int k = 100 / size;
            for (int j = 0; j < size; j++)
            {
                GenerSets();
            }
            //убираем нижние части
            temploid = new int[size];
            for (int i = 0; i < size - 1; i++)
            {
                temploid[i] = cells[cells.Count - 1][i].Index;
                if (cells[cells.Count - 1][i].Index != cells[cells.Count - 1][i + 1].Index)
                {
                    cells[cells.Count - 1][i].Right = true;
                }
            }
            temploid[size - 1] = cells[cells.Count - 1][size - 1].Index;

            //пересматриваем возможность перемещения в стороны
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    if (j > 0)
                    {
                        cells[i][j].Left = cells[i][j - 1].Right;
                    }
                    if (i > 0)
                    {
                        cells[i][j].Top = cells[i - 1][j].Down;
                    }
                }
            }
            //перекрываем нижюю строку снизу
            for (int i = 0; i < size; i++)
            {
                cells[size - 1][i].Down = false;
            }
            if (graf)
                CreateGraf();
            return cells;
        }

        //основано на том, что мы начиная от конечной точки даем ячейкам номера, на сколько клеток отдалена эта ячейка до конечной
        private void CreateGraf()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cells[i][j].Num = size * size;
                }
            }
            cells[size - 1][size - 1].Num = 0;
            RecCreateGraf(size - 1, size - 1, 0);
        }

        private void RecCreateGraf(int x, int y, int num)
        {
            if (CheckWay(x, y, x - 1, y) && cells[x - 1][y].Num > num) //вверх
            {
                cells[x - 1][y].Num = num + 1;
                RecCreateGraf(x - 1, y, num + 1);
            }
            if (CheckWay(x, y, x + 1, y) && cells[x + 1][y].Num > num) //вниз
            {
                cells[x + 1][y].Num = num + 1;
                RecCreateGraf(x + 1, y, num + 1);
            }
            if (CheckWay(x, y, x, y - 1) && cells[x][y - 1].Num > num) //влево
            {
                cells[x][y - 1].Num = num + 1;
                RecCreateGraf(x, y - 1, num + 1);
            }
            if (CheckWay(x, y, x, y + 1) && cells[x][y + 1].Num > num) //вправо
            {
                cells[x][y + 1].Num = num + 1;
                RecCreateGraf(x, y + 1, num + 1);
            }
        }

        private bool CheckWay(int i, int j, int x, int y)
        {
            if (x >= 0 && x < size && y >= 0 && y < size)
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

        /// <summary>
        /// функция генерации строки
        /// </summary>
        /// <returns>сгенерированная строка</returns>
        private void GenerSets()
        {
            Cell[] sets = new Cell[size];
            //если была ранее создана строка, мы должны ее продублировать
            if (cells.Count > 0)
            {
                CopyRow(ref sets);
            }
            else //иначе создаем полностью новое множество
            {
                for (int i = 0; i < size; i++)
                {
                    sets[i] = new Cell(i + 1);
                }
            }

            //теперь нужно объединить множества за счет рандома, или поставит стенку
            GenerationNewLevel(ref sets);

            //теперь создаем стенки снизу
            GenerationWellsDown(ref sets);
            //добавляем строку в список
            cells.Add(sets);
        }

        /// <summary>
        /// генирация нижних стенок
        /// </summary>
        private void GenerationWellsDown(ref Cell[] sets)
        {
            //множество должно size0% иметь хотя бы один выход
            //генерим стенку по принципу шанс size0/количество ячеек
            for (int i = 0; i < sets.Length;)
            {
                //сначала узнаем количество ячеек в множестве
                int index = sets[i].Index;
                int count = 1;
                for (int j = i + 1; j < sets.Length; j++)
                {
                    if (sets[j].Index == index)
                    {
                        count++;
                    }
                    else
                        break;
                }

                //теперь ставим стенки
                if (count > 1)
                {
                    int checkCount = count;
                    for (int j = i; j < i + count && checkCount > 1; j++)
                    {
                        int temp = rand.Next(0, 101);
                        if (temp <= 40)
                        {
                            sets[j].Down = false;
                            checkCount--;
                        }
                    }
                }
                i += count;
            }
        }

        /// <summary>
        /// Обновление доступности направления передвижения c предыдущей строкой
        /// </summary>
        private void UpdateParentCells(ref Cell[] sets)
        {
            for (int i = 0; i < size; i++)
            {
                sets[i].Top = cells[cells.Count - 1][i].Down;
            }
        }



        /// <summary>
        /// копирует предыдущую строку с разделением на множества
        /// </summary>
        private void CopyRow(ref Cell[] sets)
        {
            for (int i = 0; i < size; i++)
            {
                //дублирование происходит с условием, что снизу нет никаких препятствий
                if (cells[cells.Count - 1][i].Down)
                {
                    sets[i] = new Cell(cells[cells.Count - 1][i].Index);
                }
                else //иначе нужно дать свой индекс
                {
                    sets[i] = new Cell(0);
                }
            }

            for (int i = 0; i < size; i++)
            {
                if (sets[i].Index == 0)
                {
                    int index = 1;
                    while (!CheckIndex(index, sets))
                    {
                        index++;
                    }
                    sets[i] = new Cell(index);
                }

            }
        }

        /// <summary>
        /// генерация новой строки
        /// </summary>
        private void GenerationNewLevel(ref Cell[] sets)
        {
            //создадим переменую, которая будет уменьшать шанс повторного объединения в одно множетсво
            int k = 100 / size;
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                int temp = rand.Next(0, 101); //size1, потому что второе значение не входит в диапазон
                //если условие выполняется, то объединяем, первое объединение происходит 50 на 50
                if (i + 1 < size && sets[i].Index == sets[i + 1].Index)
                {
                    k = 100 / size;
                    count = 0;
                }
                else if (i + 1 < size && temp <= 50 - (k * count))
                {
                    sets[i + 1].Index = sets[i].Index; //объединяем посредством присваивания индекса
                    sets[i].Right = true;
                    count++;
                }
                else //если рандом не сработал, или мы в самой крайней ячейке, тогда проход влево закрыт
                {
                    k = 100 / size;
                    count = 0;
                }
            }
        }

        /// <summary>
        /// функци проверки существования индекса множества
        /// </summary>
        private bool CheckIndex(int index, Cell[] sets)
        {
            for (int i = 0; i < sets.Length; i++)
            {
                if (sets[i].Index == index)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class Cell
    {

        public Cell(int index)
        {
            Index = index;
            Top = false;
            Down = true;
            Left = false;
            Right = false;
        }
        /// <summary>
        /// множество к которому принадлежит
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// true - если проход вверх открыт, false - закрыт
        /// </summary>
        public bool Top { get; set; }
        /// <summary>
        /// true - если проход вниз открыт, false - закрыт
        /// </summary>
        public bool Down { get; set; }
        /// <summary>
        /// true - если проход влево открыт, false - закрыт
        /// </summary>
        public bool Left { get; set; }
        /// <summary>
        /// true - если проход вправо открыт, false - закрыт
        /// </summary>
        public bool Right { get; set; }

        public int Num { get; set; }

    }
}
