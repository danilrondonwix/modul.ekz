using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modul.ekz
{
    public class critput
    {
        string k = "";
        /// <summary>
        /// Рабочий метод по построению путей и подсчета длины.
        /// </summary>
        public void Work()
        {
            List<Str> LPath;//лист путей
            List<Str> StQ = Input();//лист исходных данных 
            LPath = StQ.FindAll(x => x.point1 == StQ[MinElem(StQ)].point1);//запись точки начала в лист путей
            List<List<Str>> LPathFunc = new List<List<Str>>();//лист путей и функций
            foreach (Str rb in LPath)//построение путей из начальных возможных перемещений
            {
                CreatePath(StQ, rb);//Построение пути
                LPathFunc.Add(Branches(StQ, k));//Построение ветвей
                k = "";
            }
            OutputLog(LPathFunc);//Для записи всех путей в лог
            int max = LPathFunc[0][0].length, maxind = 0;
            for (int i = 0; i < LPath.Count; i++)// подсчет стоимости путей
            {
                if (LenFunc(LPathFunc[i]) >= max)// выбор самого большого
                {
                    max = LenFunc(LPathFunc[i]);
                    maxind = i;
                }
            }
            Debug.WriteLine("Максимум " + max);
            Debug.WriteLine("Номер максимума " + maxind);
            Output(LPathFunc, maxind, max);//Запись в файл решения
            Debug.Listeners.Clear();
        }
        /// <summary>
        /// Метод записи в файл решения.
        /// </summary>
        /// <param name="LPathFunc"></param>
        /// <param name="maxind"></param>
        /// <param name="max"></param>
        public void OutputLog(List<List<Str>> LPathFunc)
        {
            Debug.WriteLine("Пути: ");
            for (int i = 0; i < LPathFunc.Count; i++)
            {
                foreach (Str path in LPathFunc[i])
                {
                    Debug.Write(path.point1 + " - " + path.point2 + ";(" + path.length + ") ");
                }
                Debug.WriteLine("");
            }
        }
        public void Output(List<List<Str>> LPathFunc, int maxind, int max)
        {
            using (StreamWriter sr = new StreamWriter(@"Вывод.csv", false, Encoding.Default, 10))
            {
                foreach (Str Path in LPathFunc[maxind])
                {
                    sr.Write(Path.point1 + " - " + Path.point2 + ";(" + Path.length + ") ");
                }
                sr.WriteLine("Длина " + max);
            }
        }
        public int MinElem(List<Str> StQ)
        {
            int min = StQ[0].point1, minind = 0;
            foreach (Str Path in StQ)
            {
                if (Path.point1 <= min)
                {
                    min = Path.point1;
                    minind = StQ.IndexOf(Path);
                }
            }
            return minind;
        }
        /// <summary>
        /// Поиск конечной точки, по такому же принципу что и начальную точку.
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int MaxElem(List<Str> StQ)
        {
            int min = StQ[0].point2, maxind = 0;
            foreach (Str Path in StQ)
            {
                if (Path.point2 >= min)
                {
                    min = Path.point1;
                    maxind = StQ.IndexOf(Path);
                }
            }
            return maxind;
        }
        public struct Str
        {
            public int point1;
            public int point2;
            public int length;
            public bool Equals(Str obj)
            {
                if (this.point1 == obj.point1 && this.point2 == obj.point2 && this.length == obj.length) return true;
                else return false;
            }
            public override string ToString()
            {
                return point1.ToString() + " - " + point2.ToString() + " " + length.ToString();
            }
        }
        /// <summary>
        /// Поиск начальной точки.Путем взятия самого маленького из первого столбца, которого нет во втором.
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int CreatePath(List<Str> StQ, Str minel)
        {
            int Lenght = 0;
        Str MoveVar = StQ.Find(x => x.point1 == minel.point1 && x.point2 == minel.point2);//Поиск возможных вариантов передвижения
        k += MoveVar.point1.ToString() + "-" + MoveVar.point2.ToString();//Пишем передвижение
            if (MoveVar.point2 == StQ[MaxElem(StQ)].point2)//Смотрим не в конце ли мы
            {
                k += ";";
                return MoveVar.length;
            }
            else
            {
                for (int i = 0; i<StQ.Count; i++)//Ищем стоимость перемещения в ту точку в которую мы пришли
                {
                    if (StQ[i].point1 == MoveVar.point2)
                    {
                        k += ",";
                        Lenght = CreatePath(StQ, StQ[i]) + MoveVar.length;
                    }
                }
            }
            return Lenght;
        }
        /// <summary>
        /// Чтение из файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<Str> Input()
        {
            Debug.WriteLine("Чтение:");
            List<Str> StQ = new List<Str>();
            using (StreamReader sr = new StreamReader("Ввод.csv"))
            {
                while (sr.EndOfStream != true)
                {
                    string[] s1 = sr.ReadLine().Split(';');
                    string[] s2 = s1[0].Split('-');
                    Debug.WriteLine(s2[0] + " - " + s2[1] + "; " + s1[1]);
                    StQ.Add(new Str { point1 = Convert.ToInt32(s2[0]), point2 = Convert.ToInt32(s2[1]), length = Convert.ToInt32(s1[1]) });

                }
            }
            return StQ;
        }
        /// <summary>
        /// Построение ветвлений и доставляющий в начало первую половину пути до ветвления, подсчет стоимостей.
        /// </summary>
        /// <param name="LPathFunc"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Str> Branches(List<Str> StQ, string s)
        {
            List<List<Str>> LBr = new List<List<Str>>();
            string[] s1 = s.Split(';');
            foreach (string st1 in s1)
            {
                if (st1 != "")
                {
                    LBr.Add(new List<Str>());
                    string[] s2 = st1.Split(',');
                    foreach (string str2 in s2)
                    {
                        if (str2 != "")
                        {
                            string[] str3 = str2.Split('-');
                            LBr[LBr.Count - 1].Add(StQ.Find(x => x.point1 == Convert.ToInt32(str3[0]) && x.point2 == Convert.ToInt32(str3[1])));
                        }
                    }
                }
            }
            foreach (List<Str> l in LBr)
            {
                if (l[0].point1 != StQ[MinElem(StQ)].point1)
                {
                    foreach (List<Str> l1 in LBr)
                    {
                        if (l1[0].point1 == StQ[MinElem(StQ)].point1)
                        {
                            l.InsertRange(0, l1.FindAll(x => l1.IndexOf(x) <= l1.FindIndex(y => y.point2 == l[0].point1)));
                        }
                    }
                }
            }
            int max = LBr[0][0].length, maxind = 0;
            for (int i = 0; i < LBr.Count; i++)
            {
                if (LenFunc(LBr[i]) >= max)
                {
                    max = LenFunc(LBr[i]);
                    maxind = i;
                }
            }
            return LBr[maxind];
        }
        /// <summary>
        /// Подсчет длины пути.
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int LenFunc(List<Str> StQ)
        {
            int Lenght = 0;
            foreach (Str rb in StQ)
            {
                Lenght += rb.length;
            }
            return Lenght;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Listeners.Add(new TextWriterTraceListener(File.CreateText("Log.txt")));
            Debug.AutoFlush = true;
            Critical Cpit = new Critical();
            Cpit.Work();
        }
    }
}
