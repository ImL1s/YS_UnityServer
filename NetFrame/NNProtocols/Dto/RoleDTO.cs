using System;
using System.Collections.Generic;
using System.Text;

namespace NNProtocols.Dto
{
    /// <summary>
    /// 已選擇角色DTO.
    /// </summary>
    [Serializable]
    public class RoleDTO
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public int Profession { get; set; }
        public byte LV { get; set; }
        public string Name { get; set; }
    }
}
