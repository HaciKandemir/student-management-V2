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

        public override string ToString()
        {
            return string.Format("TC no: {0} Ad: {1} Soyad: {2} Doğum Tarihi: {3}", TC, FirstName, LastName, BirthDate.Value.ToShortDateString());
        }
        /*public string GetUserFriendlyString()
        {
            return string.Format("TC no: {0} Ad: {1} Soyad: {2} Doğum Tarihi: {3}", TC, FirstName, LastName, BirthDate);
        }*/

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
