using System;
using System.Collections.Generic;

namespace Bll
{
    class Domains_3<T>
    {
        public Domains_3()
        {

        }

        private List<List<List<T>>> domains_3 = new List<List<List<T>>>();

        public Domains_3(int x, int y, int z)
        {

            for (int i = 0; i < x; i++)
            {
                domains_3.Add(new List<List<T>>());
                for (int j = 0; j < y; j++)
                {
                    domains_3[i].Add(new List<T>());
                    for (int k = 0; k < z; k++)
                    {
                        domains_3[i][j].Add((T)Activator.CreateInstance(typeof(T)));
                    }
                }
            }
            //matrix = new List<List<T>>(rows);
            //matrix.ForEach(row =>);
        }

        public void Addx()
        {

            domains_3.Add(new List<List<T>>());
        }
        public void Addy(int x)
        {
            domains_3[x].Add(new List<T>());
        }


        public void AddK(T item, int x, int y)
        {

            domains_3[x][y].Add(item);
        }


        public int x { get { return domains_3.Count; } }
        //public int y { get { return domains_3[0].Count; } }
        // public int z { get { return domains_3[0].Count; } }

        //הגישה למקום במטריצה
        public List<T> this[int i, int j]
        {
            get { return domains_3[i][j]; }



        }
        public List<List<T>> this[int i]
        {
            get { return domains_3[i]; }

        }
        public T this[int x, int y, int k]
        {
            get { return domains_3[x][y][k]; }
            set { domains_3[x][y][k] = value; }
        }



    }

}


