using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Define
    {
        /// <summary>
        /// 職業類型，內部數字按照資料庫ID排序.
        /// </summary>
        public enum RoleProfession
        {
            //戰士
            Warrior = 0,
            //法師
            Mage,
            //職業數量
            Count

        }
    }
}
