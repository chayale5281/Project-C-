using System;
using System.Collections.Generic;

namespace Bll
{
    public class hungarianAlgorithem
    {
        private const string NO_PLACEMENT = "ללא שיבוץ";

        public static int[] CompleteMatrix(DynamicMatrix<int> mat)
        {
            //בודק שמספר העובדים שווה למספר הקשישים
            int lengthrow = mat.Rows;

            int lengthcolumn = mat.Columns;

            DynamicMatrix<int> p = new DynamicMatrix<int>();
            int[,] mask;
            bool[] RowCover;
            bool[] ColCover;
            string[,] arrcheckzero = new string[mat.Rows,mat.Columns];
            
            int paths_row_0 = 0, paths_cols_0 = 0;

            if (lengthcolumn > lengthrow)
            {
                for (int col = lengthrow; col < lengthcolumn; col++)
                {
                    mat.AddRow();
                    //mat.AddColumn(-1, col);
                    for (int count = 0; count < lengthcolumn; count++)
                    {

                        List<int> t = mat[col];
                        mat.AddColumn(-1, col);




                    }
                    
                }

            }
            else if (lengthcolumn < lengthrow)
            {
                for (int row = 0; row < lengthrow; row++)
                {
                    for (int count = lengthcolumn; count < lengthrow; count++)
                    {
                        mat.AddColumn(-1, row);

                        lengthcolumn++;
                    }
                }
            }
            
            RowCover = new bool[lengthcolumn];
            ColCover = new bool[lengthcolumn];
            mask = new int[mat.Rows, lengthcolumn];
            int step = 1;
            //בודקת אם הערך ההתחלתי עוד לפני השיבוץ היה שווה 0 כלומר לא היה שיבוץ ושמה במטריצה ערך "ללא שיבו"ץ
        
        

            for (int i = 0; i < mat.Rows; i++)
            {
                for (int j = 0; j < mat.Columns; j++)
                {
                    if (mat[i][j] == 0)
                        arrcheckzero[i,j] = NO_PLACEMENT;
                }
           }
        
            while (step != -1)
            {
                switch (step)
                {
                    case 1:
                        step = step1(mat, mat.Rows, lengthcolumn);
                        break;
                    case 2:
                        step = step2(mat, mat.Rows, lengthcolumn, mask, RowCover, ColCover);
                        break;
                    case 3:
                        step = step3(mask, mat.Rows, lengthcolumn, ColCover);
                        break;
                    case 4:
                        step = step4(mat, mask, RowCover, ColCover, ref paths_row_0, ref paths_cols_0, mat.Rows, lengthcolumn);
                        break;
                    case 5:
                        step = step5(p, paths_row_0, paths_cols_0, mask, RowCover, ColCover);
                        break;
                    case 6:
                        step = step6(mat, RowCover, ColCover, mat.Rows, lengthcolumn);
                        break;
                   
                    default:
                        step = -1;
                        break;

                }
            }

            var endarr1 = new int[lengthcolumn];
            var endarr = new int[lengthcolumn];
            //בודקת את השיבוץ הסופי וכן אם היה מקומות ללא שיבוץ שמה -1
            for (int i = 0; i < lengthrow; i++)
            {
                for (int j = 0; j < lengthcolumn; j++)
                {
                    if (mask[i, j] == 1)
                    {

                        endarr1[i] = j;
                        if (arrcheckzero[i, j] == NO_PLACEMENT)
                            endarr1[i] = -1;

                        break;
                    }
                }
            }
            //לולאה שעוברת על המערך הסופי ובודקת איזה קשש שובץ לעובד שהיה מחוץ לגבולת מספר העובדים כלומר לא היה שיבוץ
            //ושם שם -1
            for (int i = lengthrow; i < lengthcolumn; i++)
            {
                endarr1[i] = -1;
            }
            return endarr1;
        }
        


        // process rows
        
        static void clear_covers(bool[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = false;
        }

        private static int step1(DynamicMatrix<int> matr, int r, int c)
        {
            //מחסר מכל שורה את הערך הקטן ביותר
            for (int i = 0; i < r; i++)
            {
                var min = int.MaxValue;
                for (int j = 0; j < c; j++)
                {
                    min = Math.Min(min, matr[i, j]);
                }
                for (int j = 0; j < c; j++)
                {
                    matr[i, j] -= min;
                }
            }
            //מחסר מכל עמודה את הערך הקטן ביותר
            for (int i = 0; i < r; i++)
            {
                var min = int.MaxValue;
                for (int j = 0; j < c; j++)
                {
                    min = Math.Min(min, matr[j, i]);
                }
                for (int j = 0; j < c; j++)
                {
                    matr[j, i] -= min;
                }

            }
            return 2;
        }
        private static int step2(DynamicMatrix<int> matr, int r, int c, int[,] m, bool[] row, bool[] col)

        {



            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (matr[i, j] == 0)
                        if (!row[i] && !col[j])
                        {
                            m[i, j] = 1;
                            row[i] = true;
                            col[j] = true;
                        }
                }
            }

