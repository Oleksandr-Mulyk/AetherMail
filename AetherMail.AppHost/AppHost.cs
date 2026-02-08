var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AetherMail_Web>("aethermail-web");

builder.Build().Run();
