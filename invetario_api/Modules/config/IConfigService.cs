using invetario_api.Modules.config.dto;
using invetario_api.Modules.config.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.config
{
    public interface IConfigService
    {
        Task<List<Config>> getConfigs();

        Task<Config?> getLast();

        Task<Config> createConfig(ConfigDto data);

    }
}
