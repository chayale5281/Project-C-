using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using Dto;
namespace Bll

{
    public class Seconde
    {
        dbEntities db = new dbEntities();
        int c, row;
        int[] countwor = new int[6];
        int[] countold = new int[6];
   
        int dayswor = 0;
        public int DifficultOfOldAndExperienceOfWor(int difficulty, int experience)
        {
            //הרמה בין הקושי ליכולת העובד תואמת
            if (difficulty == experience || difficulty == 2 && experience == 3 || difficulty == 1 && experience == 3)
                return 150;
            if (difficulty == 3 && experience == 2 || difficulty == 2 && experience == 1)
                return 60;
            //בכל מקרה אחר 
            return 0;
        }
        public int Gender(string gendero, string genderw)
        {
            //אם המין שווה
            if (gendero == genderw)
                return 80;
            else
                return 20;
        }
        public int Age(int agew, int ageo)
        {
            //בודקים אם הגיל מספיק טוב בשביל לעבוד עם קשיש
            if (agew < 60 && agew > 18)
                return 100;
            if (ageo == agew && ageo > 75)
                return 20;
            else
                return 40;
        }
        //פונקציה שמקבלת מטריצת ניקוד והופכת את ערכיה לשלילים
        public DynamicMatrix<int> FromPositiveToneNgativeMatrix(DynamicMatrix<int> mat)
        {


            int[,] matr = { { 0,0 }, { 0, 0 } };
            const int v = 0;
            for (int i = v; i < mat.Rows; i++)
            {
                for (int wor = 0; wor < mat.Columns; wor++)

                {
                    mat[i, wor] *= (-1);
                }
            }

            return mat;
        }

