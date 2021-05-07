using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PointExample.Logic
{
    class Logic
    {
        private string[] maleFirstNames;
        private string[] maleMiddleNames;
        private string[] maleLastNames;

        private string[] femaleFirstNames;
        private string[] femaleMiddleNames;
        private string[] femaleLastNames;

        private string[] Sex;

        private void CreationTestData ()
        {
            maleFirstNames = new string[] { "Александр", "Алексей", "Борис", 
                "Виктор", "Георгий", "Григорий", "Константин", 
                "Иван", "Евгений", "Сергей" };
            maleLastNames = new string[] { "Иванов", "Петров", "Сидоров",
                "Александров", "Капилевич", "Батыгин", "Самсонов",
                "Егоров", "Школьник", "Баранов"};
            maleMiddleNames = new string[] { "Александрович", "Алексеевич", "Борисович",
                "Викторович", "Георгиевич", "Григорьевич", "Константинович",
                "Иванович", "Евгеньевич", "Сергеевич" };

            femaleFirstNames = new string[] { "Александра", "Варвава", "Вероника",
                "Галина", "Дарья", "Елена", "Жанна",
                "Зоя", "Ирина", "Лия" };
            femaleLastNames = new string[] { "Иванова", "Петрова", "Сидорова",
                "Александрова", "Койхман", "Батыгина", "Самсонова",
                "Егорова", "Пустова", "Баранова"};
            femaleMiddleNames = new string[] { "Александровна", "Алексеевна", "Борисовна",
                "Викторовна", "Георгиевна", "Григорьевна", "Константиновна",
                "Ивановна", "Евгеньевна", "Сергеевна" };

            Sex = new string[] { "Мужской", "Женский" };
        }

         
        public void FillCustomerTables (int countUsers)
        {
            string customerSql = "INSERT INTO dbo.Customers(ID,LastName,FirstName,MiddleName,Sex,BirthDate,RegistrationDate) VALUES(@pr1,@pr2,@pr3,@pr4,@pr5,@pr6,@pr7)";
            
            Random rnd;
            DateTime date, dateReg;
            for (decimal i = 0; i < countUsers; i++)
            {
                rnd = new Random();
                date = new DateTime(rnd.Next(1946, 2020), rnd.Next(1, 12), rnd.Next(1, 30));
                dateReg = new DateTime(rnd.Next(2010, 2020), rnd.Next(1, 12), rnd.Next(1, 30));

                SqlCommand cmd = new SqlCommand(customerSql);
                cmd.Parameters.AddWithValue("@pr1", i);
                
                if (i%2==0)
                {
                    cmd.Parameters.AddWithValue("@pr2", maleLastNames.GetValue(rnd.Next(10)));
                    cmd.Parameters.AddWithValue("@pr3", maleFirstNames.GetValue(rnd.Next(10)));
                    cmd.Parameters.AddWithValue("@pr4", maleMiddleNames.GetValue(rnd.Next(10)));
                    cmd.Parameters.AddWithValue("@pr5", Sex.GetValue(0));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pr2", maleLastNames.GetValue(rnd.Next(10)));
                    cmd.Parameters.AddWithValue("@pr3", maleFirstNames.GetValue(rnd.Next(10)));
                    cmd.Parameters.AddWithValue("@pr4", maleMiddleNames.GetValue(rnd.Next(10)));
                    cmd.Parameters.AddWithValue("@pr5", Sex.GetValue(0));
                }
                cmd.Parameters.AddWithValue("@pr6", date);
                cmd.Parameters.AddWithValue("@pr7", dateReg);
            }
        }

        public void FillOrderTables(int countOrders, int countUsers)
        {
            Random rnd;
            DateTime orderDate;

            string countSql = "INSERT INTO dbo.Orders(ID,CustomerID,OrderDate,Price) VALUES(@pr1,@pr2,@pr3,@pr4)";

            for (decimal i = 0; i < countOrders; i++)
            {
                SqlCommand cmd = new SqlCommand(countSql);
                rnd = new Random();

                cmd.Parameters.AddWithValue("@pr1", i);
                cmd.Parameters.AddWithValue("@pr2", rnd.Next(countUsers));


            }
         }
    }
}
