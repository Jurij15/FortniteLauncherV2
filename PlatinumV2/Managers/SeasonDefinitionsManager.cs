using PlatinumV2.Classes;
using PlatinumV2.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Management.Deployment.Preview;

namespace PlatinumV2.Managers
{
    public class SeasonDefinitionsManager
    {
        public List<FortniteSeasonBaseClass> GetAllSeasonDefinitions()
        {
            Type[] classes = GetClassesExtendingAbstractClass(typeof(FortniteSeasonBaseClass));

            List<FortniteSeasonBaseClass> instances = new List<FortniteSeasonBaseClass>();
            foreach (Type item in classes)
            {
                try
                {
                    FortniteSeasonBaseClass iinstance = (FortniteSeasonBaseClass)Activator.CreateInstance(item);
 
                    instances.Add(iinstance);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return instances;
        }

        private static Type[] GetClassesExtendingAbstractClass(Type abstractClass)
        {
            Assembly assembly = Assembly.Load("PlatinumV2");
            return assembly.GetTypes()
                .Where(type => abstractClass.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                .ToArray();
        }
    }
}