        public void TheSecond()
        {
            //לשמות האזורים
            string[] arraria = new string[6];
            //מילון שממוין לפי אזורים כל הקשישים והעובדים שבאזור ממוינים לפי זה
            Dictionary<string, DynamicMatrix<int>> Dictionaryaria = new Dictionary<string, DynamicMatrix<int>>();
            //מופע לאוביקט ממחלק של אילוצי זקן
            Constraintsold co = new Constraintsold();
            //מופע לאוביקט ממחלק של אילוצי עובד
            Constraintsforeigenworker cf = new Constraintsforeigenworker();
            List<Placement> lp = new List<Placement>();
            //מטריצת עזר לשמירת הערכים הקודמים שלפני השיבוץ
            DynamicMatrix<int> temp = new DynamicMatrix<int>();
            //משתנה שבודק האם היה שיבוץ
            List<Constraintsforeigenworker> list_worker = new List<Constraintsforeigenworker>();
            List<Constraintsold> list_old = new List<Constraintsold>();
            bool isplacment = false;
            //מספר ימי עבודה שיכולים להיות בסך הכללי
            int numwork = 7;
            //להכנסה לשיבוץ הסופי אילו ימים שובצו 
            string[] days = new string[numwork];
            //לבדוק שלא ישבצו יותר ממה שאפשר
            bool flagp = true;
            int count2 = 0;
            int numofwoincity = 0;
            int countday = 0;
            int[] numofoldtowor;
            Days_of_oldDto dayo= new Days_of_oldDto();
            //מופע לימי עובד מסוג Dto
            Days_of_wor2Dto daysw = new Days_of_wor2Dto();
            List <Placement> lplacment = new List<Placement>();
            //עובר על כל האזורים ועבור כל אזור שם במערך השמות את שם העיר 
            //  וכן עבור כל אזור סוכם את מספר העובדים והקשישים
            //ושם במילון את שם העיר ומטריצה כמספר העובדים והקשישים שבאותו אזור
            for (int i = 0; i < 6; i++)
            {

                arraria[i] = db.city.ToList()[i].namecity;
                string name = arraria[i];
                int idcity1 = db.city.FirstOrDefault(xi => xi.namecity == name).idcity;
                numofwoincity = db.Constraintsforeigenworker.Where(xi => xi.idcity == idcity1).Count();
                int numofoincity = db.Constraintsold.Where(xi => xi.idcity == idcity1).Count();
                Dictionaryaria.Add(arraria[i], new DynamicMatrix<int>(numofwoincity, numofoincity));
            }
            for (int i = 0; i < 6; i++)
            {
                
                c = -1;
                row = -1;

                flagp = true;
               
               
                numofoldtowor = new int[countwor[i]];
                string name = arraria[i];
                int idcity1 = db.city.FirstOrDefault(xi => xi.namecity == name).idcity;
                int numwors = numofwoincity = db.Constraintsforeigenworker.Where(xi => xi.idcity == idcity1).Count();
                int numofoincity = db.Constraintsold.Where(xi => xi.idcity == idcity1).Count();
                int numolds = numofoincity;
                //מכניס לרשימה את העובדים שבאותו אזור
                list_worker = db.Constraintsforeigenworker.Where(xi => xi.idcity == idcity1).ToList();
                //מכניס לרשימה את הזקנים שבאותו אזור
                list_old = db.Constraintsold.Where(xi => xi.idcity == idcity1).ToList();
                //מאתחל את temp
                temp = new DynamicMatrix<int>();
                for (int wor = 0; wor < numofwoincity; wor++)
                {
                    //בודק מיהו העובד ומכניסו לאובייקט
                    //אוביקט שלתוכו נכנסים ימי העובד 
                    //בודק מיהם ימי העובד ע"פ הid 
                    days_of_wor2 dd = new days_of_wor2();
                    days_of_wor2 dw = db.days_of_wor2.Where(xi => xi.confid == cf.confid).FirstOrDefault();
                    daysw=Days_of_wor2Dto.DalToDto(dw);
                    for (int old = 0; old < numofoincity; old++)
                    {
                        //מוצא זקן מכניסו לאובייקט
                        co = list_old[old];
                        int countdayold = 0;    
                       //אוביקט שלתוכו נכנסים ימי הקשיש 
                        //בודק מיהם ימי העובד ע"פ הid 
                        days_of_old2 dold = db.days_of_old2.Where(xi => xi.conoID == co.conoID).FirstOrDefault();
                        dayo=Days_of_oldDto.DalToDto(dold);
                        //--------------day-----------------//
                        //עבור כליום מתאים מוסיפים ניקוד של 100
                        for (int day = 0; day < 7; day++)
                        {
                            if(daysw.days[day]&&dayo.days[day])
                            
                            {
                                Dictionaryaria[arraria[i]][wor, old] += 100;
                                countdayold++;
                            }
                        }

                        //  Dictionhourwor.Add(count,daywor);
                        //אם מספר הימיים ששווה אצל הקשיש והעבד קטן משתיים אין שיבוץ כלל
                        if (countdayold < 2)
                            continue;
                        //האם המין שווה
                        //------------gender---------------//
                        if (cf.gender == co.gender)
                            Dictionaryaria[arraria[i]][wor, old] += 100;
                        else
                            Dictionaryaria[arraria[i]][wor, old] += Gender((string)co.gender, (string)cf.gender);
                        //האם הגיל
                        //------------age---------------//

                        Dictionaryaria[arraria[i]][wor, old] += Age((int)cf.age_of_o, (int)co.age);
                        //האם השפה
                        //-------------langry-----------//
                        if (cf.languagefw == co.languageold)
                            Dictionaryaria[arraria[i]][wor, old] += 100;
                        //אם הרמת קושי של הקשיש שווה 1 נוסיף ניקוד מועט
                        if (cf.languagefw != co.languageold && co.Level_of_functioningo == 1)
                            Dictionaryaria[arraria[i]][wor, old] += 60;
                        else
                            Dictionaryaria[arraria[i]][wor, old] += 0;
                        //אם רמת הקושי תואמת
                        //--------------Level_of_functioningfg-------//
                        if (cf.Level_of_functioningfg == co.Level_of_functioningo)
                            Dictionaryaria[arraria[i]][wor, old] += 150;
                        else
                            Dictionaryaria[arraria[i]][wor, old] += DifficultOfOldAndExperienceOfWor((int)co.Level_of_functioningo, (int)cf.Level_of_functioningfg);
                        //האם הסכום תואם
                        //---------------money_of_hour-------------//
                        if (cf.money_of_hour == co.hanacha_and_money_for_hour)
                            Dictionaryaria[arraria[i]][wor, old] += 100;
                        if (cf.money_of_hour <= co.hanacha_and_money_for_hour)
                            Dictionaryaria[arraria[i]][wor, old] += 100;
                        else
                            Dictionaryaria[arraria[i]][wor, old] += 40;



                        //האם השעות
                        //---------------- houres------------//
                        if (dw != null && dold != null)
                        {

                            if (dw.hourstartw <= dold.hourstarto && dold.hourendo >= dw.hourendw)
                                Dictionaryaria[arraria[i]][wor, old] += 80;
                            //אם ההפרש הוא לא יותר מ4 שעות מוסיפים ניקוד מועט
                            if ((dw.hourstartw - dold.hourstarto) == 2 && (dw.hourendw - dold.hourendo) == 2)
                                Dictionaryaria[arraria[i]][wor, old] += 60;
                            else
                                Dictionaryaria[arraria[i]][wor, old] += 0;
                        }
                        //אם אין התאמה בין השעות אין כלל ניקוד
                    }
                }

                
                
                //שמירת המטריצה במטריצת עזר לשימש לאחר השיבוץ
                temp = new DynamicMatrix<int>(Dictionaryaria[arraria[i]], temp);
                //שליחה לפונקציה שמשנה את ערכי המטריצה מחיוי לשלילי כי האלגוריתם עובד על ערכי מינימום
                Dictionaryaria[arraria[i]] = FromPositiveToneNgativeMatrix(Dictionaryaria[arraria[i]]);
                //בדיקה האם מספר השורות והעמודות במטריצה גדול מ0 כי אחרת אין שיבוץ
                if (Dictionaryaria[arraria[i]].Rows > 0 && numofoincity != 0)
                {
                    //בדיקה אם גמרנו לשבץ וכן אם מספר הזקנים לא גדול ממספר העובדים ביותר מ- 3n  כי אחרת אין שיבוץ 
                    //כי עובד אחד יכול להיות עובד של מקסימום 3 זקנים
                    if (Dictionaryaria[arraria[i]].Rows >= (Dictionaryaria[arraria[i]].Columns) / 3 && flagp)
                    {
                        //המערך שמכיל את תוצאות השיבוץ
                        int[] arrschuing = hungarianAlgorithem.CompleteMatrix(Dictionaryaria[arraria[i]]);

                        //לאחר השיבוץ שומרים את הערכים בטבלת השיבוץ
                        for (int wor = 0; wor < numofwoincity; wor++)
                        //המקרים בהם לא יהיה שיבוץ :
                        //1.מספר הזקנים גדול ממספר העובדים
                        //2.היה ניקוד של 0
                        //בודק אם היה שיבוץ -1 שמים אם לא היה שיבוץ
                        {
                            if (arrschuing[wor] != -1)
                            {
                                Placement p = new Placement();
                                int idwor = list_worker[wor].confid;
                                //מיקום הקשיש נמצא כערך במער במיקום העובד
                                int old = arrschuing[wor];
                                int idold = list_old[old].conoID;
                                int years = 2010, months = 07, dayss = 1;
                                int yeare = 2022, monthe = 08, daye = 31;
                                DateTime ds = new DateTime(years, months, dayss++);
                                DateTime de = new DateTime(yeare, monthe, daye--);
                                old ol = db.old.Where(xi => xi.conoID == idold).FirstOrDefault();

                                if (ol != null)
                                {
                                    idold = ol.idold;
                                    string nameold = ol.fnameold + " " + ol.lnameold;
                                    p.nameold = ol.fnameold + " " + ol.lnameold;
                                }

                                Foreign_worker wo = db.Foreign_worker.Where(xi => xi.confid == idwor).FirstOrDefault();
                                if (wo != null)
                                {
                                    idwor = wo.idwor;
                                    string namewor = wo.fnwor + " " + wo.lnwor;
                                    p.namewor = wo.fnwor + " " + wo.lnwor;
                                }

                                if (daye == 1)
                                    daye += 30;
                                days_of_wor2 dw = db.days_of_wor2.Where(xi => xi.confid == idwor).FirstOrDefault();
                                daysw = Days_of_wor2Dto.DalToDto(dw);
                                days_of_old2 dold = db.days_of_old2.Where(xi => xi.conoID == idold).FirstOrDefault();
                                dayo = Days_of_oldDto.DalToDto(dold);
                                for (int day = 0; day < numwork; day++)
                                {
                                    if (dayo.days[day]&&daysw.days[day])
                                    {
                                        days[day] = switchs(day);
                                        p.dayes += days[day] + ',';
                                    }
                                }

                                years += 1;
                                yeare += 1;
                                months += 1;
                                monthe += 1;




                                p.idold = idold;
                                p.idwor = idwor;
                                p.Start_Datep = ds;
                                p.end_Datep = de;




                                db.Placement.Add(p);
                                db.SaveChanges();
                            }
                        }
                        // }  
                        isplacment = true;//לשנות לבליון
                                       //לולא זאת עובדת כל עוד מספר הקשישים גדול מהעובדים וכן שהיה לפחות שיבוץ אחד וכן שמספר העובדים לא שווה אפס
                        while (numofwoincity < numofoincity && isplacment || numofwoincity != 0)
                        {

                            Dayes day = new Dayes();
                            //כאן שולחים את המטיצה temp לפונקציה שעוברת על הימים ומחזירה פונקציה בהתאם לכך
                            Dictionaryaria[arraria[i]] = day.RemoveDays(ref temp, ref numofwoincity, ref numofoincity, arrschuing, ref list_old, ref list_worker, countday);

                            //  temp[daysw, dayso] = 0;
                            // Dictionaryaria[arraria[i]] = temp;


                            countday = 0;

                            Dictionaryaria[arraria[i]] = FromPositiveToneNgativeMatrix(Dictionaryaria[arraria[i]]);
                            arrschuing = hungarianAlgorithem.CompleteMatrix(Dictionaryaria[arraria[i]]);
                            for (int wor = 0; wor < numofwoincity; wor++)
                            {
                                //בודק אם היה שיבוץ -1 שמים אם לא היה שיבוץ
                                if (arrschuing[wor] != -1)
                                {
                                    int idwor = list_worker[wor].confid;
                                    int old = arrschuing[wor];
                                    int idold = list_old[old].conoID;
                                    int years = 2010, months = 07, dayss = 1;
                                    int yeare = 2022, monthe = 08, daye = 31;
                                    DateTime ds = new DateTime(years, months, dayss++);
                                    DateTime de = new DateTime(yeare, monthe, daye--);
                                    old ol = db.old.Where(xi => xi.conoID == idold).FirstOrDefault();
                                    string nameold = "";
                                    string namewor = "";
                                    if (ol != null)
                                    {
                                        idold = ol.idold;
                                        nameold = ol.fnameold + " " + ol.lnameold;
                                    }
                                    Foreign_worker wo = db.Foreign_worker.Where(xi => xi.confid == idwor).FirstOrDefault();
                                    if (wo != null)
                                    {
                                        idwor = wo.idwor;
                                        namewor = wo.fnwor + " " + wo.lnwor;
                                    }

                                    if (daye == 1)
                                        daye += 30;
                                    years += 1;
                                    yeare += 1;
                                    months += 1;
                                    monthe += 1;
                                    Placement p = new Placement();
                                    p.idold = idold;
                                    p.idwor = idwor;
                                    countwor = new int[numwork]; countold = new int[numwork];
                                    days_of_wor2 dw = db.days_of_wor2.Where(xi => xi.confid == idwor).FirstOrDefault();
                                    daysw = Days_of_wor2Dto.DalToDto(dw);
                                    days_of_old2 dold = db.days_of_old2.Where(xi => xi.conoID == idold).FirstOrDefault();
                                    dayo = Days_of_oldDto.DalToDto(dold);
                                    for (int dayw = 0; dayw < numwork; dayw++)
                                    {
                                        if (dayo.days[dayw] && daysw.days[dayw])
                                        {
                                            days[dayw] = switchs(dayw);
                                            p.dayes += days[dayw] + ',';
                                        }
                                    }
                                    p.Start_Datep = ds;
                                    p.end_Datep = de;
                                    p.nameold = nameold;
                                    p.namewor = namewor;
                                    isplacment=true;
                                    db.Placement.Add(p);
                                    db.SaveChanges();
                                    lplacment.Add(p);
                                }

                                else
                                {
                                    isplacment = false;
                                }
                            }
                            
                           
                        }
                        flagp=false;//לאחר שגמרנו לשבץ את כל הימים השיבוץ נגמר ולא ניתן לשבץ שוב
                        //לשנות לbool
                    }
                    //מאתחלים באפס


                }
            }
        }

        private void DalToDto(days_of_wor2 dw)
        {
            throw new NotImplementedException();
        }

        private void DalToDto(days_of_old2 dold)
        {
            throw new NotImplementedException();
        }

        public string switchs(int day)
        {
            string days = "";
            string[] d = { "ראשון","שני","שלישי","רביעי","חמישי","שישי","שבת" };
            return d[day];
            
        }
    }
}














