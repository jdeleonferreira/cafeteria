\# Skill: Clean Architecture



\## Purpose



Use this skill when creating, reviewing, or refactoring backend code in the Cafeteria API.



The goal is to keep business logic independent from frameworks, databases, and delivery mechanisms.



\## Project layers



Backend projects:



\- `Cafeteria.Domain`

\- `Cafeteria.Application`

\- `Cafeteria.Infrastructure`

\- `Cafeteria.Api`

\- `Cafeteria.Shared`



\## Dependency rules



Allowed dependencies:



```text

Domain -> nothing

Application -> Domain

Infrastructure -> Application, Domain

Api -> Application, Infrastructure

