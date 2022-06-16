using System;
using System.Collections.Generic;

namespace Bll
{
    public class DynamicMatrix<T>
    {
        public DynamicMatrix()
        {

        }
        private List<List<T>> matrix = new List<List<T>>();
        private List<T> arr = new List<T>();
        public DynamicMatrix(int rows, int columns)
        {

            for (int i = 0; i < rows; i++)
            {
                matrix.Add(new List<T>());
                for (int j = 0; j < columns; j++)
                {
                    matrix[i].Add((T)Activator.CreateInstance(typeof(T)));
                }
            }
            //matrix = new List<List<T>>(rows);
            //matrix.ForEach(row =>);
        }
        public DynamicMatrix(DynamicMatrix<T>mat1, DynamicMatrix<T> mat2)
        {
            mat2.AddRow();
            for (int a = 0; a < mat1.Rows; a++)
            {
                if (a > 0)
                    mat2.AddRow();
                for (int b = 0; b < mat1.Columns; b++)
                {
                    mat2.AddColumn(mat1[a, b], a);
                }

            }
        }
        public DynamicMatrix(int x, int y, int z)
        {

            for (int i = 0; i < x; i++)
            {
                matrix.Add(new List<T>());
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < z; k++)
                    {
                        matrix[i].Add((T)Activator.CreateInstance(typeof(T)));
                    }
                }
            }
            //matrix = new List<List<T>>(rows);
            //matrix.ForEach(row =>);
        }
        public void AddRow()
        {
            matrix.Add(new List<T>());
        }
        public void AddColumn(T item, int row)
        {

            matrix[row].Add(item);
        }


        public int Rows { get { return matrix.Count; } }
        public int Columns { get { return matrix[0].Count; } }
        public int columns(int row) { return matrix[row].Count; }
        //public int y { get { return domains_3[0].Count; } }
        // public int z { get { return domains_3[0].Count; } }

        //הגישה למקום במטריצה
        public T this[int i, int j]
        {
            get { return matrix[i][j]; }

            //מאפשר לעבור על ערכי המטריצה
            set { matrix[i][j] = value; }//מאפשר לעשות השמת ערך-הוספה

        }
        public List<T> this[int i]
        {
            get { return matrix[i]; }

        }




    }

}
