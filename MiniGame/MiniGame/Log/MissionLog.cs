using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class MissionLog
    {
        Map map;
        List<Treasure> treasurelist;
        List<Unit> monsterList;


        public MissionLog()
        {

        }

        public void init(Map map, List<Treasure> treasures, List<Unit> monsters)
        {
            this.map = map;
            this.treasurelist = treasures;
            this.monsterList = monsters;
        }

        private string toJson()
        {
            string strOutput = "";
            strOutput += map.convertToJson() + ",";

            strOutput += "\n\"Tools\": [";
            for (int i = 0; i < treasurelist.Count; i++)
            {
                if (treasurelist[i] is Tool)
                {
                    strOutput += treasurelist[i].convertToJson() + ",\n";
                }
            }
            strOutput += "],";
            strOutput += "\n\"Weapons\": [";
            for (int i = 0; i < treasurelist.Count; i++)
            {
                if (treasurelist[i] is Weapon)
                {
                    strOutput += treasurelist[i].convertToJson() + ",\n";
                }
            }
            strOutput += "],";
            strOutput += "\n\"Jewelrys\": [";
            for (int i = 0; i < treasurelist.Count; i++)
            {
                if (treasurelist[i] is Jewelry )
                {
                    strOutput += treasurelist[i].convertToJson() + ",\n";
                }
            }
            strOutput += "],";
            strOutput += "\n\"Statues\": [";
            for (int i = 0; i < treasurelist.Count; i++)
            {
                if (treasurelist[i] is Statue)
                {
                    strOutput += treasurelist[i].convertToJson() + ",\n";
                }
            }
            strOutput += "],";
            strOutput += "\n\"Mummies\": [";
            for (int i = 0; i < monsterList.Count; i++)
            {
                if (monsterList[i] is Mummy)
                {
                    strOutput += monsterList[i].convertToJson() + ",\n";
                }
            }
            strOutput += "],";
            strOutput += "\n\"Scorpions\": [";
            for (int i = 0; i < monsterList.Count; i++)
            {
                if (monsterList[i] is Scorpion)
                {
                    strOutput += monsterList[i].convertToJson() + ",\n";
                }
            }
            strOutput += "],";
            strOutput += "\n\"Zombies\": [";
            for (int i = 0; i < monsterList.Count; i++)
            {
                if (monsterList[i] is Zombie)
                {
                    strOutput += monsterList[i].convertToJson() + ",\n";
                }
            }
            strOutput += "]";

            for(int i = 0; i < strOutput.Length; i++)
            {
                i = strOutput.IndexOf(",\n]");
                strOutput = strOutput.Remove(i, 2);
            }

            return strOutput;
        }
        public bool outJsonFile()
        {

            string fileName = createFileName();
            string json = "{" + toJson() + "}";
            
            System.IO.File.WriteAllText(fileName, json);

            return true;
        }

        private string createFileName()
        {
            DateTime time = DateTime.Now;
            string fileName = "";
            fileName += time.Year
                + "-" + time.Month
                + "-" + time.Day
                + "-" + time.Hour + "_" + time.Minute + "_" + time.Second + ".json";
            return fileName;
        }
    }
}