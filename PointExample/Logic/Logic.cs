using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointExample.Logic
{
    class Logic
    {
        private List<string> maleFirstNames = new List<string>();
        private List<string> maleMiddleNames = new List<string>();
        private List<string> maleLastNames = new List<string>();

        private List<string> femaleFirstNames = new List<string>();
        private List<string> femaleMiddleNames = new List<string>();
        private List<string> femaleLastNames = new List<string>();

        private List<string> Sex = new List<string>();

        private void CreationTestData ()
        {
            maleFirstNames.AddRange(new string[] { "Александр", "Алексей", "Борис", 
                "Виктор", "Георгий", "Григорий", "Константин", 
                "Иван", "Евгений", "Сергей" });
            maleLastNames.AddRange(new string[] { "Иванов", "Петров", "Сидоров",
                "Александров", "Капилевич", "Батыгин", "Самсонов",
                "Егоров", "Школьник", "Баранов"});
            maleMiddleNames.AddRange(new string[] { "Александрович", "Алексеевич", "Борисович",
                "Викторович", "Георгиевич", "Григорьевич", "Константинович",
                "Иванович", "Евгеньевич", "Сергеевич" });

            femaleFirstNames.AddRange(new string[] { "Александра", "Варвава", "Вероника",
                "Галина", "Дарья", "Елена", "Жанна",
                "Зоя", "Ирина", "Лия" });
            femaleLastNames.AddRange(new string[] { "Иванова", "Петрова", "Сидорова",
                "Александрова", "Койхман", "Батыгина", "Самсонова",
                "Егорова", "Пустова", "Баранова"});
            femaleMiddleNames.AddRange(new string[] { "Александровна", "Алексеевна", "Борисовна",
                "Викторовна", "Георгиевна", "Григорьевна", "Константиновна",
                "Ивановна", "Евгеньевна", "Сергеевна" });

            Sex.AddRange(new string[] { "Мужской", "Женский" });
        }

         
        public void FillingTables ()
        {
            maleNames
        }
    }
}
