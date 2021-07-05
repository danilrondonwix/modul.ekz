using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modul.ekz
{
    public class modulekz
    {
        string k = "";
        /// <summary>
        /// Рабочий метод по построению путей и подсчета длины.
        /// </summary>
        public void Work()
        {
            List<Str> LPath;//лист путей
            List<Str> StQ = Input();//лист исходных данных 
            LPath = StQ.FindAll(x => x.tochka1 == StQ[MinimalElem(StQ)].tochka1);//запись точки начала в лист путей
            List<List<Str>> LPathFunc = new List<List<Str>>();//лист путей и функций
            foreach (Str rb in LPath)//построение путей из начальных возможных перемещений
            {
                CreatePath(StQ, rb);//Построение пути
                LPathFunc.Add(Vetvi(StQ, k));//Построение ветвей
                k = "";
            }
            OutputLog(LPathFunc);//Для записи всех путей в лог
            int max = LPathFunc[0][0].dlina, maxind = 0;
            for (int i = 0; i < LPath.Count; i++)// подсчет стоимости путей
            {
                if (LenFunc(LPathFunc[i]) >= max)// выбор самого большого
                {
                    max = LenFunc(LPathFunc[i]);
                    maxind = i;
                }
            }
            Debug.WriteLine("Макс. " + max);
            Debug.WriteLine("№ максимума " + maxind);
            Output(LPathFunc, maxind, max);//Запись в файл решения
            Debug.Listeners.Clear();
        }
        /// <summary>
        /// Метод записи в файл решения.
        /// </summary>
        /// <param name="LPathFunc"></param>
        /// <param name="maxind"></param>
        /// <param name="max"></param>
        public void Output(List<List<Str>> LPathFunc, int maxind, int max)
        {
            using (StreamWriter sr = new StreamWriter(@"Вывод.csv", false, Encoding.Default, 10))
            {
                foreach (Str Path in LPathFunc[maxind])
                {
                    sr.Write(Path.tochka1 + " - " + Path.tochka2 + ";(" + Path.dlina + ") ");
                }
                sr.WriteLine("Длина " + max);
            }
        }
        public void OutputLog(List<List<Str>> LPathFunc)
        {
            Debug.WriteLine("Пути: ");
            for (int i = 0; i < LPathFunc.Count; i++)
            {
                foreach (Str path in LPathFunc[i])
                {
                    Debug.Write(path.tochka1 + " - " + path.tochka2 + ";(" + path.dlina + ") ");
                }
                Debug.WriteLine("");
            }
        }
        /// <summary>
        /// Структура путей и стоимости перемещения
        /// </summary>
        public struct Str
        {
            public int tochka1;
            public int tochka2;
            public int dlina;
            public bool Equals(Str obj)
            {
                if (this.tochka1 == obj.tochka1 && this.tochka2 == obj.tochka2 && this.dlina == obj.dlina) return true;
                else return false;
            }
            public override string ToString()
            {
                return tochka1.ToString() + " - " + tochka2.ToString() + " " + dlina.ToString();
            }
        }
        /// <summary>
        /// Поиск начальной точки.Путем взятия самого маленького из первого столбца, которого нет во втором.
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int MinimalElem(List<Str> StQ)
        {
            int min = StQ[0].tochka1, minind = 0;
            foreach (Str Path in StQ)
            {
                if (Path.tochka1 <= min)
                {
                    min = Path.tochka1;
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
        public int MaximumElem(List<Str> StQ)
        {
            int min = StQ[0].tochka2, maxind = 0;
            foreach (Str Path in StQ)
            {
                if (Path.tochka2 >= min)
                {
                    min = Path.tochka1;
                    maxind = StQ.IndexOf(Path);
                }
            }
            return maxind;
        }
        /// <summary>
        /// Метод построения пути. Работает рекурсивно.
        /// </summary>
        /// <param name="StQ"></param>
        /// <param name="minel"></param>
        /// <returns></returns>
        public int CreatePath(List<Str> StQ, Str minel)
        {
            int Lenght = 0;
            Str MoveVar = StQ.Find(x => x.tochka1 == minel.tochka1 && x.tochka2 == minel.tochka2);//Поиск возможных вариантов передвижения
            k += MoveVar.tochka1.ToString() + "-" + MoveVar.tochka2.ToString();//Пишем передвижение
            if (MoveVar.tochka2 == StQ[MaximumElem(StQ)].tochka2)//Смотрим не в конце ли мы
            {
                k += ";";
                return MoveVar.dlina;
            }
            else
            {
                for (int i = 0; i < StQ.Count; i++)//Ищем стоимость перемещения в ту точку в которую мы пришли
                {
                    if (StQ[i].tochka1 == MoveVar.tochka2)
                    {
                        k += ",";
                        Lenght = CreatePath(StQ, StQ[i]) + MoveVar.dlina;
                    }
                }
            }
            return Lenght;
        }/// <summary>
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
                    StQ.Add(new Str { tochka1 = Convert.ToInt32(s2[0]), tochka2 = Convert.ToInt32(s2[1]), dlina = Convert.ToInt32(s1[1]) });

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
        public List<Str> Vetvi(List<Str> StQ, string s)
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
                            LBr[LBr.Count - 1].Add(StQ.Find(x => x.tochka1 == Convert.ToInt32(str3[0]) && x.tochka2 == Convert.ToInt32(str3[1])));
                        }
                    }
                }
            }
            foreach (List<Str> l in LBr)
            {
                if (l[0].tochka1 != StQ[MinimalElem(StQ)].tochka1)
                {
                    foreach (List<Str> l1 in LBr)
                    {
                        if (l1[0].tochka1 == StQ[MinimalElem(StQ)].tochka1)
                        {
                            l.InsertRange(0, l1.FindAll(x => l1.IndexOf(x) <= l1.FindIndex(y => y.tochka2 == l[0].tochka1)));
                        }
                    }
                }
            }
            int max = LBr[0][0].dlina, maxind = 0;
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
                Lenght += rb.dlina;
            }
            return Lenght;
        }
    }
}
