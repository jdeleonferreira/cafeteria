\# Database Strategy



\## Purpose



Define how persistence is handled in the Cafeteria webapp.



\## Current database



The initial database engine is SQL Server.



SQL Server is an infrastructure detail. It must not leak into Domain or Application.



\## Persistence rules



\- EF Core is used as ORM.

\- DbContext lives in Infrastructure.

\- EF Core configurations use Fluent API.

\- Migrations live in Infrastructure.

\- Domain entities must not use EF Core attributes.

\- Application defines persistence contracts when needed.

\- Infrastructure implements those contracts.



\## DbContext



The DbContext acts as the Unit of Work.



Avoid creating a generic UnitOfWork abstraction unless a real need appears.



\## Repository strategy



Repositories are allowed only when they add value.



Good reasons:



\- Complex aggregate loading

\- Domain-specific persistence operations

\- Encapsulating query complexity

\- Avoiding EF Core leakage into Application



Bad reasons:



\- Creating repositories for every table

\- Wrapping DbSet with no added value

\- Creating fake enterprise layers



\## SQL Server coupling rule



Allowed in Infrastructure:



\- SQL Server provider setup

\- SQL Server connection string

\- SQL Server migrations

\- SQL Server-specific indexes if needed



Forbidden outside Infrastructure:



\- SQL Server types

\- SQL Server-specific queries

\- Raw SQL assumptions

\- Provider-specific behavior



\## Future database change



If the database engine changes, expected impact should be limited to:



\- Infrastructure configuration

\- EF Core provider

\- Migrations

\- Some persistence optimizations



Domain and Application should remain stable.

