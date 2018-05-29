﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace SampleHost
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = JobHostBuilder.CreateDefault()
                .UseConsoleLifetime();

            var jobHost = builder.Build();

            await jobHost.RunAsync();

            //var config = new JobHostConfiguration();
            //config.Queues.VisibilityTimeout = TimeSpan.FromSeconds(15);
            //config.Queues.MaxDequeueCount = 3;
            //config.LoggerFactory = new LoggerFactory().AddConsole();

            //if (config.IsDevelopment)
            //{
            //    config.UseDevelopmentSettings();
            //}

            //CheckAndEnableAppInsights(config);

            //var host = new JobHost(config);
            //host.RunAndBlock();
        }

        private static void CheckAndEnableAppInsights(JobHostConfiguration config)
        {
            // If AppInsights is enabled, build up a LoggerFactory
            string instrumentationKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");
            if (!string.IsNullOrEmpty(instrumentationKey))
            {
                var filter = new LogCategoryFilter();
                filter.DefaultLevel = LogLevel.Debug;
                filter.CategoryLevels[LogCategories.Results] = LogLevel.Debug;
                filter.CategoryLevels[LogCategories.Aggregator] = LogLevel.Debug;

                // Adjust the LogLevel for a specific Function.
                filter.CategoryLevels[LogCategories.CreateFunctionCategory(nameof(Functions.ProcessWorkItem))] = LogLevel.Debug;

                config.LoggerFactory = new LoggerFactory()
                    .AddApplicationInsights(instrumentationKey, filter.Filter)
                    .AddConsole(filter.Filter);
            }
        }
    }
}
