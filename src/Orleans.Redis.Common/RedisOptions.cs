using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orleans.Configuration
{
    public class RedisOptions : IOptions<RedisOptions>
    {
        public string ConnectionString { get; set; }

		RedisOptions IOptions<RedisOptions>.Value => this;
	}
}
