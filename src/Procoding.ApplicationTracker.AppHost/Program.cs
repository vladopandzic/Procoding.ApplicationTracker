var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Procoding_ApplicationTracker_Api>("procoding-applicationtracker-api");

builder.AddProject<Projects.Procoding_ApplicationTracker_Web_Root>("procoding-applicationtracker-web-root");

builder.Build().Run();
