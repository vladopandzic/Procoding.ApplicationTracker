var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Procoding_ApplicationTracker_Api>("procoding-applicationtracker-api");

builder.Build().Run();
