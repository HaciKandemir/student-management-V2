using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Student_Management_V2
{
    class mStudent
    {
        public string TC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public string GetUserFriendlyString()
        {
            return "TC No:" + TC + ", \t\tAd:" + FirstName + ", \t\tSoyad:" + LastName;
        }

        public List<string> FilledProperty()
        {
            List<string> propString = new List<string>();
            var stdProperties = GetType().GetProperties().Where(x => x.GetValue(this) != null);
            foreach (var stdProp in stdProperties)
            {
                propString.Add(stdProp.Name + ": " + stdProp.GetValue(this));
            }
            return propString;
        }

    }
}