            clear_covers(row);
            clear_covers(col);
            return 3;
        }
        //פונקציה שבודקת האם מספר האחדים במטריצת הכיסויים שווה לגודל מספר העמודות\השורות אם כן מסיים אחרת
        //עובר לשלב 4 
        public static int step3(int[,] masck, int rows, int column, bool[] colcover)
        {

            int colcount = 0;//מונה למספר האחדים במטריצת הכיסויים ,גודל השורות\עמודות

            for (int r = 0; r < masck.GetLength(0); ++r)
            {
                for (int c = 0; c < masck.GetLength(1); c++)
                    if (masck[r, c] == 1)
                    {
                        colcover[c] = true;
                        colcount++;
                    }
                // מסמן במערך העמודות היכן יש עמודה מסומנת
            }


            if (colcount >= column)
            {
                return -1; // אם מספר העמודות המכוסות שווה למספר העמודות אם כן סיים
            }
            else
            {
                return 4;//מחזיר 4
            }
        }
        //פונקציות עזר לשלב 4
        //פוקציה זו מקבלת את המטריצה המקורית עמודה ושורה מערך שורות מערך עמודות 
        //ובודקת האם יש במטריצה המקורית אפסים שאינם מכוסים
        //אם כן משנה שורה ועמודה שקבל
        public static int Find_a_zero(ref int row, ref int col, DynamicMatrix<int> matrix, bool[] rowCover, bool[] colCover, int co, int ro)

        {
            int r = 0;
            int c = 0;

            bool done = false;
            row = -1;
            col = -1;

            while (!done)
            {
                c = 0;
                while (true)
                {
                    //בודק האם האפס הוא הראשון שלא מכוסה השורה ובעמודה שלו
                    if (matrix[r, c] == 0 && !rowCover[r] && !colCover[c])
                    {
                        row = r;
                        col = c;
                        done = true;
                    }
                    c += 1;
                    if (c >= co || done)
                        break;
                }
                r += 1;
                if (r >= ro)
                    done = true;
            }
            return row;
        }
        // בודק האם ישנו כוכב בשורה-כלומר 0 שמכוסה במטריצת המסיכה 
        private static bool Star_In_Row(int row,
     int[,] mascks, int co)
        {
            bool tmp = false;
            for (int c = 0; c < co; c++)
                if (mascks[row, c] == 1)
                    tmp = true;

            return tmp;
        }

        //מוצא מיקום כוכב בשורה
        static void find_star_in_row(int row,
          ref int co, int lenco, int[,] masck)
        {
            co = -1;
            for (int c = 0; c < lenco; c++)
                if (masck[row, c] == 1)
                    co = c;

        }

        //פונקציה זו תביא לשיבוץ הסופי
        public static int step4(DynamicMatrix<int> matrix,
        int[,] masck,
        bool[] rowCover,
        bool[] colCover,
         ref int path_row_0,
        ref int path_col_0, int r, int c)

        {
            int row = -1;
            int col = -1;
            bool done = false;
            while (!done)
            {
               //מוצא האם יש אפסים לא מכוסים שליחה לפונקציה שמוצאת אפסים לא מכוסים ומשנה לשורה ולעמודה הזאת
                Find_a_zero(ref row, ref col, matrix, rowCover, colCover, r, c);
                // אם אין שיבוץ במטריצה המקורית -מספר הקווים לכיסוי האפסים קטן מגודל המטריצה
                if (row == -1)
                {
                    done = true;
                    return 6;
                }
                else
                {
                    //אם נמצאו אפסים ראשוניים לא מכוסיים 
                    //האפסים מכוסים במטריצת הכיסויים ב-2
                    masck[row, col] = 2;
                    //אם יש באותה שורה עוד אפס מסומן מסמן את השורה (כלומר שורה מסומנת(
                    if (Star_In_Row(row, masck, c) == true)
                    {
                        //מוצא מיקום במטריצה שם 1 בשורה ואפס בעמודה
                        //אם יש באותה שורה עוד אפס מסומן מסמן את השורה (כלומר שורה מסומנת(
                        //מסמן עמודות ושורות שיש בהם אפסים
                        find_star_in_row(row, ref col, c, masck);
                        rowCover[row] = true;//מסמן שורה
                        colCover[col] = false;//משחרר את העמודה שיש שם אחד
                    }
                    else
                    //אם האפס הוא ראשון בשורה שלא מסומן
                    //וכן במטריצה מצאנו שורה לא מסומנת 
                    {
                        done = true;
                        //שולח את האיבר ששורתו אין לה שיבוץ
                        path_row_0 = row;
                        path_col_0 = col;
                        return 5;
                    }

                }
            }
            return 0;

        }
        // הפונקציות הבאות בעבור הצעד החמישי 
        // מוצאת האם ישנו 0 מסומן בעמודה ומחזיר את השורה
        public static void find_star_in_col(int c,
            int r, ref int row,
        int[,] masck)
        {
            row = -1;
            for (int i = 0; i < r; i++)
                if (masck[i, c] == 1)
                    row = i;

        }
        //מוצא האם באותה שורה שיש 0 מסומן יש גם 0 לא מסומן שאותו סיימנו במטריצת הכיסויים ב-2 ומחזיר את העמודה
        public static int find_prime_in_row(int r,
           ref int c, int row,
            int[,] masck)
        {
            for (int j = 0; j < row; j++)
                if (masck[r, j] == 2)
                    c = j;
            return c;
        }
        //לאחר שמצאנו פתרון מעדכן במטריצת הכיסווים איפה שיש 1 שם 0 ואיפה שיש 2 שם 1
        static void Update_after_constructore_path(DynamicMatrix<int> ps,
            int path_count,
            int[,] masck)
        {
            for (int p = 0; p < path_count; p++)
                if (masck[ps[p, 0], ps[p, 1]] == 1)
                    masck[ps[p, 0], ps[p, 1]] = 0;
                else
                    masck[ps[p, 0], ps[p, 1]] = 1;
        }
        //אם היה 2 במטריצת הכיסויים שלא בשתמשנו בו במטריצת הבניה נאפס אותו
        static void erase_primes_not_in_pathcostructore(int[,] masck)
        {
            for (int i = 0; i < masck.GetLength(0); i++)
            {
                for (int j = 0; j < masck.GetLength(1); j++)
                {
                    if (masck[i, j] == 2)
                        masck[i, j] = 0;
                }
            }
        }


