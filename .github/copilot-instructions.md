# Copilot instructions for this repository

Purpose
- Help AI coding agents be productive quickly: explain architecture, patterns, key files, and build/run notes.

Big picture
- This is an ASP.NET Core Web API using Entity Framework Core with a single ApplicationDbContext ([project/Data/ApplicationDbContext.cs](project/Data/ApplicationDbContext.cs)).
- HTTP entrypoints are Controllers under [project/Customer/Controllers](project/Customer/Controllers) and [project/Manage/Controller](project/Manage/Controller). Controllers call Services which use Repositories to access EF models.
- Dependency injection configuration lives in [project/Program.cs](project/Program.cs). Add or update service registrations there.

Key patterns & conventions (concrete)
- Controller → Service → Repository pattern. Example: [project/Customer/Controllers/UserController.cs](project/Customer/Controllers/UserController.cs) calls `IUserService` implemented in [project/Customer/Services/UserService.cs](project/Customer/Services/UserService.cs), which uses `IUserRepository`.
- Repositories talk directly to `ApplicationDbContext` (EF DbSets defined in [project/Data/ApplicationDbContext.cs](project/Data/ApplicationDbContext.cs)). Example repository: [project/Manage/Repository/DonationRepository.cs](project/Manage/Repository/DonationRepository.cs).
- DTOs are defined per-area under `*/Dtos` (e.g., [project/Customer/Dtos](project/Customer/Dtos) and [project/Manage/Dtos](project/Manage/Dtos)). Use these DTOs for controller input/output.
- Migrations are in [project/Migrations](project/Migrations). Prefer EF Core migrations for schema changes.

Startup / DI / Configuration
- Database registration and connection string are handled in [project/Program.cs](project/Program.cs). Example uses `UseSqlServer(...)` with a hard-coded connection string—prefer moving secrets to `appsettings.json` or environment variables when changing.
- Add services and repositories to DI by editing [project/Program.cs](project/Program.cs). Many services are registered with `AddScoped<TInterface, TImpl>()`.

Build / Run / Debug
- Build and run locally from the `project` folder:

  dotnet build
  dotnet run

- Swagger/OpenAPI is enabled (see [project/Program.cs](project/Program.cs)). Use the Swagger UI when running in Development.

Project-specific notes
- Many service & repository registrations are manually added in Program.cs — when introducing a new service or repository, register it there.
- Models use EF configurations in `OnModelCreating` inside [project/Data/ApplicationDbContext.cs](project/Data/ApplicationDbContext.cs). Follow existing property constraints (e.g., HasMaxLength(50)).
- Some code contains inline comments in Hebrew — mind encoding and meaning when modifying.
- No test project detected; add unit/integration tests in a new test project mirroring the `project` namespace if needed.

Integration points
- SQL Server via EF Core `UseSqlServer` (connection string in Program.cs currently).
- Swagger/OpenAPI enabled.

What AI agents should do first (practical checklist)
- Read [project/Program.cs](project/Program.cs) to understand DI and DB setup.
- Read [project/Data/ApplicationDbContext.cs](project/Data/ApplicationDbContext.cs) to understand domain entities and constraints.
- Inspect Controllers in [project/Customer/Controllers](project/Customer/Controllers) and Services in [project/Customer/Services] to follow common request handling.
- When modifying schema, create EF migrations under [project/Migrations].

Examples to copy-paste
- Register a new service: add `builder.Services.AddScoped<IMyService, MyService>();` to [project/Program.cs](project/Program.cs).
- Query via DbContext: `await _context.MyEntities.Where(e => e.IsActive).ToListAsync();` (follow null checks shown in existing repositories).

If anything here is unclear, ask for which file or behavior you'd like expanded and I'll update this document.
