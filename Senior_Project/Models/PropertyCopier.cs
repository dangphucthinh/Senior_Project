using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models
{
    public class PropertyCopier<TParent, TChild> where TParent : class
                                            where TChild : class
    {
        public static void Copy(TParent from, TChild to)
        {
            var parentProperties = from.GetType().GetProperties();
            var childProperties = to.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(to, parentProperty.GetValue(from));
                        break;
                    }
                }
            }
        }
        public static void Copy(TParent from, TChild to, List<string> exclusion)
        {
            var parentProperties = from.GetType().GetProperties();
            var childProperties = to.GetType().GetProperties();
            //List<string> e = new List<string>(exclusion);
            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (!exclusion.Exists(s => s.Equals(parentProperty.Name)) && !exclusion.Exists(s => s.Equals(childProperty.Name)))
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            childProperty.SetValue(to, parentProperty.GetValue(from));
                            break;
                        }
                }
            }
        }
        public static bool Compare(TParent from, TChild to, List<string> exclusion)
        {
            var parentProperties = from.GetType().GetProperties();
            var childProperties = to.GetType().GetProperties();
            //List<string> e = new List<string>(exclusion);
            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (!exclusion.Exists(s => s.Equals(parentProperty.Name)) && !exclusion.Exists(s => s.Equals(childProperty.Name)))
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            //childProperty.SetValue(to, parentProperty.GetValue(from));
                            //break;
                            if (!childProperty.PropertyType.IsArray && !parentProperty.PropertyType.IsArray)
                            {
                                var childVal = childProperty.GetValue(to);
                                var parentVal = parentProperty.GetValue(from);
                                if (childVal == null || parentVal == null)
                                    continue;
                                if (childVal.GetType() == typeof(DateTime) && parentVal.GetType() == typeof(DateTime))
                                {
                                    if (!childVal.ToString().Equals(parentVal.ToString()))
                                        return false;
                                }
                                else
                                {
                                    if (!(childVal.Equals(parentVal)))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                }
            }
            return true;
        }
    }
}