        //בונה מטריצה שעוברת  חלקית על האפסים הראשונים הלא מכוסים-כלומר רק אם  
        //יש צורך באפסים אלו להחלפת אפסים ראשוניים באפסיים ממסומנים ולאחר מכן מעדכן
        //את מטריצת הכיסוים היכן שיש 2 במטריצה הבנויה שם 1 ואיפה שאחד שם 0

        public static int step5(DynamicMatrix<int> p,
        int path_row_0,
        int path_col_0,
        int[,] masck,
        bool[] RowCover,
        bool[] ColCover
        )
        {
            p = new DynamicMatrix<int>();
            int r = -1;
            int c = -1;
            int path_count = 1;
            p.AddRow();
            for (int i = 0; i < 2; i++)
            {
                p.AddColumn(-1, path_count - 1);
            }
            p[path_count - 1, 0] = path_row_0;
            p[path_count - 1, 1] = path_col_0;


            bool done = false;
            //חוזר על השלבים עד שלא מוצא 1 בעמודה  
            while (!done)
            {
                //מוצא 0 מסומן בעמודה 
                find_star_in_col(p[path_count - 1, 1], masck.GetLength(0), ref r, masck);
                //אם יש
                if (r > -1)
                {
                    path_count += 1;
                    //סימון שורה עם שיבוץ בעמודה מסומנת
                    p.AddRow();
                    for (int i = 0; i < 2; i++)
                    {
                        p.AddColumn(-1, path_count - 1);
                    }
                    p[path_count - 1, 0] = r;
                    p[path_count - 1, 1] = p[path_count - 2, 1];
                }
                else { done = true; }

                if (!done)
                {
                    c = find_prime_in_row(p[path_count - 1, 0], ref c, masck.GetLength(0), masck);
                    path_count += 1;
                    //בודק היכן יש 2 באותה שורה 
                    //ומסמן את העמודה
                    p.AddRow();
                    for (int i = 0; i < 2; i++)
                    {
                        p.AddColumn(-1, path_count - 1);
                    }
                    p[path_count - 1, 0] = p[path_count - 2, 0];//שורה בעמודה מסומנת
                    p[path_count - 1, 1] = c;//עמודה בשורה מסומנת באפס ראשוני
                }
            }

            Update_after_constructore_path(p, path_count, masck);
            clear_covers(RowCover);
            clear_covers(ColCover);
            erase_primes_not_in_pathcostructore(masck);

            return 3;
        }
        //  מוצא אלמנט מינמלי מכל האיברים שגם השורה שלהם וגם העמודה שלהם לא מכוסים
        public static int find_smallest(ref int minval,
         DynamicMatrix<int> mat,
         bool[] RowCover,
         bool[] ColCover, int row, int column)
        {
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    if (RowCover[r] == false && ColCover[c] == false)
                    {
                        if (minval > mat[r, c])
                            minval = mat[r, c];
                    }
                }
            }
            return minval;
        }
        //מחסר מהעמודות הלא מכוסות ומוסיף לשורות המכוסות חוזר לשלב 4 עד לפתרון
        public static int step6(DynamicMatrix<int> mat,
         bool[] rowcover,
         bool[] ColCover, int row, int col
         )
        {
            int minval = int.MaxValue;
            minval = find_smallest(ref minval, mat, rowcover, ColCover, row, col);


            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    if (rowcover[r])
                        mat[r, c] += minval;
                    if (!ColCover[c])
                        mat[r, c] -= minval;
                }

            return 4;
        }


    }
}


