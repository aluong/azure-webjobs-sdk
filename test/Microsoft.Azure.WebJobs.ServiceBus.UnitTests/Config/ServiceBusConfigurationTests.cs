﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.WebJobs.Host;
using Xunit;

namespace Microsoft.Azure.WebJobs.ServiceBus.UnitTests.Config
{
    public class ServiceBusConfigurationTests
    {
        [Fact]
        public void Constructor_SetsExpectedDefaults()
        {
            ServiceBusConfiguration config = new ServiceBusConfiguration();
            Assert.Equal(16, config.MessageOptions.MaxConcurrentCalls);
            Assert.Equal(0, config.PrefetchCount);
        }

        [Fact]
        public void ConnectionString_ReturnsExpectedDefaultUntilSetExplicitly()
        {
            ServiceBusConfiguration config = new ServiceBusConfiguration();

            string defaultConnection = AmbientConnectionStringProvider.Instance.GetConnectionString(ConnectionStringNames.ServiceBus);
            Assert.Equal(defaultConnection, config.ConnectionString);

            string testConnection = "testconnection";
            config.ConnectionString = testConnection;
            Assert.Equal(testConnection, config.ConnectionString);
        }

        [Fact]
        public void PrefetchCount_GetSet()
        {
            ServiceBusConfiguration config = new ServiceBusConfiguration();
            Assert.Equal(0, config.PrefetchCount);
            config.PrefetchCount = 100;
            Assert.Equal(100, config.PrefetchCount);
        }
    }
}
