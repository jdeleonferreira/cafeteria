\# AI Skills



This folder contains reusable skills for AI-assisted development in the Cafeteria monorepo.



These skills are intended for GitHub Copilot, coding agents, and AI assistants working on the project.



\## Purpose



The goal is to make AI-generated changes consistent with the project's architecture, conventions, and testing strategy.



\## Rules



\- Skills are guidance, not source code.

\- Skills must stay short and operational.

\- Skills must not duplicate full documentation.

\- Skills must be updated when architecture decisions change.

\- AI-generated code must always be reviewed.



\## Skill usage



Use the relevant skill depending on the task:



\- `clean-architecture`: project boundaries and dependency rules

\- `ddd-pragmatic`: entities, value objects, aggregates, domain events

\- `application-use-cases`: commands, queries, handlers, validators

\- `efcore-persistence`: DbContext, configurations, migrations

\- `aspnetcore-api`: endpoints, versioning, errors, Swagger

\- `testing-dotnet`: unit, integration, and architecture tests

\- `documentation`: docs and ADRs



\## Golden rule



If a generated change violates Clean Architecture, reject it even if the code compiles.

