using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathProjectChallenge.Service.Infrastructure.Mapper.Models
{
    /// <summary>
    /// Represents a settings model
    /// </summary>
    public partial interface ISettingsModel
    {
        /// <summary>
        /// Gets or sets an active store scope configuration (store identifier)
        /// </summary>
        int ActiveStoreScopeConfiguration { get; set; }
    }
}
