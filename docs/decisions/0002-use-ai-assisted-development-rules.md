\# ADR 0002: Use EF Core with Fluent API



\## Status



Accepted



\## Context



The project uses EF Core for persistence and SQL Server as the initial database provider.



The domain model should remain free of persistence-specific attributes.



\## Decision



Use EF Core Fluent API configurations in Infrastructure.



Entity configurations must live outside Domain.



\## Consequences



Positive:



\- Domain remains persistence-ignorant.

\- Database mappings are centralized.

\- Future provider changes are easier.

\- Complex mappings are easier to control.



Negative:



\- More configuration files.

\- Developers must keep mappings in sync with domain changes.



\## Rules



\- Do not use EF Core attributes in Domain.

\- Use one configuration class per entity where practical.

\- Keep migrations in Infrastructure.

\- Keep SQL Server-specific setup inside Infrastructure.

