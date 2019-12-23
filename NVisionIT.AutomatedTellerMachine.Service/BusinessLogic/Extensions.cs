using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NVisionIT.AutomatedTellerMachine.Service.BusinessLogic
{
    public static class Extensions
    {
        /// <summary>
        /// Takes the source and destination object types and maps them using reflection
        /// Usefull for transforming DTOs to Objects and Vice Versa
        /// Note that both object must have the same properties
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        private static void MatchAndMap<TSource, TDestination>(this TSource source, TDestination destination)
           where TSource : class, new()
           where TDestination : class, new()
        {
            if (source != null && destination != null)
            {
                List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
                List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                    if (destinationProperty != null)
                    {
                        try
                        {
                            destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Calls Match and Map method
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapProperties<TDestination>(this object source)
            where TDestination : class, new()
        {
            var destination = Activator.CreateInstance<TDestination>();
            MatchAndMap(source, destination);

            return destination;
        }
    }
}
