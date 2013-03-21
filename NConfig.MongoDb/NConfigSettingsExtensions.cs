using System.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NConfig.ConfigurationDataProviders;
using NConfig.Model;

namespace NConfig
{
    public static class NConfigSettingsExtensions
    {
        public static INConfigSettings AddFromCollection(this INConfigSettings settings,
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
