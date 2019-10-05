using Microsoft.Extensions.Options;
using Orleans.Redis.Common;
using Orleans.Runtime;
using System;

namespace Orleans.Configuration
{
    public enum PersistenceLifetime
    {
        ServiceLifetime = 0,
        ClusterLifetime = 1
    }

    public class RedisGrainStorageOptions : RedisOptions, IOptions<RedisGrainStorageOptions>
    {
        /// <summary>
        /// Stage of silo lifecycle where storage should be initialized.  Storage must be initialized prior to use.
        /// </summary>
        public int InitStage { get; set; } = DEFAULT_INIT_STAGE;
        public const int DEFAULT_INIT_STAGE = ServiceLifecycleStage.ApplicationServices;

        public PersistenceLifetime PersistenceLifetime { get;set; } = DEFAULT_PERSISTENCE_LIFETIME;
        public const PersistenceLifetime DEFAULT_PERSISTENCE_LIFETIME = PersistenceLifetime.ServiceLifetime;

        public bool ThrowExceptionOnInconsistentETag { get; set; } = true;

        public DateTime? ExpirationDate { get; set; }
        public TimeSpan? ExpiresAfter { get; set; }

		RedisGrainStorageOptions IOptions<RedisGrainStorageOptions>.Value => this;
	}

    public class RedisGrainStorageOptionsValidator : IConfigurationValidator
    {
        private readonly RedisGrainStorageOptions options;
        private readonly string name;

        public RedisGrainStorageOptionsValidator(IOptions<RedisGrainStorageOptions> options, string name)
        {
            this.options = options.Value;
            this.name = name;
        }

        public void ValidateConfiguration()
        {
            if (String.IsNullOrEmpty(options.ConnectionString))
                throw new OrleansConfigurationException(
                    $"{nameof(RedisGrainStorageOptions)} on stream provider {this.name} is invalid. {nameof(RedisGrainStorageOptions.ConnectionString)} is invalid");
        }
    }
}
