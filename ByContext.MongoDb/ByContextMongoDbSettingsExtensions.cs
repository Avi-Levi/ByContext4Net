using System.Linq;
using ByContext.ConfigurationDataProviders;
using ByContext.Model;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ByContext
{
    public static class ByContextMongoDbSettingsExtensions
    {
        public static IByContextSettings AddFromCollection(this IByContextSettings settings,
                                                         MongoCollection<Section> collection)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Section)))
            {
                // Register Section in BsonClassMap to ignore automatically-added "_id" property
                BsonClassMap.RegisterClassMap<Section>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }

            var provider = new ConvertFromSectionDataProvider(() => collection.FindAll().ToList(), settings);
            return settings.AddConfigurationDataProvider(provider);
        }
    }
}
