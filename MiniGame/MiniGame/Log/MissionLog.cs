﻿using System;
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

            strOutput += "\n\"Treasure\": [";
            for (int i = 0; i < treasurelist.Count; i++)
            {
                strOutput += treasurelist[i].convertToJson() + ",\n";
               
            }
            strOutput += "],";
            //strOutput += "\n\"Monster\": [";
            //for (int i = 0; i < monsterList.Count; i++)
            //{
            //    if(monsterList[i] is Mummy)
            //    {
            //        strOutput += monsterList[i].convertToJson() + ",\n";
            //    }
            //}
            //strOutput += "]";
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
        public bool outJsonFile(string fileName)
        {

            string json = "{" + toJson() + "}";
            
            System.IO.File.WriteAllText(fileName, json);

            return true;
        }
    }
}