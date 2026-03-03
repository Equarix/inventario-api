using invetario_api.database;
using invetario_api.Modules.config.dto;
using invetario_api.Modules.config.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.config
{
    public class ConfigService : IConfigService
    {
        private Database _db;

        public ConfigService(Database db)
        {
            _db = db;
        }

        public Task<Config> createConfig(ConfigDto data)
        {
            var config = new Config
            {
                enterpriseName = data.enterpriseName!,
                contactEmail = data.contactEmail!,
                ruc = data.ruc!,
                address = data.address!,
                phone = data.phone!,
                logoUrl = data.logoUrl!,
                localCurrency = data.localCurrency!
            };

            _db.configs.Add(config);
            _db.SaveChanges();

            return Task.FromResult(config);
        }

        public Task<List<Config>> getConfigs()
        {
            var configs = _db.configs.ToListAsync();
            return configs;
        }

        public Task<Config?> getLast()
        {
            var config = _db.configs.OrderByDescending(c => c.createdAt).FirstOrDefaultAsync();
            return config;
        }
    }
}
