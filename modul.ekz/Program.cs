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
            for (int i = 0; i<LPath.Count; i++)// подсчет стоимости путей
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
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
