# ADR 0001: Use Clean Architecture



## Status



Accepted



## Context



The Cafeteria webapp needs a maintainable backend that can grow without coupling business rules to frameworks, databases, or delivery mechanisms.



The backend uses:



- ASP.NET Core

- EF Core

- SQL Server initially

- Clean Architecture

- Pragmatic DDD



## Decision



Use Clean Architecture with these projects:



- Cafeteria.Domain

- Cafeteria.Application

- Cafeteria.Infrastructure

- Cafeteria.Api



Dependency direction:





Api -> Application

Api -> Infrastructure

Infrastructure -> Application

Infrastructure -> Domain

Application -> Domain

Domain -> nothing

