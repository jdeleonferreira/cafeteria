

\---



\# `.ai/skills/aspnetcore-api/SKILL.md`



```md

\# Skill: ASP.NET Core API



\## Purpose



Use this skill when creating or reviewing API endpoints.



The API layer exposes use cases over HTTP. It does not contain business logic.



\## API responsibilities



Api may contain:



\- Minimal APIs or Controllers

\- Routing

\- Authentication

\- Authorization

\- Swagger/OpenAPI

\- Exception handling

\- Health checks

\- Request/response mapping

\- Dependency injection composition



\## API must not contain



\- Business rules

\- EF Core queries

\- Direct DbContext usage

\- SQL Server-specific logic

\- Domain mutation logic

\- Large orchestration logic



\## Endpoint rules



Endpoints should:



\- Accept request models

\- Validate or delegate validation

\- Call Application use cases

\- Return appropriate HTTP responses

\- Use consistent error handling



Endpoints should not:



\- Load aggregates directly

\- Execute EF queries

\- Decide business rules

\- Construct complex domain object graphs directly



\## Versioning



Use API versioning when endpoints become public/stable.



Do not overcomplicate versioning before the API has real consumers.



\## Swagger/OpenAPI



Swagger should:



\- Document endpoints

\- Show request/response models

\- Include auth requirements when relevant



Do not rely on Swagger as the only documentation for business behavior.



\## Authentication and authorization



Auth setup belongs in Api composition.



Authorization decisions may be coordinated by Application when tied to use cases.



Do not hardcode role logic randomly inside endpoints.



\## Error handling



Use global exception handling.



Avoid repeated try/catch blocks in each endpoint.



\## Checklist



Before finishing:



\- Does the endpoint call Application only?

\- Is business logic outside Api?

\- Is the HTTP response appropriate?

\- Is validation handled?

\- Is Swagger affected?

\- Are integration tests needed?

