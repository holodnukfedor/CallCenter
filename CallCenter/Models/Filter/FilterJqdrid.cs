using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CallCenter.Models.Filter
{
    public class FilterJqdrid
    {
        public string groupOp { get; set; }

        public List<FilterRuleJqgrid> rules { get; set; }

        public static FilterJqdrid DeserializeJson(string jsonData)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Deserialize<FilterJqdrid>(jsonData);
        }
    }
}