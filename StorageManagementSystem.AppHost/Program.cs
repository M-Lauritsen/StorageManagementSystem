var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddSqlServer("sql").AddDatabase("db");

builder.AddProject<Projects.StorageManagement_API>("storagemanagement-api")
    .WithReference(db);

builder.Build().Run();
