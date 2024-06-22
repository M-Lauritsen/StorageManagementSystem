var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.StorageManagement_API>("storagemanagement-api");

builder.Build().Run();
