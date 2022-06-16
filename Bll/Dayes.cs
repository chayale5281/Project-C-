using System.Collections.Generic;
using System.Linq;
using Dal;
using Dto;
namespace Bll

{
    public class Dayes
    {
        dbEntities db = new dbEntities();
        //פונקציה שאחרית לאפס במטריצה את השיבוצים שהיו.
        public DynamicMatrix<int> RemoveDays(ref DynamicMatrix<int> temp, ref int row, ref int column, int[] arr, ref List<Constraintsold> list_old, ref List<Constraintsforeigenworker> list_wor, int countday)
        {
            int r = row, c = column, countdas = 0,numWorking = 7;
            // לימים מופע לאוביקט ימים מסוג dto
            Days_of_oldDto dayo = new Days_of_oldDto();
            //מופע לימי עובד מסוג Dto
            Days_of_wor2Dto daysw = new Days_of_wor2Dto();
            for (int dayw = 0; dayw < row; dayw++)
            {
                Constraintsforeigenworker cf = list_wor[dayw];
                days_of_wor2 dw = db.days_of_wor2.Where(xi => xi.confid == cf.confid).FirstOrDefault();
                daysw = Days_of_wor2Dto.DalToDto(dw);
                for (int dayso = 0; dayso < column; dayso++)
                {
                    //מאתחלים מערך לזקן והקשיש הנוכחים ב-1
                    if (arr[dayw] == dayso) {
                        Constraintsold co = list_old[dayso];
                        days_of_old2 dao = db.days_of_old2.Where(xi => xi.conoID == co.conoID).FirstOrDefault();
                        dayo = Days_of_oldDto.DalToDto(dao);
                        for (int i = 0; i < numWorking; i++)
                        {
                            if (daysw.days[i] == dayo.days[i] && daysw.days[i]) {
                                daysw.days[i] = false;
                                dayo.days[i] = false;
                            }
                        }
                    }
                    //פונקציה שבודקת אם ימי הזקן כולם שובצו בשיבוץ הנוכחי
                    bool o = CheckIfAllDaysFalseOld(dayo.days);
                    if (o) { 
                        //אם כן מורידים ממספר הקשישים
                        c--;
                        //פונציה שמאפסת את כל העמודה -כלומר כל העובדים לא יכולים להשתבץ לקשיש זה
                        RemoveColumnOld(dayso,  ref temp);}
                    //פונקציה שבודקת אם ימי העובד כולם שובצו בשיבוץ הנוכחי
                    bool w = CheckIfAllDaysFalseWor(daysw.days);
                    if (w) { 
                        //אם כן מורידים ממספר העובדים
                        r--;
                        RemoveRowWor(dayw, ref temp);
                        //פונציה שמאפסת את כל השורה -כלומר כל קשישים לא יכולים להשתבץ לעובד זה
                    }  
                    //מאפסים במטריצה את המיקום של העובד והקשיש שלא יוכלו להשתבץ שוב
                    temp[dayw, dayso] = 0;
                    //בודקים עבור העובד הנוכחי עבור כל זקן האם הימים שנשארו לעוד לאחר השיבוץ מתאימות לשאר הקשישים בלפחות 2 ימים אם לא מאפסים
                    RemoveOldWithDays(dayw, ref temp,cf,list_old,dayo,daysw);
                }

            }row = r; column = c;return temp;
        }
        
            
        
        
        // פונקציה שבה בודקים עבור העובד הנוכחי עבור כל זקן האם הימים שנשארו לעוד לאחר השיבוץ מתאימות לשאר הקשישים בלפחות 2 ימים אם לא מאפסים 
        public void RemoveOldWithDays(int wor, ref DynamicMatrix<int> temp, Constraintsforeigenworker cf, List<Constraintsold> lo, Days_of_oldDto dayo, Days_of_wor2Dto dw)
        {
            int countdays = 0, numWorking = 7;
            days_of_wor2 dw2 = db.days_of_wor2.Where(xi => xi.confid == cf.confid).FirstOrDefault();
            dw = Days_of_wor2Dto.DalToDto(dw2);
            for (int day = 0; day < temp.Columns; day++)
            {
                Constraintsold co = lo[day];
                days_of_old2 dao = db.days_of_old2.Where(xi => xi.conoID == co.conoID).FirstOrDefault();
                dayo = Days_of_oldDto.DalToDto(dao);
                countdays = 0;

                for (int i = 0; i < numWorking; i++)
                {
                    if (dayo.days[i] && dw.days[i])
                    {
                        countdays++;

                    }
                }

                if (countdays < 2)
                {

                    temp[wor, day] = 0;
                }
            }
        }
        //פונקציה שמאפסת שורה
        public void RemoveRowWor(int dayw, ref DynamicMatrix<int> temp)
        {
            for (int i = 0; i < temp.Columns; i++)
            {
                temp[dayw, i] = 0;
            }
        }
        
        //פונקציה שמאפסת עמודה
        public void RemoveColumnOld(int dayold, ref DynamicMatrix<int> temp)
        {
            for (int i = 0; i < temp.Rows; i++)
            {
                temp[i, dayold] = 0;
            }
        }
        public bool CheckIfAllDaysFalseOld(bool[] arr)
        {
            int numWorking = 7, count = 0;
            for (int i = 0; i < numWorking; i++)
            {
                if (!arr[i])
                    count++;
            }

            if (count == numWorking)
                return true;
            return false;
        }
        public bool CheckIfAllDaysFalseWor(bool[] arr)
        {
            int numWorking = 7, count = 0;
            for (int i = 0; i < numWorking; i++)
            {
                if (!arr[i])
                    count++;
            }

            if (count == numWorking)
                return true;
            return false;
        }

    }
}



